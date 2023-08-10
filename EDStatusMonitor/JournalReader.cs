using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace EDStatusMonitor
{
    internal class JournalReader
    {
        private string _journalDirectory = null;
        private string _activeJournalFile = null;
        private List<string> _oldJournals = new List<string>();
        private DateTime _lastFileWrite = DateTime.MinValue;
        private FileStream _journalFileStream = null;
        private System.Timers.Timer _statusCheckTimer = null;
        private Dictionary<string, long> _filePointers;
        private DateTime _lastJournalEventTimeStamp = DateTime.MinValue;
        public bool ReportAllEvents = true;
        public string[] ReportEvents = { "DockSRV","SRVDestroyed", "FighterDestroyed","HullDamage","LaunchSRV", "Shutdown", "Continued", "Touchdown", "Liftoff", "Died",
            "UnderAttack", "MaterialCollected", "DatalinkScan", "DataScanned", "ApproachSettlement", "ShipTargeted", "Commander", "Synthesis"};
        public delegate void InterestingEventHandler(object sender, string eventJson);
        public event InterestingEventHandler InterestingEventOccurred;
        public string CommanderName = "";
        public int _updateIntervalInMs = 1000;

        public JournalReader()
        {
            _journalDirectory = EDJournalPath();
            _statusCheckTimer = new System.Timers.Timer(_updateIntervalInMs);
            _statusCheckTimer.Elapsed += _statusCheckTimer_Elapsed;
            _filePointers = new Dictionary<string, long>();
            FindActiveJournalFile();
        }

        public static string EDJournalPath()
        {
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\Saved Games\\Frontier Developments\\Elite Dangerous";
            if (Directory.Exists(path))
                return path;
            return "";
        }

        private void _statusCheckTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _statusCheckTimer.Stop();

            if (String.IsNullOrEmpty(_activeJournalFile))
            {
                FindActiveJournalFile();
                if (!String.IsNullOrEmpty(_activeJournalFile))
                    _statusCheckTimer.Interval = _updateIntervalInMs;
            }

            if (!String.IsNullOrEmpty(_activeJournalFile))
            {
                // If the file has been written, then process it
                DateTime lastWriteTime = File.GetLastWriteTime(_activeJournalFile);
                if (lastWriteTime != _lastFileWrite)
                {
                    ProcessJournalFileUpdate(_activeJournalFile);
                    _lastFileWrite = lastWriteTime;
                }
                else if (_lastFileWrite<DateTime.Now.AddMinutes(5))
                {
                    // File hasn't been written for over five minutes, potentially game could have crashed
                    // and we are now watching the wrong file
                    DeactivateJournal(false);  // Trigger a search for the current journal
                }
            }
            _statusCheckTimer.Start();
        }

        private bool FindActiveJournalFile()
        {
            // Journal files are named Journal.200906152959.01.log, which is Journal.yymmddhhmmss.01.log
            //string journalPrefix = $"Journal.{DateTime.UtcNow:yy:MM:dd}";

            // EDO journal files are different format:
            // Journal.2023-02-25T230904.01.log

            // We first check that Elite Dangerous is running, as if it isn't we don't have a journal to read
            System.Diagnostics.Process[] edClients = System.Diagnostics.Process.GetProcessesByName("EliteDangerous64");
            if (edClients.Length < 1)
                return false;

            string searchFilterLegacy = $"Journal.{DateTime.Today:yyMMdd}*.log";
            string searchFilterOdyssey = $"Journal.{DateTime.Today:yyyy-MM-dd}*.log";
            string[] cacheFiles = (string[])Directory.GetFiles(_journalDirectory, searchFilterLegacy).Concat(Directory.GetFiles(_journalDirectory, searchFilterOdyssey));
            if (cacheFiles.Length == 0)
                return false;

            // We find the most recently created journal log with today's date
            string mostRecentFile = "";
            DateTime mostRecentFileTime = DateTime.MinValue;
            foreach (string fileName in cacheFiles)
                if (String.IsNullOrEmpty(mostRecentFile) || (File.GetCreationTimeUtc(fileName) > mostRecentFileTime))
                {
                    if (!JournalFileIncludesShutdown(fileName))
                    {
                        mostRecentFile = fileName;
                        mostRecentFileTime = File.GetCreationTimeUtc(fileName);
                    }
                }

            if (!String.IsNullOrEmpty(mostRecentFile))
            {
                // We've found the most recent log.
                _activeJournalFile = mostRecentFile;
                return true;
            }

            return false;
        }

        private bool JournalFileIncludesShutdown(string fileName)
        {
            if (_oldJournals.Contains(fileName))
                return true;

            // We just need to read the last 62 bytes, as that should capture both Shutdown and Continued events (which both mean this log file won't be written anymore)

            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            if (fileStream.Length > 62)
                fileStream.Seek(fileStream.Length - 62, SeekOrigin.Begin);

            string lastJournalEvent = "";
            using (StreamReader sr = new StreamReader(fileStream, Encoding.Default, true, 63, false))
                lastJournalEvent = sr.ReadToEnd();

            int jsonStart = lastJournalEvent.IndexOf('{');
            if (jsonStart < 0)
                return false;

            string lastEventName = GetJournalEventName(lastJournalEvent.Substring(jsonStart));
            if (!lastEventName.Equals("Shutdown") && !lastEventName.Equals("Continued"))
                return false;

            _oldJournals.Add(fileName);  // So that we don't bother reading this one again
            return true;
        }

        public void StartMonitoring()
        {
            _statusCheckTimer.Interval = 1000;
            if (String.IsNullOrEmpty(_activeJournalFile))
                if (!FindActiveJournalFile())
                    _statusCheckTimer.Interval = 10000;

            _statusCheckTimer.Start();
        }

        public void StopMonitoring()
        {
            _statusCheckTimer.Stop();
        }

        public void Replay()
        {
            if (!String.IsNullOrEmpty(_activeJournalFile))
                ProcessJournalFileUpdate(_activeJournalFile, true);
        }

        private string GetJournalEventName(string journalEvent)
        {
            try
            {
                using (JsonDocument jsonDoc = JsonDocument.Parse(journalEvent))
                {
                    return jsonDoc.RootElement.GetProperty("event").GetString();
                }
            }
            catch
            {
                return "";
            }
        }

        private void DeactivateJournal(bool archive = true)
        {
            if (archive)
                _oldJournals.Add(_activeJournalFile);
            _activeJournalFile = "";
            _journalFileStream?.Close();
            _journalFileStream = null;
        }

        private void ProcessJournalFileUpdate(string journalFile, bool resetFilePointer = false)
        {
            long filePointer = 0;
            if (!resetFilePointer && _filePointers.ContainsKey(journalFile))
                filePointer = _filePointers[journalFile];

            string newJournalEvents = "";
            try
            {
                // Read the file - we open in file share mode as E: D will be constantly writing to this file
                if (_journalFileStream == null)
                {
                    _journalFileStream = new FileStream(journalFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    if (filePointer > _journalFileStream.Length)
                        filePointer = 0;
                    _journalFileStream.Seek(filePointer, SeekOrigin.Begin);
                }
                else if (resetFilePointer)
                    _journalFileStream.Seek(0, SeekOrigin.Begin);

                filePointer = _journalFileStream.Length;
                if (_filePointers.ContainsKey(journalFile))
                    _filePointers[journalFile] = filePointer;
                else
                    _filePointers.Add(journalFile, filePointer);

                using (StreamReader sr = new StreamReader(_journalFileStream, Encoding.Default, true, 4096, true))
                    newJournalEvents = sr.ReadToEnd();

                if (!_journalFileStream.CanSeek)
                {
                    // We only close the file if we can't seek (no point in continuously reopening)
                    _journalFileStream.Close();
                    _journalFileStream = null;
                }
            }
            catch { }
            if (String.IsNullOrEmpty(newJournalEvents))
                return;

            string[] journalEvents = newJournalEvents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string journalEvent in journalEvents)
            {
                try
                {
                    using (JsonDocument jsonDoc = JsonDocument.Parse(journalEvent))
                    {
                        JsonElement timestampElement = jsonDoc.RootElement.GetProperty("timestamp");
                        _lastJournalEventTimeStamp = timestampElement.GetDateTime();
                        JsonElement eventElement = jsonDoc.RootElement.GetProperty("event");
                        string eventName = eventElement.GetString();
                        if (eventName.Equals("Shutdown"))
                        {
                            // We won't receive any more events into this log file
                            // Chances are we're shutting down, but we'll go into slow ping mode in case it is a game restart
                            _statusCheckTimer.Interval = 10000;
                            DeactivateJournal();
                        }
                        else if (eventName.Equals("Continued"))
                        {
                            // We won't receive any more events into this log file, but a new one should be created
                            DeactivateJournal();
                        }
                        else if (eventName.Equals("Commander"))
                        {
                            // This event gives us the Commander name
                            JsonElement commanderNameElement = jsonDoc.RootElement.GetProperty("Name");
                            CommanderName = commanderNameElement.GetString();
                        }
                        else if (ReportAllEvents || ReportEvents.Contains(eventName))
                        {
                            // This is an event we are interested in
                            //File.AppendAllText("journalevents.log", $"{journalEvent}{Environment.NewLine}");
                            InterestingEventOccurred?.Invoke(this, journalEvent);
                        }
                    }
                }
                catch { }
            }
        }
    }
}
