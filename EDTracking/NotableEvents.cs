using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;

namespace EDTracking
{
    public class NotableEvents
    {
        Queue<string> _notableEvents = new Queue<string>();
        Timer _updateTimer = new Timer();
        private string _activeEvent = "";
        private string _writeToFile = "";
        public Dictionary<string, string> CustomStatusMessages = EDRace.StatusMessages;

        public int UpdateInterval { get; set; } = 5000;

        public NotableEvents(string SaveFile = "", bool autoProcess = true)
        {
            _writeToFile = SaveFile;
            if (autoProcess)
            {
                _updateTimer.Interval = UpdateInterval;
                _updateTimer.Elapsed += _updateTimer_Elapsed;
                _updateTimer.Start();
            }

            if (!String.IsNullOrEmpty(_writeToFile))
            {   // Ensure the file is blank to start
                try
                {
                    File.WriteAllText(_writeToFile, _activeEvent);
                }
                catch { }
            }
        }

        public Queue<string> EventQueue
        {
            get { return _notableEvents; }
        }

        private void _updateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (String.IsNullOrEmpty(_activeEvent) && _notableEvents.Count < 1)
                return;

            _updateTimer.Stop();
            if (_notableEvents.Count > 0)
                _activeEvent = _notableEvents.Dequeue();
            else
                _activeEvent = "";

            if (!String.IsNullOrEmpty(_writeToFile))
            {
                try
                {
                    File.WriteAllText(_writeToFile, _activeEvent);
                }
                catch { }
            }
            _updateTimer.Start();
        }

        public void AddEvent(string eventInfo)
        {
            if (!String.IsNullOrEmpty(eventInfo))
                _notableEvents.Enqueue(eventInfo);
        }

        public void AddStatusEvent(string statusEvent, string commander, string postfix = "")
        {
            AddEvent($"{commander}{CustomStatusMessages[statusEvent]}{postfix}");
        }
    }
}
