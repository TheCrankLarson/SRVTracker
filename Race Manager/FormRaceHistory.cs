﻿using EDTracking;
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

namespace Race_Manager
{
    public partial class FormRaceHistory : Form
    {
        private Dictionary<String, EDRaceStatus> _raceStatuses = null;
        private string _serverRaceGuid = "";

        public FormRaceHistory(List<string> racers, string raceGuid)
        {
            InitializeComponent();
            buttonExport.Enabled = false;
            buttonExportAll.Enabled = false;
            if (String.IsNullOrEmpty(raceGuid))
                return;
            _serverRaceGuid = raceGuid;
            comboBoxCommander.Items.Clear();
            foreach (string commander in racers)
                comboBoxCommander.Items.Add(commander);
            buttonExportAll.Enabled = racers.Count > 0;
        }

        public FormRaceHistory(Dictionary<String, EDRaceStatus> raceStatuses)
        {
            InitializeComponent();
            _raceStatuses = raceStatuses;
            buttonExport.Enabled = false;
            InitComboFromStatuses();
        }

        public FormRaceHistory(EDRace race)
        {
            InitializeComponent();
            _raceStatuses = race.Statuses;
            InitComboFromStatuses();
        }

        private void InitComboFromStatuses()
        {
            comboBoxCommander.Items.Clear();
            if (_raceStatuses.Count == 0)
                return;

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
                string statusUrl = $"http://<server>:11938/DataCollator/getcommanderracereport/{_serverRaceGuid}/{commander}";
                try
                {
                    using (WebClient webClient = new WebClient())
                    {
                        return webClient.DownloadString(statusUrl);
                    }
                }
                catch (Exception ex)
                {
                    return $"{statusUrl}{Environment.NewLine}{Environment.NewLine}Error occurred while retrieving report:{Environment.NewLine}{ex}";
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
                        System.IO.File.WriteAllText(saveFileDialog.FileName, GetCommanderHistory(comboBoxCommander.Text));
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
