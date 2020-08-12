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
            }
            catch { }
        }
    }
}
