using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace EDTracking
{
    public partial class FormTelemetrySettings : Form
    {
        private TelemetryWriter _telemetryWriter = null;
        private Dictionary<string, String> _reportDescriptions = null;
        private string _filePrefix = "";
        private Control _exportControl = null;
        private ConfigSaverClass _formConfig = null;
        public event EventHandler SelectedReportsChanged;

        public FormTelemetrySettings(TelemetryWriter telemetryWriter, Dictionary<string, string> ReportDescriptions, string ExportFilePrefix="", string WindowTitle="")
        {
            InitializeComponent();
            _telemetryWriter = telemetryWriter;
            _reportDescriptions = ReportDescriptions;
            _filePrefix = ExportFilePrefix;

            if (!String.IsNullOrEmpty(WindowTitle))
                this.Text = WindowTitle;
            _formConfig = new ConfigSaverClass(this, true, true);
            _formConfig.StoreControlInfo = false;
            _formConfig.SaveEnabled = true;
            _formConfig.RestoreFormValues();

            InitialiseGrid();
            if (String.IsNullOrEmpty(_telemetryWriter.ExportDirectory))
            {
                radioButtonExportToApplicationFolder.Checked = true;
                textBoxExportFolder.Enabled = false;
                buttonBrowseExportFolder.Enabled = false;
            }
            else
            {
                textBoxExportFolder.Text = _telemetryWriter.ExportDirectory;
                radioButtonExportToOtherFolder.Checked = true;
            }
        }

        public void ExportToControlTag(Control ExportControl)
        {
            // Set the control to which we'll export the settings
            // We set the Tag property of the control to the telemetry JSON
            _exportControl = ExportControl;
        }

        private void InitialiseGrid()
        {
            // Show the current export settings in the ListView
            // Description, ReportName, Export File, Export Enabled, Display enabled

            dataGridViewExportSettings.Columns.Clear();
            dataGridViewExportSettings.ColumnCount = 6;
            dataGridViewExportSettings.Columns[0].Name = "Description";
            dataGridViewExportSettings.Columns[0].ReadOnly = true;
            dataGridViewExportSettings.Columns[0].Width = dataGridViewExportSettings.Columns[0].Width * 2;

            dataGridViewExportSettings.Columns[1].Name = "Report Name";
            dataGridViewExportSettings.Columns[1].ReadOnly = true;

            dataGridViewExportSettings.Columns[2].Name = "Export File";

            dataGridViewExportSettings.Columns[3].Name = "Export Enabled";
            dataGridViewExportSettings.Columns[3].Width = dataGridViewExportSettings.Columns[3].Width / 2;

            dataGridViewExportSettings.Columns[4].Name = "Display Name";

            dataGridViewExportSettings.Columns[5].Name = "Display Enabled";
            dataGridViewExportSettings.Columns[5].Width = dataGridViewExportSettings.Columns[5].Width / 2;

            foreach (string report in _reportDescriptions.Keys)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Cells.Add(new DataGridViewTextBoxCell());
                row.Cells.Add(new DataGridViewTextBoxCell());
                row.Cells.Add(new DataGridViewTextBoxCell());
                row.Cells.Add(new DataGridViewCheckBoxCell());
                row.Cells.Add(new DataGridViewTextBoxCell());
                row.Cells.Add(new DataGridViewCheckBoxCell());
                row.Cells[0].Value = _reportDescriptions[report];
                row.Cells[1].Value = report;
                row.Cells[2].Value = _telemetryWriter.ReportFileName(report);
                row.Cells[3].Value = _telemetryWriter.EnabledReports.Contains(report);
                row.Cells[4].Value = _telemetryWriter.ReportDisplayName(report);
                row.Cells[5].Value = _telemetryWriter.DisplayedReports.Contains(report);
                dataGridViewExportSettings.Rows.Add(row);
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
            if (!this.CanFocus)
                return;
            string reportName = e.Item.SubItems[1].Text;
            if (e.Item.Checked && !_telemetryWriter.EnabledReports.Contains(reportName))
            {
                _telemetryWriter.EnableReportExport(reportName, e.Item.SubItems[2].Text);
                SelectedReportsChanged?.Invoke(this, null);
            }
            else if (!e.Item.Checked && _telemetryWriter.EnabledReports.Contains(reportName))
            {
                _telemetryWriter.EnableReportExport(reportName, null);
                SelectedReportsChanged?.Invoke(this, null);
            }
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

        private void buttonBrowseExportFolder_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = textBoxExportFolder.Text;
                saveFileDialog.Filter = "text files (*.txt)|*.edrace|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = "Race-Report.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
                        string directory = fileInfo.DirectoryName;
                        if (!directory.EndsWith("\\"))
                            directory = $"{directory}\\";
                        textBoxExportFolder.Text=directory;
                        _telemetryWriter.ExportDirectory = textBoxExportFolder.Text;
                    }
                    catch { }
                }
            }
        }

        private void dataGridViewExportSettings_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.CanFocus || e.RowIndex>=dataGridViewExportSettings.Rows.Count || e.RowIndex<0)
                return;

            string reportName = (string)dataGridViewExportSettings.Rows[e.RowIndex].Cells[1].Value;
            string reportFileName = "";
            string reportDisplayName = "";
            switch (e.ColumnIndex)
            {
                case 2: // Report filename changed
                    reportFileName = (string)dataGridViewExportSettings.Rows[e.RowIndex].Cells[2].Value;
                    _telemetryWriter.EnableReportExport(reportName, reportFileName);
                    break;

                case 3: // Report enabled status changed
                    bool reportEnabled = (bool)dataGridViewExportSettings.Rows[e.RowIndex].Cells[3].Value;
                    if (reportEnabled)
                    {
                        reportFileName = $"{_filePrefix}{reportName}.txt";
                        dataGridViewExportSettings.Rows[e.RowIndex].Cells[2].Value = reportFileName;
                    }
                    else
                        dataGridViewExportSettings.Rows[e.RowIndex].Cells[2].Value = "";
                    _telemetryWriter.EnableReportExport(reportName, reportFileName);
                    break;

                case 4: // Report display name changed
                    reportDisplayName = (string)dataGridViewExportSettings.Rows[e.RowIndex].Cells[4].Value;
                    _telemetryWriter.EnableReportDisplay(reportName, reportDisplayName);
                    break;

                case 5: // Report display enabled changed
                    bool reportDisplayEnabled = (bool)dataGridViewExportSettings.Rows[e.RowIndex].Cells[5].Value;
                    if (reportDisplayEnabled)
                    {
                        reportDisplayName = reportName;
                        dataGridViewExportSettings.Rows[e.RowIndex].Cells[4].Value = reportDisplayName;
                    }
                    else
                        dataGridViewExportSettings.Rows[e.RowIndex].Cells[4].Value = "";
                    _telemetryWriter.EnableReportDisplay(reportName, reportDisplayName);
                    break;
            }
            SelectedReportsChanged?.Invoke(this, null);
        }

        private void dataGridViewExportSettings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 5)
            {
                dataGridViewExportSettings.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dataGridViewExportSettings_CellValueChanged(sender, e);
            }
        }

        private void dataGridViewExportSettings_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewExportSettings.IsCurrentCellDirty && 
                (dataGridViewExportSettings.CurrentCell.ColumnIndex == 3 || dataGridViewExportSettings.CurrentCell.ColumnIndex == 5))
                dataGridViewExportSettings.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridViewExportSettings_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridViewExportSettings_CellValueChanged(sender, e);
        }
    }
}
