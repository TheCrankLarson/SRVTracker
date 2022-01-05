using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace DataCollator
{
    public partial class Form1 : Form
    {
        NotificationServer _notificationServer = null;

        public Form1()
        {
            InitializeComponent();
            textBoxAPIUrl.Text = "http://*:11938/" + Application.ProductName.ToString() + "/";
            UDPListener.DataReceived += UDPListener_DataReceived;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StartNotificationServer()
        {
            if (_notificationServer == null)

                _notificationServer = new NotificationServer(textBoxAPIUrl.Text, textBoxWebURL.Text, checkBoxDebug.Checked, checkBoxVerboseDebug.Checked);
            else
                _notificationServer.Start();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (buttonStart.Text.Equals("Start"))
            {
                UDPListener.StartListening((int)numericUpDown1.Value);
                buttonStart.Text = "Stop";
                Thread thread = new Thread(() => { StartNotificationServer(); });
                thread.Start();
                return;
            }
           
            _notificationServer.Stop();
            UDPListener.StopListening();
            buttonStart.Text = "Start";
        }

        private void UDPListener_DataReceived(object sender, string data)
        {
            Task.Run(new Action(() => { _notificationServer?.ProcessNotification(data); }));
        }

        private void checkBoxVerboseDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (_notificationServer == null)
                return;
            _notificationServer.VerboseDebugEnabled = checkBoxVerboseDebug.Checked;
        }

        private void checkBoxDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (_notificationServer == null)
                return;

            if (checkBoxDebug.Checked)
                _notificationServer.EnableDebug();
            else
                _notificationServer.DisableDebug();
        }
    }
}
