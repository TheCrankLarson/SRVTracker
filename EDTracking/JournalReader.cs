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
        private DateTime _lastFileWrite = DateTime.MinValue;
        private FileStream _journalFileStream = null;
        private System.Timers.Timer _statusCheckTimer = null;
        private Dictionary<string, long> _filePointers;
        private DateTime _lastJournalEventTimeStamp = DateTime.MinValue;
        public string[] ReportEvents = { "DockSRV","SRVDestroyed","HullDamage","LaunchSRV" };

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
                return;

            // If the file has been written, then process it
            DateTime lastWriteTime = File.GetLastWriteTime(_activeJournalFile);
            if (lastWriteTime != _lastFileWrite)
            {
                ProcessJournalFileUpdate(_activeJournalFile);
                _lastFileWrite = lastWriteTime;
            }
            _statusCheckTimer.Start();
        }

        private bool FindActiveJournalFile()
        {
            // Journal files are named Journal.200906152959.01.log, which is Journal.yymmddhhmmss.01.log
            // However, we are probably interested in the Journal.cache file this seems to contain recent events
            //string journalPrefix = $"Journal.{DateTime.UtcNow:yy:MM:dd}";

            string[] cacheFiles = Directory.GetFiles(_journalDirectory, "Journal*.cache");
            if (cacheFiles.Length==1)
            {
                _activeJournalFile = cacheFiles[0];
                return true;
            }

            // We find the cache file that has been written to today.  This will usually work, and is only likely to fail if player is running beta or other versions
            // of the game on the same machine
            foreach (string fileName in cacheFiles)
                if (File.GetLastWriteTime($"{_journalDirectory}\\{fileName}").Date == DateTime.Today.Date)
                {
                    _activeJournalFile = fileName;
                    return true;
                }

            return false;
        }

        public void StartMonitoring()
        {
            if (String.IsNullOrEmpty(_activeJournalFile))
                if (!FindActiveJournalFile())
                    return;

            _statusCheckTimer.Start();
        }

        public void StopMonitoring()
        {
            _statusCheckTimer.Stop();
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
                        JsonElement eventElement = jsonDoc.RootElement.GetProperty("event");
                        if (ReportEvents.Contains(eventElement.GetString()))
                        {
                            // This is an event we are interested in
                            File.AppendAllText("journalevents.log", journalEvent);
                        }
                    }
                }
                catch { }
            }

        }
    }
}
