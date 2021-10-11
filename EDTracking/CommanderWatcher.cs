using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Timers;
using EDTracking;
using System.Text.Json;

namespace EDTracking
{
    public static class CommanderWatcher
    {
        //private static WebClient _webClient = new WebClient();
        private static readonly Dictionary<string, EDEvent> _commanderStatuses = new Dictionary<string, EDEvent>();

        private static readonly Timer _updateTimer = new Timer(1000);
        private static bool _enabled = false;
        private static readonly object _lock = new object();
        private static string _lastStatus = "";
        private static string _serverUrl = "";
        private static byte _outstandingRequests = 0;
        private static DateTime _lastCheckForStaleData = DateTime.MinValue;
        private static int _startCount = 0;

        public delegate void UpdateReceivedEventHandler(object sender, EDEvent edEvent);
        public static event UpdateReceivedEventHandler UpdateReceived;
        public static event EventHandler OnlineCountChanged;

        public static void Start(string serverUrl)
        {
            _serverUrl = serverUrl;
            if (!_enabled)
            {
                // We only want one timer, so ensure we only subscribe once
                _updateTimer.Elapsed += _updateTimer_Elapsed;
                _updateTimer.Interval = 750;
                _updateTimer.Start();
                _enabled = true;
            }
            _startCount++;
        }

        public static void Stop()
        {
            if (_enabled)
            {
                _startCount--;
                if (_startCount < 1)
                {
                    // We only stop when we receive the same number of stops as starts
                    _updateTimer.Stop();
                    _updateTimer.Elapsed -= _updateTimer_Elapsed;
                    _enabled = false;
                }
            }
        }

        public static EDEvent GetCommanderMostRecentEvent(string commander)
        {
            if (_commanderStatuses.ContainsKey(commander))
                return _commanderStatuses[commander];
            return null;
        }

        public static EDLocation GetCommanderMostRecentLocation(string commander)
        {
            if (_commanderStatuses.ContainsKey(commander))
                return _commanderStatuses[commander].Location();
            return null;
        }

        public static bool CommanderIsTracking(string commander)
        {
            if (_commanderStatuses.ContainsKey(commander))
                return true;
            return false;
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
            System.Diagnostics.Debug.WriteLine($"CommanderWatcher_Elapsed {DateTime.UtcNow:HH:mm:ss}");
            if (_outstandingRequests > 5)
            {
                System.Diagnostics.Debug.WriteLine($"{_outstandingRequests} requests already active");
                return;
            }

            UpdateAvailableCommanders();
        }



        private static void UpdateAvailableCommanders(string commanderStatus)
        {
            if (_lastStatus.Equals(commanderStatus))
                return;

            System.Diagnostics.Debug.WriteLine($"CommanderWatcher Update Detected {DateTime.UtcNow:HH:mm:ss}");

            bool countChanged = false;

            if (!String.IsNullOrEmpty(commanderStatus))
            {
                // We have something to process (hopefully a list of commander statuses)
                Dictionary<string, EDEvent> currentEvents = (Dictionary<string, EDEvent>)JsonSerializer.Deserialize(commanderStatus, typeof(Dictionary<string, EDEvent>));
                _lastStatus = commanderStatus;

                foreach (string commander in currentEvents.Keys)
                {
                    if (_commanderStatuses.ContainsKey(commander))
                    {
                        if (currentEvents[commander].TimeStamp > _commanderStatuses[commander].TimeStamp)
                        {
                            lock (_lock)
                                _commanderStatuses[commander] = currentEvents[commander];
                            UpdateReceived?.Invoke(null, currentEvents[commander]);
                        }
                    }
                    else
                    {
                        lock (_lock)
                            _commanderStatuses.Add(commander, currentEvents[commander]);
                        countChanged = true;
                        UpdateReceived?.Invoke(null, currentEvents[commander]);
                    }
                }

                if (DateTime.UtcNow.Subtract(_lastCheckForStaleData).TotalMinutes > 1)
                {
                    List<string> missingCommanders = new List<string>();
                    foreach (string storedCommander in _commanderStatuses.Keys)
                        if (!currentEvents.ContainsKey(storedCommander))
                            missingCommanders.Add(storedCommander);

                    if (missingCommanders.Count > 0)
                    {
                        lock (_lock)
                        {
                            foreach (string missingCommander in missingCommanders)
                                _commanderStatuses.Remove(missingCommander);
                        }
                        countChanged = true;
                    }
                }

                if (countChanged)
                    OnlineCountChanged?.Invoke(null, null);
            }
        }

        private static void UpdateAvailableCommanders()
        {
            // Connect to the server and retrieve the list of any currently live commanders

            try
            {
                System.Diagnostics.Debug.WriteLine($"Requesting Commander Status {DateTime.UtcNow:HH:mm:ss}");
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
                webClient.DownloadStringAsync(new Uri($"{_serverUrl}/trackedcommanders"),webClient);
                _outstandingRequests++;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateAvailableCommanders error: {ex}");
                return;
            }
        }

        private static void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            _outstandingRequests--;
            System.Diagnostics.Debug.WriteLine($"Received Commander Status {DateTime.UtcNow:HH:mm:ss}");
            try
            {
                if (!e.Cancelled)
                    UpdateAvailableCommanders(e.Result);
            }
            catch { }
            try
            {
                ((WebClient)e.UserState).Dispose();
            }
            catch { }
        }
    }
}
