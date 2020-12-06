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
            textBoxCommanderName.Focus();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxCommanderName.Text))
            {
                textBoxCommanderName.Focus();
                return;
            }
            this.Hide();
        }

        private void textBoxCommanderName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = (e.KeyCode == Keys.Return) || (e.KeyCode == Keys.Enter);
        }

        private void textBoxCommanderName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Return || e.KeyCode==Keys.Enter)
            {
                if (!String.IsNullOrEmpty(textBoxCommanderName.Text))
                {
                    e.Handled = true;
                    this.Hide();
                }
            }
        }
    }
}
