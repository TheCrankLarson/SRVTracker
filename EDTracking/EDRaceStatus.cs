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
using System.Web.Script.Serialization;

namespace EDTracking
{
    public class EDRaceStatus
    {        
        public int Heading { get; internal set; } = -1;
        public double SpeedInMS { get; internal set; } = 0;
        public double MaxSpeedInMS { get; internal set; } = 0;
        public long Flags { get; internal set; } = -1;
        public DateTime TimeStamp { get; internal set; } = DateTime.MinValue;
        public bool Eliminated { get; internal set; } = false;
        public EDLocation Location { get; internal set; } = null;
        public int WaypointIndex { get; internal set; } = 0;
        public double DistanceToWaypoint { get; internal set; } = double.MaxValue;
        public EDRoute Route { get; set; } = null;
        public static bool Started { get; set; } = false;
        public bool Finished { get; set; } = false;
        public int PitStopCount { get; set; } = 0;
        public static DateTime StartTime { get; set; } = DateTime.MinValue;
        public DateTime FinishTime { get; internal set; } = DateTime.MinValue;

        public string Commander { get; } = "";
        public static bool EliminateOnDestruction { get; set; } = true;
        public static bool EliminateOnShipFlight { get; set; } = true;
        public static bool AllowPitStops { get; set; } = true;
        public static bool ShowDetailedStatus { get; set; } = false;


        public string _lastStatus = "";
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
        private bool _gotFirstSpeedReading = false;
        private double _lastLoggedMaxSpeed = 40;  // We don't log any maximum speeds below 40m/s
        private DateTime _pitStopStartTime = DateTime.MinValue;
        public NotableEvents notableEvents = null;

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

        public EDRaceStatus(string commander, EDRoute route, bool immediateStart = true)
        {
            _raceHistory = new StringBuilder();
            Commander = commander;
            Route = route;
            if (immediateStart)
                StartRace();
        }

        private void AddRaceHistory(string eventInfo)
        {
            _raceHistory.Append(DateTime.Now.ToString("HH:mm:ss "));
            //_raceHistory.Append("  ");
            _raceHistory.AppendLine(eventInfo);
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
            StringBuilder status = new StringBuilder();
            if (Eliminated)
                status.Append(EDRace.StatusMessages["Eliminated"]);
            else if (Finished)
            {
                TimeSpan timeSpan = FinishTime.Subtract(StartTime);
                status.Append($"{EDRace.StatusMessages["Completed"]} ({timeSpan.ToString("hh\\:mm\\:ss")})");
            }
            else if (_inPits)
                status.Append(EDRace.StatusMessages["Pitstop"]);
            else
            {
                if (Started)
                    status.Append($"-> {Route.Waypoints[WaypointIndex].Name}");

                if (_lowFuel)
                    status.Append(" (low fuel)");
            }

            //if (SpeedInMS >= 0)
            //    status.Append($" {SpeedInMS:0.0}m/s");



            if (ShowDetailedStatus)
            {
                // Flags that apply to all vehicles
                if (isFlagSet(StatusFlags.Night_Vision_Active))
                    status.Append(" NV");

                if (isFlagSet(StatusFlags.LightsOn))
                    status.Append(" L");

                if (isFlagSet(StatusFlags.In_SRV))
                {
                    // SRV only flags

                    if (isFlagSet(StatusFlags.srvHighBeam))
                        status.Append("H");
                    else
                        status.Append("L");

                    if (isFlagSet(StatusFlags.Srv_Handbrake))
                        status.Append(" HB");

                    if (isFlagSet(StatusFlags.Srv_DriveAssist))
                        status.Append(" DA");
                }
                else if (isFlagSet(StatusFlags.In_MainShip))
                {
                    // Ship only flags
                    if (isFlagSet(StatusFlags.Landing_Gear_Down))
                        status.Append(" LG");

                    if (!isFlagSet(StatusFlags.FlightAssist_Off))
                        status.Append(" FA");

                    if (isFlagSet(StatusFlags.Silent_Running))
                        status.Append(" SR");

                    if (isFlagSet(StatusFlags.Cargo_Scoop_Deployed))
                        status.Append(" C");

                    if (isFlagSet(StatusFlags.Being_Interdicted))
                        status.Append(" I");
                }


            }

            return status.ToString();
        }

        [ScriptIgnore]
        public String DistanceToWaypointDisplay
        {
            get
            {
                if (Eliminated || (DistanceToWaypoint == double.MaxValue))
                    return "NA";
                if (Finished) return "0";
                return $"{(DistanceToWaypoint/1000):F1}";
            }
        }

        public void StartRace()
        {
            Started = true;
            AddRaceHistory("Race started");
            WaypointIndex = 1;
            StatusChanged?.Invoke(null, Commander, this.ToString());
        }

        public bool Resurrect()
        {
            if (Eliminated)
            {
                Eliminated = false;
                AddRaceHistory("Resurrected (elimination manually rescinded)");
                return true;
            }
            return false;
        }

        private bool isFlagSet(StatusFlags flag)
        {
            return ((Flags & (long)flag) == (long)flag);
        }

        public void UpdateStatus(EDEvent updateEvent)
        {
            // Update our status based on the passed event

            _lastFlags = Flags;
            Flags = updateEvent.Flags;

            if (Finished || Eliminated)
                return;

            if (updateEvent.HasCoordinates())
            {
                TimeSpan timeBetweenLocations = updateEvent.TimeStamp.Subtract(_speedCalculationTimeStamp);
                if (timeBetweenLocations.TotalMilliseconds > 750)
                {
                    // We take a speed calculation once every 750 milliseconds
                    _speedCalculationTimeStamp = updateEvent.TimeStamp;
                    if (_speedCalculationLocation != null)
                    {
                        double distanceBetweenLocations = EDLocation.DistanceBetween(_speedCalculationLocation, updateEvent.Location());
                        SpeedInMS = distanceBetweenLocations * (1000 / timeBetweenLocations.TotalMilliseconds);
                    }
                    _speedCalculationLocation = updateEvent.Location();
                    if (_gotFirstSpeedReading)
                        if ((SpeedInMS - _lastSpeedInMs) > 200 && (timeBetweenLocations.TotalMilliseconds<3000))
                        {
                            // If the speed increases by more than 200m/s in three seconds, this is most likely due to respawn (i.e. invalid)
                            SpeedInMS = 0;
                            _speedCalculationLocation = null;
                        }
                    _gotFirstSpeedReading = (SpeedInMS>0);
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
                if (WaypointIndex > 0)
                {
                    DistanceToWaypoint = EDLocation.DistanceBetween(Location, Route.Waypoints[WaypointIndex].Location);
                    if (DistanceToWaypoint<Route.Waypoints[WaypointIndex].Radius)
                    {
                        // Commander has reached the target waypoint
                        AddRaceHistory($"Arrived at {Route.Waypoints[WaypointIndex].Name}");
                        WaypointIndex++;
                        if (WaypointIndex >= Route.Waypoints.Count)
                        {
                            notableEvents?.AddEvent($"{Commander}{EDRace.StatusMessages["CompletedNotification"]}");
                            Finished = true;
                            FinishTime = DateTime.Now;
                            WaypointIndex = 0;
                            DistanceToWaypoint = 0;
                        }
                    }
                }
            }

            if (!Started)
                return;

            if (DistanceToWaypoint<_nextLogDistanceToWaypoint)
            {
                AddRaceHistory($"{(DistanceToWaypoint / 1000):F1}km to {Route.Waypoints[WaypointIndex].Name}");
                _nextLogDistanceToWaypoint = DistanceToWaypoint - 5000;
            }

            if (!isFlagSet(StatusFlags.In_SRV) && (!_inPits || !isFlagSet(StatusFlags.Landed_on_planet_surface)))
            {
                if (EliminateOnShipFlight)
                {
                    Eliminated = true;
                    notableEvents?.AddEvent($"{Commander}{EDRace.StatusMessages["EliminatedNotification"]}");
                    DistanceToWaypoint = double.MaxValue;
                }
                _speedCalculationLocation = null; // If this occurred due to commander exploding, we need to clear the location otherwise we'll get a massive reading on respawn
            }

            if (AllowPitStops)
            {
                if (!_inPits)
                {
                    if (isFlagSet(StatusFlags.Srv_UnderShip))
                    {
                        _inPits = true;
                        _pitStopStartTime = DateTime.Now;
                        notableEvents?.AddEvent($"{Commander}{EDRace.StatusMessages["PitstopNotification"]}");
                        PitStopCount++;
                    }
                }
                else if (isFlagSet(StatusFlags.In_SRV) && !isFlagSet(StatusFlags.Srv_UnderShip))
                {
                    _inPits = false;
                    AddRaceHistory($"Pitstop {PitStopCount} took {DateTime.Now.Subtract(_pitStopStartTime):mm\\:ss}");
                }
            }

            _lowFuel = isFlagSet(StatusFlags.Low_Fuel);

            String currentStatus = ToString();
            if (currentStatus.Equals(_lastStatus))
                return;

            AddRaceHistory(currentStatus);
            _lastStatus = currentStatus;
            StatusChanged?.Invoke(null, Commander, currentStatus);
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
