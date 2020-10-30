using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace EDTracking
{
    public class EDRoute
    {
        public string Name { get; set; } = null;
        public List<EDWaypoint> Waypoints { get; set; } = null;
        private decimal _totalWaypointDistance = 0;
        private List<decimal> _waypointDistances = new List<decimal>();
        private List<decimal> _distanceLeftAtWaypoint = new List<decimal>();
        private int _lastWaypointCount = 0;
        private string _saveFilename = "";

        public EDRoute()
        {
            Waypoints = new List<EDWaypoint>();
            Name = "";
        }

        public EDRoute(string name)
        {
            Name = name;
            Waypoints = new List<EDWaypoint>();
        }

        public EDRoute(string name, List<EDWaypoint> waypoints)
        {
            Name = name;
            Waypoints = waypoints;
        }

        private void CalculateDistances()
        {
            if (Waypoints.Count == _lastWaypointCount)
                return;

            _distanceLeftAtWaypoint = new List<decimal>();
            _waypointDistances = new List<decimal>();
            _totalWaypointDistance = 0;
            if (Waypoints.Count < 2)
                return;

            for (int i=0; i<Waypoints.Count-1; i++)
            {
                _waypointDistances.Add(EDLocation.DistanceBetween(Waypoints[i].Location, Waypoints[i + 1].Location));
                _totalWaypointDistance += _waypointDistances[i];
            }
            
            _distanceLeftAtWaypoint.Add(_totalWaypointDistance);
            for (int i = 0; i < Waypoints.Count - 1; i++)
                _distanceLeftAtWaypoint.Add(_distanceLeftAtWaypoint[i] - _waypointDistances[i]);
        }

        public decimal TotalDistanceLeftAtWaypoint(int WaypointIndex)
        {
            CalculateDistances();
            if (WaypointIndex < _distanceLeftAtWaypoint.Count)
                return _distanceLeftAtWaypoint[WaypointIndex];
            return 0;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public static EDRoute FromString(string location)
        {
            return (EDRoute)JsonSerializer.Deserialize(location, typeof(EDRoute));
        }

        public static EDRoute LoadFromFile(string filename)
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


    }
}
