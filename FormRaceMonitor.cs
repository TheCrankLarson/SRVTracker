using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using EDTracking;
using System.Net;
using System.Text.Json;

namespace SRVTracker
{
    public partial class FormRaceMonitor : Form
    {
        private string _routeFile = "";
        private EDRace _race = null;
        private Dictionary<string, System.Windows.Forms.ListViewItem> _racers = null;
        private List<string> _eliminatedRacers = null;
        private EDWaypoint _nextWaypoint = null;
        private string _lastLeaderboardExport = "k"; // We set to a value to ensure export files are written when cleared first of all
        private string _lastStatusExport = "k";
        private string _lastSpeedExport = "k";
        private string _lastWPDistanceExport = "k";
        private string _lastTrackingTarget = "k";
        private string _lastRacePositions = "k";
        private string _lastExportTargetPitstops = "k";
        private string _lastExportTargetMaxSpeed = "k";
        private string _lastExportTargetSpeed = "k";
        private int _lastExportTargetRacePosition = 0;
        private string _saveFilename = "";
        private bool _generatingLeaderboard = false;
        private object _lockListView = new object();
        private ConfigSaverClass _formConfig = null;
        private WebClient _webClient = new WebClient();
        private string _serverRaceGuid = "";
        private NotableEvents _clientNotableEvents = null;
        private int _serverNotableEventsIndex = 0;

        public FormRaceMonitor()
        {
            InitializeComponent();
            groupBoxAddCommander.Visible = false;
            _race = new EDRace("", new EDRoute(""));
            
            CommanderWatcher.UpdateReceived += CommanderWatcher_UpdateReceived;
            CommanderWatcher.Start($"http://{FormLocator.ServerAddress}:11938/DataCollator/status");
            EDRaceStatus.StatusChanged += EDStatus_StatusChanged;
            //listViewParticipants.ListViewItemSorter = new ListViewItemComparer();
            _racers = new Dictionary<string, System.Windows.Forms.ListViewItem>();
            AddTrackedCommanders();
            FormStatusMessages.LoadFile();  // This restores any saved status messages

            // Attach our form configuration saver
            _formConfig = new ConfigSaverClass(this, true);
            _formConfig.ExcludedControls.Add(textBoxRaceName);
            _formConfig.ExcludedControls.Add(comboBoxAddCommander);
            _formConfig.ExcludedControls.Add(textBoxRouteName);
            _formConfig.ExcludedControls.Add(textBoxSystem);
            _formConfig.ExcludedControls.Add(textBoxPlanet);
            _formConfig.ExcludedControls.Add(groupBoxTrackTarget);
            _formConfig.SaveEnabled = true;
            _formConfig.StoreLabelInfo = false;
            _formConfig.StoreButtonInfo = false;
            
            ConfigSaverClass.ApplyConfiguration();
            textBoxNotableEventsFile.Enabled = checkBoxExportNotableEvents.Checked;
            numericUpDownNotableEventDuration.Enabled = checkBoxExportNotableEvents.Checked;

            checkBoxSRVRace.Checked = _race.SRVOnly;
            checkBoxAllowPitstops.Checked = _race.AllowPitstops;
            checkBoxEliminationOnDestruction.Checked = _race.EliminateOnVehicleDestruction;
            ShowHideStreamingOptions();
            UpdateButtons();
        }

        private void EDStatus_StatusChanged(object sender, string commander, string status)
        {
            if (_racers.ContainsKey(commander))
                UpdateStatus(commander, status);
        }

        private void CommanderWatcher_UpdateReceived(object sender, EDEvent edEvent)
        {

            // We've received an event for a listed racer
            Task.Run(new Action(() => { UpdateStatus(edEvent); }));
        }

        private void buttonLoadRoute_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = _routeFile;
                openFileDialog.Filter = "edroute files (*.edroute)|*.edroute|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FileName = _routeFile;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        EDRoute route = EDRoute.LoadFromFile(openFileDialog.FileName);
                        _race.Route = route;
                        textBoxRouteName.Text = route.Name;
                        DisplayRoute();
                    }
                    catch { }
                }
            }          
        }

        private void DisplayRoute()
        {
            // Update the route listview with our route
            if (_race.Route.Waypoints.Count < 1)
                return;

            if (_race.Route.Waypoints.Count > 0)
                _nextWaypoint = _race.Route.Waypoints[0];
            else
                _nextWaypoint = null;

            listBoxWaypoints.BeginUpdate();
            listBoxWaypoints.Items.Clear();
            foreach (EDWaypoint waypoint in _race.Route.Waypoints)
                listBoxWaypoints.Items.Add(waypoint.Name);
            listBoxWaypoints.EndUpdate();


            if (_race.Route.Waypoints.Count > 0)
            {
                buttonStartRace.Enabled = true;
                textBoxPlanet.Text = _race.Route.Waypoints[0].Location.PlanetName;
                textBoxSystem.Text = _race.Route.Waypoints[0].Location.SystemName;
            }
            textBoxRouteName.Text = _race.Route.Name;
        }

        private void AddTrackedCommanders()
        {
            if (CommanderWatcher.OnlineCommanderCount == 0 || EDRaceStatus.Started)
                return;

            Action action = new Action(() =>
            {
                listViewParticipants.BeginUpdate();
                foreach (string commander in CommanderWatcher.GetCommanders())
                {
                    if (!_racers.ContainsKey(commander))
                    {
                        try
                        {
                            if (checkBoxAutoAddCommanders.Checked && _race.Route.Waypoints[0].LocationIsWithinWaypoint(CommanderWatcher.GetCommanderStatus(commander).Location()))
                                AddTrackedCommander(commander);
                        }
                        catch { }
                    }
                }
                listViewParticipants.EndUpdate();
            });
            if (listViewParticipants.InvokeRequired)
                listViewParticipants.Invoke(action);
            else
                action();
        }

        private void AddTrackedCommander(string commander, string status = "Ready")
        {
            if (_racers.ContainsKey(commander))
                return;

            System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem("-");
            item.SubItems.Add(commander);
            item.SubItems.Add(status);
            item.SubItems.Add("NA");
            item.SubItems[3].Tag = double.MaxValue;
            _racers.Add(commander, item);
            _race.Contestants.Add(commander);

            Action action = new Action(() =>
            {
                lock (_lockListView)
                {
                    listViewParticipants.BeginUpdate();                    
                    listViewParticipants.Items.Add(item);
                    listViewParticipants.EndUpdate();
                }
            });
            if (listViewParticipants.InvokeRequired)
                listViewParticipants.Invoke(action);
            else
                action();

        }

        private void UpdateStatus(string commander, string status)
        {
            Action action = new Action(() =>
                {
                    if (_race.Statuses != null && _race.Statuses.ContainsKey(commander))
                        _racers[commander].SubItems[2].Text = $"{_race.Statuses[commander].SpeedInMS:F0}m/s {_race.Statuses[commander].ToString()}";
                    else
                        _racers[commander].SubItems[2].Text = status;
                });
            if (listViewParticipants.InvokeRequired)
                listViewParticipants.Invoke(action);
            else
                action();
        }

        private void UpdateStatus(EDEvent edEvent)
        {
            // Update the status table
            Action action;

            if (_race.Start>DateTime.MinValue)
            {
                // Race has started
                if (!_race.Statuses.ContainsKey(edEvent.Commander))
                    return; // We're not tracking this commander
                _race.Statuses[edEvent.Commander].UpdateStatus(edEvent);
                UpdateStatus(edEvent.Commander, _race.Statuses[edEvent.Commander].ToString()); // We do this to display the speed in the listview
            }

            if (!edEvent.HasCoordinates())
                return;


            if (checkBoxAutoAddCommanders.Checked)
                if (_race.Route.Waypoints.Count > 0)
                    if (_race.Route.Waypoints[0].LocationIsWithinWaypoint(edEvent.Location()))
                        AddTrackedCommander(edEvent.Commander);


            if (!_racers.ContainsKey(edEvent.Commander))
                return;

            if (_race.Start == DateTime.MinValue)
                return; // Race hasn't started, so we don't do anything further

            // Get current positions and sort the list view
            List<string> positions = RacePositions();
            string pos = String.Join("",positions);
            if (!pos.Equals(_lastRacePositions))
            {
                action = new Action(() =>
                {
                    lock (_lockListView)
                    {
                        listViewParticipants.BeginUpdate();
                        listViewParticipants.Items.Clear();
                        for (int i = 0; i < positions.Count; i++)
                        {
                            if (!_racers[positions[i]].SubItems[0].Text.Equals((i + 1).ToString()))
                                _racers[positions[i]].SubItems[0].Text = (i + 1).ToString();
                            listViewParticipants.Items.Add(_racers[positions[i]]);
                        }
                        listViewParticipants.EndUpdate();
                    }
                });
                if (listViewParticipants.InvokeRequired)
                    listViewParticipants.Invoke(action);
                else
                    action();
                _lastRacePositions = pos;
            }

            if (_race.Statuses == null)
            {
                // Race hasn't started, so we just show location info
            }
            else
            {
                // We have race status, so update listview item with that
                if (_race.Statuses.ContainsKey(edEvent.Commander) && !_race.Statuses[edEvent.Commander].DistanceToWaypointInKmDisplay.Equals(_racers[edEvent.Commander].SubItems[3].Text))
                {
                    // Needs updating
                    action = new Action(() => {
                        _racers[edEvent.Commander].SubItems[3].Text = _race.Statuses[edEvent.Commander].DistanceToWaypointInKmDisplay;
                    });
                    if (listViewParticipants.InvokeRequired)
                        listViewParticipants.Invoke(action);
                    else
                        action();
                }
            }

            try
            {
                ExportLeaderboard(positions);
            }
            catch
            {
                _generatingLeaderboard = false;
            }
        }

        private List<string> RacePositions()
        {
            List<string> positions = new List<string>();
            if (_race.Statuses == null)
            {
                if (_racers.Count > 0)
                    return _racers.Keys.ToList();
                return positions;
            }               

            int finishedIndex = -1;
            foreach (string racer in _race.Statuses.Keys)
            {
                if (_race.Statuses[racer].Finished)
                {
                    // If the racers are finished, then their position depends upon their time
                    if (finishedIndex<0)
                    {
                        // This is the first finisher we have, so add to top of list
                        if (positions.Count == 0)
                            positions.Add(racer);
                        else
                            positions.Insert(0, racer);
                        finishedIndex = 0;
                    }
                    else
                    {
                        // We need to work out where to add this finisher (based on finish time)
                        int i = 0;
                        while ((i <= finishedIndex) && (_race.Statuses[racer].FinishTime > _race.Statuses[positions[i]].FinishTime))
                            i++;
                        if (i < positions.Count)
                            positions.Insert(i, racer);
                        else
                            positions.Add(racer);
                        finishedIndex = i;
                    }
                }
                else if (_race.Statuses[racer].Eliminated)
                {
                    // Eliminated racers have no position, we can just add them at the end
                    positions.Add(racer);
                }
                else
                {
                    // All other positions are based on waypoint and distance from it (i.e. lowest waypoint number
                    if (positions.Count < 1)
                        positions.Add(racer);
                    else
                    {
                        int i = finishedIndex+1;
                        if (i < positions.Count)
                        {
                            // Move past anyone who is at a higher waypoint
                            while ((i < positions.Count) && _race.Statuses[positions[i]].WaypointIndex < _race.Statuses[racer].WaypointIndex && !_race.Statuses[positions[i]].Eliminated)
                                i++;
                            if ( (i < positions.Count) && _race.Statuses[positions[i]].Eliminated && (i > finishedIndex + 1))
                                i--;
                            else
                            {
                                // Now we check distances (as these positions are heading to the same waypoint)
                                while ((i < positions.Count) && (_race.Statuses[positions[i]].WaypointIndex == _race.Statuses[racer].WaypointIndex) && 
                                    (_race.Statuses[positions[i]].DistanceToWaypoint < _race.Statuses[racer].DistanceToWaypoint) && (!_race.Statuses[positions[i]].Eliminated))
                                    i++;
                                if ( (i < positions.Count) && _race.Statuses[positions[i]].Eliminated && (i > finishedIndex + 1))
                                    i--;
                            }
                        }
                        if (i < positions.Count)
                            positions.Insert(i, racer);
                        else
                            positions.Add(racer);
                    }
                }
            }
            return positions;
        }

        private void ExportLeaderboard(List<string>leaderBoard = null)
        {
            // Export the current leaderboard

            if (_generatingLeaderboard)
                return;
            _generatingLeaderboard = true;
          
            if (leaderBoard==null)
                leaderBoard = RacePositions();

            // We have the leaderboard, so now we retrieve the status for each racer in order
            if (checkBoxExportStatus.Checked)
            {
                StringBuilder status = new StringBuilder();
                for (int i = 0; i < leaderBoard.Count; i++)
                {
                    if (_race.Statuses != null)
                    {
                        if (_race.Statuses[leaderBoard[i]].Finished)
                            status.AppendLine(EDRace.StatusMessages["Completed"]);
                        //status.AppendLine($"({_racersStatus[leaderBoard[i]].FinishTime.Subtract(EDRaceStatus.StartTime):hh\\:mm\\:ss})");
                        else
                        {
                            if (checkBoxExportDistance.Checked && !_race.Statuses[leaderBoard[i]].Eliminated)
                            {
                                if ((_race.Statuses[leaderBoard[i]].DistanceToWaypoint == double.MaxValue))
                                    status.AppendLine("NA");
                                else
                                    status.AppendLine($"{_race.Statuses[leaderBoard[i]].DistanceToWaypointInKmDisplay}");
                            }
                            else
                            {
                                string s = _race.Statuses[leaderBoard[i]].ToString();
                                if (s.Length > numericUpDownStatusMaxLength.Value)
                                    s = s.Substring(0, (int)numericUpDownStatusMaxLength.Value);
                                status.AppendLine(s);
                            }
                        }
                    }
                    else
                        status.AppendLine(EDRace.StatusMessages["Ready"]);
                }

                if (!_lastStatusExport.Equals(status.ToString()))
                {
                    try
                    {
                        File.WriteAllText(textBoxExportStatusFile.Text, status.ToString());
                        _lastStatusExport = status.ToString();
                    }
                    catch { }
                }
            }

            ExportTrackingInfo();

            if (checkBoxExportLeaderboard.Checked)
            {
                StringBuilder leaderBoardExport = new StringBuilder(); ;
                for (int i = 0; i < leaderBoard.Count; i++)
                    if (leaderBoard[i].Length > numericUpDownLeaderboardMaxLength.Value)
                        leaderBoardExport.AppendLine(leaderBoard[i].Substring(0, (int)numericUpDownLeaderboardMaxLength.Value));
                    else
                        leaderBoardExport.AppendLine(leaderBoard[i]);

                if (!_lastLeaderboardExport.Equals(leaderBoardExport.ToString()))
                { 
                    try
                    {
                        File.WriteAllText(textBoxExportLeaderboardFile.Text, leaderBoardExport.ToString());
                        _lastLeaderboardExport = leaderBoardExport.ToString();
                    }
                    catch { }
                }
            }

            if (checkBoxExportSpeed.Checked)
            {
                StringBuilder speeds = new StringBuilder();
                if (_race.Statuses != null)
                {
                    for (int i = 0; i < leaderBoard.Count; i++)
                    {
                        if (EDRaceStatus.Started)
                        {
                            if (!_race.Statuses[leaderBoard[i]].Eliminated && !_race.Statuses[leaderBoard[i]].Finished)
                            {
                                speeds.Append($"{_race.Statuses[leaderBoard[i]].SpeedInMS:F0}");
                                if (checkBoxIncludeMaxSpeed.Checked)
                                    speeds.Append($" ({_race.Statuses[leaderBoard[i]].MaxSpeedInMS:F0})");
                                speeds.AppendLine();
                            }
                            else
                                speeds.AppendLine("");                            
                        }
                        else
                            speeds.AppendLine();
                    }
                }

                if (!_lastSpeedExport.Equals(speeds.ToString()))
                {
                    try
                    {
                        File.WriteAllText(textBoxExportSpeedFile.Text, speeds.ToString());
                        _lastSpeedExport = speeds.ToString();
                    }
                    catch { }
                }
            }

            if (checkBoxExportAsHTML.Checked)
                ExportLeaderboardAsHTML(leaderBoard);

            _generatingLeaderboard = false;
        }

        private string _htmlTemplateBeforeTable = "";
        private string _htmlTemplateAfterTable = "";
        private string _htmlRowTemplate = "";
        private string _lastHtml = "";

        private bool PrepareHTMLTemplate()
        {
            try
            {
                string htmlTemplate = File.ReadAllText(textBoxHTMLTemplateFile.Text);
                int tableStart = htmlTemplate.IndexOf("<!-- #LEADERBOARD# -->");
                int tableEnd = htmlTemplate.IndexOf("<!-- #/LEADERBOARD# -->") + 23;
                _htmlTemplateBeforeTable = htmlTemplate.Substring(0, tableStart);
                _htmlTemplateAfterTable = htmlTemplate.Substring(tableEnd);
                _htmlRowTemplate = htmlTemplate.Substring(tableStart + 23, tableEnd - tableStart - 46);
                return true;
            }
            catch
            {
                _htmlTemplateBeforeTable = "";  // Force reload if HTML export enabled again
            }
            return false;
        }

        private void ExportLeaderboardAsHTML(List<string> commanderPositions)
        {
            if (String.IsNullOrEmpty(_htmlTemplateBeforeTable))
                if (!PrepareHTMLTemplate())
                {
                    checkBoxExportAsHTML.Checked = false;
                    return;
                }

            StringBuilder html = new StringBuilder(_htmlTemplateBeforeTable);
            for (int i=0; i<commanderPositions.Count; i++)
            {
                string position = (i + 1).ToString();
                string rowHtml = _htmlRowTemplate.Replace("Commander", commanderPositions[i]);
                
                if (_race.Statuses != null)
                {                   
                    if (_race.Statuses[commanderPositions[i]].Eliminated)
                        position = "-";
                    rowHtml = rowHtml.Replace("Position", position);

                    if (_race.Statuses[commanderPositions[i]].Finished)
                    {
                        rowHtml = rowHtml.Replace("RemainingDistance", "0km");
                        rowHtml = rowHtml.Replace("CurrentSpeed", "NA");
                    }
                    else
                    {
                        rowHtml = rowHtml.Replace("RemainingDistance", $"{_race.Statuses[commanderPositions[i]].DistanceToWaypointInKmDisplay}km");
                        rowHtml = rowHtml.Replace("CurrentSpeed", $"{_race.Statuses[commanderPositions[i]].SpeedInMS:F0}m/s");
                    }
                    rowHtml = rowHtml.Replace("Status", _race.Statuses[commanderPositions[i]].ToString());
                    rowHtml = rowHtml.Replace("MaximumSpeed", $"{_race.Statuses[commanderPositions[i]].MaxSpeedInMS:F0}m/s");
                }
                else
                {
                    rowHtml = rowHtml.Replace("Position", position);
                    rowHtml = rowHtml.Replace("RemainingDistance", "NA");
                    rowHtml = rowHtml.Replace("CurrentSpeed", "NA");
                    rowHtml = rowHtml.Replace("Status", "");
                    rowHtml = rowHtml.Replace("MaximumSpeed", "NA");
                }
                html.AppendLine(rowHtml);
            }
            html.AppendLine(_htmlTemplateAfterTable);

            if (!html.ToString().Equals(_lastHtml))
            {
                try
                {
                    File.WriteAllText(textBoxExportHTMLTo.Text, html.ToString());
                    _lastHtml = html.ToString();
                }
                catch// (Exception ex)
                {
                    //MessageBox.Show($"Failed to write file: {ex.Message}{Environment.NewLine}{textBoxExportHTMLTo.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonRemoveParticipant_Click(object sender, EventArgs e)
        {
            if (listViewParticipants.SelectedItems.Count != 1)
                return;

            if (_racers.ContainsKey(listViewParticipants.SelectedItems[0].SubItems[1].Text))
                _racers.Remove(listViewParticipants.SelectedItems[0].SubItems[1].Text);
            listViewParticipants.SelectedItems[0].Remove();
        }

        private void buttonTrackParticipant_Click(object sender, EventArgs e)
        {
            if (listViewParticipants.SelectedItems.Count != 1)
                return;

            FormLocator.GetLocator().SetTarget(listViewParticipants.SelectedItems[0].SubItems[1].Text);
        }

        private void buttonLoadRace_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = _routeFile;
                openFileDialog.Filter = "edrace files (*.edrace)|*.edrace|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FileName = _saveFilename;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _race = EDRace.LoadFromFile(openFileDialog.FileName);
                        _saveFilename = openFileDialog.FileName;
                        textBoxRaceName.Text = _race.Name;
                        DisplayRoute();
                        checkBoxAutoAddCommanders.Checked = true;
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
                saveFileDialog.InitialDirectory = _saveFilename;
                saveFileDialog.Filter = "edrace files (*.edrace)|*.edrace|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = _saveFilename;
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
            if (!String.IsNullOrEmpty(_saveFilename))
            {
                try
                {
                    _race.SaveToFile(_saveFilename);
                }
                catch { }
            }
        }

        private void buttonAddParticipant_Click(object sender, EventArgs e)
        {
            comboBoxAddCommander.Items.Clear();
            foreach (string commander in CommanderWatcher.GetCommanders())
                comboBoxAddCommander.Items.Add(commander);
            groupBoxAddCommander.Visible = true;
            comboBoxAddCommander.Focus();
        }

        private void textBoxAddCommander_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for <ENTER>

            if (e.KeyChar == (char)Keys.Return)
                buttonAddCommander_Click(sender, null);
        }

        private void buttonAddCommander_Click(object sender, EventArgs e)
        {
            // Add the commander and hide the data entry groupbox
            groupBoxAddCommander.Visible = false;
            if (!String.IsNullOrEmpty(comboBoxAddCommander.Text))
                AddTrackedCommander(comboBoxAddCommander.Text, "");
            comboBoxAddCommander.Text = "";
        }

        private void FormRaceMonitor_Deactivate(object sender, EventArgs e)
        {
            groupBoxAddCommander.Visible = false;
        }

        private void buttonStartRace_Click(object sender, EventArgs e)
        {
            if (_race.Route.Waypoints.Count<2)
            {
                MessageBox.Show(this, "At least two waypoints are required to start a race", "Invalid Route", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_racers.Count<1)
            {
                MessageBox.Show(this, "At least one commander is required to start a race", "No participants", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (checkBoxServerMonitoring.Checked)
                if (!StartServerMonitoredRace())
                    return;
            else
                StartClientMonitoredRace();

            checkBoxAutoAddCommanders.Checked = false;
            buttonStartRace.Enabled = false;
            buttonStopRace.Enabled = true;
            buttonRaceHistory.Enabled = true;

        }

        private bool StartServerMonitoredRace()
        {
            // We send the race info to the server with StartRace command - this starts the monitoring on the server
            // We then just need to download results, and not keep track of status updates

            //string raceGuid = "";
            try
            {
                //Stream statusStream = _webClient.OpenRead($"http://{FormLocator.ServerAddress}:11938/DataCollator/startrace");
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string response = wc.UploadString($"http://{FormLocator.ServerAddress}:11938/DataCollator/startrace", JsonSerializer.Serialize(_race));
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
                            textBoxRaceStatusServerUrl.Text = $"http://{FormLocator.ServerAddress}:11938/DataCollator/getrace/{_serverRaceGuid}";
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
                    CommanderWatcher.UpdateReceived -= CommanderWatcher_UpdateReceived; // We don't want client notifications anymore
                    string notableEventOutputFile = "";
                    if (checkBoxExportNotableEvents.Checked)
                        notableEventOutputFile = textBoxNotableEventsFile.Text;
                    _clientNotableEvents = new NotableEvents(notableEventOutputFile);
                    _serverNotableEventsIndex = 0;
                    timerRefreshFromServer.Start();
                }
                return true;
            }
            catch { }
            return false;
        }

        private void StartClientMonitoredRace()
        {
            _eliminatedRacers = new List<string>();

            NotableEvents notableEvents = null;
            if (checkBoxExportNotableEvents.Checked)
            {
                notableEvents = new NotableEvents(textBoxNotableEventsFile.Text);
                notableEvents.UpdateInterval = (int)numericUpDownNotableEventDuration.Value;
            }

            _nextWaypoint = _race.Route.Waypoints[1];
            listBoxWaypoints.Refresh();
            EDRaceStatus.EliminateOnDestruction = _race.EliminateOnVehicleDestruction;
            EDRaceStatus.AllowPitStops = _race.AllowPitstops;
            EDRaceStatus.EliminateOnShipFlight = _race.SRVOnly;
            _race.StartRace(false);
            EDRaceStatus.StartTime = _race.Start;
            EDRaceStatus.Started = true;
        }

        private void buttonStopRace_Click(object sender, EventArgs e)
        {
            _race.Finished = true;
            if (_race.Statuses != null)
                foreach (EDRaceStatus status in _race.Statuses.Values)
                    status.Finished = true;
            
            if (!String.IsNullOrEmpty(_serverRaceGuid))
            {
                // Send notification to server that race is finished
                try
                {
                    using (WebClient webClient = new WebClient())
                    {
                        string raceStatus = webClient.DownloadString($"http://{FormLocator.ServerAddress}:11938/DataCollator/stoprace/{_serverRaceGuid}");
                    }
                }
                catch { }
                _serverRaceGuid = "";
            }

            buttonStopRace.Enabled = false;
            buttonReset.Enabled = true;
        }

        private void comboBoxAddCommander_Leave(object sender, EventArgs e)
        {
            if (this.ActiveControl != buttonAddCommander)
                groupBoxAddCommander.Visible = false;
        }

        private void listBoxWaypoints_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush brush = Brushes.Black;
            Font font = e.Font;
            e.Graphics.FillRectangle(new SolidBrush(((System.Windows.Forms.ListBox)sender).BackColor), e.Bounds);

            if (e.Index > -1)
            {
                if (_nextWaypoint != null)
                    if (_nextWaypoint.Name == ((System.Windows.Forms.ListBox)sender).Items[e.Index].ToString())
                    {
                        font = new Font(e.Font, FontStyle.Bold);
                        brush = Brushes.LimeGreen;
                    }

                e.Graphics.DrawString(((System.Windows.Forms.ListBox)sender).Items[e.Index].ToString(), font, brush, e.Bounds, StringFormat.GenericDefault);
            }
            e.DrawFocusRectangle();
        }

        private void checkBoxShowDetailedStatus_CheckedChanged(object sender, EventArgs e)
        {
            EDRaceStatus.ShowDetailedStatus = checkBoxShowDetailedStatus.Checked;
        }

        private void comboBoxAddCommander_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonAddCommander_Click(null, null);
            comboBoxAddCommander.SelectedIndex = -1;
        }

        private void checkBoxEliminationOnDestruction_CheckedChanged(object sender, EventArgs e)
        {
            _race.EliminateOnVehicleDestruction = checkBoxEliminationOnDestruction.Checked;
        }

        private void checkBoxSRVRace_CheckedChanged(object sender, EventArgs e)
        {
            _race.SRVOnly = checkBoxSRVRace.Checked;
            checkBoxAllowPitstops.Enabled = checkBoxSRVRace.Checked;
        }

        private void checkBoxAllowPitstops_CheckedChanged(object sender, EventArgs e)
        {
            _race.AllowPitstops = checkBoxAllowPitstops.Checked;
        }

        private void buttonEditStatusMessages_Click(object sender, EventArgs e)
        {
            using (FormStatusMessages formStatusMessages = new FormStatusMessages())
            {
                if (formStatusMessages.ShowDialog(this) == DialogResult.OK)
                    EDRace.StatusMessages = formStatusMessages.StatusMessages();
            }
        }
        private void UpdateButtons()
        {
            bool participantSelected = listViewParticipants.SelectedIndices.Count > 0;
            if (!buttonRemoveParticipant.Enabled && participantSelected)
            {
                buttonRemoveParticipant.Enabled = true;
                buttonTrackParticipant.Enabled = true;
            }
            else if (buttonRemoveParticipant.Enabled && !participantSelected)
            {
                buttonRemoveParticipant.Enabled = false;
                buttonTrackParticipant.Enabled = false;
            }

            if (String.IsNullOrEmpty(_race.Name))
            {
                if (buttonSaveRaceAs.Enabled)
                {
                    buttonSaveRace.Enabled = false;
                    buttonSaveRaceAs.Enabled = false;
                }
            }
            else
            {
                if (!buttonSaveRaceAs.Enabled)
                    buttonSaveRaceAs.Enabled = true;
                if (!buttonSaveRace.Enabled && !String.IsNullOrEmpty(_saveFilename))
                    buttonSaveRace.Enabled = true;                   
            }

            if (checkBoxExportLeaderboard.Checked)
            {
                if (!textBoxExportLeaderboardFile.Enabled)
                    textBoxExportLeaderboardFile.Enabled = true;
                if (!numericUpDownLeaderboardMaxLength.Enabled)
                    numericUpDownLeaderboardMaxLength.Enabled = true;
            }
            else
            {
                if (textBoxExportLeaderboardFile.Enabled)
                    textBoxExportLeaderboardFile.Enabled = false;
                if (numericUpDownLeaderboardMaxLength.Enabled)
                    numericUpDownLeaderboardMaxLength.Enabled = false;
            }

            if (checkBoxExportStatus.Checked)
            {
                if (!textBoxExportStatusFile.Enabled)
                    textBoxExportStatusFile.Enabled = true;
                if (!numericUpDownStatusMaxLength.Enabled)
                    numericUpDownStatusMaxLength.Enabled = true;

            }
            else
            {
                if (textBoxExportStatusFile.Enabled)
                    textBoxExportStatusFile.Enabled = false;
                if (numericUpDownStatusMaxLength.Enabled)
                    numericUpDownStatusMaxLength.Enabled = false;
            }

            textBoxExportSpeedFile.Enabled = checkBoxExportSpeed.Checked;
            textBoxExportTargetFile.Enabled = checkBoxExportTrackedTarget.Checked;
            textBoxExportTargetMaxSpeedFile.Enabled = checkBoxExportTrackedTargetMaxSpeed.Checked;
            textBoxExportTargetPitstopsFile.Enabled = checkBoxExportTrackedTargetPitstops.Checked;
            textBoxExportTargetSpeedFile.Enabled = checkBoxExportTrackedTargetSpeed.Checked;
            textBoxExportTargetPosition.Enabled = checkBoxExportTrackedTargetPosition.Checked;
            textBoxExportWaypointDistanceFile.Enabled = checkBoxExportDistance.Checked;

            buttonUneliminate.Enabled = false;
            if (listViewParticipants.SelectedItems.Count>0)
            {
                foreach (System.Windows.Forms.ListViewItem item in listViewParticipants.SelectedItems)
                    if ( item.SubItems[2].Text.Contains(EDRace.StatusMessages["Eliminated"]) || item.SubItems[2].Text.Contains("Eliminated") )
                    {
                        buttonUneliminate.Enabled = true;
                        break;
                    }
            }
        }

        private void listViewParticipants_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();   
        }

        private void checkBoxPaddingCharacters_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void checkBoxExportTarget_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtons();
            if (checkBoxExportTrackedTarget.Checked)
                FormLocator.GetLocator();
        }

        private void checkBoxExportStatus_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void checkBoxExportLeaderboard_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            EDRaceStatus.Started = false;
            _race.Statuses = null;
            _eliminatedRacers = null;
            if (_race.Route.Waypoints.Count > 0)
                _nextWaypoint = _race.Route.Waypoints[0];
            listBoxWaypoints.Refresh();
            buttonStartRace.Enabled = true;
            buttonReset.Enabled = false;
            buttonRaceHistory.Enabled = false;          
        }

        private void ShowHideStreamingOptions()
        {
            Size showStreamingOptions = new Size(1240, 464);
            Size hideStreamingOptions = new Size(742, 464);
            if (checkBoxStreamInfo.Checked)
            {
                if (!this.Size.Equals(showStreamingOptions))
                    this.Size = showStreamingOptions;
            }
            else
            {
                if (!this.Size.Equals(hideStreamingOptions))
                    this.Size = hideStreamingOptions;
            }
        }

        private void checkBoxStreamInfo_CheckedChanged(object sender, EventArgs e)
        {
            ShowHideStreamingOptions();
        }

        private void textBoxRaceName_TextChanged(object sender, EventArgs e)
        {
            _race.Name = textBoxRaceName.Text;
            if (!buttonSaveRace.Enabled)
                UpdateButtons();
        }

        private void buttonRaceHistory_Click(object sender, EventArgs e)
        {
            FormRaceHistory formRaceHistory;
            if (checkBoxServerMonitoring.Checked)
                formRaceHistory = new FormRaceHistory(_racers.Keys.ToList<string>(),_serverRaceGuid);
            else
                formRaceHistory = new FormRaceHistory(_race.Statuses);
            formRaceHistory.Show();
        }

        private void checkBoxExportSpeed_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void checkBoxExportDistance_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void checkBoxExportNotableEvents_CheckedChanged(object sender, EventArgs e)
        {
            textBoxNotableEventsFile.Enabled = checkBoxExportNotableEvents.Checked;
            numericUpDownNotableEventDuration.Enabled = checkBoxExportNotableEvents.Checked;
        }

        private void checkBoxExportTargetMaxSpeed_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void checkBoxExportTargetPitstops_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void timerRefreshFromServer_Tick(object sender, EventArgs e)
        {
            timerRefreshFromServer.Stop();
            if (String.IsNullOrEmpty(_serverRaceGuid))
                return;

            _webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            string response = _webClient.UploadString($"http://{FormLocator.ServerAddress}:11938/DataCollator/racestatus", _serverRaceGuid);
            Dictionary<string, string> raceStats = JsonSerializer.Deserialize< Dictionary<string, string>>(response);
            UpdateFromServerStats(raceStats);
            ExportTrackingInfo();
            timerRefreshFromServer.Start();
        }

        private void UpdateFromServerStats(Dictionary<string,string> serverStats)
        {
            // Export any statuses and update our list view from the data we've received from the server
            bool changeDetected = false;

            // Server sends us all the notable events, so we only fire any that are new
            string[] notableEvents = serverStats["NotableEvents"].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            while (_serverNotableEventsIndex<notableEvents.Length)
            {
                _clientNotableEvents.AddEvent(notableEvents[_serverNotableEventsIndex]);
                _serverNotableEventsIndex++;
            }

            // We have the leaderboard, so now we retrieve the status for each racer in order

            if (!_lastStatusExport.Equals(serverStats["Status"]))
            {
                if (checkBoxExportStatus.Checked)
                {
                    try
                    {
                        File.WriteAllText(textBoxExportStatusFile.Text, serverStats["Status"]);
                        _lastStatusExport = serverStats["Status"];
                    }
                    catch { }
                }
                changeDetected = true;
            }

            if (!_lastWPDistanceExport.Equals(serverStats["DistanceToWaypoint"]))
            {
                if (checkBoxExportDistance.Checked)
                {
                    try
                    {
                        File.WriteAllText(textBoxExportWaypointDistanceFile.Text, serverStats["DistanceToWaypoint"]);
                        _lastWPDistanceExport = serverStats["DistanceToWaypoint"];
                    }
                    catch { }
                }
                changeDetected = true;
            }

            if (!_lastLeaderboardExport.Equals(serverStats["Positions"]))
            {
                if (checkBoxExportLeaderboard.Checked)
                {
                    try
                    {
                        File.WriteAllText(textBoxExportLeaderboardFile.Text, serverStats["Positions"]);
                        _lastLeaderboardExport = serverStats["Positions"];
                    }
                    catch { }
                }
                changeDetected = true;
            }

            if (!_lastSpeedExport.Equals(serverStats["Speeds"]))
            {
                if (checkBoxExportSpeed.Checked)
                {
                    try
                    {
                        File.WriteAllText(textBoxExportSpeedFile.Text, serverStats["Speeds"]);
                        _lastSpeedExport = serverStats["Speeds"];
                    }
                    catch { }
                }
                changeDetected = true;
            }

            // Get current positions and sort the list view
            if (changeDetected)
            {
                List<string> positions = serverStats["Positions"].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                List<string> statuses = serverStats["Status"].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                List<string> distancesToWaypoint = serverStats["DistanceToWaypoint"].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();

                Action action = new Action(() =>
                {
                    lock (_lockListView)
                    {
                        listViewParticipants.BeginUpdate();
                        if (!_lastRacePositions.Equals(serverStats["Positions"]))
                        {
                            // If positions have changed, we clear and re-add in correct order
                            listViewParticipants.Items.Clear();
                            for (int i = 0; i < positions.Count; i++)
                            {
                                if (!_racers[positions[i]].SubItems[0].Text.Equals((i + 1).ToString()))
                                    _racers[positions[i]].SubItems[0].Text = (i + 1).ToString();
                                listViewParticipants.Items.Add(_racers[positions[i]]);
                            }
                        }
                        _lastRacePositions = serverStats["Positions"];
                        for (int i = 0; i < positions.Count; i++)
                        {
                            _racers[positions[i]].SubItems[2].Text = statuses[i];
                            _racers[positions[i]].SubItems[3].Text = distancesToWaypoint[i];
                        }
                        listViewParticipants.EndUpdate();                      
                    }
                });
                if (listViewParticipants.InvokeRequired)
                    listViewParticipants.Invoke(action);
                else
                    action();                
            }
        }

        private bool _showError = true;
        private EDRaceStatus GetCommanderRaceStatus(string commander)
        {
            if (String.IsNullOrEmpty(commander))
                return null;

            if (checkBoxServerMonitoring.Checked && !String.IsNullOrEmpty(_serverRaceGuid))
            {
                // We need to retrieve the status from the server
                try
                {
                    using (WebClient webClient = new WebClient())
                    {
                        string raceStatus = webClient.DownloadString($"http://{FormLocator.ServerAddress}:11938/DataCollator/getcommanderracestatus/{_serverRaceGuid}/{System.Web.HttpUtility.UrlEncode(commander)}");
                        if (raceStatus.Length > 2)
                            return EDRaceStatus.FromJson(raceStatus);
                    }
                }
                catch (Exception ex)
                {
                    if (_showError)
                        MessageBox.Show($"Error retrieving tracked target:{Environment.NewLine}{ex.Message}", "Tracking Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _showError = false;
                }
            }
            else if (_race.Statuses != null && _race.Statuses.ContainsKey(commander))
                    return _race.Statuses[commander];
         
            return null;
        }
        
        private void ExportTrackingInfo()
        {
            if (!checkBoxExportTrackedTarget.Checked && !checkBoxExportTrackedTargetMaxSpeed.Checked && !checkBoxExportTrackedTargetPitstops.Checked &&
                !checkBoxExportTrackedTargetPosition.Checked && !checkBoxExportTrackedTargetSpeed.Checked)
                return;

            String trackingTarget = "";
            string trackingStatus = "Track Target";

            if (checkBoxClosestPlayerTarget.Checked)
                trackingTarget = FormLocator.ClosestCommander;

            if (String.IsNullOrEmpty(trackingTarget))
                trackingTarget = FormLocator.GetLocator().TrackingTarget;

            if (!String.IsNullOrEmpty(trackingTarget))
                trackingStatus = $"Track target ({trackingTarget})";

            EDRaceStatus commanderStatus = GetCommanderRaceStatus(trackingTarget);
            if (commanderStatus == null)
                trackingStatus = $"Track target ({trackingTarget} FAILED)";

            Action action = new Action(() =>
            {
                groupBoxTrackTarget.Text = trackingStatus;
            });
            if (groupBoxTrackTarget.InvokeRequired)
                groupBoxTrackTarget.Invoke(action);
            else
                action();

            if (commanderStatus == null)
                return;

            if (checkBoxExportTrackedTarget.Checked && !_lastTrackingTarget.Equals(trackingTarget))
            {
                try
                {
                    File.WriteAllText(textBoxExportTargetFile.Text, trackingTarget);
                    _lastTrackingTarget = trackingTarget.ToString();
                }
                catch { }
            }

            if (checkBoxExportTrackedTargetSpeed.Checked)
            {
                string exportSpeed = commanderStatus.SpeedInMS.ToString("F0");

                if (!exportSpeed.Equals(_lastExportTargetSpeed))
                {
                    try
                    {
                        File.WriteAllText(textBoxExportSpeedFile.Text, exportSpeed);
                        _lastExportTargetMaxSpeed = exportSpeed;
                    }
                    catch { }
                }
            }

            if (checkBoxExportTrackedTargetPosition.Checked)
            {
                if (commanderStatus.RacePosition != _lastExportTargetRacePosition)
                {
                    try
                    {
                        File.WriteAllText(textBoxExportTargetPosition.Text, commanderStatus.RacePosition.ToString());
                        _lastExportTargetRacePosition = commanderStatus.RacePosition;
                    }
                    catch { }
                }
            }

            if (checkBoxExportTrackedTargetMaxSpeed.Checked)
            {
                string exportMaxSpeed = commanderStatus.MaxSpeedInMS.ToString("F0");

                if (!exportMaxSpeed.Equals(_lastExportTargetMaxSpeed))
                {
                    try
                    {
                        File.WriteAllText(textBoxExportTargetMaxSpeedFile.Text, exportMaxSpeed);
                        _lastExportTargetMaxSpeed = exportMaxSpeed;
                    }
                    catch { }
                }
            }

            if (checkBoxExportTrackedTargetPitstops.Checked)
            {
                string exportPitstops = commanderStatus.PitStopCount.ToString();
                if (!exportPitstops.Equals(_lastExportTargetPitstops))
                {
                    try
                    {
                        File.WriteAllText(textBoxExportTargetPitstopsFile.Text, exportPitstops);
                        _lastExportTargetPitstops = exportPitstops;
                    }
                    catch { }
                }
            }
        }

        private void Resurrect(string commander)
        {
            if (checkBoxServerMonitoring.Checked)
            {
                // Need to send resurrection request to server
                if (!String.IsNullOrEmpty(_serverRaceGuid))
                    _ = _webClient.DownloadString($"http://{FormLocator.ServerAddress}:11938/DataCollator/ResurrectCommander/{_serverRaceGuid}/{commander}");
            }
            else
            {
                if ((_race.Statuses != null) && _race.Statuses.ContainsKey(commander))
                    _race.Statuses[commander].Resurrect();                    
            }
        }

        private void buttonUneliminate_Click(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.ListViewItem item in listViewParticipants.SelectedItems)
                Resurrect(item.SubItems[1].Text);
        }

        private void checkBoxExportTargetSpeed_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void checkBoxExportTargetPosition_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void FormRaceMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!String.IsNullOrEmpty(_serverRaceGuid))
                buttonStopRace_Click(this, null);
        }
    }
}
