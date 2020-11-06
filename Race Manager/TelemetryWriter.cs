using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Race_Manager
{
    public class TelemetryWriter
    {
        private Dictionary<string, string> _reportsToExport = new Dictionary<string, string>();
        private Dictionary<string, string> _lastReport = new Dictionary<string, string>();
        private string _exportDirectory = "";

        public bool ExportOnlyIfChanged { get; set; } = true;

        public TelemetryWriter()
        { }

        public TelemetryWriter(string json)
        {
            _reportsToExport = JsonSerializer.Deserialize< Dictionary<string, string>>(json);
            if (_reportsToExport.ContainsKey("ExportDirectory"))
            {
                _exportDirectory = _reportsToExport["ExportDirectory"];
                _reportsToExport.Remove("ExportDirectory");
            }
        }

        public string ExportDirectory
        {
            get { return _exportDirectory; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    if (value.EndsWith("\\"))
                        _exportDirectory = value;
                    else
                        _exportDirectory = $"{value}\\";
                else
                    _exportDirectory = "";
            }
        }

        public void ExportFiles(Dictionary<string,string> ReportSource)
        {
            // Export any matching reports

            foreach (string reportToExport in _reportsToExport.Keys)
                if (ReportSource.ContainsKey(reportToExport))
                {
                    if (ExportOnlyIfChanged && _lastReport.ContainsKey(reportToExport))
                        if (_lastReport[reportToExport].Equals(ReportSource[reportToExport]))
                            continue; // Data didn't change

                    // Export this report
                    try
                    {
                        File.WriteAllText($"{_exportDirectory}{_reportsToExport[reportToExport]}", ReportSource[reportToExport]);
                        if (_lastReport.ContainsKey(reportToExport))
                            _lastReport[reportToExport] = ReportSource[reportToExport];
                        else
                            _lastReport.Add(reportToExport, ReportSource[reportToExport]);
                    }
                    catch { }
                }
        }

        public List<string> EnabledReports
        {
            get { return _reportsToExport.Keys.ToList<string>(); }
        }

        public void EnableReport(string reportName, string exportFileName)
        {
            if (_reportsToExport.ContainsKey(reportName))
                _reportsToExport.Remove(reportName);
            if (String.IsNullOrEmpty(exportFileName))
                return;
            _reportsToExport.Add(reportName, exportFileName);
        }

        public string ToJson()
        {
            _reportsToExport.Add("ExportDirectory", _exportDirectory);
            string json = JsonSerializer.Serialize(_reportsToExport);
            _reportsToExport.Remove("ExportDirectory");
            return json;
        }
    }
}
