using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRVTracker
{
    public class EDEventFactory
    {
        public static EDEvent CreateEventFromJSON(string json)
        {
            // Create the event from JSON (as dumped to Status.json)
            return new EDEvent(json);
        }

        public static EDEvent CreateEventFromLocation(string location)
        {
            // Recreate the event from a saved location
            // Tracking info is: Client Id,timestamp,latitude,longitude,altitude,heading,planet radius,flags

            try
            {
                string[] tracking = location.Split(',');
                return new EDEvent(Convert.ToDouble(tracking[2]), Convert.ToDouble(tracking[3]), Convert.ToDouble(tracking[4]), Convert.ToInt32(tracking[5]),
                    Convert.ToDouble(tracking[6]), Convert.ToInt64(tracking[7]));
            }
            catch { }
            return null;
        }

        public static EDEvent CreateEventFromStatus(string status)
        {
            // Recreate the event from a saved status
            try
            {
                if (status.Contains(":{"))
                    return CreateEventFromJSON(status.Substring(status.IndexOf(':')));
                else
                    return CreateEventFromLocation(status);
            }
            catch { }
            return null;
        }
    }
}
