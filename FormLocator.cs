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
        private bool _commanderListShowing = false;
        private static Size _normalView = new Size(558, 174);
        private static Size _miniView = new Size(260, 60);
        private static int _commanderListHiddenWidth = 344;
        private static ulong _vrOverlayHandle = 0;
        private static HmdMatrix34_t _vrMatrix;
        //private static IntPtr? _intPtrOverlayImage = null;
        private double _trackedTargetDistance = double.MaxValue;
        private static EDEvent _closestCommander = null;
        private static double _closestCommanderDistance = double.MaxValue;
        private static FormLocator _activeLocator = null;
        private ConfigSaverClass _formConfig = null;
        private FormVRMatrixEditor _formVRMatrixTest = null;
        private byte[] _vrPanelImageBytes = null;
        private IntPtr _intPtrVROverlayImage;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public FormLocator()
        {
            InitializeComponent();
            CalculateWindowSizes();

            // Attach our form configuration saver
            _formConfig = new ConfigSaverClass(this, true);
            _formConfig.StoreControlInfo = false;
            _formConfig.SaveEnabled = true;
            _formConfig.RestorePreviousSize = false;
            _formConfig.RestoreFormValues();

            this.Size = _normalView;
            this.Width = _commanderListHiddenWidth;
            buttonUseCurrentLocation.Enabled = false;  // We'll enable it when we have a location
            CommanderWatcher.UpdateReceived += CommanderWatcher_UpdateReceived;
            FormTracker.CommanderLocationChanged += FormTracker_CommanderLocationChanged;
            InitVRMatrix();
            InitLocationCombo();
            locatorHUD1.SetAdditionalInfo("");
        }

        private void CalculateWindowSizes()
        {
            // Calculate window sizes
            int leftBound = buttonPlayers.Location.X + buttonPlayers.Width;
            _commanderListHiddenWidth = (this.Width - this.ClientRectangle.Width) + leftBound + ((groupBoxOtherCommanders.Left - leftBound) / 2);

            _miniView.Height = locatorHUD1.Height;
            _miniView.Width = locatorHUD1.Width;
            _normalView.Width = groupBoxOtherCommanders.Location.X + groupBoxOtherCommanders.Width + (this.Width - this.ClientRectangle.Width) + 6;
            _normalView.Height = groupBoxOtherCommanders.Location.Y + groupBoxOtherCommanders.Height + (this.Height - this.ClientRectangle.Height) + 6;
        }

        private void FormTracker_CommanderLocationChanged(object sender, EventArgs e)
        {
            UpdateTracking();
        }

        public static FormLocator GetLocator(bool focus = false)
        {
            if (_activeLocator == null || _activeLocator.IsDisposed)
            {
                _activeLocator = new FormLocator();
            }
            if (!_activeLocator.Visible)
                _activeLocator.Show();
            return _activeLocator;
        }

        private double DistanceBetween(EDLocation location1, EDLocation location2)
        {
            if (checkBoxIncludeAltitudeInDistanceCalculations.Checked)
                return EDLocation.DistanceBetweenIncludingAltitude(location1, location2);
            return EDLocation.DistanceBetween(location1, location2);
        }

        private void CommanderWatcher_UpdateReceived(object sender, EDEvent edEvent)
        {
            if (!edEvent.HasCoordinates() || edEvent.Commander.Equals(FormTracker.CommanderName))
                return;

            if (FormTracker.CurrentLocation != null)
            {
                // If we are tracking the closest commander to us, we need to check all updates and change our tracking target as necessary
                // We just check if the distance from this commander is closer than our currently tracked target

                double distanceToCommander = DistanceBetween(FormTracker.CurrentLocation, edEvent.Location());
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
                return "";
            }
        }

        public string TrackingTarget { get; private set; } = "";


        public void SetTarget(EDLocation targetLocation, string additionalInfo = "")
        {
            // Sets the tracking target
            _targetPosition = targetLocation;
            UpdateTrackingTarget(targetLocation.Name);
            locatorHUD1.SetAdditionalInfo(additionalInfo);
            DisplayTarget();
            UpdateTracking();
        }

        public void SetAdditionalInfo(string additionalInfo)
        {
            locatorHUD1.SetAdditionalInfo(additionalInfo);
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

            //locatorHUD1.SetSpeed(FormTracker.SpeedInMS);

            if (_targetPosition == null)
                return;

            bool displayChanged = false;
            try
            {
                double distance = DistanceBetween(FormTracker.CurrentLocation, _targetPosition);
                string d = locatorHUD1.SetDistance(distance);
                double bearing = EDLocation.BearingToLocation(FormTracker.CurrentLocation, _targetPosition);
                if (locatorHUD1.SetBearing((int)bearing, FormTracker.CurrentHeading))
                    displayChanged = true;
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
            {
                try
                {
                    UpdateVRLocatorImage();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Error updating VR image", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonShowHideTarget_Click(object sender, EventArgs e)
        {
            if (this.Height >= _normalView.Height)
            {
                // We are expanded, so shrink
                this.FormBorderStyle = FormBorderStyle.None;
                this.Height = locatorHUD1.Height;
                this.Width = locatorHUD1.Width;
            }
            else
            {
                //this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                this.Height = _normalView.Height;
                if (_commanderListShowing)
                    this.Width = _normalView.Width;
                else
                    this.Width = _commanderListHiddenWidth;
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
            if (this.Width == _commanderListHiddenWidth)
            {
                this.Width = _normalView.Width;
                _commanderListShowing = true;
                CommanderWatcher.Start($"http://{ServerAddress}:11938/DataCollator/status");
                UpdateAvailableCommanders();;
                CommanderWatcher.OnlineCountChanged += CommanderWatcher_OnlineCountChanged;
            }
            else
            {
                this.Width = _commanderListHiddenWidth;
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

            if (checkBoxEnableVRLocator.Checked)
                HideVRLocator();
        }

        private void UpdateTrackingTarget(string target)
        {
            Action action;
            TrackingTarget = target;

            if (_targetPosition==null)
            {
                EDEvent commanderEvent = CommanderWatcher.GetCommanderMostRecentEvent(target);
                if (commanderEvent != null)
                    _targetPosition = commanderEvent.Location();
            }

            locatorHUD1.SetTarget(target);
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
            DisplayTarget();
        }

        private void checkBoxEnableVRLocator_CheckedChanged(object sender, EventArgs e)
        {
            string initError = "";
            if (!checkBoxEnableVRLocator.Enabled)
                return;

            try
            {
                if (!Valve.VR.OpenVR.IsHmdPresent())
                {
                    checkBoxEnableVRLocator.Enabled = false;
                    checkBoxEnableVRLocator.Checked = false;
                    initError = "No HMD present";
                }
            }
            catch (Exception ex)
            {
                checkBoxEnableVRLocator.Enabled = false;
                checkBoxEnableVRLocator.Checked = false;
                initError = $"IsHmdPresent:{Environment.NewLine}{ex}";
            }

            if (checkBoxEnableVRLocator.Checked)
            {
                if (!ShowVRLocator(ref initError))
                    checkBoxEnableVRLocator.Checked = false;
                else
                    return;
            }

            if (!string.IsNullOrEmpty(initError))
            {
                MessageBox.Show(this, initError, "VR Initialisation Failed",  MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void UpdateVRLocatorImage()
        {
            Bitmap locatorPanel = locatorHUD1.GetLocatorPanelBitmap();
            //Bitmap locatorPanel = new Bitmap(locatorHUD1.Width, locatorHUD1.Height);
            //locatorHUD1.DrawToBitmap(locatorPanel, new Rectangle(0,0,locatorHUD1.Width, locatorHUD1.Height));
            if (locatorHUD1.PanelRequiresReset())
            {
                // For some reason, after 200 updates the OpenVR layer locks up
                // No idea why, so this horrible hack resets it
                string info="";
                HideVRLocator(false);
                ShowVRLocator(ref info, true);
            }
            bool needToAllocateMemory = _vrPanelImageBytes == null;
            _vrPanelImageBytes = BitmapToByte(locatorPanel);
            if (needToAllocateMemory)
                _intPtrVROverlayImage = Marshal.AllocHGlobal(_vrPanelImageBytes.Length);
            Marshal.Copy(_vrPanelImageBytes, 0, _intPtrVROverlayImage, _vrPanelImageBytes.Length);
            OpenVR.Overlay.SetOverlayRaw(_vrOverlayHandle, _intPtrVROverlayImage, (uint)locatorPanel.Width, (uint)locatorPanel.Height, 4);
        }

        private static void InitVRMatrix()
        {
            _vrMatrix = new HmdMatrix34_t();
            _vrMatrix.m0 = 0.7F;
            _vrMatrix.m1 = 0.0F;
            _vrMatrix.m2 = 0.0F;
            _vrMatrix.m3 = 0.5F; // x
            _vrMatrix.m4 = 0.0F;
            _vrMatrix.m5 = -1.0F;
            _vrMatrix.m6 = 0.0F;
            _vrMatrix.m7 = 1.5F; // y
            _vrMatrix.m8 = 0F;
            _vrMatrix.m9 = 0.0F;
            _vrMatrix.m10 = 0.0F;
            _vrMatrix.m11 = -1.5F; // -z
        }

        private bool ShowVRLocator(ref string info, bool SuppressMatrixWindow = false)
        {
            if (_vrOverlayHandle > 0)
                return true;
            try
            {
                if (!VRLocator.InitVR())
                {
                    info = "";
                    return false;
                }
            }
            catch (Exception ex)
            {
                info = $"InitVR: {Environment.NewLine}{ex}";
                return false;
            }

            try
            {
                OpenVR.Overlay.CreateOverlay(Guid.NewGuid().ToString(), "SRV Tracking", ref _vrOverlayHandle);
                locatorHUD1.ResetPanel();
            }
            catch (Exception ex)
            {
                info =$"CreateOverlay: {Environment.NewLine}{ex}";
                return false;
            }

            if (!SuppressMatrixWindow)
            {
                _formVRMatrixTest = new FormVRMatrixEditor(_vrOverlayHandle);
                _formVRMatrixTest.SetMatrix(ref _vrMatrix);
                _formVRMatrixTest.SetOverlayWidth(0.6f);
                _formVRMatrixTest.Show();
                _vrMatrix = _formVRMatrixTest.GetMatrix();
            }
            else
            {
                OpenVR.Overlay.SetOverlayTransformAbsolute(_vrOverlayHandle, Valve.VR.ETrackingUniverseOrigin.TrackingUniverseStanding, ref _vrMatrix);
                OpenVR.Overlay.SetOverlayWidthInMeters(_vrOverlayHandle, 0.6f);  // Need to change to keep track of width
            }
            
            UpdateVRLocatorImage();
            _ = OpenVR.Overlay.ShowOverlay(_vrOverlayHandle);           
            
            return true;
        }

        private void HideVRLocator(bool CloseMatrixWindow = true)
        {
            try
            {
                OpenVR.Overlay.DestroyOverlay(_vrOverlayHandle);
            }
            catch { }
            if (_vrPanelImageBytes != null)
            {
                Marshal.FreeHGlobal(_intPtrVROverlayImage);
                _vrPanelImageBytes = null;
            }
            _vrOverlayHandle = 0;
            if (_formVRMatrixTest != null && !_formVRMatrixTest.IsDisposed)
                _vrMatrix = _formVRMatrixTest.GetMatrix();
            if (CloseMatrixWindow)
            {
                if (_formVRMatrixTest != null && !_formVRMatrixTest.IsDisposed && _formVRMatrixTest.Visible)
                    _formVRMatrixTest.Close();
                _formVRMatrixTest = null;
            }
            VRLocator.ShutdownVr();
        }

        private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLocation.SelectedIndex < 0)
                return;

            if (comboBoxLocation.SelectedIndex==0)
            {
                // Show the location editor
                comboBoxLocation.SelectedIndex = -1;
                FormAddLocation formAddLocation = new FormAddLocation();
                EDLocation newLocation = formAddLocation.GetLocation(this);
                if (newLocation != null)
                {
                    LocationManager.AddLocation(newLocation);
                    comboBoxLocation.Items.Add(newLocation.Name);
                    comboBoxLocation.SelectedIndex = comboBoxLocation.Items.Count - 1;
                }
                formAddLocation.Dispose();
                return;
            }

            LocationManager locationManager = new LocationManager();
            if (locationManager.Locations.Count>0)
                foreach (EDLocation location in locationManager.Locations)
                    if (location.Name.Equals(comboBoxLocation.SelectedItem.ToString()))
                    {
                        _targetPosition = location;
                        UpdateTrackingTarget(location.Name);
                        DisplayTarget();
                        break;
                    }
            locationManager.Dispose();
        }

        private void InitLocationCombo()
        {
            comboBoxLocation.Items.Clear();
            comboBoxLocation.Items.Add("Add new location...");
            LocationManager locationManager = new LocationManager();
            if (locationManager.Locations.Count > 0)
                foreach (EDLocation location in locationManager.Locations)
                    comboBoxLocation.Items.Add(location.Name);
            locationManager.Dispose();
        }

        private void buttonAlwaysOnTop_Click(object sender, EventArgs e)
        {
            if (this.TopMost)
            {
                this.TopMost = false;
                buttonAlwaysOnTop.Image = Properties.Resources.PinnedItem_16x;
            }
            else
            {
                this.TopMost = true;
                buttonAlwaysOnTop.Image = Properties.Resources.Pushpin_16x;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBoxMoveForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void buttonOpenLocationEditor_Click(object sender, EventArgs e)
        {
            FormLocationEditor.GetFormLocationEditor().ShowWithBorder(this);
        }
    }


}
