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
            return new EDEvent(json);
        }

        public static EDEvent CreateEventFromLocation(string location)
        {
            return null;
        }
    }
}
