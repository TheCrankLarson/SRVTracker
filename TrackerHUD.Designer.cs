namespace SRVTracker
{
    partial class TrackerHUD
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelTrackerLabels = new System.Windows.Forms.Label();
            this.labelHeading = new System.Windows.Forms.Label();
            this.labelAltitude = new System.Windows.Forms.Label();
            this.labelLatitude = new System.Windows.Forms.Label();
            this.labelLongitude = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelTrackerLabels
            // 
            this.labelTrackerLabels.AutoSize = true;
            this.labelTrackerLabels.BackColor = System.Drawing.Color.Black;
            this.labelTrackerLabels.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.labelTrackerLabels.Location = new System.Drawing.Point(9, 6);
            this.labelTrackerLabels.Name = "labelTrackerLabels";
            this.labelTrackerLabels.Size = new System.Drawing.Size(242, 13);
            this.labelTrackerLabels.TabIndex = 30;
            this.labelTrackerLabels.Text = "Longitude        Latitude         Altitude        Heading";
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Black;
            this.labelHeading.ForeColor = System.Drawing.Color.Yellow;
            this.labelHeading.Location = new System.Drawing.Point(203, 21);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(25, 13);
            this.labelHeading.TabIndex = 29;
            this.labelHeading.Text = "187";
            // 
            // labelAltitude
            // 
            this.labelAltitude.AutoSize = true;
            this.labelAltitude.BackColor = System.Drawing.Color.Black;
            this.labelAltitude.ForeColor = System.Drawing.Color.Yellow;
            this.labelAltitude.Location = new System.Drawing.Point(146, 21);
            this.labelAltitude.Name = "labelAltitude";
            this.labelAltitude.Size = new System.Drawing.Size(13, 13);
            this.labelAltitude.TabIndex = 28;
            this.labelAltitude.Text = "0";
            // 
            // labelLatitude
            // 
            this.labelLatitude.AutoSize = true;
            this.labelLatitude.BackColor = System.Drawing.Color.Black;
            this.labelLatitude.ForeColor = System.Drawing.Color.Yellow;
            this.labelLatitude.Location = new System.Drawing.Point(80, 21);
            this.labelLatitude.Name = "labelLatitude";
            this.labelLatitude.Size = new System.Drawing.Size(58, 13);
            this.labelLatitude.TabIndex = 27;
            this.labelLatitude.Text = "17.323208";
            // 
            // labelLongitude
            // 
            this.labelLongitude.AutoSize = true;
            this.labelLongitude.BackColor = System.Drawing.Color.Black;
            this.labelLongitude.ForeColor = System.Drawing.Color.Yellow;
            this.labelLongitude.Location = new System.Drawing.Point(9, 21);
            this.labelLongitude.Name = "labelLongitude";
            this.labelLongitude.Size = new System.Drawing.Size(58, 13);
            this.labelLongitude.TabIndex = 26;
            this.labelLongitude.Text = "17.323208";
            // 
            // TrackerHUD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.labelTrackerLabels);
            this.Controls.Add(this.labelHeading);
            this.Controls.Add(this.labelAltitude);
            this.Controls.Add(this.labelLatitude);
            this.Controls.Add(this.labelLongitude);
            this.Name = "TrackerHUD";
            this.Size = new System.Drawing.Size(260, 40);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTrackerLabels;
        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label labelAltitude;
        private System.Windows.Forms.Label labelLatitude;
        private System.Windows.Forms.Label labelLongitude;
    }
}
