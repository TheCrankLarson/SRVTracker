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

        public FormRaceMonitor(FormLocator locatorForm = null)
        {
            InitializeComponent();
            _race = new EDRace("", new EDRoute(""));
            _locatorForm = locatorForm;
            listViewParticipants.ListViewItemSorter = new ListViewItemComparer();
            CommanderWatcher.Start();
            CommanderWatcher.UpdateReceived += CommanderWatcher_UpdateReceived;
            _racers = new Dictionary<string, ListViewItem>();
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
                        if (_race.Route.Waypoints.Count > 1)
                            _nextWaypoint = _race.Route.Waypoints[1];
                        else
                            _nextWaypoint = null;
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
        }

        private void buttonAddAllOnline_Click(object sender, EventArgs e)
        {
            if (CommanderWatcher.OnlineCommanderCount == 0)
                return;

            listViewParticipants.BeginUpdate();
            foreach (string commander in CommanderWatcher.GetCommanders())
            {
                if (!_racers.ContainsKey(commander))
                {
                    ListViewItem item = new ListViewItem("-");
                    item.SubItems.Add(commander);
                    item.SubItems.Add("Unknown");
                    item.SubItems.Add("NA");
                    item.SubItems[3].Tag = double.MaxValue;
                    listViewParticipants.Items.Add(item);
                    _racers.Add(commander, item);
                }
            }
            listViewParticipants.EndUpdate();
        }

        private void UpdateStatus(EDEvent edEvent)
        {
            // Update the status table

            if (edEvent.HasCoordinates)
            {
                if (_nextWaypoint != null)
                {
                    double distanceToWaypoint = EDLocation.DistanceBetween(edEvent.Location, _nextWaypoint.Location);
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
            int maxParticipantsLineLength = 0;
            int maxStatusLineLength = 0;
            for (int i = 0; i < listViewParticipants.Items.Count; i++)
            {
                try
                {
                    if (listViewParticipants.Items[i].SubItems[1].Text.Length > maxParticipantsLineLength)
                        maxParticipantsLineLength = listViewParticipants.Items[i].SubItems[1].Text.Length;
                    participants.AppendLine(listViewParticipants.Items[i].SubItems[1].Text);

                    if (listViewParticipants.Items[i].SubItems[2].Text.Length > maxStatusLineLength)
                        maxStatusLineLength = listViewParticipants.Items[i].SubItems[2].Text.Length;
                    status.AppendLine(listViewParticipants.Items[i].SubItems[2].Text);
                }
                catch { }
            }
            participants.AppendLine(new string('ÿ', maxParticipantsLineLength));
            status.AppendLine(new string('ÿ', maxStatusLineLength));

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
                return Convert.ToInt32(((ListViewItem)y).Text) - Convert.ToInt32(((ListViewItem)x).Text);
            }
            catch { }
            return 1;
        }
    }
}
