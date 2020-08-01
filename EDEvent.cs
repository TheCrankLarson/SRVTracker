using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using System.Device.Location;


namespace SRVTracker
{
    public class EDEvent
    {
        // { "timestamp":"2020-07-28T17:46:47Z", "event":"Status", "Flags":16777229, "Pips":[4,8,0], "FireGroup":0, "GuiFocus":0, "Fuel":{ "FuelMain":16.000000, "FuelReservoir":0.430376 }, "Cargo":4.000000, "LegalState":"Clean" }
        //{ "timestamp":"2020-07-28T17:52:25Z", "event":"Status", "Flags":341852424, "Pips":[4,8,0], "FireGroup":0, "GuiFocus":0, "Fuel":{ "FuelMain":0.000000, "FuelReservoir":0.444637 }, "Cargo":0.000000, "LegalState":"Clean", "Latitude":-14.055647, "Longitude":-31.176170, "Heading":24, "Altitude":0, "BodyName":"Synuefe DJ-G b44-3 A 5", "PlanetRadius":1311227.875000 }
        //

        public EDEvent(string json)
        {
            this.RawData = json;
            Newtonsoft.Json.Linq.JObject obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(json);
            foreach (Newtonsoft.Json.Linq.JProperty prop in obj.Properties())
            {
                switch (prop.Name)
                {
                    case "Latitude":
                        this.Latitude = (double)prop.Value;
                        break;

                    case "Longitude":
                        this.Longitude = (double)prop.Value;
                        break;

                    case "Heading":
                        this.Heading = (int)prop.Value;
                        break;

                    case "Flags":
                        this.Flags = (long)prop.Value;
                        break;

                    case "timestamp":
                        this.TimeStamp = (DateTime)prop.Value;
                        break;

                    case "BodyName":
                        this.BodyName= (string)prop.Value;
                        break;

                    case "PlanetRadius":
                        this.PlanetRadius = (double)prop.Value;
                        break;

                    case "Altitude":
                        this.Altitude = (double)prop.Value;
                        break;

                    default:
                        break;
                }
            }
        }

        public EDEvent(double latitude, double longitude, double altitude, int heading, double planetRadius, long flags)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
            Heading = heading;
            PlanetRadius = planetRadius;
            this.Flags = flags;
        }

        public string RawData { get; } = "";

        public double Latitude { get; } = 0;
        public double Longitude { get; } = 0;
        public int Heading { get; } = -1;
        public long Flags { get; } = 0;
        public DateTime TimeStamp { get; } = DateTime.MinValue;
        public string BodyName { get; } = "";
        public double PlanetRadius { get; } = 0;
        public double Altitude { get; } = 0;

        public GeoCoordinate Location
        {
            get
            {
                if (HasCoordinates)
                    if (Altitude>0)
                        return new GeoCoordinate(Latitude, Longitude, Altitude);
                    else
                        return new GeoCoordinate(Latitude, Longitude);
                return null;
            }
        }

        public bool isInSRV
        {
            get { return (this.Flags & (long)StatusFlags.In_SRV) == (long)StatusFlags.In_SRV; }
        }

        public bool isInMainShip
        {
            get { return (this.Flags & (long)StatusFlags.In_MainShip) == (long)StatusFlags.In_MainShip; }
        }

        public bool isInFighter
        {
            get { return (this.Flags & (long)StatusFlags.In_Fighter) == (long)StatusFlags.In_Fighter; }
        }

        public bool HasCoordinates
        {
            get { return (this.Flags & (long)StatusFlags.Has_Lat_Long) == (long)StatusFlags.Has_Lat_Long; }
        }

        public string Vehicle
        {
            get
            {
                if (this.isInSRV) return "SRV";
                if (this.isInMainShip) return "Ship";
                if (this.isInFighter) return "SLF";
                return "Unknown";
            }
        }

        public string TrackingInfo
        {
            get
            {
                // Tracking info is: timestamp,latitude,longitude,altitude,heading,planet radius,flags
                return $"{this.TimeStamp.Ticks},{this.Latitude.ToString("0.0")},{this.Longitude.ToString("0.0")},{this.Altitude.ToString("0.0")},{this.Heading},{this.PlanetRadius.ToString("0.0")},{this.Flags}";
            }
        }
    }
}
