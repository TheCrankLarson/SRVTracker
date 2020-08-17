using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (labelLongitude.Text != location.Longitude.ToString())
                labelLongitude.Text = location.Longitude.ToString();

            if (labelLatitude.Text != location.Latitude.ToString())
                labelLatitude.Text = location.Latitude.ToString();

            if (labelAltitude.Text != location.Altitude.ToString())
                labelAltitude.Text = location.Altitude.ToString();

            if (heading > -1)
            {
                if (labelHeading.Text != heading.ToString())
                    labelHeading.Text = heading.ToString();
            }
            else
                if (labelHeading.Text != "NA")
                labelHeading.Text = "NA";
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
