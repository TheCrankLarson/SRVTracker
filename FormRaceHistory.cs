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

namespace SRVTracker
{
    public partial class FormRaceHistory : Form
    {
        private Dictionary<String, EDRaceStatus> _raceStatuses;

        public FormRaceHistory(Dictionary<String, EDRaceStatus> raceStatuses)
        {
            InitializeComponent();
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

        private void comboBoxCommander_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBoxRaceHistory.Text = _raceStatuses[comboBoxCommander.Text].RaceReport;
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
