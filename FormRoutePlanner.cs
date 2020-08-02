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

namespace SRVTracker
{
    public partial class FormRoutePlanner : Form
    {
        private string _saveFilename = "";
        private FormLocator _locatorForm = null;

        public FormRoutePlanner(FormLocator formLocator = null)
        {
            InitializeComponent();
            _locatorForm = formLocator;
            if (_locatorForm == null)
                buttonSetAsTarget.Enabled = false;
            UpdateButtons();
            locationManager.SelectionChanged += LocationManager_SelectionChanged;
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
                    listBoxWaypoints.Items.Add(new EDWaypoint(locationManager.SelectedLocation));
            }
            catch { }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex > -1)
                listBoxWaypoints.Items.RemoveAt(listBoxWaypoints.SelectedIndex);
        }

        private void buttonMoveWaypointUp_Click(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex < 1)
                return;

            try
            {
                EDLocation locationAbove = (EDLocation)listBoxWaypoints.Items[listBoxWaypoints.SelectedIndex - 1];
                listBoxWaypoints.Items[listBoxWaypoints.SelectedIndex - 1] = listBoxWaypoints.Items[listBoxWaypoints.SelectedIndex];
                listBoxWaypoints.Items[listBoxWaypoints.SelectedIndex] = locationAbove;
            }
            catch { }
        }

        private void buttonMoveWaypointDown_Click(object sender, EventArgs e)
        {
            if (listBoxWaypoints.SelectedIndex > listBoxWaypoints.SelectedIndex - 2)
                return;

            try
            {
                EDLocation locationAbove = (EDLocation)listBoxWaypoints.Items[listBoxWaypoints.SelectedIndex + 1];
                listBoxWaypoints.Items[listBoxWaypoints.SelectedIndex + 1] = listBoxWaypoints.Items[listBoxWaypoints.SelectedIndex];
                listBoxWaypoints.Items[listBoxWaypoints.SelectedIndex] = locationAbove;
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
                        Task.Run(new Action(() => { SaveRouteToFile(saveFileDialog.FileName); }));
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
                        Task.Run(new Action(() => { LoadRouteFromFile(openFileDialog.FileName); }));
                    }
                    catch { }
                }
            }
        }

        private void SaveRouteToFile(string filename)
        {
            try
            {
                StringBuilder waypoints = new StringBuilder();
                waypoints.AppendLine(textBoxRouteName.Text);
                foreach (EDWaypoint waypoint in listBoxWaypoints.Items)
                    waypoints.AppendLine(waypoint.ToString());
                File.WriteAllText(filename, waypoints.ToString());
                _saveFilename = filename;
            }
            catch { }
        }

        private void LoadRouteFromFile(string filename)
        {
            Action action = new Action(() => { listBoxWaypoints.Items.Clear(); });
            if (listBoxWaypoints.InvokeRequired)
                listBoxWaypoints.Invoke(action);
            else
                action();

            try
            {
                Stream statusStream = File.OpenRead(filename);
                
                action = new Action(() => { listBoxWaypoints.BeginUpdate(); });
                if (listBoxWaypoints.InvokeRequired)
                    listBoxWaypoints.Invoke(action);
                else
                    action();
                using (StreamReader reader = new StreamReader(statusStream))
                {
                    action = new Action(() => { textBoxRouteName.Text = reader.ReadLine(); });
                    if (textBoxRouteName.InvokeRequired)
                        textBoxRouteName.Invoke(action);
                    else
                        action();

                    while (!reader.EndOfStream)
                    {
                        try
                        {
                            EDWaypoint waypoint= EDWaypoint.FromString(reader.ReadLine());
                            action = new Action(() => { listBoxWaypoints.Items.Add(waypoint); });
                            if (listBoxWaypoints.InvokeRequired)
                                listBoxWaypoints.Invoke(action);
                            else
                                action();
                        }
                        catch { }
                    }
                    reader.Close();
                }
            }
            catch { }
            finally
            {
                action = new Action(() => { listBoxWaypoints.EndUpdate(); });
                if (listBoxWaypoints.InvokeRequired)
                    listBoxWaypoints.Invoke(action);
                else
                    action();
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
        }

        private void listBoxWaypoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }
    }
}
