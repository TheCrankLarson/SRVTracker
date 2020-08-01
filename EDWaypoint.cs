using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRVTracker
{

    class EDWaypoint
    {
        public EDLocation Location { get; set; } = null;
        public double Radius { get; set; } = 0;
        public double Direction { get; set; } = -1;

        public bool LocationIsWithinWaypoint(EDLocation location)
        {
            return (EDLocation.DistanceBetween(Location, location) < Radius);
        }
    }
}
