using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Timers;
using EDTracking;

namespace EDTracking
{
    public class CommanderWatcher
    {
        private static WebClient _webClient = new WebClient();
        private static Dictionary<string, EDEvent> _commanderStatuses = new Dictionary<string, EDEvent>();
        private static Timer _updateTimer = new Timer(250);
        private static bool _enabled = false;
        private static object _lock = new object();
        private static string _lastStatus = "";
        private static string _serverUrl = "";

        public delegate void UpdateReceivedEventHandler(object sender, EDEvent edEvent);
        public static event UpdateReceivedEventHandler UpdateReceived;
        public static event EventHandler OnlineCountChanged;

        public CommanderWatcher(string serverUrl)
        {
            Start(serverUrl);
        }

        public static void Start(string serverUrl)
        {
            _serverUrl = serverUrl;
            if (!_enabled)
            {
                // We only want one timer, so ensure we only subscribe once
                _updateTimer.Elapsed += _updateTimer_Elapsed;
                _updateTimer.Interval = 250;
                _updateTimer.Start();
                _enabled = true;
            }
        }

        public static void Stop()
        {
            if (_enabled)
            {
                _updateTimer.Stop();
                _updateTimer.Elapsed -= _updateTimer_Elapsed;
                _enabled = false;
            }
        }

        public static EDEvent GetCommanderStatus(string commander)
        {
            if (_commanderStatuses.ContainsKey(commander))
                return _commanderStatuses[commander];
            return null;
        }

        public static List<string> GetCommanders()
        {
            return _commanderStatuses.Keys.ToList<string>();
        }

        public static int OnlineCommanderCount
        {
            get
            {
                return _commanderStatuses.Count;
            }
        }

        private static void _updateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _updateTimer.Stop();
            try
            {
                UpdateAvailableCommanders();
            }
            catch { }
            if (_enabled)
                _updateTimer.Start();
        }

        private static void UpdateAvailableCommanders()
        {
            // Connect to the server and retrieve the list of any currently live commanders

            string commanderStatus = "";
            try
            {
                Stream statusStream = _webClient.OpenRead(_serverUrl);
                using (StreamReader reader = new StreamReader(statusStream))
                    commanderStatus = reader.ReadToEnd().Trim();
                statusStream.Close();
            }
            catch { }
            if (_lastStatus.Equals(commanderStatus))
                return;
            _lastStatus = commanderStatus;
            bool countChanged = false;

            if (!String.IsNullOrEmpty(commanderStatus))
            {
                // We have something to process (hopefully a list of commander statuses)
                string[] commanders = commanderStatus.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                if (commanders.Length > 0)
                {
                    for (int i = 0; i < commanders.Length; i++)
                    {
                        EDEvent edEvent = EDEvent.FromJson(commanders[i]);
                        if (edEvent!= null && !String.IsNullOrEmpty(edEvent.Commander))
                        {                           
                            if (_commanderStatuses.ContainsKey(edEvent.Commander))
                            {
                                if (edEvent.TimeStamp > _commanderStatuses[edEvent.Commander].TimeStamp)
                                {
                                    lock (_lock)
                                        _commanderStatuses[edEvent.Commander] = edEvent;
                                    UpdateReceived?.Invoke(null, edEvent);
                                }
                            }
                            else
                            {
                                lock (_lock)
                                    _commanderStatuses.Add(edEvent.Commander, edEvent);
                                countChanged = true;
                                UpdateReceived?.Invoke(null, edEvent);
                            }                               
                        }
                    }
                    if (countChanged)
                        OnlineCountChanged?.Invoke(null, null);
                }
            }
        }
    }
}
