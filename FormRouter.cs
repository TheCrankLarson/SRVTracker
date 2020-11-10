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

        public FormRouter(FormTracker formTracker)
        {
            InitializeComponent();
            CalculateWindowSizes();
            this.Size = _fullSize;

            // Attach our form configuration saver
            _formConfig = new ConfigSaverClass(this, true);
            _formConfig.ExcludedControls.Add(textBoxWaypointName);
            _formConfig.ExcludedControls.Add(textBoxRouteName);
            _formConfig.SaveEnabled = true;
            _formConfig.RestoreFormValues();

            _route = new EDRoute();
            _formTracker = formTracker;
            FormTracker.CommanderLocationChanged += FormTracker_CommanderLocationChanged;
            buttonStop.Enabled = false;
            listBoxWaypoints_SelectedIndexChanged(null, null);
            comboBoxWaypointType.SelectedIndex = 0;
            InitSoundsList();
        }

        private void CalculateWindowSizes()
        {
            // Calculate size with locations hidden
            int leftBound = groupBoxAudioSettings.Location.X + groupBoxAudioSettings.Width;
            _fullSize.Width = (this.Width - this.ClientRectangle.Width) + leftBound + 6;
            int bottomBound = groupBoxAudioSettings.Location.Y + groupBoxAudioSettings.Height;
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
        }

        private string GetBearingAfterNextWaypoint()
        {
            if (!checkBoxPlayIncudeDirection.Checked)
                return "";

            StringBuilder additionalInfo = new StringBuilder();
            if (_nextWaypoint < _route.Waypoints.Count - 1)
            {
                if (FormTracker.CurrentLocation != null)
                {
                    decimal bearingFromNextWaypoint = EDLocation.BearingToLocation(_route.Waypoints[_nextWaypoint].Location, _route.Waypoints[_nextWaypoint + 1].Location);
                    decimal bearingToNextWaypoint = EDLocation.BearingToLocation(FormTracker.CurrentLocation, _route.Waypoints[_nextWaypoint].Location);
                    decimal bearingChange = EDLocation.BearingDelta(bearingToNextWaypoint, bearingFromNextWaypoint);
                    additionalInfo.Append("Then ");
                    if (bearingChange > -5 && bearingChange < 5)
                        additionalInfo.Append("straight on");
                    else
                    {
                        if (bearingChange < 0)
                        {
                            if (bearingChange < -140)
                                additionalInfo.Append("hairpin ");
                            else if (bearingChange < -90)
                                additionalInfo.Append("sharp ");
                            if (bearingChange > -45)
                                additionalInfo.Append("bear left ");
                            else
                                additionalInfo.Append("turn left ");
                        }
                        else
                        {
                            if (bearingChange > 140)
                                additionalInfo.Append("hairpin ");
                            else if (bearingChange > 90)
                                additionalInfo.Append("sharp ");
                            if (bearingChange < 45)
                                additionalInfo.Append("bear right ");
                            else
                                additionalInfo.Append("turn right ");
                        }
                        additionalInfo.Append(Math.Abs(bearingChange).ToString("F1"));
                        additionalInfo.Append("°");
                    }
                }
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
                // We are currently replaying a route
                EDLocation previousWaypointLocation = null;
                if (_nextWaypoint > 0)
                    previousWaypointLocation = _route.Waypoints[_nextWaypoint - 1].Location;
                bool moveToNextWaypoint = _route.Waypoints[_nextWaypoint].WaypointHit(FormTracker.CurrentLocation, FormTracker.PreviousLocation, previousWaypointLocation);
                if (moveToNextWaypoint)
                {
                    // Arrived at the waypoint, target the next
                    _nextWaypoint++;
                    if (_nextWaypoint >= _route.Waypoints.Count)
                    {
                        // End of route
                        buttonStop.Enabled = false;
                        buttonStartRecording.Enabled = true;
                        PlayEventSound("Route completed");
                    }
                    else
                    {
                        PlayEventSound("Arrived at waypoint");
                        FormLocator.GetLocator().SetTarget(_route.Waypoints[_nextWaypoint].Location, GetBearingAfterNextWaypoint());
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
                else
                    FormLocator.GetLocator().SetAdditionalInfo(GetBearingAfterNextWaypoint());
                return;
            }

            if (EDLocation.DistanceBetween(_lastLoggedLocation, FormTracker.CurrentLocation) >= numericUpDownRecordDistance.Value)
            {
                _lastLoggedLocation = FormTracker.CurrentLocation.Copy();
                AddLocationToRoute(_lastLoggedLocation);
            }
        }

        private void AddLocationToRoute(EDLocation location)
        {
            EDWaypoint waypoint = new EDWaypoint(location, DateTime.Now, numericUpDownRadius.Value);
            waypoint.Radius = numericUpDownRadius.Value;
            waypoint.MinimumAltitude = numericUpDownMinAltitude.Value;
            waypoint.MaximumAltitude = numericUpDownMaxAltitude.Value;

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
                FormLocator.GetLocator().SetTarget(_route.Waypoints[listBoxWaypoints.SelectedIndex].Location);
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
                comboBoxWaypointType.SelectedIndex = 0;
            textBoxWaypointName.Text = waypoint.Name;
            numericUpDownRadius.Value = (decimal)waypoint.Radius;
            numericUpDownMinAltitude.Value = (decimal)waypoint.MinimumAltitude;
            numericUpDownMaxAltitude.Value = (decimal)waypoint.MaximumAltitude;
            checkBoxAllowPassing.Checked = waypoint.AllowPassing;
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
            _route.Waypoints[listBoxWaypoints.SelectedIndex].Radius = numericUpDownRadius.Value;
        }

        private void numericUpDownMinAltitude_ValueChanged(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0 || _updatingWaypointInfo)
                return;
            _route.Waypoints[listBoxWaypoints.SelectedIndex].MinimumAltitude = numericUpDownMinAltitude.Value;
        }

        private void numericUpDownMaxAltitude_ValueChanged(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 0 || _updatingWaypointInfo)
                return;
            _route.Waypoints[listBoxWaypoints.SelectedIndex].MaximumAltitude = numericUpDownMaxAltitude.Value;
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
            if (listBoxWaypoints.SelectedIndex > 0)
                _nextWaypoint = listBoxWaypoints.SelectedIndex;

            buttonStartRecording.Enabled = false;
            buttonStop.Enabled = true;
            _formTracker.StartTracking();
            FormLocator.GetLocator().SetTarget(_route.Waypoints[_nextWaypoint].Location, GetBearingAfterNextWaypoint());
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

        public void InitSoundsList()
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
                decimal distanceBetweenLocations = EDLocation.DistanceBetween(gateWaypoint.AdditionalLocations[0], gateWaypoint.AdditionalLocations[1]);
                decimal bearingToLocation = EDLocation.BearingToLocation(gateWaypoint.AdditionalLocations[0], gateWaypoint.AdditionalLocations[1]);
                gateWaypoint.Location = EDLocation.LocationFrom(gateWaypoint.AdditionalLocations[0], bearingToLocation, distanceBetweenLocations / 2);
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
                    ShowWaypointEditor(groupBoxBasic);
                    SetWaypointType(null);
                    break;

                case 1: // This is a gate
                    ShowWaypointEditor(groupBoxGate);
                    SetWaypointType("Gate");
                    DisplayGate();
                    break;
            }
        }

        private void buttonEditGateMarker1_Click(object sender, EventArgs e)
        {
            EDWaypoint waypoint = GetSelectedWaypoint();
            if (waypoint == null)
                return;
            FormAddLocation formAddLocation = new FormAddLocation();
            EDLocation location;
            if (waypoint.AdditionalLocations.Count > 0)
                location = waypoint.AdditionalLocations[0];
            else
            {
                location = waypoint.Location.Copy();
                waypoint.AdditionalLocations.Add(location);
            }
            formAddLocation.EditLocation(location, this, true);
            comboBoxGateLocation1.Text = location.Name;
        }

        private void buttonEditGateMarker2_Click(object sender, EventArgs e)
        {
            EDWaypoint waypoint = GetSelectedWaypoint();
            if (waypoint == null)
                return;
            FormAddLocation formAddLocation = new FormAddLocation();
            EDLocation location;
            if (waypoint.AdditionalLocations.Count > 1)
                location = waypoint.AdditionalLocations[1];
            else
            {
                if (waypoint.AdditionalLocations.Count > 0)
                {
                    location = waypoint.AdditionalLocations[0].Copy();
                }
                else
                {
                    location = waypoint.Location.Copy();
                    waypoint.AdditionalLocations.Add(null);
                }
                waypoint.AdditionalLocations.Add(location);
            }
            formAddLocation.EditLocation(location, this,true);
            comboBoxGateLocation2.Text = location.Name;
        }

        private void buttonEditGateTarget_Click(object sender, EventArgs e)
        {
            EDWaypoint waypoint = GetSelectedWaypoint();
            if (waypoint == null)
                return;
            FormAddLocation formAddLocation = new FormAddLocation();
            if (waypoint.Location == null)
                waypoint.Location = new EDLocation();
            formAddLocation.EditLocation(waypoint.Location, this,true);
            comboBoxGateTarget.Text = waypoint.Location.Name;
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
    }
}
