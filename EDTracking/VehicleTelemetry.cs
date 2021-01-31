using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace EDTracking
{
    public class VehicleTelemetry
    {
        public double HullHealth { get; set; } = 1;
        public int CurrentGroundSpeed { get; set; } = 0;
        public int CurrentHeading { get; set; } = 0;
        public int SpeedAltitudeAdjusted { get; set; } = 0;
        public int MaximumSpeedAltitudeAdjusted { get; set; } = 0;
        public int AverageGroundSpeed { get; set; } = 0;
        public int MaximumGroundSpeed { get; set; } = 0;
        public int MaximumAltitude { get; set; } = 0;
        public int MinimumAltitude { get; set; } = int.MaxValue;
        public double TotalDistanceTravelled { get; set; } = 0;
        public bool TrackSRV { get; set; } = true;
        public int TotalShipRepairs { get; set; } = 0;
        public int TotalSynthRepairs { get; set; } = 0;
        public int TotalSRVsDestroyed { get; set; } = 0;
        public static string SessionSaveFolder { get; set; } = "Session Telemetry";
        public DateTime SessionStartTime { get; set; } = DateTime.MinValue;
        public EDLocation SessionStartLocation { get; set; } = null;
        private DateTime _lastEventTime = DateTime.MinValue;
        private EDLocation _lastLocation = null;
        public List<EDEvent> SessionHistory { get; set; } = new List<EDEvent>();
        private EDLocation _speedCalculationPreviousLocation = null;
        private DateTime _speedCalculationTimeStamp = DateTime.UtcNow;
        private double[] _lastThreeSpeedReadings = new double[] { 0, 0, 0 };
        private double _lastDistanceMeasurement = 0;
        private int _numberOfSpeedReadings = 0;
        private double _totalOfSpeedReadings = 0;
        private bool _playerIsInSRV = false;

        private TelemetryWriter _srvTelemetryWriter = null;
        private FormTelemetryDisplay _srvTelemetryDisplay = null;
        private FormTelemetrySettings _srvTelemetrySettings = null;
        private bool _showTelemetryDisplayOnSettingsClose = false;
        private Dictionary<string, string> _telemetry = new Dictionary<string, string>();


        public VehicleTelemetry()
        {
            _srvTelemetryWriter = new TelemetryWriter();
            DisplayAllReports();
            InitSession();
        }

        public VehicleTelemetry(string settingJson, string commanderName)
        {
            if (!String.IsNullOrEmpty(settingJson))
                _srvTelemetryWriter = new TelemetryWriter(settingJson);
            else
            {
                // We have no previous settings, so we want to enable all telemetry
                _srvTelemetryWriter = new TelemetryWriter();
                DisplayAllReports();
            }
            _telemetry.Add("CommanderName", commanderName);
            InitSession();
        }

        private void DisplayAllReports()
        {
            List<string> allReports = TelemetryDescriptions.Keys.ToList<string>();
            foreach (string report in allReports)
                _srvTelemetryWriter.EnableReportDisplay(report, report);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public static VehicleTelemetry FromString(string telemetryInfo)
        {
            try
            {
                return (VehicleTelemetry)JsonSerializer.Deserialize(telemetryInfo, typeof(VehicleTelemetry));
            }
            catch { }
            return null;
        }

        private void InitSession()
        {
            CurrentGroundSpeed = 0;
            _numberOfSpeedReadings = 0;
            _totalOfSpeedReadings = 0;
            AverageGroundSpeed = 0;
            MaximumGroundSpeed = 0;
            MaximumAltitude = 0;
            MinimumAltitude = int.MaxValue;
            SpeedAltitudeAdjusted = 0;
            TotalDistanceTravelled = 0;
            TotalShipRepairs = 0;
            TotalSynthRepairs = 0;
            SessionStartTime = DateTime.Now;
            SessionStartLocation = null;
            TotalSRVsDestroyed = 0;

            string commanderName = "";
            if (_telemetry.ContainsKey("CommanderName"))
                commanderName = _telemetry["CommanderName"];

            _telemetry.Clear();
            _telemetry.Add("CommanderName", commanderName);
            _telemetry.Add("CurrentGroundSpeed", CurrentGroundSpeed.ToString());
            _telemetry.Add("CurrentHeading", "Unknown");
            _telemetry.Add("HullStrength", $"{(HullHealth * 100).ToString("F1")}%");
            _telemetry.Add("AverageGroundSpeed", AverageGroundSpeed.ToString());
            _telemetry.Add("MaximumGroundSpeed", MaximumGroundSpeed.ToString());
            _telemetry.Add("DistanceFromStart", "0m");
            _telemetry.Add("TotalDistanceTravelled", TotalDistanceTravelled.ToString("F1"));
            _telemetry.Add("TotalShipRepairs", TotalShipRepairs.ToString());
            _telemetry.Add("TotalSynthRepairs", TotalSynthRepairs.ToString());
            _telemetry.Add("TotalSRVsDestroyed", "0");
            _telemetry.Add("SessionStartTime", "");
            _telemetry.Add("SessionDate", "");
            _telemetry.Add("SessionTime", "00:00:00");
            _telemetry.Add("CurrentAltitude", "0");
            _telemetry.Add("MaximumAltitude", "0");
            _telemetry.Add("MinimumAltitude", "NA");
            _telemetry.Add("SpeedAltitudeAdjusted", SpeedAltitudeAdjusted.ToString());
            _telemetry.Add("MaximumSpeedAltitudeAdjusted", MaximumSpeedAltitudeAdjusted.ToString());

            SessionHistory.Clear();
            _srvTelemetryDisplay?.UpdateTargetData(Telemetry());
        }

        public static readonly Dictionary<string, string> TelemetryDescriptions = new Dictionary<string, string>()
        {
            { "CommanderName", "Commander name" },
            { "HullStrength", "Last known hull value" },
            { "CurrentGroundSpeed", "Current ground speed in m/s" },
            { "CurrentHeading", "Current heading in degrees" },
            { "AverageGroundSpeed", "Average ground speed in m/s" },
            { "MaximumGroundSpeed", "Maximum ground speed in m/s" },
            { "CurrentAltitude", "Current altitude" },
            { "MaximumAltitude", "Maximum altitude" },
            { "MinimumAltitude", "Minimum altitude" },
            { "SpeedAltitudeAdjusted", "Current speed in m/s (includes altitude adjustment)" },
            { "MaximumSpeedAltitudeAdjusted", "Maximum speed in m/s (includes altitude adjustment)" },
            { "DistanceFromStart", "Distance from session start location" },
            { "TotalDistanceTravelled", "Total distance travelled" },
            { "TotalSRVShipRepairs", "Total number of SRV repairs via ship" },
            { "TotalSRVSynthRepairs", "Total number of synthesized repairs of the SRV" },
            { "TotalSRVsDestroyed", "Total number of SRVs lost" },
            { "SessionDate", "Session date" },
            { "SessionStartTime", "Session start time" },
            { "SessionTime", "Session total time" }
        };        

        public Dictionary<string,string> Telemetry()
        {            
            if (SessionStartTime>DateTime.MinValue)
                _telemetry["SessionTime"] = DateTime.Now.Subtract(SessionStartTime).ToString(@"hh\:mm\:ss");

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
            if (String.IsNullOrEmpty(SessionSaveFolder))
                return;

            string saveFile = SessionSaveFolder;
            if (!saveFile.EndsWith("\\"))
                saveFile = $"{saveFile}\\";
            saveFile = $"{saveFile}{SessionStartTime.ToString("yyyyMMddhhmmss")} Session.json";
            try
            {
                File.WriteAllText(saveFile, this.ToString());
            }
            catch { }
        }

        public void ProcessEvent(EDEvent edEvent, bool noHistory = false)
        {
            if (SessionStartTime == DateTime.MinValue)
            {
                SessionStartTime = new DateTime(edEvent.TimeStamp.Year,edEvent.TimeStamp.Month, edEvent.TimeStamp.Day, edEvent.TimeStamp.Hour, edEvent.TimeStamp.Minute, edEvent.TimeStamp.Second);
                _telemetry["SessionStartTime"] = SessionStartTime.ToString("HH:mm:ss");
                _telemetry["SessionDate"] = SessionStartTime.ToShortDateString();
            }

            bool statsUpdated = ProcessFlags(edEvent);
            switch (edEvent.EventName)
            {
                case "DockSRV":
                    TotalShipRepairs++;
                    HullHealth = 1;
                    _telemetry["HullStrength"] = "100%";
                    _telemetry["TotalSRVShipRepairs"] = TotalShipRepairs.ToString();
                    statsUpdated = true;
                    _playerIsInSRV = false;
                    break;

                case "LaunchSRV":
                    _playerIsInSRV = true;
                    break;

                case "Shutdown":
                    SaveSession();
                    break;

                case "SRVDestroyed":
                    _playerIsInSRV = false;
                    TotalSRVsDestroyed++;
                    _telemetry["TotalSRVsDestroyed"] = TotalSRVsDestroyed.ToString();
                    statsUpdated = true;
                    break;

                case "HullDamage":
                    HullHealth = edEvent.Health;
                    _telemetry["HullStrength"] = $"{(HullHealth * 100).ToString("F1")}%";
                    statsUpdated = true;
                    break;

                case "Synthesis":
                    TotalSynthRepairs++;
                    _telemetry["TotalSRVSynthRepairs"] = TotalSynthRepairs.ToString();
                    statsUpdated = true;
                    break;

                case "Status": // We only process location updates if the event is for the correct vehicle (ship or SRV)
                    if ((_playerIsInSRV == TrackSRV) && ProcessLocationUpdate(edEvent))
                        statsUpdated = true;
                    break;
            }

            _lastEventTime = edEvent.TimeStamp;
            if (statsUpdated)
            {
                if (!noHistory)
                    SessionHistory.Add(edEvent);
                _srvTelemetryDisplay?.UpdateTargetData(Telemetry());
            }
        }

        private bool ProcessFlags(EDEvent edEvent)
        {
            if (edEvent.Flags<1)
                return false;

            if (((edEvent.Flags & (long)StatusFlags.In_MainShip) == (long)StatusFlags.In_MainShip) ||
                ((edEvent.Flags & (long)StatusFlags.In_Fighter) == (long)StatusFlags.In_Fighter))
                _playerIsInSRV = false;

            if (((edEvent.Flags & (long)StatusFlags.In_SRV) == (long)StatusFlags.In_SRV))
                _playerIsInSRV = true;
            return false;
        }

        private bool ProcessLocationUpdate(EDEvent edEvent)
        {
            EDLocation currentLocation = edEvent.Location();
            if (currentLocation == null)
                return false;

            if (SessionStartLocation == null)
                SessionStartLocation = currentLocation;

            _telemetry["CurrentAltitude"] = EDLocation.DistanceToString(edEvent.Altitude);
            if ((int)edEvent.Altitude > MaximumAltitude)
            {
                MaximumAltitude = (int)edEvent.Altitude;
                _telemetry["MaximumAltitude"] = EDLocation.DistanceToString(MaximumAltitude);
            }
            else if ((int)edEvent.Altitude < MinimumAltitude)
            {
                MinimumAltitude = (int)edEvent.Altitude;
                _telemetry["MinimumAltitude"] = EDLocation.DistanceToString(MinimumAltitude);
            }

            if (_lastLocation==null)
            {
                _lastLocation = currentLocation;
                SessionStartTime = edEvent.TimeStamp;
                return false;
            }
            if (_lastLocation.Latitude.Equals(currentLocation.Latitude) && _lastLocation.Longitude.Equals(currentLocation.Longitude))
                return false;

            // Update distance/speed statistics
            if (CalculateDistances(currentLocation))
            {
                CalculateSpeed(currentLocation, edEvent.TimeStamp);
                return true;
            }
            return false;
        }

        private bool CalculateSpeed(EDLocation CurrentLocation, DateTime TimeStamp)
        {
            if (_speedCalculationPreviousLocation == null)
            {
                _speedCalculationPreviousLocation = CurrentLocation;
                _speedCalculationTimeStamp = TimeStamp;
                return false;
            }

            TimeSpan timeBetweenLocations = TimeStamp.Subtract(_speedCalculationTimeStamp);
            if (timeBetweenLocations.TotalMilliseconds < 750)
                return false;
            // We take a speed calculation once every 750 milliseconds

            double distanceBetweenLocations = EDLocation.DistanceBetween(_speedCalculationPreviousLocation, CurrentLocation);//EDLocation.DistanceBetweenIncludingAltitude(_speedCalculationPreviousLocation, CurrentLocation);
            if (_speedCalculationPreviousLocation.Altitude != CurrentLocation.Altitude)
            {
                double distanceWithAltitudeAdjustment = Math.Sqrt(Math.Pow(distanceBetweenLocations, 2) + Math.Pow(Math.Abs(_speedCalculationPreviousLocation.Altitude - CurrentLocation.Altitude), 2));
                SpeedAltitudeAdjusted = Convert.ToInt32((distanceWithAltitudeAdjustment * 1000) / (double)timeBetweenLocations.TotalMilliseconds);
                _telemetry["SpeedAltitudeAdjusted"] = $"{SpeedAltitudeAdjusted} m/s";
                if (SpeedAltitudeAdjusted > MaximumSpeedAltitudeAdjusted)
                {
                    MaximumSpeedAltitudeAdjusted = SpeedAltitudeAdjusted;
                    _telemetry["MaximumSpeedAltitudeAdjusted"] = $"{MaximumSpeedAltitudeAdjusted} m/s";
                }
            }
            double speedInMS = (distanceBetweenLocations * 1000) / (double)timeBetweenLocations.TotalMilliseconds;
            _speedCalculationPreviousLocation = CurrentLocation;
            _speedCalculationTimeStamp = TimeStamp;

            // Update the total average speed
            _totalOfSpeedReadings += speedInMS;
            _numberOfSpeedReadings++;
            AverageGroundSpeed = (int)(_totalOfSpeedReadings / _numberOfSpeedReadings);
            _telemetry["AverageGroundSpeed"] = $"{AverageGroundSpeed} m/s";

            CurrentGroundSpeed = (int)speedInMS;
            _telemetry["CurrentGroundSpeed"] = $"{CurrentGroundSpeed} m/s";
            if (CurrentGroundSpeed > MaximumGroundSpeed)
            {
                MaximumGroundSpeed = CurrentGroundSpeed;
                _telemetry["MaximumGroundSpeed"] = $"{MaximumGroundSpeed} m/s";
            }
            return true;
        }

        private bool CalculateDistances(EDLocation CurrentLocation)
        {
            double distanceTravelled = EDLocation.DistanceBetween(_lastLocation, CurrentLocation);
            CurrentHeading = (int)EDLocation.BearingToLocation(_lastLocation, CurrentLocation);
            _telemetry["CurrentHeading"] = $"{CurrentHeading}°";
            _lastLocation = CurrentLocation;

            // Sanity check to avoid silly readings
            if (_lastDistanceMeasurement < 40 && distanceTravelled > 100)
                return false;
            if (_lastDistanceMeasurement > 40) 
                if (distanceTravelled > (_lastDistanceMeasurement * 2))
                    return false;

            _lastDistanceMeasurement = distanceTravelled;
            TotalDistanceTravelled += distanceTravelled;
            _telemetry["TotalDistanceTravelled"] = EDLocation.DistanceToString(TotalDistanceTravelled);
            _telemetry["DistanceFromStart"] = EDLocation.DistanceToString(EDLocation.DistanceBetween(SessionStartLocation, CurrentLocation));
            return true;
        }

        public void EditSettings(System.Windows.Forms.Control SettingsControl, System.Windows.Forms.IWin32Window owner = null)
        {
            if (_srvTelemetrySettings != null && _srvTelemetrySettings.IsDisposed)
                _srvTelemetrySettings = null;

            if (_srvTelemetrySettings == null)
            {
                _srvTelemetrySettings = new FormTelemetrySettings(_srvTelemetryWriter, VehicleTelemetry.TelemetryDescriptions, "SRV-", "SRV Telemetry Settings");
                _srvTelemetrySettings.SelectedReportsChanged += _srvTelemetrySettings_SelectedReportsChanged; ;
                _srvTelemetrySettings.ExportToControlTag(SettingsControl);
                if (_srvTelemetryDisplay != null && !_srvTelemetryDisplay.IsDisposed)
                {
                    _srvTelemetryDisplay.Close();
                    _srvTelemetryDisplay = null;
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
                _srvTelemetryDisplay.InitialiseRows(TelemetryDescriptions);
                _srvTelemetryDisplay.UpdateTargetData(Telemetry());
                if (owner != null)
                    _srvTelemetryDisplay.Show(owner);
                else
                    _srvTelemetryDisplay.Show();
            }
            else if (!_srvTelemetryDisplay.Visible)
            {
                if (owner != null)
                    _srvTelemetryDisplay.Show(owner);
                else
                    _srvTelemetryDisplay.Show();
            }
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
