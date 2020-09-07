using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Newtonsoft.Json;
using System.Reflection;
using System.Globalization;
using System.Text.Json;
using System.Numerics;

namespace EDTracking
{
    public class EDEvent
    {
        // { "timestamp":"2020-07-28T17:46:47Z", "event":"Status", "Flags":16777229, "Pips":[4,8,0], "FireGroup":0, "GuiFocus":0, "Fuel":{ "FuelMain":16.000000, "FuelReservoir":0.430376 }, "Cargo":4.000000, "LegalState":"Clean" }
        //{ "timestamp":"2020-07-28T17:52:25Z", "event":"Status", "Flags":341852424, "Pips":[4,8,0], "FireGroup":0, "GuiFocus":0, "Fuel":{ "FuelMain":0.000000, "FuelReservoir":0.444637 }, "Cargo":0.000000, "LegalState":"Clean", "Latitude":-14.055647, "Longitude":-31.176170, "Heading":24, "Altitude":0, "BodyName":"Synuefe DJ-G b44-3 A 5", "PlanetRadius":1311227.875000 }
        //

        public double Latitude { get; set; } = 0;
        public double Longitude { get; set; } = 0;
        public int Heading { get; set; } = -1;
        public long Flags { get; set; } = 0;
        public DateTime TimeStamp { get; set; } = DateTime.MinValue;
        public string BodyName { get; set; } = "";
        public double PlanetRadius { get;  set; } = 0;
        public double Altitude { get; set; } = 0;
        public string Commander { get; set; } = "";
        public string EventName { get; set; } = "";
        public double Health { get; set; } = -1;

        public EDEvent() { }

        public EDLocation Location()
        {
            if (HasCoordinates())
                return new EDLocation(Latitude, Longitude, Altitude, PlanetRadius);
            return null;
        }

        public static EDEvent FromJson(string json)
        {
            return (EDEvent)JsonSerializer.Deserialize(json, typeof(EDEvent));
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public EDEvent(string statusJson, string commander, DateTime? timeStamp = null)
        {
            // Initialise from the ED JSON status file

            using (JsonDocument jsonDoc = JsonDocument.Parse(statusJson))
            {
                JsonElement root = jsonDoc.RootElement;
                JsonElement property;

                if (timeStamp != null)
                    TimeStamp = ((DateTime)timeStamp).ToUniversalTime();
                else if (root.TryGetProperty("timestamp", out property))
                    TimeStamp = property.GetDateTime();

                if (root.TryGetProperty("Flags", out property))
                    Flags = property.GetInt64();
                if (root.TryGetProperty("Latitude", out property))
                    Latitude = property.GetDouble();
                if (root.TryGetProperty("Longitude", out property))
                    Longitude = property.GetDouble();
                if (root.TryGetProperty("Altitude", out property))
                    Altitude = property.GetDouble();
                if (root.TryGetProperty("PlanetRadius", out property))
                    PlanetRadius = property.GetDouble();
                if (root.TryGetProperty("Health", out property))
                    Health = property.GetDouble();
                if (root.TryGetProperty("BodyName", out property))
                    BodyName = property.GetString();
                if (root.TryGetProperty("Heading", out property))
                    Heading = property.GetInt16();
                if (root.TryGetProperty("event", out property))
                    EventName = property.GetString();
                if (root.TryGetProperty("EventName", out property))
                    EventName = property.GetString();
            }
            Commander = commander;
        }

        public EDEvent(string commander, long timestamp, double latitude, double longitude, double altitude, int heading, double planetRadius, long flags)
        {
            Commander = commander;
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
            Heading = heading;
            PlanetRadius = planetRadius;
            TimeStamp = new DateTime(timestamp);
            this.Flags = flags;
        }



        public bool isInSRV()
        {
            return (this.Flags & (long)StatusFlags.In_SRV) == (long)StatusFlags.In_SRV;
        }

        public bool srvIsUnderShip()
        {
            return (this.Flags & (long)StatusFlags.Srv_UnderShip) == (long)StatusFlags.Srv_UnderShip;
        }

        public bool isInMainShip()
        {
            return (this.Flags & (long)StatusFlags.In_MainShip) == (long)StatusFlags.In_MainShip;
        }

        public bool isInFighter()
        {
            return (this.Flags & (long)StatusFlags.In_Fighter) == (long)StatusFlags.In_Fighter;
        }

        public bool HasCoordinates()
        {
            return (this.Flags & (long)StatusFlags.Has_Lat_Long) == (long)StatusFlags.Has_Lat_Long;
        }

        public string Vehicle()
        {
            if (this.isInSRV()) return "SRV";
            if (this.isInMainShip()) return "Ship";
            if (this.isInFighter()) return "SLF";
            return "Unknown";
        }

    }
}
