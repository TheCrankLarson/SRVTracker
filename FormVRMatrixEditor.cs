using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Valve.VR;
using System.Text.Json;
using EDTracking;

namespace SRVTracker
{
    public partial class FormVRMatrixEditor : Form
    {
        private ulong _overlayHandle = 0;
        private HmdMatrix34_t _hmdMatrix;
        private Dictionary<string, MatrixDefinition> _savedMatrices = null;
        private string _matricesSaveFile = "hmd_matrices.json";
        private ConfigSaverClass _formConfig = null;
        private Control _sliderTargetControl = null;

        public FormVRMatrixEditor(ulong overlayHandle)
        {
            InitializeComponent();
            // Attach our form configuration saver
            _formConfig = new ConfigSaverClass(this, true);
            _formConfig.StoreControlInfo = false;
            _formConfig.SaveEnabled = true;
            _formConfig.RestoreFormValues();

            InitMatrices();
            _overlayHandle = overlayHandle;
            _hmdMatrix = new HmdMatrix34_t();
            buttonApply.Enabled = !checkBoxAutoApply.Checked;
            ApplyOverlayWidth();
        }

        private static MatrixDefinition DefaultVRMatrix()
        {
            MatrixDefinition vrMatrix = new MatrixDefinition();
            vrMatrix.m0 = 0.7F;
            vrMatrix.m1 = 0.0F;
            vrMatrix.m2 = 0.0F;
            vrMatrix.m3 = 1.0F; // x
            vrMatrix.m4 = 0.0F;
            vrMatrix.m5 = -1.0F;
            vrMatrix.m6 = 0.0F;
            vrMatrix.m7 = 1.5F; // y
            vrMatrix.m8 = 0F;
            vrMatrix.m9 = 0.0F;
            vrMatrix.m10 = 0.0F;
            vrMatrix.m11 = -1.0F; // -z
            return vrMatrix;
        }

        #region Load and save matrices functions

        private void InitMatrices()
        {          
            try
            {
                if (File.Exists(_matricesSaveFile))
                {
                    string json = File.ReadAllText(_matricesSaveFile);
                    _savedMatrices = (Dictionary<string, MatrixDefinition>)JsonSerializer.Deserialize(json, typeof(Dictionary<string, MatrixDefinition>));
                }
                else
                {
                    _savedMatrices = new Dictionary<string, MatrixDefinition>();
                    _savedMatrices.Add("Default", DefaultVRMatrix());
                    SaveMatrices();
                }
            }
            catch
            {
                _savedMatrices = new Dictionary<string, MatrixDefinition>();
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
            numericUpDownm0.Value = (decimal)_hmdMatrix.m0;
            numericUpDownm1.Value = (decimal)_hmdMatrix.m1;
            numericUpDownm2.Value = (decimal)_hmdMatrix.m2;
            numericUpDownm3.Value = (decimal)_hmdMatrix.m3;
            numericUpDownm4.Value = (decimal)_hmdMatrix.m4;
            numericUpDownm5.Value = (decimal)_hmdMatrix.m5;
            numericUpDownm6.Value = (decimal)_hmdMatrix.m6;
            numericUpDownm7.Value = (decimal)_hmdMatrix.m7;
            numericUpDownm8.Value = (decimal)_hmdMatrix.m8;
            numericUpDownm9.Value = (decimal)_hmdMatrix.m9;
            numericUpDownm10.Value = (decimal)_hmdMatrix.m10;
            numericUpDownm11.Value = (decimal)_hmdMatrix.m11;
        }

        public void SetMatrix(ref HmdMatrix34_t hmdMatrix)
        {
            _hmdMatrix = hmdMatrix;
            ApplyMatrixToOverlay(true);
        }

        public void ReapplyMatrix(ref HmdMatrix34_t hmdMatrix)
        {
            hmdMatrix = _hmdMatrix;
            OpenVR.Overlay.SetOverlayTransformAbsolute(_overlayHandle, Valve.VR.ETrackingUniverseOrigin.TrackingUniverseStanding, ref _hmdMatrix);
        }

        private void ApplyMatrixDefinition(MatrixDefinition hmdMatrix)
        {
            // Apply the given matrix to our VR referenced matrix
            _hmdMatrix.m0 = hmdMatrix.m0;
            _hmdMatrix.m1 = hmdMatrix.m1;
            _hmdMatrix.m2 = hmdMatrix.m2;
            _hmdMatrix.m3 = hmdMatrix.m3;
            _hmdMatrix.m4 = hmdMatrix.m4;
            _hmdMatrix.m5 = hmdMatrix.m5;
            _hmdMatrix.m6 = hmdMatrix.m6;
            _hmdMatrix.m7 = hmdMatrix.m7;
            _hmdMatrix.m8 = hmdMatrix.m8;
            _hmdMatrix.m9 = hmdMatrix.m9;
            _hmdMatrix.m10 = hmdMatrix.m10;
            _hmdMatrix.m11 = hmdMatrix.m11;
            DisplayMatrix();
        }

        public void SetOverlayWidth(Single WidthInMetres)
        {
            numericUpDownOverlayWidth.Value = (decimal)WidthInMetres;
            ApplyOverlayWidth();
        }

        public HmdMatrix34_t GetMatrix()
        {
            _hmdMatrix.m0 = (float)numericUpDownm0.Value;
            _hmdMatrix.m1 = (float)numericUpDownm1.Value;
            _hmdMatrix.m2 = (float)numericUpDownm2.Value;
            _hmdMatrix.m3 = (float)numericUpDownm3.Value;
            _hmdMatrix.m4 = (float)numericUpDownm4.Value;
            _hmdMatrix.m5 = (float)numericUpDownm5.Value;
            _hmdMatrix.m6 = (float)numericUpDownm6.Value;
            _hmdMatrix.m7 = (float)numericUpDownm7.Value;
            _hmdMatrix.m8 = (float)numericUpDownm8.Value;
            _hmdMatrix.m9 = (float)numericUpDownm9.Value;
            _hmdMatrix.m10 = (float)numericUpDownm10.Value;
            _hmdMatrix.m11 = (float)numericUpDownm11.Value;
            return _hmdMatrix;
        }

        public void ApplyOverlayWidth()
        {
            if (_overlayHandle > 0)
                OpenVR.Overlay.SetOverlayWidthInMeters(_overlayHandle, (float)numericUpDownOverlayWidth.Value);
        }

        private void ApplyMatrixToOverlay(bool force = false)
        {
            if (!force && !checkBoxAutoApply.Checked)
                return;
            if (_overlayHandle > 0)
            {
                GetMatrix();
                OpenVR.Overlay.SetOverlayTransformAbsolute(_overlayHandle, Valve.VR.ETrackingUniverseOrigin.TrackingUniverseStanding, ref _hmdMatrix);
            }
            if (!force)
                UpdateSelectedMatrixDefinition();
        }

        private void UpdateSelectedMatrixDefinition()
        {
            // Called when a value in the UI is updated so that we keep our stored matrices up-to-date

            if (listBoxMatrices.SelectedIndex < 0 || !_savedMatrices.ContainsKey(textBoxMatrixName.Text))
                return;

            _savedMatrices[textBoxMatrixName.Text].m0 = (float)numericUpDownm0.Value;
            _savedMatrices[textBoxMatrixName.Text].m1 = (float)numericUpDownm1.Value;
            _savedMatrices[textBoxMatrixName.Text].m2 = (float)numericUpDownm2.Value;
            _savedMatrices[textBoxMatrixName.Text].m3 = (float)numericUpDownm3.Value;
            _savedMatrices[textBoxMatrixName.Text].m4 = (float)numericUpDownm4.Value;
            _savedMatrices[textBoxMatrixName.Text].m5 = (float)numericUpDownm5.Value;
            _savedMatrices[textBoxMatrixName.Text].m6 = (float)numericUpDownm6.Value;
            _savedMatrices[textBoxMatrixName.Text].m7 = (float)numericUpDownm7.Value;
            _savedMatrices[textBoxMatrixName.Text].m8 = (float)numericUpDownm8.Value;
            _savedMatrices[textBoxMatrixName.Text].m9 = (float)numericUpDownm9.Value;
            _savedMatrices[textBoxMatrixName.Text].m10 = (float)numericUpDownm10.Value;
            _savedMatrices[textBoxMatrixName.Text].m11 = (float)numericUpDownm11.Value;
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

        private void buttonExport_Click(object sender, EventArgs e)
        {
            StringBuilder matrixCode = new StringBuilder();

            matrixCode.AppendLine("_vrMatrix = new HmdMatrix34_t();");
            matrixCode.AppendLine($"_vrMatrix.m0 = {numericUpDownm0.Value}F;");
            matrixCode.AppendLine($"_vrMatrix.m1 = {numericUpDownm1.Value}F;");
            matrixCode.AppendLine($"_vrMatrix.m2 = {numericUpDownm2.Value}F;");
            matrixCode.AppendLine($"_vrMatrix.m3 = {numericUpDownm3.Value}F;");
            matrixCode.AppendLine($"_vrMatrix.m4 = {numericUpDownm4.Value}F;");
            matrixCode.AppendLine($"_vrMatrix.m5 = {numericUpDownm5.Value}F;");
            matrixCode.AppendLine($"_vrMatrix.m6 = {numericUpDownm6.Value}F;");
            matrixCode.AppendLine($"_vrMatrix.m7 = {numericUpDownm7.Value}F;");
            matrixCode.AppendLine($"_vrMatrix.m8 = {numericUpDownm8.Value}F;");
            matrixCode.AppendLine($"_vrMatrix.m9 = {numericUpDownm9.Value}F;");
            matrixCode.AppendLine($"_vrMatrix.m10 = {numericUpDownm10.Value}F;");
            matrixCode.AppendLine($"_vrMatrix.m11 = {numericUpDownm11.Value}F;");
            matrixCode.AppendLine();

            File.AppendAllText("matrices.txt", matrixCode.ToString());
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
                    ApplyMatrixDefinition(_savedMatrices[textBoxMatrixName.Text]);
                    ApplyMatrixToOverlay();
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            GetMatrix();
            string matrixName = $"Matrix {_savedMatrices.Count + 1}";
            _savedMatrices.Add(matrixName, MatrixDefinition.FromHmdMatrix34_t(ref _hmdMatrix, (float)numericUpDownOverlayWidth.Value));
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

            MatrixDefinition matrixDefinition = _savedMatrices[(string)listBoxMatrices.SelectedItem];
            _savedMatrices.Remove((string)listBoxMatrices.SelectedItem);
            _savedMatrices.Add(textBoxMatrixName.Text, matrixDefinition);
            listBoxMatrices.Items[listBoxMatrices.SelectedIndex] = textBoxMatrixName.Text;
        }
        #endregion

        #region Matrix Value Updating

        private void numericUpDownm3_ValueChanged(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay();
        }

        private void numericUpDownm0_ValueChanged(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay();
        }

        private void numericUpDownm4_ValueChanged(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay();
        }

        private void numericUpDownm8_ValueChanged(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay();
        }

        private void numericUpDownm1_ValueChanged(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay();
        }

        private void numericUpDownm5_ValueChanged(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay();
        }

        private void numericUpDownm9_ValueChanged(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay();
        }

        private void numericUpDownm2_ValueChanged(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay();
        }

        private void numericUpDownm6_ValueChanged(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay();
        }

        private void numericUpDownm10_ValueChanged(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay();
        }

        private void numericUpDownm7_ValueChanged(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay();
        }

        private void numericUpDownm11_ValueChanged(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay();
        }
        #endregion

        #region Slider Functionality

        private void trackBarEditMatrixValue_Scroll(object sender, EventArgs e)
        {
            if (_sliderTargetControl == null)
                return;
            ((NumericUpDown)_sliderTargetControl).Value = ((decimal)trackBarEditMatrixValue.Value / 1000);
        }

        private void AttachSliderToNumericUpDown(NumericUpDown attachedControl)
        {
            int sliderValue = (int)(attachedControl.Value * 1000);
            _sliderTargetControl = attachedControl;
            if (sliderValue > trackBarEditMatrixValue.Maximum)
                trackBarEditMatrixValue.Maximum = sliderValue;
            if (sliderValue < trackBarEditMatrixValue.Minimum)
                trackBarEditMatrixValue.Minimum = sliderValue;
            trackBarEditMatrixValue.Value = sliderValue;
        }

        private void numericUpDownm3_Enter(object sender, EventArgs e)
        {
            AttachSliderToNumericUpDown(numericUpDownm3);
        }

        private void numericUpDownm7_Enter(object sender, EventArgs e)
        {
            AttachSliderToNumericUpDown(numericUpDownm7);
        }

        private void numericUpDownm11_Enter(object sender, EventArgs e)
        {
            AttachSliderToNumericUpDown(numericUpDownm11);
        }

        private void numericUpDownm0_Enter(object sender, EventArgs e)
        {
            AttachSliderToNumericUpDown(numericUpDownm0);
        }

        private void numericUpDownm5_Enter(object sender, EventArgs e)
        {
            AttachSliderToNumericUpDown(numericUpDownm5);
        }

        private void numericUpDownm10_Enter(object sender, EventArgs e)
        {
            AttachSliderToNumericUpDown(numericUpDownm10);
        }

        private void numericUpDownm6_Enter(object sender, EventArgs e)
        {
            AttachSliderToNumericUpDown(numericUpDownm6);
        }

        private void numericUpDownm2_Enter(object sender, EventArgs e)
        {
            AttachSliderToNumericUpDown(numericUpDownm2);
        }

        private void numericUpDownm1_Enter(object sender, EventArgs e)
        {
            AttachSliderToNumericUpDown(numericUpDownm1);
        }

        private void numericUpDownm9_Enter(object sender, EventArgs e)
        {
            AttachSliderToNumericUpDown(numericUpDownm9);
        }

        private void numericUpDownm8_Enter(object sender, EventArgs e)
        {
            AttachSliderToNumericUpDown(numericUpDownm8);
        }

        private void numericUpDownm4_Enter(object sender, EventArgs e)
        {
            AttachSliderToNumericUpDown(numericUpDownm4);
        }
        #endregion
    }
}
