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
    public partial class FormVRMatrixTest : Form
    {
        private ulong _overlayHandle = 0;
        private HmdMatrix34_t _hmdMatrix;
        private Dictionary<string, MatrixDefinition> _savedMatrices = null;
        private string _matricesSaveFile = "hmd_matrices.json";
        private ConfigSaverClass _formConfig = null;

        public FormVRMatrixTest(ulong overlayHandle)
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

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        public void GetMatrix()
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
        }

        private void ApplyOverlayWidth()
        {
            if (_overlayHandle > 0)
                OpenVR.Overlay.SetOverlayWidthInMeters(_overlayHandle, (float)numericUpDownOverlayWidth.Value);
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            ApplyMatrixToOverlay(true);
            ApplyOverlayWidth();
            if (checkBoxAutoApply.Checked)
                buttonApply.Enabled = false;
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

        private void textBoxMatrixName_TextChanged(object sender, EventArgs e)
        {

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
    }

    public class MatrixDefinition
    {
        public float m0 { get; set; } = 0;
        public float m1 { get; set; } = 0;
        public float m2 { get; set; } = 0;
        public float m3 { get; set; } = 0;
        public float m4 { get; set; } = 0;
        public float m5 { get; set; } = 0;
        public float m6 { get; set; } = 0;
        public float m7 { get; set; } = 0;
        public float m8 { get; set; } = 0;
        public float m9 { get; set; } = 0;
        public float m10 { get; set; } = 0;
        public float m11 { get; set; } = 0;
        public float PanelWidth { get; set; } = 0.8f;

        public MatrixDefinition()
        {

        }

        public MatrixDefinition(float m0, float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9, float m10, float m11, float PanelWidth)
        {
            this.m0 = m0;
            this.m1 = m1;
            this.m2 = m2;
            this.m3 = m3;
            this.m4 = m4;
            this.m5 = m5;
            this.m6 = m6;
            this.m7 = m7;
            this.m8 = m8;
            this.m9 = m9;
            this.m10 = m10;
            this.m11 = m11;
            this.PanelWidth = PanelWidth;
        }

        public static MatrixDefinition FromHmdMatrix34_t(ref HmdMatrix34_t hmdMatrix, float PanelWidth)
        {
            // Create a new matrix defintion from the provided matrix
            return new MatrixDefinition(hmdMatrix.m0, hmdMatrix.m1, hmdMatrix.m2, hmdMatrix.m3, hmdMatrix.m4, hmdMatrix.m5,
                hmdMatrix.m6, hmdMatrix.m7, hmdMatrix.m8, hmdMatrix.m9, hmdMatrix.m10, hmdMatrix.m11, PanelWidth);
        }
    }
}
