using System;
using System.Windows.Forms;
using EDTracking;

namespace SRVTracker
{
    public partial class TrackerHUD : UserControl
    {
        private bool _autoTracking = false;

        public TrackerHUD()
        {
            InitializeComponent();
            labelLatitude.Text = "Unknown";
            labelLongitude.Text = "Unknown";
            labelHeading.Text = "NA";
            labelAltitude.Text = "Unknown";
        }

        public void UpdateLocation(EDLocation location, int heading = -1)
        {
            Action action;

            if (labelLongitude.Text != location.Longitude.ToString())
            {
                action = new Action(() => { labelLongitude.Text = location.Longitude.ToString(); });
                if (labelLongitude.InvokeRequired)
                    labelLongitude.Invoke(action);
                else
                    action();                
            }

            if (labelLatitude.Text != location.Latitude.ToString())
            {
                action = new Action(() => { labelLatitude.Text = location.Latitude.ToString(); });
                if (labelLatitude.InvokeRequired)
                    labelLatitude.Invoke(action);
                else
                    action();
            }
            

            if (labelAltitude.Text != location.Altitude.ToString("F1"))
            {
                action = new Action(() => { labelAltitude.Text = location.Altitude.ToString("F1"); });
                if (labelAltitude.InvokeRequired)
                    labelAltitude.Invoke(action);
                else
                    action();
            }

            string sHeading = heading.ToString();
            if (heading < 0)
                sHeading = "NA";
            if (!labelHeading.Text.Equals(sHeading))
            {
                action = new Action(() => { labelHeading.Text = sHeading; });
                if (labelHeading.InvokeRequired)
                    labelHeading.Invoke(action);
                else
                    action();
            }
        }

        public void AutoTrack()
        {
            if (_autoTracking)
                return;
            FormTracker.CommanderLocationChanged += FormTracker_CommanderLocationChanged;
            _autoTracking = true;
        }

        private void FormTracker_CommanderLocationChanged(object sender, EventArgs e)
        {
            UpdateLocation(FormTracker.CurrentLocation, FormTracker.CurrentHeading);
        }
    }
}
