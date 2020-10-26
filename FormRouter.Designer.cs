namespace SRVTracker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRouter));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonDuplicateWaypoint = new System.Windows.Forms.Button();
            this.buttonMoveDown = new System.Windows.Forms.Button();
            this.buttonAddCurrentLocation = new System.Windows.Forms.Button();
            this.buttonSaveRoute = new System.Windows.Forms.Button();
            this.buttonMoveUp = new System.Windows.Forms.Button();
            this.buttonSetAsTarget = new System.Windows.Forms.Button();
            this.buttonSaveRouteAs = new System.Windows.Forms.Button();
            this.buttonLoadRoute = new System.Windows.Forms.Button();
            this.buttonDeleteWaypoint = new System.Windows.Forms.Button();
            this.buttonAddWaypoint = new System.Windows.Forms.Button();
            this.listBoxWaypoints = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownRecordDistance = new System.Windows.Forms.NumericUpDown();
            this.groupBoxWaypointInfo = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownMaxAltitude = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinAltitude = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxWaypointName = new System.Windows.Forms.TextBox();
            this.numericUpDownRadius = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxRouteName = new System.Windows.Forms.TextBox();
            this.locationManager1 = new SRVTracker.LocationManager();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStartRecording = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecordDistance)).BeginInit();
            this.groupBoxWaypointInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxAltitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinAltitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonDuplicateWaypoint);
            this.groupBox2.Controls.Add(this.buttonMoveDown);
            this.groupBox2.Controls.Add(this.buttonAddCurrentLocation);
            this.groupBox2.Controls.Add(this.buttonSaveRoute);
            this.groupBox2.Controls.Add(this.buttonMoveUp);
            this.groupBox2.Controls.Add(this.buttonSetAsTarget);
            this.groupBox2.Controls.Add(this.buttonSaveRouteAs);
            this.groupBox2.Controls.Add(this.buttonLoadRoute);
            this.groupBox2.Controls.Add(this.buttonDeleteWaypoint);
            this.groupBox2.Controls.Add(this.buttonAddWaypoint);
            this.groupBox2.Controls.Add(this.listBoxWaypoints);
            this.groupBox2.Location = new System.Drawing.Point(6, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 232);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Waypoints";
            // 
            // buttonDuplicateWaypoint
            // 
            this.buttonDuplicateWaypoint.Image = global::SRVTracker.Properties.Resources.CopyItem_16x;
            this.buttonDuplicateWaypoint.Location = new System.Drawing.Point(45, 203);
            this.buttonDuplicateWaypoint.Name = "buttonDuplicateWaypoint";
            this.buttonDuplicateWaypoint.Size = new System.Drawing.Size(34, 23);
            this.buttonDuplicateWaypoint.TabIndex = 19;
            this.toolTip1.SetToolTip(this.buttonDuplicateWaypoint, "Duplicate selected waypoint\r\n(duplicate added to end of list)");
            this.buttonDuplicateWaypoint.UseVisualStyleBackColor = true;
            this.buttonDuplicateWaypoint.Click += new System.EventHandler(this.buttonDuplicateWaypoint_Click);
            // 
            // buttonMoveDown
            // 
            this.buttonMoveDown.Image = global::SRVTracker.Properties.Resources.ExpandDown_lg_16x;
            this.buttonMoveDown.Location = new System.Drawing.Point(197, 203);
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.buttonMoveDown.Size = new System.Drawing.Size(30, 23);
            this.buttonMoveDown.TabIndex = 18;
            this.buttonMoveDown.UseVisualStyleBackColor = true;
            this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
            // 
            // buttonAddCurrentLocation
            // 
            this.buttonAddCurrentLocation.Image = global::SRVTracker.Properties.Resources.AddIndexer_16x;
            this.buttonAddCurrentLocation.Location = new System.Drawing.Point(9, 203);
            this.buttonAddCurrentLocation.Name = "buttonAddCurrentLocation";
            this.buttonAddCurrentLocation.Size = new System.Drawing.Size(30, 23);
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
            // buttonMoveUp
            // 
            this.buttonMoveUp.Image = global::SRVTracker.Properties.Resources.CollapseUp_lg_16x;
            this.buttonMoveUp.Location = new System.Drawing.Point(161, 203);
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.buttonMoveUp.Size = new System.Drawing.Size(30, 23);
            this.buttonMoveUp.TabIndex = 17;
            this.buttonMoveUp.UseVisualStyleBackColor = true;
            this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
            // 
            // buttonSetAsTarget
            // 
            this.buttonSetAsTarget.Image = global::SRVTracker.Properties.Resources.Target_16x;
            this.buttonSetAsTarget.Location = new System.Drawing.Point(233, 129);
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
            this.buttonDeleteWaypoint.Location = new System.Drawing.Point(121, 203);
            this.buttonDeleteWaypoint.Name = "buttonDeleteWaypoint";
            this.buttonDeleteWaypoint.Size = new System.Drawing.Size(30, 23);
            this.buttonDeleteWaypoint.TabIndex = 2;
            this.toolTip1.SetToolTip(this.buttonDeleteWaypoint, "Remove selected location");
            this.buttonDeleteWaypoint.UseVisualStyleBackColor = true;
            this.buttonDeleteWaypoint.Click += new System.EventHandler(this.buttonDeleteWaypoint_Click);
            // 
            // buttonAddWaypoint
            // 
            this.buttonAddWaypoint.Image = global::SRVTracker.Properties.Resources.Add_16x;
            this.buttonAddWaypoint.Location = new System.Drawing.Point(85, 203);
            this.buttonAddWaypoint.Name = "buttonAddWaypoint";
            this.buttonAddWaypoint.Size = new System.Drawing.Size(30, 23);
            this.buttonAddWaypoint.TabIndex = 1;
            this.toolTip1.SetToolTip(this.buttonAddWaypoint, "Add saved location");
            this.buttonAddWaypoint.UseVisualStyleBackColor = true;
            this.buttonAddWaypoint.Click += new System.EventHandler(this.buttonAddWaypoint_Click);
            // 
            // listBoxWaypoints
            // 
            this.listBoxWaypoints.DisplayMember = "Name";
            this.listBoxWaypoints.FormattingEnabled = true;
            this.listBoxWaypoints.Location = new System.Drawing.Point(9, 19);
            this.listBoxWaypoints.Name = "listBoxWaypoints";
            this.listBoxWaypoints.Size = new System.Drawing.Size(218, 173);
            this.listBoxWaypoints.TabIndex = 0;
            this.listBoxWaypoints.ValueMember = "Name";
            this.listBoxWaypoints.SelectedIndexChanged += new System.EventHandler(this.listBoxWaypoints_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.numericUpDownRecordDistance);
            this.groupBox4.Location = new System.Drawing.Point(292, 61);
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
            // groupBoxWaypointInfo
            // 
            this.groupBoxWaypointInfo.Controls.Add(this.label6);
            this.groupBoxWaypointInfo.Controls.Add(this.label5);
            this.groupBoxWaypointInfo.Controls.Add(this.label4);
            this.groupBoxWaypointInfo.Controls.Add(this.numericUpDownMaxAltitude);
            this.groupBoxWaypointInfo.Controls.Add(this.numericUpDownMinAltitude);
            this.groupBoxWaypointInfo.Controls.Add(this.label3);
            this.groupBoxWaypointInfo.Controls.Add(this.textBoxWaypointName);
            this.groupBoxWaypointInfo.Controls.Add(this.numericUpDownRadius);
            this.groupBoxWaypointInfo.Location = new System.Drawing.Point(292, 168);
            this.groupBoxWaypointInfo.Name = "groupBoxWaypointInfo";
            this.groupBoxWaypointInfo.Size = new System.Drawing.Size(163, 125);
            this.groupBoxWaypointInfo.TabIndex = 6;
            this.groupBoxWaypointInfo.TabStop = false;
            this.groupBoxWaypointInfo.Text = "Waypoint info";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Radius:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Maximum altitude:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Minimum altitude:";
            // 
            // numericUpDownMaxAltitude
            // 
            this.numericUpDownMaxAltitude.Location = new System.Drawing.Point(98, 90);
            this.numericUpDownMaxAltitude.Name = "numericUpDownMaxAltitude";
            this.numericUpDownMaxAltitude.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownMaxAltitude.TabIndex = 7;
            this.numericUpDownMaxAltitude.ValueChanged += new System.EventHandler(this.numericUpDownMaxAltitude_ValueChanged);
            // 
            // numericUpDownMinAltitude
            // 
            this.numericUpDownMinAltitude.Location = new System.Drawing.Point(98, 67);
            this.numericUpDownMinAltitude.Name = "numericUpDownMinAltitude";
            this.numericUpDownMinAltitude.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownMinAltitude.TabIndex = 5;
            this.numericUpDownMinAltitude.ValueChanged += new System.EventHandler(this.numericUpDownMinAltitude_ValueChanged);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxRouteName);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 49);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Route name";
            // 
            // textBoxRouteName
            // 
            this.textBoxRouteName.Location = new System.Drawing.Point(6, 19);
            this.textBoxRouteName.Name = "textBoxRouteName";
            this.textBoxRouteName.Size = new System.Drawing.Size(268, 20);
            this.textBoxRouteName.TabIndex = 0;
            this.textBoxRouteName.TextChanged += new System.EventHandler(this.textBoxRouteName_TextChanged);
            // 
            // locationManager1
            // 
            this.locationManager1.Location = new System.Drawing.Point(461, 6);
            this.locationManager1.LocatorForm = null;
            this.locationManager1.Name = "locationManager1";
            this.locationManager1.Size = new System.Drawing.Size(281, 287);
            this.locationManager1.TabIndex = 14;
            this.locationManager1.SelectionChanged += new System.EventHandler(this.locationManager1_SelectionChanged);
            // 
            // buttonStop
            // 
            this.buttonStop.Image = global::SRVTracker.Properties.Resources.Stop_16x;
            this.buttonStop.Location = new System.Drawing.Point(106, 16);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(34, 23);
            this.buttonStop.TabIndex = 10;
            this.toolTip1.SetToolTip(this.buttonStop, "Stop (recording or playing)");
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStopRecording_Click);
            // 
            // buttonStartRecording
            // 
            this.buttonStartRecording.Image = global::SRVTracker.Properties.Resources.RecordDot_16x;
            this.buttonStartRecording.Location = new System.Drawing.Point(26, 16);
            this.buttonStartRecording.Name = "buttonStartRecording";
            this.buttonStartRecording.Size = new System.Drawing.Size(34, 23);
            this.buttonStartRecording.TabIndex = 7;
            this.toolTip1.SetToolTip(this.buttonStartRecording, "Start recording route");
            this.buttonStartRecording.UseVisualStyleBackColor = true;
            this.buttonStartRecording.Click += new System.EventHandler(this.buttonStartRecording_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Image = global::SRVTracker.Properties.Resources.Run_16x1;
            this.buttonPlay.Location = new System.Drawing.Point(66, 16);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(34, 23);
            this.buttonPlay.TabIndex = 11;
            this.toolTip1.SetToolTip(this.buttonPlay, "Play route");
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonStop);
            this.groupBox5.Controls.Add(this.buttonStartRecording);
            this.groupBox5.Controls.Add(this.buttonPlay);
            this.groupBox5.Location = new System.Drawing.Point(292, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(163, 49);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            // 
            // FormRouter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 311);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.locationManager1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxWaypointInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormRouter";
            this.Text = "Route Recorder/Player";
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecordDistance)).EndInit();
            this.groupBoxWaypointInfo.ResumeLayout(false);
            this.groupBoxWaypointInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxAltitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinAltitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownRecordDistance;
        private System.Windows.Forms.Button buttonStartRecording;
        private System.Windows.Forms.GroupBox groupBoxWaypointInfo;
        private System.Windows.Forms.NumericUpDown numericUpDownRadius;
        private System.Windows.Forms.Button buttonSetAsTarget;
        private System.Windows.Forms.Button buttonDeleteWaypoint;
        private System.Windows.Forms.Button buttonAddWaypoint;
        private System.Windows.Forms.ListBox listBoxWaypoints;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSaveRoute;
        private System.Windows.Forms.Button buttonSaveRouteAs;
        private System.Windows.Forms.Button buttonLoadRoute;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxWaypointName;
        private System.Windows.Forms.NumericUpDown numericUpDownMinAltitude;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxRouteName;
        private System.Windows.Forms.Button buttonAddCurrentLocation;
        private System.Windows.Forms.ToolTip toolTip1;
        private LocationManager locationManager1;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxAltitude;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonMoveDown;
        private System.Windows.Forms.Button buttonMoveUp;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonDuplicateWaypoint;
    }
}