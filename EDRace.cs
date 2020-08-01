using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRVTracker
{
    class EDRace
    {
        public string Name { get; set; } = null;
        public List<EDWaypoint> Waypoints = null;
        public List<string> Contestants = null;
        public DateTime Start;

        public EDRace(string name, IEnumerable<EDWaypoint> waypoints)
        {
            Name = name;
            Waypoints = waypoints.ToList();
        }

        public EDRace(string name, IEnumerable<EDWaypoint> waypoints, IEnumerable<string> contestants): this(name, waypoints)
        {
            Contestants = contestants.ToList();
        }
    }
}
