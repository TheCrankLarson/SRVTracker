using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SRVTracker
{
    public partial class VRLocatorHUD : UserControl
    {
        public VRLocatorHUD()
        {
            InitializeComponent();
        }

        //public Texture2D Bitmap2Texture(Device device, BitmapSource source)
        //{
        //    var bitmap = new WriteableBitmap(source);
        //    bitmap.Lock();
        //    var texture = new Texture2D(device, new Texture2DDescription()
        //    {
        //        Width = source.PixelWidth,
        //        Height = source.PixelHeight,
        //        ArraySize = 1,
        //        MipLevels = 1,
        //        BindFlags = BindFlags.ShaderResource,
        //        Usage = ResourceUsage.Default,
        //        CpuAccessFlags = CpuAccessFlags.None,
        //        Format = Format.R8G8B8A8_UNorm,
        //        OptionFlags = ResourceOptionFlags.None,
        //        SampleDescription = new SampleDescription(1, 0),
        //    }, new DataRectangle(bitmap.BackBuffer, bitmap.BackBufferStride));
        //    bitmap.Unlock();
        //    return texture;
        //}


    }
}
