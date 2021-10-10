using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using EDTracking;
using System.Security.Cryptography.X509Certificates;

namespace RaceTester
{
    public partial class FormRaceReplay : Form
    {
        private EDRace _race = null;
        private Dictionary<string, List<EDEvent>> _commanderTracking = null;
        private List<EDEvent> _orderedRaceTracking = null;
        private int _raceTrackingIndex = 0;
        private DateTime _playbackStartTime = DateTime.MinValue;
        private TimeSpan _raceTimeOffset = new TimeSpan(0);
        private Dictionary<Guid, string> _commanderGuids = new Dictionary<Guid, string>();
        private Dictionary<string, Guid> _commanderNames = new Dictionary<string, Guid>();
        UdpClient _udpClient = null;


        public FormRaceReplay()
        {
            InitializeComponent();
        }

        private void OpenRaceFromFolder()
        {
            string raceStartFile = $"{textBoxRaceDataFolder.Text}\\Race.Start";
            if (!File.Exists(raceStartFile))
                return;

            _race = EDRace.LoadFromFile(raceStartFile);
            if (_race == null)
                return;

            textBoxRaceName.Text = _race.Name;
            InitialiseRaceData();
            buttonPlay.Enabled = true;
        }

        private void InitialiseRaceData()
        {
            // Read all the individual tracking data and populate listbox with those we have data for
            listBoxParticipants.Items.Clear();
            _commanderTracking = new Dictionary<string, List<EDEvent>>();
            foreach (string contestant in _race.Contestants)
                if (LoadTrackingData(contestant))
                    listBoxParticipants.Items.Add(contestant);

            // Now combine all the events into one list, ordered by event time
            CreateOrderedTrackingData();
        }

        private void CreateOrderedTrackingData()
        {
            _orderedRaceTracking = new List<EDEvent>();
            foreach (string trackedCommander in _commanderTracking.Keys)
                _orderedRaceTracking.AddRange(_commanderTracking[trackedCommander]);
            _orderedRaceTracking.Sort(new EDEventTimeComparer());
            //File.WriteAllText("events.json", JsonSerializer.Serialize(_orderedRaceTracking));
            textBoxTotalNumberOfEvents.Text = _orderedRaceTracking.Count.ToString();

        }

        private bool LoadTrackingData(string commander)
        {
            // Load the race tracking data for this commander
            string trackingFile = $"{textBoxRaceDataFolder.Text}\\{commander}.Tracking";
            if (!File.Exists(trackingFile))
                return false;

            try
            {
                List<EDEvent> trackingData = (List<EDEvent>)JsonSerializer.Deserialize(File.ReadAllText(trackingFile), typeof(List<EDEvent>));
                _commanderTracking.Add(commander, trackingData);
                return true;
            }
            catch
            { }
            return false;
        }

        private string ServerUrl()
        {
            if (radioButtonUseDefaultServer.Checked)
                return (string)radioButtonUseDefaultServer.Tag;
            return textBoxUploadServer.Text;
        }

        private bool CreateUdpClient()
        {
            // Create the UDP client for sending tracking data
            try
            {
                _udpClient = new UdpClient(ServerUrl(), 11938);
                return true;
            }
            catch
            {
            }
            return false;
        }

        private void UploadToServer(EDEvent edEvent)
        {
            if (_udpClient == null)
            {
                if (!CreateUdpClient())
                    return;
            }
            edEvent.Commander = CommanderGuid(edEvent.Commander).ToString();
            try
            {
                string eventData = edEvent.ToJson();
                Byte[] sendBytes = Encoding.UTF8.GetBytes(eventData);
                try
                {
                    _udpClient.Send(sendBytes, sendBytes.Length);
                }
                catch { }
            }
            catch { }
        }

        private void UpdateServerUI()
        {
            textBoxUploadServer.Enabled = radioButtonUseCustomServer.Checked;
        }

        private void buttonOpenRaceFolder_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = textBoxRaceDataFolder.Text;
                openFileDialog.Filter = "Race.Start files|Race.Start|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FileName = "";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        textBoxRaceDataFolder.Text = new FileInfo(openFileDialog.FileName).Directory.FullName;
                        OpenRaceFromFolder();
                    }
                    catch { }
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (!CreateUdpClient())
                return;

            if (!LoadCommanderIds())
                return;

            if (!buttonPause.Enabled)
                CreateOrderedTrackingData();  // New run - so reset the data

            buttonPlay.Enabled = false;
            buttonPause.Enabled = true;
            buttonStop.Enabled = true;
            _playbackStartTime = DateTime.UtcNow;
            _raceTimeOffset = _playbackStartTime.Subtract(_orderedRaceTracking[0].TimeStamp);
            timerPlaybackEvents.Start();
        }

        private void timerPlaybackEvents_Tick(object sender, EventArgs e)
        {
            // Send any events
            if (_raceTrackingIndex >= _orderedRaceTracking.Count)
            {
                buttonStop_Click(null, null);
                return;
            }
            TimeSpan elapsedTime = DateTime.UtcNow - _playbackStartTime;
            elapsedTime = new TimeSpan(elapsedTime.Ticks * (long)numericUpDownPlaybackSpeed.Value);
            DateTime projectedNow = _playbackStartTime.Add(elapsedTime);

            if (_orderedRaceTracking[_raceTrackingIndex].TimeStamp.Add(_raceTimeOffset) > projectedNow)
                return;  // Next event isn't due yet

            Action action;
            while (_raceTrackingIndex < _orderedRaceTracking.Count && _orderedRaceTracking[_raceTrackingIndex].TimeStamp.Add(_raceTimeOffset) <= projectedNow)
            {
                action = new Action(() =>
                {
                    UploadToServer(_orderedRaceTracking[_raceTrackingIndex].Replay());
                });
                Task.Run(action);
                _raceTrackingIndex++;
            }

            action = new Action(() => { textBoxReplayedEvents.Text = _raceTrackingIndex.ToString(); });
            if (textBoxReplayedEvents.InvokeRequired)
                textBoxReplayedEvents.Invoke(action);
            else
                action();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timerPlaybackEvents.Stop();
            try
            {
                _udpClient.Dispose();
            }
            catch { }
            _udpClient = null;
            buttonStop.Enabled = false;
            buttonPause.Enabled = false;
            buttonPlay.Enabled = true;
            _raceTrackingIndex = 0;
        }

        private void radioButtonUseDefaultServer_CheckedChanged(object sender, EventArgs e)
        {
            UpdateServerUI();
        }

        private void radioButtonUseCustomServer_CheckedChanged(object sender, EventArgs e)
        {
            UpdateServerUI();
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            timerPlaybackEvents.Stop();
            _orderedRaceTracking.RemoveRange(0, _raceTrackingIndex);
            _raceTrackingIndex = 0;
            buttonPlay.Enabled = true;
        }

        private void buttonOpenCommanderIds_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = textBoxRaceDataFolder.Text;
                openFileDialog.Filter = "Commanders.json files|Commanders.json|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FileName = "";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        textBoxCommanderIdsFile.Text = openFileDialog.FileName;
                    }
                    catch { }
                }
            }
        }

        public Guid CommanderGuid(string commanderName)
        {
            if (String.IsNullOrEmpty(commanderName))
                return Guid.Empty;

            try
            {
                return _commanderNames[commanderName];
            }
            catch
            {
                return Guid.Empty;
            }
        }

        private bool CreateCommanderNames()
        {
            _commanderNames = new Dictionary<string, Guid>();
            foreach (Guid commanderGuid in _commanderGuids.Keys)
                _commanderNames.Add(_commanderGuids[commanderGuid], commanderGuid);
            return true;
        }

        private bool LoadCommanderIds()
        {
            if (String.IsNullOrEmpty(textBoxCommanderIdsFile.Text) || !File.Exists(textBoxCommanderIdsFile.Text))
                return false;

            try
            {
                string registeredCommanders = File.ReadAllText(textBoxCommanderIdsFile.Text);
                _commanderGuids = JsonSerializer.Deserialize<Dictionary<Guid, String>>(registeredCommanders);
                return CreateCommanderNames();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load commander registrations.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Error",
                    MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return false;
        }
    }

    class EDEventTimeComparer: IComparer<EDEvent>
    {
        public int Compare(EDEvent x, EDEvent y)
        {
            if (x.TimeStamp == y.TimeStamp)
                return 0;
            return x.TimeStamp.CompareTo(y.TimeStamp);
        }
    }
}
