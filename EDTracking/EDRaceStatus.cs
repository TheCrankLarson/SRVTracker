using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Runtime.CompilerServices;

namespace EDTracking
{
    public class EDRaceStatus
    {        
        public int Heading { get; set; } = -1;
        public double SpeedInMS { get; set; } = 0;
        public double MaxSpeedInMS { get; set; } = 0;
        public long Flags { get; set; } = -1;
        public double Hull { get; set; } = 1;
        public DateTime TimeStamp { get; set; } = DateTime.MinValue;
        public bool Eliminated { get; set; } = false;
        public EDLocation Location { get; set; } = null;
        public int WaypointIndex { get; set; } = 0;
        public double DistanceToWaypoint { get; set; } = double.MaxValue;
        public double TotalDistanceLeft { get; set; } = double.MaxValue;
        public static bool Started { get; set; } = false;
        public bool Finished { get; set; } = false;
        public int PitStopCount { get; set; } = 0;
        public int RacePosition { get; set; } = 0;
        public DateTime StartTime { get; set; } = DateTime.MinValue;
        public DateTime FinishTime { get; set; } = DateTime.MaxValue;

        public string Commander { get; } = "";
        public static bool ShowDetailedStatus { get; set; } = false;


        private string _lastStatus = "";
        public delegate void StatusChangedEventHandler(object sender, string commander, string status);
        public static event StatusChangedEventHandler StatusChanged;
        private long _lastFlags = 0;
        private bool _inPits = false;
        private bool _lowFuel = false;
        private StringBuilder _raceHistory = new StringBuilder();
        private double _nextLogDistanceToWaypoint = double.MaxValue;
        private EDLocation _speedCalculationLocation = null;
        private DateTime _speedCalculationTimeStamp = DateTime.UtcNow;
        private double _lastSpeedInMs = 0;
        private double _lastLoggedMaxSpeed = 50;  // We don't log any maximum speeds below 50m/s
        private DateTime _pitStopStartTime = DateTime.MinValue;
        private DateTime _lastUnderShip = DateTime.MinValue;
        private DateTime _lastTouchDown = DateTime.MinValue;
        private DateTime _lastDockSRV = DateTime.MinValue;
        public NotableEvents notableEvents = null;
        private double[] _lastThreeSpeedReadings = new double[] { 0, 0, 0 };
        private int _oldestSpeedReading = 0;
        private string _status = "NA";
        private EDRace _race = null;
        
        public EDRaceStatus()
        {
        }

        public EDRaceStatus(EDEvent baseEvent)
        {
            // This constructor should only ever be called by the server
            Flags = baseEvent.Flags;
            Heading = baseEvent.Heading;
            TimeStamp = baseEvent.TimeStamp;
            Commander = baseEvent.Commander;
            if (baseEvent.HasCoordinates())
                Location = baseEvent.Location();
        }

        public EDRaceStatus(string commander, EDRace race, bool immediateStart = true)
        {
            _raceHistory = new StringBuilder();
            Commander = commander;
            _race = race;// Route = route;
            if (immediateStart)
                StartRace();
        }

        public void SetRace(EDRace race)
        {
            _race = race;
        }

        private string _lastHistoryLog = "";
        private void AddRaceHistory(string eventInfo)
        {
            if (eventInfo.Equals(_lastHistoryLog))
                return;
            string log = $"{TimeStamp:HH:mm:ss} {eventInfo}";
            _raceHistory.AppendLine(log);
            _lastHistoryLog = eventInfo;
        }

        public string RaceReport
        {
            get { return _raceHistory.ToString(); }
            set
            {
                _raceHistory = new StringBuilder();
                _raceHistory.Append(value);
            }
        }

        public override string ToString()
        {
            return _status;
        }

        private string StatusMessage(string messageName)
        {
            if (_race != null && _race.CustomStatusMessages.ContainsKey(messageName))
                return _race.CustomStatusMessages[messageName];
            if (EDRace.StatusMessages.ContainsKey(messageName))
                return EDRace.StatusMessages[messageName];
            return messageName;
        }

        private string GenerateStatus()
        {
            StringBuilder statusBuilder = new StringBuilder();

            if (Eliminated)
                statusBuilder.Append(StatusMessage("Eliminated"));
            else if (Finished)
            {
                TimeSpan timeSpan = FinishTime.Subtract(StartTime);
                statusBuilder.Append($"{StatusMessage("Completed")} ({timeSpan.ToString("hh\\:mm\\:ss")})");
            }           
            else if (_inPits)
            {
                statusBuilder.Append(StatusMessage("Pitstop"));
            }         

            if (statusBuilder.Length == 0 && Started)
            {
                if (_race?.Route != null)
                    statusBuilder.Append($"-> {_race.Route.Waypoints[WaypointIndex].Name}");
                if (_lowFuel)
                    statusBuilder.Append(" (low fuel)");
            }

            if (ShowDetailedStatus)
            {
                // Flags that apply to all vehicles
                if (isFlagSet(StatusFlags.Night_Vision_Active))
                    statusBuilder.Append(" NV");

                if (isFlagSet(StatusFlags.LightsOn))
                    statusBuilder.Append(" L");

                if (isFlagSet(StatusFlags.In_SRV))
                {
                    // SRV only flags

                    if (isFlagSet(StatusFlags.srvHighBeam))
                        statusBuilder.Append("H");
                    else
                        statusBuilder.Append("L");

                    if (isFlagSet(StatusFlags.Srv_Handbrake))
                        statusBuilder.Append(" HB");

                    if (isFlagSet(StatusFlags.Srv_DriveAssist))
                        statusBuilder.Append(" DA");
                }
                else if (isFlagSet(StatusFlags.In_MainShip))
                {
                    // Ship only flags
                    if (isFlagSet(StatusFlags.Landing_Gear_Down))
                        statusBuilder.Append(" LG");

                    if (!isFlagSet(StatusFlags.FlightAssist_Off))
                        statusBuilder.Append(" FA");

                    if (isFlagSet(StatusFlags.Silent_Running))
                        statusBuilder.Append(" SR");

                    if (isFlagSet(StatusFlags.Cargo_Scoop_Deployed))
                        statusBuilder.Append(" C");

                    if (isFlagSet(StatusFlags.Being_Interdicted))
                        statusBuilder.Append(" I");
                }
            }
            if (statusBuilder.Length == 0)
                statusBuilder.Append("NA");

            _status = statusBuilder.ToString();
            return _status;
        }

        public String DistanceToWaypointInKmDisplay
        {
            get
            {
                if (Eliminated || (DistanceToWaypoint == double.MaxValue))
                    return "NA";
                if (Finished) return "0";
                return $"{(DistanceToWaypoint/1000):F1}";
            }
        }

        public String TotalDistanceLeftInKmDisplay
        {
            get
            {
                if (Eliminated || (DistanceToWaypoint == double.MaxValue))
                    return "NA";
                if (Finished) return "0";
                return $"{(TotalDistanceLeft / 1000):F1}";
            }
        }

        public string HullDisplay
        {
            get
            {
                return $"{(Hull * 100):F0}";
            }
        }

        public void StartRace()
        {
            TimeStamp = DateTime.Now;
            Started = true;
            AddRaceHistory("Race started");
            WaypointIndex = 1;
            StatusChanged?.Invoke(null, Commander, this.ToString());
        }

        public bool Resurrect()
        {
            if (Eliminated)
            {
                TimeStamp = DateTime.Now;
                Eliminated = false;
                DistanceToWaypoint = double.MaxValue;
                _speedCalculationLocation = null;
                AddRaceHistory("Resurrected (elimination manually rescinded)");
                return true;
            }
            return false;
        }

        private bool isFlagSet(StatusFlags flag)
        {
            return ((Flags & (long)flag) == (long)flag);
        }

        private bool AllowPitStops()
        {
            if (_race == null)
                return true;
            return _race.AllowPitstops;
        }

        private bool EliminateOnDestruction()
        {
            if (_race == null)
                return false;
            return _race.EliminateOnVehicleDestruction;
        }

        private bool EliminateOnShipFlight()
        {
            if (_race == null)
                return false;
            return _race.SRVOnly;
        }

        public void UpdateStatus(EDEvent updateEvent)
        {
            // Update our status based on the passed event

            if ( (updateEvent.Flags > 0) && (Flags != updateEvent.Flags) )
            {
                _lastFlags = Flags;
                Flags = updateEvent.Flags;
            }
            if (updateEvent.TimeStamp>DateTime.MinValue)
                TimeStamp = updateEvent.TimeStamp;

            if (Finished || Eliminated)
                return;

            if (updateEvent.Health >= 0)
            {
                Hull = updateEvent.Health;
                AddRaceHistory($"Hull percentage: {Hull*100:F1}");
            }

            if (updateEvent.HasCoordinates())
            {
                TimeSpan timeBetweenLocations = updateEvent.TimeStamp.Subtract(_speedCalculationTimeStamp);
                if (timeBetweenLocations.TotalMilliseconds > 750)
                {
                    // We take a speed calculation once every 750 milliseconds

                    double speedInMS = 0;
                    if (_speedCalculationLocation != null)
                    {
                        double distanceBetweenLocations = EDLocation.DistanceBetween(_speedCalculationLocation, updateEvent.Location());
                        speedInMS = distanceBetweenLocations * 1000 / timeBetweenLocations.TotalMilliseconds;
                        if ((speedInMS - _lastSpeedInMs) > 200 && (timeBetweenLocations.TotalMilliseconds < 3000))
                        {
                            // If the speed increases by more than 200m/s in three seconds, this is most likely due to respawn (i.e. invalid)
                            speedInMS = 0;
                            _speedCalculationLocation = null;
                        }
                        else
                        {
                            _speedCalculationLocation = updateEvent.Location();
                            _speedCalculationTimeStamp = updateEvent.TimeStamp;
                        }
                    }
                    else
                    {
                        _speedCalculationLocation = updateEvent.Location();
                        _speedCalculationTimeStamp = updateEvent.TimeStamp;
                    }

                    _lastThreeSpeedReadings[_oldestSpeedReading] = speedInMS;
                    _oldestSpeedReading++;
                    if (_oldestSpeedReading > 2)
                        _oldestSpeedReading = 0;
                    SpeedInMS = (_lastThreeSpeedReadings[0] + _lastThreeSpeedReadings[1] + _lastThreeSpeedReadings[2]) / 3; // Returning an average of the last three readings should prevent blips
                }

                if (SpeedInMS > MaxSpeedInMS)
                {
                    MaxSpeedInMS = SpeedInMS;
                    if (MaxSpeedInMS > _lastLoggedMaxSpeed + 5)
                    {
                        AddRaceHistory($"New maximum speed: {MaxSpeedInMS:F1}m/s");
                        _lastLoggedMaxSpeed = MaxSpeedInMS;
                    }
                }

                _lastSpeedInMs = SpeedInMS;
                Location = updateEvent.Location();
            }

            if (!Started)
                return;

            if (WaypointIndex > 0)
            {
                DistanceToWaypoint = EDLocation.DistanceBetween(Location, _race.Route.Waypoints[WaypointIndex].Location);
                TotalDistanceLeft = _race.Route.TotalDistanceLeftAtWaypoint(WaypointIndex) + DistanceToWaypoint;
                if (_race.Route.Waypoints[WaypointIndex].LocationIsWithinWaypoint(Location))
                {
                    // Commander has reached the target waypoint
                    AddRaceHistory($"Arrived at {_race.Route.Waypoints[WaypointIndex].Name}");
                    WaypointIndex++;
                    if (WaypointIndex > _race.LeaderWaypoint)
                        _race.LeaderWaypoint = WaypointIndex;
                    if (WaypointIndex >= _race.Route.Waypoints.Count)
                    {
                        Finished = true;
                        FinishTime = DateTime.Now;
                        string raceTime = $"{FinishTime.Subtract(StartTime):hh\\:mm\\:ss}";
                        notableEvents?.AddStatusEvent("CompletedNotification", Commander, $" ({raceTime})");
                        AddRaceHistory($"Completed in {raceTime}");
                        WaypointIndex = 0;
                        DistanceToWaypoint = 0;
                    }
                }
            }

            if (updateEvent.EventName.Equals("SRVDestroyed") && EliminateOnDestruction())
            {
                // Eliminated
                Eliminated = true;
                AddRaceHistory("SRV destroyed");
                notableEvents?.AddStatusEvent("EliminatedNotification", Commander);
                DistanceToWaypoint = double.MaxValue;
                if (_race != null && WaypointIndex == _race.LeaderWaypoint)
                {
                    // We could have been the race leader, so we need to check that we still have the correct leader waypoint
                    int leaderWaypoint = 0;
                    foreach (EDRaceStatus status in _race.Statuses.Values)
                    {
                        if (!status.Eliminated)
                            if (status.WaypointIndex > leaderWaypoint)
                                leaderWaypoint = status.WaypointIndex;
                        if (status.Finished)
                            break; // If someone has finished they'll be at the last waypoint, so no need to check further
                    }
                    _race.LeaderWaypoint = leaderWaypoint;
                }
                SpeedInMS = 0;
                _speedCalculationLocation = null;
                Hull = 0;
            }

            if (updateEvent.EventName.Equals("ShipTargeted"))  // "$RolePanel2_unmanned; $cmdr_decorate:#name=Crank Larson;"
            {
                string commanderName = Commander;
                if (commanderName.StartsWith("cmdr", StringComparison.OrdinalIgnoreCase))
                    commanderName = commanderName.Substring(5);
                if (updateEvent.TargetedShipName.EndsWith($"{commanderName};", StringComparison.OrdinalIgnoreCase) && (_pitStopStartTime == DateTime.MinValue))
                    _pitStopStartTime = updateEvent.TimeStamp;
            }

            if (updateEvent.EventName.Equals("Touchdown") && !updateEvent.PlayerControlled)
            {
                _lastTouchDown = updateEvent.TimeStamp;
                if (DateTime.Now.Subtract(_pitStopStartTime).TotalSeconds > 120)
                    _pitStopStartTime = _lastTouchDown;
            }

            if (updateEvent.EventName.Equals("Liftoff"))
                _lastTouchDown = DateTime.MinValue;

            if (updateEvent.EventName.Equals("DockSRV"))
            {
                if (!Finished && AllowPitStops())
                {
                    // We only increase pitstop count on DockSRV
                    _lastDockSRV = updateEvent.TimeStamp;
                    PitStopCount++;
                    Hull = 1;
                    notableEvents?.AddStatusEvent("PitstopNotification", Commander);
                    _inPits = true;
                    if (_pitStopStartTime == DateTime.MinValue)
                        _pitStopStartTime = _lastDockSRV;
                }
            }

            if (updateEvent.EventName.Equals("LaunchSRV"))
            {
                if (!Finished && AllowPitStops())
                {
                    if (_pitStopStartTime > DateTime.MinValue)
                        AddRaceHistory($"Pitstop {PitStopCount} took {DateTime.Now.Subtract(_pitStopStartTime):mm\\:ss}");
                    else if (PitStopCount > 0)
                        AddRaceHistory($"Pitstop {PitStopCount} completed (time unknown)");

                    _pitStopStartTime = DateTime.MinValue;
                }
                _inPits = false;
                Hull = 1; // Hull is repaired when in ship, so ensure we have this set
            }

            if (DistanceToWaypoint<_nextLogDistanceToWaypoint)
            {
                AddRaceHistory($"{(DistanceToWaypoint / 1000):F1}km to {_race.Route.Waypoints[WaypointIndex].Name}");
                _nextLogDistanceToWaypoint = DistanceToWaypoint - 5000;
            }

            if (Flags > 0)
            {
                if (EliminateOnShipFlight())
                {
                    if (!isFlagSet(StatusFlags.In_SRV) && !isFlagSet(StatusFlags.Landed_on_planet_surface) && !isFlagSet(StatusFlags.Docked_on_a_landing_pad) )
                    {
                        Eliminated = true;
                        notableEvents?.AddStatusEvent("EliminatedNotification", Commander);
                        AddRaceHistory("Eliminated as detected not in SRV and not landed");
                        DistanceToWaypoint = double.MaxValue;
                        SpeedInMS = 0;
                        _speedCalculationLocation = null; 
                    }
                }

                if (_inPits)
                    if (isFlagSet(StatusFlags.In_SRV) && !isFlagSet(StatusFlags.Srv_UnderShip))
                        _inPits = false;

                _lowFuel = isFlagSet(StatusFlags.Low_Fuel);
            }

            GenerateStatus();
            if (_status.Equals(_lastStatus))
                return;

            AddRaceHistory(_status);
            _lastStatus = _status;
            StatusChanged?.Invoke(null, Commander, _status);
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);           
        }

        public static EDRaceStatus FromJson(string json)
        {
            try
            {
                if (!String.IsNullOrEmpty(json))
                    return (EDRaceStatus)JsonSerializer.Deserialize(json, typeof(EDRaceStatus));
            }
            catch
            {
                Debug.WriteLine(json);
            }
            return null;
        }
    }
}
