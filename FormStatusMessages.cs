using EDTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SRVTracker
{
    public partial class FormStatusMessages : Form
    {
        private string _saveFile = "statuses.txt";

        public FormStatusMessages()
        {
            InitializeComponent();
            if (File.Exists(_saveFile))
                LoadFile(_saveFile);
            DisplayMessages();
        }

        public Dictionary<string,string> StatusMessages()
        {
            Dictionary<string, string> statusMessages = new Dictionary<string, string>();
            if (dataGridViewStatusMessages.Rows.Count > 1)
                for (int i=0; i<dataGridViewStatusMessages.Rows.Count; i++)
                    statusMessages.Add((string)dataGridViewStatusMessages.Rows[i].Cells[0].Value, (string)dataGridViewStatusMessages.Rows[i].Cells[1].Value);
            return statusMessages;
        }

        private void DisplayMessages()
        {
            dataGridViewStatusMessages.Rows.Clear();
            foreach (string eventName in EDRaceStatus.StatusMessages.Keys)
                dataGridViewStatusMessages.Rows.Add(new string[] { eventName, EDRaceStatus.StatusMessages[eventName] });
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = _saveFile;
                openFileDialog.Filter = "text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FileName = _saveFile;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadFile(openFileDialog.FileName);
                }
            }
        }

        private void LoadFile(string file)
        {
            try
            {
                EDRaceStatus.StatusMessages = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(file));
                _saveFile = file;
                DisplayMessages();
                buttonSave.Enabled = true;
            }
            catch { }
        }

        private void buttonSaveAs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = _saveFile;
                saveFileDialog.Filter = "text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = _saveFile;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, JsonSerializer.Serialize<Dictionary<string,string>>(StatusMessages()));
                        _saveFile = saveFileDialog.FileName;
                        buttonSave.Enabled = true;
                    }
                    catch { }
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(_saveFile, JsonSerializer.Serialize<Dictionary<string, string>>(StatusMessages()));
            }
            catch { }
        }
    }
}