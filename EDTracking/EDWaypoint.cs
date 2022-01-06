using System;
using System.Collections.Generic;
using System.Text.Json;

namespace EDTracking
{
    public class EDWaypoint
    {
        public String Name { get; set; } = "Waypoint";
        public EDLocation Location { get; set; } = null;
        public double Radius { get; set; } = 5000;
        public double MinimumAltitude { get; set; } = 0;
        public double MaximumAltitude { get; set; } = 100;
        public bool AllowPassing { get; set; } = false;
        //public sbyte AltitudeTest { get; set; } = 0; // -1, must be below, +1 must be above, 0 not checked
        public int Direction { get; set; } = -1;
        public DateTime TimeTracked { get; internal set; }  // To store the time the location was recorded when route recording
        public Dictionary<string, string> ExtendedWaypointInformation { get; set; } = new Dictionary<string, string>();
        public List<EDLocation> AdditionalLocations { get; set; } = new List<EDLocation>();
        public string AdditionalInfo { get; set; } = null;

        public EDWaypoint()
        { }

        public EDWaypoint(EDLocation location)
        {
            Location = location;
            if (!String.IsNullOrEmpty(location.Name))
                Name = location.Name;
        }

        public EDWaypoint(EDLocation location, double hitRadius, int hitDirection): this(location)
        {
            Radius = hitRadius;
            Direction = hitDirection;
            if (!String.IsNullOrEmpty(location.Name))
                Name = location.Name;
        }

        public EDWaypoint(EDLocation location, DateTime timeTracked, double radius): this(location)
        {
            TimeTracked = timeTracked;
            Radius = radius;
            if (!String.IsNullOrEmpty(location.Name))
                Name = location.Name;
        }

        public bool IsValid()
        {
            // Check if the waypoint has all the required parameters for its type
            return true;
        }

        private bool LocationIsWithinBasicWaypoint(EDLocation location)
        {
            if (location.PlanetaryRadius != Location.PlanetaryRadius)
                return false;
            if (EDLocation.DistanceBetween(Location, location) < Radius)
                return true;

            return false;
        }

        public bool WaypointIsBehind(EDLocation location, double DirectionOfTravel)
        {
            // Checks whether the waypoint is behind the given location (given the direction of travel)
            // This allows moving on to the next waypoint even if we don't go through one

            double bearingToWaypoint = EDLocation.BearingToLocation(location, Location);
            double directionOfWaypoint = EDLocation.BearingDelta(bearingToWaypoint, DirectionOfTravel);
            if (Math.Abs(directionOfWaypoint) > 90)
                return true;
            return false;
        }

        private bool GateHit(EDLocation currentLocation, EDLocation previousLocation)
        {
            //  We need to test if the line between current and last location intersects
            // the line of the gate
            if ( AdditionalLocations.Count < 2 || AdditionalLocations[0]==null || previousLocation==null)
                return false;
            return EDLocation.PassedBetween(AdditionalLocations[0], AdditionalLocations[1],previousLocation, currentLocation);
        }

        public bool WaypointHit(EDLocation currentLocation, EDLocation previousLocation, EDLocation previousWaypointLocation = null)
        {
            // Used for testing all waypoint types

            if (MaximumAltitude > 0 && currentLocation.Altitude > MaximumAltitude)
                return false;
            if (currentLocation.Altitude < MinimumAltitude)
                return false;

            if (!ExtendedWaypointInformation.ContainsKey("WaypointType"))
            {
                // This is a basic waypoint
                bool waypointHit = LocationIsWithinBasicWaypoint(currentLocation);
                if (!waypointHit && AllowPassing && previousWaypointLocation != null)
                    waypointHit = WaypointIsBehind(currentLocation, EDLocation.BearingToLocation(previousWaypointLocation, Location));
                return waypointHit;
            }

            switch (ExtendedWaypointInformation["WaypointType"])
            {
                case "Gate": // This type of waypoint requires the target to pass between two points
                    return GateHit(currentLocation, previousLocation);

            }
            return false;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public static EDWaypoint FromString(string location)
        {
            return (EDWaypoint)JsonSerializer.Deserialize(location, typeof(EDWaypoint));
        }
    }
}
