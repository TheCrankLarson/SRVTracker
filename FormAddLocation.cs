using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRVTracker
{
    public partial class FormAddLocation : Form
    {
        private EDLocation _location = null;
        private ListBox _locationListBox = null;

        public FormAddLocation(EDLocation location = null)
        {
            InitializeComponent();
            _location =location;
            if (_location == null)
                _location = new EDLocation();
        }

        private void buttonCurrentLocation_Click(object sender, EventArgs e)
        {
            if (FormLocator.CurrentLocation == null)
                return;
            _location = FormLocator.CurrentLocation;
            DisplayLocation();
        }

        private void DisplayLocation()
        {
            textBoxLongitude.Text = _location.Longitude.ToString();
            textBoxLatitude.Text = _location.Latitude.ToString();
            textBoxLocationName.Text = _location.Name;
            textBoxAltitude.Text = _location.Altitude.ToString();
            textBoxPlanet.Text = _location.PlanetName;
            textBoxPlanetaryRadius.Text = _location.PlanetaryRadius.ToString();
            textBoxSystem.Text = _location.SystemName;
        }

        public EDLocation GetDisplayedLocation(EDLocation updateLocation = null)
        {
            // Return EDLocation with given data

            try
            {
                if (updateLocation==null)
                    return new EDLocation(textBoxLocationName.Text, textBoxSystem.Text, textBoxPlanet.Text,
                        Convert.ToDouble(textBoxLatitude.Text), Convert.ToDouble(textBoxLongitude.Text), Convert.ToDouble(textBoxAltitude.Text),
                        Convert.ToDouble(textBoxPlanetaryRadius.Text));

                updateLocation.Name = textBoxLocationName.Text;
                updateLocation.SystemName = textBoxSystem.Text;
                updateLocation.PlanetName = textBoxPlanet.Text;
                updateLocation.Latitude = Convert.ToDouble(textBoxLatitude.Text);
                updateLocation.Longitude = Convert.ToDouble(textBoxLongitude.Text);
                updateLocation.Altitude = Convert.ToDouble(textBoxAltitude.Text);
                updateLocation.PlanetaryRadius = Convert.ToDouble(textBoxPlanetaryRadius.Text);
            }
            catch { }
            return null;
        }

        public void AddLocation(ListBox locationListBox, IWin32Window owner = null)
        {
            _locationListBox = locationListBox;
            DisplayLocation();
            this.Show(owner);
        }

        public void EditLocation(EDLocation location, IWin32Window owner = null)
        {
            _location = location;
            DisplayLocation();
            buttonAdd.Text = "Update";
            this.Show(owner);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxLocationName.Text))
            {
                textBoxLocationName.Focus();
                return;
            }
            EDLocation location = GetDisplayedLocation();
            if (location == null)
                return;

            if (_locationListBox != null)
            {
                // We're adding this location
                _locationListBox.Items.Add(location);
                this.Close();
            }

            // This was an edit
            if (_location == null)
                return;
            GetDisplayedLocation(_location);
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
