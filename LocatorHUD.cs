using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;


namespace SRVTracker
{
    public partial class LocatorHUD : UserControl
    {
        private static Bitmap[] _arrowAtAngle = new Bitmap[359];
        int _lastTurnDirection = -1;
        string _lastSpeed = "";

        private Bitmap _panelBitmap = null;
        private Graphics _panelBitmapGraphics = null;
        private bool _panelBitmapStale = true;
        private int _panelRequests = 0;

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

            if (!_panelBitmapStale)
                _panelBitmapStale = updated;
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

        public String AdditionalInfo
        {
            get { return labelAdditionalInfo.Text; }
        }

        public Bitmap GetBearingImage()
        {
            return (Bitmap)pictureBoxDirection.Image;
        }

        public string SetDistance(double distance)
        {
            string distanceText;
            if (distance > 1000000)
            {
                distanceText = $"{(distance / 1000000).ToString("F1")}Mm";
            }
            else if (distance > 1000)
            {
                distanceText = $"{(distance / 1000).ToString("F1")}km";
            }
            else
                distanceText = $"{distance.ToString("F1")}m";
            if (labelDistance.Text.Equals(distanceText))
                return distanceText;

            _panelBitmapStale = true;
            Action action = new Action(() => { labelDistance.Text = distanceText; });
            if (labelDistance.InvokeRequired)
                labelDistance.Invoke(action);
            else
                action();
            return distanceText;
        }

        public void SetTarget(string target, string additionalInfo = "")
        {
            if (labelTarget.Text.Equals(target))
                return;

            _panelBitmapStale = true;
            Action action = new Action(() => { labelTarget.Text = target; });
            if (labelTarget.InvokeRequired)
                labelTarget.Invoke(action);
            else
                action();
            if (additionalInfo != null)
                SetAdditionalInfo(additionalInfo);
        }

        public void SetAdditionalInfo(string additionalInfo)
        {
            if (labelAdditionalInfo.Text.Equals(additionalInfo))
                return;

            _panelBitmapStale = true;
            Action action = new Action(() => { labelAdditionalInfo.Text = additionalInfo; });
            if (labelAdditionalInfo.InvokeRequired)
                labelAdditionalInfo.Invoke(action);
            else
                action();
        }

        private void UpdateSpeed(string speed)
        {
            if (speed.Equals(_lastSpeed))
                return;

            _panelBitmapStale = true;
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

        public void SetSpeed(int speedInMs)
        {
            UpdateSpeed(speedInMs.ToString());
        }

        public void SetSpeed(double speedInMs)
        {
            UpdateSpeed(speedInMs.ToString("F1"));

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

        public bool PanelRequiresReset()
        {
            // For some reason, OpenVR needs resetting once 200 updates have been written to the overlay... No idea why
            return _panelRequests >= 200;
        }

        public void ResetPanel()
        {
            _panelRequests = 0;
        }

        public Bitmap GetLocatorPanelBitmap()
        {
            // Generate and return a bitmap to be used as overlay
            _panelRequests++;
            if (!_panelBitmapStale)
                return _panelBitmap;

            if (_panelBitmap == null)
                _panelBitmap = new Bitmap(800, 320);

            if (_panelBitmapGraphics == null)
            {
                _panelBitmapGraphics = Graphics.FromImage(_panelBitmap);
                _panelBitmapGraphics.CompositingQuality = CompositingQuality.HighQuality;
                _panelBitmapGraphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                _panelBitmapGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                _panelBitmapGraphics.SmoothingMode = SmoothingMode.HighQuality;
                _panelBitmapGraphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            }

            // Black background
            _panelBitmapGraphics.FillRectangle(new SolidBrush(Color.Black), _panelBitmapGraphics.ClipBounds);

            // Tracking target
            Brush directionBrush = new SolidBrush(Color.White);
            Brush targetBrush = new SolidBrush(Color.Green);
            Brush additionalInfoBrush = new SolidBrush(Color.LightGray);
            Font font = new Font("Arial", 24);
            _panelBitmapGraphics.DrawString(labelTarget.Text, font, targetBrush, new PointF(0, 40));

            // Additional info
            if (!String.IsNullOrEmpty(labelAdditionalInfo.Text))
                _panelBitmapGraphics.DrawString(labelAdditionalInfo.Text, font, additionalInfoBrush, new PointF(0, 80));

            // Bearing
            font.Dispose();
            font = new Font("Arial", 56);
            //SizeF textSize = graphics.MeasureString(labelHeading.Text, font);
            _panelBitmapGraphics.DrawString(labelBearing.Text, font, directionBrush, new PointF(80, 120));

            // Distance
            _panelBitmapGraphics.DrawString(labelDistance.Text, font, directionBrush, new PointF(320, 120));
            font.Dispose();

            // Bearing arrow
            _panelBitmapGraphics.DrawImage(GetBearingImage(), new Point(0, 130));

            // Count of generations (for debugging)
            //font = new Font("Arial", 12);
            //_panelBitmapGraphics.DrawString(_panelRequests.ToString(), font, additionalInfoBrush, new PointF(0, 0));
            //font.Dispose();

            directionBrush.Dispose();
            targetBrush.Dispose();
            additionalInfoBrush.Dispose();
            return _panelBitmap;
        }
    }
}
