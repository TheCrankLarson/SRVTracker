using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SRVTracker
{
    class EDRace
    {
        private string _saveFilename = "";
        public string Name { get; set; } = null;
        public EDRoute Route { get; set; } = null;
        public List<string> Contestants = null;
        public DateTime Start;

        public EDRace(string name,EDRoute route)
        {
            Name = name;
            Route = route;
        }

        public EDRace(string name, EDRoute route, IEnumerable<string> contestants): this(name, route)
        {
            Contestants = contestants.ToList();
        }

        public override string ToString()
        {
            StringBuilder raceSerialised = new StringBuilder(Name);
            raceSerialised.Append($"┼{Route.ToString()}┼");
            foreach (string contestant in Contestants)
                raceSerialised.Append($"Ⱶ{contestant}");
            return raceSerialised.ToString();
        }

        public static EDRace FromString(string location)
        {
            try
            {
                string[] routeInfo = location.Split('┼');
                EDRoute route = EDRoute.FromString(routeInfo[1]);
                return new EDRace(routeInfo[0], route, routeInfo[2].Split('Ⱶ'));
            }
            catch { }
            return null;
        }

        public static EDRace LoadFromFile(string filename)
        {
            // Attempt to load the route from the file
            try
            {
                return FromString(File.ReadAllText(filename));
            }
            catch { }
            return null;
        }

        public void SaveToFile(string filename)
        {
            try
            {
                File.WriteAllText(filename, this.ToString());
                _saveFilename = filename;
            }
            catch { }
        }
    }
}
