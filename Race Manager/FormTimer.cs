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
        private DateTime _startTime = DateTime.UtcNow;
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

            _timerControls = new FormTimerControls(this);
            _timerControls.Show(this);
        }


        public void Play()
        {
            _startTime = DateTime.UtcNow;
            _paused = false;              
            timer1.Start();
        }

        public void Pause()
        {
            _pauseStartTime = DateTime.UtcNow;
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
            raceTimer1.SetTimer(TimerValue());
        }

        public TimeSpan TimerValue()
        {
            return DateTime.UtcNow.Subtract(_startTime).Add(_pauseCorrection);
        }
    }
}
