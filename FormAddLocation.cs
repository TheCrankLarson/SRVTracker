using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EDTracking;

namespace SRVTracker
{
    public partial class FormAddLocation : Form
    {
        private EDLocation _location = null;
        private ListBox _locationListBox = null;

        public FormAddLocation(EDLocation location = null)
        {
            InitializeComponent();
            _location = location;
            if (_location == null)
            {
                _location = new EDLocation();
                if (FormTracker.CurrentLocation.PlanetaryRadius > 0)
                    _location.PlanetaryRadius = FormTracker.CurrentLocation.PlanetaryRadius;
                _location.PlanetName = FormTracker.CurrentLocation.PlanetName;
                _location.SystemName = FormTracker.CurrentLocation.SystemName;
            }

            DisplayLocation();
        }

        public EDLocation GetLocation(IWin32Window owner)
        {
            if (this.ShowDialog(owner) != DialogResult.OK)
                return null;

            return GetDisplayedLocation();
        }

        private void buttonCurrentLocation_Click(object sender, EventArgs e)
        {
            if (FormTracker.CurrentLocation == null)
                return;

            _location = FormTracker.CurrentLocation.Copy();
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
                if (updateLocation == null)
                {
                    EDLocation newLocation = new EDLocation(textBoxLocationName.Text, textBoxSystem.Text, textBoxPlanet.Text,
                        Convert.ToDecimal(textBoxLatitude.Text), Convert.ToDecimal(textBoxLongitude.Text), Convert.ToDecimal(textBoxAltitude.Text),
                        Convert.ToDecimal(textBoxPlanetaryRadius.Text));
                    return newLocation;
                }

                updateLocation.Name = textBoxLocationName.Text;
                updateLocation.SystemName = textBoxSystem.Text;
                updateLocation.PlanetName = textBoxPlanet.Text;
                updateLocation.Latitude = Convert.ToDecimal(textBoxLatitude.Text);
                updateLocation.Longitude = Convert.ToDecimal(textBoxLongitude.Text);
                updateLocation.Altitude = Convert.ToDecimal(textBoxAltitude.Text);
                updateLocation.PlanetaryRadius = Convert.ToDecimal(textBoxPlanetaryRadius.Text);
                return updateLocation;
            }
            catch { }
            return null;
        }

        public EDLocation AddLocation(ListBox locationListBox, IWin32Window owner = null)
        {
            _locationListBox = locationListBox;
            DisplayLocation();
            if (this.ShowDialog(owner)==DialogResult.Cancel)
                return null;
            return GetDisplayedLocation();
        }

        public void EditLocation(EDLocation location, IWin32Window owner = null, bool asDialog = false)
        {
            _location = location;
            DisplayLocation();
            buttonAdd.Text = "Update";
            if (asDialog)
                this.ShowDialog();
            else
                this.Show(owner);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxLocationName.Text))
            {
                textBoxLocationName.Focus();
                return;
            }
            EDLocation location = GetDisplayedLocation(_location);
            if (location == null)
                return;

            if (_locationListBox != null)
            {
                // We're adding this location
                _locationListBox.Items.Add(location);
            }

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
