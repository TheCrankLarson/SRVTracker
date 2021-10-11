using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using Valve.VR;
using OpenVRApiModule.Maths;
using System.Runtime.InteropServices;

namespace SRVTracker
{
    public class VRLocatorOverlay
    {

        private float _widthInMetres = 1f;
        private HmdMatrix34_t _transform;
        private ulong _vrOverlayHandle = 0;
        private byte[] _vrImageBytes = null;
        private IntPtr _intPtrVROverlayImage = IntPtr.Zero;

        public VRLocatorOverlay(VRLocator vrLocator)
        {
            _transform = Matrix4x4.CreateTranslation(0, 1, 0.5f).ToHmdMatrix34_t();
            if (vrLocator.VRInitializedOk)
                CreateOverlay();
        }

        public HmdMatrix34_t Transform
        {
            get { return _transform; }
            set
            {
                _transform = value;
                ApplyOverlayTransform();
            }
        }

        public float WidthInMeters
        {
            get { return _widthInMetres; }
            set
            {
                _widthInMetres = value;
                ApplyOverlayWidth();
            }
        }

        public float WidthInMetres
        {
            get { return _widthInMetres; }
            set
            {
                _widthInMetres = value;
                ApplyOverlayWidth();
            }
        }

        private void ApplyOverlayWidth()
        {
            OpenVR.Overlay?.SetOverlayWidthInMeters(_vrOverlayHandle, _widthInMetres);
        }

        private void ApplyOverlayTransform()
        {
            OpenVR.Overlay?.SetOverlayTransformAbsolute(_vrOverlayHandle, Valve.VR.ETrackingUniverseOrigin.TrackingUniverseStanding, ref _transform);
        }

        public void Show()
        {
            // Show the overlay    
            _ = OpenVR.Overlay.ShowOverlay(_vrOverlayHandle);
        }

        public void Hide()
        {
            // Hide the overlay
            try
            {
                OpenVR.Overlay?.HideOverlay(_vrOverlayHandle);
            }
            catch { }
            if (_intPtrVROverlayImage != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_intPtrVROverlayImage);
                _intPtrVROverlayImage = IntPtr.Zero;
            }
        }

        public void Destroy()
        {
            // Destroy the overlay
            try
            {
                OpenVR.Overlay.DestroyOverlay(_vrOverlayHandle);
            }
            catch { }
        }

        private bool CreateOverlay()
        {
            try
            {
                if (OpenVR.Overlay != null)
                {
                    OpenVR.Overlay.CreateOverlay(Guid.NewGuid().ToString(), "SRV Tracking", ref _vrOverlayHandle);
                    //InitOverlay();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to create VR overlay: {Environment.NewLine}{ex}", "VR Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }

        private static float DegreesToRadians(float degrees)
        {
            return (float)(degrees * (Math.PI / 180f));
        }

        public void SetTexture(Texture_t texture)
        {
            // Set the texture of the overlay
        }

        public static byte[] BitmapToByte(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                return ms.ToArray();
            }
        }

        public void SetImage(Bitmap imageBitmap)
        {
            int memoryAllocated = 0;
            if (_vrImageBytes != null)
                memoryAllocated = _vrImageBytes.Length;

            _vrImageBytes = BitmapToByte(imageBitmap);
            if (memoryAllocated < _vrImageBytes.Length)
            {
                if (memoryAllocated>0)
                    Marshal.FreeHGlobal(_intPtrVROverlayImage);
                _intPtrVROverlayImage = Marshal.AllocHGlobal(_vrImageBytes.Length);
            }
            Marshal.Copy(_vrImageBytes, 0, _intPtrVROverlayImage, _vrImageBytes.Length);
            OpenVR.Overlay.SetOverlayRaw(_vrOverlayHandle, _intPtrVROverlayImage, (uint)imageBitmap.Width, (uint)imageBitmap.Height, 4);
        }
    }
}
