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

namespace SRVTracker
{
    public partial class FormVRMatrixTest : Form
    {
        private ulong _overlayHandle = 0;
        private HmdMatrix34_t _hmdMatrix;
        private Dictionary<string, HmdMatrix34_t> _savedMatrices = null;
        private string _matricesSaveFile = "hmd_matrices.json";

        public FormVRMatrixTest(ulong overlayHandle)
        {
            InitializeComponent();
            InitMatrices();
            _overlayHandle = overlayHandle;
            _hmdMatrix = new HmdMatrix34_t();
            buttonApply.Enabled = !checkBoxAutoApply.Checked;
        }

        private void InitMatrices()
        {          
            try
            {
                if (File.Exists(_matricesSaveFile))
                {
                    string json = File.ReadAllText(_matricesSaveFile);
                    _savedMatrices = (Dictionary<string, HmdMatrix34_t>)JsonSerializer.Deserialize(json, typeof(Dictionary<string, HmdMatrix34_t>));
                }
            }
            catch
            {
                _savedMatrices = new Dictionary<string, HmdMatrix34_t>();
            }

            listBoxMatrices.Items.Clear();
            foreach (string matrixName in _savedMatrices.Keys)
                listBoxMatrices.Items.Add(matrixName);
        }

        private void SaveMatrices()
        {
            try
            {
                File.WriteAllText(_matricesSaveFile, JsonSerializer.Serialize(_savedMatrices));
            }
            catch { }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetMatrix(ref HmdMatrix34_t hmdMatrix)
        {
            bool autoApply = checkBoxAutoApply.Checked;
            if (checkBoxAutoApply.Checked)
                checkBoxAutoApply.Checked = false;

            numericUpDownm0.Value = (decimal)hmdMatrix.m0;
            numericUpDownm1.Value = (decimal)hmdMatrix.m1;
            numericUpDownm2.Value = (decimal)hmdMatrix.m2;
            numericUpDownm3.Value = (decimal)hmdMatrix.m3;
            numericUpDownm4.Value = (decimal)hmdMatrix.m4;
            numericUpDownm5.Value = (decimal)hmdMatrix.m5;
            numericUpDownm6.Value = (decimal)hmdMatrix.m6;
            numericUpDownm7.Value = (decimal)hmdMatrix.m7;
            numericUpDownm8.Value = (decimal)hmdMatrix.m8;
            numericUpDownm9.Value = (decimal)hmdMatrix.m9;
            numericUpDownm10.Value = (decimal)hmdMatrix.m10;
            numericUpDownm11.Value = (decimal)hmdMatrix.m11;
            _hmdMatrix = hmdMatrix;

            checkBoxAutoApply.Checked = autoApply;
            ApplyMatrixToOverlay(true);
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

        private void ApplyOverlayWidth()
        {
            if (_overlayHandle > 0)
            {
                OpenVR.Overlay.SetOverlayWidthInMeters(_overlayHandle, (float)numericUpDownOverlayWidth.Value);
            }
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
        }
    }
}
