using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDTracking
{
    public class SRVTelemetry
    {
        public int CurrentGroundSpeed { get; set; } = 0;
        public int AverageGroundSpeed { get; set; } = 0;
        public int MaximumGroundSpeed { get; set; } = 0;
        public double TotalDistanceTravelled { get; set; } = 0;
        public int TotalShipRepairs { get; set; } = 0;
        public int TotalSynthRepairs { get; set; } = 0;
        public DateTime SessionStartTime { get; set; } = DateTime.MinValue;
        private DateTime _lastEventTime = DateTime.MinValue;
        private EDLocation _lastLocation = null;

        public SRVTelemetry()
        {
        }

        public static Dictionary<string, string> TelemetryDescriptions()
        {
            return new Dictionary<string, string>()
                {
                    { "CurrentGroundSpeed", "Current ground speed in m/s" },
                    { "AverageGroundSpeed", "Average ground speed in m/s" },
                    { "MaximumGroundSpeed", "Maximum ground speed in m/s" },
                    { "TotalDistanceTravelled", "Total distance travelled" },
                    { "TotalShipRepairs", "Total number of ship repairs" },
                    { "TotalSynthRepairs", "Total number of synthesized repairs" }
                };
        }

        public void ProcessEvent(EDEvent edEvent)
        {
            if (SessionStartTime == DateTime.MinValue)
                SessionStartTime = edEvent.TimeStamp;

            bool statsUpdated = ProcessLocationUpdate(edEvent);
            if (ProcessFlags(edEvent))
                statsUpdated = true;

            switch (edEvent.EventName)
            {
                case "DockSRV":
                    TotalShipRepairs++;
                    statsUpdated = true;
                    break;

                case "Synthesis":
                    TotalSynthRepairs++;
                    statsUpdated = true;
                    break;
            }
            _lastEventTime = edEvent.TimeStamp;
        }

        private bool ProcessFlags(EDEvent edEvent)
        {
            return false;
        }

        private bool ProcessLocationUpdate(EDEvent edEvent)
        {
            EDLocation currentLocation = edEvent.Location();
            if (currentLocation == null)
                return false;

            if (_lastLocation==null)
            {
                _lastLocation = currentLocation;
                SessionStartTime = edEvent.TimeStamp;
                return false;
            }
            if (_lastLocation.Latitude.Equals(currentLocation.Latitude) && _lastLocation.Longitude.Equals(currentLocation.Longitude))
                return false;

            // Update distance/speed statistics
            return true;
        }
    }
}
