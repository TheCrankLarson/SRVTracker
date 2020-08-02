using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SRVTracker
{
    class EDRoute
    {
        private List<EDWaypoint> _waypoints = null;
        public string Name { get; set; } = null;
        private string _saveFilename = "";

        public EDRoute(string name)
        {
            Name = name;
            _waypoints = new List<EDWaypoint>();
        }

        public EDRoute(string name, List<EDWaypoint> waypoints)
        {
            Name = name;
            _waypoints = waypoints;
        }

        public override string ToString()
        {
            StringBuilder routeSerialised = new StringBuilder(Name);
            foreach (EDWaypoint waypoint in _waypoints)
                routeSerialised.Append($"└{waypoint.ToString()}");
            return routeSerialised.ToString();
        }

        public static EDRoute FromString(string location)
        {
            try
            {
                string[] routeInfo = location.Split('└');
                List<EDWaypoint> waypoints = new List<EDWaypoint>();
                for (int i = 1; i < routeInfo.Length; i++)
                    waypoints.Add(EDWaypoint.FromString(routeInfo[i]));
                return new EDRoute(routeInfo[0], waypoints);
            }
            catch { }
            return null;
        }

        public List<EDWaypoint> Waypoints
        {
            get { return _waypoints; }
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
