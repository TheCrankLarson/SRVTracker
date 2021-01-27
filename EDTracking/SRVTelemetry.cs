using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDTracking
{
    public class SRVTelemetry
    {
        public int CurrentGroundSpeed { get; set; } = 0;
        public int AverageGroundSpeed { get; set; } = 0;
        public int MaximumGroundSpeed { get; set; } = 0;
        public double TotalDistanceTravelled { get; set; } = 0;
        public int TotalShipRepairs { get; set; } = 0;
        public int TotalSynthRepairs { get; set; } = 0;
        public static string SessionSaveFolder { get; set; } = "Session Telemetry";
        public DateTime SessionStartTime { get; set; } = DateTime.MinValue;
        private DateTime _lastEventTime = DateTime.MinValue;
        private EDLocation _lastLocation = null;
        private List<EDEvent> _sessionHistory = new List<EDEvent>();
        private EDLocation _speedCalculationPreviousLocation = null;
        private DateTime _speedCalculationTimeStamp = DateTime.UtcNow;
        private double[] _lastThreeSpeedReadings = new double[] { 0, 0, 0 };
        private int _oldestSpeedReading = 0;
        private int _numberOfSpeedReadings = 0;
        private double _totalOfSpeedReadings = 0;

        private TelemetryWriter _srvTelemetryWriter = null;
        private FormTelemetryDisplay _srvTelemetryDisplay = null;
        private FormTelemetrySettings _srvTelemetrySettings = null;
        private bool _showTelemetryDisplayOnSettingsClose = false;
        private Dictionary<string, string> _telemetry = new Dictionary<string, string>();


        public SRVTelemetry()
        {
            _srvTelemetryWriter = new TelemetryWriter();
            InitSession();
        }

        public SRVTelemetry(string settingJson, string commanderName): base()
        {
            if (!String.IsNullOrEmpty(settingJson))
                _srvTelemetryWriter = new TelemetryWriter(settingJson);
            else
                _srvTelemetryWriter = new TelemetryWriter();
            _telemetry.Add("CommanderName", commanderName);
        }

        private void InitSession()
        {
            CurrentGroundSpeed = 0;
            _numberOfSpeedReadings = 0;
            _totalOfSpeedReadings = 0;
            AverageGroundSpeed = 0;
            MaximumGroundSpeed = 0;
            TotalDistanceTravelled = 0;
            TotalShipRepairs = 0;
            TotalSynthRepairs = 0;
            SessionStartTime = DateTime.Now;

            _telemetry = new Dictionary<string, string>();
            _telemetry.Add("CurrentGroundSpeed", CurrentGroundSpeed.ToString());
            _telemetry.Add("AverageGroundSpeed", AverageGroundSpeed.ToString());
            _telemetry.Add("MaximumGroundSpeed", MaximumGroundSpeed.ToString());
            _telemetry.Add("TotalDistanceTravelled", TotalDistanceTravelled.ToString("F1"));
            _telemetry.Add("TotalShipRepairs", TotalShipRepairs.ToString());
            _telemetry.Add("TotalSynthRepairs", TotalSynthRepairs.ToString());
            _telemetry.Add("SessionStartTime", "NA");
            _telemetry.Add("SessionTime", "00:00:00");

            _sessionHistory = new List<EDEvent>();

        }

        public static Dictionary<string, string> TelemetryDescriptions()
        {
            return new Dictionary<string, string>()
                {
                    { "CommanderName", "Commander name" },
                    { "CurrentGroundSpeed", "Current ground speed in m/s" },
                    { "AverageGroundSpeed", "Average ground speed in m/s" },
                    { "MaximumGroundSpeed", "Maximum ground speed in m/s" },
                    { "TotalDistanceTravelled", "Total distance travelled" },
                    { "TotalShipRepairs", "Total number of ship repairs" },
                    { "TotalSynthRepairs", "Total number of synthesized repairs" },
                    { "SessionStartTime", "Session start time" },
                    { "SessionTime", "Session total time" }
                };
        }

        public Dictionary<string,string> Telemetry()
        {
            _telemetry["CurrentGroundSpeed"] = CurrentGroundSpeed.ToString();
            _telemetry["AverageGroundSpeed"] = AverageGroundSpeed.ToString();
            _telemetry["MaximumGroundSpeed"] = MaximumGroundSpeed.ToString();
            if (SessionStartTime>DateTime.MinValue)
                _telemetry["SessionTime"] = DateTime.Now.Subtract(SessionStartTime);
            _telemetry["TotalDistanceTravelled"] = TotalDistanceTravelled.ToString("F1");

            return _telemetry;
        }

        public void NewSession()
        {
            if (SessionStartTime > DateTime.MinValue)
                SaveSession();
            InitSession();
        }

        public void SaveSession()
        {

        }

        public void ProcessEvent(EDEvent edEvent)
        {
            if (SessionStartTime == DateTime.MinValue)
            {
                SessionStartTime = edEvent.TimeStamp;
                _telemetry["SessionStartTime"] = SessionStartTime.ToString();
            }

            bool statsUpdated = ProcessLocationUpdate(edEvent);
            if (ProcessFlags(edEvent))
                statsUpdated = true;

            switch (edEvent.EventName)
            {
                case "DockSRV":
                    TotalShipRepairs++;
                    _telemetry["TotalShipRepairs"] = TotalShipRepairs.ToString();
                    statsUpdated = true;
                    break;

                case "Synthesis":
                    TotalSynthRepairs++;
                    _telemetry["TotalSynthRepairs"] = TotalSynthRepairs.ToString();
                    statsUpdated = true;
                    break;
            }
            _lastEventTime = edEvent.TimeStamp;
            if (statsUpdated)
            {
                _sessionHistory.Add(edEvent);
                _srvTelemetryDisplay.UpdateTargetData(Telemetry());
            }
        }

        private bool ProcessFlags(EDEvent edEvent)
        {
            if (edEvent.Flags<1)
                return false;

            return false;
        }

        private bool ProcessLocationUpdate(EDEvent edEvent)
        {
            EDLocation currentLocation = edEvent.Location();
            if (currentLocation == null)
                return false;

            if (_lastLocation==null)
            {
                _lastLocation = currentLocation;
                SessionStartTime = edEvent.TimeStamp;
                return false;
            }
            if (_lastLocation.Latitude.Equals(currentLocation.Latitude) && _lastLocation.Longitude.Equals(currentLocation.Longitude))
                return false;

            // Update distance/speed statistics
            CalculateDistances(currentLocation);
            CalculateSpeed(currentLocation, edEvent.TimeStamp);
            return true;
        }

        private bool CalculateSpeed(EDLocation CurrentLocation, DateTime TimeStamp)
        {
            TimeSpan timeBetweenLocations = TimeStamp.Subtract(_speedCalculationTimeStamp);
            if (timeBetweenLocations.TotalMilliseconds < 750)
                return false;
            // We take a speed calculation once every 750 milliseconds

            double speedInMS = 0;
            if (_speedCalculationPreviousLocation != null)
            {
                double distanceBetweenLocations = EDLocation.DistanceBetween(_speedCalculationPreviousLocation, CurrentLocation);
                speedInMS = distanceBetweenLocations * 1000 / (double)timeBetweenLocations.TotalMilliseconds;
            }
            else
            {
                _speedCalculationPreviousLocation = CurrentLocation.Copy();
                _speedCalculationTimeStamp = TimeStamp;
                return false;
            }

            // Update the total average speed
            _totalOfSpeedReadings += speedInMS;
            _numberOfSpeedReadings++;
            AverageGroundSpeed = (int)(_totalOfSpeedReadings / _numberOfSpeedReadings);

            /*
            _lastThreeSpeedReadings[_oldestSpeedReading] = speedInMS;
            _oldestSpeedReading++;
            if (_oldestSpeedReading > 2)
                _oldestSpeedReading = 0;
            CurrentGroundSpeed = (int)((_lastThreeSpeedReadings[0] + _lastThreeSpeedReadings[1] + _lastThreeSpeedReadings[2]) / 3); // Returning an average of the last three readings should prevent blips
            */

            CurrentGroundSpeed = CurrentGroundSpeed;
            if (CurrentGroundSpeed > MaximumGroundSpeed)
                MaximumGroundSpeed = CurrentGroundSpeed;
            return true;
        }

        private void CalculateDistances(EDLocation CurrentLocation)
        {
            double distanceTravelled = EDLocation.DistanceBetween(_lastLocation, CurrentLocation);
            _lastLocation = CurrentLocation.Copy();
            TotalDistanceTravelled += distanceTravelled;
        }

        public void EditSettings(System.Windows.Forms.Control SettingsControl, System.Windows.Forms.IWin32Window owner = null)
        {
            if (_srvTelemetrySettings != null && _srvTelemetrySettings.IsDisposed)
                _srvTelemetrySettings = null;

            if (_srvTelemetrySettings == null)
            {
                _srvTelemetrySettings = new FormTelemetrySettings(_srvTelemetryWriter, SRVTelemetry.TelemetryDescriptions(), "SRV-", "SRV Telemetry Settings");
                _srvTelemetrySettings.SelectedReportsChanged += _srvTelemetrySettings_SelectedReportsChanged; ;
                _srvTelemetrySettings.ExportToControlTag(SettingsControl);
                if (_srvTelemetryDisplay != null && !_srvTelemetryDisplay.IsDisposed)
                {
                    _srvTelemetryDisplay.Close();
                    _showTelemetryDisplayOnSettingsClose = true;
                    _srvTelemetrySettings.FormClosed += _srvTelemetrySettings_FormClosed;
                }
            }
            if (!_srvTelemetrySettings.Visible)
                _srvTelemetrySettings.Show(owner);
        }

        private void _srvTelemetrySettings_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            if (_showTelemetryDisplayOnSettingsClose)
            {
                _showTelemetryDisplayOnSettingsClose = false;
                DisplayTelemetry();
            }
        }

        private void _srvTelemetrySettings_SelectedReportsChanged(object sender, EventArgs e)
        {
            // We cannot update columns once first initialised, for some reason
            // So as a hack we'll just close and reopen the form
            if (_srvTelemetryDisplay != null && !_srvTelemetryDisplay.IsDisposed)
            {
                _srvTelemetryDisplay.Close();
                _srvTelemetryDisplay = null;
                DisplayTelemetry();
            }
        }

        public void DisplayTelemetry(System.Windows.Forms.IWin32Window owner = null)
        {
            if (_srvTelemetryWriter == null)
                return;

            if (_srvTelemetryDisplay == null || _srvTelemetryDisplay.IsDisposed)
            {
                _srvTelemetryDisplay = new FormTelemetryDisplay(_srvTelemetryWriter, "Commander Telemetry");
                //_srvTelemetryDisplay.FormClosing += _targetTelemetryDisplay_FormClosing;
                _srvTelemetryDisplay.InitialiseRows(TelemetryDescriptions());
                _srvTelemetryDisplay.Show(owner);
            }
            else if (!_srvTelemetryDisplay.Visible)
                _srvTelemetryDisplay.Show(owner);
            else
                _srvTelemetryDisplay.Focus();
        }

        public void HideTelemetry()
        {
            if (_srvTelemetryDisplay == null)
                return;

            if (!_srvTelemetryDisplay.IsDisposed)
                _srvTelemetryDisplay.Close();
            _srvTelemetryDisplay = null;
        }
    }
}
