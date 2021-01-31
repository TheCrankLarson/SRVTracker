using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDTracking
{
    public class RouteGenerator
    {
        public static List<EDWaypoint> Equator(double PlanetaryRadius, int WaypointSeparationDistance, double StartLongitude = 0)
        {
            List<EDWaypoint> waypoints = new List<EDWaypoint>();
            int numberOfWaypoints = Convert.ToInt32(Circumference(PlanetaryRadius) / Convert.ToDouble(WaypointSeparationDistance));
            if (numberOfWaypoints > 500)
                return null;

            double anglePerWaypoint = 360/(double)numberOfWaypoints;
            int waypointRadius = WaypointSeparationDistance / 2;
            if (waypointRadius > 1000)
                waypointRadius = 1000;

            for (int i = 0; i < numberOfWaypoints; i++)
            {
                double thisLongitude = StartLongitude + (anglePerWaypoint * i);
                EDLocation thisLocation = new EDLocation(0, thisLongitude, 0, PlanetaryRadius);
                waypoints.Add(new EDWaypoint(thisLocation, DateTime.Now, waypointRadius));
            }
            return waypoints;
        }

        public static List<EDWaypoint> PoleToPole(double PlanetaryRadius, int WaypointSeparationDistance, double StartLatitude = 0)
        {
            List<EDWaypoint> waypoints = new List<EDWaypoint>();
            return waypoints;
        }

        public static double Circumference(double radius)
        {
            // Returns the circumference of the circle
            return 2 * radius * Math.PI;
        }
    }
}
