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

namespace Race_Manager
{
    public partial class FormStatusMessages : Form
    {
        private static string _defaultSaveFile = "statuses.txt";
        private string _saveFile = "";

        public FormStatusMessages()
        {
            InitializeComponent();
            _saveFile = _defaultSaveFile;
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
            foreach (string eventName in EDRace.StatusMessages.Keys)
                dataGridViewStatusMessages.Rows.Add(new string[] { eventName, EDRace.StatusMessages[eventName] });
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
                    if (LoadFile(openFileDialog.FileName))
                    {
                        _saveFile = openFileDialog.FileName;
                        DisplayMessages();
                        buttonSave.Enabled = true;
                    }
                }
            }
        }

        public static bool LoadFile(string file="")
        {
            if (String.IsNullOrEmpty(file))
                file = _defaultSaveFile;
            if (File.Exists(file))
            {
                try
                {
                    EDRace.StatusMessages = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(file));
                    return true;
                }
                catch { }
            }
            return false;
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