using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.Json;
using System.Diagnostics;

namespace EDTracking
{
    public class EDLocation
    {
        public decimal Latitude { get; set; } = 0;
        public decimal Longitude { get; set; } = 0;
        public decimal Altitude { get; set; } = 0;
        public decimal PlanetaryRadius { get; set; } = 0;
        public string Name { get; set; } = "";
        public string PlanetName { get; set; } = "";
        public string SystemName { get; set; } = "";
        public static decimal DefaultPlanetaryRadius = 0;

        public EDLocation()
        {
        }

        public EDLocation(decimal latitude, decimal longitude, decimal altitude, decimal planetaryRadius)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
            PlanetaryRadius = planetaryRadius;
        }

        public EDLocation(string name, decimal latitude, decimal longitude, decimal planetaryRadius)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            PlanetaryRadius = planetaryRadius;
        }

        public EDLocation(string name, string systemName, string planetName, decimal latitude, decimal longitude, decimal altitude, decimal planetaryRadius):
            this(name, latitude,longitude, planetaryRadius)
        {
            SystemName = systemName;
            PlanetName = planetName;
            Altitude = altitude;
        }

        public EDLocation Copy()
        {
            return new EDLocation(Name, SystemName, PlanetName, Latitude, Longitude, Altitude, PlanetaryRadius);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);// $"{Name}║{SystemName}║{PlanetName}║{Latitude.ToString(_enGB)}║{Longitude.ToString(_enGB)}║{Altitude.ToString(_enGB)}║{PlanetaryRadius.ToString(_enGB)}";
        }

        public static EDLocation FromString(string location)
        { 
            try
            {
                return (EDLocation)JsonSerializer.Deserialize(location, typeof(EDLocation));
            }
            catch {
                Debug.WriteLine(location);
            }
            return null;
        }

        private static decimal ConvertToRadians(decimal angle)
        {
            return ((decimal)Math.PI / 180) * angle;
        }

        public static decimal DistanceBetween(EDLocation location1, EDLocation location2)
        {
            decimal R = location1.PlanetaryRadius;
            if (R <= 0)
            {
                R = location2.PlanetaryRadius;
                if (R <= 0)
                    return 0;
            }
            decimal lat = ConvertToRadians(location2.Latitude - location1.Latitude);
            decimal lng = ConvertToRadians(location2.Longitude - location1.Longitude);
            decimal h1 = (decimal)(Math.Sin((double)lat / 2) * Math.Sin((double)lat / 2) +
                          Math.Cos((double)ConvertToRadians(location1.Latitude)) * Math.Cos((double)ConvertToRadians(location2.Latitude)) *
                          Math.Sin((double)lng / 2) * Math.Sin((double)lng / 2));
            decimal h2 = (decimal)(2 * Math.Asin(Math.Min(1, Math.Sqrt((double)h1))));
            return Math.Abs(R * h2);
        }

        public static decimal BearingToLocation(EDLocation sourceLocation, EDLocation targetLocation)
        {
            decimal dLon = ConvertToRadians(targetLocation.Longitude - sourceLocation.Longitude);
            decimal dPhi = (decimal)Math.Log(
                Math.Tan((double)ConvertToRadians(targetLocation.Latitude) / 2 + Math.PI / 4) / Math.Tan((double)ConvertToRadians(sourceLocation.Latitude) / 2 + Math.PI / 4));
            if (Math.Abs((double)dLon) > Math.PI)
                dLon = dLon > 0 ? (decimal)-(2 * Math.PI - (double)dLon) : (decimal)(2 * Math.PI + (double)dLon);
            return ConvertToBearing((decimal)(Math.Atan2((double)dLon, (double)dPhi)));
        }

        public static decimal ConvertToDegrees(decimal radians)
        {
            return radians * 180 / (decimal)Math.PI;
        }

        public static decimal ConvertToBearing(decimal radians)
        {
            // convert radians to degrees (as bearing: 0...360)
            return (ConvertToDegrees(radians) + 360) % 360;
        }

        public static decimal BearingDelta(decimal b1, decimal b2)
        {
            decimal d = 0;
 
			d = (b2-b1)%360;
 
			if(d>180)
				d -= 360;
			else if(d<-180)
				d += 360;
 
			return d;
        }
    }
}
