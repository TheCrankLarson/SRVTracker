using EDTracking;
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
    public partial class FormStatusMessages : Form
    {
        public FormStatusMessages()
        {
            InitializeComponent();

            foreach (string eventName in EDRaceStatus.StatusMessages.Keys)
                dataGridViewStatusMessages.Rows.Add(new string[] { eventName, EDRaceStatus.StatusMessages[eventName] });
        }

        public Dictionary<string,string> StatusMessages()
        {
            Dictionary<string, string> statusMessages = new Dictionary<string, string>();
            if (dataGridViewStatusMessages.Rows.Count > 1)
                for (int i=0; i<dataGridViewStatusMessages.Rows.Count; i++)
                    statusMessages.Add((string)dataGridViewStatusMessages.Rows[i].Cells[0].Value, (string)dataGridViewStatusMessages.Rows[i].Cells[1].Value);
            return statusMessages;
        }
    }
}