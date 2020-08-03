﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using EDTracking;

namespace SRVTracker
{
    public partial class FormLocator : Form
    {
        private EDLocation _targetPosition = null;
        private WebClient _webClient = new WebClient();
        private string _trackingTarget = "";
        private bool _commanderListShowing = false;
        private static Size _normalView = new Size(364, 228);

        public FormLocator()
        {
            InitializeComponent();
            this.Width = _normalView.Width;
            buttonUseCurrentLocation.Enabled = false;  // We'll enable it when we have a location
            CommanderWatcher.UpdateReceived += CommanderWatcher_UpdateReceived;
        }

        private void CommanderWatcher_UpdateReceived(object sender, EDEvent edEvent)
        {
            if (!_trackingTarget.Equals(edEvent.Commander))
                return;

            if (edEvent.HasCoordinates)
            {
                _targetPosition = edEvent.Location;
                DisplayTarget();
                UpdateTracking();
            }
        }

        public static double PlanetaryRadius { get; set; } = 0;
        public static string ServerAddress { get; set; } = null;
        

        public void SetTarget(EDLocation targetLocation)
        {
            // Sets the tracking target
            _targetPosition = targetLocation;
            UpdateTrackingTarget(targetLocation.Name, false);
            DisplayTarget();
            UpdateTracking();
        }

        public void SetTarget(string commander)
        {
            // Sets the tracking target to the specified commander

            UpdateTrackingTarget(commander, true);
        }

        private void DisplayTarget()
        {
            if (_targetPosition == null)
                return;

            Action action;

            if (textBoxLatitude.Text != _targetPosition.Latitude.ToString())
            {
                action = new Action(() => { textBoxLatitude.Text = _targetPosition.Latitude.ToString(); });
                if (textBoxLatitude.InvokeRequired)
                    textBoxLatitude.Invoke(action);
                else
                    action();
            }

            if (textBoxLongitude.Text != _targetPosition.Longitude.ToString())
            {
                action = new Action(() => { textBoxLongitude.Text = _targetPosition.Longitude.ToString(); });
                if (textBoxLongitude.InvokeRequired)
                    textBoxLongitude.Invoke(action);
                else
                    action();
            }

            if (textBoxAltitude.Text != _targetPosition.Altitude.ToString())
            {
                action = new Action(() => { textBoxAltitude.Text = _targetPosition.Altitude.ToString(); });
                if (textBoxAltitude.InvokeRequired)
                    textBoxAltitude.Invoke(action);
                else
                    action();
            }
        }

        public void UpdateTracking(EDLocation CurrentLocation =null)
        {
            // Update our position
            Action action;

            if (FormTracker.CurrentLocation == null)
                return;

            if (!buttonUseCurrentLocation.Enabled)
            {
                action = new Action(() => { buttonUseCurrentLocation.Enabled = true; });
                if (buttonUseCurrentLocation.InvokeRequired)
                    buttonUseCurrentLocation.Invoke(action);
                else
                    action();
            }

            if (_targetPosition == null)
                return;

            try
            {
                string distanceUnit = "m";
                double distance = EDLocation.DistanceBetween(FormTracker.CurrentLocation, _targetPosition);
                if (distance>1000000)
                {
                    distance = distance / 1000000;
                    distanceUnit = "Mm";
                }
                else if (distance>1000)
                {
                    distance = distance / 1000;
                    distanceUnit = "km";
                }
                double bearing = EDLocation.BearingToLocation(FormTracker.CurrentLocation, _targetPosition);
                string d = $"{distance.ToString("0.0")}{distanceUnit}";
                string b = $"{Convert.ToInt32(bearing).ToString()}°";

                if (!labelDistance.Text.Equals(d))
                {
                    action = new Action(() => { labelDistance.Text = d; });
                    if (labelDistance.InvokeRequired)
                        labelDistance.Invoke(action);
                    else
                        action();
                }
                if (!labelHeading.Text.Equals(b))
                {
                    action = new Action(() => { labelHeading.Text = b; });
                    if (labelHeading.InvokeRequired)
                        labelHeading.Invoke(action);
                    else
                        action();
                }

            }
            catch { }
        }

        private void textBoxLongitude_TextChanged(object sender, EventArgs e)
        {
            TrySetDestination();
        }

        private void textBoxLatitude_TextChanged(object sender, EventArgs e)
        {          
            TrySetDestination();
        }

        private void textBoxAltitude_TextChanged(object sender, EventArgs e)
        {
            TrySetDestination();
        }

        private void TrySetDestination()
        {
            // This shouldn't ever be called cross-thread, so no need to check for invoke
            // If an error occurs while trying to create the coordinate, then we invalidate it
            // Once we have valid location, we change colour to signify that
            try
            {
                double latitude = Convert.ToDouble(textBoxLatitude.Text);
                double longitude = Convert.ToDouble(textBoxLongitude.Text);
                if (!String.IsNullOrEmpty(textBoxAltitude.Text))
                    _targetPosition = new EDLocation(latitude, longitude, Convert.ToDouble(textBoxAltitude.Text));
                else
                    _targetPosition = new EDLocation(latitude, longitude);
                //groupBoxDestination.BackColor = Color.Lime;
                return;
            }
            catch { }
            //groupBoxDestination.BackColor = System.Drawing.SystemColors.Control;
        }

        private void buttonShowHideTarget_Click(object sender, EventArgs e)
        {
            if (this.Height >= _normalView.Height)
            {
                // We are expanded, so shrink
                this.FormBorderStyle = FormBorderStyle.None;
                groupBoxBearing.Left = 0;
                groupBoxBearing.Top = 0;
                this.Height = groupBoxBearing.Height;
                this.Width = groupBoxBearing.Width;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                groupBoxBearing.Left = 12;
                groupBoxBearing.Top = 12;
                this.Height = _normalView.Height;
                if (_commanderListShowing)
                    this.Width = 575;
                else
                    this.Width = _normalView.Width;
            }
        }

        private void buttonUseCurrentLocation_Click(object sender, EventArgs e)
        {
            if (FormTracker.CurrentLocation == null)
                return;
          
            try
            {
                _targetPosition = FormTracker.CurrentLocation;
                UpdateTrackingTarget($"{_targetPosition.Longitude.ToString()} , {_targetPosition.Latitude.ToString()}", false);
                DisplayTarget();
            }
            catch { }
        }

        private void buttonPlayers_Click(object sender, EventArgs e)
        {
            if (this.Width <= _normalView.Width)
            {
                this.Width = 575;
                _commanderListShowing = true;
                CommanderWatcher.Start();
                UpdateAvailableCommanders();;
                CommanderWatcher.OnlineCountChanged += CommanderWatcher_OnlineCountChanged;
            }
            else
            {
                this.Width = _normalView.Width;
                _commanderListShowing = false;
                CommanderWatcher.OnlineCountChanged -= CommanderWatcher_OnlineCountChanged;
            }
        }

        private void UpdateAvailableCommanders()
        {
            // Show the list of online commanders (those available for tracking)

            string info = "No commanders found";
            Action action = new Action(() => {
                listBoxCommanders.Items.Clear();
                listBoxCommanders.Items.Add(info);
            });

            if (String.IsNullOrEmpty(ServerAddress))
                info = "Invalid server address";
            else
            {
                if (CommanderWatcher.OnlineCommanderCount>0)
                    action = new Action(() =>
                    {
                        listBoxCommanders.BeginUpdate();
                        int selectedIndex = listBoxCommanders.SelectedIndex;
                        listBoxCommanders.Items.Clear();
                        foreach (string commander in CommanderWatcher.GetCommanders())
                            listBoxCommanders.Items.Add(commander);
                        if (selectedIndex >= 0 && selectedIndex < listBoxCommanders.Items.Count)
                            listBoxCommanders.SelectedIndex = selectedIndex;
                        listBoxCommanders.EndUpdate();
                    });
            }

            if (listBoxCommanders.InvokeRequired)
                listBoxCommanders.Invoke(action);
            else
                action();
            
        }

        private void CommanderWatcher_OnlineCountChanged(object sender, EventArgs e)
        {
            UpdateAvailableCommanders();
        }

        private void listBoxCommanders_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonTrackCommander.Enabled = listBoxCommanders.SelectedIndex >= 0;
            try
            {
                if (((string)listBoxCommanders.SelectedItem).Equals(_trackingTarget))
                    buttonTrackCommander.Text = "Stop";
                else
                    buttonTrackCommander.Text = "Track";
            }
            catch { }
        }

        private void FormLocator_FormClosing(object sender, FormClosingEventArgs e)
        {
            // We use the locator for tracking, so don't let it close (except when quitting)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void UpdateTrackingTarget(string target, bool isTrackedTarget = true)
        {
            Action action;
            if (isTrackedTarget)
                _trackingTarget = target;
            else
                _trackingTarget = "";

            string bearingInfo = $"Bearing (tracking {target})";
            if (String.IsNullOrEmpty(target))
                bearingInfo = "Bearing (target not set)";

            if (String.IsNullOrEmpty(_trackingTarget))
            {
                if (buttonTrackCommander.Text.Equals("Stop"))
                {
                    action = new Action(() => { buttonTrackCommander.Text = "Track"; });
                    if (buttonTrackCommander.InvokeRequired)
                        buttonTrackCommander.Invoke(action);
                    else
                        action();
                }
            }
            if (!groupBoxBearing.Text.Equals(bearingInfo))
            {
                action = new Action(() => { groupBoxBearing.Text = bearingInfo; });
                if (groupBoxBearing.InvokeRequired)
                    groupBoxBearing.Invoke(action);
                else
                    action();
            }           
        }

        private void buttonTrackCommander_Click(object sender, EventArgs e)
        {
            //  If we have a valid commander to track, we start a timer to query their position every 250ms
            if (buttonTrackCommander.Text.Equals("Track"))
            {
                // Initiate tracking
                string target = "";
                try
                {
                    target = (string)listBoxCommanders.SelectedItem;
                }
                catch { }
                if (String.IsNullOrEmpty(target) || target.Equals("No commanders found"))
                    return;

                UpdateTrackingTarget(target);
            }
            else
            {
                // Stop tracking               
                UpdateTrackingTarget("");
            }
        }
    }
}
