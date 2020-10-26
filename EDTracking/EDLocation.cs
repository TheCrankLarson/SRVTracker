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
        public double Latitude { get; set; } = 0;
        public double Longitude { get; set; } = 0;
        public double Altitude { get; set; } = 0;
        public double PlanetaryRadius { get; set; } = 0;
        public string Name { get; set; } = "";
        public string PlanetName { get; set; } = "";
        public string SystemName { get; set; } = "";
        public static double DefaultPlanetaryRadius = 0;

        public EDLocation()
        {
        }

        public EDLocation(double latitude, double longitude, double altitude, double planetaryRadius)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
            PlanetaryRadius = planetaryRadius;
        }

        public EDLocation(string name, double latitude, double longitude, double planetaryRadius)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            PlanetaryRadius = planetaryRadius;
        }

        public EDLocation(string name, string systemName, string planetName, double latitude, double longitude, double altitude, double planetaryRadius):
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

        public static double DistanceBetween(EDLocation location1, EDLocation location2)
        {
            double R = location1.PlanetaryRadius;
            if (R <= 0)
            {
                R = location2.PlanetaryRadius;
                if (R <= 0)
                    return 0;
            }
            var lat = ConvertToRadians(location2.Latitude - location1.Latitude);
            var lng = ConvertToRadians(location2.Longitude - location1.Longitude);
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(ConvertToRadians(location1.Latitude)) * Math.Cos(ConvertToRadians(location2.Latitude)) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return Math.Abs(R * h2);
        }

        public static double BearingToLocation(EDLocation sourceLocation, EDLocation targetLocation)
        {
            var dLon = ConvertToRadians(targetLocation.Longitude - sourceLocation.Longitude);
            var dPhi = Math.Log(
                Math.Tan(ConvertToRadians(targetLocation.Latitude) / 2 + Math.PI / 4) / Math.Tan(ConvertToRadians(sourceLocation.Latitude) / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI)
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            return ConvertToBearing(Math.Atan2(dLon, dPhi));
        }

        public static double ConvertToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }

        public static double ConvertToBearing(double radians)
        {
            // convert radians to degrees (as bearing: 0...360)
            return (ConvertToDegrees(radians) + 360) % 360;
        }
    }
}
