using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.Json;

namespace EDTracking
{

    public class EDWaypoint
    {
        public EDLocation Location { get; set; } = null;
        public decimal Radius { get; set; } = 5000;
        public decimal MinimumAltitude { get; set; } = 0;
        public decimal MaximumAltitude { get; set; } = 100;
        public bool AllowPassing { get; set; } = false;
        //public sbyte AltitudeTest { get; set; } = 0; // -1, must be below, +1 must be above, 0 not checked
        public int Direction { get; set; } = -1;
        public DateTime TimeTracked { get; internal set; }  // To store the time the location was recorded when route recording
        private static int _nextWaypointNumber = 1;
        public Dictionary<string, string> ExtendedWaypointInformation { get; set; } = new Dictionary<string, string>();
        public List<EDLocation> AdditionalLocations { get; set; } = new List<EDLocation>();

        public EDWaypoint()
        { }

        public EDWaypoint(EDLocation location)
        {
            Location = location;
        }

        public EDWaypoint(EDLocation location, decimal hitRadius, int hitDirection): this(location)
        {
            Radius = hitRadius;
            Direction = hitDirection;
        }

        public EDWaypoint(EDLocation location, DateTime timeTracked, decimal radius): this(location)
        {
            TimeTracked = timeTracked;
            Radius = radius;
        }

        public bool IsValid()
        {
            // Check if the waypoint has all the required parameters for its type
            return true;
        }

        public string Name
        {
            get {
                if (!String.IsNullOrEmpty(Location.Name))
                    return Location.Name;
                Location.Name = $"Waypoint {_nextWaypointNumber}";
                _nextWaypointNumber++;
                return Location.Name;
            }
            set { Location.Name = value; }
        }

        public bool LocationIsWithinWaypoint(EDLocation location)
        {
            if (EDLocation.DistanceBetween(Location, location) < Radius)
            {
                if (MinimumAltitude == 0 && MaximumAltitude == 0)
                    return true;
                if (location.Altitude >= MinimumAltitude && location.Altitude <= MaximumAltitude)
                    return true;
            }
            return false;
        }

        public bool WaypointIsBehind(EDLocation location, decimal DirectionOfTravel)
        {
            // Checks whether the waypoint is behind the given location (given the direction of travel)
            // This allows moving on to the next waypoint even if we don't go through one

            decimal bearingToWaypoint = EDLocation.BearingToLocation(location, Location);
            decimal directionOfWaypoint = EDLocation.BearingDelta(bearingToWaypoint, DirectionOfTravel);
            if (Math.Abs(directionOfWaypoint) > 90)
                return true;
            return false;
        }

        private bool GateHit(EDLocation currentLocation, EDLocation previousLocation)
        {
            //  We need to test if the line between current and last location intersects
            // the line of the gate
            if ( AdditionalLocations.Count < 2 || AdditionalLocations[0]==null )
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
                bool waypointHit = LocationIsWithinWaypoint(currentLocation);
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
