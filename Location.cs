using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace SRVTracker
{
    public class Location
    {
        public GeoCoordinate GeoCoordinate { get; set; } = null;
        public string Name { get; set; } = "";
        public string PlanetName { get; set; } = "";
        public double PlanetaryRadius { get; set; } = 0;
        public string SystemName { get; set; } = "";

        public Location(string name, GeoCoordinate geoCoordinate, double planetaryRadius)
        {
            Name = name;
            GeoCoordinate = geoCoordinate;
            PlanetaryRadius = planetaryRadius;
        }

        public Location(string name, GeoCoordinate geoCoordinate, double planetaryRadius, string systemName, string planetName): this(name, geoCoordinate, planetaryRadius)
        {
            SystemName = systemName;
            PlanetName = planetName;
        }
    }
}
