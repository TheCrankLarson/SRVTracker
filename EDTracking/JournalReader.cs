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
