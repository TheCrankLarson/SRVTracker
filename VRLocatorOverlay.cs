using OVRSharp;
using OVRSharp.Math;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SRVTracker
{
    public class VRLocatorOverlay: Overlay
    {
        public VRLocatorOverlay() : base(Guid.NewGuid().ToString(), "SRVTracker Locator")
        {
            WidthInMeters = 0.8f;
            //Transform = Matrix4x4.CreateTranslation(0, 1, 0.5f).ToHmdMatrix34_t();

            //var overlayImagePath = Utils.PathToResource("arrow.png");
            //SetTextureFromFile(overlayImagePath);
        }

        private static float DegreesToRadians(float degrees)
        {
            return (float)(degrees * (Math.PI / 180f));
        }
    }
}
