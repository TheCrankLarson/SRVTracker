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
        private string _lastLeaderboardExport = "";
        private string _lastStatusExport = "";
        private string _lastTrackingTarget = "";
        private string _saveFilename = "";

        public FormRaceMonitor()
        {
            InitializeComponent();
            groupBoxAddCommander.Visible = false;
            _race = new EDRace("", new EDRoute(""));
            
            CommanderWatcher.UpdateReceived += CommanderWatcher_UpdateReceived;
            CommanderWatcher.Start();
            EDRaceStatus.StatusChanged += EDStatus_StatusChanged;
            listViewParticipants.ListViewItemSorter = new ListViewItemComparer();
            _racers = new Dictionary<string, System.Windows.Forms.ListViewItem>();
            AddTrackedCommanders();
            checkBoxSRVRace.Checked = EDRaceStatus.EliminateOnShipFlight;
            checkBoxAllowPitstops.Checked = EDRaceStatus.AllowPitStops;
            checkBoxEliminationOnDestruction.Checked = EDRaceStatus.EliminateOnShipFlight;
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
                        string status = "Unknown";
                        try
                        {
                            if (checkBoxAutoAddCommanders.Checked && _race.Route.Waypoints[0].LocationIsWithinWaypoint(CommanderWatcher.GetCommanderStatus(commander).Location()))
                                status = "Ready";
                        }
                        catch { }
                        if (!checkBoxAutoAddCommanders.Checked || status.Equals("Ready"))
                        {
                            System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem("-");
                            item.SubItems.Add(commander);
                            item.SubItems.Add(status);
                            item.SubItems.Add("NA");
                            item.SubItems[3].Tag = double.MaxValue;
                            listViewParticipants.Items.Add(item);
                            _racers.Add(commander, item);
                            _race.Contestants.Add(commander);
                        }
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
                listViewParticipants.BeginUpdate();

                System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem("-");
                item.SubItems.Add(commander);
                item.SubItems.Add(status);
                item.SubItems.Add("NA");
                item.SubItems[3].Tag = double.MaxValue;
                listViewParticipants.Items.Add(item);
                _racers.Add(commander, item);
                _race.Contestants.Add(commander);

                listViewParticipants.EndUpdate();
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

            if (EDRaceStatus.Started)
            {
                if (!_racersStatus.ContainsKey(edEvent.Commander))
                    return; // We're not tracking this commander
                _racersStatus[edEvent.Commander].UpdateStatus(edEvent);
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

            if (_nextWaypoint != null)
            {
                double distanceToWaypoint;
                if (_racersStatus==null)
                    distanceToWaypoint = EDLocation.DistanceBetween(edEvent.Location(), _nextWaypoint.Location);
                else
                    distanceToWaypoint = _racersStatus[edEvent.Commander].DistanceToWaypoint;
                _racers[edEvent.Commander].SubItems[3].Tag = distanceToWaypoint; // Store in m for comparison
                Action action;
                string distanceToShow = "NA";

                if (distanceToWaypoint < double.MaxValue)
                {
                    if (distanceToWaypoint > 2500)
                        distanceToShow = $"{(distanceToWaypoint / 1000):0.0}km";
                    else
                        distanceToShow = $"{distanceToWaypoint:0.0}m";
                }

                if (!distanceToShow.Equals(_racers[edEvent.Commander].SubItems[3].Text))
                {
                    // Needs updating
                    action = new Action(() => { _racers[edEvent.Commander].SubItems[3].Text = distanceToShow; });
                    if (listViewParticipants.InvokeRequired)
                        listViewParticipants.Invoke(action);
                    else
                        action();
                }

                List<double> distancesToWaypoint = new List<double>();
                foreach (System.Windows.Forms.ListViewItem item in _racers.Values)
                    distancesToWaypoint.Add((double)item.SubItems[3].Tag);
                distancesToWaypoint.Sort();

                action = new Action(() =>
                {
                    listViewParticipants.BeginUpdate();
                    foreach (System.Windows.Forms.ListViewItem item in _racers.Values)
                    {
                        int i = 0;
                        while (i < distancesToWaypoint.Count && distancesToWaypoint[i] != (double)item.SubItems[3].Tag)
                            i++;
                        if (i < distancesToWaypoint.Count)
                        {
                            // We have the position
                            i++;
                            if (!item.Text.Equals(i.ToString()))
                                item.Text = (i).ToString();
                        }
                        else
                            item.Text = "-";
                    }

                    // listViewParticipants.ListViewItemSorter = new ListViewItemComparer();
                    listViewParticipants.Sort();
                    listViewParticipants.EndUpdate();
                    if (checkBoxExportLeaderboard.Checked)
                        ExportLeaderboard();
                });
                if (listViewParticipants.InvokeRequired)
                    listViewParticipants.Invoke(action);
                else
                    action();
            }
        }

        private void ExportLeaderboard()
        {
            // Export the current leaderboard
            StringBuilder status = new StringBuilder();
            string[] leaderboard = new string[listViewParticipants.Items.Count+1];
            StringBuilder trackingTarget = new StringBuilder();

            // Below is a quick fix to (hopefully) resolve missing entries in status and leaderboard.  This seems to be caused by the ListView being updated while
            // this code is reading it.  The proper fix would probably be to use locks.
            bool haveValidLeaderboard = true;  // Due to async updates, we don't always get a valid leaderboard.  Ensure we don't export any with missing info
            bool haveValidStatus = true; // Same as for leaderboard, ensure we don't export status if we are missing any information

            for (int i = 0; i < listViewParticipants.Items.Count; i++)
            {
                try
                {
                    if (haveValidLeaderboard)
                    {
                        int commanderPosition = Convert.ToInt32(listViewParticipants.Items[i].Text) - 1;
                        if (commanderPosition < 0) // Not a number, so add to bottom of leaderboard
                        {
                            commanderPosition = leaderboard.Length - 2; // Bottom row is padding (or empty)
                            while (!String.IsNullOrEmpty(leaderboard[commanderPosition]))
                                commanderPosition--; // Could potentially happen in the event of a tie
                        }
                        leaderboard[commanderPosition] = listViewParticipants.Items[i].SubItems[1].Text;
                        if (String.IsNullOrEmpty(leaderboard[commanderPosition]))
                            haveValidLeaderboard = false;
                    }
                    if (haveValidStatus)
                    {
                        string commanderStatus;
                        if (listViewParticipants.Items[i].SubItems[2].Text.StartsWith("->"))
                            if (checkBoxExportDistance.Checked)
                                commanderStatus = listViewParticipants.Items[i].SubItems[3].Text;
                            else
                                commanderStatus = listViewParticipants.Items[i].SubItems[2].Text;
                        else
                            commanderStatus = listViewParticipants.Items[i].SubItems[2].Text;
                        if (!String.IsNullOrEmpty(commanderStatus))
                            status.AppendLine(commanderStatus);
                        else
                            haveValidStatus = false;
                    }
                }
                catch { }
            }

            if (checkBoxClosestPlayerTarget.Checked)
                trackingTarget.AppendLine(FormLocator.ClosestCommander);
            else
                trackingTarget.AppendLine(FormLocator.GetLocator().TrackingTarget);

            if (haveValidLeaderboard)
            {
                if (checkBoxPaddingCharacters.Checked)
                    leaderboard[leaderboard.Length - 1] = new string(textBoxPaddingChar.Text[0], (int)numericUpDownLeaderboardPadding.Value);
                string participants = String.Join(Environment.NewLine, leaderboard);
                if (checkBoxExportLeaderboard.Checked && !_lastLeaderboardExport.Equals(participants))
                {
                    try
                    {
                        File.WriteAllText(textBoxExportLeaderboardFile.Text, participants);
                        _lastLeaderboardExport = participants;
                    }
                    catch { }
                }
            }

            if (haveValidStatus)
            {
                if (checkBoxExportStatus.Checked && !_lastStatusExport.Equals(status.ToString()))
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
            if (checkBoxExportTarget.Checked && !_lastTrackingTarget.Equals(trackingTarget.ToString()))
            {
                if (checkBoxPaddingCharacters.Checked)
                    trackingTarget.AppendLine(new string(textBoxPaddingChar.Text[0], (int)numericUpDownTargetPadding.Value));
                try
                {
                    File.WriteAllText(textBoxExportTargetFile.Text, trackingTarget.ToString());
                    _lastTrackingTarget = trackingTarget.ToString();
                }
                catch { }
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
                AddTrackedCommander(comboBoxAddCommander.Text, "Unknown");
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
            EDRaceStatus.EliminateOnDestruction = checkBoxEliminationOnDestruction.Checked;
            EDRaceStatus.AllowPitStops = checkBoxAllowPitstops.Checked;
            EDRaceStatus.EliminateOnShipFlight = checkBoxSRVRace.Checked;
            EDRaceStatus.Started = true;
            EDRaceStatus.StartTime = DateTime.Now;
            buttonStartRace.Enabled = false;
            buttonStopRace.Enabled = true;
            buttonRaceHistory.Enabled = true;
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
            EDRaceStatus.EliminateOnDestruction = checkBoxEliminationOnDestruction.Checked;
        }

        private void checkBoxSRVRace_CheckedChanged(object sender, EventArgs e)
        {
            EDRaceStatus.EliminateOnShipFlight = checkBoxSRVRace.Checked;
            checkBoxAllowPitstops.Enabled = checkBoxSRVRace.Checked;
        }

        private void checkBoxAllowPitstops_CheckedChanged(object sender, EventArgs e)
        {
            EDRaceStatus.AllowPitStops = checkBoxAllowPitstops.Checked;
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
