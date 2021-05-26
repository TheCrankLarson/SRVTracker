using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Dynamic;

namespace EDTracking
{
    public class EDRace
    {
        private string _saveFilename = "";
        public string Name { get; set; } = null;
        public EDRoute Route { get; set; } = null;
        public EDRaceStatus Leader { get; set; } = null;
        public List<string> Contestants { get; set; } = new List<string>();
        public Dictionary<string, EDRaceStatus> Statuses { get; set; } = new Dictionary<string, EDRaceStatus>();
        private NotableEvents _notableEvents;
        private Dictionary<string, List<EDEvent>> _commanderEventHistory = new Dictionary<string, List<EDEvent>>();

        public DateTime Start { get; set; } = DateTime.MinValue;
        public bool StartTimeFromFirstWaypoint { get; set; } = false;
        private bool _firstWaypointPassed = false;
        private bool _raceStarted = false;
        public bool Finished { get; set; } = false;
        public int Laps { get; set; } = 0;  // 0 means we are not using laps, any other number means we are
        public int LapStartWaypoint { get; set; } = 0;
        public int LapEndWaypoint { get; set; } = 0;
        public bool EliminateOnVehicleDestruction { get; set; } = true;
        public bool FeetAllowed { get; set; } = true;
        public bool SRVAllowed { get; set; } = true;
        public bool FighterAllowed { get; set; } = false;
        public bool ShipAllowed { get; set; } = false;
        public bool AllowPitstops { get; set; } = true;
        public bool AllowNightVision { get; set; } = true;
        public static Dictionary<string, string> StatusMessages { get; set; } = new Dictionary<string, string>()
                {
                    { "Eliminated", "Eliminated" },
                    { "Completed", "Completed" },
                    { "CompletedLap", " has completed lap" },
                    { "Pitstop", "Pitstop" },
                    { "EliminatedNotification", " has been eliminated" },
                    { "CompletedNotification", " has finished the race" },
                    { "PitstopNotification", " is in the pits" },
                    { "Ready", "" }
                };
        public Dictionary<string, string> CustomStatusMessages { get; set; } = StatusMessages;
        private string _lastStatsTable = "";
        private DateTime _statsLastGenerated = DateTime.MinValue;

        public EDRace()
        {
        }

        public EDRace(string name,EDRoute route)
        {
            Name = name;
            Route = route;
        }

        public EDRace(string name, EDRoute route, IEnumerable<string> contestants): this(name, route)
        {
            Contestants = contestants.ToList();
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public static EDRace FromString(string raceInfo)
        {
            try
            {
                return (EDRace)JsonSerializer.Deserialize(raceInfo, typeof(EDRace));
            }
            catch { }
            return null;
        }

        public static EDRace LoadFromFile(string filename)
        {
            // Attempt to load the route from the file
            try
            {
                return FromString(File.ReadAllText(filename));
            }
            catch { }
            return null;
        }

        public void SaveToFile(string filename)
        {
            try
            {
                File.WriteAllText(filename, this.ToString());
                _saveFilename = filename;
            }
            catch { }
        }
        public void StartRace(bool asServer, NotableEvents notableEvents = null)
        {
            if (_raceStarted)
                return;

            Start = DateTime.UtcNow;
            Statuses = new Dictionary<string, EDRaceStatus>();
            if (notableEvents != null)
                _notableEvents = notableEvents;
            else if (asServer)
            {
                _notableEvents = new NotableEvents("", false)
                {
                    CustomStatusMessages = CustomStatusMessages
                };
                _commanderEventHistory = new Dictionary<string, List<EDEvent>>();
            }

            _commanderEventHistory = new Dictionary<string, List<EDEvent>>();
            foreach (string contestant in Contestants)
            {
                EDRaceStatus raceStatus = new EDRaceStatus(contestant, this);
                raceStatus.StartTime = Start;
                raceStatus.LapStartTime = Start;
                raceStatus.Lap = 0;
                if (LapStartWaypoint == 0)
                {
                    raceStatus.Lap = 1;
                    LapEndWaypoint = Route.Waypoints.Count - 1;
                }
                if (asServer)
                    raceStatus.notableEvents = _notableEvents;
                Statuses.Add(contestant, raceStatus);
                _commanderEventHistory.Add(contestant, new List<EDEvent>());
            }
            InitLapCalculations();
            if (!asServer)
            {
                CommanderWatcher.UpdateReceived += CommanderWatcher_UpdateReceived;
            }

            _raceStarted = true;
        }

        public NotableEvents NotableEvents
        {
            get { return _notableEvents; }
        }

        private void CommanderWatcher_UpdateReceived(object sender, EDEvent edEvent)
        {
            Task.Run(new Action(()=> { UpdateStatus(edEvent); })) ;
        }

        public List<string> RacePositions()
        {
            if (Statuses == null || Statuses.Count == 0)
                return Contestants;

            List<string> positions = new List<string>();
            int finishedIndex = -1;

            foreach (string racer in Statuses.Keys)
            {
                if (positions.Count == 0)
                {
                    // This is the first status - we just add it
                    positions.Add(racer);
                    if (Statuses[racer].Finished)
                        finishedIndex = 0;
                }
                else
                {
                    if (Statuses[racer].Finished)
                    {
                        // If the racers are finished, then their position depends upon their time
                        if (finishedIndex < 0)
                        {
                            // This is the first finisher we have, so add to top of list
                            positions.Insert(0, racer);
                            finishedIndex = 0;
                        }
                        else
                        {
                            // We need to work out where to add this finisher (based on finish time)
                            int i = 0;
                            while ((i <= finishedIndex) && (Statuses[racer].FinishTime > Statuses[positions[i]].FinishTime))
                                i++;
                            if (i < positions.Count)
                                positions.Insert(i, racer);
                            else
                                positions.Add(racer);
                            finishedIndex++;
                        }
                    }
                    else if (Statuses[racer].Eliminated)
                    {
                        // Eliminated racers have no position, we can just add them at the end
                        positions.Add(racer);
                    }
                    else
                    {
                        // All other positions are based on waypoint and distance from it (i.e. lowest waypoint number
                        int i = finishedIndex + 1;
                        if (Laps > 0)
                            while ((i < positions.Count) && Statuses[positions[i]].Lap > Statuses[racer].Lap && !Statuses[positions[i]].Eliminated)
                                i++;
                        // We check total distance left to work out the position
                        while ((i < positions.Count) && Statuses[positions[i]].TotalDistanceLeft < Statuses[racer].TotalDistanceLeft && !Statuses[positions[i]].Eliminated)
                            i++;

                        if (i < positions.Count)
                            positions.Insert(i, racer);
                        else
                            positions.Add(racer);

                    }
                }
            }

            // Now we need to update each RaceStatus with the positions
            for (int i = 0; i < positions.Count; i++)
                Statuses[positions[i]].RacePosition = i + 1;

            return positions;
        }

        public bool CheckIfFinished()
        {
            // Go through each racer status and check if any are still running
            if (Statuses == null || Finished)
                return true;

            foreach (EDRaceStatus raceStatus in Statuses.Values)
            {
                if (!raceStatus.Eliminated && !raceStatus.Finished)
                    return false;
            }

            Finished = true;
            return true;
        }

        public Dictionary<string, string> ExportRaceStatisticsDict(int maxStatusLength = 40)
        {
            List<string> leaderBoard = RacePositions();
            Dictionary<string, string> statsTable = new Dictionary<string, string>();

            StringBuilder status = new StringBuilder();
            StringBuilder commandersExport = new StringBuilder();
            StringBuilder positionsExport = new StringBuilder();
            StringBuilder speeds = new StringBuilder();
            StringBuilder altitudes = new StringBuilder();
            StringBuilder maxSpeeds = new StringBuilder();
            StringBuilder averageSpeeds = new StringBuilder();
            StringBuilder distanceToWaypoint = new StringBuilder();
            StringBuilder totalDistanceLeft = new StringBuilder();
            StringBuilder hullStrengths = new StringBuilder();
            StringBuilder currentLaps = new StringBuilder();
            StringBuilder lapCounter = new StringBuilder();
            StringBuilder lastLapTime = new StringBuilder();
            StringBuilder fastestLapTime = new StringBuilder();

            for (int i = 0; i < leaderBoard.Count; i++)
            {
                positionsExport.AppendLine((i + 1).ToString());
                if (leaderBoard[i] == null)
                    leaderBoard[i] = "Unknown error";
                if (leaderBoard[i].Length > maxStatusLength)
                    commandersExport.AppendLine(leaderBoard[i].Substring(0, maxStatusLength));
                else
                    commandersExport.AppendLine(leaderBoard[i]);

                if (this.Start > DateTime.MinValue && Statuses != null)
                {
                    maxSpeeds.AppendLine($"{Statuses[leaderBoard[i]].MaxSpeedInMS:F0}");
                    if (!Statuses[leaderBoard[i]].Eliminated && !Statuses[leaderBoard[i]].Finished)
                    {
                        averageSpeeds.AppendLine($"{Statuses[leaderBoard[i]].AverageSpeedInMS:F0}");
                        speeds.AppendLine($"{Statuses[leaderBoard[i]].SpeedInMS:F0}");
                        altitudes.AppendLine($"{Statuses[leaderBoard[i]].Location.Altitude:F0}");
                    }
                    else
                    {
                        speeds.AppendLine();
                        altitudes.AppendLine();
                        averageSpeeds.AppendLine();
                    }
                }
                else
                {
                    speeds.AppendLine();
                    altitudes.AppendLine();
                    averageSpeeds.AppendLine();
                }

                if (Statuses != null && (Statuses.Count > i))
                {
                    lastLapTime.AppendLine(Statuses[leaderBoard[i]].LastLapTime().ToString(@"hh\:mm\:ss\:ff"));
                    fastestLapTime.AppendLine(Statuses[leaderBoard[i]].FastestLapTime().ToString(@"hh\:mm\:ss\:ff"));
                    if (Statuses[leaderBoard[i]].Finished)
                    {
                        status.Append(CustomStatusMessages["Completed"]);
                        status.AppendLine($" ({Statuses[leaderBoard[i]].FinishTime.Subtract(Start):hh\\:mm\\:ss})");
                        totalDistanceLeft.AppendLine("0");
                        distanceToWaypoint.AppendLine("0");
                        currentLaps.AppendLine(CustomStatusMessages["Completed"]);
                        lapCounter.AppendLine(CustomStatusMessages["Completed"]);                       
                    }
                    else
                    {
                        string s;
                        if (Statuses[leaderBoard[i]].Eliminated)
                        {
                            distanceToWaypoint.AppendLine("-");
                            totalDistanceLeft.AppendLine("-");
                            s = CustomStatusMessages["Eliminated"];
                            currentLaps.AppendLine(s);
                            lapCounter.AppendLine(s);
                        }
                        else
                        {
                            distanceToWaypoint.AppendLine(Statuses[leaderBoard[i]].DistanceToWaypointInKmDisplay);
                            totalDistanceLeft.AppendLine(Statuses[leaderBoard[i]].TotalDistanceLeftInKmDisplay);
                            int lapNumber = Statuses[leaderBoard[i]].Lap;
                            if (lapNumber < 1)
                                lapNumber = 1;
                            currentLaps.AppendLine(lapNumber.ToString());
                            lapCounter.AppendLine($"{lapNumber}/{Laps}");
                            
                            s = Statuses[leaderBoard[i]].ToString();
                        }

                        if (s.Length > maxStatusLength)
                            s = s.Substring(0, maxStatusLength);
                        status.AppendLine(s);
                    }

                    if (!Statuses[leaderBoard[i]].Eliminated)
                        hullStrengths.AppendLine(Statuses[leaderBoard[i]].HullDisplay);
                    else
                        hullStrengths.AppendLine(" ");
                }
                else
                {
                    // We don't have any statuses, so this is pre-race
                    status.AppendLine(CustomStatusMessages["Ready"]);
                    distanceToWaypoint.AppendLine("-");
                    totalDistanceLeft.AppendLine("-");
                    hullStrengths.AppendLine("-");
                    currentLaps.AppendLine("-");
                }
            }

            statsTable.Add("RaceName", Name);
            statsTable.Add("Positions", positionsExport.ToString());
            statsTable.Add("Commanders", commandersExport.ToString());
            statsTable.Add("Speeds", speeds.ToString());
            statsTable.Add("Altitudes", altitudes.ToString());
            statsTable.Add("MaxSpeeds", maxSpeeds.ToString());
            statsTable.Add("AverageSpeeds", averageSpeeds.ToString());
            statsTable.Add("Status", status.ToString());
            statsTable.Add("DistanceToWaypoint", distanceToWaypoint.ToString());
            statsTable.Add("TotalDistanceLeft", totalDistanceLeft.ToString());
            statsTable.Add("Hull", hullStrengths.ToString());

            if (NotableEvents != null)
                statsTable.Add("NotableEvents", String.Join(Environment.NewLine, NotableEvents.EventQueue));
            if (Leader != null)
                statsTable.Add("LeaderWaypoint", Leader.WaypointIndex.ToString());

            if (Laps > 0)
            {
                statsTable.Add("Lap", currentLaps.ToString());
                statsTable.Add("LastLapTime", lastLapTime.ToString());
                statsTable.Add("FastestLapTime", fastestLapTime.ToString());
                statsTable.Add("LapCounter", lapCounter.ToString());
                if (Leader != null)
                {
                    int leaderLap = Leader.Lap;
                    if (leaderLap < 1)
                        leaderLap = 1;
                    statsTable.Add("LeaderLap", leaderLap.ToString());
                    if (Leader.Finished)
                        statsTable.Add("LeaderLapCount", CustomStatusMessages["Completed"]);
                    else
                        statsTable.Add("LeaderLapCount", $"Lap {leaderLap}/{Laps}");
                }
            }
            return statsTable;
        }

        public static readonly Dictionary<string,string> RaceReportDescriptions = new Dictionary<string, string>()
        {
            { "RaceName", "Race name" },
            { "Positions", "Positions of contestants" },
            { "Commanders", "Names of contestants" },
            { "Speeds", "Current speeds" },
            { "Altitudes", "Current altitudes" },
            { "MaxSpeeds", "Maximum speeds" },
            { "AverageSpeeds", "Average speeds" },
            { "Status", "Statuses" },
            { "DistanceToWaypoint", "Distances to the next waypoint" },
            { "TotalDistanceLeft", "Total distances left" },
            { "Hull", "Hull strengths left" },
            { "Lap", "Current laps" },
            { "LastLapTime", "Last lap time" },
            { "FastestLapTime", "Fastest lap time" },
            { "LapCounter", "Current laps/total laps" },
            { "LeaderWaypoint", "Waypoint the current leader is heading towards" },
            { "LeaderLap", "Lap number of the current leader" },
            { "LeaderLapCount", "Lap number of the current leader in format Lap 1/2" }
        };
        

        public string ExportRaceStatistics(int maxStatusLength = 40)
        {
            // Export the current leaderboard

            // We only rebuild the statistics after a short time
            if ( (Finished || DateTime.UtcNow.Subtract(_statsLastGenerated).TotalMilliseconds<750) && !String.IsNullOrEmpty(_lastStatsTable) )
                return _lastStatsTable;

            Dictionary<string, string> statsTable = ExportRaceStatisticsDict(maxStatusLength);
            _lastStatsTable = JsonSerializer.Serialize(statsTable);
            _statsLastGenerated = DateTime.UtcNow;

            return _lastStatsTable;
        }


        public void UpdateStatus(EDEvent edEvent)
        {
            if (Statuses != null)
            {
                if (Statuses.ContainsKey(edEvent.Commander))
                {
                    Statuses[edEvent.Commander].UpdateStatus(edEvent);
                    if (StartTimeFromFirstWaypoint && !_firstWaypointPassed)
                        if (Statuses[edEvent.Commander].WaypointIndex>0)
                        {
                            _firstWaypointPassed = true;
                            Start = edEvent.TimeStamp;
                        }
                    if (!_commanderEventHistory.ContainsKey(edEvent.Commander))
                        _commanderEventHistory.Add(edEvent.Commander, new List<EDEvent>());
                    _commanderEventHistory[edEvent.Commander].Add(edEvent);
                }
            }
        }

        public List<EDEvent> GetCommanderEventHistory(string commander)
        {
            if (_commanderEventHistory.ContainsKey(commander))
                return _commanderEventHistory[commander];
            return null;
        }

        private double _preLapLength = 0;
        private double _postLapLength = 0;
        private double _lapLength = 0;
        private double _totalRaceDistance = 0;
        private double _distanceFromLastLapWPToFirst = 0;
        private void InitLapCalculations()
        {
            if (Laps < 1)
                return;

            // For lap calculations, we need to work out the total length of the course
            _lapLength = Route.TotalDistanceLeftAtWaypoint(0);
            _preLapLength = 0;
            if (LapStartWaypoint>0)
            {
                // We have custom laps, so need to work out lap length differently
                _preLapLength = _lapLength - Route.TotalDistanceLeftAtWaypoint(LapStartWaypoint - 1);
                _lapLength = _lapLength - Route.TotalDistanceLeftAtWaypoint(LapEndWaypoint - 1) - _preLapLength;
                _distanceFromLastLapWPToFirst = EDLocation.DistanceBetween(Route.Waypoints[LapEndWaypoint - 1].Location, Route.Waypoints[LapStartWaypoint-1].Location);
                _postLapLength = Route.TotalDistanceLeftAtWaypoint(LapEndWaypoint - 1);
            }
            else
                _distanceFromLastLapWPToFirst = EDLocation.DistanceBetween(Route.Waypoints[Route.Waypoints.Count - 1].Location, Route.Waypoints[0].Location);
            _lapLength += _distanceFromLastLapWPToFirst;
            if (LapStartWaypoint>0)
            {
                _totalRaceDistance = (_lapLength * Laps) + _preLapLength + _postLapLength;
            }
            else
                _totalRaceDistance = _lapLength * Laps;
        }

        public double TotalDistanceLeftAtWaypoint(int WaypointIndex, int Lap)
        {
            if (Lap > Laps)
                return 0;

            double distanceLeft = 0;
            if (LapStartWaypoint == 0)
            {
                if (Lap < 1)
                    return _lapLength * Laps;
                distanceLeft = _lapLength * (Laps - Lap);
                if (WaypointIndex > 0)
                    distanceLeft += Route.TotalDistanceLeftAtWaypoint(WaypointIndex) + _distanceFromLastLapWPToFirst;
                return distanceLeft;
            }

            // Custom laps
            if (Lap == 0) // We haven't reached the start lap waypoint yet
            {
                distanceLeft = _totalRaceDistance;
                for (int i = 1; i < WaypointIndex; i++)
                    distanceLeft -= EDLocation.DistanceBetween(Route.Waypoints[i - 1].Location, Route.Waypoints[i].Location);
                return distanceLeft;
            }
            if (Lap == Laps)
            {
                // Last lap, so we can just use distance left at waypoint
                return Route.TotalDistanceLeftAtWaypoint(WaypointIndex);
            }

            distanceLeft = (_lapLength * (Laps - Lap));

            if (WaypointIndex < LapEndWaypoint - 1)
            {
                distanceLeft += _postLapLength;
                for (int i = WaypointIndex + 1; i < LapEndWaypoint; i++)
                    distanceLeft += EDLocation.DistanceBetween(Route.Waypoints[i - 1].Location, Route.Waypoints[i].Location);
            }

            return distanceLeft;
        }
    }
}
