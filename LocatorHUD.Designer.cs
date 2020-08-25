namespace SRVTracker
{
    partial class LocatorHUD
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelTarget = new System.Windows.Forms.Label();
            this.labelBearing = new System.Windows.Forms.Label();
            this.labelDistance = new System.Windows.Forms.Label();
            this.pictureBoxDirection = new System.Windows.Forms.PictureBox();
            this.labelSpeedInMS = new System.Windows.Forms.Label();
            this.labelMs = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDirection)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Target:";
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.ForeColor = System.Drawing.Color.Yellow;
            this.labelTarget.Location = new System.Drawing.Point(40, 0);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(64, 13);
            this.labelTarget.TabIndex = 1;
            this.labelTarget.Text = "Rand Vision";
            // 
            // labelBearing
            // 
            this.labelBearing.AutoSize = true;
            this.labelBearing.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBearing.ForeColor = System.Drawing.Color.Yellow;
            this.labelBearing.Location = new System.Drawing.Point(25, 20);
            this.labelBearing.Name = "labelBearing";
            this.labelBearing.Size = new System.Drawing.Size(36, 29);
            this.labelBearing.TabIndex = 2;
            this.labelBearing.Text = "0°";
            // 
            // labelDistance
            // 
            this.labelDistance.AutoSize = true;
            this.labelDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDistance.ForeColor = System.Drawing.Color.Yellow;
            this.labelDistance.Location = new System.Drawing.Point(92, 20);
            this.labelDistance.Name = "labelDistance";
            this.labelDistance.Size = new System.Drawing.Size(82, 29);
            this.labelDistance.TabIndex = 3;
            this.labelDistance.Text = "0.0km";
            // 
            // pictureBoxDirection
            // 
            this.pictureBoxDirection.Image = global::SRVTracker.Properties.Resources.arrow;
            this.pictureBoxDirection.InitialImage = null;
            this.pictureBoxDirection.Location = new System.Drawing.Point(198, 0);
            this.pictureBoxDirection.Name = "pictureBoxDirection";
            this.pictureBoxDirection.Size = new System.Drawing.Size(60, 60);
            this.pictureBoxDirection.TabIndex = 4;
            this.pictureBoxDirection.TabStop = false;
            // 
            // labelSpeedInMS
            // 
            this.labelSpeedInMS.AutoSize = true;
            this.labelSpeedInMS.ForeColor = System.Drawing.Color.Yellow;
            this.labelSpeedInMS.Location = new System.Drawing.Point(144, 0);
            this.labelSpeedInMS.Name = "labelSpeedInMS";
            this.labelSpeedInMS.Size = new System.Drawing.Size(34, 13);
            this.labelSpeedInMS.TabIndex = 5;
            this.labelSpeedInMS.Text = "999.9";
            this.labelSpeedInMS.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelMs
            // 
            this.labelMs.AutoSize = true;
            this.labelMs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.labelMs.Location = new System.Drawing.Point(175, 0);
            this.labelMs.Name = "labelMs";
            this.labelMs.Size = new System.Drawing.Size(25, 13);
            this.labelMs.TabIndex = 6;
            this.labelMs.Text = "m/s";
            // 
            // LocatorHUD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.pictureBoxDirection);
            this.Controls.Add(this.labelSpeedInMS);
            this.Controls.Add(this.labelMs);
            this.Controls.Add(this.labelDistance);
            this.Controls.Add(this.labelBearing);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.label1);
            this.Name = "LocatorHUD";
            this.Size = new System.Drawing.Size(260, 60);
            this.Load += new System.EventHandler(this.LocatorHUD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDirection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.Label labelBearing;
        private System.Windows.Forms.Label labelDistance;
        private System.Windows.Forms.PictureBox pictureBoxDirection;
        private System.Windows.Forms.Label labelSpeedInMS;
        private System.Windows.Forms.Label labelMs;
    }
}
