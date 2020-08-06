using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRVTracker
{
    public partial class FormFirstRun : Form
    {
        public FormFirstRun()
        {
            InitializeComponent();
            try
            {
                textBoxReleaseNotes.Text = File.ReadAllText("ReleaseNotes.txt");
            }
            catch { }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {

        }
    }
}
