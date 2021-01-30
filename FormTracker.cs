using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Runtime.InteropServices;
using EDTracking;


namespace SRVTracker
{
    public partial class FormTracker : Form
    {
        UdpClient _udpClient = null;
        const string ClientIdFile = "client.id";
        private string _lastUpdateTime = "";
        private FileStream _statusFileStream = null;
        private System.Timers.Timer _statusTimer = null;
        private string _statusFile = "";
        private DateTime _lastFileWrite = DateTime.MinValue;
        private DateTime _lastStatusSend = DateTime.MinValue;
        private Size _configShowing = new Size(738, 392);
        private Size _configHidden = new Size(298, 222);
        private static FormFlagsWatcher _formFlagsWatcher = null;
        private static string _clientId = null;
        private JournalReader _journalReader = null;
        public static event EventHandler CommanderLocationChanged;

        public static EDLocation CurrentLocation { get; private set; } = new EDLocation();
        public static EDLocation PreviousLocation { get; private set; } = new EDLocation();
        public static int CurrentHeading { get; private set; } = -1;

        private SRVTelemetry _srvTelemetry = null;

        private ConfigSaverClass _formConfig = null;
        const double STATUS_JSON_CHECK_INTERVAL = 700;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public FormTracker()
        {            
            InitializeComponent();

            _statusTimer = new System.Timers.Timer(STATUS_JSON_CHECK_INTERVAL);
            _statusTimer.Elapsed += _statusTimer_Elapsed;
            _journalReader = new JournalReader(EDJournalPath());
            _journalReader.InterestingEventOccurred += _journalReader_InterestingEventOccurred;
            FormLocator.ServerAddress = (string)radioButtonUseDefaultServer.Tag;
            InitStatusLocation();
            InitClientId();

            // Attach our form configuration saver
            _formConfig = new ConfigSaverClass(this, true);
            _formConfig.ExcludedControls.Add(textBoxClientId);
            _formConfig.ExcludedControls.Add(textBoxStatusFile);
            _formConfig.SaveEnabled = true;
            _formConfig.RestoreFormValues();

            buttonTest.Visible = System.Diagnostics.Debugger.IsAttached;
            CalculateWindowSizes();

            this.Size = _configHidden;
            this.Text = Application.ProductName + " v" + Application.ProductVersion;
            groupBoxSRVTracker.Text = this.Text;
            if (!File.Exists("Race Manager.exe"))
                buttonRaceTracker.Enabled = false;
            radioButtonUseTimer.Checked = true;
            if (checkBoxAutoUpdate.Checked)
                CheckForUpdate();

            if (checkBoxCaptureSRVTelemetry.Tag != null)
                _srvTelemetry = new SRVTelemetry((string)checkBoxCaptureSRVTelemetry.Tag, _clientId);
            else
                _srvTelemetry = new SRVTelemetry();
            if (checkBoxShowSRVTelemetry.Checked)
                _srvTelemetry.DisplayTelemetry();
        }

        private void CalculateWindowSizes()
        {
            // Calculate size with setting hidden
            int leftBound = groupBoxSRVTracker.Location.X + groupBoxSRVTracker.Width + 10;
            _configHidden.Width = (this.Width-this.ClientRectangle.Width) + leftBound;
            int bottomBound = buttonShowConfig.Location.Y + buttonShowConfig.Height;
            _configHidden.Height = (this.Height - this.ClientRectangle.Height) + bottomBound + 6;

            // Calculate size with config displayed
            _configShowing.Width = tabControlSettings.Location.X + tabControlSettings.Width + (this.Width - this.ClientRectangle.Width) + 10;
            _configShowing.Height = tabControlSettings.Location.Y + tabControlSettings.Height + (this.Height - this.ClientRectangle.Height) + 10;
        }

        private void _journalReader_InterestingEventOccurred(object sender, string eventJson)
        {
            EDEvent updateEvent = new EDEvent(eventJson, textBoxClientId.Text);
            UpdateUI(updateEvent);
        }

        private void _statusTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _statusTimer.Stop();

            // If the file has been written, then process it
            DateTime lastWriteTime = File.GetLastWriteTime(_statusFile);
            if ((lastWriteTime != _lastFileWrite) || (DateTime.Now.Subtract(_lastStatusSend).TotalSeconds > 5))
            {
                ProcessStatusFileUpdate(_statusFile);
                _lastFileWrite = lastWriteTime;
            }
            else if ( DateTime.Now.Subtract(_lastStatusSend).TotalSeconds > 5 )
                ProcessStatusFileUpdate(_statusFile, true); // This is the five second ping when we are watching for file updates

            _statusTimer.Start();
        }

        private void InitClientId()
        {
            // Check if we have an Id saved, and if not, generate one
            if (!File.Exists(ClientIdFile))
            {
                // First run, so show splash and prompt for commander name
                using (FormFirstRun formFirstRun = new FormFirstRun())
                {
                    formFirstRun.ShowDialog(this);
                    _clientId = formFirstRun.textBoxCommanderName.Text;
                    if (String.IsNullOrEmpty(_clientId))
                    {
                        _clientId = ReadCommanderNameFromJournal();
                        if (String.IsNullOrEmpty(_clientId))
                        {
                            _clientId = Guid.NewGuid().ToString();
                        }
                    }
                    try
                    {
                        File.WriteAllText(ClientIdFile, _clientId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, $"Error saving client Id to file: {Environment.NewLine}{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                try
                {
                    // Read the file
                    _clientId = File.ReadAllText(ClientIdFile);
                }
                catch { }
            }

            if (!String.IsNullOrEmpty(_clientId))
                textBoxClientId.Text = _clientId;
        }

        public static string ClientId
        {
            get
            {
                return _clientId;
            }
        }

        private string ReadCommanderNameFromJournal()
        {
            // E: D writes the commander name to the journal.  We locate the most recent journal and attempt to read it from there.
            string path = EDJournalPath();
            if (!String.IsNullOrEmpty(path))
            {
                string[] files = Directory.GetFiles(path, "Journal*.cache",SearchOption.TopDirectoryOnly);
            }
            return null; // Not currently implemented
        }

        public void InitStatusLocation()
        {
            if (File.Exists($"{EDJournalPath()}\\Status.json"))
            {
                textBoxStatusFile.Text = EDJournalPath();
                return;
            }
        }

        private string EDJournalPath()
        {
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\Saved Games\\Frontier Developments\\Elite Dangerous";
            if (Directory.Exists(path))
                return path;
            return "";
        }

        private void statusFileWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            if (e.FullPath.ToLower().EndsWith("status.json"))
            {
                // Create a task to process the status (we return as quickly as possible from the event procedure
                Task.Factory.StartNew(() => ProcessStatusFileUpdate(e.FullPath));
            }
        }

        private void ProcessStatusFileUpdate(string statusFile, bool updateTimeStamp = false)
        {
            // Read the status from the file and update the UI
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
                    _statusFileStream = null;
                }
            }
            catch  {}
            if (String.IsNullOrEmpty(status))
                return;

            try
            {
                // E: D status.json file does not include milliseconds in the timestamp.  We want milliseconds, so we add our own timestamp
                // This also gives us polling every five seconds in case the commander stops moving (as soon as they move, the new status should be picked up)
                // Turns out milliseconds is pointless as E: D is very unlikely to generate a new status file more than once a second (and/or we won't detect it), but
                // we'll keep them in case this changes in future.
                EDEvent updateEvent;
                if (updateTimeStamp)
                    updateEvent = new EDEvent(status, textBoxClientId.Text, DateTime.Now);
                else
                    updateEvent = new EDEvent(status, textBoxClientId.Text);

                if (updateEvent.Flags != 0)
                    UpdateUI(updateEvent);
            }
            catch { }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            //FormDrone formDrone = new FormDrone();
            //formDrone.Show();
            //SendTestEvents();

            //TestVROverlay();
            //return;
            //if (_formFlagsWatcher != null)
            //    return;
            //_formFlagsWatcher = new FormFlagsWatcher();
            //_formFlagsWatcher.Show();
            //FormVRMatrixTest formVRMatrixTest = new FormVRMatrixTest(0);
            //formVRMatrixTest.SetOverlayWidth(0.8f);
            //formVRMatrixTest.Show();
            //formVRMatrixTest.SetMatrix(ref _vrMatrix);
        }



        private void SendTestEvents()
        {
            EDEvent edEvent;
            //"{ \"timestamp\":\"2020 - 07 - 28T17: 46:47Z\", \"event\":\"Status\", \"Flags\":16777229, \"Pips\":[4,8,0], \"FireGroup\":0, \"GuiFocus\":0, \"Fuel\":{ \"FuelMain\":16.000000, \"FuelReservoir\":0.430376 }, \"Cargo\":4.000000, \"LegalState\":\"Clean\" }");

            if (_udpClient == null)
                _udpClient = new UdpClient(textBoxUploadServer.Text, 11938);

            buttonTest.Enabled = false;

            Action action = new Action(() =>
            {
                Random rnd = new Random();
                

                for (int i = 0; i < 20; i++)
                {
                    string commanderName = $"Commander {i + 1}";
                    double latitude = -47.312565;
                    double longitude = -133.180405;
                    for (int j = 0; j < 20; j++)
                    {
                        edEvent = new EDEvent($"{{\"timestamp\":\"{String.Format("{0:s}", DateTime.Now)}\", \"event\":\"Status\", \"Flags\":69206272, \"Pips\":[4,8,0], \"FireGroup\":0, \"GuiFocus\":0, \"Fuel\":{{\"FuelMain\":0.000000, \"FuelReservoir\":0.444637 }}, \"Cargo\":0.000000, \"LegalState\":\"Clean\", \"Latitude\":{latitude}, \"Longitude\":{longitude}, \"Heading\":24, \"Altitude\":0, \"BodyName\":\"Djambe ABC1\", \"PlanetRadius\":1311227.875000}}", commanderName);
                        UpdateUI(edEvent);
                        if (j==0)
                            System.Threading.Thread.Sleep(500);
                        else
                            System.Threading.Thread.Sleep(10);
                        if (rnd.Next(2) == 1)
                            latitude += rnd.NextDouble() / 100;
                        if (rnd.Next(2) == 1)
                            longitude += rnd.NextDouble() / 100;
                    }
                }
                Action enableTest = new Action(() => { buttonTest.Enabled = true; });
                if (buttonTest.InvokeRequired)
                    buttonTest.Invoke(enableTest);
                else
                    enableTest();
            });
            Task.Run(action);
        }

        private void UpdateUI(EDEvent edEvent)
        {
            //if (checkBoxSaveTelemetryFolder.Checked)
            //    SaveToFile(edEvent);

            if (checkBoxUpload.Checked)
                UploadToServer(edEvent);

            _formFlagsWatcher?.UpdateFlags(edEvent.Flags);

            if ( (edEvent.PlanetRadius > 0) && (FormLocator.PlanetaryRadius != edEvent.PlanetRadius) )
                FormLocator.PlanetaryRadius = edEvent.PlanetRadius;

            // Update the UI with the event data
            Action action;
            if (!_lastUpdateTime.Equals(edEvent.TimeStamp.ToString("HH:MM:ss")))
            {
                _lastUpdateTime = edEvent.TimeStamp.ToString("HH:MM:ss");
                action = new Action(() => { labelLastUpdateTime.Text = _lastUpdateTime; });
                if (labelLastUpdateTime.InvokeRequired)
                    labelLastUpdateTime.Invoke(action);
                else
                    action();
            }

            if (checkBoxCaptureSRVTelemetry.Checked)
                _srvTelemetry?.ProcessEvent(edEvent);

            if (edEvent.HasCoordinates())
            {
                if (checkBoxUseDirectionOfTravelAsHeading.Checked)
                {
                    // We ignore the heading given by E: D, as that is direction we are facing, not travelling
                    // We calculate our direction based on previous location
                    double distanceBetweenLocations = EDLocation.DistanceBetween(PreviousLocation, edEvent.Location());
                    if (distanceBetweenLocations > 1)
                        CurrentHeading = (int)EDLocation.BearingToLocation(PreviousLocation, edEvent.Location());
                }
                else
                    CurrentHeading = edEvent.Heading;

                PreviousLocation = CurrentLocation.Copy();
                CurrentLocation.Latitude = edEvent.Latitude;
                CurrentLocation.Longitude = edEvent.Longitude;
                CurrentLocation.Altitude = edEvent.Altitude;
                if (!String.IsNullOrEmpty(edEvent.BodyName))
                    CurrentLocation.PlanetName = edEvent.BodyName;
                if (edEvent.PlanetRadius > 0)
                    CurrentLocation.PlanetaryRadius = edEvent.PlanetRadius;

                CommanderLocationChanged?.Invoke(null, null);
            }

            action = new Action(() => { labelLastUpdateTime.Text = DateTime.Now.ToString("HH:mm:ss"); });
            if (labelLastUpdateTime.InvokeRequired)
                labelLastUpdateTime.Invoke(action);
            else
                action();
        }

        public int SpeedInMS
        {
            get { return _srvTelemetry.CurrentGroundSpeed; }
        }

        private void SaveToFile(EDEvent edEvent)
        {
            try
            {
                // This is very inefficient, save to file should only be enabled for debugging
                // I may revisit this at some point if more features are added for local tracking
                string eventData = eventData = edEvent.ToJson();
                System.IO.File.AppendAllText(textBoxTelemetryFolder.Text, eventData);
            }
            catch (Exception ex)
            {
                checkBoxSaveTelemetryFolder.Checked = false;
            }
        }

        private void UploadToServer(EDEvent edEvent)
        {
            if (_udpClient == null)
                CreateUdpClient();
            try
            {
                string eventData = edEvent.ToJson();
                Byte[] sendBytes = Encoding.UTF8.GetBytes(eventData);
                try
                {
                    _udpClient.Send(sendBytes, sendBytes.Length);
                    _lastStatusSend = DateTime.Now;
                }
                catch (Exception e)
                {
                }
            }
            catch (Exception ex)
            {
                checkBoxUpload.Checked = false;
            }
        }

        private void buttonBrowseStatusFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = textBoxStatusFile.Text;
                openFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = false;
                openFileDialog.FileName = "Status.json";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        textBoxStatusFile.Text = new FileInfo(openFileDialog.FileName).Directory.FullName;
                    }
                    catch { }
                }
            }
        }

        private void textBoxClientId_TextChanged(object sender, EventArgs e)
        {
            if (textBoxClientId.Text.Contains(','))
                textBoxClientId.Text = textBoxClientId.Text.Replace(",","");
            if (!textBoxClientId.Text.Equals(_clientId))
            {
                _clientId = textBoxClientId.Text;
                try
                {
                    File.WriteAllText(ClientIdFile, textBoxClientId.Text); // Too noisy, as it writes after every change! Too lazy to optimise this
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void buttonShowConfig_Click(object sender, EventArgs e)
        {
            if (this.Size == _configHidden)
            {
                this.Size = _configShowing;
                this.Refresh();
                return;
            }

            this.Size = _configHidden;
            this.Refresh();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLocator_Click(object sender, EventArgs e)
        {
            FormLocator.GetLocator(true);
        }

        private void textBoxUploadServer_TextChanged(object sender, EventArgs e)
        {
            FormLocator.ServerAddress = textBoxUploadServer.Text;
        }

        private void CreateUdpClient()
        {
            // Create the UDP client for sending tracking data
            try
            {
                string serverUrl = (string)radioButtonUseDefaultServer.Tag;
                if (radioButtonUseCustomServer.Checked)
                    serverUrl = textBoxUploadServer.Text;
                _udpClient = new UdpClient(serverUrl, 11938);
            }
            catch (Exception ex)
            {
                checkBoxUpload.Checked = false;
            }
        }

        private void checkBoxUpload_CheckedChanged(object sender, EventArgs e)
        {
            UpdateServerSettings();
            if (checkBoxUpload.Checked)
            {
                // Create the UDP client for sending tracking data
                CreateUdpClient();
            }
            else  if (_udpClient != null)
            { 
                _udpClient.Dispose();
                _udpClient = null;
            }
        }

        private void UpdateServerSettings()
        {
            textBoxUploadServer.Enabled = radioButtonUseCustomServer.Checked;
            if (radioButtonUseDefaultServer.Checked)
                FormLocator.ServerAddress = (string)radioButtonUseDefaultServer.Tag;
            else
                FormLocator.ServerAddress = textBoxUploadServer.Text;
        }

        private void checkBoxSaveToFile_CheckedChanged(object sender, EventArgs e)
        {
            textBoxTelemetryFolder.Enabled = checkBoxSaveTelemetryFolder.Checked;
            buttonBrowseTelemetryFolder.Enabled = checkBoxSaveTelemetryFolder.Checked;
            if (!checkBoxSaveTelemetryFolder.Checked)
                SRVTelemetry.SessionSaveFolder = "";
            else
                SRVTelemetry.SessionSaveFolder = textBoxTelemetryFolder.Text;
        }

        private void radioButtonUseDefaultServer_CheckedChanged(object sender, EventArgs e)
        {
            UpdateServerSettings();
        }

        private void radioButtonUseCustomServer_CheckedChanged(object sender, EventArgs e)
        {
            UpdateServerSettings();
        }

        private void buttonRoutePlanner_Click(object sender, EventArgs e)
        {
            FormRouter formRouter = new FormRouter(this);
            formRouter.Show();
        }
        
        public void StartTracking()
        {
            _statusFile = $"{textBoxStatusFile.Text}\\Status.json";

            if (radioButtonWatchStatusFile.Checked)
            {
                if (!statusFileWatcher.EnableRaisingEvents)
                {
                    try
                    {
                        statusFileWatcher.Path = textBoxStatusFile.Text;
                        statusFileWatcher.Filter = "Status.json";
                        statusFileWatcher.NotifyFilter = System.IO.NotifyFilters.LastWrite;
                        statusFileWatcher.EnableRaisingEvents = true;
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }
                // File events enabled, but we also add a 5 second timer ping also
                _statusTimer.Interval = 5000;
                _statusTimer.Start();
            }
            else
            {
                if (!_statusTimer.Enabled)
                {
                    _statusTimer.Interval = STATUS_JSON_CHECK_INTERVAL;
                    if (File.Exists(_statusFile))
                    {
                        _statusTimer.Start();
                    }
                    else
                    {
                        _statusFile = "";
                        checkBoxTrack.Checked = false;
                        return;
                    }
                }
            }
            if (!checkBoxTrack.Checked)
                checkBoxTrack.Checked = true;
            _journalReader.StartMonitoring();
        }

        private void StopTracking()
        {
            // Stop tracking
            statusFileWatcher.EnableRaisingEvents = false;
            _statusTimer.Stop();
            try
            {
                _udpClient?.Close();
            }
            catch { }
            _udpClient = null;
            _journalReader.StopMonitoring();
        }

        private void checkBoxTrack_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTrack.Checked)
            {
                if (checkBoxUpload.Checked)
                {
                    if (String.IsNullOrEmpty(textBoxClientId.Text))
                        {
                            checkBoxUpload.Checked = false;
                            return;
                        }
                }
                StartTracking();
                return;
            }

            StopTracking();
        }

        private void buttonRaceTracker_Click(object sender, EventArgs e)
        {
            if (File.Exists("Race Manager.exe"))
            {
                // Use the external race manager if it is available
                Updater.LaunchApplication("Race Manager.exe");
                return;
            }
            buttonRaceTracker.Enabled = false; // Manager doesn't exist
        }

        private void radioButtonWatchStatusFile_CheckedChanged(object sender, EventArgs e)
        {
            StopTracking();
            StartTracking();
        }

        private void radioButtonUseTimer_CheckedChanged(object sender, EventArgs e)
        {
            // We only trap events in the other radio button (this is a pair), as they will always both change at the same time
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void CheckForUpdate(bool ShowIfCurrent = false)
        {
            Action action = new Action(() =>
            {
                Updater updater = new Updater();
                updater.ClearUpdateFiles();

                if (updater.RunningVersionIsBeta)
                {
                    // If we're already running a beta version we always check for beta updates
                    Action updateBetaAction = new Action(() =>
                    {
                        checkBoxIncludeBetaUpdates.Checked = true;
                    });
                    if (checkBoxIncludeBetaUpdates.InvokeRequired)
                        checkBoxIncludeBetaUpdates.Invoke(updateBetaAction);
                    else
                        updateBetaAction();
                }

                if (ShowIfCurrent && !updater.UpdateAvailable(checkBoxIncludeBetaUpdates.Checked))
                {
                    MessageBox.Show($"{Application.ProductName} is up to date.", "Update Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (updater.DownloadUpdate(checkBoxIncludeBetaUpdates.Checked))
                    Close();
            });
            Task.Run(action);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            CheckForUpdate(true);
        }

        private void checkBoxCaptureSRVTelemetry_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxExportSRVTelemetry.Enabled = checkBoxCaptureSRVTelemetry.Checked;
            checkBoxShowSRVTelemetry.Enabled = checkBoxCaptureSRVTelemetry.Checked;
        }

        private void buttonSRVTelemetryExportSettings_Click(object sender, EventArgs e)
        {
            _srvTelemetry.EditSettings(checkBoxCaptureSRVTelemetry, this);
        }

        private void checkBoxShowSRVTelemetry_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowSRVTelemetry.Checked)
                _srvTelemetry?.DisplayTelemetry();
            else
                _srvTelemetry?.HideTelemetry();
        }

        private void checkBoxExportSRVTelemetry_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonNewSession_Click(object sender, EventArgs e)
        {
            _srvTelemetry?.NewSession();
        }

        private void textBoxTelemetryFolder_Validated(object sender, EventArgs e)
        {
            SRVTelemetry.SessionSaveFolder = textBoxTelemetryFolder.Text;
        }

        private void buttonBrowseTelemetryFolder_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = textBoxTelemetryFolder.Text;
                saveFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = false;
                saveFileDialog.FileName = "Session.json";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        textBoxTelemetryFolder.Text = new FileInfo(saveFileDialog.FileName).Directory.FullName;
                    }
                    catch { }
                }
            }
        }

        private void FormTracker_FormClosing(object sender, FormClosingEventArgs e)
        {
            _srvTelemetry?.SaveSession();
        }
    }
}
