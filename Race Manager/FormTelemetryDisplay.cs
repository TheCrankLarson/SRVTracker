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
    public partial class FormTelemetryDisplay : Form
    {
        private TelemetryWriter _telemetryWriter = null;

        public FormTelemetryDisplay(TelemetryWriter telemetryWriter, string windowTitle = "Race Telemetry")
        {
            InitializeComponent();
            _telemetryWriter = telemetryWriter;
            telemetryTable1.SetTelemetryWriter(_telemetryWriter);
            this.Text = windowTitle;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.elementHost1.AutoSize = true;
            this.elementHost1.Dock = DockStyle.Fill;
        }

        public void InitialiseColumns(Dictionary<string, string> columnHeaderNames, int rowCount)
        {
            telemetryTable1.InitialiseColumns(columnHeaderNames, rowCount);
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
    }
}
