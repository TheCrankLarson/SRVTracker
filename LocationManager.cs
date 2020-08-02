﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SRVTracker
{
    public partial class LocationManager : UserControl
    {
        private string _saveFilename = "";

        public LocationManager()
        {
            InitializeComponent();
        }

        public EDLocation SelectedLocation
        {
            get
            {
                try
                {
                    return (EDLocation)listBoxLocations.SelectedItem;
                }
                catch { }
                return null;
            }
        }

        private void buttonLoadLocations_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = _saveFilename;
                openFileDialog.Filter = "edlocations files (*.edlocations)|*.edlocations|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FileName = _saveFilename;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Task.Run(new Action(() => { LoadLocationsFromFile(openFileDialog.FileName); }));
                    }
                    catch { }
                }
            }
        }

        private void buttonSaveLocations_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = _saveFilename;
                saveFileDialog.Filter = "edlocations files (*.edlocations)|*.edlocations|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = _saveFilename;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Task.Run(new Action(() => { SaveLocationsToFile(saveFileDialog.FileName); }));
                    }
                    catch { }
                }
            }
        }

        public void LoadLocationsFromFile(string filename)
        {
            Action action = new Action(() => { listBoxLocations.Items.Clear(); });
            if (listBoxLocations.InvokeRequired)
                listBoxLocations.Invoke(action);
            else
                action();

            try
            {
                Stream statusStream = File.OpenRead(filename);
                action = new Action(() => { listBoxLocations.BeginUpdate(); });
                if (listBoxLocations.InvokeRequired)
                    listBoxLocations.Invoke(action);
                else
                    action();
                using (StreamReader reader = new StreamReader(statusStream))
                {
                    while (!reader.EndOfStream)
                    {
                        try
                        {
                            EDLocation location = EDLocation.FromString(reader.ReadLine());
                            action = new Action(() => { listBoxLocations.Items.Add(location); });
                            if (listBoxLocations.InvokeRequired)
                                listBoxLocations.Invoke(action);
                            else
                                action();
                        }
                        catch { }
                    }
                    reader.Close();
                }
            }
            catch { }
            finally
            {
                action = new Action(() => { listBoxLocations.EndUpdate(); });
                if (listBoxLocations.InvokeRequired)
                    listBoxLocations.Invoke(action);
                else
                    action();
            }
        }

        public void SaveLocationsToFile(string filename)
        {
            try
            {
                StringBuilder locations = new StringBuilder();
                foreach (EDLocation location in listBoxLocations.Items)
                    locations.AppendLine(location.ToString());
                File.WriteAllText(filename, locations.ToString());
                _saveFilename = filename;
            }
            catch { }
        }

        private void buttonAddLocation_Click(object sender, EventArgs e)
        {
            FormAddLocation formAddLocation = new FormAddLocation();
            formAddLocation.AddLocation(listBoxLocations, this);
        }

        private void buttonDeleteLocation_Click(object sender, EventArgs e)
        {
            if (listBoxLocations.SelectedIndex < 0)
                return;
            try
            {
                listBoxLocations.Items.RemoveAt(listBoxLocations.SelectedIndex);
            }
            catch { }
        }

        private void buttonEditLocation_Click(object sender, EventArgs e)
        {
            if (listBoxLocations.SelectedIndex < 0)
                return;

            try
            {
                FormAddLocation formAddLocation = new FormAddLocation();
                formAddLocation.EditLocation((EDLocation)listBoxLocations.SelectedItem, this);
            }
            catch { }
        }
    }
}
