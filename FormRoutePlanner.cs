﻿using System;
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
        private EDLocation _lastRecordedLocation = null;

        public FormRoutePlanner(FormLocator formLocator = null)
        {
            InitializeComponent();
            _locatorForm = formLocator;
            locationManager.LocatorForm = _locatorForm;
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
            if (listBoxWaypoints.SelectedIndex == listBoxWaypoints.Items.Count-1)
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

        private void buttonSaveRouteAs_Click(object sender, EventArgs e)
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
                        _saveFilename = saveFileDialog.FileName;
                        buttonSaveRoute.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, $"Error saving file: {ex.Message}", "File not saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                        _saveFilename = openFileDialog.FileName;
                        UpdateButtons();
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
            try
            {
                _locatorForm.SetTarget(_route.Waypoints[listBoxWaypoints.SelectedIndex].Location);
            }
            catch { }
        }

        private void UpdateButtons()
        {
            buttonAddWaypoint.Enabled = locationManager.SelectedLocation != null;
            buttonDeleteWaypoint.Enabled = listBoxWaypoints.SelectedIndex >= 0;
            buttonMoveWaypointUp.Enabled = listBoxWaypoints.SelectedIndex > 0;
            buttonMoveWaypointDown.Enabled = listBoxWaypoints.SelectedIndex < listBoxWaypoints.Items.Count - 1;
            buttonSetAsTarget.Enabled = listBoxWaypoints.SelectedIndex >= 0;
            buttonSaveRouteAs.Enabled = !String.IsNullOrEmpty(textBoxRouteName.Text);
            buttonSaveRoute.Enabled = buttonSaveRouteAs.Enabled && !(String.IsNullOrEmpty(_saveFilename));
        }

        private void textBoxRouteName_TextChanged(object sender, EventArgs e)
        {
            buttonSaveRouteAs.Enabled = !String.IsNullOrEmpty(textBoxRouteName.Text);
            buttonSaveRoute.Enabled = !String.IsNullOrEmpty(_saveFilename) && !String.IsNullOrEmpty(textBoxRouteName.Text);
            _route.Name = textBoxRouteName.Text;
        }

        private void listBoxWaypoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
            if (listBoxWaypoints.SelectedIndex >= 0)
            {
                numericUpDownRadius.Value = (decimal)_route.Waypoints[listBoxWaypoints.SelectedIndex].Radius;
                if (!numericUpDownRadius.Enabled)
                    numericUpDownRadius.Enabled = true;
            }
            else
                numericUpDownRadius.Enabled = false;
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

        private void numericUpDownRadius_ValueChanged(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex > -1)
                _route.Waypoints[listBoxWaypoints.SelectedIndex].Radius = (double)numericUpDownRadius.Value;
        }

        private void buttonSaveRoute_Click(object sender, EventArgs e)
        {
            try
            {
                _route.SaveToFile(_saveFilename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error saving file: {ex.Message}", "File not saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxRadius_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRadius.Checked && !numericUpDownRadius.Enabled)
                numericUpDownRadius.Enabled = true;
            else if (!checkBoxRadius.Checked && numericUpDownRadius.Enabled)
                numericUpDownRadius.Enabled = false;
        }

        private void AddCurrentLocationAsWaypoint()
        {
            if (FormTracker.CurrentLocation == null)
                return;

            _lastRecordedLocation = FormTracker.CurrentLocation;
            EDWaypoint waypoint = new EDWaypoint(_lastRecordedLocation, DateTime.Now, Convert.ToDouble(numericUpDownRadius.Value));
            waypoint.Location.Name = $"{waypoint.Location.Latitude:0.000}  {waypoint.Location.Longitude:0.000}";
            _route.Waypoints.Add(waypoint);
            Action action = new Action(() =>
            {
                listBoxWaypoints.Items.Add(waypoint.Name);
            });
            if (listBoxWaypoints.InvokeRequired)
                listBoxWaypoints.Invoke(action);
            else
                action();
        }

        private void buttonStartRecording_Click(object sender, EventArgs e)
        {
            buttonStartRecording.Enabled = false;
            buttonStopRecording.Enabled = true;
            AddCurrentLocationAsWaypoint();
            FormTracker.CommanderLocationChanged += FormTracker_CommanderLocationChanged;
        }

        private void FormTracker_CommanderLocationChanged(object sender, EventArgs e)
        {
            if (EDLocation.DistanceBetween(FormTracker.CurrentLocation, _lastRecordedLocation)>=Convert.ToDouble(numericUpDownRecordDistance.Value))
                AddCurrentLocationAsWaypoint();
        }

        private void buttonStopRecording_Click(object sender, EventArgs e)
        {
            FormTracker.CommanderLocationChanged -= FormTracker_CommanderLocationChanged;
            AddCurrentLocationAsWaypoint();
            buttonStartRecording.Enabled = true;
            buttonStopRecording.Enabled = false;
        }
    }
}
