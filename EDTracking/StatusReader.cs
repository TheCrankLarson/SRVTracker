using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EDTracking
{
    internal class StatusReader : IDisposable
    {
        private string _statusFile = "";
        private FileStream _statusFileStream = null;
        private System.Timers.Timer _statusCheckTimer = null;
        private DateTime _lastFileWrite = DateTime.MinValue;
        private DateTime _lastStatusUpdate = DateTime.MinValue;
        private DateTime _lastStatusSend = DateTime.MinValue;
        private bool _enable5SecondPing = false;
        private bool disposedValue;

        public delegate void StatusEventHandler(object sender, string eventJson);
        public event StatusEventHandler StatusUpdated;

        StatusReader()
        {
            _statusCheckTimer = new System.Timers.Timer(750);
            _statusCheckTimer.Elapsed += _statusCheckTimer_Elapsed;
            InitStatusLocation();
            StartMonitoring();
        }

        private void _statusCheckTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            // If the file has been written, then process it
            DateTime lastWriteTime = File.GetLastWriteTime(_statusFile);
            if (lastWriteTime > _lastFileWrite)
            {
                _statusCheckTimer.Stop();
                ProcessStatusFileUpdate(_statusFile);
                _lastStatusUpdate = DateTime.UtcNow;
                _lastFileWrite = lastWriteTime;
                _statusCheckTimer.Start();
            }
            else if (_enable5SecondPing && (DateTime.UtcNow.Subtract(_lastStatusSend).TotalSeconds > 5))
                if (StatusUpdated != null)
                    StatusUpdated(this, "");
            //     UploadToServer(null, true); // This is the five second ping in case we are not moving (otherwise server will lose tracking)

        }

        internal void InitStatusLocation()
        {
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\Saved Games\\Frontier Developments\\Elite Dangerous";
            if (File.Exists($"{path}\\Status.json"))
                _statusFile = $"{path}\\Status.json";
        }

        internal void StartMonitoring()
        {
            if (String.IsNullOrEmpty(_statusFile) || _statusCheckTimer.Enabled)
                return;

            _statusCheckTimer.Start();
        }

        public bool IsRunning
        {
            get { return _statusCheckTimer.Enabled; }
        }

        private void ProcessStatusFileUpdate(string statusFile, bool updateTimeStamp = false)
        {
            // Read the status from the file and check if it has changed
            if (String.IsNullOrEmpty(statusFile))
                return;

            string status = "";
            try
            {
                // Read the file - we open in file share mode as E: D will be constantly writing to this file
                if (_statusFileStream == null)
                {
                    _statusFileStream = new FileStream(statusFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
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

            try
            {
                // E: D status.json file does not include milliseconds in the timestamp.  We want milliseconds, so we add our own timestamp
                // This also gives us polling every five seconds in case the commander stops moving (as soon as they move, the new status should be picked up)
                // Turns out milliseconds is pointless as E: D is very unlikely to generate a new status file more than once a second (and/or we won't detect it), but
                // we'll keep them in case this changes in future.

                if (StatusUpdated != null)
                    StatusUpdated(this,status);
                EDEvent updateEvent;
                //if (updateTimeStamp)
                //    updateEvent = new EDEvent(status, textBoxCommanderName.Text, DateTime.UtcNow);
                //else
                //    updateEvent = new EDEvent(status, textBoxCommanderName.Text);

                //UpdateUI(updateEvent);
            }
            catch { }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_statusCheckTimer != null && _statusCheckTimer.Enabled)
                        _statusCheckTimer.Stop();
                    if (_statusCheckTimer == null)
                        _statusCheckTimer.Dispose();
                    _statusCheckTimer = null;

                    if (_statusFileStream != null)
                    {
                        _statusFileStream.Close();
                        _statusFileStream.Dispose();
                    }
                    _statusFileStream = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~StatusReader()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
