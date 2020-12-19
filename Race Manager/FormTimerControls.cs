using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Race_Manager
{
    public partial class FormTimerControls : Form
    {
        private FormTimer _formTimer = null;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public FormTimerControls(FormTimer formTimer)
        {
            InitializeComponent();
            _formTimer = formTimer;
            AttachToTimer();
            _formTimer.Move += _formTimer_Move;
            _formTimer.Activated += _formTimer_Activated;
        }

        private void _formTimer_Activated(object sender, EventArgs e)
        {
            AttachToTimer();
        }

        private void _formTimer_Move(object sender, EventArgs e)
        {
            AttachToTimer();
        }

        public void AttachToTimer()
        {
            Point controlsLocation = new Point(_formTimer.Location.X, _formTimer.Location.Y - this.Height);
            if (!controlsLocation.Equals(this.Location))
                this.Location = controlsLocation;
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            _formTimer.Play();
            buttonPlay.Enabled = false;
            buttonPause.Enabled = true;
            buttonStop.Enabled = false;
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            _formTimer.Pause();
            buttonPlay.Enabled = true;
            buttonPause.Enabled = false;
            buttonStop.Enabled = true;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _formTimer.Stop();
            buttonStop.Enabled = false;
            buttonPause.Enabled = false;
            buttonPlay.Enabled = true;
        }

        private void buttonMove_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(_formTimer.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
