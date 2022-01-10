using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace EDTracking
{
    public class EDRacerProfile
    {
        public string CommanderName { get; set; } = "Anonymous";
        public string TwitchChannel { get; set; } = String.Empty;
        public string YouTubeChannel { get; set; } = String.Empty;
        public string SRVImageName { get; set; } = "SRV64.png";
        public string SLFImageName { get; set; } = "SLF64.png";
        public string ShipImageName { get; set; } = "Courier64.png";
        public string Description { get; set; } = String.Empty;

        public EDRacerProfile()
        {

        }

        public static EDRacerProfile FromJSON(string JSON)
        {
            try
            {
                return (EDRacerProfile)JsonSerializer.Deserialize(JSON, typeof(EDRacerProfile));
            }
            catch
            {
            }
            return null;
        }

        public string ToJSON()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
