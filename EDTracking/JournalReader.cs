using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EDTracking
{
    public class JournalReader
    {
        private string _journalDirectory = null;
        private string _activeJournalFile = null;
        private DateTime _lastFileWrite = DateTime.MinValue;
        private System.Timers.Timer _statusCheckTimer = null;
        private Dictionary<string, long> _filePointers;

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

            // If the file has been written, then process it
            DateTime lastWriteTime = File.GetLastWriteTime(_activeJournalFile);
            if (lastWriteTime != _lastFileWrite)
            {
                ProcessJournalFileUpdate(_activeJournalFile);
                _lastFileWrite = lastWriteTime;
            }
            _statusCheckTimer.Start();
        }

        private void FindActiveJournalFile()
        {
            // Journal files are named Journal.200906152959.01.log, which is Journal.yymmddhhmmss.01.log
            // However, we are probably interested in the Journal.cache file this seems to contain recent events
            string journalPrefix = $"Journal.{DateTime.UtcNow:yy:MM:dd}";
        }

        private void ProcessJournalFileUpdate(string journalFile)
        {
            long filePointer = 0;
            if (_filePointers.ContainsKey(journalFile))
                filePointer = _filePointers[journalFile];


        }
    }
}
