using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SRVTracker
{
    public class EDEventFactory
    {
        private static CultureInfo _enGB = new CultureInfo("en-GB");
        
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
                return new EDEvent(Convert.ToDouble(tracking[2], _enGB), Convert.ToDouble(tracking[3], _enGB), Convert.ToDouble(tracking[4], _enGB),
                    Convert.ToInt32(tracking[5], _enGB), Convert.ToDouble(tracking[6], _enGB), Convert.ToInt64(tracking[7], _enGB));
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
                    return CreateEventFromJSON(status.Substring(status.IndexOf(':')+1));
                else
                    return CreateEventFromLocation(status);
            }
            catch { }
            return null;
        }
    }
}
