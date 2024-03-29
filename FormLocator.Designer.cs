﻿namespace SRVTracker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLocator));
            this.groupBoxDestination = new System.Windows.Forms.GroupBox();
            this.checkBoxIncludeAltitudeInDistanceCalculations = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAltitude = new System.Windows.Forms.TextBox();
            this.textBoxLatitude = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLongitude = new System.Windows.Forms.TextBox();
            this.groupBoxBearing = new System.Windows.Forms.GroupBox();
            this.labelDistance = new System.Windows.Forms.Label();
            this.labelHeading = new System.Windows.Forms.Label();
            this.buttonShowHideTarget = new System.Windows.Forms.Button();
            this.buttonPlayers = new System.Windows.Forms.Button();
            this.groupBoxOtherCommanders = new System.Windows.Forms.GroupBox();
            this.checkBoxTrackClosest = new System.Windows.Forms.CheckBox();
            this.listBoxCommanders = new System.Windows.Forms.ListBox();
            this.buttonTrackCommander = new System.Windows.Forms.Button();
            this.checkBoxEnableVRLocator = new System.Windows.Forms.CheckBox();
            this.comboBoxLocation = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonOpenLocationEditor = new System.Windows.Forms.Button();
            this.pictureBoxMoveForm = new System.Windows.Forms.PictureBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonAlwaysOnTop = new System.Windows.Forms.Button();
            this.buttonUseCurrentLocation = new System.Windows.Forms.Button();
            this.locatorHUD1 = new SRVTracker.LocatorHUD();
            this.groupBoxDestination.SuspendLayout();
            this.groupBoxBearing.SuspendLayout();
            this.groupBoxOtherCommanders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMoveForm)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxDestination
            // 
            this.groupBoxDestination.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBoxDestination.Controls.Add(this.checkBoxIncludeAltitudeInDistanceCalculations);
            this.groupBoxDestination.Controls.Add(this.label2);
            this.groupBoxDestination.Controls.Add(this.textBoxAltitude);
            this.groupBoxDestination.Controls.Add(this.textBoxLatitude);
            this.groupBoxDestination.Controls.Add(this.label1);
            this.groupBoxDestination.Controls.Add(this.textBoxLongitude);
            this.groupBoxDestination.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBoxDestination.ForeColor = System.Drawing.Color.White;
            this.groupBoxDestination.Location = new System.Drawing.Point(6, 66);
            this.groupBoxDestination.Name = "groupBoxDestination";
            this.groupBoxDestination.Size = new System.Drawing.Size(331, 64);
            this.groupBoxDestination.TabIndex = 0;
            this.groupBoxDestination.TabStop = false;
            this.groupBoxDestination.Text = "Target location";
            // 
            // checkBoxIncludeAltitudeInDistanceCalculations
            // 
            this.checkBoxIncludeAltitudeInDistanceCalculations.AutoSize = true;
            this.checkBoxIncludeAltitudeInDistanceCalculations.Location = new System.Drawing.Point(223, 15);
            this.checkBoxIncludeAltitudeInDistanceCalculations.Name = "checkBoxIncludeAltitudeInDistanceCalculations";
            this.checkBoxIncludeAltitudeInDistanceCalculations.Size = new System.Drawing.Size(61, 17);
            this.checkBoxIncludeAltitudeInDistanceCalculations.TabIndex = 6;
            this.checkBoxIncludeAltitudeInDistanceCalculations.Text = "Altitude";
            this.toolTip1.SetToolTip(this.checkBoxIncludeAltitudeInDistanceCalculations, "When selected, distance calculations take altitude into account.\r\nBy default, alt" +
        "itude is ignored and ground distance is calculated.");
            this.checkBoxIncludeAltitudeInDistanceCalculations.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Longitude";
            // 
            // textBoxAltitude
            // 
            this.textBoxAltitude.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxAltitude.ForeColor = System.Drawing.Color.White;
            this.textBoxAltitude.Location = new System.Drawing.Point(223, 32);
            this.textBoxAltitude.Name = "textBoxAltitude";
            this.textBoxAltitude.ReadOnly = true;
            this.textBoxAltitude.Size = new System.Drawing.Size(102, 20);
            this.textBoxAltitude.TabIndex = 3;
            this.toolTip1.SetToolTip(this.textBoxAltitude, "The target\'s altitude");
            // 
            // textBoxLatitude
            // 
            this.textBoxLatitude.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxLatitude.ForeColor = System.Drawing.Color.White;
            this.textBoxLatitude.Location = new System.Drawing.Point(9, 32);
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
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Latitude";
            // 
            // textBoxLongitude
            // 
            this.textBoxLongitude.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxLongitude.ForeColor = System.Drawing.Color.White;
            this.textBoxLongitude.Location = new System.Drawing.Point(115, 32);
            this.textBoxLongitude.Name = "textBoxLongitude";
            this.textBoxLongitude.ReadOnly = true;
            this.textBoxLongitude.Size = new System.Drawing.Size(100, 20);
            this.textBoxLongitude.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textBoxLongitude, "The target\'s longitude");
            // 
            // groupBoxBearing
            // 
            this.groupBoxBearing.Controls.Add(this.labelDistance);
            this.groupBoxBearing.Controls.Add(this.labelHeading);
            this.groupBoxBearing.Location = new System.Drawing.Point(457, 208);
            this.groupBoxBearing.Name = "groupBoxBearing";
            this.groupBoxBearing.Size = new System.Drawing.Size(331, 65);
            this.groupBoxBearing.TabIndex = 1;
            this.groupBoxBearing.TabStop = false;
            this.groupBoxBearing.Text = "Bearing (target not set)";
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
            // buttonShowHideTarget
            // 
            this.buttonShowHideTarget.BackColor = System.Drawing.Color.DimGray;
            this.buttonShowHideTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShowHideTarget.ForeColor = System.Drawing.Color.Black;
            this.buttonShowHideTarget.Location = new System.Drawing.Point(247, 0);
            this.buttonShowHideTarget.Name = "buttonShowHideTarget";
            this.buttonShowHideTarget.Size = new System.Drawing.Size(13, 13);
            this.buttonShowHideTarget.TabIndex = 2;
            this.toolTip1.SetToolTip(this.buttonShowHideTarget, "Shrink/expand the window");
            this.buttonShowHideTarget.UseVisualStyleBackColor = false;
            this.buttonShowHideTarget.Click += new System.EventHandler(this.buttonShowHideTarget_Click);
            // 
            // buttonPlayers
            // 
            this.buttonPlayers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPlayers.Location = new System.Drawing.Point(262, 137);
            this.buttonPlayers.Name = "buttonPlayers";
            this.buttonPlayers.Size = new System.Drawing.Size(75, 23);
            this.buttonPlayers.TabIndex = 8;
            this.buttonPlayers.Text = "Commander";
            this.toolTip1.SetToolTip(this.buttonPlayers, "Track other commander (other commander must be uploading status)");
            this.buttonPlayers.UseVisualStyleBackColor = true;
            this.buttonPlayers.Click += new System.EventHandler(this.buttonPlayers_Click);
            // 
            // groupBoxOtherCommanders
            // 
            this.groupBoxOtherCommanders.Controls.Add(this.checkBoxTrackClosest);
            this.groupBoxOtherCommanders.Controls.Add(this.listBoxCommanders);
            this.groupBoxOtherCommanders.Controls.Add(this.buttonTrackCommander);
            this.groupBoxOtherCommanders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBoxOtherCommanders.ForeColor = System.Drawing.Color.White;
            this.groupBoxOtherCommanders.Location = new System.Drawing.Point(349, 0);
            this.groupBoxOtherCommanders.Name = "groupBoxOtherCommanders";
            this.groupBoxOtherCommanders.Size = new System.Drawing.Size(200, 162);
            this.groupBoxOtherCommanders.TabIndex = 9;
            this.groupBoxOtherCommanders.TabStop = false;
            this.groupBoxOtherCommanders.Text = "Other Commanders";
            // 
            // checkBoxTrackClosest
            // 
            this.checkBoxTrackClosest.AutoSize = true;
            this.checkBoxTrackClosest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxTrackClosest.Location = new System.Drawing.Point(6, 135);
            this.checkBoxTrackClosest.Name = "checkBoxTrackClosest";
            this.checkBoxTrackClosest.Size = new System.Drawing.Size(87, 17);
            this.checkBoxTrackClosest.TabIndex = 2;
            this.checkBoxTrackClosest.Text = "Track closest";
            this.toolTip1.SetToolTip(this.checkBoxTrackClosest, "If selected, will track closest commander to current location (may affect CPU usa" +
        "ge)");
            this.checkBoxTrackClosest.UseVisualStyleBackColor = true;
            // 
            // listBoxCommanders
            // 
            this.listBoxCommanders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listBoxCommanders.ForeColor = System.Drawing.Color.White;
            this.listBoxCommanders.FormattingEnabled = true;
            this.listBoxCommanders.Location = new System.Drawing.Point(6, 19);
            this.listBoxCommanders.Name = "listBoxCommanders";
            this.listBoxCommanders.Size = new System.Drawing.Size(188, 108);
            this.listBoxCommanders.TabIndex = 1;
            this.toolTip1.SetToolTip(this.listBoxCommanders, "A list of other commanders currently (or recently) online");
            this.listBoxCommanders.SelectedIndexChanged += new System.EventHandler(this.listBoxCommanders_SelectedIndexChanged);
            // 
            // buttonTrackCommander
            // 
            this.buttonTrackCommander.Enabled = false;
            this.buttonTrackCommander.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTrackCommander.ForeColor = System.Drawing.Color.White;
            this.buttonTrackCommander.Location = new System.Drawing.Point(119, 132);
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
            this.checkBoxEnableVRLocator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxEnableVRLocator.Location = new System.Drawing.Point(47, 141);
            this.checkBoxEnableVRLocator.Name = "checkBoxEnableVRLocator";
            this.checkBoxEnableVRLocator.Size = new System.Drawing.Size(38, 17);
            this.checkBoxEnableVRLocator.TabIndex = 10;
            this.checkBoxEnableVRLocator.Text = "VR";
            this.toolTip1.SetToolTip(this.checkBoxEnableVRLocator, "Enable/disable the VR bearing/distance display");
            this.checkBoxEnableVRLocator.UseVisualStyleBackColor = true;
            this.checkBoxEnableVRLocator.CheckedChanged += new System.EventHandler(this.checkBoxEnableVRLocator_CheckedChanged);
            // 
            // comboBoxLocation
            // 
            this.comboBoxLocation.BackColor = System.Drawing.Color.Black;
            this.comboBoxLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxLocation.ForeColor = System.Drawing.Color.White;
            this.comboBoxLocation.FormattingEnabled = true;
            this.comboBoxLocation.Location = new System.Drawing.Point(94, 138);
            this.comboBoxLocation.Name = "comboBoxLocation";
            this.comboBoxLocation.Size = new System.Drawing.Size(127, 21);
            this.comboBoxLocation.TabIndex = 11;
            this.toolTip1.SetToolTip(this.comboBoxLocation, "Select or add a location to target");
            this.comboBoxLocation.SelectedIndexChanged += new System.EventHandler(this.comboBoxLocation_SelectedIndexChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 1000;
            this.toolTip1.ReshowDelay = 1000;
            // 
            // buttonOpenLocationEditor
            // 
            this.buttonOpenLocationEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpenLocationEditor.Image = global::SRVTracker.Properties.Resources.AddressEditor_16x;
            this.buttonOpenLocationEditor.Location = new System.Drawing.Point(221, 137);
            this.buttonOpenLocationEditor.Name = "buttonOpenLocationEditor";
            this.buttonOpenLocationEditor.Size = new System.Drawing.Size(35, 23);
            this.buttonOpenLocationEditor.TabIndex = 29;
            this.toolTip1.SetToolTip(this.buttonOpenLocationEditor, "Open the location editor");
            this.buttonOpenLocationEditor.UseVisualStyleBackColor = true;
            this.buttonOpenLocationEditor.Click += new System.EventHandler(this.buttonOpenLocationEditor_Click);
            // 
            // pictureBoxMoveForm
            // 
            this.pictureBoxMoveForm.BackgroundImage = global::SRVTracker.Properties.Resources.Move__wb_;
            this.pictureBoxMoveForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxMoveForm.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pictureBoxMoveForm.Location = new System.Drawing.Point(272, 30);
            this.pictureBoxMoveForm.Name = "pictureBoxMoveForm";
            this.pictureBoxMoveForm.Size = new System.Drawing.Size(65, 30);
            this.pictureBoxMoveForm.TabIndex = 28;
            this.pictureBoxMoveForm.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxMoveForm, "Click and drag here to move the form");
            this.pictureBoxMoveForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMoveForm_MouseDown);
            // 
            // buttonClose
            // 
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Image = global::SRVTracker.Properties.Resources.Close_red_16x;
            this.buttonClose.Location = new System.Drawing.Point(306, 1);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(31, 23);
            this.buttonClose.TabIndex = 13;
            this.toolTip1.SetToolTip(this.buttonClose, "Close the locator");
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonAlwaysOnTop
            // 
            this.buttonAlwaysOnTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAlwaysOnTop.Image = global::SRVTracker.Properties.Resources.PinnedItem_16x;
            this.buttonAlwaysOnTop.Location = new System.Drawing.Point(272, 1);
            this.buttonAlwaysOnTop.Name = "buttonAlwaysOnTop";
            this.buttonAlwaysOnTop.Size = new System.Drawing.Size(28, 23);
            this.buttonAlwaysOnTop.TabIndex = 12;
            this.toolTip1.SetToolTip(this.buttonAlwaysOnTop, "Pin window topmost");
            this.buttonAlwaysOnTop.UseVisualStyleBackColor = true;
            this.buttonAlwaysOnTop.Click += new System.EventHandler(this.buttonAlwaysOnTop_Click);
            // 
            // buttonUseCurrentLocation
            // 
            this.buttonUseCurrentLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUseCurrentLocation.Image = global::SRVTracker.Properties.Resources.Home_16x;
            this.buttonUseCurrentLocation.Location = new System.Drawing.Point(6, 137);
            this.buttonUseCurrentLocation.Name = "buttonUseCurrentLocation";
            this.buttonUseCurrentLocation.Size = new System.Drawing.Size(35, 23);
            this.buttonUseCurrentLocation.TabIndex = 6;
            this.toolTip1.SetToolTip(this.buttonUseCurrentLocation, "Set as home (track current location)");
            this.buttonUseCurrentLocation.UseVisualStyleBackColor = true;
            this.buttonUseCurrentLocation.Click += new System.EventHandler(this.buttonUseCurrentLocation_Click);
            // 
            // locatorHUD1
            // 
            this.locatorHUD1.BackColor = System.Drawing.Color.Black;
            this.locatorHUD1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.locatorHUD1.Location = new System.Drawing.Point(0, 0);
            this.locatorHUD1.Name = "locatorHUD1";
            this.locatorHUD1.ShowSpeedIndicator = true;
            this.locatorHUD1.Size = new System.Drawing.Size(260, 60);
            this.locatorHUD1.TabIndex = 3;
            // 
            // FormLocator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(562, 174);
            this.Controls.Add(this.buttonOpenLocationEditor);
            this.Controls.Add(this.pictureBoxMoveForm);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonAlwaysOnTop);
            this.Controls.Add(this.buttonShowHideTarget);
            this.Controls.Add(this.locatorHUD1);
            this.Controls.Add(this.comboBoxLocation);
            this.Controls.Add(this.checkBoxEnableVRLocator);
            this.Controls.Add(this.groupBoxOtherCommanders);
            this.Controls.Add(this.buttonPlayers);
            this.Controls.Add(this.groupBoxBearing);
            this.Controls.Add(this.buttonUseCurrentLocation);
            this.Controls.Add(this.groupBoxDestination);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(562, 174);
            this.MinimizeBox = false;
            this.Name = "FormLocator";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Waypoint Locator";
            this.toolTip1.SetToolTip(this, "Click and drag the form background to move");
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLocator_FormClosing);
            this.groupBoxDestination.ResumeLayout(false);
            this.groupBoxDestination.PerformLayout();
            this.groupBoxBearing.ResumeLayout(false);
            this.groupBoxBearing.PerformLayout();
            this.groupBoxOtherCommanders.ResumeLayout(false);
            this.groupBoxOtherCommanders.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMoveForm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxDestination;
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
        private System.Windows.Forms.GroupBox groupBoxOtherCommanders;
        private System.Windows.Forms.ListBox listBoxCommanders;
        private System.Windows.Forms.Button buttonTrackCommander;
        private System.Windows.Forms.CheckBox checkBoxEnableVRLocator;
        private System.Windows.Forms.ComboBox comboBoxLocation;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBoxTrackClosest;
        private LocatorHUD locatorHUD1;
        private System.Windows.Forms.Button buttonAlwaysOnTop;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.PictureBox pictureBoxMoveForm;
        private System.Windows.Forms.CheckBox checkBoxIncludeAltitudeInDistanceCalculations;
        private System.Windows.Forms.Button buttonOpenLocationEditor;
    }
}