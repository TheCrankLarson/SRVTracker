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
        private TelemetryWriter _raceTelemetryWriter = null;
        private TelemetryWriter _trackedTelemetryWriter = null;
        private FormTelemetryDisplay _raceTelemetryDisplay = null;
        private FormTelemetryDisplay _targetTelemetryDisplay = null;
        private FormFileExportSettings _raceTelemetryExportSettings = null;
        private FormFileExportSettings _targetTelemetryExportSettings = null;

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
            ConfigSaverClass.ApplyConfiguration();
            groupBoxAddCommander.Visible = false;

            this.Text = Application.ProductName + " v" + Application.ProductVersion;
            CommanderWatcher.UpdateReceived += CommanderWatcher_UpdateReceived;
            if (!String.IsNullOrEmpty(ServerAddress()))
                CommanderWatcher.Start($"http://{ServerAddress()}:11938/DataCollator/status");
            InitTelemetryWriters();
            UpdateUI();
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
                        if (!_skipAutoAdd.Contains(edEvent.Commander) && _race.Route.Waypoints[0].LocationIsWithinWaypoint(edEvent.Location()))
                            AddTrackedCommander(edEvent.Commander);
            }
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
                    timerDownloadRaceTelemetry.Start();
                }
                return true;
            }
            catch { }
            return false;
        }

        private void UpdateFromServerStats(Dictionary<string, string> serverStats)
        {
            if (_raceTelemetryDisplay != null && _raceTelemetryDisplay.Visible)
                _raceTelemetryDisplay.UpdateData(serverStats);
            if (_raceTelemetryWriter != null)
                _raceTelemetryWriter.ExportFiles(serverStats);
        }

        private void UpdateUI()
        {
            bool raceValid = (_race != null);
            bool raceStarted = _race?.Start > DateTime.MinValue;
            buttonAddParticipant.Enabled = raceValid;
            buttonRemoveParticipant.Enabled = raceValid;
            buttonUneliminate.Enabled = raceValid && raceStarted;
            buttonTrackParticipant.Enabled = listBoxParticipants.SelectedIndex > -1;
            checkBoxAutoAddCommanders.Enabled = raceValid && raceStarted;
            buttonLoadRace.Enabled = !raceStarted;
            buttonSaveRaceAs.Enabled = raceValid;
            buttonSaveRace.Enabled = raceValid;
            buttonLoadRoute.Enabled = !raceStarted;
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
                openFileDialog.Filter = "edrace files (*.edrace)|*.edrace|All files (*.*)|*.*";
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
                    _saveFileName = openFileDialog.FileName;
                    textBoxRaceName.Text = _race.Name;
                    DisplayRoute();
                    checkBoxAutoAddCommanders.Checked = true;
                }
            }
            UpdateUI();
        }

        private void buttonStopRace_Click(object sender, EventArgs e)
        {

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {

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
                return;

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
                _race.Contestants.Remove(commanderToRemove);
            listBoxParticipants.Items.RemoveAt(listBoxParticipants.SelectedIndex);
            _skipAutoAdd.Add(commanderToRemove);
        }

        private void listBoxParticipants_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            FormTelemetryDisplay formTelemetry = new FormTelemetryDisplay(_raceTelemetryWriter);
            formTelemetry.InitialiseColumns(EDRace.RaceReportDescriptions());
            formTelemetry.Show();
        }

        private void buttonRaceTelemetryExportSettings_Click(object sender, EventArgs e)
        {
            if (_raceTelemetryExportSettings != null && _raceTelemetryExportSettings.IsDisposed)
                _raceTelemetryExportSettings = null;

            if (_raceTelemetryExportSettings == null)
            {
                _raceTelemetryExportSettings = new FormFileExportSettings(_raceTelemetryWriter,EDRace.RaceReportDescriptions(),"Race-", "Race Telemetry Settings");
                _raceTelemetryExportSettings.ExportToControlTag(checkBoxExportRaceTelemetry);
            }
            _raceTelemetryExportSettings.Show(this);
        }

        private void buttonCommanderTelemetryExportSettings_Click(object sender, EventArgs e)
        {
            if (_targetTelemetryExportSettings != null && _targetTelemetryExportSettings.IsDisposed)
                _targetTelemetryExportSettings = null;

            if (_targetTelemetryExportSettings == null)
            {
                _targetTelemetryExportSettings = new FormFileExportSettings(_trackedTelemetryWriter, EDRaceStatus.RaceReportDescriptions(), "Target-", "Target Telemetry Settings");
                _targetTelemetryExportSettings.ExportToControlTag(checkBoxExportTargetTelemetry);
            }
            _targetTelemetryExportSettings.Show(this);
        }

        private void checkBoxShowRaceTelemetry_CheckedChanged(object sender, EventArgs e)
        {
            if (_raceTelemetryWriter == null)
                return;

            if (checkBoxShowRaceTelemetry.Checked)
            {
                if (_raceTelemetryDisplay == null)
                {
                    _raceTelemetryDisplay = new FormTelemetryDisplay(_raceTelemetryWriter);
                    _raceTelemetryDisplay.InitialiseColumns(EDRace.RaceReportDescriptions());
                    _raceTelemetryDisplay.Show(this);
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

        private void checkBoxShowTargetTelemetry_CheckedChanged(object sender, EventArgs e)
        {
            if (_trackedTelemetryWriter == null)
                return;

            if (checkBoxShowTargetTelemetry.Checked)
            {
                if (_targetTelemetryDisplay == null)
                {
                    _targetTelemetryDisplay = new FormTelemetryDisplay(_trackedTelemetryWriter);
                    _targetTelemetryDisplay.InitialiseColumns(EDRaceStatus.RaceReportDescriptions());
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


    }
}
