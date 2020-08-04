using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EDTracking
{
    public class EDStatus
    {        
        public int Heading { get; internal set; } = -1;
        public long Flags { get; internal set; } = -1;
        public DateTime TimeStamp { get; internal set; } = DateTime.MinValue;
        public bool Eliminated { get; internal set; } = false;
        public EDLocation Location { get; internal set; } = null;
        public int WaypointIndex { get; internal set; } = 0;
        public EDRoute Route { get; set; } = null;
        public bool Started { get; internal set; } = true;

        public string Commander { get; } = "";
        public static bool EliminateOnDestruction { get; set; } = true;
        public static bool ShowDetailedStatus { get; set; } = true;

        public string _lastStatus = "";
        public delegate void StatusChangedEventHandler(object sender, string commander, string status);
        public static event StatusChangedEventHandler StatusChanged;
        private long _lastFlags = 0;
        private bool _inPits = false;
        private bool _lowFuel = false;

        public EDStatus(EDEvent baseEvent)
        {
            Flags = baseEvent.Flags;
            Heading = baseEvent.Heading;
            TimeStamp = baseEvent.TimeStamp;
            Commander = baseEvent.Commander;
        }

        public EDStatus(string commander, EDRoute route, bool immediateStart = true)
        {
            Commander = commander;
            Route = route;
            if (immediateStart)
                StartRace();
        }

        public override string ToString()
        {
            if (Eliminated)
                return "Eliminated";

            if (_inPits)
                return "Pitstop";

            StringBuilder status = new StringBuilder();

            if (Started)
                status.Append($"-> {Route.Waypoints[WaypointIndex].Name}");

            if (_lowFuel)
                status.Append(" (low fuel)");

            return status.ToString();
        }

        public void StartRace()
        {
            Started = true;
            WaypointIndex = 1;
            StatusChanged?.Invoke(null, Commander, this.ToString());
        }


        private bool isFlagSet(StatusFlags flag)
        {
            return ((Flags & (long)flag) == (long)flag);
        }

        public string DetailedStatus()
        {
            StringBuilder status = new StringBuilder();

            if (isFlagSet(StatusFlags.In_SRV))
            {
                // SRV only flags
                if (isFlagSet(StatusFlags.LightsOn))
                {
                    status.Append("L");
                    if (isFlagSet(StatusFlags.srvHighBeam))
                        status.Append("H");
                    else
                        status.Append("L");
                }

                if (isFlagSet(StatusFlags.Srv_Handbrake))
                    status.Append(" HB");

                if (isFlagSet(StatusFlags.Srv_DriveAssist))
                    status.Append(" DA");
            }
            else if (isFlagSet(StatusFlags.In_MainShip))
            {
                // Ship only flags
                if (isFlagSet(StatusFlags.Landing_Gear_Down))
                    status.Append(" LG");

                if (!isFlagSet(StatusFlags.FlightAssist_Off))
                    status.Append(" FA");

                if (isFlagSet(StatusFlags.Silent_Running))
                    status.Append(" SR");

                if (isFlagSet(StatusFlags.Cargo_Scoop_Deployed))
                    status.Append(" C");

                if (isFlagSet(StatusFlags.Being_Interdicted))
                    status.Append(" I");
            }

            // Flags that apply to all vehicles
            if (isFlagSet(StatusFlags.Night_Vision_Active))
                status.Append(" NV");

            return status.ToString().Trim();
        }

        public void UpdateStatus(EDEvent updateEvent)
        {
            // Update our status based on the passed event

            _lastFlags = Flags;
            Flags = updateEvent.Flags;

            if (updateEvent.HasCoordinates)
                Location = updateEvent.Location;

            if (Eliminated)
                return;

            if (!isFlagSet(StatusFlags.In_SRV) && !_inPits)
                Eliminated = true;
            else if (isFlagSet(StatusFlags.In_SRV))
                _inPits = isFlagSet(StatusFlags.Srv_UnderShip);

            _lowFuel = isFlagSet(StatusFlags.Low_Fuel);

            StringBuilder currentStatus = new StringBuilder(ToString());
            if (ShowDetailedStatus)
                currentStatus.Append($" {DetailedStatus()}");
            if (currentStatus.Equals(_lastStatus))
                return;

        }
    }
}
