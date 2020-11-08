using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.Json;
using System.Diagnostics;

namespace EDTracking
{
    public class EDLocation
    {
        public decimal Latitude { get; set; } = 0;
        public decimal Longitude { get; set; } = 0;
        public decimal Altitude { get; set; } = 0;
        public decimal PlanetaryRadius { get; set; } = 0;
        public string Name { get; set; } = "";
        public string PlanetName { get; set; } = "";
        public string SystemName { get; set; } = "";
        public static decimal DefaultPlanetaryRadius = 0;

        public EDLocation()
        {
        }

        public EDLocation(decimal latitude, decimal longitude, decimal altitude, decimal planetaryRadius)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
            PlanetaryRadius = planetaryRadius;
        }

        public EDLocation(string name, decimal latitude, decimal longitude, decimal planetaryRadius)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            PlanetaryRadius = planetaryRadius;
        }

        public EDLocation(string name, string systemName, string planetName, decimal latitude, decimal longitude, decimal altitude, decimal planetaryRadius):
            this(name, latitude,longitude, planetaryRadius)
        {
            SystemName = systemName;
            PlanetName = planetName;
            Altitude = altitude;
        }

        public EDLocation Copy()
        {
            return new EDLocation(Name, SystemName, PlanetName, Latitude, Longitude, Altitude, PlanetaryRadius);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);// $"{Name}║{SystemName}║{PlanetName}║{Latitude.ToString(_enGB)}║{Longitude.ToString(_enGB)}║{Altitude.ToString(_enGB)}║{PlanetaryRadius.ToString(_enGB)}";
        }

        public static EDLocation FromString(string location)
        { 
            try
            {
                return (EDLocation)JsonSerializer.Deserialize(location, typeof(EDLocation));
            }
            catch {
                Debug.WriteLine(location);
            }
            return null;
        }

        private static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        private static double ConvertToRadians(decimal angle)
        {
            return ConvertToRadians((double)angle);
        }

        public static decimal DistanceBetween(EDLocation location1, EDLocation location2)
        {
            if (location1==null || location2==null || location1.PlanetaryRadius != location2.PlanetaryRadius)
                return 0;

            decimal R = location1.PlanetaryRadius;
            if (R <= 0)
                    return 0;

            double lat = ConvertToRadians(location2.Latitude - location1.Latitude);
            double lng = ConvertToRadians(location2.Longitude - location1.Longitude);
            double h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(ConvertToRadians(location1.Latitude)) * Math.Cos(ConvertToRadians(location2.Latitude)) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            decimal h2 = (decimal)(2 * Math.Asin(Math.Min(1, Math.Sqrt(h1))));
            return Math.Abs(R * h2);
        }

        public static decimal DistanceBetweenIncludingAltitude(EDLocation location1, EDLocation location2)
        {
            if (location1.PlanetaryRadius != location2.PlanetaryRadius)
                return 0;

            double R = (double)location1.PlanetaryRadius;
            if (R <= 0)
                    return 0;

            double R1 = R + (double)location1.Altitude;
            double x_1 = R1 * Math.Sin((double)location1.Longitude) * Math.Cos((double)location1.Latitude);
            double y_1 = R1 * Math.Sin((double)location1.Longitude) * Math.Sin((double)location1.Latitude);
            double z_1 = R1 * Math.Cos((double)location1.Longitude);

            double R2 = R + (double)location2.Altitude;
            double x_2 = R2 * Math.Sin((double)location2.Longitude) * Math.Cos((double)location2.Latitude);
            double y_2 = R2 * Math.Sin((double)location2.Longitude) * Math.Sin((double)location2.Latitude);
            double z_2 = R2 * Math.Cos((double)location2.Longitude);

            return (decimal)Math.Sqrt((x_2 - x_1) * (x_2 - x_1) + (y_2 - y_1) *
                               (y_2 - y_1) + (z_2 - z_1) * (z_2 - z_1));
        }

        public static decimal BearingToLocation(EDLocation sourceLocation, EDLocation targetLocation)
        {
            double dLon = (ConvertToRadians(targetLocation.Longitude - sourceLocation.Longitude));
            double dPhi = Math.Log(
                Math.Tan(ConvertToRadians(targetLocation.Latitude) / 2 + Math.PI / 4) / Math.Tan(ConvertToRadians(sourceLocation.Latitude) / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI)
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            return ConvertToBearing((decimal)(Math.Atan2(dLon, dPhi)));
        }

        public static decimal ConvertToDegrees(decimal radians)
        {
            return radians * 180 / (decimal)Math.PI;
        }

        public static decimal ConvertToDegrees(double radians)
        {
            return (decimal)(radians * 180 / Math.PI);
        }

        public static decimal ConvertToBearing(decimal radians)
        {
            // convert radians to degrees (as bearing: 0...360)
            return (ConvertToDegrees(radians) + 360) % 360;
        }

        public static decimal BearingDelta(decimal b1, decimal b2)
        {
            decimal d = 0;
 
			d = (b2-b1)%360;
 
			if(d>180)
				d -= 360;
			else if(d<-180)
				d += 360;
 
			return d;
        }

        public static EDLocation LocationFrom(EDLocation SourceLocation, decimal Bearing, decimal Distance)
        {
            // Return the location that is the specified distance away from source location at the given bearing

            double r = (double)SourceLocation.PlanetaryRadius;
            if (r < 1)
                return null;
            double lat1 = ConvertToRadians(SourceLocation.Latitude);
            double lon1 = ConvertToRadians(SourceLocation.Longitude);


            double lat2 = Math.Asin(Math.Sin(lat1) * Math.Cos((double)Distance / r) + Math.Cos(lat1) * Math.Sin((double)Distance / r) * Math.Cos((double)Bearing));
            double lon2 = lon1 + Math.Atan2(Math.Sin((double)Bearing) * Math.Sin((double)Distance / r) * Math.Cos(lat1),
                                Math.Cos((double)Distance / r) - Math.Sin(lat1) * Math.Sin(lat2));

            return new EDLocation(ConvertToDegrees(lat2), ConvertToDegrees(lon2), 0, SourceLocation.PlanetaryRadius);

        }

        public static bool PassedBetween(EDLocation GatePost1, EDLocation GatePost2, EDLocation PreviousLocation, EDLocation CurrentLocation)
        {
            // Work out if the line from previous location to this one passed through the defined gate
            // We convert to x/y/z, and then just use the x and y.  This will be inaccurate, but should be good enough

            return doLinesIntersect(new Point(GatePost1), new Point(GatePost2), new Point(PreviousLocation), new Point(CurrentLocation));
        }

        public class Point
        {
            public decimal x;
            public decimal y;
            public decimal z;

            public Point(decimal x, decimal y)
            {
                this.x = x;
                this.y = y;
            }

            public Point(EDLocation location)
            {
                decimal planetaryRadius = location.PlanetaryRadius;
                if (planetaryRadius < 1)
                    planetaryRadius = 1;

                x = planetaryRadius * (decimal)Math.Cos((double)location.Latitude) * (decimal)Math.Cos((double)location.Longitude);
                y = planetaryRadius * (decimal)Math.Cos((double)location.Latitude) * (decimal)Math.Sin((double)location.Longitude);
                z = planetaryRadius * (decimal)Math.Sin((double)location.Latitude);
            }

        };

        // Given three colinear points p, q, r, the function checks if 
        // point q lies on line segment 'pr' 
        static Boolean onSegment(Point p, Point q, Point r)
        {
            if (q.x <= Math.Max(p.x, r.x) && q.x >= Math.Min(p.x, r.x) &&
                q.y <= Math.Max(p.y, r.y) && q.y >= Math.Min(p.y, r.y))
                return true;

            return false;
        }

        // To find orientation of ordered triplet (p, q, r). 
        // The function returns following values 
        // 0 --> p, q and r are colinear 
        // 1 --> Clockwise 
        // 2 --> Counterclockwise 
        static decimal orientation(Point p, Point q, Point r)
        {
            // See https://www.geeksforgeeks.org/orientation-3-ordered-points/ 
            // for details of below formula. 
            decimal val = (q.y - p.y) * (r.x - q.x) -
                    (q.x - p.x) * (r.y - q.y);

            if (val == 0) return 0; // colinear 

            return (val > 0) ? 1 : 2; // clock or counterclock wise 
        }

        // The main function that returns true if line segment 'p1q1' 
        // and 'p2q2' intersect. 
        static Boolean doLinesIntersect(Point p1, Point q1, Point p2, Point q2)
        {
            // Find the four orientations needed for general and 
            // special cases 
            decimal o1 = orientation(p1, q1, p2);
            decimal o2 = orientation(p1, q1, q2);
            decimal o3 = orientation(p2, q2, p1);
            decimal o4 = orientation(p2, q2, q1);

            // General case 
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases 
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1 
            if (o1 == 0 && onSegment(p1, p2, q1)) return true;

            // p1, q1 and q2 are colinear and q2 lies on segment p1q1 
            if (o2 == 0 && onSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2 
            if (o3 == 0 && onSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2 
            if (o4 == 0 && onSegment(p2, q1, q2)) return true;

            return false; // Doesn't fall in any of the above cases 
        }
    }
}
