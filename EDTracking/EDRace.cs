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
        public List<string> Contestants { get; set; } = new List<string>();

        // For moving race monitoring away from the form.  Will be public once that work complete
        private Dictionary<string, EDRaceStatus> _statuses = new Dictionary<string, EDRaceStatus>();  

        public DateTime Start { get; set; } = DateTime.MinValue;
        private bool _raceStarted = false;
        public bool EliminateOnVehicleDestruction { get; set; } = true;
        public bool SRVOnly { get; set; } = true;
        public bool FighterOnly { get; set; } = false;
        public bool ShipOnly { get; set; } = false;
        public bool AllowPitstops { get; set; } = true;
        public bool AllowNightVision { get; set; } = true;


        public EDRace()
        { }

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

        public static EDRace FromString(string location)
        {
            try
            {
                return (EDRace)JsonSerializer.Deserialize(location, typeof(EDRace));
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
        public void StartRace(bool asServer)
        {
            if (_raceStarted)
                return;
            Start = DateTime.Now;
            _statuses = new Dictionary<string, EDRaceStatus>();
            foreach (string contestant in Contestants)
                _statuses.Add(contestant, new EDRaceStatus(contestant, Route));
            if (!asServer)
                CommanderWatcher.UpdateReceived += CommanderWatcher_UpdateReceived;
            _raceStarted = true;
        }

        private void CommanderWatcher_UpdateReceived(object sender, EDEvent edEvent)
        {
            Task.Run(new Action(()=> { UpdateStatus(edEvent); })) ;
        }

        private List<string> RacePositions()
        {
            if (_statuses is null)
            {
                return Contestants;
            }

            List<string> positions = new List<string>();
            int finishedIndex = -1;
            foreach (string racer in _statuses.Keys)
            {
                if (_statuses[racer].Finished)
                {
                    // If the racers are finished, then their position depends upon their time
                    if (finishedIndex < 0)
                    {
                        // This is the first finisher we have, so add to top of list
                        if (positions.Count == 0)
                            positions.Add(racer);
                        else
                            positions.Insert(0, racer);
                        finishedIndex = 0;
                    }
                    else
                    {
                        // We need to work out where to add this finisher (based on finish time)
                        int i = 0;
                        while ((i <= finishedIndex) && (_statuses[racer].FinishTime > _statuses[positions[i]].FinishTime))
                            i++;
                        if (i < positions.Count)
                            positions.Insert(i, racer);
                        else
                            positions.Add(racer);
                        finishedIndex = i;
                    }
                }
                else if (_statuses[racer].Eliminated)
                {
                    // Eliminated racers have no position, we can just add them at the end
                    positions.Add(racer);
                }
                else
                {
                    // All other positions are based on waypoint and distance from it (i.e. lowest waypoint number
                    if (positions.Count < 1)
                        positions.Add(racer);
                    else
                    {
                        int i = finishedIndex + 1;
                        if (i < positions.Count)
                        {
                            // Move past anyone who is at a higher waypoint
                            while ((i < positions.Count) && _statuses[positions[i]].WaypointIndex < _statuses[racer].WaypointIndex && (!_statuses[positions[i]].Eliminated))
                                i++;
                            // Now we check distances (as these positions are heading to the same waypoint)
                            while ((i < positions.Count) && (_statuses[positions[i]].WaypointIndex == _statuses[racer].WaypointIndex) && (_statuses[positions[i]].DistanceToWaypoint < _statuses[racer].DistanceToWaypoint) && (!_statuses[positions[i]].Eliminated))
                                i++;
                        }
                        if (i < positions.Count)
                            positions.Insert(i, racer);
                        else
                            positions.Add(racer);
                    }
                }
            }
            return positions;
        }

        public void UpdateStatus(EDEvent edEvent)
        {
            if (_statuses != null)
                if (_statuses.ContainsKey(edEvent.Commander))
                    _statuses[edEvent.Commander].UpdateStatus(edEvent);
        }
    }
}
