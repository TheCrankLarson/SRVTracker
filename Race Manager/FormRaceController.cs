using System;
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
        private bool _showRaceDisplayOnSettingsClose = false;
        private bool _raceTelemetrySettingsClosing = false;

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

            this.Text = Application.ProductName + " v" + Application.ProductVersion;
            StartWatching();
            CommanderWatcher.UpdateReceived += CommanderWatcher_UpdateReceived;
            CommanderWatcher.OnlineCountChanged += CommanderWatcher_OnlineCountChanged;
            InitTelemetryWriters();
            UpdateUI();
            UpdateAvailableTargets();
            timerDownloadRaceTelemetry.Start();
        }

        private void StartWatching()
        {
            CommanderWatcher.Stop();
            if (!String.IsNullOrEmpty(ServerAddress()))
                CommanderWatcher.Start($"http://{ServerAddress()}:11938/DataCollator/status");
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
                    /*if (false)//DateTime.Now.Subtract(_errorLastShown).TotalSeconds > 60)
                    {
                        MessageBox.Show($"Error retrieving tracked target:{Environment.NewLine}{ex.Message}{Environment.NewLine}{statusUrl}", "Tracking Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _errorLastShown = DateTime.Now;
                    }*/
                }
            }
            else
            {
                if (DateTime.Now.Subtract(_errorLastShown).TotalSeconds > 60)
                {
                    MessageBox.Show($"Race Guid not set - cannot query server", "Tracking Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _errorLastShown = DateTime.Now;
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
                _raceTelemetryWriter = new TelemetryWriter();

            if (!String.IsNullOrEmpty((string)checkBoxExportTargetTelemetry.Tag))
            {
                _trackedTelemetryWriter = new TelemetryWriter((string)checkBoxExportTargetTelemetry.Tag);
            }
            else
                _trackedTelemetryWriter = new TelemetryWriter();
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
                _raceTelemetryDisplay.InitialiseColumns(EDRace.RaceReportDescriptions(), _race.Contestants.Count);
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

            _race.CustomStatusMessages = EDRace.StatusMessages;
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
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

                    _serverNotableEventsIndex = 0;
                    timerTrackTarget.Start();
                }
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
        }

        private void UpdateUI()
        {
            bool raceValid = (_race != null);
            bool raceStarted = _race?.Start > DateTime.MinValue;
            buttonAddParticipant.Enabled = raceValid;
            buttonRemoveParticipant.Enabled = raceValid;
            buttonUneliminate.Enabled = raceValid && raceStarted;
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
            if (_race == null)
                return;
            _race.SRVAllowed = checkBoxAllowSRV.Checked;
            checkBoxAllowPitstops.Enabled = _race.SRVAllowed;
            _race.FighterAllowed = checkBoxAllowFighter.Checked;
            _race.ShipAllowed = checkBoxAllowMainShip.Checked;
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
                openFileDialog.Filter = "edrace files (*.edrace)|*.edrace|Race.Start files|Race.Start|All files (*.*)|*.*";
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
                                _raceTelemetryDisplay.InitialiseColumns(EDRace.RaceReportDescriptions(), _race.Contestants.Count);
                        }
                    }

                    _saveFileName = openFileDialog.FileName;
                    textBoxRaceName.Text = _race.Name;
                    DisplayRoute();
                    DisplayRaceSettings();
                    checkBoxAutoAddCommanders.Checked = true;
                }
            }
            UpdateUI();
        }

        private void buttonStopRace_Click(object sender, EventArgs e)
        {
            //_race.Finished = true;
            //if (_race.Statuses != null)
            //    foreach (EDRaceStatus status in _race.Statuses.Values)
            //        status.Finished = true;

            if (!String.IsNullOrEmpty(_serverRaceGuid))
            {
                timerDownloadRaceTelemetry.Stop();
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
            buttonUneliminate.Enabled = false;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            EDRaceStatus.Started = false;
            _race.Finished = false;
            _serverRaceGuid = "";
            textBoxServerRaceGuid.Text = "";
            _race.Statuses = null;
            _race.Start = DateTime.MinValue;
            listBoxWaypoints.Refresh();
            buttonStartRace.Enabled = true;
            buttonReset.Enabled = false;
            buttonRaceHistory.Enabled = false;
            _raceTelemetryWriter.ClearFiles();
            _trackedTelemetryWriter.ClearFiles();
            _skipAutoAdd = new List<string>();
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

            if (!StartServerMonitoredRace())
                return;

            buttonStartRace.Enabled = false;
            buttonStopRace.Enabled = true;
            buttonRemoveParticipant.Enabled = false;
            buttonRaceHistory.Enabled = true;
            checkBoxShowRaceTelemetry_CheckedChanged(null, null);
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
                if (_refreshFromServerTask.IsFaulted || DateTime.Now.Subtract(_refreshFromServerTaskStart).TotalSeconds > 5)
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

                Dictionary<string, string> raceStats = JsonSerializer.Deserialize<Dictionary<string, string>>(response);
                UpdateFromServerStats(raceStats);
                //ExportTrackingInfo();
                _refreshFromServerTask = null;
            }));
            _refreshFromServerTaskStart = DateTime.Now;
        }

        private void buttonAddCommander_Click(object sender, EventArgs e)
        {
            groupBoxAddCommander.Visible = false;
            if (!String.IsNullOrEmpty(comboBoxAddCommander.Text))
                AddTrackedCommander(comboBoxAddCommander.Text, "");
            comboBoxAddCommander.Text = "";
        }

        private void buttonAddParticipant_Click(object sender, EventArgs e)
        {
            comboBoxAddCommander.Items.Clear();
            foreach (string commander in CommanderWatcher.GetCommanders())
                comboBoxAddCommander.Items.Add(commander);
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
            if ((listBoxParticipants.SelectedItems.Count != 1) || (_race.Start > DateTime.MinValue))
                return;

            string commanderToRemove = (string)listBoxParticipants.SelectedItem;
            if (_race.Contestants.Contains(commanderToRemove))
            {
                _race.Contestants.Remove(commanderToRemove);
                if (_raceTelemetryDisplay != null && !_raceTelemetryDisplay.IsDisposed)
                    _raceTelemetryDisplay.InitialiseColumns(EDRace.RaceReportDescriptions(), _race.Contestants.Count);
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
                _raceTelemetrySettings = new FormTelemetrySettings(_raceTelemetryWriter,EDRace.RaceReportDescriptions(),"Race-", "Race Telemetry Settings");
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
                _targetTelemetrySettings = new FormTelemetrySettings(_trackedTelemetryWriter, EDRaceStatus.RaceReportDescriptions(), "Target-", "Target Telemetry Settings");
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
                    _raceTelemetryDisplay.InitialiseColumns(EDRace.RaceReportDescriptions(), rows);
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
                    _targetTelemetryDisplay.InitialiseRows(EDRaceStatus.RaceReportDescriptions());
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
                _targetTelemetryDisplay.UpdateTargetData(null);
                _trackedTelemetryWriter.ClearFiles();
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
    }
}
