using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;

namespace EDTracking
{
    /// <summary>
    /// Interaction logic for TelemetryTable.xaml
    /// </summary>
    public partial class TelemetryTable : System.Windows.Controls.UserControl
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

        private void UpdateDataGrid(bool includeLayout)
        {
            if (dataGridTelemetry.ItemsSource==null)
                dataGridTelemetry.ItemsSource = _telemetryTable.DefaultView;
            if (includeLayout)
                dataGridTelemetry.UpdateLayout();
            dataGridTelemetry.Items.Refresh();
        }

        private void InitialiseColumns(int RowCount)
        {
            List<string> enabledReports = _telemetryWriter.EnabledReports;
            _telemetryTable.Columns.Clear();
            // Enabled reports will not be in order, and we want to maintain the order in which reports are listed
            foreach (string columnHeader in _columnHeaderNames.Keys)
            {
                if (enabledReports.Contains(columnHeader))
                {
                    DataColumn dataColumn = new DataColumn(columnHeader, typeof(string));
                    _telemetryTable.Columns.Add(dataColumn);
                }
            }

            while (_telemetryTable.Rows.Count > RowCount)
                _telemetryTable.Rows[_telemetryTable.Rows.Count - 1].Delete();

            while (_telemetryTable.Rows.Count < RowCount)
                _telemetryTable.Rows.Add(_telemetryTable.NewRow());
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

            if (this.Dispatcher.CheckAccess())
                InitialiseColumns(rowCount);
            else
                this.Dispatcher.Invoke(() =>
                {
                    InitialiseColumns(rowCount);
                });

            if (dataGridTelemetry.Dispatcher.CheckAccess())
                UpdateDataGrid(true);
            else
                dataGridTelemetry.Dispatcher.Invoke(() =>
                {
                    UpdateDataGrid(true);
                });
        }

        private void InitialiseRows()
        {
            if (_telemetryTable.Columns.Count == 0)
            {
                _telemetryTable.Columns.Add(new DataColumn("Target", typeof(string)));
                _telemetryTable.Columns.Add(new DataColumn("Metric", typeof(string)));
            }
            dataGridTelemetry.HeadersVisibility = DataGridHeadersVisibility.None;

            List<string> enabledReports = _telemetryWriter.EnabledReports;
            _telemetryTable.Rows.Clear();
            foreach (string rowName in _rowHeaderNames.Keys)
            {
                if (enabledReports.Contains(rowName))
                {
                    _telemetryTable.Rows.Add(_telemetryTable.NewRow());
                    _telemetryTable.Rows[_telemetryTable.Rows.Count - 1][0] = rowName;
                }
            }
        }

        public void InitialiseRows(Dictionary<string,string> rowNames = null)
        {
            if (rowNames == null)
            {
                if (_rowHeaderNames == null)
                    return;
                rowNames = _rowHeaderNames;
            }
            else
                _rowHeaderNames = rowNames;

            if (this.Dispatcher.CheckAccess())
                InitialiseRows();
            else
                this.Dispatcher.Invoke(() =>
                {
                    InitialiseRows();
                });

            if (dataGridTelemetry.Dispatcher.CheckAccess())
                UpdateDataGrid(false);
            else
                dataGridTelemetry.Dispatcher.Invoke(() =>
                {
                    UpdateDataGrid(false);
                });
        }

        public void AddRow(string Title, string Description)
        {
            Action action = new Action(() =>
            {
                _telemetryTable.Rows.Add(_telemetryTable.NewRow());
                _telemetryTable.Rows[_telemetryTable.Rows.Count - 1][0] = Title;
                _telemetryTable.Rows[_telemetryTable.Rows.Count - 1][1] = Description;
            });

            if (this.Dispatcher.CheckAccess())
                action();
            else
                this.Dispatcher.Invoke(action);

            if (dataGridTelemetry.Dispatcher.CheckAccess())
                UpdateDataGrid(false);
            else
                dataGridTelemetry.Dispatcher.Invoke(() =>
                {
                    UpdateDataGrid(false);
                });
        }

        private void UpdateRaceData()
        {
            if (_telemetryData == null)
                return;

            Dictionary<string, string[]> rowData = new Dictionary<string, string[]>();
            int numRows = 0;
            for (int i = 0; i < _telemetryTable.Columns.Count; i++)
            {
                if (_telemetryData.ContainsKey(_telemetryTable.Columns[i].ColumnName))
                {
                    string[] rowText = _telemetryData[_telemetryTable.Columns[i].ColumnName].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
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
                    if (rowData.ContainsKey(_telemetryTable.Columns[i].ColumnName))
                    {
                        if (rowData[_telemetryTable.Columns[i].ColumnName].Length > j)
                            dataRow[i] = rowData[_telemetryTable.Columns[i].ColumnName][j];
                        else
                            dataRow[i] = "";
                    }
                }
            }
        }

        public void UpdateRaceData(Dictionary<string, string> telemetryData = null)
        {

            if (telemetryData == null)
            {
                if (_telemetryData == null)
                    return;
            }
            else
                _telemetryData = telemetryData;

            if (this.Dispatcher.CheckAccess())
                UpdateRaceData();
            else
                this.Dispatcher.Invoke(() =>
                {
                    UpdateRaceData();
                });

        }

        public void UpdateTargetData(Dictionary<string, string> TargetData)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (_telemetryTable.Rows.Count < 0)
                    return;

                for (int i = 0; i < _telemetryTable.Rows.Count; i++)
                {
                    if (TargetData.ContainsKey(_telemetryTable.Rows[i][0].ToString()))
                        _telemetryTable.Rows[i][1] = TargetData[_telemetryTable.Rows[i][0].ToString()];
                }
            });
        }

        public System.Drawing.Size DataGridSize
        {
            
            get { return new System.Drawing.Size((int)this.ActualWidth, (int)this.ActualHeight); }
        }
    }
}
