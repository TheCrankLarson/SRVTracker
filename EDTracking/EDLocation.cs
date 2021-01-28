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
        public double Latitude { get; set; } = 0;
        public double Longitude { get; set; } = 0;
        public double Altitude { get; set; } = 0;
        public double PlanetaryRadius { get; set; } = 0;
        public string Name { get; set; } = "";
        public string PlanetName { get; set; } = "";
        public string SystemName { get; set; } = "";
        public static double DefaultPlanetaryRadius = 0;

        public EDLocation()
        {
        }

        public EDLocation(double latitude, double longitude, double altitude, double planetaryRadius)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
            PlanetaryRadius = planetaryRadius;
        }

        public EDLocation(string name, double latitude, double longitude, double planetaryRadius)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            PlanetaryRadius = planetaryRadius;
        }

        public EDLocation(string name, string systemName, string planetName, double latitude, double longitude, double altitude, double planetaryRadius) :
            this(name, latitude, longitude, planetaryRadius)
        {
            SystemName = systemName;
            PlanetName = planetName;
            Altitude = altitude;
        }

        public EDLocation Copy()
        {
            return new EDLocation(Name, SystemName, PlanetName, Latitude, Longitude, Altitude, PlanetaryRadius);
        }

        public static string DistanceToString(double distance)
        {
            if (distance < 1000)
                return $"{distance.ToString("F1")} m";
            else if (distance<1000000)
                return $"{(distance / 1000).ToString("F1")} km";
            return $"{(distance / 1000000).ToString("F1")} Mm";
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);// $"{Name}║{SystemName}║{PlanetName}║{Latitude.ToString(_enGB)}║{Longitude.ToString(_enGB)}║{Altitude.ToString(_enGB)}║{PlanetaryRadius.ToString(_enGB)}";
        }

        public bool Equals(EDLocation location)
        {
            if (Latitude != location.Latitude) return false;
            if (Longitude != location.Longitude) return false;
            return true;
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


        public static double ConvertToDegrees(double radians)
        {
            return radians * 180 / (double)Math.PI;
        }

        public static double ConvertToBearing(double radians)
        {
            return (ConvertToDegrees(radians) + 360) % 360;
        }

        public static double DistanceBetween(EDLocation location1, EDLocation location2)
        {
            if (location1 == null || location2 == null || location1.PlanetaryRadius != location2.PlanetaryRadius)
                return 0;

            double R = location1.PlanetaryRadius;
            if (R <= 0)
                return 0;

            double lat = ConvertToRadians(location2.Latitude - location1.Latitude);
            double lng = ConvertToRadians(location2.Longitude - location1.Longitude);
            double h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(ConvertToRadians(location1.Latitude)) * Math.Cos(ConvertToRadians(location2.Latitude)) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            double h2 = (double)(2 * Math.Asin(Math.Min(1, Math.Sqrt(h1))));
            return Math.Abs(R * h2);
        }

        public static double DistanceBetweenIncludingAltitude(EDLocation location1, EDLocation location2)
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

            return (double)Math.Sqrt((x_2 - x_1) * (x_2 - x_1) + (y_2 - y_1) *
                               (y_2 - y_1) + (z_2 - z_1) * (z_2 - z_1));
        }

        public static double BearingToLocation(EDLocation sourceLocation, EDLocation targetLocation)
        {
            double dLon = (ConvertToRadians(targetLocation.Longitude - sourceLocation.Longitude));
            double dPhi = Math.Log(
                Math.Tan(ConvertToRadians(targetLocation.Latitude) / 2 + Math.PI / 4) / Math.Tan(ConvertToRadians(sourceLocation.Latitude) / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI)
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            return ConvertToBearing(Math.Atan2(dLon, dPhi));
        }

        public static double BearingDelta(double b1, double b2)
        {
            double d = 0;

            d = (b2 - b1) % 360;

            if (d > 180)
                d -= 360;
            else if (d < -180)
                d += 360;

            return d;
        }

        public static EDLocation LocationFrom(EDLocation SourceLocation, double Bearing, double Distance)
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

        public static EDLocation MidpointBetween(List<EDLocation> Locations)
        {
            // Calculates and returns the geographic midpoint of the set of locations
            //List<CartesianCoordinate> _cartesianCoordinates = new List<CartesianCoordinate>();
            CartesianCoordinate average = new CartesianCoordinate(0d, 0d, 0d);
            foreach (EDLocation location in Locations)
            {
                double lat = ConvertToRadians(location.Latitude);
                double lon = ConvertToRadians(location.Longitude);
                CartesianCoordinate cc = new CartesianCoordinate(Math.Cos(lat) * Math.Cos(lon), Math.Cos(lat)*Math.Sin(lon), Math.Sin(lat));
                average.x += cc.x;
                average.y += cc.y;
                average.z += cc.z;
            }
            average.x = average.x / Locations.Count;
            average.y = average.y / Locations.Count;
            average.z = average.z / Locations.Count;
            double hyp = Math.Sqrt(average.x * average.x + average.y * average.y);
            return new EDLocation(ConvertToDegrees(Math.Atan2(average.z,hyp)), ConvertToDegrees(Math.Atan2(average.y, average.x)), 0, Locations[0].PlanetaryRadius);
        }

        public static bool PassedBetween(EDLocation GatePost1, EDLocation GatePost2, EDLocation PreviousLocation, EDLocation CurrentLocation)
        {
            // Work out if the line from previous location to this one passed through the defined gate
            // We convert to x/y/z, and then just use the x and y.  This will be inaccurate, but should be good enough

            return doLinesIntersect(new Point(GatePost1), new Point(GatePost2), new Point(PreviousLocation), new Point(CurrentLocation));
        }

        public class Point
        {
            public double x;
            public double y;
            public double z;

            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public Point(EDLocation location)
            {
                double planetaryRadius = location.PlanetaryRadius;
                if (planetaryRadius < 1)
                    planetaryRadius = 1;

                x = planetaryRadius * (double)Math.Cos((double)location.Latitude) * (double)Math.Cos((double)location.Longitude);
                y = planetaryRadius * (double)Math.Cos((double)location.Latitude) * (double)Math.Sin((double)location.Longitude);
                z = planetaryRadius * (double)Math.Sin((double)location.Latitude);
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
        static double orientation(Point p, Point q, Point r)
        {
            // See https://www.geeksforgeeks.org/orientation-3-ordered-points/ 
            // for details of below formula. 
            double val = (q.y - p.y) * (r.x - q.x) -
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
            double o1 = orientation(p1, q1, p2);
            double o2 = orientation(p1, q1, q2);
            double o3 = orientation(p2, q2, p1);
            double o4 = orientation(p2, q2, q1);

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

    class CartesianCoordinate
    {
        public double x;
        public double y;
        public double z;

        public CartesianCoordinate(double X, double Y, double Z)
        {
            x = X;
            y = Y;
            z = Z;
        }
    }
}
