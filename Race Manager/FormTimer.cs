using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Race_Manager
{
    public partial class FormTimer : Form
    {
        private TimeSpan _pauseCorrection = new TimeSpan(0);
        private DateTime _startTime = DateTime.Now;
        private bool _paused = false;
        private DateTime _pauseStartTime = DateTime.MinValue;
        private FormTimerControls _timerControls = null;
        private EDTracking.ConfigSaverClass _formConfig = null;

        public FormTimer()
        {
            InitializeComponent();

            // Attach our form configuration saver
            _formConfig = new EDTracking.ConfigSaverClass(this, true);
            _formConfig.StoreControlInfo = false;
            _formConfig.SaveEnabled = true;
            _formConfig.RestorePreviousSize = false;
            _formConfig.RestoreFormValues();

            raceTimer1.MouseClicked += RaceTimer1_MouseClicked;
            _timerControls = new FormTimerControls(this);
            _timerControls.Show(this);
        }

        private void RaceTimer1_MouseClicked(object sender, EventArgs e)
        {
        }

        public void Play()
        {
            _startTime = DateTime.Now;
            _paused = false;              
            timer1.Start();
        }

        public void Pause()
        {
            _pauseStartTime = DateTime.Now;
            _pauseCorrection += _pauseStartTime.Subtract(_startTime);
            _paused = true;
            timer1.Stop();
        }

        public void Stop()
        {
            timer1.Stop();
            _paused = false;
            _pauseCorrection = new TimeSpan(0);
            _pauseStartTime = DateTime.MinValue;
            raceTimer1.SetTimer(_pauseCorrection);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan timerTime = DateTime.Now.Subtract(_startTime).Add(_pauseCorrection);
            raceTimer1.SetTimer(timerTime);
        }

    }
}
