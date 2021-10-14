using System;
using System.Drawing;
using System.Windows.Forms;
using EDTracking;
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
        //private static ulong _vrOverlayHandle = 0;
        //private static HmdMatrix34_t _vrMatrix;
        //private static IntPtr? _intPtrOverlayImage = null;
        private double _trackedTargetDistance = double.MaxValue;
        private static EDEvent _closestCommander = null;
        private static double _closestCommanderDistance = double.MaxValue;
        private static FormLocator _activeLocator = null;
        private ConfigSaverClass _formConfig = null;
        private VRLocator _vrLocator = null;
        private bool _useEnhancedVRHUD = true;
        //private FormVRMatrixEditor _formVRMatrixTest = null;
        //private byte[] _vrPanelImageBytes = null;
        //private IntPtr _intPtrVROverlayImage;

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

            if (checkBoxTrackClosest.Checked && FormTracker.CurrentLocation != null)
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

                // Switch tracking to this target
                _targetPosition = edEvent.Location();
                SetTarget(edEvent.Commander);
                return;
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


        public void SetTarget(EDLocation targetLocation, string additionalInfo = "", string targetName = "")
        {
            // Sets the tracking target
            _targetPosition = targetLocation;
            if (String.IsNullOrEmpty(targetName))
                targetName = targetLocation.Name;
            UpdateTrackingTarget(targetName);
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

            locatorHUD1.SetSpeed(FormTracker.Tracker.SpeedInMS);

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
                _targetPosition = FormTracker.CurrentLocation.Copy();
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
                CommanderWatcher.Start($"http://{ServerAddress}:11938/DataCollator");
                UpdateAvailableCommanders();;
                CommanderWatcher.OnlineCountChanged += CommanderWatcher_OnlineCountChanged;
            }
            else
            {
                CommanderWatcher.Stop();
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

        private bool CreateVRLocator()
        {
            //if (_useWindowClientAreaForVR)
            //    _vrLocator = new VRLocator(locatorHUD1.Width, locatorHUD1.Height);
            //else
                _vrLocator = new VRLocator();
            return true;
        }

        private void checkBoxEnableVRLocator_CheckedChanged(object sender, EventArgs e)
        {
            string initError = "";
            if (!checkBoxEnableVRLocator.Enabled)
                return;

            if (_vrLocator == null)
            {
                CreateVRLocator();
                checkBoxEnableVRLocator.Checked = _vrLocator.VRInitializedOk;
                if (!_vrLocator.VRInitializedOk)
                {
                    checkBoxEnableVRLocator.Enabled = checkBoxEnableVRLocator.Checked; // If we fail to initialise, we disable the option completely
                    initError = "Failed to initialise VR subsystem";
                }              
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

        private void UpdateVRLocatorImage()
        {
            if (_vrLocator == null)
                return;

            string info = "";
            if (locatorHUD1.PanelRequiresReset())
            {
                // For some reason, after 200 updates the OpenVR layer locks up
                // No idea why, so this horrible hack resets it
                _vrLocator.Hide(false);
                if (!_vrLocator.ResetVR())
                {
                    checkBoxEnableVRLocator.Checked = false;
                    return;
                }
                locatorHUD1.ResetPanel();
                ShowVRLocator(ref info);
                return;
            }

            Bitmap locatorPanel = null;
            if (_useEnhancedVRHUD)
                locatorPanel = locatorHUD1.GetVRLocatorHUD();
            else
                locatorPanel = locatorHUD1.GetLocatorPanelBitmap();

            try
            {
                _vrLocator.UpdateVRLocatorImage(locatorPanel);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Error updating VR image", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
            // If we get here the overlay update failed, so we attempt to reset Open VR
            HideVRLocator(false);
            locatorHUD1.ResetPanel();

            ShowVRLocator(ref info);
        }

        private bool ShowVRLocator(ref string info)
        {
            if (_vrLocator == null)
                CreateVRLocator();

            UpdateVRLocatorImage();
            _vrLocator.ShowMatrixEditor();

            _vrLocator.Show();
            return true;
        }

        private void HideVRLocator(bool CloseMatrixWindow = true)
        {
            try
            {
                if (_vrLocator != null)
                    _vrLocator.Hide(CloseMatrixWindow);
            }
            catch { }
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
