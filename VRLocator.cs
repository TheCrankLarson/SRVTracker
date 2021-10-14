using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using Valve.VR;

namespace SRVTracker
{
    public class VRLocator
    {
        private Bitmap _vrbitmap = null;
        private FormVRMatrixEditor _formVRMatrixEditor = null;
        private VRLocatorOverlay _locatorOverlay = null;
        //private PictureBox _locatorPictureBox = new PictureBox();
        static CVRSystem _VRSystem = null;
        public bool VRInitializedOk { private set; get; } = false;

        public VRLocator()//int panelWidth = 800, int panelHeight = 320)
        {
            //_panelTexture.eType = ETextureType.DirectX;
            VRInitializedOk = InitVR();
            //_locatorPictureBox.Width = panelWidth;
            //_locatorPictureBox.Height = panelHeight;

            if (VRInitializedOk)
            {
                _locatorOverlay = new VRLocatorOverlay();
                InitMatrixEditor();
            }
        }

        public bool InitVR()
        {
            if (_VRSystem == null)
            {
                var initError = EVRInitError.None;
                try
                {
                    _VRSystem = OpenVR.Init(ref initError, EVRApplicationType.VRApplication_Overlay);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(null, $"Failed to initialise VR: {ex.Message}\r\nInit error: {initError}", "VR Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            return _VRSystem != null;
        }

        public bool ResetVR()
        {
            if (_VRSystem != null)
            {
                OpenVR.Shutdown();
                _VRSystem = null;
            }
            if (InitVR())
            {
                Show();
                return true;
            }
            return false;
        }

        public void InitMatrixEditor()
        {
            if (_formVRMatrixEditor == null || _formVRMatrixEditor.IsDisposed)
                _formVRMatrixEditor = new FormVRMatrixEditor(_locatorOverlay);
            else
                _formVRMatrixEditor.SetOverlay(_locatorOverlay);             
        }

        public void Show()
        {
            if (_locatorOverlay==null)
            {
                _locatorOverlay = new VRLocatorOverlay();
                InitMatrixEditor();
            }
            _locatorOverlay.Show();
        }

        public void Hide(bool CloseMatrixWindow = true)
        {
            if (_locatorOverlay != null)
            {
                try
                {
                    _locatorOverlay.Hide();
                    _locatorOverlay.Destroy();
                    _locatorOverlay = null;
                }
                catch { }
            }

            if (CloseMatrixWindow)
            {
                if (_formVRMatrixEditor != null && !_formVRMatrixEditor.IsDisposed && _formVRMatrixEditor.Visible)
                    _formVRMatrixEditor.Close();
                _formVRMatrixEditor = null;
            }
        }

        public void ShowMatrixEditor()
        {
            if (_formVRMatrixEditor == null || _formVRMatrixEditor.Visible)
                return;
            _formVRMatrixEditor.Show();
        }

        public static void ShutdownVr()
        {
            OpenVR.Shutdown();
        }



        //public bool InitializeGraphics()
        //{
        //    if (!_locatorVRAsTexture || _d3DDevice != null)
        //        return true;
        //    try
        //    {
        //        // Now  setup our D3D stuff


        //        SlimDX.Direct3D9.PresentParameters presentParams = new SlimDX.Direct3D9.PresentParameters
        //        {
        //            BackBufferWidth = _locatorPictureBox.Width,
        //            BackBufferHeight = _locatorPictureBox.Height,
        //            DeviceWindowHandle = _locatorPictureBox.Handle,
        //            PresentFlags = SlimDX.Direct3D9.PresentFlags.None,
        //            Multisample = SlimDX.Direct3D9.MultisampleType.None,
        //            BackBufferCount = 0,
        //            PresentationInterval = SlimDX.Direct3D9.PresentInterval.Immediate,
        //            SwapEffect = SlimDX.Direct3D9.SwapEffect.Flip,
        //            BackBufferFormat = SlimDX.Direct3D9.Format.X8R8G8B8,
        //            Windowed = true,
        //        };

        //        _d3DDevice = new SlimDX.Direct3D9.Device(new SlimDX.Direct3D9.Direct3D(), 0, SlimDX.Direct3D9.DeviceType.Hardware, _locatorPictureBox.Handle, SlimDX.Direct3D9.CreateFlags.HardwareVertexProcessing, presentParams);

        //        //SlimDX.Direct3D9.PresentParameters presentParams = new SlimDX.Direct3D9.PresentParameters();
        //        //presentParams.Windowed = true;
        //        //presentParams.SwapEffect = SlimDX.Direct3D9.SwapEffect.Discard;
        //        ////presentParams.DeviceWindow =;
        //        //_d3DDevice = new SlimDX.Direct3D9.Device(null, 0, SlimDX.Direct3D9.DeviceType.Hardware, null,
        //        //        SlimDX.Direct3D9.CreateFlags.HardwareVertexProcessing, presentParams);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //InitError = ex.Message;
        //        return false;
        //    }
        //}

        //private SlimDX.Direct3D9.Texture GetVRLocatorTexture()
        //{
        //    // https://docs.microsoft.com/en-us/windows/win32/direct3d10/d3d10-graphics-programming-guide-resources-creating-textures#filling-textures-manually
        //    // https://docs.microsoft.com/en-us/windows/win32/direct3d11/overviews-direct3d-11-resources-textures-create

        //    if (_d3DDevice == null)
        //        InitializeGraphics();

        //    if (_locatorTexture == null)
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            _vrbitmap.Save(ms, ImageFormat.Bmp);
                    
        //            _locatorTexture = SlimDX.Direct3D9.Texture.FromStream(_d3DDevice, ms);
        //        }
        //    return _locatorTexture;
        //    /*
        //    _texture = new Microsoft.DirectX.Direct3D.Texture2D(D3DDevice, new D3D11.Texture2DDescription()
        //    {
        //        Width = _bitmap.Width,
        //        Height = _bitmap.Height,
        //        ArraySize = 1,
        //        BindFlags = D3D11.BindFlags.ShaderResource,
        //        Usage = D3D11.ResourceUsage.Immutable,
        //        CpuAccessFlags = D3D11.CpuAccessFlags.None,
        //        Format = DXGI.Format.B8G8R8A8_UNorm,
        //        MipLevels = 1,
        //        OptionFlags = D3D11.ResourceOptionFlags.None,
        //        SampleDescription = new DXGI.SampleDescription(1, 0),
        //    }, new SharpDX.DataRectangle(textData.Scan0, textData.Stride));
        //    */
        //}


        //private void UpdateVRLocatorTexture()
        //{
        //    try
        //    {
        //        GetVRLocatorTexture();
        //    }
        //    catch { }

        //    if (_locatorTexture == null)
        //        return;
        //    _locatorOverlay.SetTexture(_panelTexture);

        //    return;
        //}

        public void UpdateVRLocatorImage(Bitmap PanelImage)
        {
            if (_locatorOverlay == null)
                return;
            if (_vrbitmap != null)
                _vrbitmap.Dispose();
            _vrbitmap = PanelImage;
            _vrbitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            //if (_locatorVRAsTexture)
            //{
            //    UpdateVRLocatorTexture();
            //    return;
            //}

            _locatorOverlay.SetImage(_vrbitmap);
        }
    }
}
