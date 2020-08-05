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
        private Dictionary<string, EDStatus> _racersStatus = null;
        private List<string> _eliminatedRacers = null;
        private EDWaypoint _nextWaypoint = null;
        private FormLocator _locatorForm = null;
        private string _lastLeaderboardExport = "";
        private string _lastStatusExport = "";
        private string _saveFilename = "";
        private bool _raceStarted = false;
        private bool _raceFinished = false;

        public FormRaceMonitor(FormLocator locatorForm = null)
        {
            InitializeComponent();
            groupBoxAddCommander.Visible = false;
            _race = new EDRace("", new EDRoute(""));
            _locatorForm = locatorForm;
            
            CommanderWatcher.Start();
            CommanderWatcher.UpdateReceived += CommanderWatcher_UpdateReceived;
            EDStatus.StatusChanged += EDStatus_StatusChanged;
            _racers = new Dictionary<string, System.Windows.Forms.ListViewItem>();
            AddTrackedCommanders();
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
            if (CommanderWatcher.OnlineCommanderCount == 0 || _raceStarted)
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
                            if (checkBoxAutoAddCommanders.Checked && _race.Route.Waypoints[0].LocationIsWithinWaypoint(CommanderWatcher.GetCommanderStatus(commander).Location))
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

            if (_raceStarted)
            {
                if (!_racersStatus.ContainsKey(edEvent.Commander))
                    return; // We're not tracking this commander
                _racersStatus[edEvent.Commander].UpdateStatus(edEvent);
            }

            if (!edEvent.HasCoordinates)
                return;

            if (!_raceStarted)
            {
                if (checkBoxAutoAddCommanders.Checked)
                    if (_race.Route.Waypoints.Count > 0)
                        if (_race.Route.Waypoints[0].LocationIsWithinWaypoint(edEvent.Location))
                            AddTrackedCommander(edEvent.Commander);
            }

            if (!_racers.ContainsKey(edEvent.Commander))
                return;

            if (_nextWaypoint != null)
            {
                double distanceToWaypoint = EDLocation.DistanceBetween(edEvent.Location, _nextWaypoint.Location) - _nextWaypoint.Radius;
                _racers[edEvent.Commander].SubItems[3].Tag = distanceToWaypoint; // Store in m for comparison
                Action action;
                string distanceToShow = "0";
                        
                if (distanceToWaypoint > 2500)
                    distanceToShow = $"{(distanceToWaypoint / 1000).ToString("#.0")}km";
                else
                    distanceToShow = $"{distanceToWaypoint.ToString("#.0")}m";
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
            for (int i = 0; i < listViewParticipants.Items.Count; i++)
            {
                try
                {
                    int commanderPosition = Convert.ToInt32(listViewParticipants.Items[i].Text)-1;
                    if (commanderPosition < 0) // Not a number, so add to bottom of leaderboard
                    {
                        commanderPosition = leaderboard.Length - 2;
                        while (!String.IsNullOrEmpty(leaderboard[commanderPosition]))
                            commanderPosition--; // Could potentially happen in the event of a tie
                    }
                    leaderboard[commanderPosition] = listViewParticipants.Items[i].SubItems[1].Text;
                    status.AppendLine(listViewParticipants.Items[i].SubItems[2].Text);
                }
                catch { }
            }
            if (numericUpDownPaddingChars.Value > 0)
            {
                leaderboard[leaderboard.Length-1] = new string(' ', (int)numericUpDownPaddingChars.Value);
                status.AppendLine(new string(' ', (int)numericUpDownPaddingChars.Value));
            }

            string participants = String.Join(Environment.NewLine, leaderboard);
            if (!_lastLeaderboardExport.Equals(participants))
            {
                try
                {
                    File.WriteAllText("Timing-Names.txt", participants);
                    _lastLeaderboardExport = participants;
                }
                catch { }
            }
            if (!_lastStatusExport.Equals(status.ToString()))
            {
                try
                {
                    File.WriteAllText("Timing-Stats.txt", status.ToString());
                    _lastStatusExport = status.ToString();
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

            _locatorForm?.SetTarget(listViewParticipants.SelectedItems[0].SubItems[1].Text);
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

        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                _race.Start = dateTimePickerStart.Value.Date.Add(dateTimePickerStartTime.Value.TimeOfDay);
            }
            catch { }
        }

        private void dateTimePickerStartTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                _race.Start = dateTimePickerStart.Value.Date.Add(dateTimePickerStartTime.Value.TimeOfDay);
            }
            catch { }
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
            _eliminatedRacers = new List<string>();
            _racersStatus = new Dictionary<string, EDStatus>();
            foreach (string commander in _racers.Keys)
                _racersStatus.Add(commander, new EDStatus(commander, _race.Route));
            _nextWaypoint = _race.Route.Waypoints[1];
            listBoxWaypoints.Refresh();
            _raceStarted = true;
            buttonStartRace.Enabled = false;
            buttonStopRace.Enabled = true;
        }

        private void buttonStopRace_Click(object sender, EventArgs e)
        {
            _raceFinished = true;
            buttonStopRace.Enabled = false;
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
            EDStatus.ShowDetailedStatus = checkBoxShowDetailedStatus.Checked;
        }

        private void comboBoxAddCommander_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonAddCommander_Click(null, null);
            comboBoxAddCommander.SelectedIndex = -1;
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
