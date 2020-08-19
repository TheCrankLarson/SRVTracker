﻿namespace SRVTracker
{
    partial class FormRouter
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxWaypoints = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownRecordDistance = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonBelowAltitude = new System.Windows.Forms.RadioButton();
            this.radioButtonAboveAltitude = new System.Windows.Forms.RadioButton();
            this.numericUpDownAltitude = new System.Windows.Forms.NumericUpDown();
            this.checkBoxTestAltitude = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxWaypointName = new System.Windows.Forms.TextBox();
            this.numericUpDownRadius = new System.Windows.Forms.NumericUpDown();
            this.checkBoxRadius = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxRouteName = new System.Windows.Forms.TextBox();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonAddCurrentLocation = new System.Windows.Forms.Button();
            this.buttonSaveRoute = new System.Windows.Forms.Button();
            this.buttonSetAsTarget = new System.Windows.Forms.Button();
            this.buttonSaveRouteAs = new System.Windows.Forms.Button();
            this.buttonLoadRoute = new System.Windows.Forms.Button();
            this.buttonDeleteWaypoint = new System.Windows.Forms.Button();
            this.buttonAddWaypoint = new System.Windows.Forms.Button();
            this.buttonStartRecording = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecordDistance)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAltitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonAddCurrentLocation);
            this.groupBox2.Controls.Add(this.buttonSaveRoute);
            this.groupBox2.Controls.Add(this.buttonSetAsTarget);
            this.groupBox2.Controls.Add(this.buttonSaveRouteAs);
            this.groupBox2.Controls.Add(this.buttonLoadRoute);
            this.groupBox2.Controls.Add(this.buttonDeleteWaypoint);
            this.groupBox2.Controls.Add(this.buttonAddWaypoint);
            this.groupBox2.Controls.Add(this.listBoxWaypoints);
            this.groupBox2.Location = new System.Drawing.Point(12, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 206);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Waypoints";
            // 
            // listBoxWaypoints
            // 
            this.listBoxWaypoints.DisplayMember = "Name";
            this.listBoxWaypoints.FormattingEnabled = true;
            this.listBoxWaypoints.Location = new System.Drawing.Point(9, 19);
            this.listBoxWaypoints.Name = "listBoxWaypoints";
            this.listBoxWaypoints.Size = new System.Drawing.Size(218, 147);
            this.listBoxWaypoints.TabIndex = 0;
            this.listBoxWaypoints.ValueMember = "Name";
            this.listBoxWaypoints.SelectedIndexChanged += new System.EventHandler(this.listBoxWaypoints_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.numericUpDownRecordDistance);
            this.groupBox4.Location = new System.Drawing.Point(298, 67);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(163, 49);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Recording options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Log every:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "m";
            // 
            // numericUpDownRecordDistance
            // 
            this.numericUpDownRecordDistance.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownRecordDistance.Location = new System.Drawing.Point(69, 19);
            this.numericUpDownRecordDistance.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownRecordDistance.Name = "numericUpDownRecordDistance";
            this.numericUpDownRecordDistance.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownRecordDistance.TabIndex = 8;
            this.toolTip1.SetToolTip(this.numericUpDownRecordDistance, "At what distance to log locations while route recording");
            this.numericUpDownRecordDistance.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonBelowAltitude);
            this.groupBox3.Controls.Add(this.radioButtonAboveAltitude);
            this.groupBox3.Controls.Add(this.numericUpDownAltitude);
            this.groupBox3.Controls.Add(this.checkBoxTestAltitude);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.textBoxWaypointName);
            this.groupBox3.Controls.Add(this.numericUpDownRadius);
            this.groupBox3.Controls.Add(this.checkBoxRadius);
            this.groupBox3.Location = new System.Drawing.Point(298, 122);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(163, 125);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Waypoint info";
            // 
            // radioButtonBelowAltitude
            // 
            this.radioButtonBelowAltitude.AutoSize = true;
            this.radioButtonBelowAltitude.Location = new System.Drawing.Point(104, 94);
            this.radioButtonBelowAltitude.Name = "radioButtonBelowAltitude";
            this.radioButtonBelowAltitude.Size = new System.Drawing.Size(53, 17);
            this.radioButtonBelowAltitude.TabIndex = 7;
            this.radioButtonBelowAltitude.TabStop = true;
            this.radioButtonBelowAltitude.Text = "below";
            this.toolTip1.SetToolTip(this.radioButtonBelowAltitude, "Only altitudes above that specified are within the waypoint");
            this.radioButtonBelowAltitude.UseVisualStyleBackColor = true;
            this.radioButtonBelowAltitude.CheckedChanged += new System.EventHandler(this.radioButtonBelowAltitude_CheckedChanged);
            // 
            // radioButtonAboveAltitude
            // 
            this.radioButtonAboveAltitude.AutoSize = true;
            this.radioButtonAboveAltitude.Location = new System.Drawing.Point(43, 94);
            this.radioButtonAboveAltitude.Name = "radioButtonAboveAltitude";
            this.radioButtonAboveAltitude.Size = new System.Drawing.Size(55, 17);
            this.radioButtonAboveAltitude.TabIndex = 6;
            this.radioButtonAboveAltitude.TabStop = true;
            this.radioButtonAboveAltitude.Text = "above";
            this.toolTip1.SetToolTip(this.radioButtonAboveAltitude, "Only altitudes below that specified are within the waypoint");
            this.radioButtonAboveAltitude.UseVisualStyleBackColor = true;
            this.radioButtonAboveAltitude.CheckedChanged += new System.EventHandler(this.radioButtonAboveAltitude_CheckedChanged);
            // 
            // numericUpDownAltitude
            // 
            this.numericUpDownAltitude.Location = new System.Drawing.Point(98, 70);
            this.numericUpDownAltitude.Name = "numericUpDownAltitude";
            this.numericUpDownAltitude.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownAltitude.TabIndex = 5;
            this.numericUpDownAltitude.ValueChanged += new System.EventHandler(this.numericUpDownAltitude_ValueChanged);
            // 
            // checkBoxTestAltitude
            // 
            this.checkBoxTestAltitude.AutoSize = true;
            this.checkBoxTestAltitude.Location = new System.Drawing.Point(6, 71);
            this.checkBoxTestAltitude.Name = "checkBoxTestAltitude";
            this.checkBoxTestAltitude.Size = new System.Drawing.Size(64, 17);
            this.checkBoxTestAltitude.TabIndex = 4;
            this.checkBoxTestAltitude.Text = "Altitude:";
            this.toolTip1.SetToolTip(this.checkBoxTestAltitude, "Whether altitude is included as part of the waypoint");
            this.checkBoxTestAltitude.UseVisualStyleBackColor = true;
            this.checkBoxTestAltitude.CheckedChanged += new System.EventHandler(this.checkBoxTestAltitude_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Name:";
            // 
            // textBoxWaypointName
            // 
            this.textBoxWaypointName.Location = new System.Drawing.Point(50, 19);
            this.textBoxWaypointName.Name = "textBoxWaypointName";
            this.textBoxWaypointName.Size = new System.Drawing.Size(107, 20);
            this.textBoxWaypointName.TabIndex = 2;
            this.toolTip1.SetToolTip(this.textBoxWaypointName, "Name of waypoint");
            this.textBoxWaypointName.TextChanged += new System.EventHandler(this.textBoxWaypointName_TextChanged);
            // 
            // numericUpDownRadius
            // 
            this.numericUpDownRadius.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownRadius.Location = new System.Drawing.Point(98, 44);
            this.numericUpDownRadius.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownRadius.Name = "numericUpDownRadius";
            this.numericUpDownRadius.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownRadius.TabIndex = 1;
            this.numericUpDownRadius.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownRadius.ValueChanged += new System.EventHandler(this.numericUpDownRadius_ValueChanged);
            // 
            // checkBoxRadius
            // 
            this.checkBoxRadius.AutoSize = true;
            this.checkBoxRadius.Checked = true;
            this.checkBoxRadius.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRadius.Location = new System.Drawing.Point(6, 45);
            this.checkBoxRadius.Name = "checkBoxRadius";
            this.checkBoxRadius.Size = new System.Drawing.Size(79, 17);
            this.checkBoxRadius.TabIndex = 0;
            this.checkBoxRadius.Text = "Radius (m):";
            this.toolTip1.SetToolTip(this.checkBoxRadius, "Radius defining the hitbox of the waypoint");
            this.checkBoxRadius.UseVisualStyleBackColor = true;
            this.checkBoxRadius.CheckedChanged += new System.EventHandler(this.checkBoxRadius_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxRouteName);
            this.groupBox1.Location = new System.Drawing.Point(298, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 49);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Route name";
            // 
            // textBoxRouteName
            // 
            this.textBoxRouteName.Location = new System.Drawing.Point(6, 19);
            this.textBoxRouteName.Name = "textBoxRouteName";
            this.textBoxRouteName.Size = new System.Drawing.Size(151, 20);
            this.textBoxRouteName.TabIndex = 0;
            this.textBoxRouteName.TextChanged += new System.EventHandler(this.textBoxRouteName_TextChanged);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Image = global::SRVTracker.Properties.Resources.Settings_16x;
            this.buttonSettings.Location = new System.Drawing.Point(258, 12);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(34, 23);
            this.buttonSettings.TabIndex = 12;
            this.toolTip1.SetToolTip(this.buttonSettings, "Show/hide further settings");
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Image = global::SRVTracker.Properties.Resources.Run_16x1;
            this.buttonPlay.Location = new System.Drawing.Point(52, 12);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(34, 23);
            this.buttonPlay.TabIndex = 11;
            this.toolTip1.SetToolTip(this.buttonPlay, "Play route");
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Image = global::SRVTracker.Properties.Resources.Stop_16x;
            this.buttonStop.Location = new System.Drawing.Point(92, 12);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(34, 23);
            this.buttonStop.TabIndex = 10;
            this.toolTip1.SetToolTip(this.buttonStop, "Stop (recording or playing)");
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStopRecording_Click);
            // 
            // buttonAddCurrentLocation
            // 
            this.buttonAddCurrentLocation.Image = global::SRVTracker.Properties.Resources.AddIndexer_16x;
            this.buttonAddCurrentLocation.Location = new System.Drawing.Point(9, 172);
            this.buttonAddCurrentLocation.Name = "buttonAddCurrentLocation";
            this.buttonAddCurrentLocation.Size = new System.Drawing.Size(34, 23);
            this.buttonAddCurrentLocation.TabIndex = 16;
            this.toolTip1.SetToolTip(this.buttonAddCurrentLocation, "Add current location");
            this.buttonAddCurrentLocation.UseVisualStyleBackColor = true;
            this.buttonAddCurrentLocation.Click += new System.EventHandler(this.buttonAddCurrentLocation_Click);
            // 
            // buttonSaveRoute
            // 
            this.buttonSaveRoute.Enabled = false;
            this.buttonSaveRoute.Image = global::SRVTracker.Properties.Resources.Save_16x;
            this.buttonSaveRoute.Location = new System.Drawing.Point(233, 77);
            this.buttonSaveRoute.Name = "buttonSaveRoute";
            this.buttonSaveRoute.Size = new System.Drawing.Size(34, 23);
            this.buttonSaveRoute.TabIndex = 15;
            this.toolTip1.SetToolTip(this.buttonSaveRoute, "Save");
            this.buttonSaveRoute.UseVisualStyleBackColor = true;
            this.buttonSaveRoute.Click += new System.EventHandler(this.buttonSaveRoute_Click);
            // 
            // buttonSetAsTarget
            // 
            this.buttonSetAsTarget.Image = global::SRVTracker.Properties.Resources.Target_16x;
            this.buttonSetAsTarget.Location = new System.Drawing.Point(193, 172);
            this.buttonSetAsTarget.Name = "buttonSetAsTarget";
            this.buttonSetAsTarget.Size = new System.Drawing.Size(34, 23);
            this.buttonSetAsTarget.TabIndex = 5;
            this.toolTip1.SetToolTip(this.buttonSetAsTarget, "Target currently selected location");
            this.buttonSetAsTarget.UseVisualStyleBackColor = true;
            this.buttonSetAsTarget.Click += new System.EventHandler(this.buttonSetAsTarget_Click);
            // 
            // buttonSaveRouteAs
            // 
            this.buttonSaveRouteAs.Image = global::SRVTracker.Properties.Resources.SaveAs_16x;
            this.buttonSaveRouteAs.Location = new System.Drawing.Point(233, 48);
            this.buttonSaveRouteAs.Name = "buttonSaveRouteAs";
            this.buttonSaveRouteAs.Size = new System.Drawing.Size(34, 23);
            this.buttonSaveRouteAs.TabIndex = 13;
            this.toolTip1.SetToolTip(this.buttonSaveRouteAs, "Save as...");
            this.buttonSaveRouteAs.UseVisualStyleBackColor = true;
            this.buttonSaveRouteAs.Click += new System.EventHandler(this.buttonSaveRouteAs_Click);
            // 
            // buttonLoadRoute
            // 
            this.buttonLoadRoute.Image = global::SRVTracker.Properties.Resources.OpenFile_16x;
            this.buttonLoadRoute.Location = new System.Drawing.Point(233, 19);
            this.buttonLoadRoute.Name = "buttonLoadRoute";
            this.buttonLoadRoute.Size = new System.Drawing.Size(34, 23);
            this.buttonLoadRoute.TabIndex = 14;
            this.buttonLoadRoute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.buttonLoadRoute, "Load from file");
            this.buttonLoadRoute.UseVisualStyleBackColor = true;
            this.buttonLoadRoute.Click += new System.EventHandler(this.buttonLoadRoute_Click);
            // 
            // buttonDeleteWaypoint
            // 
            this.buttonDeleteWaypoint.Image = global::SRVTracker.Properties.Resources.Remove_color_16x;
            this.buttonDeleteWaypoint.Location = new System.Drawing.Point(89, 172);
            this.buttonDeleteWaypoint.Name = "buttonDeleteWaypoint";
            this.buttonDeleteWaypoint.Size = new System.Drawing.Size(34, 23);
            this.buttonDeleteWaypoint.TabIndex = 2;
            this.toolTip1.SetToolTip(this.buttonDeleteWaypoint, "Remove selected location");
            this.buttonDeleteWaypoint.UseVisualStyleBackColor = true;
            this.buttonDeleteWaypoint.Click += new System.EventHandler(this.buttonDeleteWaypoint_Click);
            // 
            // buttonAddWaypoint
            // 
            this.buttonAddWaypoint.Enabled = false;
            this.buttonAddWaypoint.Image = global::SRVTracker.Properties.Resources.Add_16x;
            this.buttonAddWaypoint.Location = new System.Drawing.Point(49, 172);
            this.buttonAddWaypoint.Name = "buttonAddWaypoint";
            this.buttonAddWaypoint.Size = new System.Drawing.Size(34, 23);
            this.buttonAddWaypoint.TabIndex = 1;
            this.toolTip1.SetToolTip(this.buttonAddWaypoint, "Add saved location");
            this.buttonAddWaypoint.UseVisualStyleBackColor = true;
            this.buttonAddWaypoint.Click += new System.EventHandler(this.buttonAddWaypoint_Click);
            // 
            // buttonStartRecording
            // 
            this.buttonStartRecording.Image = global::SRVTracker.Properties.Resources.RecordDot_16x;
            this.buttonStartRecording.Location = new System.Drawing.Point(12, 12);
            this.buttonStartRecording.Name = "buttonStartRecording";
            this.buttonStartRecording.Size = new System.Drawing.Size(34, 23);
            this.buttonStartRecording.TabIndex = 7;
            this.toolTip1.SetToolTip(this.buttonStartRecording, "Start recording route");
            this.buttonStartRecording.UseVisualStyleBackColor = true;
            this.buttonStartRecording.Click += new System.EventHandler(this.buttonStartRecording_Click);
            // 
            // FormRouter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 279);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonStartRecording);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormRouter";
            this.Text = "Route Recorder/Player";
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecordDistance)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAltitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownRecordDistance;
        private System.Windows.Forms.Button buttonStartRecording;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericUpDownRadius;
        private System.Windows.Forms.CheckBox checkBoxRadius;
        private System.Windows.Forms.Button buttonSetAsTarget;
        private System.Windows.Forms.Button buttonDeleteWaypoint;
        private System.Windows.Forms.Button buttonAddWaypoint;
        private System.Windows.Forms.ListBox listBoxWaypoints;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSaveRoute;
        private System.Windows.Forms.Button buttonSaveRouteAs;
        private System.Windows.Forms.Button buttonLoadRoute;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxWaypointName;
        private System.Windows.Forms.RadioButton radioButtonBelowAltitude;
        private System.Windows.Forms.RadioButton radioButtonAboveAltitude;
        private System.Windows.Forms.NumericUpDown numericUpDownAltitude;
        private System.Windows.Forms.CheckBox checkBoxTestAltitude;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxRouteName;
        private System.Windows.Forms.Button buttonAddCurrentLocation;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}