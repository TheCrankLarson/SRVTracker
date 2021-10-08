using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Valve.VR;
using System.Text.Json;
using EDTracking;

namespace SRVTracker
{
    public partial class FormVRMatrixEditor : Form
    {
        private VRLocatorOverlay _vrLocatorOverlay = null;
        //private HmdMatrix34_t _hmdMatrix;
        private Dictionary<string, TransformDefinition> _savedMatrices = null;
        private string _matricesSaveFile = "hmd_matrices.json";
        private ConfigSaverClass _formConfig = null;
        private Control _sliderTargetControl = null;
        private bool _displayingMatrixData = false;

        public FormVRMatrixEditor(VRLocatorOverlay vrLocatorOverlay)
        {
            InitializeComponent();
            // Attach our form configuration saver
            _formConfig = new ConfigSaverClass(this, true);
            _formConfig.RestorePreviousSize = false;
            _formConfig.ExcludedControls.Add(textBoxMatrixName);
            _formConfig.SaveEnabled = true;
            _formConfig.RestoreFormValues();

            _vrLocatorOverlay = vrLocatorOverlay;
            InitMatrices();
            //_hmdMatrix = new HmdMatrix34_t();
            buttonApply.Enabled = !checkBoxAutoApply.Checked;
            ApplyOverlayWidth();
            if (listBoxMatrices.SelectedIndex>-1)
                _vrLocatorOverlay.Transform = _savedMatrices[(string)listBoxMatrices.SelectedItem].ToHmdMatrix34_t();
        }

        private TransformDefinition DefaultVRMatrix()
        {
            return new TransformDefinition(-1.5f,2,-0.8f,0,0,0,0.8f);
        }

        #region Load and save matrices functions

        private void InitMatrices()
        {          
            try
            {
                if (File.Exists(_matricesSaveFile))
                {
                    string json = File.ReadAllText(_matricesSaveFile);
                    _savedMatrices = (Dictionary<string, TransformDefinition>)JsonSerializer.Deserialize(json, typeof(Dictionary<string, TransformDefinition>));
                }
                else
                {
                    _savedMatrices = new Dictionary<string, TransformDefinition>();
                    _savedMatrices.Add("Default", DefaultVRMatrix());
                    SaveMatrices();
                }
            }
            catch
            {
                _savedMatrices = new Dictionary<string, TransformDefinition>();
            }

            listBoxMatrices.Items.Clear();
            foreach (string matrixName in _savedMatrices.Keys)
                listBoxMatrices.Items.Add(matrixName);
            if (listBoxMatrices.Items.Count>0)
                listBoxMatrices.SelectedIndex = 0;
        }

        private void SaveMatrices()
        {
            try
            {
                File.WriteAllText(_matricesSaveFile, JsonSerializer.Serialize(_savedMatrices));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save matrices:{Environment.NewLine}{Environment.NewLine}{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        private void DisplayMatrix()
        {
            if (listBoxMatrices.SelectedIndex < 0)
                return;

            _displayingMatrixData = true;
            TransformDefinition matrix = _savedMatrices[(string)listBoxMatrices.SelectedItem];
            numericUpDownX.Value = (decimal)matrix.PositionX;
            numericUpDownY.Value = (decimal)matrix.PositionY;
            numericUpDownZ.Value = (decimal)matrix.PositionZ;
            trackBarRotationX.Value = (int)(matrix.RotationX);
            trackBarRotationY.Value = (int)(matrix.RotationY);
            trackBarRotationZ.Value = (int)(matrix.RotationZ);
            numericUpDownOverlayWidth.Value = (decimal)matrix.Width;
            _displayingMatrixData = false;
            _vrLocatorOverlay.Transform = _savedMatrices[(string)listBoxMatrices.SelectedItem].ToHmdMatrix34_t();
        }

        public void SetOverlayWidth(Single WidthInMetres)
        {
            numericUpDownOverlayWidth.Value = (decimal)WidthInMetres;
            ApplyOverlayWidth();
        }

        public void ApplyOverlayWidth()
        {
            _vrLocatorOverlay.WidthInMeters = (float)numericUpDownOverlayWidth.Value;
        }

        public void ApplyMatrixToOverlay(bool force = false)
        {
            if (_displayingMatrixData || _vrLocatorOverlay == null || (!force && !checkBoxAutoApply.Checked) )
                return;

            if (listBoxMatrices.SelectedIndex < 0)
                return;

            if (checkBoxAutoApply.Checked || force)
                UpdateSelectedMatrixDefinition();

            _vrLocatorOverlay.Transform = _savedMatrices[(string)listBoxMatrices.SelectedItem].ToHmdMatrix34_t();
        }

        private void UpdateSelectedMatrixDefinition()
        {
            // Called when a value in the UI is updated so that we keep our stored matrices up-to-date

            if (listBoxMatrices.SelectedIndex < 0 )
                return;

            _savedMatrices[(string)listBoxMatrices.SelectedItem].PositionX = (float)trackBarPositionX.Value / 100;
            _savedMatrices[(string)listBoxMatrices.SelectedItem].PositionY = (float)trackBarPositionY.Value / 100;
            _savedMatrices[(string)listBoxMatrices.SelectedItem].PositionZ = (float)trackBarPositionZ.Value / 100;

            _savedMatrices[(string)listBoxMatrices.SelectedItem].RotationX = (float)trackBarRotationX.Value;
            _savedMatrices[(string)listBoxMatrices.SelectedItem].RotationY = (float)trackBarRotationY.Value;
            _savedMatrices[(string)listBoxMatrices.SelectedItem].RotationZ = (float)trackBarRotationZ.Value;

            _savedMatrices[(string)listBoxMatrices.SelectedItem].Width = (float)numericUpDownOverlayWidth.Value;
        }

        #region Control Events

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay(true);
            ApplyOverlayWidth();
            if (checkBoxAutoApply.Checked)
                buttonApply.Enabled = false;
        }

        private void checkBoxAutoApply_CheckedChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = !checkBoxAutoApply.Checked;
        }

        private void numericUpDownOverlayWidth_ValueChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveMatrices();
        }

        private void listBoxMatrices_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool itemSelected = listBoxMatrices.SelectedIndex >= 0;
            textBoxMatrixName.Enabled = itemSelected;
            buttonDelete.Enabled = itemSelected;
            if (itemSelected)
            {
                textBoxMatrixName.Text = (string)listBoxMatrices.SelectedItem;
                if (_savedMatrices.ContainsKey(textBoxMatrixName.Text))
                {
                    DisplayMatrix();
                    ApplyMatrixToOverlay();
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string matrixName = $"Matrix {_savedMatrices.Count + 1}";
            _savedMatrices.Add(matrixName, new TransformDefinition());
            listBoxMatrices.Items.Add(matrixName);
            listBoxMatrices.SelectedIndex = listBoxMatrices.Items.Count - 1;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxMatrices.SelectedIndex < 0)
                return;

            string matrixToDelete = (string)listBoxMatrices.SelectedItem;
            textBoxMatrixName.Text = "";
            listBoxMatrices.Items.RemoveAt(listBoxMatrices.SelectedIndex);
            if (_savedMatrices.ContainsKey(matrixToDelete))
                _savedMatrices.Remove(matrixToDelete);
        }

        private void textBoxMatrixName_Validating(object sender, CancelEventArgs e)
        {
            if (listBoxMatrices.SelectedIndex < 0)
                return;

            TransformDefinition matrixDefinition = _savedMatrices[(string)listBoxMatrices.SelectedItem];
            _savedMatrices.Remove((string)listBoxMatrices.SelectedItem);
            _savedMatrices.Add(textBoxMatrixName.Text, matrixDefinition);
            listBoxMatrices.Items[listBoxMatrices.SelectedIndex] = textBoxMatrixName.Text;
        }

        private void checkBoxMatrixIsRelative_CheckedChanged(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay(true);
        }
        #endregion



        private void trackBarPositionX_Scroll(object sender, EventArgs e)
        {
            numericUpDownX.Value = (decimal)trackBarPositionX.Value/100;
            ApplyMatrixToOverlay();
        }

        private void trackBarPositionY_Scroll(object sender, EventArgs e)
        {
            numericUpDownY.Value = (decimal)trackBarPositionY.Value / 100;
            ApplyMatrixToOverlay();
        }

        private void trackBarPositionZ_Scroll(object sender, EventArgs e)
        {
            numericUpDownZ.Value = (decimal)trackBarPositionZ.Value / 100;
            ApplyMatrixToOverlay();
        }

        private void trackBarRotationX_Scroll(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay();
        }

        private void trackBarRotationY_Scroll(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay();
        }

        private void trackBarRotationZ_Scroll(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay();
        }

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            if (trackBarPositionX.Value != (int)(numericUpDownX.Value * 100))
            {
                trackBarPositionX.Value = (int)(numericUpDownX.Value * 100);
                ApplyMatrixToOverlay();
            }            
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            if (trackBarPositionY.Value != (int)(numericUpDownY.Value * 100))
            {
                trackBarPositionY.Value = (int)(numericUpDownY.Value * 100);
                ApplyMatrixToOverlay();
            }
        }

        private void numericUpDownZ_ValueChanged(object sender, EventArgs e)
        {
            if (trackBarPositionZ.Value != (int)(numericUpDownZ.Value * 100))
            {
                trackBarPositionZ.Value = (int)(numericUpDownZ.Value * 100);
                ApplyMatrixToOverlay();
            }
        }

        private void buttonResetRotation_Click(object sender, EventArgs e)
        {
            trackBarRotationX.Value = 0;
            trackBarRotationY.Value = 0;
            trackBarRotationZ.Value = 0;
            ApplyMatrixToOverlay();
        }
    }
}
