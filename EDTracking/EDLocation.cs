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

        private static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        private static double ConvertToRadians(decimal angle)
        {
            return ConvertToRadians((double)angle);
        }

        public static decimal DistanceBetween(EDLocation location1, EDLocation location2)
        {
            if (location1==null || location2==null || location1.PlanetaryRadius != location2.PlanetaryRadius)
                return 0;

            decimal R = location1.PlanetaryRadius;
            if (R <= 0)
                    return 0;

            double lat = ConvertToRadians(location2.Latitude - location1.Latitude);
            double lng = ConvertToRadians(location2.Longitude - location1.Longitude);
            double h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(ConvertToRadians(location1.Latitude)) * Math.Cos(ConvertToRadians(location2.Latitude)) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            decimal h2 = (decimal)(2 * Math.Asin(Math.Min(1, Math.Sqrt(h1))));
            return Math.Abs(R * h2);
        }

        public static decimal DistanceBetweenIncludingAltitude(EDLocation location1, EDLocation location2)
        {
            if (location1.PlanetaryRadius != location2.PlanetaryRadius)
                return 0;

            double R = (double)location1.PlanetaryRadius;
            if (R <= 0)
                    return 0;

            double R1 = R + (double)location1.Altitude;
            double x_1 = R1 * Math.Sin((double)location1.Longitude) * Math.Cos((double)location1.Latitude);
            double y_1 = R1 * Math.Sin((double)location1.Longitude) * Math.Sin((double)location1.Latitude);
            double z_1 = R1 * Math.Cos((double)location1.Longitude);

            double R2 = R + (double)location2.Altitude;
            double x_2 = R2 * Math.Sin((double)location2.Longitude) * Math.Cos((double)location2.Latitude);
            double y_2 = R2 * Math.Sin((double)location2.Longitude) * Math.Sin((double)location2.Latitude);
            double z_2 = R2 * Math.Cos((double)location2.Longitude);

            return (decimal)Math.Sqrt((x_2 - x_1) * (x_2 - x_1) + (y_2 - y_1) *
                               (y_2 - y_1) + (z_2 - z_1) * (z_2 - z_1));
        }

        public static decimal BearingToLocation(EDLocation sourceLocation, EDLocation targetLocation)
        {
            double dLon = (ConvertToRadians(targetLocation.Longitude - sourceLocation.Longitude));
            double dPhi = Math.Log(
                Math.Tan(ConvertToRadians(targetLocation.Latitude) / 2 + Math.PI / 4) / Math.Tan(ConvertToRadians(sourceLocation.Latitude) / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI)
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            return ConvertToBearing((decimal)(Math.Atan2(dLon, dPhi)));
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
