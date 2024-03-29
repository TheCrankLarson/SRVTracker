﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EDTracking;
using System.Net;
using System.Text.Json;
using NAudio.Wave;

namespace Race_Manager
{
    public partial class FormRaceController : Form
    {
        private ConfigSaverClass _formConfig = null;
        private EDRace _race = null;
        private List<string> _skipAutoAdd = new List<string>();
        private string _saveFileName = "";
        private string _serverRaceGuid = "";
        private int _serverNotableEventsIndex = 0;
        private bool _updatingTargets = false;
        private string _targetCommander = "";
        private TelemetryWriter _raceTelemetryWriter = null;
        private TelemetryWriter _trackedTelemetryWriter = null;
        private FormTelemetryDisplay _raceTelemetryDisplay = null;
        private FormTelemetryDisplay _targetTelemetryDisplay = null;
        private FormTelemetrySettings _raceTelemetrySettings = null;
        private FormTelemetrySettings _targetTelemetrySettings = null;
        private FormTimer _raceTimer = null;
        private bool _showRaceDisplayOnSettingsClose = false;
        private bool _raceTelemetrySettingsClosing = false;
        private Dictionary<string, string> _activeServerRaces = null;

        private WaveOutEvent _audioOutputDevice = null;
        private AudioFileReader _audioFile;
        private bool _audioAnnounced = false;
        private bool _audioTest = false;
        private bool _falseStart = false;

        public FormRaceController()
        {
            InitializeComponent();

            // Attach our form configuration saver
            _formConfig = new ConfigSaverClass(this, true);
            _formConfig.ExcludedControls.Add(textBoxRaceName);
            _formConfig.ExcludedControls.Add(comboBoxAddCommander);
            _formConfig.ExcludedControls.Add(textBoxRouteName);
            _formConfig.ExcludedControls.Add(textBoxSystem);
            _formConfig.ExcludedControls.Add(textBoxPlanet);
            _formConfig.SaveEnabled = true;
            _formConfig.RestorePreviousSize = false;
            _formConfig.RestoreFormValues();
            groupBoxAddCommander.Visible = false;
            InitSoundOptions();

            this.Text = Application.ProductName + " v" + Application.ProductVersion;
            StartWatching();
            CommanderWatcher.UpdateReceived += CommanderWatcher_UpdateReceived;
            CommanderWatcher.OnlineCountChanged += CommanderWatcher_OnlineCountChanged;
            InitTelemetryWriters();
            UpdateUI();
            UpdateAudioUI();
            UpdateAvailableTargets();
            timerDownloadRaceTelemetry.Start();

            using (FormStatusMessages formStatusMessages = new FormStatusMessages())
            {
                EDRace.StatusMessages = formStatusMessages.StatusMessages();
            }
        }

        private void InitSoundOptions()
        {
            if (!String.IsNullOrEmpty((string)comboBoxAudioStartAnnouncement.Tag))
            {
                comboBoxAudioStartAnnouncement.Items.Insert(0, comboBoxAudioStartAnnouncement.Tag);
                comboBoxAudioStartAnnouncement.SelectedIndex = 0;
            }
            if (!String.IsNullOrEmpty((string)comboBoxAudioStartStart.Tag))
            {
                comboBoxAudioStartStart.Items.Insert(0, comboBoxAudioStartStart.Tag);
                comboBoxAudioStartStart.SelectedIndex = 0;
            }
        }

        private void StartWatching()
        {
            CommanderWatcher.Stop();
            if (!String.IsNullOrEmpty(ServerAddress()))
                CommanderWatcher.Start($"http://{ServerAddress()}:11938/DataCollator");
        }

        private void CommanderWatcher_OnlineCountChanged(object sender, EventArgs e)
        {
            UpdateAvailableTargets();
        }

        private void UpdateAvailableTargets()
        {
            Action action = new Action(() => {
                _updatingTargets = true;
                int selectedIndex = 0;
                string selectedTarget = "";
                if (comboBoxTarget.SelectedIndex > 0)
                {
                    selectedIndex = comboBoxTarget.SelectedIndex;
                    selectedTarget = (string)comboBoxTarget.SelectedItem;
                }

                comboBoxTarget.Items.Clear();
                comboBoxTarget.Items.Add("None");
                foreach (string commander in CommanderWatcher.GetCommanders())
                    comboBoxTarget.Items.Add(commander);

                if (selectedIndex > 0)
                {
                    if (!selectedTarget.Equals((string)comboBoxTarget.Items[selectedIndex]))
                    {
                        // Check if target still available
                        for (int i = 0; i < comboBoxTarget.Items.Count; i++)
                            if (selectedTarget.Equals((string)comboBoxTarget.Items[i]))
                            {
                                selectedIndex = i;
                                break;
                            }
                        if (!selectedTarget.Equals((string)comboBoxTarget.Items[selectedIndex]))
                            selectedIndex = 0; // Target has disappeared, so reset to none
                    }
                }
                comboBoxTarget.SelectedIndex = selectedIndex;
                _updatingTargets = false;
            });

            if (comboBoxTarget.InvokeRequired)
                comboBoxTarget.Invoke(action);
            else
                action();
        }

        private void CommanderWatcher_UpdateReceived(object sender, EDEvent edEvent)
        {
            // This is only used to track commanders before the race

            if (buttonStartRace.Enabled)
            {
                if (_race.Contestants.Contains(edEvent.Commander))
                    return; // Already tracking this commander
                if (checkBoxAutoAddCommanders.Checked && edEvent.HasCoordinates())
                    if (_race.Route.Waypoints.Count > 0)
                        if (!_skipAutoAdd.Contains(edEvent.Commander) && _race.Route.Waypoints[0].WaypointHit(edEvent.Location(), null))
                            AddTrackedCommander(edEvent.Commander);
            }
        }

        private string FindClosestTo(string Commander)
        {
            // Find the closest commander to this commander

            List<string> commanders;
            if (!buttonStartRace.Enabled)
            {
                // Race is started, so closest are limited to only race entrants
                commanders = _race.Contestants;
            }
            else
                commanders = CommanderWatcher.GetCommanders();

            double closestDistance = double.MaxValue;
            string closestCommander = "";
            EDEvent lastCommanderEvent = CommanderWatcher.GetCommanderMostRecentEvent(Commander);
            if (lastCommanderEvent == null)
                return "";

            EDLocation commanderLocation = lastCommanderEvent.Location();
            foreach (string commander in commanders)
                if (!commander.Equals(Commander))
                {
                    EDEvent lastcommanderEvent = CommanderWatcher.GetCommanderMostRecentEvent(commander);
                    if (lastcommanderEvent != null)
                    {
                        double distanceToCommander = EDLocation.DistanceBetween(commanderLocation, lastcommanderEvent.Location());
                        if (distanceToCommander<closestDistance)
                        {
                            closestDistance = distanceToCommander;
                            closestCommander = commander;
                        }
                    }
                }

            return closestCommander;
        }

        private DateTime _errorLastShown = DateTime.MinValue;
        private EDRaceStatus GetCommanderRaceStatus(string commander)
        {
            if (String.IsNullOrEmpty(commander))
                return null;

            if (!String.IsNullOrEmpty(_serverRaceGuid))
            {
                // We need to retrieve the status from the server
                string statusUrl = $"http://{ServerAddress()}:11938/DataCollator/getcommanderracestatus/{_serverRaceGuid}/{System.Web.HttpUtility.UrlEncode(commander)}";
                try
                {

                    using (WebClient webClient = new WebClient())
                    {
                        string raceStatus = webClient.DownloadString(statusUrl);
                        if (raceStatus.Length > 2)
                            return EDRaceStatus.FromJson(raceStatus);
                        throw new Exception($"Unexpected server response: {raceStatus}");
                    }
                }
                catch //(Exception ex)
                {
                    /*if (false)//DateTime.UtcNow.Subtract(_errorLastShown).TotalSeconds > 60)
                    {
                        MessageBox.Show($"Error retrieving tracked target:{Environment.NewLine}{ex.Message}{Environment.NewLine}{statusUrl}", "Tracking Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _errorLastShown = DateTime.UtcNow;
                    }*/
                }
            }
            else
            {
                if (DateTime.UtcNow.Subtract(_errorLastShown).TotalSeconds > 60)
                {
                    MessageBox.Show($"Race Guid not set - cannot query server", "Tracking Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _errorLastShown = DateTime.UtcNow;
                }
            }

            return null;
        }


        private void InitTelemetryWriters()
        {
            // We store our telemetry settings in the Tag of the Options button
            // Then it is saved automatically by our config saver class

            if (!String.IsNullOrEmpty((string)checkBoxExportRaceTelemetry.Tag))
            {
                _raceTelemetryWriter = new TelemetryWriter((string)checkBoxExportRaceTelemetry.Tag);
            }
            else
            {
                _raceTelemetryWriter = new TelemetryWriter();
                List<string> allReports = EDRace.RaceReportDescriptions.Keys.ToList<string>();
                foreach (string report in allReports)
                    _raceTelemetryWriter.EnableReportDisplay(report, report);
            }

            if (!String.IsNullOrEmpty((string)checkBoxExportTargetTelemetry.Tag))
            {
                _trackedTelemetryWriter = new TelemetryWriter((string)checkBoxExportTargetTelemetry.Tag);
            }
            else
            {
                _trackedTelemetryWriter = new TelemetryWriter();
                List<string> allReports = EDRaceStatus.RaceReportDescriptions.Keys.ToList<string>();
                foreach (string report in allReports)
                    _trackedTelemetryWriter.EnableReportDisplay(report, report);
            }
            checkBoxShowRaceTelemetry_CheckedChanged(null, null);
            checkBoxShowTargetTelemetry_CheckedChanged(null, null);
        }

        private void DisplayRoute()
        {
            // Update the route listview with our route
            textBoxRouteName.Text = _race.Route.Name;
            listBoxWaypoints.Items.Clear();
            if (_race.Route.Waypoints.Count < 1)
                return;

            listBoxWaypoints.BeginUpdate();
            foreach (EDWaypoint waypoint in _race.Route.Waypoints)
                listBoxWaypoints.Items.Add(waypoint.Name);
            listBoxWaypoints.EndUpdate();

            buttonStartRace.Enabled = true;
            textBoxPlanet.Text = _race.Route.Waypoints[0].Location.PlanetName;
            textBoxSystem.Text = _race.Route.Waypoints[0].Location.SystemName;
        }

        private void AddTrackedCommander(string commander, string status = "Ready", bool SkipAddContestant = false)
        {
            if (_race.Contestants.Contains(commander))
                return;

            _race.Contestants.Add(commander);
            Action action = new Action(() => { listBoxParticipants.Items.Add(commander); });
            if (listBoxParticipants.InvokeRequired)
                listBoxParticipants.Invoke(action);
            else
                action();
            if (_raceTelemetryDisplay != null && !_raceTelemetryDisplay.IsDisposed)
                _raceTelemetryDisplay.InitialiseColumns(EDRace.RaceReportDescriptions, _race.Contestants.Count);
            GeneratePreraceExports();
        }

        private void GeneratePreraceExports()
        {
            if (_race == null)
                return;

            Dictionary<string, string> raceStats = new Dictionary<string, string>();
            raceStats.Add("Commanders", String.Join(Environment.NewLine, _race.Contestants));
            raceStats.Add("RaceName", _race.Name);
            if (checkBoxExportRaceTelemetry.Checked)
                _raceTelemetryWriter?.ExportFiles(raceStats);
            _raceTelemetryDisplay?.UpdateRaceData(raceStats);
        }

        private string ServerAddress()
        {
            if (radioButtonUseDefaultServer.Checked)
                return (string)radioButtonUseDefaultServer.Tag;
            return textBoxUploadServer.Text;
        }

        private bool StartServerMonitoredRace()
        {
            // We send the race info to the server with StartRace command - this starts the monitoring on the server
            // We then just need to download results, and not keep track of status updates
          
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                    _race.CustomStatusMessages = EDRace.StatusMessages;
                    string response = wc.UploadString($"http://{ServerAddress()}:11938/DataCollator/startrace", JsonSerializer.Serialize(_race));
                    Guid raceGuid = Guid.Empty;
                    if (Guid.TryParse(response, out raceGuid))
                    {
                        _serverRaceGuid = response;
                        Action action = new Action(() =>
                        {
                            textBoxServerRaceGuid.Text = _serverRaceGuid;
                        });
                        if (textBoxServerRaceGuid.InvokeRequired)
                            textBoxServerRaceGuid.Invoke(action);
                        else
                            action();

                        action = new Action(() =>
                        {
                            textBoxRaceStatusServerUrl.Text = $"http://{ServerAddress()}:11938/DataCollator/getrace/{_serverRaceGuid}";
                        });
                        if (textBoxRaceStatusServerUrl.InvokeRequired)
                            textBoxRaceStatusServerUrl.Invoke(action);
                        else
                            action();
                    }
                    else
                    {
                        MessageBox.Show($"Unexpected server response:{Environment.NewLine}{response}", "Failed to start race", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                _serverNotableEventsIndex = 0;
                return true;
            }
            catch { }
            return false;
        }

        private void UpdateFromServerStats(Dictionary<string, string> serverStats)
        {
            if (_raceTelemetryDisplay != null && _raceTelemetryDisplay.Visible)
                _raceTelemetryDisplay.UpdateRaceData(serverStats);
            if (checkBoxExportRaceTelemetry.Checked)
                _raceTelemetryWriter?.ExportFiles(serverStats);
        }

        private void DisplayRaceSettings()
        {
            if (_race == null)
                return;

            checkBoxAllowSRV.Checked = _race.SRVAllowed;
            checkBoxAllowPitstops.Checked = _race.AllowPitstops;
            checkBoxAllowMainShip.Checked = _race.ShipAllowed;
            checkBoxAllowFighter.Checked = _race.FighterAllowed;
            checkBoxEliminationOnDestruction.Checked = _race.EliminateOnVehicleDestruction;
            checkBoxLappedRace.Checked = (_race.Laps > 1);
            if (_race.Laps>0)
                numericUpDownLapCount.Value = _race.Laps;
            checkBoxAnyWaypointOrder.Checked = !_race.WaypointsMustBeVisitedInOrder;
        }

        private void UpdateUI()
        {
            bool raceValid = (_race != null);
            bool raceStarted = _race?.Start > DateTime.MinValue;
            buttonAddParticipant.Enabled = raceValid;
            buttonRemoveParticipant.Enabled = raceValid && !raceStarted;
            buttonUneliminate.Enabled = true;// raceValid && raceStarted;
            buttonTrackParticipant.Enabled = listBoxParticipants.SelectedIndex > -1;
            checkBoxAutoAddCommanders.Enabled = raceValid && !raceStarted;
            buttonLoadRace.Enabled = !raceStarted;
            buttonSaveRaceAs.Enabled = raceValid;
            buttonSaveRace.Enabled = raceValid;
            buttonLoadRoute.Enabled = !raceStarted;
            if (listBoxParticipants.SelectedIndex>-1)
            {
                buttonTrackParticipant.Enabled = false;
                foreach (string availableTarget in comboBoxTarget.Items)
                    if (availableTarget.Equals((string)listBoxParticipants.SelectedItem))
                    {
                        buttonTrackParticipant.Enabled = true;
                        break;
                    }
            }
            numericUpDownLapCount.Enabled = checkBoxLappedRace.Checked;
            numericUpDownLapStartWaypoint.Enabled = checkBoxLapCustomWaypoints.Checked;
            numericUpDownLapEndWaypoint.Enabled = checkBoxLapCustomWaypoints.Checked;
            buttonRaceHistory.Enabled = !String.IsNullOrEmpty(_serverRaceGuid) || (_race != null && _race.Statuses != null && _race.Statuses.Count > 0);
        }

        private void Resurrect(string commander)
        {
            // Need to send resurrection request to server
            if (!String.IsNullOrEmpty(_serverRaceGuid))
                using (WebClient webClient = new WebClient())
                    _ = webClient.DownloadString($"http://{ServerAddress()}:11938/DataCollator/ResurrectCommander/{_serverRaceGuid}/{commander}");
        }

        private void UpdateAllowedVehicles()
        {
            bool allEnabled = checkBoxAllowAnyLocomotion.Checked;
            checkBoxAllowMainShip.Enabled = !allEnabled;
            checkBoxAllowSRV.Enabled = !allEnabled;
            checkBoxAllowFighter.Enabled = !allEnabled;

            if (_race == null)
                return;

            _race.SRVAllowed = checkBoxAllowSRV.Checked || allEnabled;
            checkBoxAllowPitstops.Enabled = _race.SRVAllowed;
            _race.FighterAllowed = checkBoxAllowFighter.Checked || allEnabled;
            _race.ShipAllowed = checkBoxAllowMainShip.Checked || allEnabled;
            checkBoxEliminationOnDestruction.Enabled = _race.SRVAllowed || _race.FighterAllowed || _race.ShipAllowed;
            _race.EliminateOnVehicleDestruction = checkBoxEliminationOnDestruction.Checked;
        }

        private void buttonLoadRace_Click(object sender, EventArgs e)
        {
            if (buttonStopRace.Enabled)
            {
                // Don't load a race while one is running
                if (MessageBox.Show(this, "Stop the currently running race?", "Race is running", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                buttonStopRace_Click(sender, null);
            }
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "edrace files (*.edrace)|*.edrace|Race.Start files|Race.Start|Race.Summary files|Race.Summary|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FileName = _saveFileName;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    EDRace race = EDRace.LoadFromFile(openFileDialog.FileName);
                    if (race == null)
                    {
                        MessageBox.Show("Invalid race file", "Load Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (buttonReset.Enabled)
                        buttonReset_Click(sender, null);

                    _race = race;
                    listBoxParticipants.Items.Clear();
                    if (_race.Contestants.Count > 0)
                    {
                        // We have contestants - this will happen if we load the race from a saved race folder
                        if (MessageBox.Show($"{_race.Contestants.Count} contestants listed in race file.{Environment.NewLine}{Environment.NewLine}Restore them?",
                            "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (string contestant in _race.Contestants)
                                listBoxParticipants.Items.Add(contestant);
                            if (_raceTelemetryDisplay != null && !_raceTelemetryDisplay.IsDisposed)
                                _raceTelemetryDisplay.InitialiseColumns(EDRace.RaceReportDescriptions, _race.Contestants.Count);
                        }
                        else
                            _race.Contestants = new List<string>();
                    }

                    _saveFileName = openFileDialog.FileName;
                    textBoxRaceName.Text = _race.Name;
                    DisplayRoute();
                    DisplayRaceSettings();
                    checkBoxAutoAddCommanders.Checked = true;
                    if (_race.Route != null & _race.Route.Waypoints != null)
                    {
                        numericUpDownLapStartWaypoint.Maximum = _race.Route.Waypoints.Count;
                        numericUpDownLapEndWaypoint.Maximum = _race.Route.Waypoints.Count;
                    }
                    checkBoxLapCustomWaypoints.Checked = _race.LapStartWaypoint > 0;
                    if (checkBoxLapCustomWaypoints.Checked)
                    {
                        numericUpDownLapStartWaypoint.Value = _race.LapStartWaypoint;
                        numericUpDownLapEndWaypoint.Value = _race.LapEndWaypoint;
                    }
                }
            }
            UpdateUI();
        }

        private void buttonStopRace_Click(object sender, EventArgs e)
        {
            if (_audioOutputDevice != null && _audioOutputDevice.PlaybackState != PlaybackState.Stopped)
            {
                // We want to stop any playing sound
                _falseStart = true;
                _audioOutputDevice.Stop();
            }

            if (!String.IsNullOrEmpty(_serverRaceGuid))
            {
                timerTrackTarget.Stop();
                // Send notification to server that race is finished
                try
                {
                    using (WebClient webClient = new WebClient())
                    {
                        string raceStatus = webClient.DownloadString($"http://{ServerAddress()}:11938/DataCollator/stoprace/{_serverRaceGuid}");
                    }
                }
                catch { }
            }

            buttonStopRace.Enabled = false;
            buttonReset.Enabled = true;
            //buttonUneliminate.Enabled = false;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            EDRaceStatus.Started = false;
            _race.Finished = false;
            _serverRaceGuid = "";
            textBoxServerRaceGuid.Text = "";
            _race.Statuses = new Dictionary<string, EDRaceStatus>();
            _race.Start = DateTime.MinValue;
            //_race.Contestants = new List<String>();
            //listBoxParticipants.Items.Clear();
            listBoxWaypoints.Refresh();
            buttonStartRace.Enabled = true;
            buttonReset.Enabled = false;
            buttonRaceHistory.Enabled = false;
            _raceTelemetryWriter.ClearFiles();
            _trackedTelemetryWriter.ClearFiles();
            _skipAutoAdd = new List<string>();
            if (_raceTelemetryDisplay != null && !_raceTelemetryDisplay.IsDisposed)
                _raceTelemetryDisplay.InitialiseColumns(EDRace.RaceReportDescriptions, _race.Contestants.Count);
            GeneratePreraceExports();
        }

        private void buttonStartRace_Click(object sender, EventArgs e)
        {
            if (_race.Route.Waypoints.Count < 2)
            {
                MessageBox.Show(this, "At least two waypoints are required to start a race", "Invalid Route", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_race.Contestants.Count < 1)
            {
                MessageBox.Show(this, "At least one commander is required to start a race", "No participants", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (checkBoxLappedRace.Checked)
            {
                _race.Laps = (int)numericUpDownLapCount.Value;
            }
            else
                _race.Laps = 0;

            _race.Finished = false;
            _falseStart = false;
            if (checkBoxEnableAudioStart.Checked)
            {
                _audioTest = false;
                PlayStartAudio();
            }
            else
                StartRace();

        }

        private void StartRace(bool AlreadyRunningOnServer = false)
        {
            if (!AlreadyRunningOnServer)
                if (!StartServerMonitoredRace())
                    return;

            if (_raceTimer != null && !_raceTimer.IsDisposed)
                _raceTimer.Play();
            buttonStartRace.Enabled = false;
            buttonStopRace.Enabled = true;
            buttonRemoveParticipant.Enabled = false;
            buttonRaceHistory.Enabled = true;
            checkBoxShowRaceTelemetry_CheckedChanged(null, null);
            timerTrackTarget.Start();
        }

        private void PlayStartAudio()
        {
            if (!_audioTest)
                buttonStopRace.Enabled = true;
            if (_audioOutputDevice==null)
            {
                _audioOutputDevice = new WaveOutEvent();
                _audioOutputDevice.PlaybackStopped += _audioOutputDevice_PlaybackStopped;
            }
            else
            {
                // Check device is clear, if not, reset it
                if (_audioOutputDevice.PlaybackState != PlaybackState.Stopped)
                    _audioOutputDevice.Stop();
            }
            if (!_audioTest)
                buttonStartRace.Enabled = false;

            _audioAnnounced = false;
            if (checkBoxAudioStartAnnouncement.Checked)
                PlayAudioFile(comboBoxAudioStartAnnouncement.Text);
            else
                _audioOutputDevice_PlaybackStopped(null, null);
        }

        private bool LoadAudioFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return false;

            if (_audioOutputDevice.PlaybackState != PlaybackState.Stopped)
                _audioOutputDevice.Stop();

            try
            {
                _audioFile = new AudioFileReader(filePath);
                _audioOutputDevice.Init(_audioFile);
                return true;
            }
            catch { }
            return false;
        }

        private void PlayAudioFile(string filePath)
        {
            if (!LoadAudioFile(filePath))
                return;
            _audioOutputDevice.Play();
        }

        private void _audioOutputDevice_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            // This event occurs once the audio announcement has completed
            if (_audioFile != null)
                _audioFile.Dispose();
            if (_audioAnnounced || _falseStart)
            {
                // We just need to dispose of the audio resources
                _audioOutputDevice.Dispose();
                _audioOutputDevice = null;
                buttonAudioTest.Enabled = true;
                return;
            }
            if (checkBoxAudioStartPause.Checked)
            {
                int delay = (int)numericUpDownAudioStartPause.Value;
                if (checkBoxAudioRandomiseStartPause.Checked)
                {
                    Random r = new Random();
                    delay = r.Next(0, (int)numericUpDownAudioStartPause.Value);
                }
                if (!_audioTest)
                    buttonStopRace.Enabled = false;
                System.Threading.Thread.Sleep(delay * 1000);
            }
            if (checkBoxAudioStartStart.Checked)
            {
                PlayAudioFile(comboBoxAudioStartStart.Text);
                _audioAnnounced = true;
            }
            if (!_audioTest)
                StartRace();
            _audioTest = false;
            buttonAudioTest.Enabled = true;
        }

        private string OpenSoundFile(string currentFile = "")
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = currentFile;
                openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|Wave files (*.wav)|*.wav|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FileName = currentFile;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    return openFileDialog.FileName;
                return "";
            }
        }

        private void buttonLoadRoute_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "edroute files (*.edroute)|*.edroute|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        EDRoute route = EDRoute.LoadFromFile(openFileDialog.FileName);
                        if (_race == null)
                        {
                            _race = new EDRace($"{route.Name} Race", route);
                            textBoxRaceName.Text = _race.Name;
                        }
                        else
                            _race.Route = route;
                        textBoxRouteName.Text = route.Name;
                        DisplayRoute();
                        UpdateUI();
                        numericUpDownLapStartWaypoint.Maximum = route.Waypoints.Count;
                        numericUpDownLapEndWaypoint.Maximum = route.Waypoints.Count;
                    }
                    catch { }
                }
            }
        }

        private void buttonSaveRaceAs_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxRaceName.Text))
            {
                textBoxRaceName.Focus();
                return;
            }

            _race.Name = textBoxRaceName.Text;
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = _saveFileName;
                saveFileDialog.Filter = "edrace files (*.edrace)|*.edrace|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = _saveFileName;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _race.SaveToFile(saveFileDialog.FileName);
                    }
                    catch { }
                }
            }
        }

        private void buttonSaveRace_Click(object sender, EventArgs e)
        {
            _race.Name = textBoxRaceName.Text;
            if (!String.IsNullOrEmpty(_saveFileName))
            {
                try
                {
                    _race.SaveToFile(_saveFileName);
                }
                catch { }
            }
        }

        Task _refreshFromServerTask = null;
        DateTime _refreshFromServerTaskStart = DateTime.MinValue;
        private void timerDownloadRaceTelemetry_Tick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_serverRaceGuid))
            {
                // No race running, so we just deal with pre-race exports
                GeneratePreraceExports();
                return;
            }

            if (_refreshFromServerTask != null)
            {
                // We haven't finished previous refresh.  If more than five seconds, we ignore this task and start a new one
                if (_refreshFromServerTask.IsFaulted || DateTime.UtcNow.Subtract(_refreshFromServerTaskStart).TotalSeconds > 5)
                {
                    try
                    {
                        _refreshFromServerTask.Dispose();
                    }
                    catch { }
                    _refreshFromServerTask = null;
                }
                else
                    return;
            }

            _refreshFromServerTask = Task.Run(new Action(() =>
            {
                string response = "";
                try
                {
                    using (WebClient webClient = new WebClient())
                        response = webClient.DownloadString($"http://{ServerAddress()}:11938/DataCollator/racestatus/{_serverRaceGuid}");
                }
                catch
                {
                    _refreshFromServerTask = null;
                    return;
                }

                try
                {
                    if (!String.IsNullOrEmpty(response))
                    {
                        Dictionary<string, string> raceStats = JsonSerializer.Deserialize<Dictionary<string, string>>(response);
                        UpdateFromServerStats(raceStats);
                    }
                }
                catch { }
                //ExportTrackingInfo();
                _refreshFromServerTask = null;
            }));
            _refreshFromServerTaskStart = DateTime.UtcNow;
        }

        private void buttonAddCommander_Click(object sender, EventArgs e)
        {
            groupBoxAddCommander.Visible = false;
            if (comboBoxAddCommander.SelectedIndex==0)
            {
                // Add all online commanders
                foreach (string commander in CommanderWatcher.GetCommanders())
                    AddTrackedCommander(commander,"");
            }
            else if (!String.IsNullOrEmpty(comboBoxAddCommander.Text))
                AddTrackedCommander(comboBoxAddCommander.Text, "");
            comboBoxAddCommander.Text = "";
        }

        private void buttonAddParticipant_Click(object sender, EventArgs e)
        {
            comboBoxAddCommander.Items.Clear();
            comboBoxAddCommander.Items.Add("Add all");
            foreach (string commander in CommanderWatcher.GetCommanders())
                comboBoxAddCommander.Items.Add(commander);
            if (comboBoxAddCommander.Items.Count == 1)
                comboBoxAddCommander.Items.Clear();
            groupBoxAddCommander.Visible = true;
            comboBoxAddCommander.Focus();
        }

        private void comboBoxAddCommander_Leave(object sender, EventArgs e)
        {
            if (this.ActiveControl != buttonAddCommander)
                groupBoxAddCommander.Visible = false;
        }

        private void comboBoxAddCommander_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonAddCommander_Click(null, null);
            comboBoxAddCommander.SelectedIndex = -1;
        }

        private void buttonRemoveParticipant_Click(object sender, EventArgs e)
        {
            if ( (listBoxParticipants.SelectedItems.Count != 1) || (_race.Start > DateTime.MinValue) || (!String.IsNullOrEmpty(_serverRaceGuid)) )
                return;

            string commanderToRemove = (string)listBoxParticipants.SelectedItem;
            if (_race.Contestants.Contains(commanderToRemove))
            {
                _race.Contestants.Remove(commanderToRemove);
                if (_raceTelemetryDisplay != null && !_raceTelemetryDisplay.IsDisposed)
                    _raceTelemetryDisplay.InitialiseColumns(EDRace.RaceReportDescriptions, _race.Contestants.Count);
            }
            listBoxParticipants.Items.RemoveAt(listBoxParticipants.SelectedIndex);
            _skipAutoAdd.Add(commanderToRemove);
        }

        private void listBoxParticipants_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {

        }

        private void buttonRaceTelemetryExportSettings_Click(object sender, EventArgs e)
        {
            if (_raceTelemetrySettings != null && _raceTelemetrySettings.IsDisposed)
                _raceTelemetrySettings = null;

            if (_raceTelemetrySettings == null)
            {
                _raceTelemetrySettings = new FormTelemetrySettings(_raceTelemetryWriter,EDRace.RaceReportDescriptions,"Race-", "Race Telemetry Settings");
                _raceTelemetrySettings.SelectedReportsChanged += _raceTelemetrySettings_SelectionChanged;
                _raceTelemetrySettings.ExportToControlTag(checkBoxExportRaceTelemetry);
                if (_raceTelemetryDisplay != null && !_raceTelemetryDisplay.IsDisposed)
                {
                    _raceTelemetryDisplay.Close();
                    checkBoxShowRaceTelemetry.Checked = false;
                    _showRaceDisplayOnSettingsClose = true;
                    _raceTelemetrySettings.FormClosed += _raceTelemetrySettings_FormClosed;
                }
            }
            if (!_raceTelemetrySettings.Visible)
                _raceTelemetrySettings.Show(this);
        }

        private void _raceTelemetrySettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_showRaceDisplayOnSettingsClose)
            {
                _showRaceDisplayOnSettingsClose = false;
                _raceTelemetrySettingsClosing = true;
                checkBoxShowRaceTelemetry.Checked = true;
            }
        }

        private void _raceTelemetrySettings_SelectionChanged(object sender, EventArgs e)
        {
            // We cannot update columns once first initialised, for some reason
            // So as a hack we'll just close and reopen the form
            if (_raceTelemetryDisplay!= null && !_raceTelemetryDisplay.IsDisposed)
            {
                _raceTelemetryDisplay.Close();
                checkBoxShowRaceTelemetry.Checked = false;
                checkBoxShowRaceTelemetry.Checked = true;
            }
        }

        private void buttonCommanderTelemetryExportSettings_Click(object sender, EventArgs e)
        {
            if (_targetTelemetrySettings != null && _targetTelemetrySettings.IsDisposed)
                _targetTelemetrySettings = null;

            if (_targetTelemetrySettings == null)
            {
                _targetTelemetrySettings = new FormTelemetrySettings(_trackedTelemetryWriter, EDRaceStatus.RaceReportDescriptions, "Target-", "Target Telemetry Settings");
                _targetTelemetrySettings.ExportToControlTag(checkBoxExportTargetTelemetry);
            }
            if (!_targetTelemetrySettings.Visible)
                _targetTelemetrySettings.Show(this);
        }

        private void checkBoxShowRaceTelemetry_CheckedChanged(object sender, EventArgs e)
        {
            if (_raceTelemetryWriter == null)
                return;

            if (checkBoxShowRaceTelemetry.Checked)
            {
                if (_raceTelemetrySettings != null && !_raceTelemetrySettings.IsDisposed)
                {
                    // We close the display form while editing these settings, as it doesn't update properly
                    if (!_raceTelemetrySettingsClosing)
                    {
                        checkBoxShowRaceTelemetry.Checked = false;
                        return;
                    }
                    _raceTelemetrySettingsClosing = false;
                }
                if (_raceTelemetryDisplay == null || _raceTelemetryDisplay.IsDisposed)
                {
                    _raceTelemetryDisplay = new FormTelemetryDisplay(_raceTelemetryWriter);
                    _raceTelemetryDisplay.FormClosing += _raceTelemetryDisplay_FormClosing;
                    _raceTelemetryDisplay.Show(this);
                    int rows = 0;
                    if (_race != null)
                        rows = _race.Contestants.Count;
                    _raceTelemetryDisplay.InitialiseColumns(EDRace.RaceReportDescriptions, rows);
                }
                else if (!_raceTelemetryDisplay.Visible)
                    _raceTelemetryDisplay.Show(this);
                else
                    _raceTelemetryDisplay.Focus();
                return;
            }
            if (_raceTelemetryDisplay != null && _raceTelemetryDisplay.Visible)
                _raceTelemetryDisplay.Hide();
        }

        private void _raceTelemetryDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                checkBoxShowRaceTelemetry.Checked = false;
        }

        private void checkBoxShowTargetTelemetry_CheckedChanged(object sender, EventArgs e)
        {
            if (_trackedTelemetryWriter == null)
                return;

            if (checkBoxShowTargetTelemetry.Checked)
            {
                if (_targetTelemetryDisplay == null || _targetTelemetryDisplay.IsDisposed)
                {
                    _targetTelemetryDisplay = new FormTelemetryDisplay(_trackedTelemetryWriter, "Commander Telemetry");
                    _targetTelemetryDisplay.FormClosing += _targetTelemetryDisplay_FormClosing;
                    _targetTelemetryDisplay.InitialiseRows(EDRaceStatus.RaceReportDescriptions);
                    _targetTelemetryDisplay.Show(this);
                }
                else if (!_targetTelemetryDisplay.Visible)
                    _targetTelemetryDisplay.Show(this);
                else
                    _targetTelemetryDisplay.Focus();
                return;
            }
            if (_targetTelemetryDisplay != null && _targetTelemetryDisplay.Visible)
                _targetTelemetryDisplay.Hide();
        }

        private void _targetTelemetryDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                checkBoxShowTargetTelemetry.Checked = false;
        }

        private void buttonUneliminate_Click(object sender, EventArgs e)
        {
            if (listBoxParticipants.SelectedIndex < 0)
                return;

            Resurrect((string)listBoxParticipants.SelectedItem);
        }

        private void buttonTrackParticipant_Click(object sender, EventArgs e)
        {
            if (listBoxParticipants.SelectedIndex < 0)
                return;

            for (int i=0; i<comboBoxTarget.Items.Count; i++)
                if (((string)listBoxParticipants.SelectedItem).Equals((string)comboBoxTarget.Items[i]))
                {
                    comboBoxTarget.SelectedIndex = i;
                    break;
                }
        }

        private void radioButtonUseCustomServer_CheckedChanged(object sender, EventArgs e)
        {
            textBoxUploadServer.Enabled = radioButtonUseCustomServer.Checked;
            StartWatching();
        }

        private void radioButtonUseDefaultServer_CheckedChanged(object sender, EventArgs e)
        {
            textBoxUploadServer.Enabled = radioButtonUseCustomServer.Checked;
            StartWatching();
        }

        private void checkBoxAllowSRV_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAllowedVehicles();
        }

        private void checkBoxAllowFighter_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAllowedVehicles();
        }

        private void checkBoxAllowMainShip_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAllowedVehicles();
        }

        private void checkBoxLappedRace_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLappedRace.Checked)
            {
                numericUpDownLapCount.Enabled = true;
                if (_race != null)
                    _race.Laps = (int)numericUpDownLapCount.Value;
            }
            else
            {
                numericUpDownLapCount.Enabled = false;
                if (_race != null)
                    _race.Laps = 0;
            }
        }

        private void buttonEditStatusMessages_Click(object sender, EventArgs e)
        {
            using (FormStatusMessages formStatusMessages = new FormStatusMessages())
            {
                if (formStatusMessages.ShowDialog(this) == DialogResult.OK)
                    EDRace.StatusMessages = formStatusMessages.StatusMessages();
            }
        }

        private void checkBoxCustomStatusMessages_CheckedChanged(object sender, EventArgs e)
        {
            buttonEditStatusMessages.Enabled = checkBoxCustomStatusMessages.Checked;
        }

        private void comboBoxTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_updatingTargets)
                return;

            if (comboBoxTarget.SelectedIndex == 0)
            {
                // Select none, so clear exports and display
                _targetTelemetryDisplay?.UpdateTargetData(null);
                _trackedTelemetryWriter?.ClearFiles();
                return;
            }
        }

        private void timerTrackTarget_Tick(object sender, EventArgs e)
        {
            if ((!checkBoxShowTargetTelemetry.Checked && !checkBoxExportTargetTelemetry.Checked) || comboBoxTarget.SelectedIndex<1)
                return;

            _targetCommander = (string)comboBoxTarget.SelectedItem;
            if (checkBoxTargetClosestTo.Checked)
                _targetCommander = FindClosestTo(_targetCommander);

            Action action = new Action(() =>
            {                
                EDRaceStatus commanderStatus = GetCommanderRaceStatus(_targetCommander);
                if (commanderStatus != null)
                {
                    Dictionary<string, string> commanderTelemetry = commanderStatus.Telemetry();
                    _targetTelemetryDisplay?.UpdateTargetData(commanderTelemetry);
                    if (checkBoxExportTargetTelemetry.Checked)
                        _trackedTelemetryWriter?.ExportFiles(commanderTelemetry);
                }
            });
            Task.Run(action);
        }

        private void checkBoxLapCustomWaypoints_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
            if (_race == null)
                return;

            if (checkBoxLapCustomWaypoints.Checked)
            {
                _race.LapStartWaypoint = (int)numericUpDownLapStartWaypoint.Value;
                _race.LapEndWaypoint = (int)numericUpDownLapEndWaypoint.Value;
            }
            else
                _race.LapStartWaypoint = 0;
        }

        private void numericUpDownLapStartWaypoint_ValueChanged(object sender, EventArgs e)
        {
            if (_race == null || !checkBoxLapCustomWaypoints.Checked)
                return;

            if (_race.LapStartWaypoint != (int)numericUpDownLapStartWaypoint.Value)
            {
                if ((int)numericUpDownLapStartWaypoint.Value > listBoxWaypoints.Items.Count)
                {
                    numericUpDownLapStartWaypoint.Value = listBoxWaypoints.Items.Count;
                    numericUpDownLapStartWaypoint.Maximum = listBoxWaypoints.Items.Count;
                }
                _race.LapStartWaypoint = (int)numericUpDownLapStartWaypoint.Value;
                listBoxWaypoints.SelectedIndex = _race.LapStartWaypoint - 1;
            }
        }

        private void numericUpDownLapEndWaypoint_ValueChanged(object sender, EventArgs e)
        {
            if (_race == null || !checkBoxLapCustomWaypoints.Checked)
                return;

            if (_race.LapEndWaypoint != (int)numericUpDownLapEndWaypoint.Value)
            {
                if ((int)numericUpDownLapEndWaypoint.Value > listBoxWaypoints.Items.Count)
                {
                    numericUpDownLapEndWaypoint.Value = listBoxWaypoints.Items.Count;
                    numericUpDownLapEndWaypoint.Maximum = listBoxWaypoints.Items.Count;
                }
                _race.LapEndWaypoint = (int)numericUpDownLapEndWaypoint.Value;
                listBoxWaypoints.SelectedIndex = _race.LapEndWaypoint - 1;
            }
        }

        private void numericUpDownLapStartWaypoint_Enter(object sender, EventArgs e)
        {
            if (_race == null)
                return;

            if ((int)numericUpDownLapStartWaypoint.Value <= listBoxWaypoints.Items.Count)
                listBoxWaypoints.SelectedIndex = _race.LapStartWaypoint - 1;
        }

        private void numericUpDownLapEndWaypoint_Enter(object sender, EventArgs e)
        {
            if (_race == null)
                return;

            if ((int)numericUpDownLapEndWaypoint.Value <= listBoxWaypoints.Items.Count)
                listBoxWaypoints.SelectedIndex = _race.LapEndWaypoint - 1;
        }

        private void numericUpDownLapCount_ValueChanged(object sender, EventArgs e)
        {
            if (_race == null)
                return;

            if ((int)numericUpDownLapCount.Value != _race.Laps)
                _race.Laps = (int)numericUpDownLapCount.Value;
        }

        private void checkBoxStartRaceTimerAtFirstWaypoint_CheckedChanged(object sender, EventArgs e)
        {
            if (_race == null)
                return;

            if (_race.StartTimeFromFirstWaypoint != checkBoxStartRaceTimerAtFirstWaypoint.Checked)
                _race.StartTimeFromFirstWaypoint = checkBoxStartRaceTimerAtFirstWaypoint.Checked;
        }

        private void GetActiveServerRaces()
        {
            // Retrieve list of races on server and show them in the list
            string response = "";
            comboBoxConnectToRace.Items.Clear();

            _activeServerRaces = new Dictionary<string, string>();
            try
            {
                using (WebClient webClient = new WebClient())
                    response = webClient.DownloadString($"http://{ServerAddress()}:11938/DataCollator/getactiveraces");
            }
            catch { }

            if (String.IsNullOrEmpty(response))
                return;

            comboBoxConnectToRace.Items.Add("Select race:");
            // Races are returned in format guid,Name
            using (System.IO.StringReader reader = new System.IO.StringReader(response))
            {
                string activeRace = reader.ReadLine();
                int commaPos = activeRace.IndexOf(',');
                if (commaPos > 0)
                {
                    string activeRaceGuid = activeRace.Substring(0, commaPos);
                    string activeRaceName = activeRace.Substring(commaPos + 1);
                    _activeServerRaces.Add(activeRaceName, activeRaceGuid);
                    comboBoxConnectToRace.Items.Add(activeRaceName);
                }
            }
            if (_activeServerRaces.Count > 0)
                comboBoxConnectToRace.Visible = true;

        }

        private void buttonConnectToRace_Click(object sender, EventArgs e)
        {
            if (comboBoxConnectToRace.Visible)
            {
                comboBoxConnectToRace.Visible = false;
                return;
            }

            GetActiveServerRaces();
          }

        private void comboBoxConnectToRace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxConnectToRace.SelectedIndex < 1 || !_activeServerRaces.ContainsKey((string)comboBoxConnectToRace.SelectedItem))
                return;

            string selectedRaceGuid = _activeServerRaces[(string)comboBoxConnectToRace.SelectedItem];
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    string raceData = webClient.DownloadString($"http://{ServerAddress()}:11938/DataCollator/getrace/{selectedRaceGuid}");
                    _race = EDRace.FromString(raceData);
                    _serverRaceGuid = selectedRaceGuid;
                    DisplayRoute();
                    DisplayRaceSettings();
                    listBoxParticipants.Items.Clear();
                    foreach (string contestant in _race.Contestants)
                        listBoxParticipants.Items.Add(contestant);
                    UpdateUI();

                    // The Race Telemetry form doesn't update unless we reset it
                    bool raceTelemetryEnabled = checkBoxShowRaceTelemetry.Checked;
                    if (raceTelemetryEnabled)
                    {
                        checkBoxShowRaceTelemetry.Checked = false;
                        _raceTelemetryDisplay?.Close();
                        _raceTelemetryDisplay = null;
                    }
                    StartRace(true);
                    if (raceTelemetryEnabled)
                        checkBoxShowRaceTelemetry.Checked = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
            comboBoxConnectToRace.Visible = false;
        }

        private void comboBoxConnectToRace_Leave(object sender, EventArgs e)
        {
            comboBoxConnectToRace.Visible = false;
        }

        private void checkBoxShowRaceTimer_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowRaceTimer.Checked)
            {
                _raceTimer = new FormTimer();
                _raceTimer.Show();
            }
            else if (_raceTimer != null)
            {
                if (!_raceTimer.IsDisposed)
                    _raceTimer.Close();
                _raceTimer = null;
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (_raceTimer != null && !_raceTimer.IsDisposed)
                _raceTimer.Pause();
        }

        private void buttonRaceHistory_Click(object sender, EventArgs e)
        {
            if (_race.Statuses == null || _race.Statuses.Count < 1)
            {
                buttonRaceHistory.Enabled = false;
                return;
            }

            FormRaceHistory formRaceHistory = null;
            if (!String.IsNullOrEmpty(_serverRaceGuid))
            {
                // Race running on server, so connect to that
                formRaceHistory = new FormRaceHistory(_race.Contestants, _serverRaceGuid);
            }
            else
            {
                // Looking at history from local race data (e.g. past race that has been loaded)
                formRaceHistory = new FormRaceHistory(_race);
            }
            formRaceHistory.Show(this);
        }

        private void comboBoxAudioStartAnnouncement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAudioStartAnnouncement.SelectedIndex < 0)
                return;

            string selectedSound = (string)comboBoxAudioStartAnnouncement.SelectedItem;
            if (selectedSound.Equals("Load audio file...") && this.Visible)
            {
                // Add custom sound
                string currentFile = "";
                selectedSound = OpenSoundFile(currentFile);
                if (!String.IsNullOrEmpty(selectedSound))
                {
                    comboBoxAudioStartAnnouncement.Items.Insert(0, selectedSound);
                    comboBoxAudioStartAnnouncement.Tag = selectedSound;
                    comboBoxAudioStartAnnouncement.SelectedIndex = 0;
                }
                else
                    comboBoxAudioStartAnnouncement.SelectedIndex = -1;
            }
        }

        private void comboBoxAudioStartStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAudioStartStart.SelectedIndex < 0)
                return;

            string selectedSound = (string)comboBoxAudioStartStart.SelectedItem;
            if (selectedSound.Equals("Load audio file...") && this.Visible)
            {
                // Add custom sound
                string currentFile = "";
                selectedSound = OpenSoundFile(currentFile);
                if (!String.IsNullOrEmpty(selectedSound))
                {
                    comboBoxAudioStartStart.Items.Insert(0, selectedSound);
                    comboBoxAudioStartStart.Tag = selectedSound;
                    comboBoxAudioStartStart.SelectedIndex = 0;
                }
                else
                    comboBoxAudioStartStart.SelectedIndex = -1;
            }
        }

        private void buttonAudioTest_Click(object sender, EventArgs e)
        {
            _audioTest = true;
            buttonAudioTest.Enabled = false;
            PlayStartAudio();
        }

        private void UpdateAudioUI()
        {
            buttonAudioTest.Enabled = checkBoxEnableAudioStart.Checked;
            checkBoxAudioStartAnnouncement.Enabled = checkBoxEnableAudioStart.Checked;
            checkBoxAudioStartPause.Enabled = checkBoxEnableAudioStart.Checked;
            checkBoxAudioStartStart.Enabled = checkBoxEnableAudioStart.Checked;
            comboBoxAudioStartAnnouncement.Enabled = checkBoxAudioStartAnnouncement.Enabled && checkBoxAudioStartAnnouncement.Checked;
            numericUpDownAudioStartPause.Enabled = checkBoxAudioStartPause.Enabled && checkBoxAudioStartPause.Checked;
            checkBoxAudioRandomiseStartPause.Enabled = checkBoxAudioStartPause.Enabled && checkBoxAudioStartPause.Checked;
            comboBoxAudioStartStart.Enabled = checkBoxAudioStartStart.Enabled && checkBoxAudioStartStart.Checked;
        }

        private void checkBoxEnableAudioStart_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAudioUI();   
        }

        private void checkBoxAudioStartAnnouncement_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAudioUI();
        }

        private void checkBoxAudioStartPause_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAudioUI();
        }

        private void checkBoxAudioStartStart_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAudioUI();
        }


        private void checkBoxAllowAnyLocomotion_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAllowedVehicles();
        }

        private void checkBoxAnyWaypointOrder_CheckedChanged(object sender, EventArgs e)
        {
            if (_race == null)
                return;
            _race.WaypointsMustBeVisitedInOrder = !checkBoxAnyWaypointOrder.Checked;
        }
    }
}
