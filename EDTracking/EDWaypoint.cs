using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace EDTracking
{

    public class EDWaypoint
    {
        private static CultureInfo _enGB = new CultureInfo("en-GB");
        public EDLocation Location { get; set; } = null;
        public double Radius { get; set; } = 5000;
        public int Direction { get; set; } = -1;

        public EDWaypoint(EDLocation location)
        {
            Location = location;
        }

        public EDWaypoint(EDLocation location, double hitRadius, int hitDirection): this(location)
        {
            Radius = hitRadius;
            Direction = hitDirection;
        }

        public string Name
        {
            get { return Location.Name; }
        }

        public bool LocationIsWithinWaypoint(EDLocation location)
        {
            return (EDLocation.DistanceBetween(Location, location) < Radius);
        }

        public override string ToString()
        {
            return $"{Location.ToString()}╫{Radius.ToString(_enGB)}╫{Direction}";
        }

        public static EDWaypoint FromString(string location)
        {
            try
            {
                string[] locationInfo = location.Split('╫');
                return new EDWaypoint(EDLocation.FromString(locationInfo[0]), Convert.ToDouble(locationInfo[1],_enGB), Convert.ToInt32(locationInfo[2]));
            }
            catch { }
            return null;
        }
    }
}
