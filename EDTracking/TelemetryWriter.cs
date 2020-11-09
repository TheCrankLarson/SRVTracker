using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using EDTracking;

namespace EDTracking
{
    public class TelemetryWriter
    {
        private Dictionary<string, string> _reportsToExport = new Dictionary<string, string>();
        private Dictionary<string, string> _lastReport = new Dictionary<string, string>();
        private Dictionary<string, string> _lastHTMLData = new Dictionary<string, string>();
        private string _exportDirectory = "";
        public event EventHandler SelectionChanged;

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
            ClearFiles();
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

        public void ClearFiles()
        {
            // Delete any files (do this at start or end of tracking)

            foreach (string reportToExport in _reportsToExport.Keys)
            {
                string reportFile = $"{_exportDirectory}{_reportsToExport[reportToExport]}";
                if (File.Exists(reportFile))
                {
                    try
                    {
                        File.Delete(reportFile);
                    }
                    catch { }
                }
            }
        }

        public List<string> EnabledReports
        {
            get { return _reportsToExport.Keys.ToList<string>(); }
        }

        public void EnableReport(string reportName, string exportFileName)
        {
            bool changeMade = false;
            if (_reportsToExport.ContainsKey(reportName))
            {
                _reportsToExport.Remove(reportName);
                changeMade = true;
            }
            if (!String.IsNullOrEmpty(exportFileName))
            {
                _reportsToExport.Add(reportName, exportFileName);
                changeMade = true;
            }
            if (changeMade)
                SelectionChanged?.Invoke(null, null);
        }

        public string ToJson()
        {
            _reportsToExport.Add("ExportDirectory", _exportDirectory);
            string json = JsonSerializer.Serialize(_reportsToExport);
            _reportsToExport.Remove("ExportDirectory");
            return json;
        }

        private string _htmlTemplateBeforeTable = "";
        private string _htmlTemplateAfterTable = "";
        private string _htmlRowTemplate = "";
        private string _lastHtml = "";
        private bool PrepareHTMLTemplate(string HTMLTemplate)
        {
            try
            {
                int tableStart = HTMLTemplate.IndexOf("<!-- #LEADERBOARD# -->");
                int tableEnd = HTMLTemplate.IndexOf("<!-- #/LEADERBOARD# -->") + 23;
                if (tableStart < 0 || tableEnd < 23)
                    return false;
                _htmlTemplateBeforeTable = HTMLTemplate.Substring(0, tableStart);
                _htmlTemplateAfterTable = HTMLTemplate.Substring(tableEnd);
                _htmlRowTemplate = HTMLTemplate.Substring(tableStart + 23, tableEnd - tableStart - 46);
                return true;
            }
            catch
            {
                _htmlTemplateBeforeTable = "";  // Force reload if HTML export enabled again
            }
            return false;
        }

        public string GenerateLeaderboardAsHTML(Dictionary<string, string> ReportSource, string HTMLTemplate)
        {
            if (String.IsNullOrEmpty(_htmlTemplateBeforeTable))
                if (!PrepareHTMLTemplate(HTMLTemplate))
                    return null;

            if (!ReportSource.ContainsKey("Positions"))
                return null;

            StringBuilder html = new StringBuilder(_htmlTemplateBeforeTable);

            Dictionary<string, string[]> dataPoints = new Dictionary<string, string[]>();
            foreach (string dataToExport in ReportSource.Keys)
                dataPoints.Add(dataToExport, ReportSource[dataToExport].Split(new string[] { Environment.NewLine }, StringSplitOptions.None));

            for (int i = 0; i < dataPoints["Positions"].Length; i++)
            {
                string rowHtml = _htmlRowTemplate;
                foreach (string dataPoint in dataPoints.Keys)
                    rowHtml = _htmlRowTemplate.Replace(dataPoint, dataPoints[dataPoint][i]);

                html.AppendLine(rowHtml);
            }
            html.AppendLine(_htmlTemplateAfterTable);

            return html.ToString();
        }
    }
}
