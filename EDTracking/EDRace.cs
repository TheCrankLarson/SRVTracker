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
        public int LeaderWaypoint { get; set; } = 0;
        public List<string> Contestants { get; set; } = new List<string>();

        // For moving race monitoring away from the form.  Will be public once that work complete
        public Dictionary<string, EDRaceStatus> Statuses { get; set; } = new Dictionary<string, EDRaceStatus>();
        private NotableEvents _notableEvents;
        private Dictionary<string, List<EDEvent>> _commanderEventHistory = new Dictionary<string, List<EDEvent>>();

        public DateTime Start { get; set; } = DateTime.MinValue;
        private bool _raceStarted = false;
        public bool Finished { get; set; } = false;
        public bool EliminateOnVehicleDestruction { get; set; } = true;
        public bool SRVOnly { get; set; } = true;
        public bool FighterOnly { get; set; } = false;
        public bool ShipOnly { get; set; } = false;
        public bool AllowPitstops { get; set; } = true;
        public bool AllowNightVision { get; set; } = true;
        public static Dictionary<string, string> StatusMessages { get; set; } = new Dictionary<string, string>()
                {
                    { "Eliminated", "Eliminated" },
                    { "Completed", "Completed" },
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

            Start = DateTime.Now;
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
                if (asServer)
                    raceStatus.notableEvents = _notableEvents;
                Statuses.Add(contestant, raceStatus);
                _commanderEventHistory.Add(contestant, new List<EDEvent>());
            }
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
            List<string> positions = new List<string>();
            if (Statuses == null)
            {
                if (Contestants.Count > 0)
                    return Contestants;
                return positions;
            }
            if (Statuses.Count == 0 && (Contestants.Count > 0))
                return Contestants;

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
                            finishedIndex = i;
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
                        if (i < positions.Count && !Statuses[positions[i]].Eliminated)
                        {
                            // Move past anyone who is at a higher waypoint
                            while ((i < positions.Count) && Statuses[positions[i]].WaypointIndex < Statuses[racer].WaypointIndex && !Statuses[positions[i]].Eliminated)
                                i++;
                            if ((i < positions.Count) && !Statuses[positions[i]].Eliminated)
                            {
                                // Now we check distances (as these positions are heading to the same waypoint)
                                while ((i < positions.Count) && (Statuses[positions[i]].WaypointIndex == Statuses[racer].WaypointIndex) && (Statuses[positions[i]].DistanceToWaypoint < Statuses[racer].DistanceToWaypoint) && (!Statuses[positions[i]].Eliminated))
                                    i++;
                                //if ((i < positions.Count) && Statuses[positions[i]].Eliminated && (i > finishedIndex + 1))
                                //i--;
                            }
                        }
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

        public Dictionary<string, string> ExportRaceStatisticsDict(int maxStatusLength = 20)
        {
            List<string> leaderBoard = RacePositions();
            Dictionary<string, string> statsTable = new Dictionary<string, string>();

            StringBuilder status = new StringBuilder();
            StringBuilder leaderBoardExport = new StringBuilder();
            StringBuilder speeds = new StringBuilder();
            StringBuilder maxSpeeds = new StringBuilder();
            StringBuilder distanceToWaypoint = new StringBuilder();
            StringBuilder hullStrengths = new StringBuilder();

            for (int i = 0; i < leaderBoard.Count; i++)
            {
                if (leaderBoard[i].Length > maxStatusLength)
                    leaderBoardExport.AppendLine(leaderBoard[i].Substring(0, maxStatusLength));
                else
                    leaderBoardExport.AppendLine(leaderBoard[i]);

                if (this.Start > DateTime.MinValue && Statuses != null)
                {
                    maxSpeeds.AppendLine($"{Statuses[leaderBoard[i]].MaxSpeedInMS:F0}");
                    if (!Statuses[leaderBoard[i]].Eliminated && !Statuses[leaderBoard[i]].Finished)
                    {
                        speeds.AppendLine($"{Statuses[leaderBoard[i]].SpeedInMS:F0}");
                    }
                    else
                        speeds.AppendLine();
                }
                else
                    speeds.AppendLine();

                if (Statuses != null && (Statuses.Count > 0))
                {
                    if (Statuses[leaderBoard[i]].Finished)
                    {
                        status.Append(CustomStatusMessages["Completed"]);
                        status.AppendLine($" ({Statuses[leaderBoard[i]].FinishTime.Subtract(Start):hh\\:mm\\:ss})");
                        distanceToWaypoint.AppendLine(CustomStatusMessages["Completed"]);
                    }
                    else
                    {
                        string s;
                        if (Statuses[leaderBoard[i]].Eliminated)
                        {
                            distanceToWaypoint.AppendLine(CustomStatusMessages["Eliminated"]);
                            s = CustomStatusMessages["Eliminated"];
                        }
                        else
                        {
                            distanceToWaypoint.AppendLine(Statuses[leaderBoard[i]].DistanceToWaypointInKmDisplay);
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
                    distanceToWaypoint.AppendLine(CustomStatusMessages["Ready"]);
                    hullStrengths.AppendLine(" ");
                }
            }

            statsTable.Add("Positions", leaderBoardExport.ToString());
            statsTable.Add("Speeds", speeds.ToString());
            statsTable.Add("MaxSpeeds", maxSpeeds.ToString());
            statsTable.Add("Status", status.ToString());
            statsTable.Add("DistanceToWaypoint", distanceToWaypoint.ToString());
            if (NotableEvents != null)
                statsTable.Add("NotableEvents", String.Join(Environment.NewLine, NotableEvents.EventQueue));
            statsTable.Add("HullStrengths", hullStrengths.ToString());
            statsTable.Add("LeaderWaypoint", LeaderWaypoint.ToString());

            return statsTable;
        }

        public string ExportRaceStatistics(int maxStatusLength = 20)
        {
            // Export the current leaderboard

            // We only rebuild the statistics after a short time
            if ( (Finished || DateTime.Now.Subtract(_statsLastGenerated).TotalMilliseconds<750) && !String.IsNullOrEmpty(_lastStatsTable) )
                return _lastStatsTable;

            Dictionary<string, string> statsTable = ExportRaceStatisticsDict(maxStatusLength);
            _lastStatsTable = JsonSerializer.Serialize(statsTable);
            _statsLastGenerated = DateTime.Now;

            return _lastStatsTable;
        }


        public void UpdateStatus(EDEvent edEvent)
        {
            if (Statuses != null)
            {
                if (Statuses.ContainsKey(edEvent.Commander))
                {
                    Statuses[edEvent.Commander].UpdateStatus(edEvent);
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
    }
}
