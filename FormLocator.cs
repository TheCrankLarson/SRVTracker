using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Device.Location;

namespace SRVTracker
{
    public partial class FormLocator : Form
    {
        private static GeoCoordinate _currentPosition = null;
        private GeoCoordinate _targetPosition = null;

        public FormLocator()
        {
            InitializeComponent();
            buttonUseCurrentLocation.Enabled = false;  // We'll enable it when we have a location
        }

        public static double PlanetaryRadius { get; set; } = 0;
        public static GeoCoordinate CurrentLocation
        {
            get { return _currentPosition; }
        }

        public void UpdateLocation(GeoCoordinate CurrentLocation)
        {
            // Update our position
            if (_targetPosition == null)
                return;

            _currentPosition = CurrentLocation;
            try
            {
                string distanceUnit = "m";
                double distance = HaversineDistance(_currentPosition, _targetPosition);
                if (distance>1000000)
                {
                    distance = distance / 1000000;
                    distanceUnit = "MThanks m";
                }
                else if (distance>1000)
                {
                    distance = distance / 1000;
                    distanceUnit = "km";
                }
                double bearing = DegreeBearing(_currentPosition, _targetPosition);
                string d = $"{distance.ToString("#.0")}{distanceUnit}";
                string b = $"{Convert.ToInt32(bearing).ToString()}°";

                Action action;
                if (!labelDistance.Text.Equals(d))
                {
                    action = new Action(() => { labelDistance.Text = d; });
                    if (labelDistance.InvokeRequired)
                        labelDistance.Invoke(action);
                    else
                        action();
                }
                if (!labelHeading.Text.Equals(b))
                {
                    action = new Action(() => { labelHeading.Text = b; });
                    if (labelHeading.InvokeRequired)
                        labelHeading.Invoke(action);
                    else
                        action();
                }
                action = new Action(() => { buttonUseCurrentLocation.Enabled = false; });
                if (buttonUseCurrentLocation.InvokeRequired)
                    buttonUseCurrentLocation.Invoke(action);
                else
                    action();
            }
            catch { }
        }
        private static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static double HaversineDistance(GeoCoordinate pos1, GeoCoordinate pos2, DistanceUnit unit = DistanceUnit.Kilometers)
        {
            // For R, we can take the average of the two altitudes (if we have them)
            if (PlanetaryRadius == 0)
                return 0;
            double R = (unit == DistanceUnit.Miles) ? (PlanetaryRadius / 1.609344) : PlanetaryRadius;
            var lat = ConvertToRadians(pos2.Latitude - pos1.Latitude);
            var lng = ConvertToRadians(pos2.Longitude - pos1.Longitude);
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(ConvertToRadians(pos1.Latitude)) * Math.Cos(ConvertToRadians(pos2.Latitude)) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return R * h2;
        }

        public static double DegreeBearing(GeoCoordinate pos1, GeoCoordinate pos2)
        {
            var dLon = ConvertToRadians(pos2.Longitude - pos1.Longitude);
            var dPhi = Math.Log(
                Math.Tan(ConvertToRadians(pos2.Latitude) / 2 + Math.PI / 4) / Math.Tan(ConvertToRadians(pos1.Latitude) / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI)
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            return ConvertToBearing(Math.Atan2(dLon, dPhi));
        }

        public static double ConvertToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }

        public static double ConvertToBearing(double radians)
        {
            // convert radians to degrees (as bearing: 0...360)
            return (ConvertToDegrees(radians) + 360) % 360;
        }

        public enum DistanceUnit { Miles, Kilometers };

        private void textBoxLongitude_TextChanged(object sender, EventArgs e)
        {
            TrySetDestination();
        }

        private void textBoxLatitude_TextChanged(object sender, EventArgs e)
        {
            TrySetDestination();
        }

        private void textBoxAltitude_TextChanged(object sender, EventArgs e)
        {
            TrySetDestination();
        }

        private void TrySetDestination()
        {
            // This shouldn't ever be called cross-thread, so no need to check for invoke
            // If an error occurs while trying to create the coordinate, then we invalidate it
            // Once we have valid location, we change colour to signify that
            try
            {
                double latitude = Convert.ToDouble(textBoxLatitude.Text);
                double longitude = Convert.ToDouble(textBoxLongitude.Text);
                if (!String.IsNullOrEmpty(textBoxAltitude.Text))
                    _targetPosition = new GeoCoordinate(latitude, longitude, Convert.ToDouble(textBoxAltitude.Text));
                else
                    _targetPosition = new GeoCoordinate(latitude, longitude);
                groupBoxDestination.BackColor = Color.Lime;
                return;
            }
            catch { }
            _targetPosition = null;
            groupBoxDestination.BackColor = System.Drawing.SystemColors.Control;
        }

        private void buttonShowHideTarget_Click(object sender, EventArgs e)
        {
            if (this.Height == 228)
            {
                // We are expanded, so shrink
                this.Height = 126;
            }
            else
                this.Height = 228;
        }

        private void buttonUseCurrentLocation_Click(object sender, EventArgs e)
        {
            if (_currentPosition == null)
                return;
            _targetPosition = _currentPosition;
            textBoxLongitude.Text = _targetPosition.Longitude.ToString() ;
            textBoxLatitude.Text = _targetPosition.Latitude.ToString();

        }
    }
}
