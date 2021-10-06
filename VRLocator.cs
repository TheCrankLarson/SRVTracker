using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace SRVTracker
{
    public class VRLocator
    {
        //private ulong _vrOverlayHandle = 0;
        private Bitmap _vrbitmap = null;
        private SlimDX.Direct3D9.Device _d3DDevice = null;
        private SlimDX.Direct3D9.Texture _locatorTexture = null;
        private byte[] _vrPanelImageBytes = null;
        private IntPtr _intPtrVROverlayImage;
        private bool _locatorVRAsTexture = false; // Whether to create texture for locator VR HUD, or just use raw image
        private Valve.VR.Texture_t _panelTexture = new Valve.VR.Texture_t();
        //public static CVRSystem VRSystem = null;
        private FormVRMatrixEditor _formVRMatrixEditor = null;
        private VRLocatorOverlay _locatorOverlay = null;
        private PictureBox _locatorPictureBox = new PictureBox();

        public VRLocator(bool ShowMatrixEditor = true)
        {
            //_panelTexture.eType = ETextureType.DirectX;
            _locatorOverlay = new VRLocatorOverlay();
            _locatorPictureBox.Width = 800;
            _locatorPictureBox.Height = 320;

            //if (ShowMatrixEditor)
                InitMatrixEditor();
            //string initError="";
            //if (_locatorVRAsTexture)
            //    InitializeGraphics(ref initError);
        }

        public ulong OverlayHandle
        {
            get { return _locatorOverlay.Handle; }
        }

        //public bool CreateOverlay(ref string errorInfo)
        //{
        //    try
        //    {
        //        OpenVR.Overlay.CreateOverlay(Guid.NewGuid().ToString(), "SRV Tracking", ref _vrOverlayHandle);
        //        InitOverlay();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        errorInfo = $"CreateOverlay: {Environment.NewLine}{ex}";
        //        return false;
        //    }
        //}

        public void InitMatrixEditor()
        {
            if (_formVRMatrixEditor == null || _formVRMatrixEditor.IsDisposed)
                _formVRMatrixEditor = new FormVRMatrixEditor(_locatorOverlay);

            //_formVRMatrixTest.ApplyMatrixToOverlay(true);
        }

        public void Show()
        {
            _locatorOverlay.Show();
        }

        public void Hide(bool CloseMatrixWindow = true)
        {
            try
            {
                _locatorOverlay.Hide();
            }
            catch { }

            if (_vrPanelImageBytes != null)
            {
                Marshal.FreeHGlobal(_intPtrVROverlayImage);
                _vrPanelImageBytes = null;
            }

            if (CloseMatrixWindow)
            {
                if (_formVRMatrixEditor != null && !_formVRMatrixEditor.IsDisposed && _formVRMatrixEditor.Visible)
                    _formVRMatrixEditor.Close();
                _formVRMatrixEditor = null;
            }
            //VRLocator.ShutdownVr();
        }

        //public static bool InitVR()
        //{
        //    if (VRSystem == null)
        //    {
        //        var initError = EVRInitError.None;
        //        try
        //        {
        //            VRSystem = OpenVR.Init(ref initError, EVRApplicationType.VRApplication_Overlay);
        //        }
        //        catch (Exception ex)
        //        {
        //            System.Windows.Forms.MessageBox.Show(null, $"Failed to initialise VR: {ex.Message}\r\nInit error: {initError}", "VR Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        public void ShowMatrixEditor()
        {
            if (_formVRMatrixEditor == null || _formVRMatrixEditor.Visible)
                return;
            _formVRMatrixEditor.Show();
        }

        //public static void ShutdownVr()
        //{
        //    OpenVR.Shutdown();
        //    VRSystem = null;
        //}

        /*
        private void InitVRMatrix()
        {
            _vrMatrix = new HmdMatrix34_t();

            /*
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
            

            _vrMatrix.m0 = 1.0F;
            _vrMatrix.m1 = 0.0F;
            _vrMatrix.m2 = 0.0F;
            _vrMatrix.m3 = 0.12F; // x
            _vrMatrix.m4 = 0.0F;
            _vrMatrix.m5 = 1.0F;
            _vrMatrix.m6 = 0.0F;
            _vrMatrix.m7 = 0.08F; // y
            _vrMatrix.m8 = 0F;
            _vrMatrix.m9 = 0.0F;
            _vrMatrix.m10 = 1.0F;
            _vrMatrix.m11 = -0.3F; // -z
        }*/

        public bool InitializeGraphics()
        {
            if (!_locatorVRAsTexture || _d3DDevice != null)
                return true;
            try
            {
                // Now  setup our D3D stuff


                SlimDX.Direct3D9.PresentParameters presentParams = new SlimDX.Direct3D9.PresentParameters
                {
                    BackBufferWidth = _locatorPictureBox.Width,
                    BackBufferHeight = _locatorPictureBox.Height,
                    DeviceWindowHandle = _locatorPictureBox.Handle,
                    PresentFlags = SlimDX.Direct3D9.PresentFlags.None,
                    Multisample = SlimDX.Direct3D9.MultisampleType.None,
                    BackBufferCount = 0,
                    PresentationInterval = SlimDX.Direct3D9.PresentInterval.Immediate,
                    SwapEffect = SlimDX.Direct3D9.SwapEffect.Flip,
                    BackBufferFormat = SlimDX.Direct3D9.Format.X8R8G8B8,
                    Windowed = true,
                };

                _d3DDevice = new SlimDX.Direct3D9.Device(new SlimDX.Direct3D9.Direct3D(), 0, SlimDX.Direct3D9.DeviceType.Hardware, _locatorPictureBox.Handle, SlimDX.Direct3D9.CreateFlags.HardwareVertexProcessing, presentParams);

                //SlimDX.Direct3D9.PresentParameters presentParams = new SlimDX.Direct3D9.PresentParameters();
                //presentParams.Windowed = true;
                //presentParams.SwapEffect = SlimDX.Direct3D9.SwapEffect.Discard;
                ////presentParams.DeviceWindow =;
                //_d3DDevice = new SlimDX.Direct3D9.Device(null, 0, SlimDX.Direct3D9.DeviceType.Hardware, null,
                //        SlimDX.Direct3D9.CreateFlags.HardwareVertexProcessing, presentParams);
                return true;
            }
            catch (Exception ex)
            {
                //InitError = ex.Message;
                return false;
            }
        }

        private SlimDX.Direct3D9.Texture GetVRLocatorTexture()
        {
            // https://docs.microsoft.com/en-us/windows/win32/direct3d10/d3d10-graphics-programming-guide-resources-creating-textures#filling-textures-manually
            // https://docs.microsoft.com/en-us/windows/win32/direct3d11/overviews-direct3d-11-resources-textures-create

            if (_d3DDevice == null)
                InitializeGraphics();

            if (_locatorTexture == null)
                using (MemoryStream ms = new MemoryStream())
                {
                    _vrbitmap.Save(ms, ImageFormat.Bmp);
                    
                    _locatorTexture = SlimDX.Direct3D9.Texture.FromStream(_d3DDevice, ms);
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

        public static byte[] BitmapToByte(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                return ms.ToArray();
            }
        }

        private void UpdateVRLocatorTexture()
        {
            try
            {
                GetVRLocatorTexture();
            }
            catch { }

            if (_locatorTexture == null)
                return;
            _locatorOverlay.SetTexture(_panelTexture);

            return;
        }

        public void UpdateVRLocatorImage(Bitmap PanelImage)
        {
            _vrbitmap = PanelImage;
            _vrbitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            if (_locatorVRAsTexture)
            {
                UpdateVRLocatorTexture();
                return;
            }

            bool needToAllocateMemory = _vrPanelImageBytes == null;
            _vrPanelImageBytes = BitmapToByte(_vrbitmap);
            if (needToAllocateMemory)
                _intPtrVROverlayImage = Marshal.AllocHGlobal(_vrPanelImageBytes.Length);
            Marshal.Copy(_vrPanelImageBytes, 0, _intPtrVROverlayImage, _vrPanelImageBytes.Length);
            _locatorOverlay.SetTextureRaw(PanelImage, _intPtrVROverlayImage);
        }
    }
}
