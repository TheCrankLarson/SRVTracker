using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valve.VR;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using d3d = Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using System.IO;

namespace SRVTracker
{
    class VRLocator
    {
        private ulong _vrOverlayHandle = 0;
        private HmdMatrix34_t _vrMatrix;
        private Bitmap _vrbitmap = null;
        private d3d.Device _d3DDevice = null;
        private d3d.Texture _locatorTexture = null;
        private bool _locatorVRAsTexture = false; // Whether to create texture for locator VR HUD, or just use raw image
        private Texture_t _panelTexture = new Texture_t();
        public static CVRSystem VRSystem = null;
        private FormVRMatrixEditor _formVRMatrixTest = null;

        public VRLocator()
        {
            _panelTexture.eType = ETextureType.DirectX;
            InitVRMatrix();
        }

        public ulong OverlayHandle
        {
            get { return _vrOverlayHandle; }
        }

        public bool CreateOverlay(ref string errorInfo)
        {
            try
            {
                OpenVR.Overlay.CreateOverlay(Guid.NewGuid().ToString(), "SRV Tracking", ref _vrOverlayHandle);
                InitOverlay();
                return true;
            }
            catch (Exception ex)
            {
                errorInfo = $"CreateOverlay: {Environment.NewLine}{ex}";
                return false;
            }
        }

        public void InitOverlay()
        {
            if (_formVRMatrixTest==null || _formVRMatrixTest.IsDisposed)
                _formVRMatrixTest = new FormVRMatrixEditor(_vrOverlayHandle);
            _formVRMatrixTest.SetMatrix(ref _vrMatrix);
            _formVRMatrixTest.SetOverlayWidth(0.6f);
            _vrMatrix = _formVRMatrixTest.GetMatrix();

            OpenVR.Overlay.SetOverlayTransformAbsolute(_vrOverlayHandle, Valve.VR.ETrackingUniverseOrigin.TrackingUniverseStanding, ref _vrMatrix);
            OpenVR.Overlay.SetOverlayWidthInMeters(_vrOverlayHandle, 0.6f);  // Need to change to keep track of width
        }

        public void Show()
        {
            _ = OpenVR.Overlay.ShowOverlay(_vrOverlayHandle);
        }

        public void Hide(bool CloseMatrixWindow = true)
        {
            try
            {
                OpenVR.Overlay.DestroyOverlay(_vrOverlayHandle);
            }
            catch { }

            _vrOverlayHandle = 0;
            if (_formVRMatrixTest != null && !_formVRMatrixTest.IsDisposed)
                _vrMatrix = _formVRMatrixTest.GetMatrix();

            if (CloseMatrixWindow)
            {
                if (_formVRMatrixTest != null && !_formVRMatrixTest.IsDisposed && _formVRMatrixTest.Visible)
                    _formVRMatrixTest.Close();
                _formVRMatrixTest = null;
            }
            VRLocator.ShutdownVr();
        }

        public static bool InitVR()
        {
            if (VRSystem == null)
            {
                var initError = EVRInitError.None;
                try
                {
                    VRSystem = OpenVR.Init(ref initError, EVRApplicationType.VRApplication_Overlay);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(null, $"Failed to initialise VR: {ex.Message}\r\nInit error: {initError}", "VR Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        public void ShowMatrixEditor()
        {
            if (_formVRMatrixTest == null || _formVRMatrixTest.Visible)
                return;
            _formVRMatrixTest.Show();
        }

        public static void ShutdownVr()
        {
            OpenVR.Shutdown();
            VRSystem = null;
        }

        private void InitVRMatrix()
        {
            _vrMatrix = new HmdMatrix34_t();
            _vrMatrix.m0 = 0.7F;
            _vrMatrix.m1 = 0.0F;
            _vrMatrix.m2 = 0.0F;
            _vrMatrix.m3 = 0.5F; // x
            _vrMatrix.m4 = 0.0F;
            _vrMatrix.m5 = -1.0F;
            _vrMatrix.m6 = 0.0F;
            _vrMatrix.m7 = 1.5F; // y
            _vrMatrix.m8 = 0F;
            _vrMatrix.m9 = 0.0F;
            _vrMatrix.m10 = 0.0F;
            _vrMatrix.m11 = -1.5F; // -z
        }

        public bool InitializeGraphics(ref string InitError)
        {
            if (!_locatorVRAsTexture || _d3DDevice != null)
                return true;
            try
            {
                // Now  setup our D3D stuff
                d3d.PresentParameters presentParams = new d3d.PresentParameters();
                presentParams.Windowed = true;
                presentParams.SwapEffect = d3d.SwapEffect.Discard;
                _d3DDevice = new d3d.Device(0, d3d.DeviceType.Hardware, null,
                        d3d.CreateFlags.SoftwareVertexProcessing, presentParams);
                return true;
            }
            catch (DirectXException ex)
            {
                InitError = ex.Message;
                return false;
            }
        }

        private d3d.Texture GetVRLocatorTexture()
        {
            // https://docs.microsoft.com/en-us/windows/win32/direct3d10/d3d10-graphics-programming-guide-resources-creating-textures#filling-textures-manually
            // https://docs.microsoft.com/en-us/windows/win32/direct3d11/overviews-direct3d-11-resources-textures-create

            if (_locatorTexture == null)
                using (MemoryStream ms = new MemoryStream())
                {
                    _vrbitmap.Save(ms, ImageFormat.Bmp);
                    _locatorTexture = d3d.TextureLoader.FromStream(_d3DDevice, ms);
                }
            return _locatorTexture;
            /*
            _texture = new Microsoft.DirectX.Direct3D.Texture2D(D3DDevice, new D3D11.Texture2DDescription()
            {
                Width = _bitmap.Width,
                Height = _bitmap.Height,
                ArraySize = 1,
                BindFlags = D3D11.BindFlags.ShaderResource,
                Usage = D3D11.ResourceUsage.Immutable,
                CpuAccessFlags = D3D11.CpuAccessFlags.None,
                Format = DXGI.Format.B8G8R8A8_UNorm,
                MipLevels = 1,
                OptionFlags = D3D11.ResourceOptionFlags.None,
                SampleDescription = new DXGI.SampleDescription(1, 0),
            }, new SharpDX.DataRectangle(textData.Scan0, textData.Stride));
            */
        }

        public unsafe void UpdateVRLocatorImage(Bitmap PanelImage)
        {
            _vrbitmap = PanelImage;
            GetVRLocatorTexture();
            
            if (_locatorTexture == null)
                return;
            _panelTexture.handle = (IntPtr)_locatorTexture.UnmanagedComPointer;
            OpenVR.Overlay.SetOverlayTexture(_vrOverlayHandle, ref _panelTexture);
        }
    }
}
