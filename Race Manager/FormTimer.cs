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
    public partial class FormTimer : Form
    {
        private TimeSpan _pauseCorrection = new TimeSpan(0);
        private DateTime _startTime = DateTime.Now;
        private bool _paused = false;
        private DateTime _pauseStartTime = DateTime.MinValue;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public FormTimer()
        {
            InitializeComponent();
            raceTimer1.MouseIsOver += RaceTimer1_MouseIsOver;
            raceTimer1.MouseIsNotOver += RaceTimer1_MouseIsNotOver;
            buttonPause.Visible = false;
            buttonStop.Visible = false;
            buttonMove.Visible = false;
        }

        private void RaceTimer1_MouseIsNotOver(object sender, EventArgs e)
        {

        }

        private void RaceTimer1_MouseIsOver(object sender, EventArgs e)
        {
            if (!buttonMove.Visible)
            {
                buttonPlay.Visible = true;
                buttonPause.Visible = true;
                buttonStop.Visible = true;
                buttonMove.Visible = true;
            }
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            _startTime = DateTime.Now;
            _paused = false;
                
            timer1.Start();
            buttonPlay.Enabled = false;
            buttonPause.Enabled = true;
            buttonStop.Enabled = false;
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            _pauseStartTime = DateTime.Now;
            _pauseCorrection += _pauseStartTime.Subtract(_startTime);
            _paused = true;
            timer1.Stop();
            buttonPlay.Enabled = true;
            buttonPause.Enabled = false;
            buttonStop.Enabled = true;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            _paused = false;
            _pauseCorrection = new TimeSpan(0);
            _pauseStartTime = DateTime.MinValue;
            raceTimer1.SetTimer(_pauseCorrection);
            buttonStop.Enabled = false;
            buttonPause.Enabled = false;
            buttonPlay.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan timerTime = DateTime.Now.Subtract(_startTime).Add(_pauseCorrection);
            raceTimer1.SetTimer(timerTime);
        }

        private void FormTimer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void buttonMove_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void FormTimer_Leave(object sender, EventArgs e)
        {
            if (buttonMove.Visible)
            {
                buttonPlay.Visible = false;
                buttonPause.Visible = false;
                buttonStop.Visible = false;
                buttonMove.Visible = false;
            }
        }
    }
}
