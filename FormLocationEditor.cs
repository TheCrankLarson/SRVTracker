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
    public partial class FormLocationEditor : Form
    {
        private static FormLocationEditor _formInstance = null;
        private static Point _formLocation;

        public FormLocationEditor()
        {
            InitializeComponent();
            locationManager1.SelectionChanged += LocationManager1_SelectionChanged;
        }

        public static FormLocationEditor GetFormLocationEditor()
        {
            if (_formInstance == null || _formInstance.IsDisposed)
            {
                _formInstance = new FormLocationEditor();
                if (_formLocation != null)
                    _formInstance.Location = _formLocation;
            }
            return _formInstance;
        }

        private void LocationManager1_SelectionChanged(object sender, EventArgs e)
        {
            if (locationManager1.AllowSelectionOnly)
            {
                DialogResult = DialogResult.OK;
                Hide();
            }
        }

        public EDLocation SelectLocation(IWin32Window parent, Point? windowBottomLeft = null)
        {
            Point previousWindowLocation = this.Location;
            bool wasVisible = false;
            if (this.Visible)
            {
                this.Hide();
                wasVisible = true;
            }
            this.FormBorderStyle = FormBorderStyle.None;
            locationManager1.AllowSelectionOnly = true;
            if (windowBottomLeft != null)
            {
                Point windowTopLeft = (Point)windowBottomLeft;
                windowTopLeft.Y -= this.Height;
                Location = (Point)windowTopLeft;
            }

            locationManager1.ClearSelection();
            DialogResult result = this.ShowDialog(parent);

            if (wasVisible)
            {
                this.Location = previousWindowLocation;
                ShowWithBorder();
            }
            if (result == DialogResult.Cancel)
                return null;
            return locationManager1.SelectedLocation;
        }

        public void ShowWithBorder(IWin32Window owner = null)
        {
            if (this.Visible)
                this.Hide();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            locationManager1.AllowSelectionOnly = false;
            this.Show(owner);
        }

        private void FormLocationEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formLocation = this.Location;
        }
    }
}
