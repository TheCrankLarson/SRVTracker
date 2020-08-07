namespace SRVTracker
{
    partial class FormTracker
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBrowseStatusFile = new System.Windows.Forms.Button();
            this.textBoxStatusFile = new System.Windows.Forms.TextBox();
            this.statusFileWatcher = new System.IO.FileSystemWatcher();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxAltitude = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLatitude = new System.Windows.Forms.TextBox();
            this.textBoxLongitude = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxHeading = new System.Windows.Forms.TextBox();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.buttonTest = new System.Windows.Forms.Button();
            this.labelLastUpdateTime = new System.Windows.Forms.Label();
            this.checkBoxAutoScroll = new System.Windows.Forms.CheckBox();
            this.checkBoxSendLocationOnly = new System.Windows.Forms.CheckBox();
            this.checkBoxShowLive = new System.Windows.Forms.CheckBox();
            this.textBoxClientId = new System.Windows.Forms.TextBox();
            this.textBoxSaveFile = new System.Windows.Forms.TextBox();
            this.checkBoxSaveToFile = new System.Windows.Forms.CheckBox();
            this.textBoxUploadServer = new System.Windows.Forms.TextBox();
            this.checkBoxUpload = new System.Windows.Forms.CheckBox();
            this.buttonLocator = new System.Windows.Forms.Button();
            this.buttonRoutePlanner = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonShowConfig = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.radioButtonUseCustomServer = new System.Windows.Forms.RadioButton();
            this.radioButtonUseDefaultServer = new System.Windows.Forms.RadioButton();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.radioButtonWatchStatusFile = new System.Windows.Forms.RadioButton();
            this.radioButtonUseTimer = new System.Windows.Forms.RadioButton();
            this.buttonRaceTracker = new System.Windows.Forms.Button();
            this.checkBoxTrack = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPlanetRadius = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusFileWatcher)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBrowseStatusFile);
            this.groupBox1.Controls.Add(this.textBoxStatusFile);
            this.groupBox1.Location = new System.Drawing.Point(281, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 48);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status.json location";
            // 
            // buttonBrowseStatusFile
            // 
            this.buttonBrowseStatusFile.Location = new System.Drawing.Point(371, 17);
            this.buttonBrowseStatusFile.Name = "buttonBrowseStatusFile";
            this.buttonBrowseStatusFile.Size = new System.Drawing.Size(54, 23);
            this.buttonBrowseStatusFile.TabIndex = 1;
            this.buttonBrowseStatusFile.Text = "Browse";
            this.buttonBrowseStatusFile.UseVisualStyleBackColor = true;
            this.buttonBrowseStatusFile.Click += new System.EventHandler(this.buttonBrowseStatusFile_Click);
            // 
            // textBoxStatusFile
            // 
            this.textBoxStatusFile.Location = new System.Drawing.Point(6, 19);
            this.textBoxStatusFile.Name = "textBoxStatusFile";
            this.textBoxStatusFile.Size = new System.Drawing.Size(359, 20);
            this.textBoxStatusFile.TabIndex = 0;
            // 
            // statusFileWatcher
            // 
            this.statusFileWatcher.EnableRaisingEvents = true;
            this.statusFileWatcher.SynchronizingObject = this;
            this.statusFileWatcher.Changed += new System.IO.FileSystemEventHandler(this.statusFileWatcher_Changed);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(174, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Altitude:";
            // 
            // textBoxAltitude
            // 
            this.textBoxAltitude.Location = new System.Drawing.Point(177, 32);
            this.textBoxAltitude.MaxLength = 20;
            this.textBoxAltitude.Name = "textBoxAltitude";
            this.textBoxAltitude.Size = new System.Drawing.Size(78, 20);
            this.textBoxAltitude.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Latitude:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Longitude:";
            // 
            // textBoxLatitude
            // 
            this.textBoxLatitude.Location = new System.Drawing.Point(93, 32);
            this.textBoxLatitude.MaxLength = 20;
            this.textBoxLatitude.Name = "textBoxLatitude";
            this.textBoxLatitude.Size = new System.Drawing.Size(78, 20);
            this.textBoxLatitude.TabIndex = 1;
            // 
            // textBoxLongitude
            // 
            this.textBoxLongitude.Location = new System.Drawing.Point(9, 32);
            this.textBoxLongitude.MaxLength = 20;
            this.textBoxLongitude.Name = "textBoxLongitude";
            this.textBoxLongitude.Size = new System.Drawing.Size(78, 20);
            this.textBoxLongitude.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Heading:";
            // 
            // textBoxHeading
            // 
            this.textBoxHeading.Location = new System.Drawing.Point(9, 76);
            this.textBoxHeading.MaxLength = 20;
            this.textBoxHeading.Name = "textBoxHeading";
            this.textBoxHeading.Size = new System.Drawing.Size(78, 20);
            this.textBoxHeading.TabIndex = 4;
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(12, 221);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(700, 147);
            this.listBoxLog.TabIndex = 4;
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(72, 151);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(54, 23);
            this.buttonTest.TabIndex = 5;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // labelLastUpdateTime
            // 
            this.labelLastUpdateTime.AutoSize = true;
            this.labelLastUpdateTime.Location = new System.Drawing.Point(206, 83);
            this.labelLastUpdateTime.Name = "labelLastUpdateTime";
            this.labelLastUpdateTime.Size = new System.Drawing.Size(49, 13);
            this.labelLastUpdateTime.TabIndex = 7;
            this.labelLastUpdateTime.Text = "00:00:00";
            // 
            // checkBoxAutoScroll
            // 
            this.checkBoxAutoScroll.AutoSize = true;
            this.checkBoxAutoScroll.Checked = true;
            this.checkBoxAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoScroll.Location = new System.Drawing.Point(542, 202);
            this.checkBoxAutoScroll.Name = "checkBoxAutoScroll";
            this.checkBoxAutoScroll.Size = new System.Drawing.Size(75, 17);
            this.checkBoxAutoScroll.TabIndex = 8;
            this.checkBoxAutoScroll.Text = "Auto-scroll";
            this.checkBoxAutoScroll.UseVisualStyleBackColor = true;
            // 
            // checkBoxSendLocationOnly
            // 
            this.checkBoxSendLocationOnly.AutoSize = true;
            this.checkBoxSendLocationOnly.Checked = true;
            this.checkBoxSendLocationOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSendLocationOnly.Location = new System.Drawing.Point(6, 19);
            this.checkBoxSendLocationOnly.Name = "checkBoxSendLocationOnly";
            this.checkBoxSendLocationOnly.Size = new System.Drawing.Size(160, 17);
            this.checkBoxSendLocationOnly.TabIndex = 7;
            this.checkBoxSendLocationOnly.Text = "Log/send location data Only";
            this.checkBoxSendLocationOnly.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowLive
            // 
            this.checkBoxShowLive.AutoSize = true;
            this.checkBoxShowLive.Location = new System.Drawing.Point(623, 202);
            this.checkBoxShowLive.Name = "checkBoxShowLive";
            this.checkBoxShowLive.Size = new System.Drawing.Size(89, 17);
            this.checkBoxShowLive.TabIndex = 6;
            this.checkBoxShowLive.Text = "Show live log";
            this.checkBoxShowLive.UseVisualStyleBackColor = true;
            // 
            // textBoxClientId
            // 
            this.textBoxClientId.Location = new System.Drawing.Point(6, 19);
            this.textBoxClientId.MaxLength = 25;
            this.textBoxClientId.Name = "textBoxClientId";
            this.textBoxClientId.Size = new System.Drawing.Size(171, 20);
            this.textBoxClientId.TabIndex = 4;
            this.textBoxClientId.TextChanged += new System.EventHandler(this.textBoxClientId_TextChanged);
            // 
            // textBoxSaveFile
            // 
            this.textBoxSaveFile.Enabled = false;
            this.textBoxSaveFile.Location = new System.Drawing.Point(94, 19);
            this.textBoxSaveFile.Name = "textBoxSaveFile";
            this.textBoxSaveFile.Size = new System.Drawing.Size(142, 20);
            this.textBoxSaveFile.TabIndex = 3;
            this.textBoxSaveFile.Text = "tracking.log";
            // 
            // checkBoxSaveToFile
            // 
            this.checkBoxSaveToFile.AutoSize = true;
            this.checkBoxSaveToFile.Location = new System.Drawing.Point(6, 21);
            this.checkBoxSaveToFile.Name = "checkBoxSaveToFile";
            this.checkBoxSaveToFile.Size = new System.Drawing.Size(82, 17);
            this.checkBoxSaveToFile.TabIndex = 2;
            this.checkBoxSaveToFile.Text = "Save to file:";
            this.checkBoxSaveToFile.UseVisualStyleBackColor = true;
            this.checkBoxSaveToFile.CheckedChanged += new System.EventHandler(this.checkBoxSaveToFile_CheckedChanged);
            // 
            // textBoxUploadServer
            // 
            this.textBoxUploadServer.Enabled = false;
            this.textBoxUploadServer.Location = new System.Drawing.Point(140, 19);
            this.textBoxUploadServer.MaxLength = 100;
            this.textBoxUploadServer.Name = "textBoxUploadServer";
            this.textBoxUploadServer.Size = new System.Drawing.Size(109, 20);
            this.textBoxUploadServer.TabIndex = 1;
            this.textBoxUploadServer.TextChanged += new System.EventHandler(this.textBoxUploadServer_TextChanged);
            // 
            // checkBoxUpload
            // 
            this.checkBoxUpload.AutoSize = true;
            this.checkBoxUpload.Location = new System.Drawing.Point(133, 155);
            this.checkBoxUpload.Name = "checkBoxUpload";
            this.checkBoxUpload.Size = new System.Drawing.Size(60, 17);
            this.checkBoxUpload.TabIndex = 0;
            this.checkBoxUpload.Text = "Upload";
            this.checkBoxUpload.UseVisualStyleBackColor = true;
            this.checkBoxUpload.CheckedChanged += new System.EventHandler(this.checkBoxUpload_CheckedChanged);
            // 
            // buttonLocator
            // 
            this.buttonLocator.Location = new System.Drawing.Point(72, 122);
            this.buttonLocator.Name = "buttonLocator";
            this.buttonLocator.Size = new System.Drawing.Size(55, 23);
            this.buttonLocator.TabIndex = 9;
            this.buttonLocator.Text = "Locate";
            this.buttonLocator.UseVisualStyleBackColor = true;
            this.buttonLocator.Click += new System.EventHandler(this.buttonLocator_Click);
            // 
            // buttonRoutePlanner
            // 
            this.buttonRoutePlanner.Location = new System.Drawing.Point(11, 122);
            this.buttonRoutePlanner.Name = "buttonRoutePlanner";
            this.buttonRoutePlanner.Size = new System.Drawing.Size(55, 23);
            this.buttonRoutePlanner.TabIndex = 10;
            this.buttonRoutePlanner.Text = "Route";
            this.buttonRoutePlanner.UseVisualStyleBackColor = true;
            this.buttonRoutePlanner.Click += new System.EventHandler(this.buttonRoutePlanner_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(199, 151);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 11;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonShowConfig
            // 
            this.buttonShowConfig.Location = new System.Drawing.Point(199, 122);
            this.buttonShowConfig.Name = "buttonShowConfig";
            this.buttonShowConfig.Size = new System.Drawing.Size(75, 23);
            this.buttonShowConfig.TabIndex = 12;
            this.buttonShowConfig.Text = "Show Config";
            this.buttonShowConfig.UseVisualStyleBackColor = true;
            this.buttonShowConfig.Click += new System.EventHandler(this.buttonShowConfig_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBoxClientId);
            this.groupBox5.Location = new System.Drawing.Point(281, 66);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(183, 50);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Client Id (or commander name)";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.checkBoxSaveToFile);
            this.groupBox6.Controls.Add(this.textBoxSaveFile);
            this.groupBox6.Location = new System.Drawing.Point(470, 66);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(242, 50);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Local Log";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.radioButtonUseCustomServer);
            this.groupBox7.Controls.Add(this.radioButtonUseDefaultServer);
            this.groupBox7.Controls.Add(this.textBoxUploadServer);
            this.groupBox7.Location = new System.Drawing.Point(281, 122);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(255, 52);
            this.groupBox7.TabIndex = 15;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Server";
            // 
            // radioButtonUseCustomServer
            // 
            this.radioButtonUseCustomServer.AutoSize = true;
            this.radioButtonUseCustomServer.Location = new System.Drawing.Point(71, 20);
            this.radioButtonUseCustomServer.Name = "radioButtonUseCustomServer";
            this.radioButtonUseCustomServer.Size = new System.Drawing.Size(63, 17);
            this.radioButtonUseCustomServer.TabIndex = 3;
            this.radioButtonUseCustomServer.Text = "Custom:";
            this.radioButtonUseCustomServer.UseVisualStyleBackColor = true;
            this.radioButtonUseCustomServer.CheckedChanged += new System.EventHandler(this.radioButtonUseCustomServer_CheckedChanged);
            // 
            // radioButtonUseDefaultServer
            // 
            this.radioButtonUseDefaultServer.AutoSize = true;
            this.radioButtonUseDefaultServer.Checked = true;
            this.radioButtonUseDefaultServer.Location = new System.Drawing.Point(6, 20);
            this.radioButtonUseDefaultServer.Name = "radioButtonUseDefaultServer";
            this.radioButtonUseDefaultServer.Size = new System.Drawing.Size(59, 17);
            this.radioButtonUseDefaultServer.TabIndex = 2;
            this.radioButtonUseDefaultServer.TabStop = true;
            this.radioButtonUseDefaultServer.Tag = "srvtracker.darkbytes.co.uk";
            this.radioButtonUseDefaultServer.Text = "Default";
            this.radioButtonUseDefaultServer.UseVisualStyleBackColor = true;
            this.radioButtonUseDefaultServer.CheckedChanged += new System.EventHandler(this.radioButtonUseDefaultServer_CheckedChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label8);
            this.groupBox8.Controls.Add(this.radioButtonWatchStatusFile);
            this.groupBox8.Controls.Add(this.radioButtonUseTimer);
            this.groupBox8.Controls.Add(this.checkBoxSendLocationOnly);
            this.groupBox8.Location = new System.Drawing.Point(542, 122);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(170, 74);
            this.groupBox8.TabIndex = 16;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Data Settings";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Mode:";
            // 
            // radioButtonWatchStatusFile
            // 
            this.radioButtonWatchStatusFile.AutoSize = true;
            this.radioButtonWatchStatusFile.Location = new System.Drawing.Point(54, 42);
            this.radioButtonWatchStatusFile.Name = "radioButtonWatchStatusFile";
            this.radioButtonWatchStatusFile.Size = new System.Drawing.Size(53, 17);
            this.radioButtonWatchStatusFile.TabIndex = 9;
            this.radioButtonWatchStatusFile.Text = "Event";
            this.radioButtonWatchStatusFile.UseVisualStyleBackColor = true;
            // 
            // radioButtonUseTimer
            // 
            this.radioButtonUseTimer.AutoSize = true;
            this.radioButtonUseTimer.Checked = true;
            this.radioButtonUseTimer.Location = new System.Drawing.Point(113, 42);
            this.radioButtonUseTimer.Name = "radioButtonUseTimer";
            this.radioButtonUseTimer.Size = new System.Drawing.Size(51, 17);
            this.radioButtonUseTimer.TabIndex = 8;
            this.radioButtonUseTimer.TabStop = true;
            this.radioButtonUseTimer.Text = "Timer";
            this.radioButtonUseTimer.UseVisualStyleBackColor = true;
            // 
            // buttonRaceTracker
            // 
            this.buttonRaceTracker.Location = new System.Drawing.Point(11, 151);
            this.buttonRaceTracker.Name = "buttonRaceTracker";
            this.buttonRaceTracker.Size = new System.Drawing.Size(55, 23);
            this.buttonRaceTracker.TabIndex = 17;
            this.buttonRaceTracker.Text = "Race";
            this.buttonRaceTracker.UseVisualStyleBackColor = true;
            this.buttonRaceTracker.Click += new System.EventHandler(this.buttonRaceTracker_Click);
            // 
            // checkBoxTrack
            // 
            this.checkBoxTrack.AutoSize = true;
            this.checkBoxTrack.Location = new System.Drawing.Point(133, 126);
            this.checkBoxTrack.Name = "checkBoxTrack";
            this.checkBoxTrack.Size = new System.Drawing.Size(54, 17);
            this.checkBoxTrack.TabIndex = 18;
            this.checkBoxTrack.Text = "Track";
            this.checkBoxTrack.UseVisualStyleBackColor = true;
            this.checkBoxTrack.CheckedChanged += new System.EventHandler(this.checkBoxTrack_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.textBoxPlanetRadius);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.textBoxLatitude);
            this.groupBox4.Controls.Add(this.textBoxAltitude);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.textBoxLongitude);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.textBoxHeading);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.labelLastUpdateTime);
            this.groupBox4.Location = new System.Drawing.Point(12, 11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(263, 105);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Location";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(93, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Planet radius:";
            // 
            // textBoxPlanetRadius
            // 
            this.textBoxPlanetRadius.Location = new System.Drawing.Point(93, 76);
            this.textBoxPlanetRadius.Name = "textBoxPlanetRadius";
            this.textBoxPlanetRadius.Size = new System.Drawing.Size(78, 20);
            this.textBoxPlanetRadius.TabIndex = 7;
            // 
            // FormTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 184);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.checkBoxTrack);
            this.Controls.Add(this.checkBoxUpload);
            this.Controls.Add(this.buttonShowConfig);
            this.Controls.Add(this.buttonRaceTracker);
            this.Controls.Add(this.checkBoxAutoScroll);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.checkBoxShowLive);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonRoutePlanner);
            this.Controls.Add(this.buttonLocator);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormTracker";
            this.Text = "Vehicle Tracker";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusFileWatcher)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonBrowseStatusFile;
        private System.Windows.Forms.TextBox textBoxStatusFile;
        private System.IO.FileSystemWatcher statusFileWatcher;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxHeading;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLatitude;
        private System.Windows.Forms.TextBox textBoxLongitude;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxAltitude;
        private System.Windows.Forms.Label labelLastUpdateTime;
        private System.Windows.Forms.TextBox textBoxSaveFile;
        private System.Windows.Forms.CheckBox checkBoxSaveToFile;
        private System.Windows.Forms.TextBox textBoxUploadServer;
        private System.Windows.Forms.CheckBox checkBoxUpload;
        private System.Windows.Forms.TextBox textBoxClientId;
        private System.Windows.Forms.CheckBox checkBoxShowLive;
        private System.Windows.Forms.CheckBox checkBoxSendLocationOnly;
        private System.Windows.Forms.CheckBox checkBoxAutoScroll;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonShowConfig;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonRoutePlanner;
        private System.Windows.Forms.Button buttonLocator;
        private System.Windows.Forms.RadioButton radioButtonUseCustomServer;
        private System.Windows.Forms.RadioButton radioButtonUseDefaultServer;
        private System.Windows.Forms.Button buttonRaceTracker;
        private System.Windows.Forms.CheckBox checkBoxTrack;
        private System.Windows.Forms.RadioButton radioButtonWatchStatusFile;
        private System.Windows.Forms.RadioButton radioButtonUseTimer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPlanetRadius;
    }
}

