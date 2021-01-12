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
        public int TotalDistanceTravelled { get; set; } = 0;
        public int TotalShipRepairs { get; set; } = 0;
        public int TotalSynthRepairs { get; set; } = 0;
        private DateTime _lastEventTime = DateTime.MinValue;
        private EDLocation _lastLocation = null;

        public SRVTelemetry()
        {
        }

        public void ProcessEvent(EDEvent edEvent)
        {
            ProcessLocationUpdate(edEvent.Location());
            
        }

        private void ProcessLocationUpdate(EDLocation CurrentLocation)
        {
            if (CurrentLocation == null)
                return;

            if (_lastLocation==null)
            {
                _lastLocation = CurrentLocation;
                return;
            }


        }
    }
}
