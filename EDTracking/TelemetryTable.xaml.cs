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
        private Dictionary<string, string> _columnHeaderNameToReportName = null;
        private Dictionary<string, string> _rowNameToReportName = null;
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
            Action action = new Action(() =>
            {
                if (dataGridTelemetry.ItemsSource == null)
                    dataGridTelemetry.ItemsSource = _telemetryTable.DefaultView;
                if (includeLayout)
                    dataGridTelemetry.UpdateLayout();
                dataGridTelemetry.Items.Refresh();
            });

            if (dataGridTelemetry.Dispatcher.CheckAccess())
                action();
            else
                dataGridTelemetry.Dispatcher.Invoke(action);
        }

        private void InitialiseColumns(int RowCount)
        {
            List<string> displayedReports = _telemetryWriter.DisplayedReports;
            _telemetryTable.Columns.Clear();
            _columnHeaderNameToReportName = new Dictionary<string, string>();
            // Enabled reports will not be in order, and we want to maintain the order in which reports are listed
            foreach (string columnHeader in _columnHeaderNames.Keys)
            {
                if (displayedReports.Contains(columnHeader))
                {
                    DataColumn dataColumn = new DataColumn(_telemetryWriter.ReportDisplayName(columnHeader), typeof(string));
                    _telemetryTable.Columns.Add(dataColumn);
                    _columnHeaderNameToReportName.Add(dataColumn.ColumnName, columnHeader);
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

            UpdateDataGrid(true);
        }

        private void InitialiseRows()
        {
            if (_telemetryTable.Columns.Count == 0)
            {
                _telemetryTable.Columns.Add(new DataColumn("Target", typeof(string)));
                _telemetryTable.Columns.Add(new DataColumn("Metric", typeof(string)));
            }
            dataGridTelemetry.HeadersVisibility = DataGridHeadersVisibility.None;

            List<string> displayedReports = _telemetryWriter.DisplayedReports;
            _telemetryTable.Rows.Clear();
            _rowNameToReportName = new Dictionary<string, string>();
            foreach (string rowName in _rowHeaderNames.Keys)
            {
                if (displayedReports.Contains(rowName))
                {
                    _telemetryTable.Rows.Add(_telemetryTable.NewRow());
                    _telemetryTable.Rows[_telemetryTable.Rows.Count - 1][0] = _telemetryWriter.ReportDisplayName(rowName);
                    _rowNameToReportName.Add(_telemetryWriter.ReportDisplayName(rowName), rowName);
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

            UpdateDataGrid(false);
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

            UpdateDataGrid(false);
        }

        public void UpdateCell(int Column, int Row, string CellContent)
        {
            Action action = new Action(() =>
            {
                if (_telemetryTable.Rows.Count < Row)
                {
                    // If the row is one above our count, then we add a new row - otherwise, we do nothing
                    if (_telemetryTable.Rows.Count == Row - 1)
                        _telemetryTable.Rows.Add(_telemetryTable.NewRow());
                    else
                        return;
                }
                _telemetryTable.Rows[Row][Column] = CellContent;
            });

            if (this.Dispatcher.CheckAccess())
                action();
            else
                this.Dispatcher.Invoke(action);

            UpdateDataGrid(false);
        }

        private void UpdateRaceData()
        {
            if (_telemetryData == null)
                return;

            Dictionary<string, string[]> rowData = new Dictionary<string, string[]>();
            int numRows = 0;
            for (int i = 0; i < _telemetryTable.Columns.Count; i++)
            {
                if (_telemetryData.ContainsKey(_columnHeaderNameToReportName[_telemetryTable.Columns[i].ColumnName]))
                {
                    string[] rowText = _telemetryData[_columnHeaderNameToReportName[_telemetryTable.Columns[i].ColumnName]].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
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
                    if (TargetData == null)
                        _telemetryTable.Rows[i][1] = "";
                    else
                    {
                        
                        if (_rowNameToReportName.ContainsKey(_telemetryTable.Rows[i][0].ToString()))
                        {
                            string fieldName = _rowNameToReportName[_telemetryTable.Rows[i][0].ToString()];
                            if (TargetData.ContainsKey(fieldName))
                                _telemetryTable.Rows[i][1] = TargetData[fieldName];
                        }
                        
                    }
                }
            });
        }

        public System.Drawing.Size DataGridSize
        {
            
            get { return new System.Drawing.Size((int)this.ActualWidth, (int)this.ActualHeight); }
        }
    }
}
