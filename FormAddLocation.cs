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
        public FormAddLocation()
        {
            InitializeComponent();
        }

        private void buttonCurrentLocation_Click(object sender, EventArgs e)
        {
            if (FormLocator.CurrentLocation == null)
                return;
            textBoxLongitude.Text = FormLocator.CurrentLocation.Longitude.ToString();
            textBoxLatitude.Text = FormLocator.CurrentLocation.Latitude.ToString();
        }


    }
}
