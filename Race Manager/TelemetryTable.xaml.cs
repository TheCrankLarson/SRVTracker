using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Race_Manager
{
    /// <summary>
    /// Interaction logic for TelemetryTable.xaml
    /// </summary>
    public partial class TelemetryTable : UserControl
    {
        private TelemetryWriter _telemetryWriter = null;

        public TelemetryTable()
        {
            InitializeComponent();
        }

        public TelemetryTable(TelemetryWriter telemetryWriter): this()
        {
            _telemetryWriter = telemetryWriter;
        }

        public void SetTelemetryWriter(TelemetryWriter telemetryWriter)
        {
            _telemetryWriter = telemetryWriter;
        }

        public void InitialiseColumns(Dictionary<string,string> columnHeaderNames)
        {
            List<string> columnHeaders = _telemetryWriter.EnabledReports;
            dataGridTelemetry.Columns.Clear();
            foreach (string columnHeader in columnHeaders)
                if (columnHeaderNames.ContainsKey(columnHeader))
                {
                    DataGridTextColumn dgColumn = new DataGridTextColumn();
                    dgColumn.Header = columnHeader;
                    dataGridTelemetry.Columns.Add(dgColumn);
                }
        }

        public void UpdateData(Dictionary<string, string> telemetryData)
        {
            dataGridTelemetry.Items.Clear();

        }
    }
}
