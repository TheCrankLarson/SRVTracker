using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using EDTracking;

namespace SRVTracker
{
    public partial class LocationManager : UserControl
    {
        private static string _saveFilename = "default.edlocations";
        public event EventHandler SelectionChanged;
        public FormLocator LocatorForm { get; set; } = null;
        private bool _allowSelectionOnly = false;
        private static List<EDLocation> _locations = null;

        public LocationManager()
        {
            InitializeComponent();
            if (_locations == null)
                LoadLocations();
            ShowLocations();
            ShowCancelButton(AllowSelectionOnly);
        }

        protected virtual void OnSelectionChanged(EventArgs e)
        {
            SelectionChanged?.Invoke(this, e);
        }

        public EDLocation SelectedLocation
        {
            get
            {
                try
                {
                    if (listBoxLocations.SelectedIndex < 0)
                        return null;
                    return (EDLocation)listBoxLocations.SelectedItem;
                }
                catch { }
                return null;
            }
        }

        public bool AllowSelectionOnly
        {
            get { return _allowSelectionOnly; }
            set
            {
                _allowSelectionOnly = value;
                ShowCancelButton(value);
            }
        }

        private void ShowCancelButton(bool show = true)
        {
            foreach (Control control in groupBox3.Controls)
                if (control is Button)
                    control.Visible = !show;
            buttonCancel.Visible = show;
        }

        public List<EDLocation> Locations
        {
            get
            {
                return _locations;
            }
        }

        public void ClearSelection()
        {
            listBoxLocations.SelectedIndex = -1;
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
                        Task.Run(new Action(() =>
                        {
                            _saveFilename = openFileDialog.FileName;
                            LoadLocations();
                            ShowLocations();
                        }));
                    }
                    catch { }
                }
            }
        }

        private void buttonSaveLocations_Click(object sender, EventArgs e)
        {
            SaveLocationsToFile(_saveFilename);
        }

        private void LoadLocations()
        {
            _locations = new List<EDLocation>();
            if (String.IsNullOrEmpty(_saveFilename) || !File.Exists(_saveFilename))
                return;

            try
            {
                Stream statusStream = File.OpenRead(_saveFilename);
                using (StreamReader reader = new StreamReader(statusStream))
                {
                    while (!reader.EndOfStream)
                    {
                        try
                        {
                            _locations.Add(EDLocation.FromString(reader.ReadLine()));
                        }
                        catch { }
                    }
                    reader.Close();
                }
            }
            catch { }
        }

        private void ShowLocations()
        {
            Action action = new Action(() =>
            {
                listBoxLocations.BeginUpdate();
                listBoxLocations.Items.Clear();
                if (_locations.Count>0)
                    for (int i = 0; i < _locations.Count; i++)
                        listBoxLocations.Items.Add(_locations[i].Name);
                listBoxLocations.EndUpdate();
            });

            if (listBoxLocations.InvokeRequired)
                listBoxLocations.Invoke(action);
            else
                action();
            UpdateButtons();
        }

        public void SaveLocationsToFile(string filename)
        {
            try
            {
                StringBuilder locations = new StringBuilder();
                if (_locations.Count > 0)
                    for (int i = 0; i < _locations.Count; i++)
                        locations.AppendLine(_locations[i].ToString());
                File.WriteAllText(filename, locations.ToString());
                _saveFilename = filename;
            }
            catch { }
        }

        private void buttonAddLocation_Click(object sender, EventArgs e)
        {
            FormAddLocation formAddLocation = new FormAddLocation();
            EDLocation newLocation = formAddLocation.AddLocation(listBoxLocations, this);
            if (newLocation == null)
                return;
            _locations.Add(newLocation);
            listBoxLocations.Items.Add(newLocation.Name);
        }

        private void buttonDeleteLocation_Click(object sender, EventArgs e)
        {
            if (listBoxLocations.SelectedIndex < 0)
                return;
            try
            {
                _locations.RemoveAt(listBoxLocations.SelectedIndex);
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
                EDLocation location = _locations[listBoxLocations.SelectedIndex];
                formAddLocation.EditLocation(location, this);
                if (!((string)listBoxLocations.SelectedItem).Equals(location.Name))
                    listBoxLocations.Items[listBoxLocations.SelectedIndex] = location.Name;
            }
            catch { }
        }

        private void buttonSaveAs_Click(object sender, EventArgs e)
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

        private void UpdateButtons()
        {
            buttonDeleteLocation.Enabled = listBoxLocations.SelectedIndex >= 0;
            buttonEditLocation.Enabled = listBoxLocations.SelectedIndex >= 0;
            buttonTrackLocation.Enabled = listBoxLocations.SelectedIndex >= 0;
        }

        private void listBoxLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
            OnSelectionChanged(e);
        }

        private void buttonTrackLocation_Click(object sender, EventArgs e)
        {
            if (listBoxLocations.SelectedIndex < 0)
                return;

            if (LocatorForm == null)
                return;
            LocatorForm.SetTarget((EDLocation)listBoxLocations.SelectedItem);
        }
    }
}
