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
using Valve.VR;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace SRVTracker
{
    public partial class FormLocator : Form
    {
        private EDLocation _targetPosition = null;
        private WebClient _webClient = new WebClient();
        private bool _commanderListShowing = false;
        private static Size _normalView = new Size(364, 228);
        private static ulong _vrOverlayHandle = 0;
        private static HmdMatrix34_t _vrMatrix;
        private static IntPtr? _intPtrOverlayImage = null;
        private static Bitmap _vrbitmap = null;
        private static Graphics _vrgraphics = null;
        private double _trackedTargetDistance = double.MaxValue;
        private static EDEvent _closestCommander = null;
        private static double _closestCommanderDistance = double.MaxValue;
        private static FormLocator _activeLocator = null;

        public FormLocator()
        {
            InitializeComponent();
            this.Width = _normalView.Width;
            buttonUseCurrentLocation.Enabled = false;  // We'll enable it when we have a location
            CommanderWatcher.UpdateReceived += CommanderWatcher_UpdateReceived;
            FormTracker.CommanderLocationChanged += FormTracker_CommanderLocationChanged;
            InitVRMatrix();
            InitLocationCombo();
        }

        private void FormTracker_CommanderLocationChanged(object sender, EventArgs e)
        {
            UpdateTracking();
        }

        public static FormLocator GetLocator()
        {
            if (_activeLocator != null)
            {
                if (_activeLocator.Visible)
                {
                    _activeLocator.Focus();
                    return _activeLocator;
                }
            }

            try
            {
                _activeLocator?.Dispose();
            }
            catch { }
            _activeLocator = new FormLocator();
            _activeLocator.Show();
            return _activeLocator;
        }

        private void CommanderWatcher_UpdateReceived(object sender, EDEvent edEvent)
        {
            if (!edEvent.HasCoordinates() || edEvent.Commander.Equals(FormTracker.ClientId))
                return;

            if (FormTracker.CurrentLocation != null)
            {
                // If we are tracking the closest commander to us, we need to check all updates and change our tracking target as necessary
                // We just check if the distance from this commander is closer than our currently tracked target

                double distanceToCommander = EDLocation.DistanceBetween(FormTracker.CurrentLocation, edEvent.Location());
                if (distanceToCommander == 0) // This is impossible, and just means we haven't got data on the tracked target
                    distanceToCommander = double.MaxValue;
                if (distanceToCommander < _closestCommanderDistance)
                {
                    _closestCommander = edEvent;
                    _closestCommanderDistance = distanceToCommander;
                }
                else if (edEvent.Commander == _closestCommander.Commander)
                    _closestCommanderDistance = distanceToCommander;
                if (checkBoxTrackClosest.Checked)
                {
                    // Switch tracking to this target
                    _targetPosition = edEvent.Location();
                    SetTarget(edEvent.Commander);
                    return;
                }
            }

            if (!TrackingTarget.Equals(edEvent.Commander))
                return;

            _targetPosition = edEvent.Location();
            DisplayTarget();
            UpdateTracking();
        }

        public static double PlanetaryRadius { get; set; } = 0;
        public static string ServerAddress { get; set; } = null;
        public static string ClosestCommander
        {
            get { 
                if (_closestCommander!=null)
                    return _closestCommander.Commander;
                return null;
            }
        }

        public string TrackingTarget { get; private set; } = "";


        public void SetTarget(EDLocation targetLocation)
        {
            // Sets the tracking target
            _targetPosition = targetLocation;
            UpdateTrackingTarget(targetLocation.Name);
            DisplayTarget();
            UpdateTracking();
        }

        public void SetTarget(string commander)
        {
            // Sets the tracking target to the specified commander

            UpdateTrackingTarget(commander);
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

        public void UpdateTracking()
        {
            // Update our position
            if (FormTracker.CurrentLocation == null)
                return;

            Action action;
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

            bool displayChanged = false;
            try
            {
                string d;
                double distance = EDLocation.DistanceBetween(FormTracker.CurrentLocation, _targetPosition);
                if (distance>1000000)
                {
                    d = $"({(distance / 1000000).ToString("0.0")}Mm";
                }
                else if (distance>1000)
                {
                    d = $"{(distance / 1000).ToString("0.0")}km";
                }
                else
                    d = $"{distance.ToString("0.0")}m";
                double bearing = EDLocation.BearingToLocation(FormTracker.CurrentLocation, _targetPosition);
                string b = $"{Convert.ToInt32(bearing).ToString()}°";
                
                if (!labelDistance.Text.Equals(d))
                {
                    displayChanged = true;
                    action = new Action(() => { labelDistance.Text = d; });
                    if (labelDistance.InvokeRequired)
                        labelDistance.Invoke(action);
                    else
                        action();
                }
                if (!labelHeading.Text.Equals(b))
                {
                    displayChanged = true;
                    action = new Action(() => { labelHeading.Text = b; });
                    if (labelHeading.InvokeRequired)
                        labelHeading.Invoke(action);
                    else
                        action();
                }

                if (checkBoxTrackClosest.Checked)
                    _trackedTargetDistance = distance;
            }
            catch { }
            if (displayChanged && checkBoxEnableVRLocator.Checked)
                UpdateVRLocatorImage();
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
                UpdateTrackingTarget($"{_targetPosition.Longitude.ToString()} , {_targetPosition.Latitude.ToString()}");
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
                CommanderWatcher.Start($"http://{ServerAddress}:11938/DataCollator/status");
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
                if (((string)listBoxCommanders.SelectedItem).Equals(TrackingTarget))
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

            if (_intPtrOverlayImage != null)
                HideVRLocator();
        }

        private void UpdateTrackingTarget(string target)
        {
            Action action;
            TrackingTarget = target;


            string bearingInfo = $"Bearing (tracking {target})";
            if (String.IsNullOrEmpty(target))
                bearingInfo = "Bearing (target not set)";

            if (String.IsNullOrEmpty(TrackingTarget))
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

                if (checkBoxTrackClosest.Checked)
                    checkBoxTrackClosest.Checked = false;
                UpdateTrackingTarget(target);
            }
            else
            {
                // Stop tracking               
                UpdateTrackingTarget("");
            }
        }

        private void checkBoxEnableVRLocator_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Valve.VR.OpenVR.IsHmdPresent())
                {
                    checkBoxEnableVRLocator.Checked = false;
                    checkBoxEnableVRLocator.Enabled = false;
                    return;
                }
            }
            catch
            {
                checkBoxEnableVRLocator.Checked = false;
                checkBoxEnableVRLocator.Enabled = false;
                return;
            }

            if (checkBoxEnableVRLocator.Checked)
            {
                if (!ShowVRLocator())
                    checkBoxEnableVRLocator.Checked = false;
                return;
            }
            
            HideVRLocator();
        }

        public static byte[] BitmapToByte(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                return ms.ToArray();
            }
        }

        private void UpdateLocatorBitmap()
        {
            // Generate and return a bitmap to be used as overlay

            if (_vrbitmap == null)
            {
                Size bitmapSize = new Size(800, 320);
                _vrbitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            }

            
            if (_vrgraphics == null)
            {
                _vrgraphics = Graphics.FromImage(_vrbitmap);
                _vrgraphics.CompositingQuality = CompositingQuality.HighQuality;
                _vrgraphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                _vrgraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                _vrgraphics.SmoothingMode = SmoothingMode.HighQuality;
                _vrgraphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            }

            // Black background
            _vrgraphics.FillRectangle(new SolidBrush(Color.Black), _vrgraphics.ClipBounds);

            // Tracking target
            Brush directionBrush = new SolidBrush(Color.White);
            Brush targetBrush = new SolidBrush(Color.Green);
            Font font = new Font("Arial", 24);
            _vrgraphics.DrawString(TrackingTarget, font, targetBrush, new PointF(0, 80));

            // Bearing
            font.Dispose();
            font = new Font("Arial", 56);
            //SizeF textSize = graphics.MeasureString(labelHeading.Text, font);
            _vrgraphics.DrawString(labelHeading.Text, font, directionBrush, new PointF(80, 120));

            // Distance
            _vrgraphics.DrawString(labelDistance.Text, font, directionBrush, new PointF(320, 120));
            font.Dispose();
            _vrgraphics.Save();
            directionBrush.Dispose();
            targetBrush.Dispose();
        }

        private void UpdateVRLocatorImage()
        {
            UpdateLocatorBitmap();
            byte[] imgBytes = BitmapToByte(_vrbitmap);
            if (_intPtrOverlayImage == null)
                _intPtrOverlayImage = Marshal.AllocHGlobal(imgBytes.Length);
            Marshal.Copy(imgBytes, 0, _intPtrOverlayImage.Value, imgBytes.Length);

            OpenVR.Overlay.SetOverlayRaw(_vrOverlayHandle, _intPtrOverlayImage.Value, (uint)_vrbitmap.Width, (uint)_vrbitmap.Height, 4);
        }

        private static void InitVRMatrix()
        {
            _vrMatrix = new HmdMatrix34_t();
            _vrMatrix.m0 = 0.7F;
            _vrMatrix.m1 = 0.0F;
            _vrMatrix.m2 = 0.0F;
            _vrMatrix.m3 = 0.42F;
            _vrMatrix.m4 = 0.0F;
            _vrMatrix.m5 = -1.0F;
            _vrMatrix.m6 = 0.0F;
            _vrMatrix.m7 = 0.78F;
            _vrMatrix.m8 = 0F;
            _vrMatrix.m9 = 0.0F;
            _vrMatrix.m10 = 0.0F;
            _vrMatrix.m11 = -0.1F;
        }

        private bool ShowVRLocator()
        {
            try
            {
                if (!FormTracker.InitVR() || (_vrOverlayHandle > 0))
                    return false;
            }
            catch
            {
                return false;
            }

            try
            {
                OpenVR.Overlay.CreateOverlay("overlaySRVTracker", "SRV Tracking", ref _vrOverlayHandle);
            }
            catch
            {
                return false;
            }
            OpenVR.Overlay.SetOverlayWidthInMeters(_vrOverlayHandle, 0.8f);
            UpdateVRLocatorImage();

            var error = OpenVR.Overlay.ShowOverlay(_vrOverlayHandle);

            OpenVR.Overlay.SetOverlayTransformAbsolute(_vrOverlayHandle, Valve.VR.ETrackingUniverseOrigin.TrackingUniverseStanding, ref _vrMatrix);
            
            /*
            FormVRMatrixTest formVRMatrixTest = new FormVRMatrixTest(_vrOverlayHandle);
            formVRMatrixTest.SetMatrix(ref _vrMatrix);
            formVRMatrixTest.Show();
            */
            return true;
        }

        private void HideVRLocator()
        {
            try
            {
                OpenVR.Overlay.DestroyOverlay(_vrOverlayHandle);
            }
            catch { }
            _vrOverlayHandle = 0;
            if (_intPtrOverlayImage != null)
                Marshal.FreeHGlobal((IntPtr)_intPtrOverlayImage);
            if (_vrgraphics != null)
            {
                _vrgraphics.Dispose();
                _vrgraphics = null;
            }
            if (_vrbitmap != null)
            {
                _vrbitmap.Dispose();
                _vrbitmap = null;
            }
            _intPtrOverlayImage = null;
        }

        private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLocation.SelectedIndex < 0)
                return;

            LocationManager locationManager = new LocationManager();
            if (locationManager.Locations.Count>0)
                foreach (EDLocation location in locationManager.Locations)
                    if (location.Name.Equals(comboBoxLocation.SelectedItem.ToString()))
                    {
                        _targetPosition = location;
                        UpdateTrackingTarget(location.Name);
                        break;
                    }
            locationManager.Dispose();
        }

        private void InitLocationCombo()
        {
            comboBoxLocation.Items.Clear();
            LocationManager locationManager = new LocationManager();
            if (locationManager.Locations.Count > 0)
                foreach (EDLocation location in locationManager.Locations)
                    comboBoxLocation.Items.Add(location.Name);
            locationManager.Dispose();
        }
    }
}
