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
using System.Windows.Forms;
using EDTracking;

namespace SRVTracker
{
    public partial class FormRaceMonitor : Form
    {
        private string _routeFile = "";
        private EDRace _race = null;
        private Dictionary<string, ListViewItem> _racers = null;
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
            _race = new EDRace("", new EDRoute(""));
            _locatorForm = locatorForm;
            
            CommanderWatcher.Start();
            CommanderWatcher.UpdateReceived += CommanderWatcher_UpdateReceived;
            _racers = new Dictionary<string, ListViewItem>();
            AddTrackedCommanders();
        }

        private void CommanderWatcher_UpdateReceived(object sender, EDEvent edEvent)
        {
            if (!_racers.ContainsKey(edEvent.Commander))
                return;

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

            listBoxWaypoints.BeginUpdate();
            listBoxWaypoints.Items.Clear();
            foreach (EDWaypoint waypoint in _race.Route.Waypoints)
                listBoxWaypoints.Items.Add(waypoint.Name);
            listBoxWaypoints.EndUpdate();

            if (_race.Route.Waypoints.Count > 1)
                _nextWaypoint = _race.Route.Waypoints[1];
            else
                _nextWaypoint = null;
            if (_race.Route.Waypoints.Count > 0)
            {
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
                            ListViewItem item = new ListViewItem("-");
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

        private void AddTrackedCommander(string commander)
        {
            if (_racers.ContainsKey(commander))
                return;

            Action action = new Action(() =>
            {
                listViewParticipants.BeginUpdate();

                ListViewItem item = new ListViewItem("-");
                item.SubItems.Add(commander);
                item.SubItems.Add("Ready");
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

        private void UpdateStatus(EDEvent edEvent)
        {
            // Update the status table

            if (edEvent.HasCoordinates)
            {
                if (!_raceStarted && _race.Route.Waypoints.Count > 0)
                {
                    if (_race.Route.Waypoints[0].LocationIsWithinWaypoint(edEvent.Location))
                        AddTrackedCommander(edEvent.Commander);
                }

                if (!_racers.ContainsKey(edEvent.Commander))
                    return; // We're not tracking this commander

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
                    foreach (ListViewItem item in _racers.Values)
                        distancesToWaypoint.Add((double)item.SubItems[3].Tag);
                    distancesToWaypoint.Sort();

                    action = new Action(() =>
                    {
                        listViewParticipants.BeginUpdate();
                        foreach (ListViewItem item in _racers.Values)
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


                //Task.Run(new Action(() => { ExportLeaderboard(); }));
        }

        private void ExportLeaderboard()
        {
            // Export the current leaderboard
            StringBuilder participants = new StringBuilder();
            StringBuilder status = new StringBuilder();

            for (int i = 0; i < listViewParticipants.Items.Count; i++)
            {
                try
                {
                    participants.AppendLine(listViewParticipants.Items[i].SubItems[1].Text);
                    status.AppendLine(listViewParticipants.Items[i].SubItems[2].Text);
                }
                catch { }
            }
            if (numericUpDownPaddingChars.Value > 0)
            {
                participants.AppendLine(new string(' ', (int)numericUpDownPaddingChars.Value));
                status.AppendLine(new string(' ', (int)numericUpDownPaddingChars.Value));
            }

            if (!_lastLeaderboardExport.Equals(participants.ToString()))
            {
                try
                {
                    File.WriteAllText("Timing-Names.txt", participants.ToString());
                    _lastLeaderboardExport = participants.ToString();
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
                return Convert.ToInt32(((ListViewItem)x).SubItems[0].Text) - Convert.ToInt32(((ListViewItem)y).SubItems[0].Text);
            }
            catch { }
            return 1;
        }
    }
}
