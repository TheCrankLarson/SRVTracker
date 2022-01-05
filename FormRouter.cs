using EDTracking;
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
using System.Media;
using System.Speech.Synthesis;
using System.Text.Json;
using System.IO;
using Valve.VR;


namespace SRVTracker
{
    public partial class FormRouter : Form
    {
        static Size _fullSize = new Size(484, 350);
        private EDLocation _lastLoggedLocation = null;
        private int _nextWaypoint = 0;
        private EDRoute _route = null;
        private string _saveFileName = "";
        private FormTracker _formTracker = null; // We need a reference to the tracker so that we can start tracking if necessary
        private ConfigSaverClass _formConfig = null;
        private bool _updatingWaypointInfo = false;
        private Dictionary<string, string> _eventSounds = null;
        private List<string> _soundSources = null;
        private SoundPlayer _soundPlayer = new SoundPlayer();
        private int _lapNumber = 1;
        private DateTime _timeTrialStart = DateTime.MinValue;
        private List<DateTime> _timeTrialWaypointTimes = null;
        private FormTelemetryDisplay _timeTrialTelemetryDisplay = null;
        private TelemetryWriter _timeTrialTelemetryWriter = null;
        private SpeechSynthesizer _speechSynthesizer = null;
        private Dictionary<string, string> _speechEventPhrases = null;

        public FormRouter(FormTracker formTracker)
        {
            InitializeComponent();
            CalculateWindowSizes();
            this.Size = _fullSize;
            _timeTrialTelemetryWriter = new TelemetryWriter();
            InitVoiceCombo();

            // Attach our form configuration saver
            _formConfig = new ConfigSaverClass(this, true);
            _formConfig.ExcludedControls.Add(textBoxWaypointName);
            _formConfig.ExcludedControls.Add(textBoxRouteName);
            _formConfig.ExcludedControls.Add(comboBoxBasicLocation);
            _formConfig.ExcludedControls.Add(comboBoxGateLocation1);
            _formConfig.ExcludedControls.Add(comboBoxGateLocation2);
            _formConfig.ExcludedControls.Add(comboBoxGateTarget);
            _formConfig.ExcludedControls.Add(comboBoxPolygonTarget);
            _formConfig.ExcludedControls.Add(comboBoxWaypointType);
            _formConfig.ExcludedControls.Add(numericUpDownGenerationDistanceBetweenWaypoint);
            _formConfig.ExcludedControls.Add(comboBoxGenerationDistanceBetweenWaypointUnit);
            _formConfig.ExcludedControls.Add(comboBoxGenerationRouteTemplate);
            _formConfig.SaveEnabled = true;
            _formConfig.RestorePreviousSize = false;
            _formConfig.RestoreFormValues();

            comboBoxGenerationDistanceBetweenWaypointUnit.SelectedIndex = 1; // Default to km

            _route = new EDRoute();
            _formTracker = formTracker;
            FormTracker.CommanderLocationChanged += FormTracker_CommanderLocationChanged;
            buttonStop.Enabled = false;
            listBoxWaypoints_SelectedIndexChanged(null, null);
            comboBoxWaypointType.SelectedIndex = 0;
            InitSoundsList();
            InitSpeechList();

            if (comboBoxSelectedVoice.SelectedIndex < 0 && comboBoxSelectedVoice.Items.Count > 0)
                comboBoxSelectedVoice.SelectedIndex = 0;
        }

        private void CalculateWindowSizes()
        {
            // Calculate size with locations hidden
            int leftBound = tabControl1.Location.X + tabControl1.Width;
            _fullSize.Width = (this.Width - this.ClientRectangle.Width) + leftBound + 6;
            int bottomBound = tabControl1.Location.Y + tabControl1.Height;
            _fullSize.Height = (this.Height - this.ClientRectangle.Height) + bottomBound + 6;
        }

        private void DisplayRoute()
        {
            Action action = new Action(() =>
            {
                int selectedIndex = listBoxWaypoints.SelectedIndex;
                listBoxWaypoints.BeginUpdate();
                listBoxWaypoints.Items.Clear();
                for (int i = 0; i < _route.Waypoints.Count; i++)
                    listBoxWaypoints.Items.Add(_route.Waypoints[i].Name);
                if (selectedIndex > -1)
                {
                    if (selectedIndex >= listBoxWaypoints.Items.Count)
                        selectedIndex = listBoxWaypoints.Items.Count - 1;
                    listBoxWaypoints.SelectedIndex = selectedIndex;
                }
                listBoxWaypoints.EndUpdate();
            });
            if (listBoxWaypoints.InvokeRequired)
                listBoxWaypoints.Invoke(action);
            else
                action();
            _timeTrialTelemetryDisplay?.UpdateCell(1, 0, _route.Name);
        }

        private string GetBearingAfterNextWaypoint()
        {
            if (!checkBoxPlayIncudeDirection.Checked)
                return "";

            StringBuilder additionalInfo = new StringBuilder();

            if (_route.Waypoints[_nextWaypoint] != null && FormTracker.CurrentLocation != null)
            {
                bool finalWaypoint = false;
                EDWaypoint nextNextWaypoint = null;
                if (numericUpDownTotalLaps.Value > 1)
                {
                    // More than one lap, so start/finish is first waypoint
                    if (_nextWaypoint == _route.Waypoints.Count - 1)
                        nextNextWaypoint = _route.Waypoints[0];
                    else
                        nextNextWaypoint = _route.Waypoints[_nextWaypoint + 1];

                    if ((_nextWaypoint == 0) && (_lapNumber == numericUpDownTotalLaps.Value))
                        finalWaypoint = true;
                }
                else
                {
                    // One lap, finish is final waypoint
                    if (_nextWaypoint<_route.Waypoints.Count-1)
                        nextNextWaypoint = _route.Waypoints[_nextWaypoint + 1];
                    else
                        finalWaypoint = true;
                }

                if (!finalWaypoint)
                {
                    try
                    {
                        double bearingFromNextWaypoint = EDLocation.BearingToLocation(_route.Waypoints[_nextWaypoint].Location, nextNextWaypoint.Location);
                        double bearingToNextWaypoint = EDLocation.BearingToLocation(FormTracker.CurrentLocation, _route.Waypoints[_nextWaypoint].Location);
                        double bearingChange = EDLocation.BearingDelta(bearingToNextWaypoint, bearingFromNextWaypoint);
                        //additionalInfo.Append("Then ");
                        if (bearingChange > -5 && bearingChange < 5)
                            additionalInfo.Append(_speechEventPhrases["Straight on"]);
                        else
                        {
                            if (bearingChange < 0)
                            {
                                if (bearingChange < -140)
                                    additionalInfo.Append(_speechEventPhrases["Hairpin left"]);
                                else if (bearingChange < -80)
                                    additionalInfo.Append(_speechEventPhrases["Sharp left"]);
                                if (bearingChange > -45)
                                    additionalInfo.Append(_speechEventPhrases["Bear left"]);
                                else
                                    additionalInfo.Append(_speechEventPhrases["Turn left"]);
                            }
                            else
                            {
                                if (bearingChange > 140)
                                    additionalInfo.Append(_speechEventPhrases["Hairpin right"]);
                                else if (bearingChange > 80)
                                    additionalInfo.Append(_speechEventPhrases["Sharp right"]);
                                if (bearingChange < 45)
                                    additionalInfo.Append(_speechEventPhrases["Bear right"]);
                                else
                                    additionalInfo.Append(_speechEventPhrases["Turn right"]);
                            }
                            additionalInfo.Append($" {_speechEventPhrases["To Bearing"]} ");
                            additionalInfo.Append(Math.Abs(bearingChange).ToString("F1"));
                            additionalInfo.Append("°");
                        }
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
                else
                    additionalInfo.Append(_route.Waypoints[_nextWaypoint].Name);
            }
            return additionalInfo.ToString();
        }

        private void FormTracker_CommanderLocationChanged(object sender, EventArgs e)
        {
            if (buttonStartRecording.Enabled)
                return;

            if (_lastLoggedLocation==null)
            {
                _lastLoggedLocation = FormTracker.CurrentLocation.Copy();
                return;
            }

            if (buttonPlay.Enabled)
            {
                // Start the time trial timer only on first movement
                if (checkBoxTimeTrial.Checked && _timeTrialStart == DateTime.MinValue)
                    if (!_lastLoggedLocation.Equals(FormTracker.CurrentLocation))
                    {
                        _timeTrialStart = DateTime.UtcNow;
                        _timeTrialWaypointTimes = new List<DateTime>();
                        _timeTrialWaypointTimes.Add(_timeTrialStart);
                        if (numericUpDownTotalLaps.Value > 1)
                            _timeTrialTelemetryDisplay.AddRow("LAP 1 Start", "00:00:00.00");
                        else
                            _timeTrialTelemetryDisplay.AddRow("Start", "00:00:00.00");
                        _lapNumber = 1;
                    }

                // We are currently replaying a route
                EDLocation previousWaypointLocation = null;
                if (_nextWaypoint > 0)
                    previousWaypointLocation = _route.Waypoints[_nextWaypoint - 1].Location;
                else if (numericUpDownTotalLaps.Value > 1)
                    previousWaypointLocation = _route.Waypoints[_route.Waypoints.Count - 1].Location;


                if (_route.Waypoints[_nextWaypoint].WaypointHit(FormTracker.CurrentLocation, FormTracker.PreviousLocation, previousWaypointLocation))
                {
                    // Arrived at the waypoint
                    _timeTrialWaypointTimes?.Add(DateTime.UtcNow);
                    if (_nextWaypoint == 0 && (numericUpDownTotalLaps.Value > 1))
                    {
                        // We've arrived back at start, so have completed a lap
                        _lapNumber++;
                        if (_lapNumber<=numericUpDownTotalLaps.Value)
                            _timeTrialTelemetryDisplay?.AddRow($"LAP {_lapNumber}: {_route.Waypoints[_nextWaypoint].Name}",
                                _timeTrialWaypointTimes[_timeTrialWaypointTimes.Count - 1].Subtract(_timeTrialWaypointTimes[0]).ToString(@"hh\:mm\:ss\.ff"));
                    }
                    else
                        _timeTrialTelemetryDisplay?.AddRow(_route.Waypoints[_nextWaypoint].Name,
                            _timeTrialWaypointTimes[_timeTrialWaypointTimes.Count - 1].Subtract(_timeTrialWaypointTimes[0]).ToString(@"hh\:mm\:ss\.ff"));

                    if (checkBoxScreenshot.Checked)
                        FormDrone.SendKey(Keyboard.DirectXKeyStrokes.DIK_F10);

                    _nextWaypoint++;
                    if (_nextWaypoint >= _route.Waypoints.Count && numericUpDownTotalLaps.Value > 1)
                        _nextWaypoint = 0;

                    if (_nextWaypoint >= _route.Waypoints.Count || (_nextWaypoint==1 && numericUpDownTotalLaps.Value<_lapNumber))
                    {
                        // End of route
                        buttonStop.Enabled = false;
                        buttonStartRecording.Enabled = true;
                        PlayEventSound("Route completed");
                        Speak(_speechEventPhrases["Reached destination"]);
                        _timeTrialWaypointTimes?.Add(DateTime.UtcNow);
                        _timeTrialTelemetryDisplay?.AddRow("Finished",
                            _timeTrialWaypointTimes[_timeTrialWaypointTimes.Count - 1].Subtract(_timeTrialWaypointTimes[0]).ToString(@"hh\:mm\:ss\.ff"));
                        FormLocator.GetLocator().SetTarget(_route.Waypoints[_route.Waypoints.Count-1].Location, _route.Waypoints[_route.Waypoints.Count - 1].Name, "Destination Reached");
                    }
                    else
                    {
                        PlayEventSound("Arrived at waypoint");
                        string waypointDirections = GetBearingAfterNextWaypoint();
                        FormLocator.GetLocator().SetTarget(_route.Waypoints[_nextWaypoint].Location, waypointDirections, _route.Waypoints[_nextWaypoint].Name);
                        if (checkBoxAnnounceDirectionHints.Checked)
                        {
                            string directionNow = $"Head towards bearing {FormLocator.GetLocator().BearingToTarget:f0}, then {waypointDirections}";
                            Speak(directionNow);
                        }
                        Action action = new Action(() =>
                        {
                            listBoxWaypoints.SelectedIndex = _nextWaypoint;
                        });
                        if (listBoxWaypoints.InvokeRequired)
                            listBoxWaypoints.Invoke(action);
                        else
                            action();
                    }

                    if (checkBoxScreenshot.Checked)
                        FormDrone.SendKey(Keyboard.DirectXKeyStrokes.DIK_F10, true);
                }
                else
                    FormLocator.GetLocator().SetAdditionalInfo(GetBearingAfterNextWaypoint());
                return;
            }

            if (EDLocation.DistanceBetween(_lastLoggedLocation, FormTracker.CurrentLocation) >= (double)numericUpDownRecordDistance.Value)
            {
                _lastLoggedLocation = FormTracker.CurrentLocation.Copy();
                AddLocationToRoute(_lastLoggedLocation);
            }
        }

        private void AddLocationToRoute(EDLocation location)
        {
            EDWaypoint waypoint = new EDWaypoint(location, DateTime.UtcNow, (double)numericUpDownRadius.Value);
            waypoint.Radius = (double)numericUpDownRadius.Value;
            waypoint.MinimumAltitude = (double)numericUpDownMinAltitude.Value;
            waypoint.MaximumAltitude = (double)numericUpDownMaxAltitude.Value;

            if (String.IsNullOrEmpty(location.Name))
            {
                // If we have a name that ends in a number, then we take that name and increase the number by one
                // Otherwise we don't do anything as the waypoint will be automatically generated if blank
                string waypointName = textBoxWaypointName.Text;
                int postfix = waypointName.LastIndexOf(' ');
                if (postfix > -1)
                {
                    string waypointNum = waypointName.Substring(postfix).Trim();
                    int wpNum;
                    if (int.TryParse(waypointNum, out wpNum))
                    {
                        wpNum++;
                        waypoint.Name = $"{waypointName.Substring(0, postfix)} {wpNum}";
                    }
                }
            }

            if (comboBoxWaypointType.SelectedIndex == 1)
            {
                // This is a gate, so initialise it if necessary
                SetWaypointType("Gate", waypoint);
                if (waypoint.AdditionalLocations.Count < 1)
                {
                    waypoint.AdditionalLocations.Add(waypoint.Location.Copy());
                    waypoint.AdditionalLocations[0].Name = "Gate marker 1";
                }
            }

            AddWaypointToRoute(waypoint);
        }


        private void DisplayGate()
        {
            ShowWaypointEditor(groupBoxGate);
            EDWaypoint waypoint = GetSelectedWaypoint();

            comboBoxGateTarget.Text = "";
            comboBoxGateLocation1.Text = "";
            comboBoxGateLocation2.Text = "";
            if (waypoint == null)
                return;

            if (waypoint?.Location != null)
                comboBoxGateTarget.Text = waypoint.Location.Name;

            if (waypoint?.AdditionalLocations.Count > 0 && waypoint.AdditionalLocations[0] != null)
                comboBoxGateLocation1.Text = waypoint.AdditionalLocations[0].Name;

            if (waypoint?.AdditionalLocations.Count > 1 && waypoint.AdditionalLocations[1] != null)
                comboBoxGateLocation2.Text = waypoint.AdditionalLocations[1].Name;

            if (waypoint.AdditionalLocations.Count < 1)
            {
                waypoint.AdditionalLocations.Add(waypoint.Location.Copy());
                waypoint.AdditionalLocations[0].Name = "Gate marker 1";
                comboBoxGateLocation1.Text = "Gate marker 1";
                comboBoxGateLocation2.Text = "";
                comboBoxGateTarget.Text = "";
            }
            else
            {
                if (waypoint.AdditionalLocations[0] != null)
                    comboBoxGateLocation1.Text = waypoint.AdditionalLocations[0].Name;
                if (waypoint.AdditionalLocations.Count > 1 && waypoint.AdditionalLocations[1] != null)
                    comboBoxGateLocation2.Text = waypoint.AdditionalLocations[1].Name;
            }
        }

        private void DisplayPolygon()
        {
            ShowWaypointEditor(groupBoxPolygon);
            EDWaypoint waypoint = GetSelectedWaypoint();

            comboBoxPolygonTarget.Text = "";
            listBoxPolygonMarkers.Items.Clear();

            if (waypoint != null)
            {
                for (int i = 0; i < waypoint.AdditionalLocations.Count; i++)
                    listBoxPolygonMarkers.Items.Add(waypoint.AdditionalLocations[i].Name);
                comboBoxPolygonTarget.Text = waypoint.Location.Name;
            }

            UpdatePolygonButtons();
        }


        private void DisplayBasic()
        {
            ShowWaypointEditor(groupBoxBasic);
            EDWaypoint waypoint = GetSelectedWaypoint();
            if (waypoint != null)
            {
                if (waypoint.Location != null)
                {
                    if (String.IsNullOrEmpty(waypoint.Location.Name))
                        waypoint.Location.Name = waypoint.Name;
                    comboBoxBasicLocation.Text = waypoint.Location.Name;
                }
                checkBoxAllowPassing.Checked = waypoint.AllowPassing;
                numericUpDownRadius.Value = (decimal)waypoint.Radius;
            }
            else
            {
                comboBoxBasicLocation.Text = "";
            }    
        }

        private void AddWaypointToRoute(EDWaypoint waypoint)
        {
            _route.Waypoints.Add(waypoint);

            PlayEventSound("Waypoint added");

            Action action = new Action(() =>
            {

                listBoxWaypoints.Items.Add(waypoint.Name);
                listBoxWaypoints.SelectedIndex = listBoxWaypoints.Items.Count - 1;
                comboBoxWaypointType_SelectedIndexChanged(null, null); // This ensures that the correct waypoint type is applied

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
                tabControl1.SelectedTab = tabControl1.TabPages["tabPageRoute"];
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
                        if (_route != null)
                        {
                            textBoxRouteName.Text = _route.Name;
                            _saveFileName = openFileDialog.FileName;
                            buttonSaveRoute.Enabled = true;
                            DisplayRoute();
                        }
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
                FormLocator.GetLocator().SetTarget(_route.Waypoints[listBoxWaypoints.SelectedIndex].Location,"", _route.Waypoints[listBoxWaypoints.SelectedIndex].Name);
            }
            catch { }
        }

        private void listBoxWaypoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            _updatingWaypointInfo = true;
            if (listBoxWaypoints.SelectedIndex < 0)
            {
                buttonDuplicateWaypoint.Enabled = false;
                buttonDeleteWaypoint.Enabled = false;
                buttonMoveDown.Enabled = false;
                buttonMoveUp.Enabled = false;
                buttonSetAsTarget.Enabled = false;
                textBoxWaypointName.Text = "";
                return;
            }

            EDWaypoint waypoint = _route.Waypoints[listBoxWaypoints.SelectedIndex];
            if (waypoint.ExtendedWaypointInformation.ContainsKey("WaypointType"))
            {
                switch (waypoint.ExtendedWaypointInformation["WaypointType"])
                {
                    case "Gate":
                        comboBoxWaypointType.SelectedIndex = 1;
                        DisplayGate();
                        break;
                }
            }
            else
            {
                DisplayBasic();
                comboBoxWaypointType.SelectedIndex = 0;
            }
            textBoxWaypointName.Text = waypoint.Name;
            numericUpDownMinAltitude.Value = (decimal)waypoint.MinimumAltitude;
            numericUpDownMaxAltitude.Value = (decimal)waypoint.MaximumAltitude;
            buttonDuplicateWaypoint.Enabled = true;
            buttonDeleteWaypoint.Enabled = true;
            buttonMoveDown.Enabled = true;
            buttonMoveUp.Enabled = true;
            buttonSetAsTarget.Enabled = true;
            _updatingWaypointInfo = false;
        }

        private void numericUpDownRadius_ValueChanged(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0 || _updatingWaypointInfo)
                return;
            _route.Waypoints[listBoxWaypoints.SelectedIndex].Radius = (double)numericUpDownRadius.Value;
        }

        private void numericUpDownMinAltitude_ValueChanged(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0 || _updatingWaypointInfo)
                return;
            _route.Waypoints[listBoxWaypoints.SelectedIndex].MinimumAltitude = (double)numericUpDownMinAltitude.Value;
        }

        private void numericUpDownMaxAltitude_ValueChanged(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0 || _updatingWaypointInfo)
                return;
            _route.Waypoints[listBoxWaypoints.SelectedIndex].MaximumAltitude = (double)numericUpDownMaxAltitude.Value;
        }

        private void textBoxWaypointName_TextChanged(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0 || _updatingWaypointInfo)
                return;
            _route.Waypoints[listBoxWaypoints.SelectedIndex].Name = textBoxWaypointName.Text;
            
            DisplayRoute();
        }

        private void checkBoxAllowPassing_CheckedChanged(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0 || _updatingWaypointInfo)
                return;
            _route.Waypoints[listBoxWaypoints.SelectedIndex].AllowPassing = checkBoxAllowPassing.Checked;
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            _nextWaypoint = 0;
            if (numericUpDownTotalLaps.Value > 1)
                _nextWaypoint = 1;  // Lapped circuits have the first waypoint as the Start/Finish
            _timeTrialStart = DateTime.MinValue;
            if (listBoxWaypoints.SelectedIndex > 0)
                _nextWaypoint = listBoxWaypoints.SelectedIndex;

            if (checkBoxTimeTrial.Checked)
                Speak("Timer will start on first movement");
            buttonStartRecording.Enabled = false;
            buttonStop.Enabled = true;
            _formTracker.StartTracking();
            FormLocator.GetLocator().SetTarget(_route.Waypoints[_nextWaypoint].Location, GetBearingAfterNextWaypoint(), _route.Waypoints[_nextWaypoint].Name);
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
            EDLocation locationToAdd = FormLocationEditor.GetFormLocationEditor().SelectLocation(this, ((Control)sender).PointToScreen(new Point(buttonAddWaypoint.Width, buttonAddWaypoint.Height)));
            if (locationToAdd != null)
                AddLocationToRoute(locationToAdd);
        }

        private void buttonAddCurrentLocation_Click(object sender, EventArgs e)
        {
            if (FormTracker.CurrentLocation != null)
                AddLocationToRoute(FormTracker.CurrentLocation.Copy());
        }

        private void textBoxRouteName_TextChanged(object sender, EventArgs e)
        {
            _route.Name = textBoxRouteName.Text;
            _timeTrialTelemetryDisplay?.UpdateCell(1, 0, _route.Name);
        }


        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 1)
                return;

            int newIndex = listBoxWaypoints.SelectedIndex - 1;
            EDWaypoint waypoint = _route.Waypoints[listBoxWaypoints.SelectedIndex];
            _route.Waypoints[listBoxWaypoints.SelectedIndex] = _route.Waypoints[newIndex];
            _route.Waypoints[newIndex] = waypoint;
            Action action = new Action(() =>
            {
                object selected = listBoxWaypoints.SelectedItem;
                listBoxWaypoints.Items.Remove(selected);
                listBoxWaypoints.Items.Insert(newIndex, selected);
                listBoxWaypoints.SetSelected(newIndex, true);
            });
            if (listBoxWaypoints.InvokeRequired)
                listBoxWaypoints.Invoke(action);
            else
                action();
        }

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0 || listBoxWaypoints.SelectedIndex == listBoxWaypoints.Items.Count - 1)
                return;

            int newIndex = listBoxWaypoints.SelectedIndex + 1;
            EDWaypoint waypoint = _route.Waypoints[listBoxWaypoints.SelectedIndex];
            _route.Waypoints[listBoxWaypoints.SelectedIndex] = _route.Waypoints[newIndex];
            _route.Waypoints[newIndex] = waypoint;
            Action action = new Action(() =>
            {
                object selected = listBoxWaypoints.SelectedItem;
                listBoxWaypoints.Items.Remove(selected);
                listBoxWaypoints.Items.Insert(newIndex, selected);
                listBoxWaypoints.SetSelected(newIndex, true);
            });
            if (listBoxWaypoints.InvokeRequired)
                listBoxWaypoints.Invoke(action);
            else
                action();
        }

        private void buttonDuplicateWaypoint_Click(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0)
                return;

            EDWaypoint newWaypoint = EDWaypoint.FromString(_route.Waypoints[listBoxWaypoints.SelectedIndex].ToString());
            AddWaypointToRoute(newWaypoint);
        }

        private string[] SpeechEvents()
        {
            string[] speechEvents = { "Straight on", "Turn left", "Turn right",
                "Bear left", "Bear right", "Sharp left", "Sharp right", "Hairpin left", "Hairpin right",
                "Destination reached", "To Bearing"};
            return speechEvents;
        }

        private string[] SoundEvents()
        {
            string[] soundEvents= { "Arrived at waypoint", "Route completed", "Waypoint added" };
            return soundEvents;
        }

        private string[] RemovedSoundEvents()
        {
            string[] soundEvents = { "New waypoint recorded" };
            return soundEvents;
        }

        private string[] SoundSources()
        {
            string[] soundSources = { "None", "Custom sound...", "System.Asterisk", "System.Beep", "System.Exclamation", "System.Hand", "System.Question" };
            return soundSources;

        }

        private void UpdateSoundUI()
        {
            listBoxAudioEvents.Enabled = checkBoxEnableAudio.Checked;
            comboBoxChooseSound.Enabled = checkBoxEnableAudio.Checked && listBoxAudioEvents.SelectedIndex>-1;
        }

        private void InitSoundsList()
        {
            if (_soundSources==null)
            {
                // Sounds sources are stored in the tag of comboBoxChooseSound
                string soundData = (string)comboBoxChooseSound.Tag;
                if (!String.IsNullOrEmpty(soundData))
                {
                    // We restore saved data from the Tag of our checkbox (saved automatically by ConfigSaver)
                    _soundSources = (List<string>)JsonSerializer.Deserialize(soundData, typeof(List<string>));
                }
                if (_soundSources == null)
                    _soundSources = new List<string>();

                // Check we have all available events present
                foreach (string soundSource in SoundSources())
                    if (!_soundSources.Contains(soundSource))
                        _soundSources.Add(soundSource);
            }

            // Initialise the sound choices
            comboBoxChooseSound.Items.Clear();
            comboBoxChooseSound.Items.AddRange(_soundSources.ToArray());

            if (_eventSounds==null)
            {
                string soundData = (string)checkBoxEnableAudio.Tag;
                if (!String.IsNullOrEmpty(soundData))
                {
                    // We restore saved data from the Tag of our checkbox (saved automatically by ConfigSaver)
                    _eventSounds = (Dictionary<string, string>)JsonSerializer.Deserialize(soundData, typeof(Dictionary<string, string>));
                }
                if (_eventSounds == null)
                    _eventSounds = new Dictionary<string, string>();

                // Check we have all available events present
                foreach (string soundEvent in SoundEvents())
                    if (!_eventSounds.ContainsKey(soundEvent))
                        _eventSounds.Add(soundEvent, "");
                // And remove any old ones (happens if they are renamed or removed)
                foreach (string soundEvent in RemovedSoundEvents())
                    if (_eventSounds.ContainsKey(soundEvent))
                        _eventSounds.Remove(soundEvent);
            }

            // Add the events to our event sound list
            listBoxAudioEvents.Items.Clear();
            listBoxAudioEvents.Items.AddRange(_eventSounds.Keys.ToArray<string>());

            UpdateSoundUI();
        }

        private void InitSpeechList()
        {
            if (_speechEventPhrases == null)
            {
                string speechData = (string)checkBoxEnableSpeech.Tag;
                if (!String.IsNullOrEmpty(speechData))
                {
                    // We restore saved data from the Tag of our checkbox (saved automatically by ConfigSaver)
                    _speechEventPhrases = (Dictionary<string, string>)JsonSerializer.Deserialize(speechData, typeof(Dictionary<string, string>));
                }
                if (_speechEventPhrases == null)
                    _speechEventPhrases = new Dictionary<string, string>();

                //// Check we have all available events present
                foreach (string speechEvent in SpeechEvents())
                    if (!_speechEventPhrases.ContainsKey(speechEvent))
                        _speechEventPhrases.Add(speechEvent, speechEvent);
                //// And remove any old ones (happens if they are renamed or removed)
                //foreach (string soundEvent in RemovedSoundEvents())
                //    if (_eventSounds.ContainsKey(soundEvent))
                //        _eventSounds.Remove(soundEvent);
            }

            // Add the events to our event sound list
            listBoxSpeechEvents.Items.Clear();
            listBoxSpeechEvents.Items.AddRange(_speechEventPhrases.Keys.ToArray<string>());

            listBoxSpeechEvents_SelectedIndexChanged(null, null);
        }

        private void StoreSoundSettings()
        {
            if (_eventSounds == null)
                checkBoxEnableAudio.Tag = null;
            else
                checkBoxEnableAudio.Tag = JsonSerializer.Serialize(_eventSounds);
            if (_soundSources == null)
                comboBoxChooseSound.Tag = null;
            else
                comboBoxChooseSound.Tag = JsonSerializer.Serialize(_soundSources);
        }

        private void StoreSpeechSettings()
        {
            if (_speechEventPhrases == null)
                checkBoxEnableSpeech.Tag = null;
            else
                checkBoxEnableSpeech.Tag = JsonSerializer.Serialize(_speechEventPhrases);
        }

        private void checkBoxEnableAudio_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSoundUI();
        }

        private void listBoxAudioEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSoundUI();
            for (int i=0; i<comboBoxChooseSound.Items.Count; i++)
                if (((string)comboBoxChooseSound.Items[i]).Equals(_eventSounds[(string)listBoxAudioEvents.SelectedItem]))
                {
                    comboBoxChooseSound.SelectedIndex = i;
                    return;
                }
            comboBoxChooseSound.SelectedIndex = -1;
        }

        private string OpenSoundFile(string currentFile = "")
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = currentFile;
                openFileDialog.Filter = "Sound files (*.wav)|*.wav|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FileName = currentFile;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    return openFileDialog.FileName;
                return "";
            }
        }

        private void comboBoxChooseSound_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxChooseSound.SelectedIndex<0 || listBoxAudioEvents.SelectedIndex<0)
            {
                UpdateSoundUI();
                return;
            }

            string selectedSound = (string)comboBoxChooseSound.SelectedItem;
            if (selectedSound.Equals("Custom sound..."))
            {
                // Add custom sound
                string currentFile = "";
                if (_eventSounds.ContainsKey((string)listBoxAudioEvents.SelectedItem))
                    currentFile = _eventSounds[(string)listBoxAudioEvents.SelectedItem];
                selectedSound = OpenSoundFile(currentFile);
                if (!String.IsNullOrEmpty(selectedSound))
                {
                    if (!_soundSources.Contains(selectedSound))
                    {
                        _soundSources.Add(selectedSound);
                        comboBoxChooseSound.Items.Add(selectedSound);
                        comboBoxChooseSound.SelectedIndex = comboBoxChooseSound.Items.Count - 1;
                    }
                }
                else
                    selectedSound = currentFile;
            }
            if (String.IsNullOrEmpty(selectedSound))
                selectedSound = "None";
            _eventSounds[(string)listBoxAudioEvents.SelectedItem] = selectedSound;
            StoreSoundSettings();
        }

        private void PlayEventSound(string eventName)
        {
            if (!checkBoxEnableAudio.Checked || !_eventSounds.ContainsKey(eventName) || String.IsNullOrEmpty(_eventSounds[eventName]) || _eventSounds[eventName].Equals("None"))
                return;

            if (_eventSounds[eventName].StartsWith("System"))
            {
                try
                {
                    // System sound
                    switch (_eventSounds[eventName])
                    {
                        case "System.Asterisk":
                            System.Media.SystemSounds.Asterisk.Play();
                            break;

                        case "System.Beep":
                            System.Media.SystemSounds.Beep.Play();
                            break;

                        case "System.Exclamation":
                            System.Media.SystemSounds.Exclamation.Play();
                            break;

                        case "System.Hand":
                            System.Media.SystemSounds.Hand.Play();
                            break;

                        case "System.Question":
                            System.Media.SystemSounds.Question.Play();
                            break;
                    }
                }
                catch { }
                return;
            }

            // Assume that the sound is an audio file
            if (!File.Exists(_eventSounds[eventName]))
            {
                try
                {
                    System.Media.SystemSounds.Exclamation.Play();
                } catch { }
                return;
            }

            try
            {
                _soundPlayer.SoundLocation = _eventSounds[eventName];
                _soundPlayer.Play();
            }
            catch { }
        }

        private void InitVoiceCombo()
        {
            comboBoxSelectedVoice.Items.Clear();
            try
            {
                if (_speechSynthesizer == null)
                {
                    _speechSynthesizer = new SpeechSynthesizer();
                    _speechSynthesizer.SetOutputToDefaultAudioDevice();
                }
            }
            catch { }
            if (_speechSynthesizer == null)
                return;

            foreach (InstalledVoice voice in _speechSynthesizer.GetInstalledVoices())
                comboBoxSelectedVoice.Items.Add(voice.VoiceInfo.Name);
        }

        private void Speak(string speech)
        {
            if (String.IsNullOrEmpty(speech) || !checkBoxEnableAudio.Checked)
                return;

            try
            {
                if (_speechSynthesizer == null)
                {
                    _speechSynthesizer = new SpeechSynthesizer();
                    _speechSynthesizer.SetOutputToDefaultAudioDevice();
                }
                _speechSynthesizer.SpeakAsync(speech);
            }
            catch { }
        }

        private void buttonReverseWaypointOrder_Click(object sender, EventArgs e)
        {
            _route.ReverseRoute();
            DisplayRoute();
        }

        private void CalculateGateTarget(EDWaypoint gateWaypoint)
        {
            // Calculates the target location for the Gate waypoint
            // Target will be mid-point between the two gates

            if (gateWaypoint.AdditionalLocations.Count == 0)
                return;
            if (gateWaypoint.AdditionalLocations.Count == 1)
                gateWaypoint.Location = gateWaypoint.AdditionalLocations[0];
            else  if (gateWaypoint.AdditionalLocations[0] == null)
                gateWaypoint.Location = gateWaypoint.AdditionalLocations[1];
            else
            {
                // We work out the midpoint between the two locations
                gateWaypoint.Location = EDLocation.MidpointBetween(gateWaypoint.AdditionalLocations);
                if (!String.IsNullOrEmpty(gateWaypoint.Name))
                    gateWaypoint.Location.Name = gateWaypoint.Name;
                else
                    gateWaypoint.Location.Name = "Midpoint";
            }
        }

        private void buttonSetGateLocation1ToCurrentLocation_Click(object sender, EventArgs e)
        {
            EDWaypoint thisWaypoint = GetSelectedWaypoint();
            if (thisWaypoint == null)
                return;

            EDLocation gateLocation = FormTracker.CurrentLocation?.Copy();
            if (gateLocation == null)
                return;

            gateLocation.Name = "Gate marker 1";
            if (thisWaypoint.AdditionalLocations.Count > 0)
                thisWaypoint.AdditionalLocations[0] = gateLocation;
            else
                thisWaypoint.AdditionalLocations.Add(gateLocation);
            comboBoxGateLocation1.Text = gateLocation.Name;
        }

        private void buttonSetGateLocation2ToCurrentLocation_Click(object sender, EventArgs e)
        {
            EDWaypoint thisWaypoint = GetSelectedWaypoint();
            if (thisWaypoint == null)
                return;

            EDLocation gateLocation = FormTracker.CurrentLocation?.Copy();
            if (gateLocation == null)
                return;

            gateLocation.Name = "Gate marker 2";
            if (thisWaypoint.AdditionalLocations.Count < 1)
                thisWaypoint.AdditionalLocations.Add(null);
            if (thisWaypoint.AdditionalLocations.Count > 1)
                thisWaypoint.AdditionalLocations[1] = gateLocation;
            else
                thisWaypoint.AdditionalLocations.Add(gateLocation);
            comboBoxGateLocation2.Text = gateLocation.Name;
        }

        private void ShowWaypointEditor(GroupBox ActiveEditor)
        {
            groupBoxBasic.Visible = false;
            groupBoxGate.Visible = false;
            groupBoxPolygon.Visible = false;

            ActiveEditor.Visible = true;
        }

        private EDWaypoint GetSelectedWaypoint()
        {
            if (listBoxWaypoints.SelectedIndex < 0)
                return null;

            return _route.Waypoints[listBoxWaypoints.SelectedIndex];
        }

        private void SetWaypointType(string WaypointType=null, EDWaypoint waypoint = null)
        {
            if (waypoint==null)
                waypoint = GetSelectedWaypoint();
            if (waypoint == null)
                return;

            if (WaypointType == null)
            {
                if (waypoint.ExtendedWaypointInformation.ContainsKey("WaypointType"))
                    waypoint.ExtendedWaypointInformation.Remove("WaypointType");
                return;
            }

            if (waypoint.ExtendedWaypointInformation.ContainsKey("WaypointType"))
            {
                if (!waypoint.ExtendedWaypointInformation["WaypointType"].Equals(WaypointType))
                    waypoint.ExtendedWaypointInformation["WaypointType"] = WaypointType;
                return;
            }
                
            waypoint.ExtendedWaypointInformation.Add("WaypointType", WaypointType);
        }

        private void comboBoxWaypointType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxWaypointType.SelectedIndex)
            {
                case 0: // This is basic waypoint
                    SetWaypointType(null);
                    DisplayBasic();
                    break;

                case 1: // This is a gate                    
                    SetWaypointType("Gate");
                    DisplayGate();
                    break;

                case 2: // This is a polygon
                    SetWaypointType("Polygon");
                    DisplayPolygon();
                    break;
            }
        }

        private void buttonEditGateMarker1_Click(object sender, EventArgs e)
        {
            EditAdditionalLocation(0, comboBoxGateLocation1);
        }

        private void buttonEditGateMarker2_Click(object sender, EventArgs e)
        {
            EditAdditionalLocation(1, comboBoxGateLocation2);
        }

        private void buttonEditGateTarget_Click(object sender, EventArgs e)
        {
            EditMainLocation(comboBoxGateTarget);
        }

        private void buttonSetGateTargetToCurrentLocation_Click(object sender, EventArgs e)
        {
            EDWaypoint thisWaypoint = GetSelectedWaypoint();
            if (thisWaypoint == null || FormTracker.CurrentLocation == null)
                return;

            thisWaypoint.Location = FormTracker.CurrentLocation.Copy();
            comboBoxGateTarget.Text = thisWaypoint.Location.Name;
        }

        private void buttonCalculateGateTarget_Click(object sender, EventArgs e)
        {
            EDWaypoint thisWaypoint = GetSelectedWaypoint();
            if (thisWaypoint == null)
                return;

            if (thisWaypoint.AdditionalLocations.Count < 2 || thisWaypoint.AdditionalLocations[0]==null || thisWaypoint.AdditionalLocations[1] == null)
                return;

            CalculateGateTarget(thisWaypoint);
            comboBoxGateTarget.Text = thisWaypoint.Location.Name;
        }

        private void buttonEditLocations_Click(object sender, EventArgs e)
        {
            FormLocationEditor.GetFormLocationEditor().ShowWithBorder(this);
        }

        private void EditMainLocation(Control TargetControl = null)
        {
            EDWaypoint waypoint = GetSelectedWaypoint();
            if (waypoint == null)
                return;
            FormAddLocation formAddLocation = new FormAddLocation();
            if (waypoint.Location == null)
                waypoint.Location = new EDLocation();
            formAddLocation.EditLocation(waypoint.Location, this, true);
            if (TargetControl != null)
                TargetControl.Text = waypoint.Location.Name;
        }

        private void EditAdditionalLocation(int AdditionalLocationIndex, Control TargetControl = null)
        {
            EDWaypoint waypoint = GetSelectedWaypoint();
            if (waypoint == null)
                return;
            FormAddLocation formAddLocation = new FormAddLocation();
            EDLocation location;
            if (waypoint.AdditionalLocations.Count > AdditionalLocationIndex)
                location = waypoint.AdditionalLocations[AdditionalLocationIndex];
            else
            {
                while (waypoint.AdditionalLocations.Count < AdditionalLocationIndex-2)
                    waypoint.AdditionalLocations.Add(null);
                location = new EDLocation();
                waypoint.AdditionalLocations.Add(location);
            }
            formAddLocation.EditLocation(location, this, true);
            if (TargetControl != null)
                TargetControl.Text = location.Name;
        }

        private void buttonEditBasicLocation_Click(object sender, EventArgs e)
        {
            EditMainLocation(comboBoxBasicLocation);
        }

        private void buttonTargetGateMarker1_Click(object sender, EventArgs e)
        {
            try
            {
                FormLocator.GetLocator().SetTarget(GetSelectedWaypoint().AdditionalLocations[0]);
            }
            catch { }
        }

        private void buttonTargetGateMarker2_Click(object sender, EventArgs e)
        {
            try
            {
                FormLocator.GetLocator().SetTarget(GetSelectedWaypoint().AdditionalLocations[1]);
            }
            catch { }
        }

        private void buttonTargetGateTarget_Click(object sender, EventArgs e)
        {
            try
            {
                FormLocator.GetLocator().SetTarget(GetSelectedWaypoint().Location);
            }
            catch { }
        }

        private void checkBoxTimeTrial_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTimeTrial.Checked)
            {
                if (_timeTrialTelemetryDisplay == null || _timeTrialTelemetryDisplay.IsDisposed)
                {
                    _timeTrialTelemetryDisplay = new FormTelemetryDisplay(_timeTrialTelemetryWriter, " Time Trial Telemetry");
                    _timeTrialTelemetryDisplay.InitialiseRows(new Dictionary<string, string>());
                    _timeTrialTelemetryDisplay.AddRow("Time Trial Telemetry", textBoxRouteName.Text);
                    _timeTrialTelemetryDisplay.Show(this);
                }
                else if (!_timeTrialTelemetryDisplay.Visible)
                    _timeTrialTelemetryDisplay.Show(this);
                else
                    _timeTrialTelemetryDisplay.Focus();
                return;
            }
            if (_timeTrialTelemetryDisplay != null && _timeTrialTelemetryDisplay.Visible)
                _timeTrialTelemetryDisplay.Hide();
        }

        private void UpdatePolygonButtons()
        {
            bool markerSelected = listBoxPolygonMarkers.SelectedIndex > -1;
            buttonPolygonMarkerEdit.Enabled = markerSelected;
            buttonPolygonMarkerTarget.Enabled = markerSelected;
            buttonPolygonMarkerUseCurrentLocation.Enabled = markerSelected;
            buttonPolygonMarkerDelete.Enabled = markerSelected;
        }

        private void listBoxPolygonMarkers_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePolygonButtons();
        }

        private void buttonGenerateCircumnavigationWaypoints_Click(object sender, EventArgs e)
        {
            bool haveValidLocation = (FormTracker.CurrentLocation != null) && (FormTracker.CurrentLocation.PlanetaryRadius>0);
            addNorthPoleToolStripMenuItem.Enabled = haveValidLocation;
            addSouthPoleToolStripMenuItem.Enabled = haveValidLocation;
            addEquatorToolStripMenuItem.Enabled = haveValidLocation;
            addPrimeMeridianToolStripMenuItem.Enabled = haveValidLocation;
            addOriginToolStripMenuItem.Enabled = haveValidLocation;
            circumnavigationContextMenuStrip.Show(buttonGenerateCircumnavigationWaypoints, new Point(buttonGenerateCircumnavigationWaypoints.Width, 0));
        }

        private void addNorthPoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormTracker.CurrentLocation == null)
                return;

            EDLocation northPole = FormTracker.CurrentLocation.Copy();  // So that we get radius and planet information
            northPole.Latitude = 90;
            northPole.Longitude = 0;
            northPole.Name = "North Pole";
            AddLocationToRoute(northPole);
        }

        private void addSouthPoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormTracker.CurrentLocation == null)
                return;

            EDLocation southPole = FormTracker.CurrentLocation.Copy();  // So that we get radius and planet information
            southPole.Latitude = -90;
            southPole.Longitude = 0;
            southPole.Name = "South Pole";
            AddLocationToRoute(southPole);
        }

        private void addEquatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormTracker.CurrentLocation == null)
                return;

            EDLocation equatorClosest = FormTracker.CurrentLocation.Copy();  // So that we get radius and planet information
            equatorClosest.Latitude = 0;
            equatorClosest.Name = "Equator";
            AddLocationToRoute(equatorClosest);
        }

        private void addPrimeMeridianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormTracker.CurrentLocation == null)
                return;

            EDLocation primeMeridian = FormTracker.CurrentLocation.Copy();  // So that we get radius and planet information
            primeMeridian.Longitude = 0;
            primeMeridian.Name = "Prime Meridian";
            AddLocationToRoute(primeMeridian);
        }

        private void addOriginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormTracker.CurrentLocation == null)
                return;

            EDLocation origin = FormTracker.CurrentLocation.Copy();  // So that we get radius and planet information
            origin.Latitude = 0;
            origin.Longitude = 0;
            origin.Name = "Origin";
            AddLocationToRoute(origin);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxRouteTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonCreateWaypoints.Enabled = comboBoxGenerationRouteTemplate.SelectedIndex > -1;
        }

        private void buttonCreateWaypoints_Click(object sender, EventArgs e)
        {
            if (FormTracker.CurrentLocation.PlanetaryRadius < 1)
                return;

            List<EDWaypoint> routeWaypoints = null;
            int waypointSeparationDistance = (int)numericUpDownGenerationDistanceBetweenWaypoint.Value;
            for (int i = 0; i < comboBoxGenerationDistanceBetweenWaypointUnit.SelectedIndex; i++)
                waypointSeparationDistance = waypointSeparationDistance * 100;

            switch (comboBoxGenerationRouteTemplate.SelectedIndex)
            {
                case 0: // Circumnavigation: from current position via North and South Pole
                    routeWaypoints = RouteGenerator.PoleToPole(FormTracker.CurrentLocation.PlanetaryRadius, waypointSeparationDistance,
                        FormTracker.CurrentLocation.Latitude, FormTracker.CurrentLocation.Longitude);
                    break;

                case 1: // Circumnavigation: around the equator
                    routeWaypoints = RouteGenerator.Equator(FormTracker.CurrentLocation.PlanetaryRadius, waypointSeparationDistance, FormTracker.CurrentLocation.Longitude);
                    break;
            }

            if (routeWaypoints != null)
            {
                // Add the waypoints to our route
                for (int i = 0; i < routeWaypoints.Count; i++)
                    AddWaypointToRoute(routeWaypoints[i]);
            }
        }

        private void buttonRandomizeWaypoints_Click(object sender, EventArgs e)
        {
            List<EDWaypoint> randomizedList = new List<EDWaypoint>();
            Random random = new Random();
            while (_route.Waypoints.Count>0)
            {
                int j = random.Next(0, _route.Waypoints.Count - 1);
                randomizedList.Add(_route.Waypoints[j]);
                _route.Waypoints.RemoveAt(j);
            }
            _route.Waypoints = randomizedList;
            DisplayRoute();
        }

        private void listBoxSpeechEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSpeechEvents.SelectedIndex<0)
            {
                textBoxSpeechEventPhrase.Text = "";
                textBoxSpeechEventPhrase.Enabled = false;
                return;
            }

            string selectedEvent = listBoxSpeechEvents.SelectedItem as string;
            if (_speechEventPhrases.ContainsKey(selectedEvent))
            {
                textBoxSpeechEventPhrase.Text = _speechEventPhrases[selectedEvent];
                textBoxSpeechEventPhrase.Enabled = true;
            }
        }

        private void textBoxSpeechEventPhrase_Validated(object sender, EventArgs e)
        {
            if (listBoxSpeechEvents.SelectedIndex < 0)
                return;

            string selectedEvent = listBoxSpeechEvents.SelectedItem as string;
            if (_speechEventPhrases.ContainsKey(selectedEvent))
            {
                _speechEventPhrases[selectedEvent] = textBoxSpeechEventPhrase.Text;
                StoreSpeechSettings();
            }
        }

        private void comboBoxSelectedVoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSelectedVoice.SelectedIndex < 0 || _speechSynthesizer==null)
                return;

            _speechSynthesizer.SelectVoice(comboBoxSelectedVoice.SelectedItem as string);
        }
    }
}
