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
        }

        public void InitialiseColumns(Dictionary<string, string> columnHeaderNames)
        {
            telemetryTable1.InitialiseColumns(columnHeaderNames);
        }

        public void UpdateData(Dictionary<string,string> ReportData)
        {
            telemetryTable1.UpdateData(ReportData);
        }
    }
}
