using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusMonitor
{
    internal class StatusReader
    {
        public delegate void StatusChangedEventHandler(object sender, string statusJson);
        public event StatusChangedEventHandler StatusChanged;

        private string _statusFile = null;
        private DateTime _lastFileWrite = DateTime.MinValue;
        private FileStream _statusFileStream = null;
        private System.Timers.Timer _statusCheckTimer = null;
        public int _updateIntervalInMs = 1000;

        public StatusReader()
        {
            string journalDirectory = JournalReader.EDJournalPath();
            if (!String.IsNullOrEmpty(journalDirectory))
            {
                _statusFile = $"{journalDirectory}\\status.json";
                _statusCheckTimer = new System.Timers.Timer(_updateIntervalInMs);
                _statusCheckTimer.Elapsed += _statusCheckTimer_Elapsed;
                _statusCheckTimer.Start();
            }
        }

        private void _statusCheckTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (String.IsNullOrEmpty(_statusFile))
                return;

            _statusCheckTimer.Stop();
            if (_statusFileStream==null)
            {
                if (File.Exists(_statusFile))
                {
                    _statusFileStream = new FileStream(_statusFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
            }

            if (_statusFileStream != null)
            {
                DateTime lastWriteTime = File.GetLastWriteTime(_statusFile);
                if (lastWriteTime > _lastFileWrite)
                {
                    _lastFileWrite = lastWriteTime;
                    ProcessStatusUpdate();
                }
            }
            _statusCheckTimer.Start();
        }

        private void ProcessStatusUpdate()
        {
            string status = "";
            try
            {
                // Read the file - we open in file share mode as E: D will be constantly writing to this file
                _statusFileStream.Seek(0, SeekOrigin.Begin);

                using (StreamReader sr = new StreamReader(_statusFileStream, Encoding.Default, true, 1000, true))
                    status = sr.ReadToEnd();

                if (!_statusFileStream.CanSeek)
                {
                    // We only close the file if we can't seek (no point in continuously reopening)
                    _statusFileStream.Close();
                    _statusFileStream.Dispose();
                    _statusFileStream = null;
                }
            }
            catch { }
            if (String.IsNullOrEmpty(status))
                return;

            StatusChanged?.Invoke(this, status);
        }
    }
}
