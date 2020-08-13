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
        private string _saveFilename = "";

        public EDRoute() { }

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

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
            /*
            StringBuilder routeSerialised = new StringBuilder(Name);
            foreach (EDWaypoint waypoint in _waypoints)
                _ = routeSerialised.Append($"└{waypoint.ToString()}");
            return routeSerialised.ToString();*/
        }

        public static EDRoute FromString(string location)
        {
            if (location.Contains('└'))
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
