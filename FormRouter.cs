﻿using EDTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRVTracker
{
    public partial class FormRouter : Form
    {
        static Size _fullSize = new Size(489, 297);
        static Size _miniSize = new Size(313, 291);
        private EDLocation _lastLoggedLocation = null;
        private int _nextWaypoint = 0;
        private EDRoute _route = null;
        private string _saveFileName = "";
        private FormTracker _formTracker = null; // We need a reference to the tracker so that we can start tracking if necessary

        public FormRouter(FormTracker formTracker)
        {
            InitializeComponent();
            this.Size = _miniSize;
            _route = new EDRoute();
            _formTracker = formTracker;
            FormTracker.CommanderLocationChanged += FormTracker_CommanderLocationChanged;
            buttonStop.Enabled = false;
        }

        private void DisplayRoute()
        {
            Action action = new Action(() =>
            {
                listBoxWaypoints.BeginUpdate();
                listBoxWaypoints.Items.Clear();
                for (int i = 0; i < _route.Waypoints.Count; i++)
                    listBoxWaypoints.Items.Add(_route.Waypoints[i].Name);
                listBoxWaypoints.EndUpdate();
            });
            if (listBoxWaypoints.InvokeRequired)
                listBoxWaypoints.Invoke(action);
            else
                action();
        }

        private void FormTracker_CommanderLocationChanged(object sender, EventArgs e)
        {
            if (buttonStartRecording.Enabled)
                return;

            if (_lastLoggedLocation==null)
            {
                _lastLoggedLocation = FormTracker.CurrentLocation;
                return;
            }

            if (buttonPlay.Enabled)
            {
                // We are currently tracking
                if (_route.Waypoints[_nextWaypoint].LocationIsWithinWaypoint(FormTracker.CurrentLocation))
                {
                    // Arrived at the waypoint, target the next
                    _nextWaypoint++;
                    if (_nextWaypoint >= _route.Waypoints.Count)
                    {
                        // End of route
                        buttonStop.Enabled = false;
                        buttonStartRecording.Enabled = true;
                    }
                    else
                    {
                        FormLocator.GetLocator().SetTarget(_route.Waypoints[_nextWaypoint].Location);
                        Action action = new Action(() =>
                        {
                            listBoxWaypoints.SelectedIndex = _nextWaypoint;
                        });
                        if (listBoxWaypoints.InvokeRequired)
                            listBoxWaypoints.Invoke(action);
                        else
                            action();
                    }
                }
                return;
            }

            if (EDLocation.DistanceBetween(_lastLoggedLocation, FormTracker.CurrentLocation) >= (double)numericUpDownRecordDistance.Value)
            {
                _lastLoggedLocation = FormTracker.CurrentLocation;
                AddLocationToRoute(_lastLoggedLocation);
            }
        }

        private void AddLocationToRoute(EDLocation location)
        {
            EDWaypoint waypoint = new EDWaypoint(location, DateTime.Now, (double)numericUpDownRadius.Value);
            if (checkBoxRadius.Checked)
                waypoint.Radius = (double)numericUpDownRadius.Value;
            else
                waypoint.Radius = 1;

            if (checkBoxTestAltitude.Checked)
            {
                if (radioButtonAboveAltitude.Checked)
                    waypoint.AltitudeTest = 1;
                else
                    waypoint.AltitudeTest = -1;
            }
            waypoint.Altitude = (double)numericUpDownAltitude.Value;
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

        private void buttonStopRecording_Click(object sender, EventArgs e)
        {
            buttonStartRecording.Enabled = true;
            buttonPlay.Enabled = true;
            buttonStop.Enabled = false;
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if (this.Size == _miniSize)
                this.Size = _fullSize;
            else
                this.Size = _miniSize;
        }

        private void buttonStartRecording_Click(object sender, EventArgs e)
        {
            buttonStop.Enabled = true;
            buttonPlay.Enabled = false;
            buttonStartRecording.Enabled = false;
            _formTracker.StartTracking();
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
                saveFileDialog.InitialDirectory = _saveFileName;
                saveFileDialog.Filter = "edroute files (*.edroute)|*.edroute|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = _saveFileName;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _route.SaveToFile(saveFileDialog.FileName);
                        _saveFileName = saveFileDialog.FileName;
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
                openFileDialog.InitialDirectory = _saveFileName;
                openFileDialog.Filter = "edroute files (*.edroute)|*.edroute|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FileName = _saveFileName;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _route = EDRoute.LoadFromFile(openFileDialog.FileName);
                        textBoxRouteName.Text = _route.Name;
                        _saveFileName = openFileDialog.FileName;
                        buttonSaveRoute.Enabled = true;
                        DisplayRoute();
                    }
                    catch { }
                }
            }
        }

        private void buttonSaveRoute_Click(object sender, EventArgs e)
        {
            try
            {
                _route.SaveToFile(_saveFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error saving file: {ex.Message}", "File not saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSetAsTarget_Click(object sender, EventArgs e)
        {
            // Set the selected waypoint as the current target
            if (listBoxWaypoints.SelectedItem == null)
                return;
            try
            {
                FormLocator.GetLocator().SetTarget(_route.Waypoints[listBoxWaypoints.SelectedIndex].Location);
            }
            catch { }
        }

        private void listBoxWaypoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            EDWaypoint waypoint = _route.Waypoints[listBoxWaypoints.SelectedIndex];
            textBoxWaypointName.Text = waypoint.Name;
            numericUpDownRadius.Value = (decimal)waypoint.Radius;
            numericUpDownAltitude.Value = (decimal)waypoint.Altitude;
            checkBoxTestAltitude.Checked = waypoint.AltitudeTest != 0;
            if (checkBoxTestAltitude.Checked)
            {
                radioButtonAboveAltitude.Checked = waypoint.AltitudeTest == 1;
            }
            else
            {
                radioButtonAboveAltitude.Enabled = false;
                radioButtonBelowAltitude.Enabled = false;
            }
        }

        private void checkBoxRadius_CheckedChanged(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0)
                return;

            if (!checkBoxRadius.Checked)
            {
                _route.Waypoints[listBoxWaypoints.SelectedIndex].Radius = 1;
                numericUpDownRadius.Enabled = false;
            }
            else
                _route.Waypoints[listBoxWaypoints.SelectedIndex].Radius = (double)numericUpDownRadius.Value;
        }

        private void numericUpDownRadius_ValueChanged(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0)
                return;
            _route.Waypoints[listBoxWaypoints.SelectedIndex].Radius = (double)numericUpDownRadius.Value;
        }

        private void numericUpDownAltitude_ValueChanged(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0)
                return;
            _route.Waypoints[listBoxWaypoints.SelectedIndex].Altitude = (double)numericUpDownAltitude.Value;
        }

        private void radioButtonAboveAltitude_CheckedChanged(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0)
                return;
            _route.Waypoints[listBoxWaypoints.SelectedIndex].AltitudeTest = 1;
        }

        private void radioButtonBelowAltitude_CheckedChanged(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0)
                return;
            _route.Waypoints[listBoxWaypoints.SelectedIndex].AltitudeTest = -1;
        }

        private void checkBoxTestAltitude_CheckedChanged(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0)
                return;

            if (checkBoxTestAltitude.Checked)
            {
                if (!radioButtonAboveAltitude.Enabled)
                {
                    radioButtonAboveAltitude.Enabled = true;
                    radioButtonBelowAltitude.Enabled = true;
                    numericUpDownAltitude.Enabled = true;
                }
                if (radioButtonAboveAltitude.Checked)
                    _route.Waypoints[listBoxWaypoints.SelectedIndex].AltitudeTest = 1;
                else
                    _route.Waypoints[listBoxWaypoints.SelectedIndex].AltitudeTest = -1;
            }
            else
            {
                if (radioButtonAboveAltitude.Enabled)
                {
                    radioButtonAboveAltitude.Enabled = false;
                    radioButtonBelowAltitude.Enabled = false;
                    numericUpDownAltitude.Enabled = false;
                }
                _route.Waypoints[listBoxWaypoints.SelectedIndex].AltitudeTest = 0;
            }
        }

        private void textBoxWaypointName_TextChanged(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0)
                return;
            _route.Waypoints[listBoxWaypoints.SelectedIndex].Name = textBoxWaypointName.Text;
            int selectedIndex = listBoxWaypoints.SelectedIndex;
            DisplayRoute();
            listBoxWaypoints.SelectedIndex = selectedIndex;
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            _nextWaypoint = 0;
            if (buttonStartRecording.Enabled)
            {
                if (listBoxWaypoints.SelectedIndex > 0)
                {
                    if (MessageBox.Show($"Start tracking from currently selected waypoint?{Environment.NewLine}(Choose no to start from the first waypoint)", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        _nextWaypoint = listBoxWaypoints.SelectedIndex;
                }
            }
            else
                _nextWaypoint = listBoxWaypoints.SelectedIndex;
            buttonStartRecording.Enabled = false;
            buttonStop.Enabled = true;
            _formTracker.StartTracking();
            FormLocator.GetLocator().SetTarget(_route.Waypoints[_nextWaypoint].Location);
        }

        private void buttonDeleteWaypoint_Click(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0)
                return;

            _route.Waypoints.RemoveAt(listBoxWaypoints.SelectedIndex);
            listBoxWaypoints.Items.RemoveAt(listBoxWaypoints.SelectedIndex);
        }

        private void buttonAddWaypoint_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddCurrentLocation_Click(object sender, EventArgs e)
        {
            if (FormTracker.CurrentLocation != null)
                AddLocationToRoute(FormTracker.CurrentLocation);
        }

        private void textBoxRouteName_TextChanged(object sender, EventArgs e)
        {
            _route.Name = textBoxRouteName.Text;
        }
    }
}