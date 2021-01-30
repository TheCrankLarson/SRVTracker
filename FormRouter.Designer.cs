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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRouter));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonGenerateCircumnavigationWaypoints = new System.Windows.Forms.Button();
            this.buttonEditLocations = new System.Windows.Forms.Button();
            this.buttonReverseWaypointOrder = new System.Windows.Forms.Button();
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
            this.groupBoxPolygon = new System.Windows.Forms.GroupBox();
            this.buttonPolygonMarkerAdd = new System.Windows.Forms.Button();
            this.buttonPolygonMarkerDelete = new System.Windows.Forms.Button();
            this.listBoxPolygonMarkers = new System.Windows.Forms.ListBox();
            this.buttonPolygonTargetTarget = new System.Windows.Forms.Button();
            this.buttonPolygonMarkerTarget = new System.Windows.Forms.Button();
            this.buttonPolygonTargetCalculate = new System.Windows.Forms.Button();
            this.buttonPolygonTargetEdit = new System.Windows.Forms.Button();
            this.buttonPolygonTargetUseCurrentLocation = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxPolygonTarget = new System.Windows.Forms.ComboBox();
            this.buttonPolygonMarkerEdit = new System.Windows.Forms.Button();
            this.buttonPolygonMarkerUseCurrentLocation = new System.Windows.Forms.Button();
            this.groupBoxAltitude = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownMinAltitude = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownMaxAltitude = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxWaypointType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxWaypointName = new System.Windows.Forms.TextBox();
            this.groupBoxGate = new System.Windows.Forms.GroupBox();
            this.buttonTargetGateTarget = new System.Windows.Forms.Button();
            this.buttonTargetGateMarker2 = new System.Windows.Forms.Button();
            this.buttonTargetGateMarker1 = new System.Windows.Forms.Button();
            this.buttonCalculateGateTarget = new System.Windows.Forms.Button();
            this.buttonEditGateTarget = new System.Windows.Forms.Button();
            this.buttonSetGateTargetToCurrentLocation = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxGateTarget = new System.Windows.Forms.ComboBox();
            this.buttonEditGateMarker2 = new System.Windows.Forms.Button();
            this.buttonEditGateMarker1 = new System.Windows.Forms.Button();
            this.buttonSetGateLocation2ToCurrentLocation = new System.Windows.Forms.Button();
            this.buttonSetGateLocation1ToCurrentLocation = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxGateLocation2 = new System.Windows.Forms.ComboBox();
            this.comboBoxGateLocation1 = new System.Windows.Forms.ComboBox();
            this.groupBoxBasic = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxBasicLocation = new System.Windows.Forms.ComboBox();
            this.buttonEditBasicLocation = new System.Windows.Forms.Button();
            this.numericUpDownRadius = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAllowPassing = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxRouteName = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxPlayIncudeDirection = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableAudio = new System.Windows.Forms.CheckBox();
            this.checkBoxTimeTrial = new System.Windows.Forms.CheckBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStartRecording = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.checkBoxScreenshot = new System.Windows.Forms.CheckBox();
            this.numericUpDownTotalLaps = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBoxAudioSettings = new System.Windows.Forms.GroupBox();
            this.comboBoxChooseSound = new System.Windows.Forms.ComboBox();
            this.listBoxAudioEvents = new System.Windows.Forms.ListBox();
            this.circumnavigationContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNorthPoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSouthPoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addOriginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPrimeMeridianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addEquatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateRouteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageRoute = new System.Windows.Forms.TabPage();
            this.tabPageWaypoints = new System.Windows.Forms.TabPage();
            this.tabPageAudio = new System.Windows.Forms.TabPage();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecordDistance)).BeginInit();
            this.groupBoxWaypointInfo.SuspendLayout();
            this.groupBoxPolygon.SuspendLayout();
            this.groupBoxAltitude.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinAltitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxAltitude)).BeginInit();
            this.groupBoxGate.SuspendLayout();
            this.groupBoxBasic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalLaps)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBoxAudioSettings.SuspendLayout();
            this.circumnavigationContextMenuStrip.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageRoute.SuspendLayout();
            this.tabPageWaypoints.SuspendLayout();
            this.tabPageAudio.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonGenerateCircumnavigationWaypoints);
            this.groupBox2.Controls.Add(this.buttonEditLocations);
            this.groupBox2.Controls.Add(this.buttonReverseWaypointOrder);
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
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 232);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Waypoints";
            // 
            // buttonGenerateCircumnavigationWaypoints
            // 
            this.buttonGenerateCircumnavigationWaypoints.Image = global::SRVTracker.Properties.Resources.SpherePreview_16x;
            this.buttonGenerateCircumnavigationWaypoints.Location = new System.Drawing.Point(198, 198);
            this.buttonGenerateCircumnavigationWaypoints.Name = "buttonGenerateCircumnavigationWaypoints";
            this.buttonGenerateCircumnavigationWaypoints.Size = new System.Drawing.Size(26, 23);
            this.buttonGenerateCircumnavigationWaypoints.TabIndex = 22;
            this.toolTip1.SetToolTip(this.buttonGenerateCircumnavigationWaypoints, "Circumnavigation features\r\n(e.g. North and South Pole)");
            this.buttonGenerateCircumnavigationWaypoints.UseVisualStyleBackColor = true;
            this.buttonGenerateCircumnavigationWaypoints.Click += new System.EventHandler(this.buttonGenerateCircumnavigationWaypoints_Click);
            // 
            // buttonEditLocations
            // 
            this.buttonEditLocations.Image = global::SRVTracker.Properties.Resources.AddressEditor_16x;
            this.buttonEditLocations.Location = new System.Drawing.Point(233, 169);
            this.buttonEditLocations.Name = "buttonEditLocations";
            this.buttonEditLocations.Size = new System.Drawing.Size(34, 23);
            this.buttonEditLocations.TabIndex = 21;
            this.toolTip1.SetToolTip(this.buttonEditLocations, "Edit locations");
            this.buttonEditLocations.UseVisualStyleBackColor = true;
            this.buttonEditLocations.Click += new System.EventHandler(this.buttonEditLocations_Click);
            // 
            // buttonReverseWaypointOrder
            // 
            this.buttonReverseWaypointOrder.Image = global::SRVTracker.Properties.Resources.ReversePath_16x;
            this.buttonReverseWaypointOrder.Location = new System.Drawing.Point(171, 198);
            this.buttonReverseWaypointOrder.Name = "buttonReverseWaypointOrder";
            this.buttonReverseWaypointOrder.Size = new System.Drawing.Size(26, 23);
            this.buttonReverseWaypointOrder.TabIndex = 20;
            this.toolTip1.SetToolTip(this.buttonReverseWaypointOrder, "Reverse the entire route");
            this.buttonReverseWaypointOrder.UseVisualStyleBackColor = true;
            this.buttonReverseWaypointOrder.Click += new System.EventHandler(this.buttonReverseWaypointOrder_Click);
            // 
            // buttonDuplicateWaypoint
            // 
            this.buttonDuplicateWaypoint.Image = global::SRVTracker.Properties.Resources.CopyItem_16x;
            this.buttonDuplicateWaypoint.Location = new System.Drawing.Point(36, 198);
            this.buttonDuplicateWaypoint.Name = "buttonDuplicateWaypoint";
            this.buttonDuplicateWaypoint.Size = new System.Drawing.Size(26, 23);
            this.buttonDuplicateWaypoint.TabIndex = 19;
            this.toolTip1.SetToolTip(this.buttonDuplicateWaypoint, "Duplicate selected waypoint\r\n(duplicate added to end of list)");
            this.buttonDuplicateWaypoint.UseVisualStyleBackColor = true;
            this.buttonDuplicateWaypoint.Click += new System.EventHandler(this.buttonDuplicateWaypoint_Click);
            // 
            // buttonMoveDown
            // 
            this.buttonMoveDown.Image = global::SRVTracker.Properties.Resources.ExpandDown_lg_16x;
            this.buttonMoveDown.Location = new System.Drawing.Point(144, 198);
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.buttonMoveDown.Size = new System.Drawing.Size(26, 23);
            this.buttonMoveDown.TabIndex = 18;
            this.buttonMoveDown.UseVisualStyleBackColor = true;
            this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
            // 
            // buttonAddCurrentLocation
            // 
            this.buttonAddCurrentLocation.Image = global::SRVTracker.Properties.Resources.AddIndexer_16x;
            this.buttonAddCurrentLocation.Location = new System.Drawing.Point(9, 198);
            this.buttonAddCurrentLocation.Name = "buttonAddCurrentLocation";
            this.buttonAddCurrentLocation.Size = new System.Drawing.Size(26, 23);
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
            this.buttonMoveUp.Location = new System.Drawing.Point(117, 198);
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.buttonMoveUp.Size = new System.Drawing.Size(26, 23);
            this.buttonMoveUp.TabIndex = 17;
            this.buttonMoveUp.UseVisualStyleBackColor = true;
            this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
            // 
            // buttonSetAsTarget
            // 
            this.buttonSetAsTarget.Image = global::SRVTracker.Properties.Resources.Target_16x;
            this.buttonSetAsTarget.Location = new System.Drawing.Point(233, 123);
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
            this.buttonDeleteWaypoint.Location = new System.Drawing.Point(90, 198);
            this.buttonDeleteWaypoint.Name = "buttonDeleteWaypoint";
            this.buttonDeleteWaypoint.Size = new System.Drawing.Size(26, 23);
            this.buttonDeleteWaypoint.TabIndex = 2;
            this.toolTip1.SetToolTip(this.buttonDeleteWaypoint, "Remove selected location");
            this.buttonDeleteWaypoint.UseVisualStyleBackColor = true;
            this.buttonDeleteWaypoint.Click += new System.EventHandler(this.buttonDeleteWaypoint_Click);
            // 
            // buttonAddWaypoint
            // 
            this.buttonAddWaypoint.Image = global::SRVTracker.Properties.Resources.Add_16x;
            this.buttonAddWaypoint.Location = new System.Drawing.Point(63, 198);
            this.buttonAddWaypoint.Name = "buttonAddWaypoint";
            this.buttonAddWaypoint.Size = new System.Drawing.Size(26, 23);
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
            this.groupBox4.Location = new System.Drawing.Point(6, 61);
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
            250,
            0,
            0,
            0});
            // 
            // groupBoxWaypointInfo
            // 
            this.groupBoxWaypointInfo.Controls.Add(this.groupBoxPolygon);
            this.groupBoxWaypointInfo.Controls.Add(this.groupBoxAltitude);
            this.groupBoxWaypointInfo.Controls.Add(this.label7);
            this.groupBoxWaypointInfo.Controls.Add(this.comboBoxWaypointType);
            this.groupBoxWaypointInfo.Controls.Add(this.label3);
            this.groupBoxWaypointInfo.Controls.Add(this.textBoxWaypointName);
            this.groupBoxWaypointInfo.Controls.Add(this.groupBoxGate);
            this.groupBoxWaypointInfo.Controls.Add(this.groupBoxBasic);
            this.groupBoxWaypointInfo.Location = new System.Drawing.Point(6, 6);
            this.groupBoxWaypointInfo.Name = "groupBoxWaypointInfo";
            this.groupBoxWaypointInfo.Size = new System.Drawing.Size(332, 231);
            this.groupBoxWaypointInfo.TabIndex = 6;
            this.groupBoxWaypointInfo.TabStop = false;
            this.groupBoxWaypointInfo.Text = "Waypoint Information";
            // 
            // groupBoxPolygon
            // 
            this.groupBoxPolygon.Controls.Add(this.buttonPolygonMarkerAdd);
            this.groupBoxPolygon.Controls.Add(this.buttonPolygonMarkerDelete);
            this.groupBoxPolygon.Controls.Add(this.listBoxPolygonMarkers);
            this.groupBoxPolygon.Controls.Add(this.buttonPolygonTargetTarget);
            this.groupBoxPolygon.Controls.Add(this.buttonPolygonMarkerTarget);
            this.groupBoxPolygon.Controls.Add(this.buttonPolygonTargetCalculate);
            this.groupBoxPolygon.Controls.Add(this.buttonPolygonTargetEdit);
            this.groupBoxPolygon.Controls.Add(this.buttonPolygonTargetUseCurrentLocation);
            this.groupBoxPolygon.Controls.Add(this.label13);
            this.groupBoxPolygon.Controls.Add(this.comboBoxPolygonTarget);
            this.groupBoxPolygon.Controls.Add(this.buttonPolygonMarkerEdit);
            this.groupBoxPolygon.Controls.Add(this.buttonPolygonMarkerUseCurrentLocation);
            this.groupBoxPolygon.Location = new System.Drawing.Point(32, 72);
            this.groupBoxPolygon.Name = "groupBoxPolygon";
            this.groupBoxPolygon.Size = new System.Drawing.Size(270, 97);
            this.groupBoxPolygon.TabIndex = 19;
            this.groupBoxPolygon.TabStop = false;
            this.groupBoxPolygon.Visible = false;
            // 
            // buttonPolygonMarkerAdd
            // 
            this.buttonPolygonMarkerAdd.Image = global::SRVTracker.Properties.Resources.Add_16x;
            this.buttonPolygonMarkerAdd.Location = new System.Drawing.Point(221, 34);
            this.buttonPolygonMarkerAdd.Name = "buttonPolygonMarkerAdd";
            this.buttonPolygonMarkerAdd.Size = new System.Drawing.Size(22, 22);
            this.buttonPolygonMarkerAdd.TabIndex = 23;
            this.buttonPolygonMarkerAdd.UseVisualStyleBackColor = true;
            // 
            // buttonPolygonMarkerDelete
            // 
            this.buttonPolygonMarkerDelete.Image = global::SRVTracker.Properties.Resources.Remove_color_16x;
            this.buttonPolygonMarkerDelete.Location = new System.Drawing.Point(242, 34);
            this.buttonPolygonMarkerDelete.Name = "buttonPolygonMarkerDelete";
            this.buttonPolygonMarkerDelete.Size = new System.Drawing.Size(22, 22);
            this.buttonPolygonMarkerDelete.TabIndex = 22;
            this.toolTip1.SetToolTip(this.buttonPolygonMarkerDelete, "Add current location as gate marker 1");
            this.buttonPolygonMarkerDelete.UseVisualStyleBackColor = true;
            // 
            // listBoxPolygonMarkers
            // 
            this.listBoxPolygonMarkers.FormattingEnabled = true;
            this.listBoxPolygonMarkers.Location = new System.Drawing.Point(6, 13);
            this.listBoxPolygonMarkers.Name = "listBoxPolygonMarkers";
            this.listBoxPolygonMarkers.Size = new System.Drawing.Size(188, 43);
            this.listBoxPolygonMarkers.TabIndex = 21;
            this.listBoxPolygonMarkers.SelectedIndexChanged += new System.EventHandler(this.listBoxPolygonMarkers_SelectedIndexChanged);
            // 
            // buttonPolygonTargetTarget
            // 
            this.buttonPolygonTargetTarget.Image = global::SRVTracker.Properties.Resources.Target_16x;
            this.buttonPolygonTargetTarget.Location = new System.Drawing.Point(200, 67);
            this.buttonPolygonTargetTarget.Name = "buttonPolygonTargetTarget";
            this.buttonPolygonTargetTarget.Size = new System.Drawing.Size(22, 22);
            this.buttonPolygonTargetTarget.TabIndex = 20;
            this.toolTip1.SetToolTip(this.buttonPolygonTargetTarget, "Target currently selected location");
            this.buttonPolygonTargetTarget.UseVisualStyleBackColor = true;
            // 
            // buttonPolygonMarkerTarget
            // 
            this.buttonPolygonMarkerTarget.Image = global::SRVTracker.Properties.Resources.Target_16x;
            this.buttonPolygonMarkerTarget.Location = new System.Drawing.Point(200, 13);
            this.buttonPolygonMarkerTarget.Name = "buttonPolygonMarkerTarget";
            this.buttonPolygonMarkerTarget.Size = new System.Drawing.Size(22, 22);
            this.buttonPolygonMarkerTarget.TabIndex = 18;
            this.toolTip1.SetToolTip(this.buttonPolygonMarkerTarget, "Target currently selected location");
            this.buttonPolygonMarkerTarget.UseVisualStyleBackColor = true;
            // 
            // buttonPolygonTargetCalculate
            // 
            this.buttonPolygonTargetCalculate.Image = global::SRVTracker.Properties.Resources.Calculator_16x;
            this.buttonPolygonTargetCalculate.Location = new System.Drawing.Point(179, 67);
            this.buttonPolygonTargetCalculate.Name = "buttonPolygonTargetCalculate";
            this.buttonPolygonTargetCalculate.Size = new System.Drawing.Size(22, 22);
            this.buttonPolygonTargetCalculate.TabIndex = 13;
            this.buttonPolygonTargetCalculate.UseVisualStyleBackColor = true;
            // 
            // buttonPolygonTargetEdit
            // 
            this.buttonPolygonTargetEdit.Image = global::SRVTracker.Properties.Resources.Edit_16x;
            this.buttonPolygonTargetEdit.Location = new System.Drawing.Point(221, 67);
            this.buttonPolygonTargetEdit.Name = "buttonPolygonTargetEdit";
            this.buttonPolygonTargetEdit.Size = new System.Drawing.Size(22, 22);
            this.buttonPolygonTargetEdit.TabIndex = 12;
            this.buttonPolygonTargetEdit.UseVisualStyleBackColor = true;
            // 
            // buttonPolygonTargetUseCurrentLocation
            // 
            this.buttonPolygonTargetUseCurrentLocation.Image = global::SRVTracker.Properties.Resources.AddIndexer_16x;
            this.buttonPolygonTargetUseCurrentLocation.Location = new System.Drawing.Point(242, 67);
            this.buttonPolygonTargetUseCurrentLocation.Name = "buttonPolygonTargetUseCurrentLocation";
            this.buttonPolygonTargetUseCurrentLocation.Size = new System.Drawing.Size(22, 22);
            this.buttonPolygonTargetUseCurrentLocation.TabIndex = 11;
            this.toolTip1.SetToolTip(this.buttonPolygonTargetUseCurrentLocation, "Add current location as gate marker 2");
            this.buttonPolygonTargetUseCurrentLocation.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 71);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Target:";
            // 
            // comboBoxPolygonTarget
            // 
            this.comboBoxPolygonTarget.Enabled = false;
            this.comboBoxPolygonTarget.FormattingEnabled = true;
            this.comboBoxPolygonTarget.Location = new System.Drawing.Point(53, 68);
            this.comboBoxPolygonTarget.Name = "comboBoxPolygonTarget";
            this.comboBoxPolygonTarget.Size = new System.Drawing.Size(120, 21);
            this.comboBoxPolygonTarget.TabIndex = 9;
            // 
            // buttonPolygonMarkerEdit
            // 
            this.buttonPolygonMarkerEdit.Image = global::SRVTracker.Properties.Resources.Edit_16x;
            this.buttonPolygonMarkerEdit.Location = new System.Drawing.Point(221, 13);
            this.buttonPolygonMarkerEdit.Name = "buttonPolygonMarkerEdit";
            this.buttonPolygonMarkerEdit.Size = new System.Drawing.Size(22, 22);
            this.buttonPolygonMarkerEdit.TabIndex = 6;
            this.buttonPolygonMarkerEdit.UseVisualStyleBackColor = true;
            // 
            // buttonPolygonMarkerUseCurrentLocation
            // 
            this.buttonPolygonMarkerUseCurrentLocation.Image = global::SRVTracker.Properties.Resources.AddIndexer_16x;
            this.buttonPolygonMarkerUseCurrentLocation.Location = new System.Drawing.Point(242, 13);
            this.buttonPolygonMarkerUseCurrentLocation.Name = "buttonPolygonMarkerUseCurrentLocation";
            this.buttonPolygonMarkerUseCurrentLocation.Size = new System.Drawing.Size(22, 22);
            this.buttonPolygonMarkerUseCurrentLocation.TabIndex = 4;
            this.toolTip1.SetToolTip(this.buttonPolygonMarkerUseCurrentLocation, "Add current location as gate marker 1");
            this.buttonPolygonMarkerUseCurrentLocation.UseVisualStyleBackColor = true;
            // 
            // groupBoxAltitude
            // 
            this.groupBoxAltitude.Controls.Add(this.label4);
            this.groupBoxAltitude.Controls.Add(this.numericUpDownMinAltitude);
            this.groupBoxAltitude.Controls.Add(this.label5);
            this.groupBoxAltitude.Controls.Add(this.numericUpDownMaxAltitude);
            this.groupBoxAltitude.Location = new System.Drawing.Point(39, 175);
            this.groupBoxAltitude.Name = "groupBoxAltitude";
            this.groupBoxAltitude.Size = new System.Drawing.Size(256, 50);
            this.groupBoxAltitude.TabIndex = 14;
            this.groupBoxAltitude.TabStop = false;
            this.groupBoxAltitude.Text = "Altitude";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Minimum:";
            // 
            // numericUpDownMinAltitude
            // 
            this.numericUpDownMinAltitude.Location = new System.Drawing.Point(63, 19);
            this.numericUpDownMinAltitude.Name = "numericUpDownMinAltitude";
            this.numericUpDownMinAltitude.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownMinAltitude.TabIndex = 5;
            this.numericUpDownMinAltitude.ValueChanged += new System.EventHandler(this.numericUpDownMinAltitude_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(128, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Maximum:";
            // 
            // numericUpDownMaxAltitude
            // 
            this.numericUpDownMaxAltitude.Location = new System.Drawing.Point(186, 19);
            this.numericUpDownMaxAltitude.Name = "numericUpDownMaxAltitude";
            this.numericUpDownMaxAltitude.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownMaxAltitude.TabIndex = 7;
            this.numericUpDownMaxAltitude.ValueChanged += new System.EventHandler(this.numericUpDownMaxAltitude_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Waypoint type:";
            // 
            // comboBoxWaypointType
            // 
            this.comboBoxWaypointType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWaypointType.FormattingEnabled = true;
            this.comboBoxWaypointType.Items.AddRange(new object[] {
            "Basic (point with radius)",
            "Gate (pass between two points)",
            "Polygon (area defined by multiple points)"});
            this.comboBoxWaypointType.Location = new System.Drawing.Point(90, 45);
            this.comboBoxWaypointType.Name = "comboBoxWaypointType";
            this.comboBoxWaypointType.Size = new System.Drawing.Size(236, 21);
            this.comboBoxWaypointType.TabIndex = 12;
            this.toolTip1.SetToolTip(this.comboBoxWaypointType, "The waypoint type.  This affects the criteria that are used\r\nto determine if a wa" +
        "ypoint has been reached/passed.");
            this.comboBoxWaypointType.SelectedIndexChanged += new System.EventHandler(this.comboBoxWaypointType_SelectedIndexChanged);
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
            this.textBoxWaypointName.Size = new System.Drawing.Size(276, 20);
            this.textBoxWaypointName.TabIndex = 2;
            this.toolTip1.SetToolTip(this.textBoxWaypointName, "Name of waypoint");
            this.textBoxWaypointName.TextChanged += new System.EventHandler(this.textBoxWaypointName_TextChanged);
            // 
            // groupBoxGate
            // 
            this.groupBoxGate.Controls.Add(this.buttonTargetGateTarget);
            this.groupBoxGate.Controls.Add(this.buttonTargetGateMarker2);
            this.groupBoxGate.Controls.Add(this.buttonTargetGateMarker1);
            this.groupBoxGate.Controls.Add(this.buttonCalculateGateTarget);
            this.groupBoxGate.Controls.Add(this.buttonEditGateTarget);
            this.groupBoxGate.Controls.Add(this.buttonSetGateTargetToCurrentLocation);
            this.groupBoxGate.Controls.Add(this.label10);
            this.groupBoxGate.Controls.Add(this.comboBoxGateTarget);
            this.groupBoxGate.Controls.Add(this.buttonEditGateMarker2);
            this.groupBoxGate.Controls.Add(this.buttonEditGateMarker1);
            this.groupBoxGate.Controls.Add(this.buttonSetGateLocation2ToCurrentLocation);
            this.groupBoxGate.Controls.Add(this.buttonSetGateLocation1ToCurrentLocation);
            this.groupBoxGate.Controls.Add(this.label9);
            this.groupBoxGate.Controls.Add(this.label8);
            this.groupBoxGate.Controls.Add(this.comboBoxGateLocation2);
            this.groupBoxGate.Controls.Add(this.comboBoxGateLocation1);
            this.groupBoxGate.Location = new System.Drawing.Point(32, 72);
            this.groupBoxGate.Name = "groupBoxGate";
            this.groupBoxGate.Size = new System.Drawing.Size(270, 97);
            this.groupBoxGate.TabIndex = 18;
            this.groupBoxGate.TabStop = false;
            this.groupBoxGate.Visible = false;
            // 
            // buttonTargetGateTarget
            // 
            this.buttonTargetGateTarget.Image = global::SRVTracker.Properties.Resources.Target_16x;
            this.buttonTargetGateTarget.Location = new System.Drawing.Point(200, 67);
            this.buttonTargetGateTarget.Name = "buttonTargetGateTarget";
            this.buttonTargetGateTarget.Size = new System.Drawing.Size(22, 22);
            this.buttonTargetGateTarget.TabIndex = 20;
            this.toolTip1.SetToolTip(this.buttonTargetGateTarget, "Target currently selected location");
            this.buttonTargetGateTarget.UseVisualStyleBackColor = true;
            this.buttonTargetGateTarget.Click += new System.EventHandler(this.buttonTargetGateTarget_Click);
            // 
            // buttonTargetGateMarker2
            // 
            this.buttonTargetGateMarker2.Image = global::SRVTracker.Properties.Resources.Target_16x;
            this.buttonTargetGateMarker2.Location = new System.Drawing.Point(200, 41);
            this.buttonTargetGateMarker2.Name = "buttonTargetGateMarker2";
            this.buttonTargetGateMarker2.Size = new System.Drawing.Size(22, 22);
            this.buttonTargetGateMarker2.TabIndex = 19;
            this.toolTip1.SetToolTip(this.buttonTargetGateMarker2, "Target currently selected location");
            this.buttonTargetGateMarker2.UseVisualStyleBackColor = true;
            this.buttonTargetGateMarker2.Click += new System.EventHandler(this.buttonTargetGateMarker2_Click);
            // 
            // buttonTargetGateMarker1
            // 
            this.buttonTargetGateMarker1.Image = global::SRVTracker.Properties.Resources.Target_16x;
            this.buttonTargetGateMarker1.Location = new System.Drawing.Point(200, 13);
            this.buttonTargetGateMarker1.Name = "buttonTargetGateMarker1";
            this.buttonTargetGateMarker1.Size = new System.Drawing.Size(22, 22);
            this.buttonTargetGateMarker1.TabIndex = 18;
            this.toolTip1.SetToolTip(this.buttonTargetGateMarker1, "Target currently selected location");
            this.buttonTargetGateMarker1.UseVisualStyleBackColor = true;
            this.buttonTargetGateMarker1.Click += new System.EventHandler(this.buttonTargetGateMarker1_Click);
            // 
            // buttonCalculateGateTarget
            // 
            this.buttonCalculateGateTarget.Image = global::SRVTracker.Properties.Resources.Calculator_16x;
            this.buttonCalculateGateTarget.Location = new System.Drawing.Point(179, 67);
            this.buttonCalculateGateTarget.Name = "buttonCalculateGateTarget";
            this.buttonCalculateGateTarget.Size = new System.Drawing.Size(22, 22);
            this.buttonCalculateGateTarget.TabIndex = 13;
            this.buttonCalculateGateTarget.UseVisualStyleBackColor = true;
            this.buttonCalculateGateTarget.Click += new System.EventHandler(this.buttonCalculateGateTarget_Click);
            // 
            // buttonEditGateTarget
            // 
            this.buttonEditGateTarget.Image = global::SRVTracker.Properties.Resources.Edit_16x;
            this.buttonEditGateTarget.Location = new System.Drawing.Point(221, 67);
            this.buttonEditGateTarget.Name = "buttonEditGateTarget";
            this.buttonEditGateTarget.Size = new System.Drawing.Size(22, 22);
            this.buttonEditGateTarget.TabIndex = 12;
            this.buttonEditGateTarget.UseVisualStyleBackColor = true;
            this.buttonEditGateTarget.Click += new System.EventHandler(this.buttonEditGateTarget_Click);
            // 
            // buttonSetGateTargetToCurrentLocation
            // 
            this.buttonSetGateTargetToCurrentLocation.Image = global::SRVTracker.Properties.Resources.AddIndexer_16x;
            this.buttonSetGateTargetToCurrentLocation.Location = new System.Drawing.Point(242, 67);
            this.buttonSetGateTargetToCurrentLocation.Name = "buttonSetGateTargetToCurrentLocation";
            this.buttonSetGateTargetToCurrentLocation.Size = new System.Drawing.Size(22, 22);
            this.buttonSetGateTargetToCurrentLocation.TabIndex = 11;
            this.toolTip1.SetToolTip(this.buttonSetGateTargetToCurrentLocation, "Add current location as gate marker 2");
            this.buttonSetGateTargetToCurrentLocation.UseVisualStyleBackColor = true;
            this.buttonSetGateTargetToCurrentLocation.Click += new System.EventHandler(this.buttonSetGateTargetToCurrentLocation_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Target:";
            // 
            // comboBoxGateTarget
            // 
            this.comboBoxGateTarget.Enabled = false;
            this.comboBoxGateTarget.FormattingEnabled = true;
            this.comboBoxGateTarget.Location = new System.Drawing.Point(64, 68);
            this.comboBoxGateTarget.Name = "comboBoxGateTarget";
            this.comboBoxGateTarget.Size = new System.Drawing.Size(109, 21);
            this.comboBoxGateTarget.TabIndex = 9;
            // 
            // buttonEditGateMarker2
            // 
            this.buttonEditGateMarker2.Image = global::SRVTracker.Properties.Resources.Edit_16x;
            this.buttonEditGateMarker2.Location = new System.Drawing.Point(221, 41);
            this.buttonEditGateMarker2.Name = "buttonEditGateMarker2";
            this.buttonEditGateMarker2.Size = new System.Drawing.Size(22, 22);
            this.buttonEditGateMarker2.TabIndex = 7;
            this.buttonEditGateMarker2.UseVisualStyleBackColor = true;
            this.buttonEditGateMarker2.Click += new System.EventHandler(this.buttonEditGateMarker2_Click);
            // 
            // buttonEditGateMarker1
            // 
            this.buttonEditGateMarker1.Image = global::SRVTracker.Properties.Resources.Edit_16x;
            this.buttonEditGateMarker1.Location = new System.Drawing.Point(221, 13);
            this.buttonEditGateMarker1.Name = "buttonEditGateMarker1";
            this.buttonEditGateMarker1.Size = new System.Drawing.Size(22, 22);
            this.buttonEditGateMarker1.TabIndex = 6;
            this.buttonEditGateMarker1.UseVisualStyleBackColor = true;
            this.buttonEditGateMarker1.Click += new System.EventHandler(this.buttonEditGateMarker1_Click);
            // 
            // buttonSetGateLocation2ToCurrentLocation
            // 
            this.buttonSetGateLocation2ToCurrentLocation.Image = global::SRVTracker.Properties.Resources.AddIndexer_16x;
            this.buttonSetGateLocation2ToCurrentLocation.Location = new System.Drawing.Point(242, 41);
            this.buttonSetGateLocation2ToCurrentLocation.Name = "buttonSetGateLocation2ToCurrentLocation";
            this.buttonSetGateLocation2ToCurrentLocation.Size = new System.Drawing.Size(22, 22);
            this.buttonSetGateLocation2ToCurrentLocation.TabIndex = 5;
            this.toolTip1.SetToolTip(this.buttonSetGateLocation2ToCurrentLocation, "Add current location as gate marker 2");
            this.buttonSetGateLocation2ToCurrentLocation.UseVisualStyleBackColor = true;
            this.buttonSetGateLocation2ToCurrentLocation.Click += new System.EventHandler(this.buttonSetGateLocation2ToCurrentLocation_Click);
            // 
            // buttonSetGateLocation1ToCurrentLocation
            // 
            this.buttonSetGateLocation1ToCurrentLocation.Image = global::SRVTracker.Properties.Resources.AddIndexer_16x;
            this.buttonSetGateLocation1ToCurrentLocation.Location = new System.Drawing.Point(242, 13);
            this.buttonSetGateLocation1ToCurrentLocation.Name = "buttonSetGateLocation1ToCurrentLocation";
            this.buttonSetGateLocation1ToCurrentLocation.Size = new System.Drawing.Size(22, 22);
            this.buttonSetGateLocation1ToCurrentLocation.TabIndex = 4;
            this.toolTip1.SetToolTip(this.buttonSetGateLocation1ToCurrentLocation, "Add current location as gate marker 1");
            this.buttonSetGateLocation1ToCurrentLocation.UseVisualStyleBackColor = true;
            this.buttonSetGateLocation1ToCurrentLocation.Click += new System.EventHandler(this.buttonSetGateLocation1ToCurrentLocation_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Marker 2:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Marker 1:";
            // 
            // comboBoxGateLocation2
            // 
            this.comboBoxGateLocation2.Enabled = false;
            this.comboBoxGateLocation2.FormattingEnabled = true;
            this.comboBoxGateLocation2.Location = new System.Drawing.Point(64, 41);
            this.comboBoxGateLocation2.Name = "comboBoxGateLocation2";
            this.comboBoxGateLocation2.Size = new System.Drawing.Size(130, 21);
            this.comboBoxGateLocation2.TabIndex = 1;
            // 
            // comboBoxGateLocation1
            // 
            this.comboBoxGateLocation1.Enabled = false;
            this.comboBoxGateLocation1.FormattingEnabled = true;
            this.comboBoxGateLocation1.Location = new System.Drawing.Point(64, 14);
            this.comboBoxGateLocation1.Name = "comboBoxGateLocation1";
            this.comboBoxGateLocation1.Size = new System.Drawing.Size(130, 21);
            this.comboBoxGateLocation1.TabIndex = 0;
            // 
            // groupBoxBasic
            // 
            this.groupBoxBasic.Controls.Add(this.label11);
            this.groupBoxBasic.Controls.Add(this.comboBoxBasicLocation);
            this.groupBoxBasic.Controls.Add(this.buttonEditBasicLocation);
            this.groupBoxBasic.Controls.Add(this.numericUpDownRadius);
            this.groupBoxBasic.Controls.Add(this.checkBoxAllowPassing);
            this.groupBoxBasic.Controls.Add(this.label6);
            this.groupBoxBasic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBoxBasic.Location = new System.Drawing.Point(32, 77);
            this.groupBoxBasic.Name = "groupBoxBasic";
            this.groupBoxBasic.Size = new System.Drawing.Size(270, 92);
            this.groupBoxBasic.TabIndex = 14;
            this.groupBoxBasic.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Location:";
            // 
            // comboBoxBasicLocation
            // 
            this.comboBoxBasicLocation.Enabled = false;
            this.comboBoxBasicLocation.FormattingEnabled = true;
            this.comboBoxBasicLocation.Location = new System.Drawing.Point(74, 20);
            this.comboBoxBasicLocation.Name = "comboBoxBasicLocation";
            this.comboBoxBasicLocation.Size = new System.Drawing.Size(150, 21);
            this.comboBoxBasicLocation.TabIndex = 14;
            // 
            // buttonEditBasicLocation
            // 
            this.buttonEditBasicLocation.Image = global::SRVTracker.Properties.Resources.Edit_16x;
            this.buttonEditBasicLocation.Location = new System.Drawing.Point(230, 19);
            this.buttonEditBasicLocation.Name = "buttonEditBasicLocation";
            this.buttonEditBasicLocation.Size = new System.Drawing.Size(22, 22);
            this.buttonEditBasicLocation.TabIndex = 13;
            this.buttonEditBasicLocation.UseVisualStyleBackColor = true;
            this.buttonEditBasicLocation.Click += new System.EventHandler(this.buttonEditBasicLocation_Click);
            // 
            // numericUpDownRadius
            // 
            this.numericUpDownRadius.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownRadius.Location = new System.Drawing.Point(74, 47);
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
            // checkBoxAllowPassing
            // 
            this.checkBoxAllowPassing.AutoSize = true;
            this.checkBoxAllowPassing.Location = new System.Drawing.Point(173, 50);
            this.checkBoxAllowPassing.Name = "checkBoxAllowPassing";
            this.checkBoxAllowPassing.Size = new System.Drawing.Size(90, 17);
            this.checkBoxAllowPassing.TabIndex = 11;
            this.checkBoxAllowPassing.Text = "Allow passing";
            this.toolTip1.SetToolTip(this.checkBoxAllowPassing, resources.GetString("checkBoxAllowPassing.ToolTip"));
            this.checkBoxAllowPassing.UseVisualStyleBackColor = true;
            this.checkBoxAllowPassing.CheckedChanged += new System.EventHandler(this.checkBoxAllowPassing_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Radius:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxRouteName);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 49);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Route name";
            // 
            // textBoxRouteName
            // 
            this.textBoxRouteName.Location = new System.Drawing.Point(6, 19);
            this.textBoxRouteName.Name = "textBoxRouteName";
            this.textBoxRouteName.Size = new System.Drawing.Size(331, 20);
            this.textBoxRouteName.TabIndex = 0;
            this.textBoxRouteName.TextChanged += new System.EventHandler(this.textBoxRouteName_TextChanged);
            // 
            // checkBoxPlayIncudeDirection
            // 
            this.checkBoxPlayIncudeDirection.AutoSize = true;
            this.checkBoxPlayIncudeDirection.Location = new System.Drawing.Point(9, 19);
            this.checkBoxPlayIncudeDirection.Name = "checkBoxPlayIncudeDirection";
            this.checkBoxPlayIncudeDirection.Size = new System.Drawing.Size(129, 17);
            this.checkBoxPlayIncudeDirection.TabIndex = 0;
            this.checkBoxPlayIncudeDirection.Text = "Include direction hints";
            this.toolTip1.SetToolTip(this.checkBoxPlayIncudeDirection, "If enabled, direction indication is added to the\r\nwaypoint name to show which way" +
        " you will\r\nneed to turn at that waypoint");
            this.checkBoxPlayIncudeDirection.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableAudio
            // 
            this.checkBoxEnableAudio.AutoSize = true;
            this.checkBoxEnableAudio.Location = new System.Drawing.Point(6, 19);
            this.checkBoxEnableAudio.Name = "checkBoxEnableAudio";
            this.checkBoxEnableAudio.Size = new System.Drawing.Size(65, 17);
            this.checkBoxEnableAudio.TabIndex = 1;
            this.checkBoxEnableAudio.Text = "Enabled";
            this.toolTip1.SetToolTip(this.checkBoxEnableAudio, "Enable or disable audio feedback while traveling a route");
            this.checkBoxEnableAudio.UseVisualStyleBackColor = true;
            this.checkBoxEnableAudio.CheckedChanged += new System.EventHandler(this.checkBoxEnableAudio_CheckedChanged);
            // 
            // checkBoxTimeTrial
            // 
            this.checkBoxTimeTrial.AutoSize = true;
            this.checkBoxTimeTrial.Location = new System.Drawing.Point(9, 42);
            this.checkBoxTimeTrial.Name = "checkBoxTimeTrial";
            this.checkBoxTimeTrial.Size = new System.Drawing.Size(68, 17);
            this.checkBoxTimeTrial.TabIndex = 2;
            this.checkBoxTimeTrial.Text = "Time trial";
            this.toolTip1.SetToolTip(this.checkBoxTimeTrial, "If enabled, the telemetry window will be displayed\r\nand timer will start on first" +
        " movement.");
            this.checkBoxTimeTrial.UseVisualStyleBackColor = true;
            this.checkBoxTimeTrial.CheckedChanged += new System.EventHandler(this.checkBoxTimeTrial_CheckedChanged);
            // 
            // buttonStop
            // 
            this.buttonStop.Image = global::SRVTracker.Properties.Resources.Stop_16x;
            this.buttonStop.Location = new System.Drawing.Point(93, 245);
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
            this.buttonStartRecording.Location = new System.Drawing.Point(53, 245);
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
            this.buttonPlay.Location = new System.Drawing.Point(13, 245);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(34, 23);
            this.buttonPlay.TabIndex = 11;
            this.toolTip1.SetToolTip(this.buttonPlay, "Play route");
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // checkBoxScreenshot
            // 
            this.checkBoxScreenshot.AutoSize = true;
            this.checkBoxScreenshot.Location = new System.Drawing.Point(9, 65);
            this.checkBoxScreenshot.Name = "checkBoxScreenshot";
            this.checkBoxScreenshot.Size = new System.Drawing.Size(152, 17);
            this.checkBoxScreenshot.TabIndex = 3;
            this.checkBoxScreenshot.Text = "Screenshot each waypoint";
            this.toolTip1.SetToolTip(this.checkBoxScreenshot, "Take a screenshot at each waypoint.");
            this.checkBoxScreenshot.UseVisualStyleBackColor = true;
            // 
            // numericUpDownTotalLaps
            // 
            this.numericUpDownTotalLaps.Location = new System.Drawing.Point(83, 41);
            this.numericUpDownTotalLaps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTotalLaps.Name = "numericUpDownTotalLaps";
            this.numericUpDownTotalLaps.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownTotalLaps.TabIndex = 4;
            this.toolTip1.SetToolTip(this.numericUpDownTotalLaps, "If set to a value higher than 1, course will repeat for the given number of times" +
        "");
            this.numericUpDownTotalLaps.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.checkBoxScreenshot);
            this.groupBox3.Controls.Add(this.checkBoxTimeTrial);
            this.groupBox3.Controls.Add(this.numericUpDownTotalLaps);
            this.groupBox3.Controls.Add(this.checkBoxPlayIncudeDirection);
            this.groupBox3.Location = new System.Drawing.Point(186, 61);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(163, 90);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tracking options";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(130, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "laps";
            // 
            // groupBoxAudioSettings
            // 
            this.groupBoxAudioSettings.Controls.Add(this.comboBoxChooseSound);
            this.groupBoxAudioSettings.Controls.Add(this.listBoxAudioEvents);
            this.groupBoxAudioSettings.Controls.Add(this.checkBoxEnableAudio);
            this.groupBoxAudioSettings.Location = new System.Drawing.Point(3, 3);
            this.groupBoxAudioSettings.Name = "groupBoxAudioSettings";
            this.groupBoxAudioSettings.Size = new System.Drawing.Size(348, 253);
            this.groupBoxAudioSettings.TabIndex = 17;
            this.groupBoxAudioSettings.TabStop = false;
            this.groupBoxAudioSettings.Text = "Audio Settings";
            // 
            // comboBoxChooseSound
            // 
            this.comboBoxChooseSound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChooseSound.Enabled = false;
            this.comboBoxChooseSound.FormattingEnabled = true;
            this.comboBoxChooseSound.Location = new System.Drawing.Point(6, 226);
            this.comboBoxChooseSound.Name = "comboBoxChooseSound";
            this.comboBoxChooseSound.Size = new System.Drawing.Size(336, 21);
            this.comboBoxChooseSound.TabIndex = 3;
            this.comboBoxChooseSound.SelectedIndexChanged += new System.EventHandler(this.comboBoxChooseSound_SelectedIndexChanged);
            // 
            // listBoxAudioEvents
            // 
            this.listBoxAudioEvents.FormattingEnabled = true;
            this.listBoxAudioEvents.Location = new System.Drawing.Point(6, 42);
            this.listBoxAudioEvents.Name = "listBoxAudioEvents";
            this.listBoxAudioEvents.Size = new System.Drawing.Size(336, 173);
            this.listBoxAudioEvents.TabIndex = 2;
            this.listBoxAudioEvents.SelectedIndexChanged += new System.EventHandler(this.listBoxAudioEvents_SelectedIndexChanged);
            // 
            // circumnavigationContextMenuStrip
            // 
            this.circumnavigationContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNorthPoleToolStripMenuItem,
            this.addSouthPoleToolStripMenuItem,
            this.addOriginToolStripMenuItem,
            this.addPrimeMeridianToolStripMenuItem,
            this.addEquatorToolStripMenuItem,
            this.generateRouteToolStripMenuItem});
            this.circumnavigationContextMenuStrip.Name = "circumnavigationContextMenuStrip";
            this.circumnavigationContextMenuStrip.Size = new System.Drawing.Size(259, 136);
            this.circumnavigationContextMenuStrip.Text = "Circumnavigation";
            // 
            // addNorthPoleToolStripMenuItem
            // 
            this.addNorthPoleToolStripMenuItem.Name = "addNorthPoleToolStripMenuItem";
            this.addNorthPoleToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.addNorthPoleToolStripMenuItem.Text = "Add North Pole (90°, 0°)";
            this.addNorthPoleToolStripMenuItem.Click += new System.EventHandler(this.addNorthPoleToolStripMenuItem_Click);
            // 
            // addSouthPoleToolStripMenuItem
            // 
            this.addSouthPoleToolStripMenuItem.Name = "addSouthPoleToolStripMenuItem";
            this.addSouthPoleToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.addSouthPoleToolStripMenuItem.Text = "Add South Pole (-90°, 0°)";
            this.addSouthPoleToolStripMenuItem.Click += new System.EventHandler(this.addSouthPoleToolStripMenuItem_Click);
            // 
            // addOriginToolStripMenuItem
            // 
            this.addOriginToolStripMenuItem.Name = "addOriginToolStripMenuItem";
            this.addOriginToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.addOriginToolStripMenuItem.Text = "Add Origin (0°, 0°)";
            this.addOriginToolStripMenuItem.Click += new System.EventHandler(this.addOriginToolStripMenuItem_Click);
            // 
            // addPrimeMeridianToolStripMenuItem
            // 
            this.addPrimeMeridianToolStripMenuItem.Name = "addPrimeMeridianToolStripMenuItem";
            this.addPrimeMeridianToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.addPrimeMeridianToolStripMenuItem.Text = "Add Prime Meridian (closest point)";
            this.addPrimeMeridianToolStripMenuItem.Click += new System.EventHandler(this.addPrimeMeridianToolStripMenuItem_Click);
            // 
            // addEquatorToolStripMenuItem
            // 
            this.addEquatorToolStripMenuItem.Name = "addEquatorToolStripMenuItem";
            this.addEquatorToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.addEquatorToolStripMenuItem.Text = "Add Equator (closest point)";
            this.addEquatorToolStripMenuItem.Click += new System.EventHandler(this.addEquatorToolStripMenuItem_Click);
            // 
            // generateRouteToolStripMenuItem
            // 
            this.generateRouteToolStripMenuItem.Enabled = false;
            this.generateRouteToolStripMenuItem.Name = "generateRouteToolStripMenuItem";
            this.generateRouteToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.generateRouteToolStripMenuItem.Text = "Generate Route...";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageWaypoints);
            this.tabControl1.Controls.Add(this.tabPageRoute);
            this.tabControl1.Controls.Add(this.tabPageAudio);
            this.tabControl1.Location = new System.Drawing.Point(289, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(363, 285);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPageRoute
            // 
            this.tabPageRoute.Controls.Add(this.groupBox3);
            this.tabPageRoute.Controls.Add(this.groupBox4);
            this.tabPageRoute.Controls.Add(this.groupBox1);
            this.tabPageRoute.Location = new System.Drawing.Point(4, 22);
            this.tabPageRoute.Name = "tabPageRoute";
            this.tabPageRoute.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRoute.Size = new System.Drawing.Size(355, 259);
            this.tabPageRoute.TabIndex = 0;
            this.tabPageRoute.Text = "Route";
            this.tabPageRoute.UseVisualStyleBackColor = true;
            // 
            // tabPageWaypoints
            // 
            this.tabPageWaypoints.Controls.Add(this.groupBoxWaypointInfo);
            this.tabPageWaypoints.Location = new System.Drawing.Point(4, 22);
            this.tabPageWaypoints.Name = "tabPageWaypoints";
            this.tabPageWaypoints.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWaypoints.Size = new System.Drawing.Size(355, 259);
            this.tabPageWaypoints.TabIndex = 1;
            this.tabPageWaypoints.Text = "Waypoint Editor";
            this.tabPageWaypoints.UseVisualStyleBackColor = true;
            // 
            // tabPageAudio
            // 
            this.tabPageAudio.Controls.Add(this.groupBoxAudioSettings);
            this.tabPageAudio.Location = new System.Drawing.Point(4, 22);
            this.tabPageAudio.Name = "tabPageAudio";
            this.tabPageAudio.Size = new System.Drawing.Size(355, 259);
            this.tabPageAudio.TabIndex = 4;
            this.tabPageAudio.Text = "Audio";
            this.tabPageAudio.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(208, 245);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 20;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // FormRouter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 509);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonStartRecording);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormRouter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Route Recorder/Player";
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecordDistance)).EndInit();
            this.groupBoxWaypointInfo.ResumeLayout(false);
            this.groupBoxWaypointInfo.PerformLayout();
            this.groupBoxPolygon.ResumeLayout(false);
            this.groupBoxPolygon.PerformLayout();
            this.groupBoxAltitude.ResumeLayout(false);
            this.groupBoxAltitude.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinAltitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxAltitude)).EndInit();
            this.groupBoxGate.ResumeLayout(false);
            this.groupBoxGate.PerformLayout();
            this.groupBoxBasic.ResumeLayout(false);
            this.groupBoxBasic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalLaps)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBoxAudioSettings.ResumeLayout(false);
            this.groupBoxAudioSettings.PerformLayout();
            this.circumnavigationContextMenuStrip.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageRoute.ResumeLayout(false);
            this.tabPageWaypoints.ResumeLayout(false);
            this.tabPageAudio.ResumeLayout(false);
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
        private System.Windows.Forms.NumericUpDown numericUpDownMaxAltitude;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonMoveDown;
        private System.Windows.Forms.Button buttonMoveUp;
        private System.Windows.Forms.Button buttonDuplicateWaypoint;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxPlayIncudeDirection;
        private System.Windows.Forms.CheckBox checkBoxAllowPassing;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxWaypointType;
        private System.Windows.Forms.GroupBox groupBoxBasic;
        private System.Windows.Forms.CheckBox checkBoxEnableAudio;
        private System.Windows.Forms.GroupBox groupBoxAudioSettings;
        private System.Windows.Forms.ComboBox comboBoxChooseSound;
        private System.Windows.Forms.ListBox listBoxAudioEvents;
        private System.Windows.Forms.GroupBox groupBoxAltitude;
        private System.Windows.Forms.GroupBox groupBoxGate;
        private System.Windows.Forms.Button buttonSetGateLocation2ToCurrentLocation;
        private System.Windows.Forms.Button buttonSetGateLocation1ToCurrentLocation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxGateLocation2;
        private System.Windows.Forms.ComboBox comboBoxGateLocation1;
        private System.Windows.Forms.Button buttonReverseWaypointOrder;
        private System.Windows.Forms.Button buttonEditGateMarker2;
        private System.Windows.Forms.Button buttonEditGateMarker1;
        private System.Windows.Forms.Button buttonEditGateTarget;
        private System.Windows.Forms.Button buttonSetGateTargetToCurrentLocation;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxGateTarget;
        private System.Windows.Forms.Button buttonCalculateGateTarget;
        private System.Windows.Forms.Button buttonEditLocations;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxBasicLocation;
        private System.Windows.Forms.Button buttonEditBasicLocation;
        private System.Windows.Forms.Button buttonTargetGateTarget;
        private System.Windows.Forms.Button buttonTargetGateMarker2;
        private System.Windows.Forms.Button buttonTargetGateMarker1;
        private System.Windows.Forms.CheckBox checkBoxTimeTrial;
        private System.Windows.Forms.CheckBox checkBoxScreenshot;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericUpDownTotalLaps;
        private System.Windows.Forms.GroupBox groupBoxPolygon;
        private System.Windows.Forms.Button buttonPolygonMarkerAdd;
        private System.Windows.Forms.Button buttonPolygonMarkerDelete;
        private System.Windows.Forms.ListBox listBoxPolygonMarkers;
        private System.Windows.Forms.Button buttonPolygonTargetTarget;
        private System.Windows.Forms.Button buttonPolygonMarkerTarget;
        private System.Windows.Forms.Button buttonPolygonTargetCalculate;
        private System.Windows.Forms.Button buttonPolygonTargetEdit;
        private System.Windows.Forms.Button buttonPolygonTargetUseCurrentLocation;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxPolygonTarget;
        private System.Windows.Forms.Button buttonPolygonMarkerEdit;
        private System.Windows.Forms.Button buttonPolygonMarkerUseCurrentLocation;
        private System.Windows.Forms.Button buttonGenerateCircumnavigationWaypoints;
        private System.Windows.Forms.ContextMenuStrip circumnavigationContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addNorthPoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSouthPoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateRouteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addEquatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPrimeMeridianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addOriginToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageRoute;
        private System.Windows.Forms.TabPage tabPageWaypoints;
        private System.Windows.Forms.TabPage tabPageAudio;
        private System.Windows.Forms.Button buttonClose;
    }
}