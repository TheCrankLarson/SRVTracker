using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;

namespace SRVTracker
{
    public partial class LocatorHUD : UserControl
    {
        private static Bitmap[] _arrowAtAngle = new Bitmap[359];
        public LocatorHUD()
        {
            InitializeComponent();
            _arrowAtAngle[0] = (Bitmap)pictureBoxDirection.Image.Clone();
        }

        private void LocatorHUD_Load(object sender, EventArgs e)
        {

        }

        public void SetBearing(int bearingInDegrees)
        {
            Action action = new Action(() => { labelBearing.Text = $"{bearingInDegrees}°"; });
            if (labelBearing.InvokeRequired)
                labelBearing.Invoke(action);
            else
                action();

            if (_arrowAtAngle[bearingInDegrees]==null)
            {
                // We haven't rotated the arrow to this angle yet, so create a copy and do that
                _arrowAtAngle[bearingInDegrees] = RotateImage((Bitmap)_arrowAtAngle[0].Clone(),(float)bearingInDegrees);

            }
            action = new Action(() => { pictureBoxDirection.Image = _arrowAtAngle[bearingInDegrees]; });
            if (pictureBoxDirection.InvokeRequired)
                pictureBoxDirection.Invoke(action);
            else
                action();          
        }

        public void SetDistance(double distance)
        {
            Action action = new Action(() => { labelDistance.Text = $"{(distance / 1000):F1}km"; });
            if (labelDistance.InvokeRequired)
                labelDistance.Invoke(action);
            else
                action();
            
        }

        public void SetTarget(string target)
        {
            Action action = new Action(() => { labelTarget.Text = target; });
            if (labelTarget.InvokeRequired)
                labelTarget.Invoke(action);
            else
                action();
        }

        /// <summary>
        /// method to rotate an image either clockwise or counter-clockwise
        /// </summary>
        /// <param name="img">the image to be rotated</param>
        /// <param name="rotationAngle">the angle (in degrees).
        /// NOTE: 
        /// Positive values will rotate clockwise
        /// negative values will rotate counter-clockwise
        /// </param>
        /// <returns></returns>
        public static Bitmap RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }
    }
}
