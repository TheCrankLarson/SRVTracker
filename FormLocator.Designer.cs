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
            this.components = new System.ComponentModel.Container();
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
            this.buttonPlayers = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxCommanders = new System.Windows.Forms.ListBox();
            this.buttonTrackCommander = new System.Windows.Forms.Button();
            this.checkBoxEnableVRLocator = new System.Windows.Forms.CheckBox();
            this.comboBoxLocation = new System.Windows.Forms.ComboBox();
            this.buttonUseCurrentLocation = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
            this.groupBoxDestination.Text = "Target location";
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
            this.textBoxAltitude.ReadOnly = true;
            this.textBoxAltitude.Size = new System.Drawing.Size(102, 20);
            this.textBoxAltitude.TabIndex = 3;
            this.toolTip1.SetToolTip(this.textBoxAltitude, "The target\'s altitude");
            // 
            // textBoxLatitude
            // 
            this.textBoxLatitude.Location = new System.Drawing.Point(115, 32);
            this.textBoxLatitude.Name = "textBoxLatitude";
            this.textBoxLatitude.ReadOnly = true;
            this.textBoxLatitude.Size = new System.Drawing.Size(100, 20);
            this.textBoxLatitude.TabIndex = 2;
            this.toolTip1.SetToolTip(this.textBoxLatitude, "The target\'s latitude");
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
            this.textBoxLongitude.ReadOnly = true;
            this.textBoxLongitude.Size = new System.Drawing.Size(100, 20);
            this.textBoxLongitude.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textBoxLongitude, "The target\'s longitude");
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
            this.toolTip1.SetToolTip(this.buttonShowHideTarget, "Shrink/expand the window");
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
            this.toolTip1.SetToolTip(this.labelDistance, "Distance to the target");
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
            this.toolTip1.SetToolTip(this.labelHeading, "The direction you must travel to reach the target");
            // 
            // buttonPlayers
            // 
            this.buttonPlayers.Location = new System.Drawing.Point(268, 157);
            this.buttonPlayers.Name = "buttonPlayers";
            this.buttonPlayers.Size = new System.Drawing.Size(75, 23);
            this.buttonPlayers.TabIndex = 8;
            this.buttonPlayers.Text = "Commander";
            this.toolTip1.SetToolTip(this.buttonPlayers, "Track other commander (other commander must be uploading status)");
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
            this.toolTip1.SetToolTip(this.buttonTrackCommander, "Start tracking the selected commander");
            this.buttonTrackCommander.UseVisualStyleBackColor = true;
            this.buttonTrackCommander.Click += new System.EventHandler(this.buttonTrackCommander_Click);
            // 
            // checkBoxEnableVRLocator
            // 
            this.checkBoxEnableVRLocator.AutoSize = true;
            this.checkBoxEnableVRLocator.Location = new System.Drawing.Point(53, 161);
            this.checkBoxEnableVRLocator.Name = "checkBoxEnableVRLocator";
            this.checkBoxEnableVRLocator.Size = new System.Drawing.Size(41, 17);
            this.checkBoxEnableVRLocator.TabIndex = 10;
            this.checkBoxEnableVRLocator.Text = "VR";
            this.toolTip1.SetToolTip(this.checkBoxEnableVRLocator, "Enable/disable the VR bearing/distance display");
            this.checkBoxEnableVRLocator.UseVisualStyleBackColor = true;
            this.checkBoxEnableVRLocator.CheckedChanged += new System.EventHandler(this.checkBoxEnableVRLocator_CheckedChanged);
            // 
            // comboBoxLocation
            // 
            this.comboBoxLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLocation.FormattingEnabled = true;
            this.comboBoxLocation.Location = new System.Drawing.Point(100, 159);
            this.comboBoxLocation.Name = "comboBoxLocation";
            this.comboBoxLocation.Size = new System.Drawing.Size(162, 21);
            this.comboBoxLocation.TabIndex = 11;
            this.toolTip1.SetToolTip(this.comboBoxLocation, "Select a location to target (add locations in the Route Planner)");
            this.comboBoxLocation.SelectedIndexChanged += new System.EventHandler(this.comboBoxLocation_SelectedIndexChanged);
            // 
            // buttonUseCurrentLocation
            // 
            this.buttonUseCurrentLocation.Image = global::SRVTracker.Properties.Resources.Home_16x;
            this.buttonUseCurrentLocation.Location = new System.Drawing.Point(12, 157);
            this.buttonUseCurrentLocation.Name = "buttonUseCurrentLocation";
            this.buttonUseCurrentLocation.Size = new System.Drawing.Size(35, 23);
            this.buttonUseCurrentLocation.TabIndex = 6;
            this.toolTip1.SetToolTip(this.buttonUseCurrentLocation, "Set as home (track current location)");
            this.buttonUseCurrentLocation.UseVisualStyleBackColor = true;
            this.buttonUseCurrentLocation.Click += new System.EventHandler(this.buttonUseCurrentLocation_Click);
            // 
            // FormLocator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 189);
            this.Controls.Add(this.comboBoxLocation);
            this.Controls.Add(this.checkBoxEnableVRLocator);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonPlayers);
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
            this.PerformLayout();

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
        private System.Windows.Forms.Button buttonPlayers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxCommanders;
        private System.Windows.Forms.Button buttonTrackCommander;
        private System.Windows.Forms.CheckBox checkBoxEnableVRLocator;
        private System.Windows.Forms.ComboBox comboBoxLocation;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}