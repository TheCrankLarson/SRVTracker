namespace SRVTracker
{
    partial class FormLocator
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxDestination = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAltitude = new System.Windows.Forms.TextBox();
            this.textBoxLatitude = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLongitude = new System.Windows.Forms.TextBox();
            this.groupBoxBearing = new System.Windows.Forms.GroupBox();
            this.buttonShowHideTarget = new System.Windows.Forms.Button();
            this.labelDistance = new System.Windows.Forms.Label();
            this.labelHeading = new System.Windows.Forms.Label();
            this.buttonUseCurrentLocation = new System.Windows.Forms.Button();
            this.buttonLocations = new System.Windows.Forms.Button();
            this.buttonPlayers = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxCommanders = new System.Windows.Forms.ListBox();
            this.buttonTrackCommander = new System.Windows.Forms.Button();
            this.groupBoxDestination.SuspendLayout();
            this.groupBoxBearing.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxDestination
            // 
            this.groupBoxDestination.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxDestination.Controls.Add(this.label3);
            this.groupBoxDestination.Controls.Add(this.label2);
            this.groupBoxDestination.Controls.Add(this.textBoxAltitude);
            this.groupBoxDestination.Controls.Add(this.textBoxLatitude);
            this.groupBoxDestination.Controls.Add(this.label1);
            this.groupBoxDestination.Controls.Add(this.textBoxLongitude);
            this.groupBoxDestination.Location = new System.Drawing.Point(12, 87);
            this.groupBoxDestination.Name = "groupBoxDestination";
            this.groupBoxDestination.Size = new System.Drawing.Size(331, 64);
            this.groupBoxDestination.TabIndex = 0;
            this.groupBoxDestination.TabStop = false;
            this.groupBoxDestination.Text = "Target";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Altitude";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Latitude";
            // 
            // textBoxAltitude
            // 
            this.textBoxAltitude.Location = new System.Drawing.Point(223, 32);
            this.textBoxAltitude.Name = "textBoxAltitude";
            this.textBoxAltitude.Size = new System.Drawing.Size(102, 20);
            this.textBoxAltitude.TabIndex = 3;
            this.textBoxAltitude.TextChanged += new System.EventHandler(this.textBoxAltitude_TextChanged);
            // 
            // textBoxLatitude
            // 
            this.textBoxLatitude.Location = new System.Drawing.Point(115, 32);
            this.textBoxLatitude.Name = "textBoxLatitude";
            this.textBoxLatitude.Size = new System.Drawing.Size(100, 20);
            this.textBoxLatitude.TabIndex = 2;
            this.textBoxLatitude.TextChanged += new System.EventHandler(this.textBoxLatitude_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Longitude";
            // 
            // textBoxLongitude
            // 
            this.textBoxLongitude.Location = new System.Drawing.Point(9, 32);
            this.textBoxLongitude.Name = "textBoxLongitude";
            this.textBoxLongitude.Size = new System.Drawing.Size(100, 20);
            this.textBoxLongitude.TabIndex = 0;
            this.textBoxLongitude.TextChanged += new System.EventHandler(this.textBoxLongitude_TextChanged);
            // 
            // groupBoxBearing
            // 
            this.groupBoxBearing.Controls.Add(this.buttonShowHideTarget);
            this.groupBoxBearing.Controls.Add(this.labelDistance);
            this.groupBoxBearing.Controls.Add(this.labelHeading);
            this.groupBoxBearing.Location = new System.Drawing.Point(12, 12);
            this.groupBoxBearing.Name = "groupBoxBearing";
            this.groupBoxBearing.Size = new System.Drawing.Size(331, 65);
            this.groupBoxBearing.TabIndex = 1;
            this.groupBoxBearing.TabStop = false;
            this.groupBoxBearing.Text = "Bearing (target not set)";
            // 
            // buttonShowHideTarget
            // 
            this.buttonShowHideTarget.Location = new System.Drawing.Point(312, 46);
            this.buttonShowHideTarget.Name = "buttonShowHideTarget";
            this.buttonShowHideTarget.Size = new System.Drawing.Size(13, 13);
            this.buttonShowHideTarget.TabIndex = 2;
            this.buttonShowHideTarget.UseVisualStyleBackColor = true;
            this.buttonShowHideTarget.Click += new System.EventHandler(this.buttonShowHideTarget_Click);
            // 
            // labelDistance
            // 
            this.labelDistance.AutoSize = true;
            this.labelDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDistance.Location = new System.Drawing.Point(152, 16);
            this.labelDistance.Name = "labelDistance";
            this.labelDistance.Size = new System.Drawing.Size(110, 37);
            this.labelDistance.TabIndex = 1;
            this.labelDistance.Text = "0.0km";
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.Location = new System.Drawing.Point(54, 16);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(48, 37);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "0°";
            // 
            // buttonUseCurrentLocation
            // 
            this.buttonUseCurrentLocation.Location = new System.Drawing.Point(12, 157);
            this.buttonUseCurrentLocation.Name = "buttonUseCurrentLocation";
            this.buttonUseCurrentLocation.Size = new System.Drawing.Size(117, 23);
            this.buttonUseCurrentLocation.TabIndex = 6;
            this.buttonUseCurrentLocation.Text = "Use current location";
            this.buttonUseCurrentLocation.UseVisualStyleBackColor = true;
            this.buttonUseCurrentLocation.Click += new System.EventHandler(this.buttonUseCurrentLocation_Click);
            // 
            // buttonLocations
            // 
            this.buttonLocations.Enabled = false;
            this.buttonLocations.Location = new System.Drawing.Point(187, 157);
            this.buttonLocations.Name = "buttonLocations";
            this.buttonLocations.Size = new System.Drawing.Size(75, 23);
            this.buttonLocations.TabIndex = 7;
            this.buttonLocations.Text = "Location...";
            this.buttonLocations.UseVisualStyleBackColor = true;
            // 
            // buttonPlayers
            // 
            this.buttonPlayers.Location = new System.Drawing.Point(268, 157);
            this.buttonPlayers.Name = "buttonPlayers";
            this.buttonPlayers.Size = new System.Drawing.Size(75, 23);
            this.buttonPlayers.TabIndex = 8;
            this.buttonPlayers.Text = "Commander";
            this.buttonPlayers.UseVisualStyleBackColor = true;
            this.buttonPlayers.Click += new System.EventHandler(this.buttonPlayers_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxCommanders);
            this.groupBox1.Controls.Add(this.buttonTrackCommander);
            this.groupBox1.Location = new System.Drawing.Point(349, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 168);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Other Commanders";
            // 
            // listBoxCommanders
            // 
            this.listBoxCommanders.FormattingEnabled = true;
            this.listBoxCommanders.Location = new System.Drawing.Point(6, 19);
            this.listBoxCommanders.Name = "listBoxCommanders";
            this.listBoxCommanders.Size = new System.Drawing.Size(188, 108);
            this.listBoxCommanders.TabIndex = 1;
            this.listBoxCommanders.SelectedIndexChanged += new System.EventHandler(this.listBoxCommanders_SelectedIndexChanged);
            // 
            // buttonTrackCommander
            // 
            this.buttonTrackCommander.Enabled = false;
            this.buttonTrackCommander.Location = new System.Drawing.Point(119, 139);
            this.buttonTrackCommander.Name = "buttonTrackCommander";
            this.buttonTrackCommander.Size = new System.Drawing.Size(75, 23);
            this.buttonTrackCommander.TabIndex = 0;
            this.buttonTrackCommander.Text = "Track";
            this.buttonTrackCommander.UseVisualStyleBackColor = true;
            this.buttonTrackCommander.Click += new System.EventHandler(this.buttonTrackCommander_Click);
            // 
            // FormLocator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 189);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonPlayers);
            this.Controls.Add(this.buttonLocations);
            this.Controls.Add(this.groupBoxBearing);
            this.Controls.Add(this.buttonUseCurrentLocation);
            this.Controls.Add(this.groupBoxDestination);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormLocator";
            this.Text = "Waypoint Locator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLocator_FormClosing);
            this.groupBoxDestination.ResumeLayout(false);
            this.groupBoxDestination.PerformLayout();
            this.groupBoxBearing.ResumeLayout(false);
            this.groupBoxBearing.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxDestination;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAltitude;
        private System.Windows.Forms.TextBox textBoxLatitude;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLongitude;
        private System.Windows.Forms.GroupBox groupBoxBearing;
        private System.Windows.Forms.Label labelDistance;
        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Button buttonShowHideTarget;
        private System.Windows.Forms.Button buttonUseCurrentLocation;
        private System.Windows.Forms.Button buttonLocations;
        private System.Windows.Forms.Button buttonPlayers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxCommanders;
        private System.Windows.Forms.Button buttonTrackCommander;
    }
}