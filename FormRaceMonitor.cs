using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRVTracker
{
    public partial class FormRaceMonitor : Form
    {
        private string _routeFile = "";
        private EDRace _race = null;

        public FormRaceMonitor()
        {
            InitializeComponent();
            _race = new EDRace("", new EDRoute(""));
            CommanderWatcher.Start();
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
        }

        private void buttonAddAllOnline_Click(object sender, EventArgs e)
        {
            if (CommanderWatcher.OnlineCommanderCount == 0)
                return;

            listViewParticipants.BeginUpdate();
            listViewParticipants.Items.Clear();
            foreach (string commander in CommanderWatcher.GetCommanders())
            {
                ListViewItem item = new ListViewItem("-");
                item.SubItems.Add(commander);
                item.SubItems.Add("Unknown");
                item.SubItems.Add("0m");
                listViewParticipants.Items.Add(item);
            }
            listViewParticipants.EndUpdate();
        }
    }
}
