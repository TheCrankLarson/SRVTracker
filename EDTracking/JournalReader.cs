using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace EDTracking
{
    public class JournalReader
    {
        private string _journalDirectory = null;
        private string _activeJournalFile = null;
        private List<string> _oldJournals = new List<string>();
        private DateTime _lastFileWrite = DateTime.MinValue;
        private FileStream _journalFileStream = null;
        private System.Timers.Timer _statusCheckTimer = null;
        private Dictionary<string, long> _filePointers;
        private DateTime _lastJournalEventTimeStamp = DateTime.MinValue;
        public string[] ReportEvents = { "DockSRV","SRVDestroyed","HullDamage","LaunchSRV", "Shutdown", "Touchdown", "Liftoff" };
        public delegate void InterestingEventHandler(object sender, string eventJson);
        public  event InterestingEventHandler InterestingEventOccurred;

        public JournalReader(string journalFolder)
        {
            _journalDirectory = journalFolder;
            _statusCheckTimer = new System.Timers.Timer(1000);
            _statusCheckTimer.Elapsed += _statusCheckTimer_Elapsed;
            _filePointers = new Dictionary<string, long>();
            FindActiveJournalFile();
        }

        private void _statusCheckTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _statusCheckTimer.Stop();

            if (String.IsNullOrEmpty(_activeJournalFile))
            {
                FindActiveJournalFile();
                if (!String.IsNullOrEmpty(_activeJournalFile))
                    _statusCheckTimer.Interval = 1000;
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
            }
            _statusCheckTimer.Start();
        }

        private bool FindActiveJournalFile()
        {
            // Journal files are named Journal.200906152959.01.log, which is Journal.yymmddhhmmss.01.log
            //string journalPrefix = $"Journal.{DateTime.UtcNow:yy:MM:dd}";

            // We first check that Elite Dangerous is running, as if it isn't we could choose the wrong journal file
            System.Diagnostics.Process[] edClients = System.Diagnostics.Process.GetProcessesByName("EliteDangerous64");
            if (edClients.Length < 1)
                return false;

            string searchFilter = $"Journal.{DateTime.Today:yyMMdd}*.log";
            string[] cacheFiles = Directory.GetFiles(_journalDirectory, searchFilter);
            if (cacheFiles.Length==0)
                return false;

            // We find the most recently created journal log with today's date
            string mostRecentFile = "";
            DateTime mostRecentFileTime = DateTime.MinValue;
            foreach (string fileName in cacheFiles)
                if ( String.IsNullOrEmpty(mostRecentFile) || (File.GetCreationTimeUtc(fileName) > mostRecentFileTime) )
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

            // We just need to read the last 62 bytes, as the Shutdown event should always be the same length

            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            if (fileStream.Length > 62)
                fileStream.Seek(fileStream.Length - 62, SeekOrigin.Begin);

            string lastJournalEvent = "";
            using (StreamReader sr = new StreamReader(fileStream, Encoding.Default, true, 1000, false))
                lastJournalEvent = sr.ReadToEnd();

            int jsonStart = lastJournalEvent.IndexOf('{');
            if (jsonStart < 0)
                return false;

            if (!GetJournalEventName(lastJournalEvent.Substring(jsonStart)).Equals("Shutdown"))
            {
                return false;
            }
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

        private void ProcessJournalFileUpdate(string journalFile)
        {
            long filePointer = 0;
            if (_filePointers.ContainsKey(journalFile))
                filePointer = _filePointers[journalFile];

            string newJournalEvents = "";
            try
            {
                // Read the file - we open in file share mode as E: D will be constantly writing to this file
                if (_journalFileStream == null)
                    _journalFileStream = new FileStream(journalFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                if (filePointer > _journalFileStream.Length)
                    filePointer = 0;
                _journalFileStream.Seek(filePointer, SeekOrigin.Begin);
                filePointer = _journalFileStream.Length;

                using (StreamReader sr = new StreamReader(_journalFileStream, Encoding.Default, true, 1000, true))
                    newJournalEvents = sr.ReadToEnd();

                if (!_journalFileStream.CanSeek)
                {
                    // We only close the file if we can't seek (no point in continuously reopening)
                    _journalFileStream.Close();
                    _journalFileStream = null;
                }
                else
                {
                    if (_filePointers.ContainsKey(journalFile))
                        _filePointers[journalFile] = filePointer;
                    else
                        _filePointers.Add(journalFile, filePointer);
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
                        if (DateTime.UtcNow.Subtract(timestampElement.GetDateTime()).TotalSeconds < 60)
                        {
                            JsonElement eventElement = jsonDoc.RootElement.GetProperty("event");
                            string eventName = eventElement.GetString();
                            if (ReportEvents.Contains(eventName))
                            {
                                // This is an event we are interested in
                                File.AppendAllText("journalevents.log", $"{journalEvent}{Environment.NewLine}");
                                if (eventName.Equals("Shutdown"))
                                {
                                    // We won't receive any more events into this log file
                                    _statusCheckTimer.Interval = 10000;
                                    _activeJournalFile = "";
                                    _journalFileStream?.Close();
                                    _journalFileStream = null;
                                }
                                else
                                    InterestingEventOccurred?.Invoke(this, journalEvent);
                            }
                        }
                    }
                }
                catch { }
            }
        }
    }
}
