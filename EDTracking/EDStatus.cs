using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDTracking
{
    public class EDStatus
    {
        public int Heading { get; set; } = -1;
        public long Flags { get; set; } = -1;
        public DateTime TimeStamp { get; set; } = DateTime.MinValue;
        public string BodyName { get; set; } = "";
        public double PlanetRadius { get; set; } = 0;
        public double Altitude { get; set; } = 0;
        public string Commander { get; set; } = "";
        public EDLocation Location { get; set; } = null;


        public EDStatus(EDEvent initialEvent)
        {

        }
    }
}
