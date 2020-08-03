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

namespace SRVTracker
{
    public partial class FormRaceMonitor : Form
    {
        private string _routeFile = "";
        private EDRace _race = null;
        private Dictionary<string, ListViewItem> _racers = null;
        private EDWaypoint _nextWaypoint = null;
        private FormLocator _locatorForm = null;

        public FormRaceMonitor(FormLocator locatorForm = null)
        {
            InitializeComponent();
            _race = new EDRace("", new EDRoute(""));
            _locatorForm = locatorForm;
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
                    item.SubItems.Add("0m");
                    item.SubItems[3].Tag = (double)0;
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
                    if (distanceToWaypoint > 2500)
                        action = new Action(() => { _racers[edEvent.Commander].SubItems[3].Text = $"{(distanceToWaypoint / 1000).ToString("#.0")}km"; });
                    else
                        action = new Action(() => { _racers[edEvent.Commander].SubItems[3].Text = $"{distanceToWaypoint.ToString("#.0")}m";});
                    if (listViewParticipants.InvokeRequired)
                        listViewParticipants.Invoke(action);
                    else
                        action();

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
                                if (!item.Text.Equals(i.ToString()))
                                    item.Text = (i+1).ToString();
                            }
                            else
                                item.Text = "-";
                        }
                    });
                    if (listViewParticipants.InvokeRequired)
                        listViewParticipants.Invoke(action);
                    else
                        action();
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

            _locatorForm?.SetTarget(listViewParticipants.SelectedItems[0].SubItems[1].Text);
        }
    }
}
