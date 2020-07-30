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

        public FormTracker()
        {
            InitializeComponent();
            InitClientId();
            InitStatusLocation();
            buttonTest.Visible = System.Diagnostics.Debugger.IsAttached;
        }

        private void InitClientId()
        {
            // Check if we have an Id saved, and if not, generate one
            string clientId = "";
            try
            {
                // Read the file
                clientId = System.IO.File.ReadAllText(ClientIdFile);
            }
            catch {}

            if (!String.IsNullOrEmpty(clientId))
            {
                textBoxClientId.Text = clientId;
                AddLog("Restored client Id");
                return;
            }

            AddLog("New client Id generated");
            textBoxClientId.Text = Guid.NewGuid().ToString();
            try
            {
                System.IO.File.AppendAllText(ClientIdFile, textBoxClientId.Text);
                AddLog($"Saved client Id to file: {ClientIdFile}");
            }
            catch (Exception ex)
            {
                AddLog($"Error saving client Id to file: {ex.Message}");
            }
        }

        public void InitStatusLocation()
        {
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\Saved Games\\Frontier Developments\\Elite Dangerous";
            if (System.IO.File.Exists($"{path}\\status.json"))
            {
                textBoxStatusFile.Text = path;
                return;
            }
        }

        private void statusFileWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            if (e.FullPath.ToLower().EndsWith("status.json"))
            {
                // Create a task to process the status (we return as quickly as possible from the event procedure
                Task.Factory.StartNew(() => ProcessStatusFileUpdate(e.FullPath));
            }
        }

        private void buttonTrack_Click(object sender, EventArgs e)
        {
            // We set up a watch on the status file to check for when it is written to

            if (buttonTrack.Text.Equals("Track"))
            {
                try
                {
                    statusFileWatcher.Path = textBoxStatusFile.Text;
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
                        _udpClient = new UdpClient(textBoxUploadServer.Text, 11938);
                    }
                    catch (Exception ex)
                    {
                        AddLog($"Error creating UDP client: {ex.Message}");
                        checkBoxUpload.Checked = false;
                    }
                }
                AddLog("Status tracking started");
                buttonTrack.Text = "Stop";
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
            buttonTrack.Text = "Track";
            AddLog("Status tracking stopped");
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
                using (FileStream fs = new FileStream(statusFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs))
                        status = sr.ReadToEnd();
                    fs.Close();
                }
            }
            catch  {}
            if (String.IsNullOrEmpty(status))
                return;

           // AddLog("Updated status file read");

            try
            {
                UpdateUI(new EDEvent(status));
            }
            catch { }

            try
            {
                System.IO.File.AppendAllText("status.log", status);
                AddLog("Status written to local log");
            }
            catch (Exception ex)
            {
                AddLog(ex.Message);
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

            // Update the UI with the event data
            Action action;
            action = new Action(() => { labelLastUpdateTime.Text = edEvent.TimeStamp.ToString("HH:MM:ss"); });
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

                action = new Action(() => { textBoxSRVHeading.Text = edEvent.Heading.ToString(); });
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
            try
            {
                System.IO.File.AppendAllText(ClientIdFile, textBoxClientId.Text);
                //AddLog($"Saved client Id to file: {ClientIdFile}"); // Too noisy, as it writes after every change! Too lazy to optimise this
            }
            catch (Exception ex)
            {
                AddLog($"Error saving client Id to file: {ex.Message}");
            }
        }
    }
}
