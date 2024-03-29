﻿using System;
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
        public double AverageSpeedInMS { get; set; } = 0;
        public long Flags { get; set; } = -1;
        public long Flags2 { get; set; } = -1;
        public byte[] Pips { get; set; } = new byte[3] { 4, 4, 4 };
        public double Hull { get; set; } = 1;
        public DateTime TimeStamp { get; set; } = DateTime.MinValue;
        public bool Eliminated { get; set; } = false;
        public EDLocation Location { get; set; } = null;
        public int WaypointIndex { get; set; } = 0;
        //public bool WaypointsMustBeVisitedInOrder = true;
        public int Lap { get; set; } = 1;
        public List<DateTime> LapEndTimes { get; set; } = new List<DateTime>();
        public int NumberOfWaypointsVisited { get; set; } = 0;
        public double DistanceToWaypoint { get; set; } = double.MaxValue;
        public double TotalDistanceLeft { get; set; } = double.MaxValue;
        public static bool Started { get; set; } = false;
        public bool Finished { get; set; } = false;
        public int PitStopCount { get; set; } = 0;
        public int RacePosition { get; set; } = 0;
        public DateTime StartTime { get; set; } = DateTime.MinValue;
        public DateTime FinishTime { get; set; } = DateTime.MaxValue;
        public DateTime LapStartTime { get; set; } = DateTime.MinValue;
        public List<TimeSpan> LapTimes { get; set; } = new List<TimeSpan>();
        public int FastestLap { get; set; } = 0;

        public string Commander { get; set; } = "";
        public static bool ShowDetailedStatus { get; set; } = false;


        private string _lastStatus = "";
        public delegate void StatusChangedEventHandler(object sender, string commander, string status);
        public static event StatusChangedEventHandler StatusChanged;
        private long _lastFlags = 0;
        private bool _inPits = false;
        private bool _lowFuel = false;
        private StringBuilder _raceHistory = new StringBuilder();
        private double _nextLogDistanceToWaypoint = double.MaxValue;
        private EDLocation _previousLocation = null;
        private EDLocation _speedCalculationPreviousLocation = null;
        private DateTime _speedCalculationTimeStamp = DateTime.UtcNow;
        private double _lastSpeedInMs = 0;
        private double _lastLoggedMaxSpeed = 50;  // We don't log any maximum speeds below 50m/s
        private DateTime _pitStopStartTime = DateTime.MinValue;
        private DateTime _lastTouchDown = DateTime.MinValue;
        private DateTime _lastDockSRV = DateTime.MinValue;
        public NotableEvents notableEvents = null;
        private double[] _lastThreeSpeedReadings = new double[] { 0, 0, 0 };
        private int _oldestSpeedReading = 0;
        private int _numberOfSpeedReadings = 0;
        private double _totalOfSpeedReadings = 0;
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

        public static readonly Dictionary<string, string> RaceReportDescriptions = new Dictionary<string, string>()
        {
            { "Commander", "Commander name" },
            { "Position", "Current position" },
            { "Speed", "Current speed" },
            { "Altitude", "Altitude" },
            { "MaxSpeed", "Maximum speed" },
            { "AverageSpeed", "Average speed" },
            { "Status", "Status" },
            { "NumberOfWaypointsVisited", "Number of waypoints visited" },
            { "DistanceToWaypoint", "Distance to the next waypoint" },
            { "TotalDistanceLeft", "Total distance left" },
            { "Hull", "Hull strength left" },
            { "Shield", "Shield status (up or down)" },
            { "CargoScoop", "Cargo scoop status (up or down)" },
            { "Pips", "Power Distributor settings" },
            { "Lap", "Current lap" },
            { "LastLapTime", "Time taken for last complete lap" },
            { "FastestLap", "Fastest lap" },
            { "FastestLapTime", "Fastest lap time" }
        };
        

        public Dictionary<string,string> Telemetry()
        {
            Dictionary<string, string> telemetry = new Dictionary<string, string>();
            telemetry.Add("Commander", Commander);
            telemetry.Add("Position", RacePosition.ToString());
            telemetry.Add("Speed", SpeedInMS.ToString("F1"));
            if (Location != null)
                telemetry.Add("Altitude", Location.Altitude.ToString("F0"));
            telemetry.Add("MaxSpeed", MaxSpeedInMS.ToString("F1"));
            telemetry.Add("AverageSpeed", AverageSpeedInMS.ToString("F1"));
            telemetry.Add("Status", _status);
            telemetry.Add("NumberOfWaypointsVisited", NumberOfWaypointsVisited.ToString());
            telemetry.Add("DistanceToWaypoint", DistanceToWaypointInKmDisplay);
            telemetry.Add("TotalDistanceLeft", TotalDistanceLeftInKmDisplay);
            telemetry.Add("Hull", HullDisplay);
            telemetry.Add("Shield", ShieldStatus());
            telemetry.Add("CargoScoop", CargoScoopStatus());
            telemetry.Add("Pips", String.Join(",", Pips));
            if (_race?.Laps > 0)
            {
                if (Lap>0)
                    telemetry.Add("Lap", Lap.ToString());
                else
                    telemetry.Add("Lap", "1");
                telemetry.Add("LastLapTime", LastLapTime().ToString("g"));
                if (FastestLap > 0)
                {
                    telemetry.Add("FastestLap", FastestLap.ToString());
                    telemetry.Add("FastestLapTime", FastestLapTime().ToString());
                }
            }
            return telemetry;
        }

        public string ShieldStatus()
        {
            if (isFlagSet(StatusFlags.Shields_Up))
                return "Up";
            return "Down";
        }

        public string CargoScoopStatus()
        {
            if (isFlagSet(StatusFlags.Cargo_Scoop_Deployed))
                return "Down";
            return "Up";
        }

        public void SetRace(EDRace race)
        {
            _race = race;
        }

        public TimeSpan LastLapTime()
        {
            if (LapEndTimes.Count < 1)
                return new TimeSpan(0);
            else if (LapEndTimes.Count == 1)
                return LapEndTimes[0].Subtract(StartTime);
            return LapEndTimes[LapEndTimes.Count - 1].Subtract(LapEndTimes[LapEndTimes.Count - 2]);
        }

        public TimeSpan FastestLapTime()
        {
            if (FastestLap < 1 || LapTimes.Count < 1)
                return new TimeSpan(0);

            return LapTimes[FastestLap - 1];
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
            if (Eliminated)
                _status = StatusMessage("Eliminated");
            else if (Finished)
                _status = StatusMessage("Finished");
            else if (_inPits)
                _status = StatusMessage("Pitstop");
            else
                _status = "";

            return _status;           
        }

        public string StatusFlagsPanel()
        {
            StringBuilder statusBuilder = new StringBuilder();

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

            return statusBuilder.ToString();
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
            TimeStamp = DateTime.UtcNow;
            Started = true;
            AddRaceHistory("Race started");
            WaypointIndex = 1;
            StatusChanged?.Invoke(null, Commander, this.ToString());
        }

        public bool Resurrect()
        {
            if (Eliminated)
            {
                TimeStamp = DateTime.UtcNow;
                Eliminated = false;
                DistanceToWaypoint = double.MaxValue;
                _speedCalculationPreviousLocation = null;
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
                return false;
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
            return !_race.ShipAllowed;
        }

        private void ValidateRaceLeader()
        {
            if (_race != null && _race.Leader.Eliminated)
            {
                // We were the race leader, so we need to work out the new leader
                EDRaceStatus leader = null;
                foreach (EDRaceStatus status in _race.Statuses.Values)
                {
                    if (!status.Eliminated)
                        if (leader == null || status.TotalDistanceLeft < leader.TotalDistanceLeft)
                            leader = status;
                    if (status.Finished)
                        break; // If someone has finished they'll be at the last waypoint, so no need to check further
                }
                _race.Leader = leader;
            }
        }

        private void ProcessTouchdownEvent(EDEvent updateEvent)
        {
            _lastTouchDown = updateEvent.TimeStamp;
            if (DateTime.UtcNow.Subtract(_pitStopStartTime).TotalSeconds > 120)
                _pitStopStartTime = _lastTouchDown;
        }

        private void ProcessShipTargetedEvent(EDEvent updateEvent)
        {
            string commanderName = Commander;
            if (commanderName.StartsWith("cmdr", StringComparison.OrdinalIgnoreCase))
                commanderName = commanderName.Substring(5);
            if (updateEvent.TargetedShipName.EndsWith($"{commanderName};", StringComparison.OrdinalIgnoreCase) && (_pitStopStartTime == DateTime.MinValue))
                _pitStopStartTime = updateEvent.TimeStamp;
        }

        private void Eliminate(string reason)
        {
            if (Eliminated || Finished)
                return;
            Eliminated = true;
            notableEvents?.AddStatusEvent("EliminatedNotification", Commander);
            AddRaceHistory($"Eliminated: {reason}");
            ValidateRaceLeader();
        }

        private void ProcessSRVDestroyedEvent()
        {
            // SRV destroyed
            if (EliminateOnDestruction())
                Eliminate("SRV destroyed");
            else
                AddRaceHistory("SRV destroyed");
        }

        private void ProcessFighterDestroyedEvent()
        {
            // Fighter destroyed 
            if (EliminateOnDestruction())
                Eliminate("Fighter destroyed");
            else
                AddRaceHistory("Fighter destroyed");
        }

        private void ProcessDockSRVEvent(EDEvent updateEvent)
        {
            if (!Finished && !Eliminated && AllowPitStops())
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

        private void ProcessSynthesisEvent(EDEvent updateEvent)
        {
            // We need to check what the synthesis is - if it is repair or refuel outside pits, this could be disqualification
            if (updateEvent.AdditionalData.StartsWith("Repair"))
            {
                Hull = 1;
                AddRaceHistory(updateEvent.AdditionalData);
                if (!AllowPitStops() || _inPits) return;

                // We aren't in pits and pit stops are required... This is elimination
                Eliminate("Synthesis repair used outside pits");
            }
            else if (updateEvent.AdditionalData.StartsWith("Fuel"))
            {
                AddRaceHistory(updateEvent.AdditionalData);
                if (!AllowPitStops() || _inPits) return;

                // We aren't in pits and pit stops are required... This is elimination
                Eliminate("Synthesis refuel used outside pits");
            }
        }

        private void ProcessLaunchSRVEvent()
        {
            if (!Finished && AllowPitStops())
            {
                if (_pitStopStartTime > DateTime.MinValue)
                    AddRaceHistory($"Pitstop {PitStopCount} took {DateTime.UtcNow.Subtract(_pitStopStartTime):mm\\:ss}");
                else if (PitStopCount > 0)
                    AddRaceHistory($"Pitstop {PitStopCount} completed (time unknown)");

                _pitStopStartTime = DateTime.MinValue;
            }
            _inPits = false;
            Hull = 1; // Hull is always 1 when leaving ship
        }

        private void ProcessFlags()
        {
            if (Flags < 1)
                return;

            if (_race != null && !(_race.SRVAllowed && _race.FighterAllowed && _race.ShipAllowed && _race.FeetAllowed))
            {
                // Not all locomotion is allowed, so check that the current one is valid
                if (isFlagSet(StatusFlags.In_SRV) || isFlagSet(StatusFlags.In_MainShip) || isFlagSet(StatusFlags.In_Fighter))
                {
                    // We have a valid vehicle flag, so check the vehicle is allowed
                    bool vehicleDisallowed = false;
                    if (!_race.SRVAllowed && isFlagSet(StatusFlags.In_SRV))
                        vehicleDisallowed = true;
                    if (!_race.ShipAllowed && isFlagSet(StatusFlags.In_MainShip))
                    {
                        if (_race.AllowPitstops)
                            vehicleDisallowed = !isFlagSet(StatusFlags.Landed_on_planet_surface) && !isFlagSet(StatusFlags.Docked_on_a_landing_pad);
                        else
                            vehicleDisallowed = true;
                    }
                    if (!_race.FighterAllowed && isFlagSet(StatusFlags.In_Fighter))
                        vehicleDisallowed = true;

                    if (vehicleDisallowed && !Eliminated)
                    {
                        Eliminate("Commander using invalid mode of transport");
                        DistanceToWaypoint = double.MaxValue;
                        SpeedInMS = 0;
                        _speedCalculationPreviousLocation = null;
                    }
                }
            }

            if (_inPits)
                if (isFlagSet(StatusFlags.In_SRV) && !isFlagSet(StatusFlags.Srv_UnderShip))
                    _inPits = false;

            _lowFuel = isFlagSet(StatusFlags.Low_Fuel);
        }

        private void ProcessUnorderedRouteLocationChange()
        {
            // The waypoints don't need to be visited in order, so we need to check if we are at any of them

            for (int i = 0; i < _race.Route.Waypoints.Count; i++)
            {
                if (!_race.WaypointVisited[i])
                {
                    EDWaypoint waypoint = _race.Route.Waypoints[i];
                    if (waypoint.WaypointHit(Location, _previousLocation))
                    {
                        // We've reached a waypoint
                        WaypointIndex = i;
                        AddRaceHistory($"Arrived at {waypoint.Name}");
                        NumberOfWaypointsVisited++;
                        _race.WaypointVisited[i] = true;

                        // Check if there are any waypoints left to visit (if not, race is finished)
                        if (NumberOfWaypointsVisited < _race.Route.Waypoints.Count)
                            return;

                        // If we get here, then all waypoints have been visited and so the race is finished
                        Finished = true;
                        FinishTime = DateTime.UtcNow;
                        string raceTime = $"{FinishTime.Subtract(StartTime):hh\\:mm\\:ss}";
                        notableEvents?.AddStatusEvent("CompletedNotification", Commander, $" ({raceTime})");
                        AddRaceHistory($"Completed in {raceTime}");
                        WaypointIndex = 0;
                        DistanceToWaypoint = 0;
                        return;
                    }
                }
            }
        }

        private void ProcessLocationChange()
        {
            if (_race == null)
                return;

            if (!_race.WaypointsMustBeVisitedInOrder)
            {
                ProcessUnorderedRouteLocationChange();
                return;
            }

            DistanceToWaypoint = EDLocation.DistanceBetween(Location, _race.Route.Waypoints[WaypointIndex].Location);

            int lapStartWaypoint = _race.LapStartWaypoint - 1;
            if (_race.Laps == 0)
                TotalDistanceLeft = _race.Route.TotalDistanceLeftAtWaypoint(WaypointIndex) + DistanceToWaypoint;
            else
            {
                // Total distance left needs to take into account the laps
                TotalDistanceLeft = _race.TotalDistanceLeftAtWaypoint(WaypointIndex, Lap) + DistanceToWaypoint;
                if (lapStartWaypoint < 0)
                    lapStartWaypoint = 0;
            }
            if ((_race.Leader == null) || (TotalDistanceLeft < _race.Leader.TotalDistanceLeft))
                _race.Leader = this;

            EDWaypoint previousWaypoint = null;
            if (WaypointIndex > 0)
                previousWaypoint = _race.Route.Waypoints[WaypointIndex - 1];
            else if (_race.Laps > 0)
                previousWaypoint = _race.Route.Waypoints[_race.Route.Waypoints.Count - 1];

            if (_race.Route.Waypoints[WaypointIndex].WaypointHit(Location, _previousLocation, previousWaypoint?.Location))
            {
                // Commander has reached the target waypoint
                NumberOfWaypointsVisited++;
                if (_race.Laps > 0)
                {

                    if (WaypointIndex != lapStartWaypoint)
                        AddRaceHistory($"Arrived at {_race.Route.Waypoints[WaypointIndex].Name} (lap {Lap})");
                    else
                    {
                        // We're at the start waypoint, so have completed a lap
                        DateTime lapEndTime = TimeStamp;
                        LapEndTimes.Add(lapEndTime);
                        TimeSpan thisLapTime = lapEndTime.Subtract(LapStartTime);
                        LapTimes.Add(thisLapTime);
                        LapStartTime = lapEndTime;
                        string lapTime = $"{thisLapTime:hh\\:mm\\:ss}";

                        if (Lap == 1)
                            FastestLap = 1;
                        else if (thisLapTime < FastestLapTime())
                            FastestLap = Lap;

                        Lap++;

                        // We've only finished if this lap number is greater than the number of laps
                        if (Lap > _race.Laps)
                        {
                            Finished = true;
                            if (!Eliminated)
                            {
                                FinishTime = DateTime.UtcNow;
                                string raceTime = $"{FinishTime.Subtract(StartTime):hh\\:mm\\:ss}";
                                notableEvents?.AddStatusEvent("CompletedNotification", Commander, $" ({raceTime})");
                                AddRaceHistory($"Completed in {raceTime}");
                            }
                            WaypointIndex = 0;
                            DistanceToWaypoint = 0;
                        }
                        else if (!Eliminated)
                        {
                            notableEvents?.AddStatusEvent("CompletedLap", Commander, $" {Lap - 1} ({lapTime})");
                            AddRaceHistory($"Completed lap {Lap - 1} in {lapTime}");
                        }
                        if (Lap > 2 && FastestLap == Lap - 1)
                            notableEvents?.AddStatusEvent("FastestLapNotification", Commander, lapTime);
                    }
                }
                else
                    AddRaceHistory($"Arrived at {_race.Route.Waypoints[WaypointIndex].Name}");

                WaypointIndex++;

                if ( (_race.Laps>0) && (WaypointIndex > _race.LapEndWaypoint) && (Lap<=_race.Laps) )
                {
                    WaypointIndex = lapStartWaypoint;
                }
                else if (WaypointIndex >= _race.Route.Waypoints.Count)
                {
                    if (!Eliminated)
                    {
                        Finished = true;
                        FinishTime = DateTime.UtcNow;
                        string raceTime = $"{FinishTime.Subtract(StartTime):hh\\:mm\\:ss}";
                        notableEvents?.AddStatusEvent("CompletedNotification", Commander, $" ({raceTime})");
                        AddRaceHistory($"Completed in {raceTime}");
                        WaypointIndex = 0;
                        DistanceToWaypoint = 0;
                    }
                }
            }

            if (DistanceToWaypoint < _nextLogDistanceToWaypoint)
            {
                AddRaceHistory($"{(DistanceToWaypoint / 1000):F1}km to {_race.Route.Waypoints[WaypointIndex].Name}");
                _nextLogDistanceToWaypoint = DistanceToWaypoint - 5000;
            }            
        }

        private void CalculateSpeed()
        {
            TimeSpan timeBetweenLocations = TimeStamp.Subtract(_speedCalculationTimeStamp);
            if (timeBetweenLocations.TotalMilliseconds > 750)
            {
                // We take a speed calculation once every 750 milliseconds

                double speedInMS = 0;
                if (_speedCalculationPreviousLocation != null)
                {
                    double distanceBetweenLocations = EDLocation.DistanceBetween(_speedCalculationPreviousLocation, Location);
                    speedInMS = distanceBetweenLocations * 1000 / (double)timeBetweenLocations.TotalMilliseconds;
                    if (isFlagSet(StatusFlags.In_SRV) && (speedInMS - _lastSpeedInMs) > 200 && (timeBetweenLocations.TotalMilliseconds < 3000))
                    {
                        // If the speed increases by more than 200m/s in three seconds, this is most likely due to respawn (i.e. invalid)
                        speedInMS = 0;
                        _speedCalculationPreviousLocation = null;
                    }
                    else
                    {
                        _speedCalculationPreviousLocation = Location.Copy();
                        _speedCalculationTimeStamp = TimeStamp;
                    }
                }
                else
                {
                    _speedCalculationPreviousLocation = Location.Copy();
                    _speedCalculationTimeStamp = TimeStamp;
                }

                // Update the total average speed
                _totalOfSpeedReadings += speedInMS;
                _numberOfSpeedReadings++;
                AverageSpeedInMS = _totalOfSpeedReadings / _numberOfSpeedReadings;

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
        }

        public void UpdateStatus(EDEvent updateEvent)
        {
            // Update our status based on the passed event

            if (updateEvent.TimeStamp>DateTime.MinValue)
                TimeStamp = updateEvent.TimeStamp;

            if (Finished)  // We keep tracking when eliminated in case of mistake (racers can be restored)
                return;

            if ((updateEvent.Flags > 0) && (Flags != updateEvent.Flags))
            {
                _lastFlags = Flags;
                Flags = updateEvent.Flags;
            }
            Flags2 = updateEvent.Flags2;

            Pips = (byte[])updateEvent.Pips.Clone();
            Heading = updateEvent.Heading;

            if (updateEvent.Health >= 0)
            {
                Hull = updateEvent.Health;
                AddRaceHistory($"Hull percentage: {Hull*100:F1}");
            }

            if (updateEvent.HasCoordinates())
            {
                _previousLocation = Location;
                Location = updateEvent.Location();
                ProcessLocationChange();
                CalculateSpeed();
            }

            if (!Started)
                return;

            ProcessFlags();

            switch (updateEvent.EventName)
            {
                case "SRVDestroyed":
                    ProcessSRVDestroyedEvent();
                    break;

                case "FighterDestroyed":
                    ProcessFighterDestroyedEvent();
                    break;

                case "ShipTargeted":
                    ProcessShipTargetedEvent(updateEvent);
                    break;

                case "Touchdown":
                    ProcessTouchdownEvent(updateEvent);
                    break;

                case "Liftoff":
                    _lastTouchDown = DateTime.MinValue;
                    break;

                case "DockSRV":
                    ProcessDockSRVEvent(updateEvent);
                    break;

                case "LaunchSRV":
                    ProcessLaunchSRVEvent();
                    break;

                case "Synthesis":
                    ProcessSynthesisEvent(updateEvent);
                    break;
                    
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
