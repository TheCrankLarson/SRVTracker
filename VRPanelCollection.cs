using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using EDTracking;
using Valve.VR;
using OpenVRApiModule.Maths;
using System.Numerics;

namespace SRVTracker
{
    public class VRPanelCollection
    {
        //private List<VRPanelSetting> _vrPanelSettings;
        private Dictionary<string, TransformDefinition> _vrPanelSettings = null;
        private string _vrPanelSettingsSaveFile = "hmd_matrices.json";
        public string ActivePanelName { get; internal set; } = "";

        public VRPanelCollection()
        {
            LoadPanels();
        }

        public void ApplyPanelSettings(VRLocatorOverlay VROverlay, string PanelName = "")
        {
            // Apply the transform to the given Overlay (we set the matrix and panel size)
            if (String.IsNullOrEmpty(PanelName))
                PanelName = ActivePanelName;
            if (!_vrPanelSettings.ContainsKey(PanelName))
                return;

            VROverlay.WidthInMeters = _vrPanelSettings[PanelName].Width;
            VROverlay.Transform = _vrPanelSettings[PanelName].ToHmdMatrix34_t();
        }

        public List<String> PanelNames
        {
            get
            {
                return new List<String>(_vrPanelSettings.Keys);
            }
        }

        public bool UpdatePanel(TransformDefinition NewDefinition, string PanelName = "")
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

            TransformDefinition m = _vrPanelSettings[ExistingPanelName];
            _vrPanelSettings.Remove(ExistingPanelName);
            _vrPanelSettings.Add(NewPanelName, m);
            return true;
        }

        public TransformDefinition GetPanel(string PanelName = "")
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
                    _vrPanelSettings = (Dictionary<string, TransformDefinition>)JsonSerializer.Deserialize(json, typeof(Dictionary<string, TransformDefinition>));
                    ActivePanelName = "";
                    if (_vrPanelSettings.Count>0)
                        if (_vrPanelSettings.ContainsKey("Default"))
                            ActivePanelName = "Default";
                    return;
                }
            }
            catch { }
            _vrPanelSettings = new Dictionary<string, TransformDefinition>();
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
        public TransformDefinition HMDPanelMatrix;
        public string PanelName = "Panel definition";

        public VRPanelSetting()
        {
            // Required for JSON serialisation
        }

        public VRPanelSetting(string Name, TransformDefinition Matrix)
        {
            HMDPanelMatrix = Matrix;
            PanelName = Name;
        }

        public static VRPanelSetting DefaultVRMatrix()
        {
            TransformDefinition defaultMatrix = new TransformDefinition();
            return new VRPanelSetting("Default", defaultMatrix);
        }
    }

    public class TransformDefinition
    {
        public float PositionX { get; set; } = 0;
        public float PositionY { get; set; } = 0;
        public float PositionZ { get; set; } = 0;
        public float RotationX { get; set; } = 0;
        public float RotationY { get; set; } = 0;
        public float RotationZ { get; set; } = 0;
        public float Width { get; set; } = 0.8f;

        public TransformDefinition()
        {
            // Required for JSON serialisation to work
        }

        public TransformDefinition(float PositionX, 
                                   float PositionY, 
                                   float PositionZ, 
                                   float RotationX, 
                                   float RotationY,
                                   float RotationZ, 
                                   float Width)
        {
            this.PositionX = PositionX;
            this.PositionY = PositionY;
            this.PositionZ = PositionZ;
            this.RotationX = RotationX;
            this.RotationY = RotationY;
            this.RotationZ = RotationZ;
            this.Width = Width;
        }

        private float DegreesToRadians(float degrees)
        {
            if (degrees < 0)
                degrees = 360 + degrees;
            return (float)((Math.PI / 180) * degrees); // degrees to radians
        }

        public HmdMatrix34_t ToHmdMatrix34_t()
        {
            // Return this transform as HmdMatrix34_t

            Matrix4x4 rotation = Matrix4x4.CreateFromYawPitchRoll(DegreesToRadians(RotationY), DegreesToRadians(RotationX), DegreesToRadians(RotationZ));
            Matrix4x4 translation = Matrix4x4.CreateTranslation(PositionX, PositionY, PositionZ);
            
            return Matrix4x4.Add(rotation, translation).ToHmdMatrix34_t();
        }
    }
}