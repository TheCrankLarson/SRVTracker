using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;

namespace EDTracking
{
    public class RaceReplayGenerator
    {
        private FileStream _writeStream = null;
        private EDRace _race = null;
        private Timer _collectStatusTimer = null;
        private string _lastStatus = null;

        public RaceReplayGenerator(EDRace race, string outputFile)
        {
            _race = race;
            try
            {
                _writeStream = File.OpenWrite(outputFile);
            }
            catch
            {
                return;
            }
            _collectStatusTimer = new Timer(1000);
            _collectStatusTimer.Elapsed += _collectStatusTimer_Elapsed;
        }

        private void _collectStatusTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string latestStatus = _race.CachedToString();
            if (latestStatus.Equals(_lastStatus))
                return;
        }
    }
}
