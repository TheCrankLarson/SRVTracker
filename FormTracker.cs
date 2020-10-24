using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using EDTracking;
using OpenVR = Valve.VR.OpenVR;
using Valve.VR;

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
        public static CVRSystem VRSystem = null;
        private static FormFlagsWatcher _formFlagsWatcher = null;
        private static string _clientId = null;
        private JournalReader _journalReader = null;
        public static event EventHandler CommanderLocationChanged;
        public static EDLocation CurrentLocation { get; private set; } = new EDLocation();
        public static int CurrentHeading { get; private set; } = -1;
        public static double SpeedInMS { get; internal set; } = 0;
        FormRaceMonitor _formRaceMonitor = null;

        // Keep track of ground speed (E: D shows speed you are travelling in the direction you are facing, which is not ground speed)
        private EDLocation _speedCalculationLocation = null;
        private DateTime _speedCalculationTimeStamp = DateTime.UtcNow;
        private double _lastSpeedInMs = 0;
        private ConfigSaverClass _formConfig = null;

        public FormTracker()
        {            
            InitializeComponent();

            _statusTimer = new System.Timers.Timer(700);
            _statusTimer.Elapsed += _statusTimer_Elapsed;
            _journalReader = new JournalReader(EDJournalPath());
            _journalReader.InterestingEventOccurred += _journalReader_InterestingEventOccurred;

            // Attach our form configuration saver
            _formConfig = new ConfigSaverClass(this, true);
            _formConfig.ExcludedControls.Add(textBoxClientId);
            _formConfig.ExcludedControls.Add(textBoxStatusFile);
            _formConfig.ExcludedControls.Add(textBoxLatitude);
            _formConfig.ExcludedControls.Add(textBoxLongitude);
            _formConfig.ExcludedControls.Add(textBoxAltitude);
            _formConfig.ExcludedControls.Add(textBoxHeading);
            _formConfig.ExcludedControls.Add(textBoxPlanetRadius);
            _formConfig.SaveEnabled = true;
            _formConfig.StoreLabelInfo = false;
            _formConfig.StoreButtonInfo = false;
            ConfigSaverClass.ApplyConfiguration();

            InitClientId();
            InitStatusLocation();
            buttonTest.Visible = System.Diagnostics.Debugger.IsAttached;
            FormLocator.ServerAddress = (string)radioButtonUseDefaultServer.Tag;

            this.Size = _configHidden;
            this.Text = Application.ProductName + " v" + Application.ProductVersion;
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
            if ( (lastWriteTime != _lastFileWrite) || ( DateTime.Now.Subtract(_lastStatusSend).TotalSeconds>5 ) )
            {
                ProcessStatusFileUpdate(_statusFile);
                _lastFileWrite = lastWriteTime;
                _lastStatusSend = DateTime.Now;
            }
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
                            AddLog("New client Id generated");
                            _clientId = Guid.NewGuid().ToString();
                        }
                    }
                    try
                    {
                        File.WriteAllText(ClientIdFile, _clientId);
                        AddLog($"Saved client Id to file: {ClientIdFile}");
                    }
                    catch (Exception ex)
                    {
                        AddLog($"Error saving client Id to file: {ex.Message}");
                    }
                }
            }
            else
            {
                try
                {
                    // Read the file
                    _clientId = File.ReadAllText(ClientIdFile);
                    AddLog("Restored client Id");
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

        private void AddLog(string log)
        {
            log = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}  {log}";
            Action addLogAction = new Action(() => {
                listBoxLog.BeginUpdate();
                listBoxLog.Items.Add(log);                
                if (listBoxLog.Items.Count > 200)
                {
                    int topOffset = listBoxLog.Items.Count - listBoxLog.TopIndex;
                    listBoxLog.Items.RemoveAt(0);

                    if ( !checkBoxAutoScroll.Checked && (listBoxLog.Items.Count - topOffset >= 0) )
                        listBoxLog.TopIndex = listBoxLog.Items.Count - topOffset;
                }
                if (checkBoxAutoScroll.Checked)
                {
                    listBoxLog.TopIndex = listBoxLog.Items.Count-1;
                }
                listBoxLog.EndUpdate();
            });
            if (listBoxLog.InvokeRequired)
            {
                listBoxLog.Invoke(addLogAction);
            }
            else
                addLogAction();
        }

        private void ProcessStatusFileUpdate(string statusFile)
        {
            // Read the status from the file and update the UI
            string status = "";
            try
            {
                // Read the file - we open in file share mode as E: D will be constantly writing to this file
                if (_statusFileStream == null )
                    _statusFileStream = new FileStream(statusFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
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
                EDEvent updateEvent = new EDEvent(status, textBoxClientId.Text, DateTime.Now);
                if (updateEvent.Flags != 0)
                    UpdateUI(updateEvent);
            }
            catch { }

            try
            {
                if (checkBoxSaveToFile.Checked)
                    File.AppendAllText(textBoxSaveFile.Text, status);
            }
            catch (Exception ex)
            {
                AddLog($"Failed to save to local log file: {ex.Message}");
                Action action = new Action(() => { checkBoxSaveToFile.Checked = false; });
                if (checkBoxSaveToFile.InvokeRequired)
                    checkBoxSaveToFile.Invoke(action);
                else
                    action();
            }
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
            _formFlagsWatcher = new FormFlagsWatcher();
            _formFlagsWatcher.Show();
        }

        public static bool InitVR()
        {
            if (VRSystem == null)
            {
                var initError = EVRInitError.None;
                try
                {
                    VRSystem = OpenVR.Init(ref initError, EVRApplicationType.VRApplication_Overlay);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(null, $"Failed to initialise VR: {ex.Message}\r\nInit error: {initError}", "VR Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void TestVROverlay()
        {
            if (!InitVR()) return;

            var overlay = OpenVR.Overlay;
            ulong overlayHandle = 0;

            overlay.CreateOverlay("overlaySRVTracker", "SRV Tracking", ref overlayHandle);
            overlay.SetOverlayWidthInMeters(overlayHandle, 2.5f);
            overlay.SetOverlayFromFile(overlayHandle, "c:\\temp\\Crank VR.png");
            //overlay.SetOverlayRaw()
            var error = overlay.ShowOverlay(overlayHandle);
            Valve.VR.HmdMatrix34_t hmdMatrix = new HmdMatrix34_t();
            hmdMatrix.m0 = 1.0F;
            hmdMatrix.m1 = 0.0F;
            hmdMatrix.m2 = 0.0F;
            hmdMatrix.m3 = 0.0F;
            hmdMatrix.m4 = 0.0F;
            hmdMatrix.m5 = 1.0F;
            hmdMatrix.m6 = 0.0F;
            hmdMatrix.m7 = 1.0F;
            hmdMatrix.m8 = 0.0F;
            hmdMatrix.m9 = 0.0F;
            hmdMatrix.m10 = 1.0F;
            hmdMatrix.m11 = -2.0F;

            overlay.SetOverlayTransformAbsolute(overlayHandle, Valve.VR.ETrackingUniverseOrigin.TrackingUniverseStanding, ref hmdMatrix);
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
            if (checkBoxSaveToFile.Checked)
                SaveToFile(edEvent);

            if (checkBoxUpload.Checked)
                UploadToServer(edEvent);

            if (_formFlagsWatcher != null)
                _formFlagsWatcher.UpdateFlags(edEvent.Flags);

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

            if (edEvent.HasCoordinates())
            {
                TimeSpan timeBetweenLocations = edEvent.TimeStamp.Subtract(_speedCalculationTimeStamp);
                if (timeBetweenLocations.TotalMilliseconds > 750)
                {
                    // We take a speed calculation once every 750 milliseconds
                    _speedCalculationTimeStamp = edEvent.TimeStamp;
                    if (_speedCalculationLocation != null)
                    {
                        double distanceBetweenLocations = EDLocation.DistanceBetween(_speedCalculationLocation, edEvent.Location());
                        SpeedInMS = distanceBetweenLocations * (1000 / timeBetweenLocations.TotalMilliseconds);
                    }
                    _speedCalculationLocation = edEvent.Location();
                    if ((SpeedInMS - _lastSpeedInMs) > 20)
                    {
                        // If the speed increases by more than 20m/s in a short time (i.e. less than a second!), this is impossible and due to respawn
                        SpeedInMS = 0;
                        _speedCalculationLocation = null;
                    }
                }
                _lastSpeedInMs = SpeedInMS;

                CurrentLocation.Latitude = edEvent.Latitude;
                CurrentLocation.Longitude = edEvent.Longitude;
                CurrentLocation.Altitude = edEvent.Altitude;
                if (!String.IsNullOrEmpty(edEvent.BodyName))
                    CurrentLocation.PlanetName = edEvent.BodyName;
                if (edEvent.PlanetRadius > 0)
                    CurrentLocation.PlanetaryRadius = edEvent.PlanetRadius;

                CommanderLocationChanged?.Invoke(null, null);
                CurrentHeading = edEvent.Heading;
            }

            action = new Action(() => { labelLastUpdateTime.Text = DateTime.Now.ToString("HH:mm:ss"); });
            if (labelLastUpdateTime.InvokeRequired)
                labelLastUpdateTime.Invoke(action);
            else
                action();

            if (edEvent.HasCoordinates())
            {
                action = new Action(() => { textBoxLatitude.Text = edEvent.Latitude.ToString(); });
                if (textBoxLatitude.InvokeRequired)
                    textBoxLatitude.Invoke(action);
                else
                    action();

                action = new Action(() => { textBoxLongitude.Text = edEvent.Longitude.ToString(); });
                if (textBoxLongitude.InvokeRequired)
                    textBoxLongitude.Invoke(action);
                else
                    action();

                action = new Action(() => { textBoxAltitude.Text = edEvent.Altitude.ToString(); });
                if (textBoxAltitude.InvokeRequired)
                    textBoxAltitude.Invoke(action);
                else
                    action();
            }

            if (edEvent.PlanetRadius>0)
                if (!edEvent.PlanetRadius.ToString().Equals(textBoxPlanetRadius.Text))
                {
                    action = new Action(() => { textBoxPlanetRadius.Text = edEvent.PlanetRadius.ToString(); });
                    if (textBoxPlanetRadius.InvokeRequired)
                        textBoxPlanetRadius.Invoke(action);
                    else
                        action();
                }

            action = new Action(() => { textBoxHeading.Text = edEvent.Heading.ToString(); });
            if (textBoxHeading.InvokeRequired)
                textBoxHeading.Invoke(action);
            else
                action();
        }

        private void SaveToFile(EDEvent edEvent)
        {
            try
            {
                // This is very inefficient, save to file should only be enabled for debugging
                // I may revisit this at some point if more features are added for local tracking
                string eventData = eventData = edEvent.ToJson();
                System.IO.File.AppendAllText(textBoxSaveFile.Text, eventData);
            }
            catch (Exception ex)
            {
                AddLog($"Error saving to tracking log: {ex.Message}");
                checkBoxSaveToFile.Checked = false;
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
                    if (checkBoxShowLive.Checked)
                        AddLog($"Send {eventData}");
                }
                catch (Exception e)
                {
                    AddLog($"Failed to send UDP update: {e.Message}");
                }
            }
            catch (Exception ex)
            {
                AddLog($"Error uploading to server: {ex.Message}");
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
                    AddLog($"Error saving client Id to file: {ex.Message}");
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
                AddLog($"Error creating UDP client: {ex.Message}");
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
            textBoxSaveFile.Enabled = checkBoxSaveToFile.Checked;
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
                        AddLog($"Error initiating file watcher: {ex.Message}");
                        return;
                    }
                }
            }
            else
            {
                if (!_statusTimer.Enabled)
                {
                    _statusFile = $"{textBoxStatusFile.Text}\\Status.json";
                    if (File.Exists(_statusFile))
                    {
                        _statusTimer.Start();
                    }
                    else
                    {
                        AddLog($"Unable to find status file: {_statusFile}");
                        _statusFile = "";
                        checkBoxTrack.Checked = false;
                        return;
                    }
                }
            }
            if (!checkBoxTrack.Checked)
                checkBoxTrack.Checked = true;
            _journalReader.StartMonitoring();
            AddLog("Status tracking started");
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
            AddLog("Status tracking stopped");
        }

        private void checkBoxTrack_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTrack.Checked)
            {
                if (checkBoxUpload.Checked)
                {
                    if (String.IsNullOrEmpty(textBoxClientId.Text))
                        {
                            AddLog($"Client Id cannot be empty for server upload");
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
            if (_formRaceMonitor != null)
            {
                try
                {
                    _formRaceMonitor.Show();
                    _formRaceMonitor.Focus();
                    return;
                }
                catch { }
            }

            _formRaceMonitor = new FormRaceMonitor();
            _formRaceMonitor.Show();
        }

        private void checkBoxAutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            // Note that this gets called when the ConfigSaver restores values (if auto-update is enabled), so we check each time the form loads

            if (checkBoxAutoUpdate.Checked)
            {
                Action action = new Action(() =>
                {
                    Updater updater = new Updater();
                    if (updater.DownloadUpdate(checkBoxIncludeBetaUpdates.Checked))
                    {
                        Close();
                    }
                    if (updater.RunningVersionIsBeta)
                    {
                        Action updateBetaAction = new Action(() =>
                        {
                            checkBoxIncludeBetaUpdates.Checked = true;
                        });
                        if (checkBoxIncludeBetaUpdates.InvokeRequired)
                            checkBoxIncludeBetaUpdates.Invoke(updateBetaAction);
                        else
                            updateBetaAction();
                    }
                    updater.ClearUpdateFiles();
                });
                Task.Run(action);
            }
        }
    }
}
