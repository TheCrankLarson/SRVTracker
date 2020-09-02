using EDTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace SRVTracker
{
    public partial class FormRaceHistory : Form
    {
        private Dictionary<String, EDRaceStatus> _raceStatuses = null;
        private string _serverRaceGuid = "";

        public FormRaceHistory(List<string> racers, string raceGuid)
        {
            InitializeComponent();
            buttonExport.Enabled = false;
            _serverRaceGuid = raceGuid;
            comboBoxCommander.Items.Clear();
            foreach (string commander in racers)
                comboBoxCommander.Items.Add(commander);
        }

        public FormRaceHistory(Dictionary<String, EDRaceStatus> raceStatuses)
        {
            _raceStatuses = raceStatuses;
            buttonExport.Enabled = false;
            comboBoxCommander.Items.Clear();
            foreach (string commander in _raceStatuses.Keys)
                comboBoxCommander.Items.Add(commander);
        }


        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GetCommanderHistory(string commander)
        {
            if (_raceStatuses == null)
            {
                // Need to retrieve race history from the server
                try
                {
                    using (WebClient webClient = new WebClient())
                    {
                        string raceStatus = webClient.DownloadString($"http://{FormLocator.ServerAddress}:11938/DataCollator/getcommanderraceevents/{_serverRaceGuid}/{commander}");
                        if (raceStatus.Length > 2)
                            return EDRaceStatus.FromJson(raceStatus).RaceReport;
                        else
                            return "No report found";
                    }
                }
                catch (Exception ex)
                {
                    return $"Error occurred while retrieving report:{Environment.NewLine}{ex}";
                }
            }
            else
            {
                if (_raceStatuses.ContainsKey(commander))
                    return _raceStatuses[commander].RaceReport;
            }
            return "No report found";
        }

        private void comboBoxCommander_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBoxRaceHistory.Text = GetCommanderHistory(comboBoxCommander.Text);
                buttonExport.Enabled = true;
            }
            catch { }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = $"{comboBoxCommander.Text}.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        System.IO.File.WriteAllText(saveFileDialog.FileName,_raceStatuses[comboBoxCommander.Text].RaceReport);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error on save: {ex.Message}", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonExportAll_Click(object sender, EventArgs e)
        {
            if (_raceStatuses.Count < 1)
                return;
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string commander in _raceStatuses.Keys)
                    {
                        try
                        {
                            System.IO.File.WriteAllText($"{folderDialog.SelectedPath}\\{commander}.txt", _raceStatuses[commander].RaceReport);
                        }
                        catch (Exception ex)
                        {
                            if (MessageBox.Show($"{commander}{Environment.NewLine}Error on save: {ex.Message}", "Save Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)==DialogResult.Cancel)
                                break;
                        }
                    }
                }
            }
        }
    }
}
