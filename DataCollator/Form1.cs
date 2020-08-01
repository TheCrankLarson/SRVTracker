using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace DataCollator
{
    public partial class Form1 : Form
    {
        NotificationServer notificationServer = null;

        public Form1()
        {
            InitializeComponent();
            textBoxWebhookUrl.Text = "http://*:11938/" + Application.ProductName.ToString() + "/";
            UDPListener.DataReceived += UDPListener_DataReceived;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (buttonStart.Text.Equals("Start"))
            {
                UDPListener.StartListening((int)numericUpDown1.Value);
                buttonStart.Text = "Stop";
                notificationServer = new NotificationServer(textBoxWebhookUrl.Text, checkBoxDebug.Checked);
                return;
            }
            
        }

        private void UDPListener_DataReceived(object sender, string data)
        {
            Task.Run(new Action(() => { notificationServer?.SendNotification($"{data}\n"); }));
        }
    }
}
