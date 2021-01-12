using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Valve.VR;
using System.Text.Json;
using EDTracking;

namespace SRVTracker
{
    public class VRPanelCollection
    {
        //private List<VRPanelSetting> _vrPanelSettings;
        private Dictionary<string, MatrixDefinition> _vrPanelSettings = null;
        private string _vrPanelSettingsSaveFile = "hmd_matrices.json";
        public string ActivePanelName { get; internal set; } = "";

        public VRPanelCollection()
        {
            LoadPanels();
        }

        public void ApplyPanelSettings(ulong VROverlayHandle, string PanelName = "")
        {
            // Apply the transform to the given Overlay (we set the matrix and panel size)
            if (String.IsNullOrEmpty(PanelName))
                PanelName = ActivePanelName;
            if (!_vrPanelSettings.ContainsKey(PanelName))
                return;

            OpenVR.Overlay.SetOverlayWidthInMeters(VROverlayHandle, _vrPanelSettings[PanelName].PanelWidth);
            HmdMatrix34_t matrix = _vrPanelSettings[PanelName].ToHmdMatrix34_t();
            OpenVR.Overlay.SetOverlayTransformAbsolute(VROverlayHandle, Valve.VR.ETrackingUniverseOrigin.TrackingUniverseStanding, ref matrix);
        }

        public List<String> PanelNames
        {
            get
            {
                return new List<String>(_vrPanelSettings.Keys);
            }
        }

        public bool UpdatePanel(MatrixDefinition NewDefinition, string PanelName = "")
        {
            if (String.IsNullOrEmpty(PanelName))
                PanelName = ActivePanelName;

            if (!_vrPanelSettings.ContainsKey(PanelName))
                return false;

            _vrPanelSettings[PanelName] = NewDefinition;
            return true;
        }

        public bool RenamePanel(string NewPanelName, string ExistingPanelName = "")
        {
            if (String.IsNullOrEmpty(ExistingPanelName))
                ExistingPanelName = ActivePanelName;

            if (!_vrPanelSettings.ContainsKey(ExistingPanelName))
                return false;

            MatrixDefinition m = _vrPanelSettings[ExistingPanelName];
            _vrPanelSettings.Remove(ExistingPanelName);
            _vrPanelSettings.Add(NewPanelName, m);
            return true;
        }

        public MatrixDefinition GetPanel(string PanelName = "")
        {
            if (String.IsNullOrEmpty(PanelName))
                PanelName = ActivePanelName;

            if (!_vrPanelSettings.ContainsKey(PanelName))
                return null;

            return _vrPanelSettings[PanelName];
        }

        public bool SetActivePanel(string PanelName)
        {
            if (!_vrPanelSettings.ContainsKey(PanelName))
                return false;

            ActivePanelName = PanelName;
            return true;
        }

        private void LoadPanels()
        {
            try
            {
                if (File.Exists(_vrPanelSettingsSaveFile))
                {
                    string json = File.ReadAllText(_vrPanelSettingsSaveFile);
                    _vrPanelSettings = (Dictionary<string, MatrixDefinition>)JsonSerializer.Deserialize(json, typeof(Dictionary<string, MatrixDefinition>));
                    ActivePanelName = "";
                    if (_vrPanelSettings.Count>0)
                        if (_vrPanelSettings.ContainsKey("Default"))
                            ActivePanelName = "Default";
                    return;
                }
            }
            catch { }
            _vrPanelSettings = new Dictionary<string, MatrixDefinition>();
            _vrPanelSettings.Add("Default", VRPanelSetting.DefaultVRMatrix().HMDPanelMatrix);
            SavePanels();
        }

        public void SavePanels()
        {
            try
            {
                File.WriteAllText(_vrPanelSettingsSaveFile, JsonSerializer.Serialize(_vrPanelSettings));
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to save matrices:{Environment.NewLine}{Environment.NewLine}{ex}", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

    }

    public class VRPanelSetting
    {
        public MatrixDefinition HMDPanelMatrix;
        public string PanelName = "Panel definition";

        public VRPanelSetting()
        {
            // Required for JSON serialisation
        }

        public VRPanelSetting(string Name, MatrixDefinition Matrix)
        {
            HMDPanelMatrix = Matrix;
            PanelName = Name;
        }

        public static VRPanelSetting DefaultVRMatrix()
        {
            MatrixDefinition defaultMatrix = new MatrixDefinition();
            defaultMatrix.m0 = 0.7F;
            defaultMatrix.m1 = 0.0F;
            defaultMatrix.m2 = 0.0F;
            defaultMatrix.m3 = 1.0F; // x
            defaultMatrix.m4 = 0.0F;
            defaultMatrix.m5 = -1.0F;
            defaultMatrix.m6 = 0.0F;
            defaultMatrix.m7 = 1.5F; // y
            defaultMatrix.m8 = 0F;
            defaultMatrix.m9 = 0.0F;
            defaultMatrix.m10 = 0.0F;
            defaultMatrix.m11 = -1.0F; // -z
            defaultMatrix.PanelWidth = 0.6f;
            return new VRPanelSetting("Default", defaultMatrix);
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
            // Required for JSON serialisation to work
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

        public HmdMatrix34_t ToHmdMatrix34_t()
        {
            // Return this matrix as HmdMatrix34_t
            HmdMatrix34_t hmdMatrix = new HmdMatrix34_t();
            hmdMatrix.m0 = m0;
            hmdMatrix.m1 = m1;
            hmdMatrix.m2 = m2;
            hmdMatrix.m3 = m3;
            hmdMatrix.m4 = m4;
            hmdMatrix.m5 = m5;
            hmdMatrix.m6 = m6;
            hmdMatrix.m7 = m7;
            hmdMatrix.m8 = m8;
            hmdMatrix.m9 = m9;
            hmdMatrix.m10 = m10;
            hmdMatrix.m11 = m11;
            return hmdMatrix;
        }
    }
}