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

namespace SRVTracker
{
    public partial class FormRaceMonitor : Form
    {
        private string _routeFile = "";
        private EDRace _race = null;
        private Dictionary<string, System.Windows.Forms.ListViewItem> _racers = null;
        private Dictionary<string, EDRaceStatus> _racersStatus = null;
        private List<string> _eliminatedRacers = null;
        private EDWaypoint _nextWaypoint = null;
        private string _lastLeaderboardExport = "(We add random rubbish here to ensure that files are cleared on new race monitor instantiation)";
        private string _lastStatusExport = "kpokdpokwqpkdpqw";
        private string _lastSpeedExport = "ljoijgojerjgor";
        private string _lastTrackingTarget = "ojoijoje";
        private string _saveFilename = "";
        private bool _generatingLeaderboard = false;
        private object _lockListView = new object();
        private string _lastRacePositions = "";

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
            checkBoxSRVRace.Checked = _race.SRVOnly;
            checkBoxAllowPitstops.Checked = _race.AllowPitstops;
            checkBoxEliminationOnDestruction.Checked = _race.EliminateOnVehicleDestruction;
            UpdateButtons();
            ShowHideStreamingOptions();
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

            Action action = new Action(() =>
            {
                System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem("-");
                item.SubItems.Add(commander);
                item.SubItems.Add(status);
                item.SubItems.Add("NA");
                item.SubItems[3].Tag = double.MaxValue;
                lock (_lockListView)
                {
                    listViewParticipants.BeginUpdate();
                    _racers.Add(commander, item);
                    listViewParticipants.Items.Add(item);
                    _race.Contestants.Add(commander);
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
                    if (_racersStatus.ContainsKey(commander))
                        _racers[commander].SubItems[2].Text = $"{_racersStatus[commander].SpeedInMS:F0}m/s {_racersStatus[commander].ToString()}";
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

            if (_racersStatus!=null)
            {
                if (!_racersStatus.ContainsKey(edEvent.Commander))
                    return; // We're not tracking this commander
                _racersStatus[edEvent.Commander].UpdateStatus(edEvent);
                UpdateStatus(edEvent.Commander, _racersStatus[edEvent.Commander].ToString()); // We do this to display the speed in the listview
            }

            if (!edEvent.HasCoordinates())
                return;

            if (!EDRaceStatus.Started)
            {
                if (checkBoxAutoAddCommanders.Checked)
                    if (_race.Route.Waypoints.Count > 0)
                        if (_race.Route.Waypoints[0].LocationIsWithinWaypoint(edEvent.Location()))
                            AddTrackedCommander(edEvent.Commander);
            }

            if (!_racers.ContainsKey(edEvent.Commander))
                return;


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
                            /*if (listViewParticipants.Items[i].SubItems[1].Text!=_racers[positions[i]].SubItems[1].Text)
                                listViewParticipants.Items[i] = _racers[positions[i]];
                            if (!_racers[positions[i]].SubItems[0].Text.Equals((i + 1).ToString()))
                                _racers[positions[i]].SubItems[0].Text = (i + 1).ToString();*/
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

            if (_racersStatus==null)
            {
                // Race hasn't started, so we just show location info
            }
            else
            {
                // We have race status, so update listview item with that
                double distanceToWaypoint = _racersStatus[edEvent.Commander].DistanceToWaypoint;
                string distanceToShow = "NA";

                if (distanceToWaypoint < double.MaxValue)
                {
                    if (distanceToWaypoint > 2500)
                        distanceToShow = $"{(distanceToWaypoint / 1000):F1}km";
                    else
                        distanceToShow = $"{distanceToWaypoint:F1}m";
                }
                if (!distanceToShow.Equals(_racers[edEvent.Commander].SubItems[3].Text))
                {
                    // Needs updating
                    action = new Action(() => {
                        _racers[edEvent.Commander].SubItems[3].Text = distanceToShow;
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
            if (_racersStatus == null)
            {
                if (_racers.Count > 0)
                    return _racers.Keys.ToList();
                return positions;
            }               

            int finishedIndex = -1;
            foreach (string racer in _racersStatus.Keys)
            {
                if (_racersStatus[racer].Finished)
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
                        while ((i <= finishedIndex) && (_racersStatus[racer].FinishTime > _racersStatus[positions[i]].FinishTime))
                            i++;
                        if (i < positions.Count)
                            positions.Insert(i, racer);
                        else
                            positions.Add(racer);
                        finishedIndex = i;
                    }
                }
                else if (_racersStatus[racer].Eliminated)
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
                            while ((i < positions.Count) && _racersStatus[positions[i]].WaypointIndex < _racersStatus[racer].WaypointIndex && !_racersStatus[positions[i]].Eliminated)
                                i++;
                            if (i < positions.Count && _racersStatus[positions[i]].Eliminated)
                                i--;
                            else
                            {
                                // Now we check distances (as these positions are heading to the same waypoint)
                                while ((i < positions.Count) && (_racersStatus[positions[i]].WaypointIndex == _racersStatus[racer].WaypointIndex) && (_racersStatus[positions[i]].DistanceToWaypoint < _racersStatus[racer].DistanceToWaypoint) && (!_racersStatus[positions[i]].Eliminated))
                                    i++;
                                if (i < positions.Count && _racersStatus[positions[i]].Eliminated)
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
                    if (_racersStatus != null)
                    {
                        if (_racersStatus[leaderBoard[i]].Finished)
                            status.AppendLine($"({_racersStatus[leaderBoard[i]].FinishTime.Subtract(EDRaceStatus.StartTime):hh\\:mm\\:ss})");
                        else
                        {
                            if (checkBoxExportDistance.Checked && !_racersStatus[leaderBoard[i]].Eliminated)
                                status.AppendLine($"{(_racersStatus[leaderBoard[i]].DistanceToWaypoint / 1000):F1}");
                            else
                                status.AppendLine(_racersStatus[leaderBoard[i]].ToString());
                        }
                    }
                    else
                        status.AppendLine(EDRaceStatus.StatusMessages["Ready"]);
                }
                if (!_lastStatusExport.Equals(status.ToString()))
                {
                    if (checkBoxPaddingCharacters.Checked)
                        status.AppendLine(new string(textBoxPaddingChar.Text[0], (int)numericUpDownStatusPadding.Value));
                    try
                    {
                        File.WriteAllText(textBoxExportStatusFile.Text, status.ToString());
                        _lastStatusExport = status.ToString();
                    }
                    catch { }
                }
            }

            if (checkBoxExportTarget.Checked)
            {
                StringBuilder trackingTarget = new StringBuilder();
                if (checkBoxClosestPlayerTarget.Checked)
                    trackingTarget.Append(FormLocator.ClosestCommander);
                else
                    trackingTarget.Append(FormLocator.GetLocator().TrackingTarget);

                if (checkBoxPaddingCharacters.Checked)
                    trackingTarget.AppendLine(new string(textBoxPaddingChar.Text[0], (int)numericUpDownTargetPadding.Value));
                if (!_lastTrackingTarget.Equals(trackingTarget.ToString()))
                {
                    try
                    {
                        File.WriteAllText(textBoxExportTargetFile.Text, trackingTarget.ToString());
                        _lastTrackingTarget = trackingTarget.ToString();
                    }
                    catch { }
                }
            }

            if (checkBoxExportLeaderboard.Checked)
            {
                StringBuilder leaderBoardExport = new StringBuilder(String.Join(Environment.NewLine, leaderBoard));
                if (checkBoxPaddingCharacters.Checked)
                    leaderBoardExport.Append(new string(textBoxPaddingChar.Text[0], (int)numericUpDownLeaderboardPadding.Value));
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
                if (_racersStatus != null)
                {
                    for (int i = 0; i < leaderBoard.Count; i++)
                    {
                        if (EDRaceStatus.Started)
                        {
                            speeds.Append($"{_racersStatus[leaderBoard[i]].SpeedInMS:F0}");
                            if (checkBoxIncludeMaxSpeed.Checked)
                                speeds.Append($" ({_racersStatus[leaderBoard[i]].MaxSpeedInMS:F0})");
                            speeds.AppendLine();
                        }
                        else
                            speeds.AppendLine();
                    }
                }

                if (!_lastSpeedExport.Equals(speeds.ToString()))
                {
                    if (checkBoxPaddingCharacters.Checked)
                        speeds.AppendLine(new string(textBoxPaddingChar.Text[0], (int)numericUpDownSpeedPadding.Value));
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
                
                if (_racersStatus != null)
                {                   
                    if (_racersStatus[commanderPositions[i]].Eliminated)
                        position = "-";
                    rowHtml = rowHtml.Replace("Position", position);

                    if (_racersStatus[commanderPositions[i]].Finished)
                    {
                        rowHtml = rowHtml.Replace("RemainingDistance", "0km");
                        rowHtml = rowHtml.Replace("CurrentSpeed", "NA");
                    }
                    else
                    {
                        rowHtml = rowHtml.Replace("RemainingDistance", $"{(_racersStatus[commanderPositions[i]].DistanceToWaypoint / 1000):F1}km");
                        rowHtml = rowHtml.Replace("CurrentSpeed", $"{_racersStatus[commanderPositions[i]].SpeedInMS:F0}m/s");
                    }
                    rowHtml = rowHtml.Replace("Status", _racersStatus[commanderPositions[i]].ToString());
                    rowHtml = rowHtml.Replace("MaximumSpeed", $"{_racersStatus[commanderPositions[i]].MaxSpeedInMS:F0}m/s");
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
            checkBoxAutoAddCommanders.Checked = false;
            _eliminatedRacers = new List<string>();
            _racersStatus = new Dictionary<string, EDRaceStatus>();
            foreach (string commander in _racers.Keys)
                _racersStatus.Add(commander, new EDRaceStatus(commander, _race.Route));
            _nextWaypoint = _race.Route.Waypoints[1];
            listBoxWaypoints.Refresh();
            EDRaceStatus.EliminateOnDestruction = _race.EliminateOnVehicleDestruction;
            EDRaceStatus.AllowPitStops = _race.AllowPitstops;
            EDRaceStatus.EliminateOnShipFlight = _race.SRVOnly;
            _race.Start = DateTime.Now;
            EDRaceStatus.StartTime = _race.Start;
            buttonStartRace.Enabled = false;
            buttonStopRace.Enabled = true;
            buttonRaceHistory.Enabled = true;
            EDRaceStatus.Started = true;
        }

        private void buttonStopRace_Click(object sender, EventArgs e)
        {
            foreach (EDRaceStatus status in _racersStatus.Values)
                status.Finished = true;
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

            if (_nextWaypoint != null)
                if (_nextWaypoint.Name == ((System.Windows.Forms.ListBox)sender).Items[e.Index].ToString())
                {
                    font = new Font(e.Font, FontStyle.Bold);
                    brush = Brushes.LimeGreen;
                }

            e.Graphics.DrawString(((System.Windows.Forms.ListBox)sender).Items[e.Index].ToString(), font, brush, e.Bounds, StringFormat.GenericDefault);
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
                    EDRaceStatus.StatusMessages = formStatusMessages.StatusMessages();
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

            if (!checkBoxPaddingCharacters.Checked)
            {
                if (numericUpDownLeaderboardPadding.Enabled)
                        numericUpDownLeaderboardPadding.Enabled = false;
                if (numericUpDownStatusPadding.Enabled)
                        numericUpDownStatusPadding.Enabled = false;
                if (numericUpDownTargetPadding.Enabled)
                        numericUpDownTargetPadding.Enabled = false;
                if (numericUpDownSpeedPadding.Enabled)
                    numericUpDownSpeedPadding.Enabled = false;
                textBoxPaddingChar.Enabled = false;
            }
            else
            {
                if (!numericUpDownLeaderboardPadding.Enabled)
                    if (checkBoxExportLeaderboard.Checked)
                        numericUpDownLeaderboardPadding.Enabled = true;
                if (!numericUpDownStatusPadding.Enabled)
                    if (checkBoxExportStatus.Checked)
                        numericUpDownStatusPadding.Enabled = true;
                if (!numericUpDownSpeedPadding.Enabled)
                    if (checkBoxExportSpeed.Checked)
                        numericUpDownSpeedPadding.Enabled = true;
                if (!numericUpDownTargetPadding.Enabled)
                    if (checkBoxExportTarget.Checked)
                        numericUpDownTargetPadding.Enabled = true;
                textBoxPaddingChar.Enabled = true;
                
            }

            if (checkBoxExportLeaderboard.Checked)
            {
                if (!textBoxExportLeaderboardFile.Enabled)
                    textBoxExportLeaderboardFile.Enabled = true;
            }
            else
            {
                if (textBoxExportLeaderboardFile.Enabled)
                    textBoxExportLeaderboardFile.Enabled = false;
                if (numericUpDownLeaderboardPadding.Enabled)
                    numericUpDownLeaderboardPadding.Enabled = false;
            }

            if (checkBoxExportStatus.Checked)
            {
                if (!textBoxExportStatusFile.Enabled)
                    textBoxExportStatusFile.Enabled = true;
            }
            else
            {
                if (textBoxExportStatusFile.Enabled)
                    textBoxExportStatusFile.Enabled = false;
                if (numericUpDownStatusPadding.Enabled)
                    numericUpDownStatusPadding.Enabled = false;
            }

            if (checkBoxExportSpeed.Checked)
            {
                if (!textBoxExportSpeedFile.Enabled)
                    textBoxExportSpeedFile.Enabled = true;
            }
            else
            {
                if (textBoxExportSpeedFile.Enabled)
                    textBoxExportSpeedFile.Enabled = false;
                if (numericUpDownSpeedPadding.Enabled)
                    numericUpDownSpeedPadding.Enabled = false;
            }

            if (checkBoxExportTarget.Checked)
            {
                if (!textBoxExportTargetFile.Enabled)
                    textBoxExportTargetFile.Enabled = true;
            }
            else
            {
                if (textBoxExportTargetFile.Enabled)
                    textBoxExportTargetFile.Enabled = false;
                if (numericUpDownTargetPadding.Enabled)
                    numericUpDownTargetPadding.Enabled = false;
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
            _racersStatus = null;
            _eliminatedRacers = null;
            _racersStatus = null;
            if (_race.Route.Waypoints.Count > 0)
                _nextWaypoint = _race.Route.Waypoints[0];
            listBoxWaypoints.Refresh();
            buttonStartRace.Enabled = true;
            buttonReset.Enabled = false;
            buttonRaceHistory.Enabled = false;
        }

        private void ShowHideStreamingOptions()
        {
            Size showStreamingOptions = new Size(998, 464);
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
            FormRaceHistory formRaceHistory = new FormRaceHistory(_racersStatus);
            formRaceHistory.Show();
        }

        private void checkBoxExportSpeed_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void checkBoxExportDistance_CheckedChanged(object sender, EventArgs e)
        {
            // Don't need to do anything here as this is built into the export
        }


    }

    class ListViewItemComparer : IComparer
    {
        public ListViewItemComparer()
        {
        }
        public int Compare(object x, object y)
        {
            try
            {                
                return Convert.ToInt32(((System.Windows.Forms.ListViewItem)x).SubItems[0].Text) - Convert.ToInt32(((System.Windows.Forms.ListViewItem)y).SubItems[0].Text);
            }
            catch { }
            return 1;
        }
    }
}
