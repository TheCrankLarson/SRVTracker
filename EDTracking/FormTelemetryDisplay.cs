using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDTracking
{
    public partial class FormTelemetryDisplay : Form
    {
        private TelemetryWriter _telemetryWriter = null;
        private ConfigSaverClass _formConfig = null;
        private bool _colConfig = false;

        public FormTelemetryDisplay(TelemetryWriter telemetryWriter, string windowTitle = "Race Telemetry")
        {
            InitializeComponent();
            // Attach our form configuration saver
            this.Text = windowTitle;
            _formConfig = new ConfigSaverClass(this, true, true);
            _formConfig.StoreControlInfo = false;
            _formConfig.SaveEnabled = true;
            _formConfig.RestorePreviousSize = false;
            _formConfig.RestoreFormValues();
            _telemetryWriter = telemetryWriter;
            _telemetryWriter.SelectionChanged += _telemetryWriter_SelectionChanged;
            telemetryTable1.SetTelemetryWriter(_telemetryWriter);
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.elementHost1.AutoSize = true;
            this.elementHost1.Dock = DockStyle.Fill;
        }

        private void _telemetryWriter_SelectionChanged(object sender, EventArgs e)
        {
            if (_colConfig)
                telemetryTable1.InitialiseColumns();
            else
                telemetryTable1.InitialiseRows();
            elementHost1.OnPropertyChanged("dataGridTelemetry", telemetryTable1.dataGridTelemetry);
            elementHost1.Refresh();
        }

        public void InitialiseColumns(Dictionary<string, string> columnHeaderNames, int rowCount)
        {
            telemetryTable1.InitialiseColumns(columnHeaderNames, rowCount);
            _colConfig = true;
        }

        public void InitialiseRows(Dictionary<string, string> rowNames)
        {
            telemetryTable1.InitialiseRows(rowNames);
        }

        public void UpdateRaceData(Dictionary<string,string> ReportData)
        {
            telemetryTable1.UpdateRaceData(ReportData);
        }

        public void UpdateTargetData(Dictionary<string,string> TargetData)
        {
            telemetryTable1.UpdateTargetData(TargetData);
        }

        public void AddRow(string Title, string Description)
        {
            telemetryTable1.AddRow(Title, Description);
        }
    }
}
