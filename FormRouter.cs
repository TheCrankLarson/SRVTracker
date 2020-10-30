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

namespace SRVTracker
{
    public partial class FormRouter : Form
    {
        static Size _fullSize = new Size(484, 350);
        //static Size _miniSize = new Size(313, 291);
        static Size _chooseLocation = new Size(774, 350);
        private EDLocation _lastLoggedLocation = null;
        private int _nextWaypoint = 0;
        private EDRoute _route = null;
        private string _saveFileName = "";
        private FormTracker _formTracker = null; // We need a reference to the tracker so that we can start tracking if necessary
        private ConfigSaverClass _formConfig = null;
        private bool _updatingWaypointInfo = false;

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
            ConfigSaverClass.ApplyConfiguration();

            _route = new EDRoute();
            _formTracker = formTracker;
            FormTracker.CommanderLocationChanged += FormTracker_CommanderLocationChanged;
            buttonStop.Enabled = false;
            listBoxWaypoints_SelectedIndexChanged(null, null);
        }

        private void CalculateWindowSizes()
        {
            // Calculate size with locations hidden
            int leftBound = groupBoxWaypointInfo.Location.X + groupBoxWaypointInfo.Width;
            _fullSize.Width = (this.Width - this.ClientRectangle.Width) + leftBound + ((locationManager1.Left - leftBound) / 2);
            int bottomBound = groupBoxWaypointInfo.Location.Y + groupBoxWaypointInfo.Height;
            _fullSize.Height = (this.Height - this.ClientRectangle.Height) + bottomBound + 6;

            // Calculate size with locations displayed
            _chooseLocation.Width = locationManager1.Location.X + locationManager1.Width + (this.Width - this.ClientRectangle.Width) + 6;
            _chooseLocation.Height = locationManager1.Location.Y + locationManager1.Height + (this.Height - this.ClientRectangle.Height) + 6;
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
                            if (bearingChange < -90)
                                additionalInfo.Append("sharp ");
                            if (bearingChange > -45)
                                additionalInfo.Append("bear left ");
                            else
                                additionalInfo.Append("turn left ");
                        }
                        else
                        {
                            if (bearingChange > 90)
                                additionalInfo.Append("sharp ");
                            if (bearingChange < 45)
                                additionalInfo.Append("bear right ");
                            else
                                additionalInfo.Append("turn right ");
                        }
                        additionalInfo.Append(bearingChange.ToString("F1"));
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
                // We are currently tracking
                bool moveToNextWaypoint = _route.Waypoints[_nextWaypoint].LocationIsWithinWaypoint(FormTracker.CurrentLocation);
                if (!moveToNextWaypoint && (_route.Waypoints[_nextWaypoint].AllowPassing))
                    moveToNextWaypoint = _route.Waypoints[_nextWaypoint].WaypointIsBehind(FormTracker.CurrentLocation, FormTracker.CurrentHeading);
                if (moveToNextWaypoint)
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

            if (EDLocation.DistanceBetween(_lastLoggedLocation, FormTracker.CurrentLocation) >= (decimal)numericUpDownRecordDistance.Value)
            {
                _lastLoggedLocation = FormTracker.CurrentLocation;
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

            AddWaypointToRoute(waypoint);
        }

        private void AddWaypointToRoute(EDWaypoint waypoint)
        {
            _route.Waypoints.Add(waypoint);
            Action action = new Action(() =>
            {
                listBoxWaypoints.Items.Add(waypoint.Name);
                listBoxWaypoints.SelectedIndex = listBoxWaypoints.Items.Count - 1;
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
            if (this.Size == _fullSize)
            {
                locationManager1.ClearSelection();
                this.Size = _chooseLocation;
            }
            else
                this.Size = _fullSize;
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

        private void locationManager1_SelectionChanged(object sender, EventArgs e)
        {
            EDLocation selectedLocation = locationManager1.SelectedLocation;
            if (selectedLocation != null)
            {
                AddLocationToRoute(locationManager1.SelectedLocation);
                this.Size = _fullSize;
            }
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


    }
}
