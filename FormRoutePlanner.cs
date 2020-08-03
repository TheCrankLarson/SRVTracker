using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using EDTracking;

namespace SRVTracker
{
    public partial class FormRoutePlanner : Form
    {
        private string _saveFilename = "";
        private FormLocator _locatorForm = null;
        private EDRoute _route = null;

        public FormRoutePlanner(FormLocator formLocator = null)
        {
            InitializeComponent();
            _locatorForm = formLocator;
            if (_locatorForm == null)
                buttonSetAsTarget.Enabled = false;
            UpdateButtons();
            locationManager.SelectionChanged += LocationManager_SelectionChanged;
            _route = new EDRoute("");
        }

        private void LocationManager_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void buttonAddWaypoint_Click(object sender, EventArgs e)
        {
            try
            {
                if (locationManager.SelectedLocation != null)
                {
                    EDWaypoint waypoint = new EDWaypoint(locationManager.SelectedLocation);
                    listBoxWaypoints.Items.Add(waypoint);
                    _route.Waypoints.Add(waypoint);
                }
                    
            }
            catch { }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex > -1)
            {
                int selIndex = listBoxWaypoints.SelectedIndex;
                _route.Waypoints.RemoveAt(listBoxWaypoints.SelectedIndex);
                DisplayRoute();
                if (selIndex < listBoxWaypoints.Items.Count)
                    listBoxWaypoints.SelectedIndex = selIndex;
            }
        }

        private void buttonMoveWaypointUp_Click(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 1)
                return;

            try
            {
                int newSelectedIndex = listBoxWaypoints.SelectedIndex - 1;
                EDWaypoint waypointAbove = _route.Waypoints[listBoxWaypoints.SelectedIndex - 1];
                _route.Waypoints[listBoxWaypoints.SelectedIndex - 1] = _route.Waypoints[listBoxWaypoints.SelectedIndex];
                _route.Waypoints[listBoxWaypoints.SelectedIndex] = waypointAbove;
                DisplayRoute();
                listBoxWaypoints.SelectedIndex = newSelectedIndex;
            }
            catch { }
        }

        private void buttonMoveWaypointDown_Click(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex > listBoxWaypoints.SelectedIndex - 2)
                return;

            try
            {
                int newSelectedIndex = listBoxWaypoints.SelectedIndex + 1;
                EDWaypoint waypointBelow = _route.Waypoints[listBoxWaypoints.SelectedIndex + 1];
                _route.Waypoints[listBoxWaypoints.SelectedIndex + 1] = _route.Waypoints[listBoxWaypoints.SelectedIndex];
                _route.Waypoints[listBoxWaypoints.SelectedIndex] = waypointBelow;
                DisplayRoute();
                listBoxWaypoints.SelectedIndex = newSelectedIndex;
            }
            catch { }
        }

        private void buttonSaveRoute_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxRouteName.Text))
            {
                textBoxRouteName.Focus();
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = _saveFilename;
                saveFileDialog.Filter = "edroute files (*.edroute)|*.edroute|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = _saveFilename;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _route.SaveToFile(saveFileDialog.FileName);
                    }
                    catch { }
                }
            }
        }

        private void buttonLoadRoute_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = _saveFilename;
                openFileDialog.Filter = "edroute files (*.edroute)|*.edroute|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FileName = _saveFilename;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _route = EDRoute.LoadFromFile(openFileDialog.FileName);
                        textBoxRouteName.Text = _route.Name;
                        DisplayRoute();
                    }
                    catch { }
                }
            }
        }


        private void buttonSetAsTarget_Click(object sender, EventArgs e)
        {
            // Set the selected waypoint as the current target
            if ( _locatorForm == null || listBoxWaypoints.SelectedItem == null)
                return;
            _locatorForm.SetTarget(((EDWaypoint)listBoxWaypoints.SelectedItem).Location);
        }

        private void UpdateButtons()
        {
            buttonAddWaypoint.Enabled = locationManager.SelectedLocation != null;
            buttonDeleteWaypoint.Enabled = listBoxWaypoints.SelectedIndex >= 0;
            buttonMoveWaypointUp.Enabled = listBoxWaypoints.SelectedIndex > 0;
            buttonMoveWaypointDown.Enabled = listBoxWaypoints.SelectedIndex < listBoxWaypoints.Items.Count - 1;
            buttonSetAsTarget.Enabled = listBoxWaypoints.SelectedIndex >= 0;
            buttonSaveRoute.Enabled = !String.IsNullOrEmpty(textBoxRouteName.Text);
        }

        private void textBoxRouteName_TextChanged(object sender, EventArgs e)
        {
            buttonSaveRoute.Enabled = !String.IsNullOrEmpty(textBoxRouteName.Text);
            _route.Name = textBoxRouteName.Text;
        }

        private void listBoxWaypoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void DisplayRoute()
        {
            if (_route.Waypoints.Count == 0)
                return;

            listBoxWaypoints.BeginUpdate();
            listBoxWaypoints.Items.Clear();
            foreach (EDWaypoint waypoint in _route.Waypoints)
                listBoxWaypoints.Items.Add(waypoint.Name);
            listBoxWaypoints.EndUpdate();
        }
    }
}
