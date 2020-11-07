using System;
using System.Collections.Generic;
using System.Data;
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
        private DataTable _telemetryTable = null;
        private Dictionary<string, string> _columnHeaderNames = null;
        private Dictionary<string, string> _rowHeaderNames = null;
        private Dictionary<string, string> _telemetryData = null;

        public TelemetryTable()
        {
            InitializeComponent();
            _telemetryTable = new DataTable("Telemetry");
        }

        public TelemetryTable(TelemetryWriter telemetryWriter): this()
        {
            _telemetryWriter = telemetryWriter;
        }

        public void SetTelemetryWriter(TelemetryWriter telemetryWriter)
        {
            _telemetryWriter = telemetryWriter;
            _telemetryWriter.SelectionChanged += _telemetryWriter_SelectionChanged;
        }

        private void _telemetryWriter_SelectionChanged(object sender, EventArgs e)
        {
            InitialiseColumns();
            UpdateRaceData();
        }

        public void InitialiseColumns(Dictionary<string,string> columnHeaderNames = null, int rowCount = 20)
        {
            if (columnHeaderNames == null)
            {
                rowCount = 0;
                if (_columnHeaderNames == null)
                    return;
                columnHeaderNames = _columnHeaderNames;
            }
            else
                _columnHeaderNames = columnHeaderNames;

            _telemetryTable.Columns.Clear();
            List<string> enabledReports = _telemetryWriter.EnabledReports;
            // Enabled reports will not be in order, and we want to maintain the order in which reports are listed
            foreach (string columnHeader in columnHeaderNames.Keys)
            {
                if (enabledReports.Contains(columnHeader))
                {
                    DataColumn dataColumn = new DataColumn(columnHeader, typeof(string));
                    _telemetryTable.Columns.Add(dataColumn);
                }
            }

            while (_telemetryTable.Rows.Count > rowCount)
                _telemetryTable.Rows[_telemetryTable.Rows.Count-1].Delete();

            while (_telemetryTable.Rows.Count<rowCount)
                _telemetryTable.Rows.Add(_telemetryTable.NewRow());

            dataGridTelemetry.ItemsSource = _telemetryTable.DefaultView;
        }

        public void InitialiseRows(Dictionary<string,string> rowNames = null)
        {
            if (rowNames == null)
            {
                if (_columnHeaderNames == null)
                    return;
                rowNames = _rowHeaderNames;
            }
            else
                _rowHeaderNames = rowNames;

            _telemetryTable.Columns.Clear();
            _telemetryTable.Columns.Add(new DataColumn("Target", typeof(string)));
            _telemetryTable.Columns.Add(new DataColumn("Metric", typeof(string)));
            dataGridTelemetry.HeadersVisibility = DataGridHeadersVisibility.None;

            List<string> enabledReports = _telemetryWriter.EnabledReports;
            foreach (string rowName in rowNames.Keys)
            {
                if (enabledReports.Contains(rowName))
                {
                    _telemetryTable.Rows.Add(_telemetryTable.NewRow());
                    _telemetryTable.Rows[_telemetryTable.Rows.Count - 1][0] = rowName;
                }
            }

            dataGridTelemetry.ItemsSource = _telemetryTable.DefaultView;
        }

        public void UpdateRaceData(Dictionary<string, string> telemetryData = null)
        {
            if (telemetryData == null)
            {
                if (_telemetryData == null)
                    return;
                telemetryData = _telemetryData;
            }
            else
                _telemetryData = telemetryData;

            Dictionary<string, string[]> rowData = new Dictionary<string, string[]>();
            int numRows = 0;
            for (int i=0; i<_telemetryTable.Columns.Count; i++)
            {
                if (telemetryData.ContainsKey(_telemetryTable.Columns[i].ColumnName))
                {
                    string[] rowText = telemetryData[_telemetryTable.Columns[i].ColumnName].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    if (rowText.Length > numRows)
                        numRows = rowText.Length;
                    rowData.Add(_telemetryTable.Columns[i].ColumnName, rowText);
                }
            }
            if (rowData.Count < 1)
                return;

            if (numRows > _telemetryTable.Rows.Count)
                numRows = _telemetryTable.Rows.Count;

            for (int j = 0; j < numRows; j++)
            {
                DataRow dataRow = _telemetryTable.Rows[j];
                for (int i = 0; i < _telemetryTable.Columns.Count; i++)
                {
                    if (rowData[_telemetryTable.Columns[i].ColumnName].Length > j)
                        dataRow[i] = rowData[_telemetryTable.Columns[i].ColumnName][j];
                    else
                        dataRow[i] = "";
                }
            }
        }

        public void UpdateTargetData(Dictionary<string, string> TargetData)
        {
            if (_telemetryTable.Rows.Count < 0)
                return;

            for (int i=0; i<_telemetryTable.Rows.Count; i++)
            {
                if (TargetData.ContainsKey(_telemetryTable.Rows[i][0].ToString()))
                    _telemetryTable.Rows[i][1] = TargetData[_telemetryTable.Rows[i][0].ToString()];
            }
        }

        public System.Drawing.Size DataGridSize
        {
            
            get { return new System.Drawing.Size((int)this.ActualWidth, (int)this.ActualHeight); }
        }
    }
}
