
namespace SRVTracker
{
    partial class VRLocatorHUD
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelTarget = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelDistance = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.labelBearing = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.pictureBoxDirection = new System.Windows.Forms.PictureBox();
            this.labelPanelUpdates = new System.Windows.Forms.Label();
            this.groupBoxAdditionalInfo = new System.Windows.Forms.GroupBox();
            this.labelAdditionalInfo = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDirection)).BeginInit();
            this.groupBoxAdditionalInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.labelTarget);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 46);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Target";
            // 
            // labelTarget
            // 
            this.labelTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTarget.Location = new System.Drawing.Point(3, 18);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(194, 25);
            this.labelTarget.TabIndex = 0;
            this.labelTarget.Text = "Not set";
            this.labelTarget.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.labelSpeed);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(209, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(136, 46);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Speed (m/s)";
            // 
            // labelSpeed
            // 
            this.labelSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSpeed.Location = new System.Drawing.Point(3, 18);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(130, 25);
            this.labelSpeed.TabIndex = 0;
            this.labelSpeed.Text = "0";
            this.labelSpeed.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.labelDistance);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(209, 55);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(136, 46);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Distance to target";
            // 
            // labelDistance
            // 
            this.labelDistance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDistance.Location = new System.Drawing.Point(3, 18);
            this.labelDistance.Name = "labelDistance";
            this.labelDistance.Size = new System.Drawing.Size(130, 25);
            this.labelDistance.TabIndex = 0;
            this.labelDistance.Text = "0";
            this.labelDistance.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.labelBearing);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(67, 55);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(136, 46);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Bearing to target";
            // 
            // labelBearing
            // 
            this.labelBearing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelBearing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBearing.Location = new System.Drawing.Point(3, 18);
            this.labelBearing.Name = "labelBearing";
            this.labelBearing.Size = new System.Drawing.Size(130, 25);
            this.labelBearing.TabIndex = 0;
            this.labelBearing.Text = "0";
            this.labelBearing.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.pictureBoxDirection);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.White;
            this.groupBox5.Location = new System.Drawing.Point(351, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(105, 98);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Direction";
            // 
            // pictureBoxDirection
            // 
            this.pictureBoxDirection.Image = global::SRVTracker.Properties.Resources.Silver_arrow;
            this.pictureBoxDirection.InitialImage = null;
            this.pictureBoxDirection.Location = new System.Drawing.Point(22, 25);
            this.pictureBoxDirection.Name = "pictureBoxDirection";
            this.pictureBoxDirection.Size = new System.Drawing.Size(60, 60);
            this.pictureBoxDirection.TabIndex = 5;
            this.pictureBoxDirection.TabStop = false;
            // 
            // labelPanelUpdates
            // 
            this.labelPanelUpdates.AutoSize = true;
            this.labelPanelUpdates.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPanelUpdates.ForeColor = System.Drawing.Color.Green;
            this.labelPanelUpdates.Location = new System.Drawing.Point(5, 69);
            this.labelPanelUpdates.Name = "labelPanelUpdates";
            this.labelPanelUpdates.Size = new System.Drawing.Size(16, 18);
            this.labelPanelUpdates.TabIndex = 5;
            this.labelPanelUpdates.Text = "0";
            this.labelPanelUpdates.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelPanelUpdates.Visible = false;
            // 
            // groupBoxAdditionalInfo
            // 
            this.groupBoxAdditionalInfo.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxAdditionalInfo.Controls.Add(this.labelAdditionalInfo);
            this.groupBoxAdditionalInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBoxAdditionalInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAdditionalInfo.ForeColor = System.Drawing.Color.White;
            this.groupBoxAdditionalInfo.Location = new System.Drawing.Point(3, 107);
            this.groupBoxAdditionalInfo.Name = "groupBoxAdditionalInfo";
            this.groupBoxAdditionalInfo.Size = new System.Drawing.Size(453, 46);
            this.groupBoxAdditionalInfo.TabIndex = 6;
            this.groupBoxAdditionalInfo.TabStop = false;
            // 
            // labelAdditionalInfo
            // 
            this.labelAdditionalInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAdditionalInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAdditionalInfo.Location = new System.Drawing.Point(3, 18);
            this.labelAdditionalInfo.Name = "labelAdditionalInfo";
            this.labelAdditionalInfo.Size = new System.Drawing.Size(447, 25);
            this.labelAdditionalInfo.TabIndex = 0;
            this.labelAdditionalInfo.Text = "0";
            this.labelAdditionalInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // VRLocatorHUD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.groupBoxAdditionalInfo);
            this.Controls.Add(this.labelPanelUpdates);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "VRLocatorHUD";
            this.Size = new System.Drawing.Size(800, 320);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDirection)).EndInit();
            this.groupBoxAdditionalInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.Label labelTarget;
        public System.Windows.Forms.Label labelSpeed;
        public System.Windows.Forms.Label labelDistance;
        public System.Windows.Forms.Label labelBearing;
        public System.Windows.Forms.PictureBox pictureBoxDirection;
        public System.Windows.Forms.Label labelPanelUpdates;
        public System.Windows.Forms.Label labelAdditionalInfo;
        public System.Windows.Forms.GroupBox groupBoxAdditionalInfo;
    }
}
