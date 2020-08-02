﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;

namespace SRVTracker
{
    public partial class FormTracker : Form
    {
        UdpClient _udpClient = null;
        const string ClientIdFile = "client.id";
        private string _lastUpdateTime = "";
        private FormLocator _formLocator = null;
        private FileStream _statusFileStream = null;

        public FormTracker()
        {
            InitializeComponent();
            InitClientId();
            InitStatusLocation();
            buttonTest.Visible = System.Diagnostics.Debugger.IsAttached;
            FormLocator.ServerAddress = (string)radioButtonUseDefaultServer.Tag;
            _formLocator = new FormLocator();
        }

        private void InitClientId()
        {
            // Check if we have an Id saved, and if not, generate one
            string clientId = "";
            try
            {
                // Read the file
                clientId = File.ReadAllText(ClientIdFile);
            }
            catch {}

            if (!String.IsNullOrEmpty(clientId))
            {
                textBoxClientId.Text = clientId;
                AddLog("Restored client Id");
                return;
            }

            clientId = ReadCommanderNameFromJournal();
            if (String.IsNullOrEmpty(clientId))
            {
                AddLog("New client Id generated");
                textBoxClientId.Text = Guid.NewGuid().ToString();
            }
            try
            {
                File.WriteAllText(ClientIdFile, textBoxClientId.Text);
                AddLog($"Saved client Id to file: {ClientIdFile}");
            }
            catch (Exception ex)
            {
                AddLog($"Error saving client Id to file: {ex.Message}");
            }
        }

        private string ReadCommanderNameFromJournal()
        {
            // E: D writes the commander name to the journal.  We locate the most recent journal and attempt to read it from there.
            string path = EDJournalPath();
            if (!String.IsNullOrEmpty(path))
            {
                string[] files = Directory.GetFiles(path, "Journal*.cache",SearchOption.TopDirectoryOnly);
            }
            return null; // Not currently implemented
        }

        public void InitStatusLocation()
        {
            if (File.Exists($"{EDJournalPath()}\\Status.json"))
            {
                textBoxStatusFile.Text = EDJournalPath();
                return;
            }
        }

        private string EDJournalPath()
        {
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\Saved Games\\Frontier Developments\\Elite Dangerous";
            if (Directory.Exists(path))
                return path;
            return "";
        }

        private void statusFileWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            if (e.FullPath.ToLower().EndsWith("status.json"))
            {
                // Create a task to process the status (we return as quickly as possible from the event procedure
                Task.Factory.StartNew(() => ProcessStatusFileUpdate(e.FullPath));
            }
        }

        private void AddLog(string log)
        {
            log = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}  {log}";
            Action addLogAction = new Action(() => {
                listBoxLog.BeginUpdate();
                listBoxLog.Items.Add(log);                
                if (listBoxLog.Items.Count > 200)
                {
                    int topOffset = listBoxLog.Items.Count - listBoxLog.TopIndex;
                    listBoxLog.Items.RemoveAt(0);

                    if ( !checkBoxAutoScroll.Checked && (listBoxLog.Items.Count - topOffset >= 0) )
                        listBoxLog.TopIndex = listBoxLog.Items.Count - topOffset;
                }
                if (checkBoxAutoScroll.Checked)
                {
                    listBoxLog.TopIndex = listBoxLog.Items.Count-1;
                }
                listBoxLog.EndUpdate();
            });
            if (listBoxLog.InvokeRequired)
            {
                listBoxLog.Invoke(addLogAction);
            }
            else
                addLogAction();
        }

        private void ProcessStatusFileUpdate(string statusFile)
        {
            // Read the status from the file and update the UI
            string status = "";
            try
            {
                // Read the file - we open in file share mode as E: D will be constantly writing to this file
                if (_statusFileStream == null )
                    _statusFileStream = new FileStream(statusFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                _statusFileStream.Seek(0, SeekOrigin.Begin);

                using (StreamReader sr = new StreamReader(_statusFileStream, Encoding.Default, true, 1000, true))
                    status = sr.ReadToEnd();

                if (!_statusFileStream.CanSeek)
                {
                    // We only close the file if we can't seek (no point in continuously reopening)
                    _statusFileStream.Close();
                    _statusFileStream = null;
                }
            }
            catch  {}
            if (String.IsNullOrEmpty(status))
                return;

            try
            {
                UpdateUI(new EDEvent(status));
            }
            catch { }

            try
            {
                if (checkBoxSaveToFile.Checked)
                    File.AppendAllText(textBoxSaveFile.Text, status);
            }
            catch (Exception ex)
            {
                AddLog($"Failed to save to local log file: {ex.Message}");
                Action action = new Action(() => { checkBoxSaveToFile.Checked = false; });
                if (checkBoxSaveToFile.InvokeRequired)
                    checkBoxSaveToFile.Invoke(action);
                else
                    action();
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            EDEvent edEvent;
            //"{ \"timestamp\":\"2020 - 07 - 28T17: 46:47Z\", \"event\":\"Status\", \"Flags\":16777229, \"Pips\":[4,8,0], \"FireGroup\":0, \"GuiFocus\":0, \"Fuel\":{ \"FuelMain\":16.000000, \"FuelReservoir\":0.430376 }, \"Cargo\":4.000000, \"LegalState\":\"Clean\" }");

            if (_udpClient == null)
                _udpClient = new UdpClient(textBoxUploadServer.Text, 11938);

            buttonTest.Enabled = false;

            Action action = new Action(() =>
            {
                double latitude = -14.055647;
                double longitude = -31.176170;
                Random rnd = new Random();

                for (int i = 0; i < 200; i++)
                {
                    edEvent = new EDEvent($"{{\"timestamp\":\"2020-07-28T17:52:25Z\", \"event\":\"Status\", \"Flags\":341852424, \"Pips\":[4,8,0], \"FireGroup\":0, \"GuiFocus\":0, \"Fuel\":{{\"FuelMain\":0.000000, \"FuelReservoir\":0.444637 }}, \"Cargo\":0.000000, \"LegalState\":\"Clean\", \"Latitude\":{latitude}, \"Longitude\":{longitude}, \"Heading\":24, \"Altitude\":0, \"BodyName\":\"Synuefe DJ-G b44-3 A 5\", \"PlanetRadius\":1311227.875000}}");
                    UpdateUI(edEvent);
                    System.Threading.Thread.Sleep(10);
                    if (rnd.Next(2) == 1)
                        latitude += rnd.NextDouble() / 100;
                    if (rnd.Next(2) == 1)
                        longitude += rnd.NextDouble() / 100;
                }
                Action enableTest = new Action(() => { buttonTest.Enabled = true; });
                if (buttonTest.InvokeRequired)
                    buttonTest.Invoke(enableTest);
                else
                    enableTest();
            });
            Task.Run(action);
        }

        private void UpdateUI(EDEvent edEvent)
        {
            if (checkBoxSaveToFile.Checked)
                SaveToFile(edEvent);

            if (checkBoxUpload.Checked)
                UploadToServer(edEvent);

            if (edEvent.PlanetRadius > 0) 
                if (FormLocator.PlanetaryRadius != edEvent.PlanetRadius)
                    FormLocator.PlanetaryRadius = edEvent.PlanetRadius;

            // Update the UI with the event data
            Action action;
            if (!_lastUpdateTime.Equals(edEvent.TimeStamp.ToString("HH:MM:ss")))
            {
                _lastUpdateTime = edEvent.TimeStamp.ToString("HH:MM:ss");
                action = new Action(() => { labelLastUpdateTime.Text = _lastUpdateTime; });
                if (labelLastUpdateTime.InvokeRequired)
                    labelLastUpdateTime.Invoke(action);
                else
                    action();
            }

            if (edEvent.HasCoordinates)
            {
                if (_formLocator != null)
                    if (_formLocator.Visible)
                    {

                        action = new Action(() => { _formLocator.UpdateTracking(edEvent.Location); });
                        Task.Run(action);
                    }
            }

            action = new Action(() => { labelLastUpdateTime.Text = DateTime.Now.ToString("HH:mm:ss"); });
            if (labelLastUpdateTime.InvokeRequired)
                labelLastUpdateTime.Invoke(action);
            else
                action();

            if (edEvent.isInSRV)
            {
                // In SRV
                if (edEvent.HasCoordinates)
                {
                    action = new Action(() => { textBoxSRVLatitude.Text = edEvent.Latitude.ToString(); });
                    if (textBoxSRVLatitude.InvokeRequired)
                        textBoxSRVLatitude.Invoke(action);
                    else
                        action();

                    action = new Action(() => { textBoxSRVLongitude.Text = edEvent.Longitude.ToString(); });
                    if (textBoxSRVLongitude.InvokeRequired)
                        textBoxSRVLongitude.Invoke(action);
                    else
                        action();
                }

                action = new Action(() => { textBoxSRVHeading.Text = edEvent.Heading.ToString(); });
                if (textBoxSRVHeading.InvokeRequired)
                    textBoxSRVHeading.Invoke(action);
                else
                    action();
            }
            else if (edEvent.isInMainShip)
            {
                // In ship
                if (edEvent.HasCoordinates)
                {
                    action = new Action(() => { textBoxShipLatitude.Text = edEvent.Latitude.ToString(); });
                    if (textBoxShipLatitude.InvokeRequired)
                        textBoxShipLatitude.Invoke(action);
                    else
                        action();

                    action = new Action(() => { textBoxShipLongitude.Text = edEvent.Longitude.ToString(); });
                    if (textBoxShipLongitude.InvokeRequired)
                        textBoxShipLongitude.Invoke(action);
                    else
                        action();
                }

                action = new Action(() => { textBoxShipHeading.Text = edEvent.Heading.ToString(); });
                if (textBoxShipHeading.InvokeRequired)
                    textBoxShipHeading.Invoke(action);
                else
                    action();
            }

        }

        private void SaveToFile(EDEvent edEvent)
        {
            try
            {
                // This is very inefficient, save to file should only be enabled for debugging
                // I may revisit this at some point if more features are added for local tracking
                System.IO.File.AppendAllText(textBoxSaveFile.Text, edEvent.TrackingInfo);
            }
            catch (Exception ex)
            {
                AddLog($"Error saving to tracking log: {ex.Message}");
                checkBoxSaveToFile.Checked = false;
            }
        }

        private void UploadToServer(EDEvent edEvent)
        {
            if (String.IsNullOrEmpty(textBoxClientId.Text))
            {
                AddLog($"Client Id cannot be empty");
                checkBoxUpload.Checked = false;
                return;
            }
            try
            {
                string eventData = "";
                if (checkBoxSendLocationOnly.Checked)
                    eventData = $"{textBoxClientId.Text},{edEvent.TrackingInfo}";
                else
                    eventData = $"{textBoxClientId.Text}:{edEvent.RawData.Trim()}";
                Byte[] sendBytes = Encoding.ASCII.GetBytes(eventData);
                try
                {
                    _udpClient.Send(sendBytes, sendBytes.Length);
                    if (checkBoxShowLive.Checked)
                        AddLog($"Send {eventData}");
                }
                catch (Exception e)
                {
                    AddLog($"Failed to send UDP update: {e.Message}");
                }
            }
            catch (Exception ex)
            {
                AddLog($"Error uploading to server: {ex.Message}");
                checkBoxUpload.Checked = false;
            }
        }

        private void buttonBrowseStatusFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = textBoxStatusFile.Text;
                openFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = false;
                openFileDialog.FileName = "Status.json";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        textBoxStatusFile.Text = new FileInfo(openFileDialog.FileName).Directory.FullName;
                    }
                    catch { }
                }
            }
        }

        private void textBoxClientId_TextChanged(object sender, EventArgs e)
        {
            if (textBoxClientId.Text.Contains(','))
                textBoxClientId.Text = textBoxClientId.Text.Replace(",","");
            try
            {
                File.WriteAllText(ClientIdFile, textBoxClientId.Text);
                //AddLog($"Saved client Id to file: {ClientIdFile}"); // Too noisy, as it writes after every change! Too lazy to optimise this
            }
            catch (Exception ex)
            {
                AddLog($"Error saving client Id to file: {ex.Message}");
            }
        }

        private void buttonShowConfig_Click(object sender, EventArgs e)
        {
            if (buttonShowConfig.Text.Equals("Show Config"))
            {
                buttonShowConfig.Text = "Hide Config";
                this.Width = 743;
                this.Height = 420;
                this.Refresh();
                return;
            }

            buttonShowConfig.Text = "Show Config";
            this.Width = 296;
            this.Height = 258;
            this.Refresh();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLocator_Click(object sender, EventArgs e)
        {
            if (_formLocator == null)
                _formLocator = new FormLocator();
            if (!_formLocator.Visible)
                _formLocator.Show();
        }

        private void textBoxUploadServer_TextChanged(object sender, EventArgs e)
        {
            FormLocator.ServerAddress = textBoxUploadServer.Text;
        }

        private void checkBoxUpload_CheckedChanged(object sender, EventArgs e)
        {
            UpdateServerSettings();
        }

        private void UpdateServerSettings()
        {
            radioButtonUseCustomServer.Enabled = checkBoxUpload.Checked;
            radioButtonUseDefaultServer.Enabled = checkBoxUpload.Checked;
            textBoxUploadServer.Enabled = checkBoxUpload.Checked && radioButtonUseCustomServer.Checked;
            if (radioButtonUseDefaultServer.Checked)
                FormLocator.ServerAddress = (string)radioButtonUseDefaultServer.Tag;
            else
                FormLocator.ServerAddress = textBoxUploadServer.Text;
        }

        private void checkBoxSaveToFile_CheckedChanged(object sender, EventArgs e)
        {
            textBoxSaveFile.Enabled = checkBoxSaveToFile.Checked;
        }

        private void radioButtonUseDefaultServer_CheckedChanged(object sender, EventArgs e)
        {
            UpdateServerSettings();
        }

        private void radioButtonUseCustomServer_CheckedChanged(object sender, EventArgs e)
        {
            UpdateServerSettings();
        }

        private void buttonRoutePlanner_Click(object sender, EventArgs e)
        {
            FormRoutePlanner formRoutePlanner = new FormRoutePlanner(_formLocator);
            formRoutePlanner.Show();
        }

        private void checkBoxTrack_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTrack.Checked)
            {
                try
                {
                    statusFileWatcher.Path = textBoxStatusFile.Text;
                    statusFileWatcher.Filter = "Status.json";
                    statusFileWatcher.NotifyFilter = System.IO.NotifyFilters.LastWrite;
                    statusFileWatcher.EnableRaisingEvents = true;
                }
                catch (Exception ex)
                {
                    AddLog($"Error initiating file watcher: {ex.Message}");
                    return;
                }
                if (checkBoxUpload.Checked)
                {
                    // Create the UDP client for sending tracking data
                    try
                    {
                        string serverUrl = (string)radioButtonUseDefaultServer.Tag;
                        if (radioButtonUseCustomServer.Checked)
                            serverUrl = textBoxUploadServer.Text;
                        _udpClient = new UdpClient(serverUrl, 11938);
                    }
                    catch (Exception ex)
                    {
                        AddLog($"Error creating UDP client: {ex.Message}");
                        checkBoxUpload.Checked = false;
                    }
                }
                AddLog("Status tracking started");
                return;
            }

            // Stop tracking
            statusFileWatcher.EnableRaisingEvents = false;
            try
            {
                _udpClient.Close();
            }
            catch { }
            _udpClient = null;
            AddLog("Status tracking stopped");
        }

        private void buttonRaceTracker_Click(object sender, EventArgs e)
        {
            FormRaceMonitor formRaceMonitor = new FormRaceMonitor();
            formRaceMonitor.Show();
        }
    }
}
