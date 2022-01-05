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
    public partial class FormRacerProfile : Form
    {
        private EDRacerProfile _racerProfile = new EDRacerProfile();

        public FormRacerProfile()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadImage(PictureBox TargetPictureBox)
        {

        }

        private void SaveImage(PictureBox SourcePictureBox)
        {

        }

        private void buttonLoadSRVImage_Click(object sender, EventArgs e)
        {
            LoadImage(pictureBoxSRV);
        }

        private void buttonLoadSLFImage_Click(object sender, EventArgs e)
        {
            LoadImage(pictureBoxSLF);
        }

        private void buttonLoadShipImage_Click(object sender, EventArgs e)
        {
            LoadImage(pictureBoxShip);
        }

        private void buttonSaveSRVImage_Click(object sender, EventArgs e)
        {
            SaveImage(pictureBoxSRV);
        }

        private void buttonSaveSLFImage_Click(object sender, EventArgs e)
        {
            SaveImage(pictureBoxSLF);
        }

        private void buttonSaveShipImage_Click(object sender, EventArgs e)
        {
            SaveImage(pictureBoxShip);
        }
    }
}
