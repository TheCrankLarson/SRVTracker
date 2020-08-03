using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace EDTracking
{
    public class EDEventFactory
    {
        private static CultureInfo _enGB = new CultureInfo("en-GB");
        
        public static EDEvent CreateEventFromJSON(string json, string commander = "")
        {
            // Create the event from JSON (as dumped to Status.json)
            return new EDEvent(json, commander);
        }

        public static EDEvent CreateEventFromLocation(string location)
        {
            // Recreate the event from a saved location
            // Tracking info is: Client Id,timestamp,latitude,longitude,altitude,heading,planet radius,flags

            try
            {
                string[] tracking = location.Split(',');
                return new EDEvent(tracking[0], Convert.ToInt64(tracking[1]), Convert.ToDouble(tracking[2], _enGB), Convert.ToDouble(tracking[3], _enGB),
                    Convert.ToDouble(tracking[4], _enGB), Convert.ToInt32(tracking[5], _enGB), Convert.ToDouble(tracking[6], _enGB), Convert.ToInt64(tracking[7], _enGB));
            }
            catch { }
            return null;
        }

        public static EDEvent CreateEventFromStatus(string status)
        {
            // Recreate the event from a saved status
            try
            {
                int cmdrNamePos = status.IndexOf(":{");
                if (cmdrNamePos>0)
                {
                    return CreateEventFromJSON(status.Substring(cmdrNamePos + 1), status.Substring(0, cmdrNamePos));
                }
                else
                    return CreateEventFromLocation(status);
            }
            catch { }
            return null;
        }
    }
}
