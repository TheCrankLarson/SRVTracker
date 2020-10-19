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
        int _lastTurnDirection = -1;
        string _lastSpeed = "";

        public LocatorHUD()
        {
            InitializeComponent();           
            labelSpeedInMS.Visible = false;
            labelMs.Visible = false;
        }

        public bool SetBearing(int bearingToTarget, int currentHeading)
        {
            if (currentHeading < 0)
                return false;

            if (_arrowAtAngle[0] == null)
                _arrowAtAngle[0] = (Bitmap)pictureBoxDirection.Image.Clone();

            Action action;
            bool updated = false;

            int directionToTurn = bearingToTarget - currentHeading;
            if (directionToTurn < 0)
                directionToTurn += 360;

            string bearingText = $"{bearingToTarget}°";
            if (!labelBearing.Text.Equals(bearingText))
            {
                action = new Action(() => { labelBearing.Text = bearingText; });
                if (labelBearing.InvokeRequired)
                    labelBearing.Invoke(action);
                else
                    action();
                updated = true;
            }

            if (directionToTurn == _lastTurnDirection)
                return updated;

            _lastTurnDirection = directionToTurn;
            if (_arrowAtAngle[directionToTurn] == null)
            {
                // We haven't rotated the arrow to this angle yet, so create a copy and do that
                _arrowAtAngle[directionToTurn] = RotateImage((Bitmap)_arrowAtAngle[0].Clone(),(float)directionToTurn);

            }
            action = new Action(() => { pictureBoxDirection.Image = _arrowAtAngle[directionToTurn]; });
            if (pictureBoxDirection.InvokeRequired)
                pictureBoxDirection.Invoke(action);
            else
                action();
            return true;
        }

        public Bitmap GetBearingImage()
        {
            return (Bitmap)pictureBoxDirection.Image;
        }

        public void SetDistance(double distance)
        {
            string distanceText = $"{(distance / 1000):F1}km";
            if (labelDistance.Text.Equals(distanceText))
                return;

            Action action = new Action(() => { labelDistance.Text = distanceText; });
            if (labelDistance.InvokeRequired)
                labelDistance.Invoke(action);
            else
                action();
            
        }

        public void SetTarget(string target)
        {
            if (labelTarget.Text.Equals(target))
                return;

            Action action = new Action(() => { labelTarget.Text = target; });
            if (labelTarget.InvokeRequired)
                labelTarget.Invoke(action);
            else
                action();
        }

        public void SetSpeed(double speedInMs)
        {
            string speed = speedInMs.ToString("F1");
            if (speed.Equals(_lastSpeed))
                return;

            Action action = new Action(() => { labelSpeedInMS.Text = speed; });
            if (labelSpeedInMS.InvokeRequired)
                labelSpeedInMS.Invoke(action);
            else
                action();

            if (labelSpeedInMS.Visible)
                return;

            action = new Action(() =>
            {
                labelSpeedInMS.Visible = true;
                labelMs.Visible = true;
            });
            if (labelSpeedInMS.InvokeRequired)
                labelSpeedInMS.Invoke(action);
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
