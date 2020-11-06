using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Race_Manager
{
    public partial class FormFileExportSettings : Form
    {
        private TelemetryWriter _telemetryWriter = null;
        private Dictionary<string, String> _reportDescriptions = null;
        private string _filePrefix = "";
        private Control _exportControl = null;

        public FormFileExportSettings(TelemetryWriter telemetryWriter, Dictionary<string, string> ReportDescriptions, string ExportFilePrefix="", string WindowTitle="")
        {
            InitializeComponent();
            _telemetryWriter = telemetryWriter;
            _reportDescriptions = ReportDescriptions;
            _filePrefix = ExportFilePrefix;
            if (!String.IsNullOrEmpty(WindowTitle))
                this.Text = WindowTitle;
            InitialiseList();
        }

        public void ExportToControlTag(Control ExportControl)
        {
            // Set the control to which we'll export the settings
            // We set the Tag property of the control to the telemetry JSON
            _exportControl = ExportControl;
        }

        private void InitialiseList()
        {
            // Show the current export settings in the ListView
            listViewTextExportSettings.Items.Clear();
            foreach (string report in _reportDescriptions.Keys)
            {
                ListViewItem lvItem = new ListViewItem(_reportDescriptions[report]);
                lvItem.SubItems.Add(report);
                lvItem.SubItems.Add($"{_filePrefix}{report}.txt");
                if (_telemetryWriter.EnabledReports.Contains(report))
                    lvItem.Checked = true;
                listViewTextExportSettings.Items.Add(lvItem);
            }
        }

        private void UpdateLocationUI()
        {
            if (radioButtonExportToApplicationFolder.Checked)
            {
                textBoxExportFolder.Enabled = false;
                buttonBrowseExportFolder.Enabled = false;
                _telemetryWriter.ExportDirectory = "";
                return;
            }
            textBoxExportFolder.Enabled = true;
            buttonBrowseExportFolder.Enabled = true;
            _telemetryWriter.ExportDirectory = textBoxExportFolder.Text;
        }

        private void listViewTextExportSettings_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            string reportName = e.Item.SubItems[1].Text;
            if (e.Item.Checked && !_telemetryWriter.EnabledReports.Contains(reportName))
                _telemetryWriter.EnableReport(reportName, e.Item.SubItems[2].Text);
            else if (!e.Item.Checked && _telemetryWriter.EnabledReports.Contains(reportName))
                _telemetryWriter.EnableReport(reportName, null);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButtonExportToApplicationFolder_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLocationUI();
        }

        private void radioButtonExportToOtherFolder_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLocationUI();
        }

        private void textBoxExportFolder_Validating(object sender, CancelEventArgs e)
        {
            _telemetryWriter.ExportDirectory = textBoxExportFolder.Text;
        }

        private void FormFileExportSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_exportControl != null)
            {
                Action action = new Action(() =>
                {
                    _exportControl.Tag = _telemetryWriter.ToJson();
                });
                if (_exportControl.InvokeRequired)
                    _exportControl.Invoke(action);
                else
                    action();
            }
        }
    }
}
