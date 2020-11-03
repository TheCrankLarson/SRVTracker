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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTracker));
            this.groupBoxStatusLocation = new System.Windows.Forms.GroupBox();
            this.buttonBrowseStatusFile = new System.Windows.Forms.Button();
            this.textBoxStatusFile = new System.Windows.Forms.TextBox();
            this.statusFileWatcher = new System.IO.FileSystemWatcher();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.buttonTest = new System.Windows.Forms.Button();
            this.labelLastUpdateTime = new System.Windows.Forms.Label();
            this.checkBoxAutoScroll = new System.Windows.Forms.CheckBox();
            this.checkBoxShowLive = new System.Windows.Forms.CheckBox();
            this.textBoxClientId = new System.Windows.Forms.TextBox();
            this.textBoxSaveFile = new System.Windows.Forms.TextBox();
            this.checkBoxSaveToFile = new System.Windows.Forms.CheckBox();
            this.textBoxUploadServer = new System.Windows.Forms.TextBox();
            this.checkBoxUpload = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.radioButtonUseCustomServer = new System.Windows.Forms.RadioButton();
            this.radioButtonUseDefaultServer = new System.Windows.Forms.RadioButton();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.radioButtonWatchStatusFile = new System.Windows.Forms.RadioButton();
            this.radioButtonUseTimer = new System.Windows.Forms.RadioButton();
            this.checkBoxTrack = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxAutoUpdate = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeBetaUpdates = new System.Windows.Forms.CheckBox();
            this.checkBoxUseDirectionOfTravelAsHeading = new System.Windows.Forms.CheckBox();
            this.buttonShowConfig = new System.Windows.Forms.Button();
            this.buttonRaceTracker = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonRoutePlanner = new System.Windows.Forms.Button();
            this.buttonLocator = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonToggleMiniView = new System.Windows.Forms.Button();
            this.buttonAlwaysOnTop = new System.Windows.Forms.Button();
            this.groupBoxSRVTracker = new System.Windows.Forms.GroupBox();
            this.trackerHUD1 = new SRVTracker.TrackerHUD();
            this.groupBoxStatusLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusFileWatcher)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBoxSRVTracker.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxStatusLocation
            // 
            this.groupBoxStatusLocation.Controls.Add(this.buttonBrowseStatusFile);
            this.groupBoxStatusLocation.Controls.Add(this.textBoxStatusFile);
            this.groupBoxStatusLocation.ForeColor = System.Drawing.Color.White;
            this.groupBoxStatusLocation.Location = new System.Drawing.Point(264, 0);
            this.groupBoxStatusLocation.Name = "groupBoxStatusLocation";
            this.groupBoxStatusLocation.Size = new System.Drawing.Size(303, 48);
            this.groupBoxStatusLocation.TabIndex = 0;
            this.groupBoxStatusLocation.TabStop = false;
            this.groupBoxStatusLocation.Text = "Status.json location";
            // 
            // buttonBrowseStatusFile
            // 
            this.buttonBrowseStatusFile.Image = global::SRVTracker.Properties.Resources.FolderOpened_16x;
            this.buttonBrowseStatusFile.Location = new System.Drawing.Point(270, 17);
            this.buttonBrowseStatusFile.Name = "buttonBrowseStatusFile";
            this.buttonBrowseStatusFile.Size = new System.Drawing.Size(27, 23);
            this.buttonBrowseStatusFile.TabIndex = 1;
            this.buttonBrowseStatusFile.UseVisualStyleBackColor = true;
            this.buttonBrowseStatusFile.Click += new System.EventHandler(this.buttonBrowseStatusFile_Click);
            // 
            // textBoxStatusFile
            // 
            this.textBoxStatusFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxStatusFile.ForeColor = System.Drawing.Color.White;
            this.textBoxStatusFile.Location = new System.Drawing.Point(6, 19);
            this.textBoxStatusFile.Name = "textBoxStatusFile";
            this.textBoxStatusFile.Size = new System.Drawing.Size(258, 20);
            this.textBoxStatusFile.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textBoxStatusFile, "Location of the Status.json file of Elite: Dangerous.\r\nThis should be automatical" +
        "ly located.");
            // 
            // statusFileWatcher
            // 
            this.statusFileWatcher.EnableRaisingEvents = true;
            this.statusFileWatcher.SynchronizingObject = this;
            this.statusFileWatcher.Changed += new System.IO.FileSystemEventHandler(this.statusFileWatcher_Changed);
            // 
            // listBoxLog
            // 
            this.listBoxLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listBoxLog.ForeColor = System.Drawing.Color.White;
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(6, 183);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(689, 147);
            this.listBoxLog.TabIndex = 4;
            // 
            // buttonTest
            // 
            this.buttonTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTest.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonTest.Location = new System.Drawing.Point(206, 100);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(52, 23);
            this.buttonTest.TabIndex = 5;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = false;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // labelLastUpdateTime
            // 
            this.labelLastUpdateTime.AutoSize = true;
            this.labelLastUpdateTime.ForeColor = System.Drawing.Color.White;
            this.labelLastUpdateTime.Location = new System.Drawing.Point(164, 24);
            this.labelLastUpdateTime.Name = "labelLastUpdateTime";
            this.labelLastUpdateTime.Size = new System.Drawing.Size(49, 13);
            this.labelLastUpdateTime.TabIndex = 7;
            this.labelLastUpdateTime.Text = "00:00:00";
            this.toolTip1.SetToolTip(this.labelLastUpdateTime, "Time last status update was received");
            // 
            // checkBoxAutoScroll
            // 
            this.checkBoxAutoScroll.AutoSize = true;
            this.checkBoxAutoScroll.Checked = true;
            this.checkBoxAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoScroll.ForeColor = System.Drawing.Color.White;
            this.checkBoxAutoScroll.Location = new System.Drawing.Point(525, 160);
            this.checkBoxAutoScroll.Name = "checkBoxAutoScroll";
            this.checkBoxAutoScroll.Size = new System.Drawing.Size(75, 17);
            this.checkBoxAutoScroll.TabIndex = 8;
            this.checkBoxAutoScroll.Text = "Auto-scroll";
            this.toolTip1.SetToolTip(this.checkBoxAutoScroll, "Automatically scroll to the most recent event");
            this.checkBoxAutoScroll.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowLive
            // 
            this.checkBoxShowLive.AutoSize = true;
            this.checkBoxShowLive.ForeColor = System.Drawing.Color.White;
            this.checkBoxShowLive.Location = new System.Drawing.Point(606, 160);
            this.checkBoxShowLive.Name = "checkBoxShowLive";
            this.checkBoxShowLive.Size = new System.Drawing.Size(89, 17);
            this.checkBoxShowLive.TabIndex = 6;
            this.checkBoxShowLive.Text = "Show live log";
            this.toolTip1.SetToolTip(this.checkBoxShowLive, "Show data being tracked. Primarily for toubleshooting purposes.\r\nNot recommended " +
        "to enable during general use.");
            this.checkBoxShowLive.UseVisualStyleBackColor = true;
            // 
            // textBoxClientId
            // 
            this.textBoxClientId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxClientId.ForeColor = System.Drawing.Color.White;
            this.textBoxClientId.Location = new System.Drawing.Point(6, 19);
            this.textBoxClientId.MaxLength = 25;
            this.textBoxClientId.Name = "textBoxClientId";
            this.textBoxClientId.Size = new System.Drawing.Size(171, 20);
            this.textBoxClientId.TabIndex = 4;
            this.toolTip1.SetToolTip(this.textBoxClientId, "Client Id (usually commander name) sent with status updates");
            this.textBoxClientId.TextChanged += new System.EventHandler(this.textBoxClientId_TextChanged);
            // 
            // textBoxSaveFile
            // 
            this.textBoxSaveFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxSaveFile.Enabled = false;
            this.textBoxSaveFile.ForeColor = System.Drawing.Color.White;
            this.textBoxSaveFile.Location = new System.Drawing.Point(94, 19);
            this.textBoxSaveFile.Name = "textBoxSaveFile";
            this.textBoxSaveFile.Size = new System.Drawing.Size(142, 20);
            this.textBoxSaveFile.TabIndex = 3;
            this.textBoxSaveFile.Text = "tracking.log";
            this.toolTip1.SetToolTip(this.textBoxSaveFile, "The file to which status updates will be saved");
            // 
            // checkBoxSaveToFile
            // 
            this.checkBoxSaveToFile.AutoSize = true;
            this.checkBoxSaveToFile.ForeColor = System.Drawing.Color.White;
            this.checkBoxSaveToFile.Location = new System.Drawing.Point(6, 21);
            this.checkBoxSaveToFile.Name = "checkBoxSaveToFile";
            this.checkBoxSaveToFile.Size = new System.Drawing.Size(82, 17);
            this.checkBoxSaveToFile.TabIndex = 2;
            this.checkBoxSaveToFile.Text = "Save to file:";
            this.toolTip1.SetToolTip(this.checkBoxSaveToFile, "If enabled, all status updates will be logged to the specified file");
            this.checkBoxSaveToFile.UseVisualStyleBackColor = true;
            this.checkBoxSaveToFile.CheckedChanged += new System.EventHandler(this.checkBoxSaveToFile_CheckedChanged);
            // 
            // textBoxUploadServer
            // 
            this.textBoxUploadServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxUploadServer.Enabled = false;
            this.textBoxUploadServer.ForeColor = System.Drawing.Color.White;
            this.textBoxUploadServer.Location = new System.Drawing.Point(140, 17);
            this.textBoxUploadServer.MaxLength = 100;
            this.textBoxUploadServer.Name = "textBoxUploadServer";
            this.textBoxUploadServer.Size = new System.Drawing.Size(109, 20);
            this.textBoxUploadServer.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textBoxUploadServer, "The server to send the status updates to");
            this.textBoxUploadServer.TextChanged += new System.EventHandler(this.textBoxUploadServer_TextChanged);
            // 
            // checkBoxUpload
            // 
            this.checkBoxUpload.AutoSize = true;
            this.checkBoxUpload.ForeColor = System.Drawing.Color.White;
            this.checkBoxUpload.Location = new System.Drawing.Point(65, 77);
            this.checkBoxUpload.Name = "checkBoxUpload";
            this.checkBoxUpload.Size = new System.Drawing.Size(60, 17);
            this.checkBoxUpload.TabIndex = 0;
            this.checkBoxUpload.Text = "Upload";
            this.toolTip1.SetToolTip(this.checkBoxUpload, "Upload all tracked status updates to the server.\r\nThis is required for race manag" +
        "ement and to\r\nallow other commanders to locate you.\r\nIt is NOT required for loca" +
        "l tracking or route\r\nplanning.");
            this.checkBoxUpload.UseVisualStyleBackColor = true;
            this.checkBoxUpload.CheckedChanged += new System.EventHandler(this.checkBoxUpload_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBoxClientId);
            this.groupBox5.ForeColor = System.Drawing.Color.White;
            this.groupBox5.Location = new System.Drawing.Point(264, 54);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(183, 50);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Commander Name";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.checkBoxSaveToFile);
            this.groupBox6.Controls.Add(this.textBoxSaveFile);
            this.groupBox6.ForeColor = System.Drawing.Color.White;
            this.groupBox6.Location = new System.Drawing.Point(453, 54);
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
            this.groupBox7.ForeColor = System.Drawing.Color.White;
            this.groupBox7.Location = new System.Drawing.Point(264, 110);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(255, 44);
            this.groupBox7.TabIndex = 15;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Server";
            // 
            // radioButtonUseCustomServer
            // 
            this.radioButtonUseCustomServer.AutoSize = true;
            this.radioButtonUseCustomServer.ForeColor = System.Drawing.Color.White;
            this.radioButtonUseCustomServer.Location = new System.Drawing.Point(71, 18);
            this.radioButtonUseCustomServer.Name = "radioButtonUseCustomServer";
            this.radioButtonUseCustomServer.Size = new System.Drawing.Size(63, 17);
            this.radioButtonUseCustomServer.TabIndex = 3;
            this.radioButtonUseCustomServer.Text = "Custom:";
            this.toolTip1.SetToolTip(this.radioButtonUseCustomServer, "Select to upload to a custom server");
            this.radioButtonUseCustomServer.UseVisualStyleBackColor = true;
            this.radioButtonUseCustomServer.CheckedChanged += new System.EventHandler(this.radioButtonUseCustomServer_CheckedChanged);
            // 
            // radioButtonUseDefaultServer
            // 
            this.radioButtonUseDefaultServer.AutoSize = true;
            this.radioButtonUseDefaultServer.Checked = true;
            this.radioButtonUseDefaultServer.ForeColor = System.Drawing.Color.White;
            this.radioButtonUseDefaultServer.Location = new System.Drawing.Point(6, 18);
            this.radioButtonUseDefaultServer.Name = "radioButtonUseDefaultServer";
            this.radioButtonUseDefaultServer.Size = new System.Drawing.Size(59, 17);
            this.radioButtonUseDefaultServer.TabIndex = 2;
            this.radioButtonUseDefaultServer.TabStop = true;
            this.radioButtonUseDefaultServer.Tag = "srvtracker.darkbytes.co.uk";
            this.radioButtonUseDefaultServer.Text = "Default";
            this.toolTip1.SetToolTip(this.radioButtonUseDefaultServer, "Select to upload to the default server");
            this.radioButtonUseDefaultServer.UseVisualStyleBackColor = true;
            this.radioButtonUseDefaultServer.CheckedChanged += new System.EventHandler(this.radioButtonUseDefaultServer_CheckedChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.radioButtonWatchStatusFile);
            this.groupBox8.Controls.Add(this.radioButtonUseTimer);
            this.groupBox8.ForeColor = System.Drawing.Color.White;
            this.groupBox8.Location = new System.Drawing.Point(573, 0);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(122, 48);
            this.groupBox8.TabIndex = 16;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Monitor method";
            // 
            // radioButtonWatchStatusFile
            // 
            this.radioButtonWatchStatusFile.AutoSize = true;
            this.radioButtonWatchStatusFile.Checked = true;
            this.radioButtonWatchStatusFile.ForeColor = System.Drawing.Color.White;
            this.radioButtonWatchStatusFile.Location = new System.Drawing.Point(6, 20);
            this.radioButtonWatchStatusFile.Name = "radioButtonWatchStatusFile";
            this.radioButtonWatchStatusFile.Size = new System.Drawing.Size(53, 17);
            this.radioButtonWatchStatusFile.TabIndex = 9;
            this.radioButtonWatchStatusFile.TabStop = true;
            this.radioButtonWatchStatusFile.Text = "Event";
            this.toolTip1.SetToolTip(this.radioButtonWatchStatusFile, "This method of monitoring the status.json file registers for\r\nnotifications of ch" +
        "anges to the file, and when it receives\r\nsuch a notification will read the updat" +
        "ed file.");
            this.radioButtonWatchStatusFile.UseVisualStyleBackColor = true;
            this.radioButtonWatchStatusFile.CheckedChanged += new System.EventHandler(this.radioButtonWatchStatusFile_CheckedChanged);
            // 
            // radioButtonUseTimer
            // 
            this.radioButtonUseTimer.AutoSize = true;
            this.radioButtonUseTimer.ForeColor = System.Drawing.Color.White;
            this.radioButtonUseTimer.Location = new System.Drawing.Point(65, 20);
            this.radioButtonUseTimer.Name = "radioButtonUseTimer";
            this.radioButtonUseTimer.Size = new System.Drawing.Size(51, 17);
            this.radioButtonUseTimer.TabIndex = 8;
            this.radioButtonUseTimer.Text = "Timer";
            this.toolTip1.SetToolTip(this.radioButtonUseTimer, "This method of Status.json monitoring simply checks whether the\r\nfile has been up" +
        "dated every 750ms.  If it has, it reads and processes\r\nit.");
            this.radioButtonUseTimer.UseVisualStyleBackColor = true;
            this.radioButtonUseTimer.CheckedChanged += new System.EventHandler(this.radioButtonUseTimer_CheckedChanged);
            // 
            // checkBoxTrack
            // 
            this.checkBoxTrack.AutoSize = true;
            this.checkBoxTrack.ForeColor = System.Drawing.Color.White;
            this.checkBoxTrack.Location = new System.Drawing.Point(5, 77);
            this.checkBoxTrack.Name = "checkBoxTrack";
            this.checkBoxTrack.Size = new System.Drawing.Size(54, 17);
            this.checkBoxTrack.TabIndex = 18;
            this.checkBoxTrack.Text = "Track";
            this.toolTip1.SetToolTip(this.checkBoxTrack, "Monitor status file for tracking updates.\r\nRequired for all tracking and locating" +
        " functions.");
            this.checkBoxTrack.UseVisualStyleBackColor = true;
            this.checkBoxTrack.CheckedChanged += new System.EventHandler(this.checkBoxTrack_CheckedChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 8000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // checkBoxAutoUpdate
            // 
            this.checkBoxAutoUpdate.AutoSize = true;
            this.checkBoxAutoUpdate.ForeColor = System.Drawing.Color.White;
            this.checkBoxAutoUpdate.Location = new System.Drawing.Point(9, 20);
            this.checkBoxAutoUpdate.Name = "checkBoxAutoUpdate";
            this.checkBoxAutoUpdate.Size = new System.Drawing.Size(15, 14);
            this.checkBoxAutoUpdate.TabIndex = 21;
            this.toolTip1.SetToolTip(this.checkBoxAutoUpdate, "If enabled, will automatically check for update on start-up.");
            this.checkBoxAutoUpdate.UseVisualStyleBackColor = true;
            this.checkBoxAutoUpdate.CheckedChanged += new System.EventHandler(this.checkBoxAutoUpdate_CheckedChanged);
            // 
            // checkBoxIncludeBetaUpdates
            // 
            this.checkBoxIncludeBetaUpdates.AutoSize = true;
            this.checkBoxIncludeBetaUpdates.ForeColor = System.Drawing.Color.White;
            this.checkBoxIncludeBetaUpdates.Location = new System.Drawing.Point(37, 19);
            this.checkBoxIncludeBetaUpdates.Name = "checkBoxIncludeBetaUpdates";
            this.checkBoxIncludeBetaUpdates.Size = new System.Drawing.Size(127, 17);
            this.checkBoxIncludeBetaUpdates.TabIndex = 22;
            this.checkBoxIncludeBetaUpdates.Text = "Include beta releases";
            this.toolTip1.SetToolTip(this.checkBoxIncludeBetaUpdates, "If selected, auto-update will include beta versions.  If\r\nnot set, only release v" +
        "ersions will be updated.\r\nSetting is automatically enabled if a beta version is " +
        "run.\r\n");
            this.checkBoxIncludeBetaUpdates.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseDirectionOfTravelAsHeading
            // 
            this.checkBoxUseDirectionOfTravelAsHeading.AutoSize = true;
            this.checkBoxUseDirectionOfTravelAsHeading.Checked = true;
            this.checkBoxUseDirectionOfTravelAsHeading.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseDirectionOfTravelAsHeading.ForeColor = System.Drawing.Color.White;
            this.checkBoxUseDirectionOfTravelAsHeading.Location = new System.Drawing.Point(5, 100);
            this.checkBoxUseDirectionOfTravelAsHeading.Name = "checkBoxUseDirectionOfTravelAsHeading";
            this.checkBoxUseDirectionOfTravelAsHeading.Size = new System.Drawing.Size(184, 17);
            this.checkBoxUseDirectionOfTravelAsHeading.TabIndex = 24;
            this.checkBoxUseDirectionOfTravelAsHeading.Text = "Use direction of travel as heading";
            this.toolTip1.SetToolTip(this.checkBoxUseDirectionOfTravelAsHeading, "When selected, direction of travel will be calculated and\r\nused as heading instea" +
        "d of direction vehicle is facing\r\n(which is what E: D gives us)");
            this.checkBoxUseDirectionOfTravelAsHeading.UseVisualStyleBackColor = true;
            // 
            // buttonShowConfig
            // 
            this.buttonShowConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonShowConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShowConfig.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonShowConfig.Image = global::SRVTracker.Properties.Resources.Settings_16x;
            this.buttonShowConfig.Location = new System.Drawing.Point(182, 44);
            this.buttonShowConfig.Name = "buttonShowConfig";
            this.buttonShowConfig.Size = new System.Drawing.Size(38, 23);
            this.buttonShowConfig.TabIndex = 12;
            this.toolTip1.SetToolTip(this.buttonShowConfig, "Show Settings");
            this.buttonShowConfig.UseVisualStyleBackColor = false;
            this.buttonShowConfig.Click += new System.EventHandler(this.buttonShowConfig_Click);
            // 
            // buttonRaceTracker
            // 
            this.buttonRaceTracker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonRaceTracker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRaceTracker.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonRaceTracker.Image = global::SRVTracker.Properties.Resources.race_flag_white_16x16;
            this.buttonRaceTracker.Location = new System.Drawing.Point(121, 44);
            this.buttonRaceTracker.Name = "buttonRaceTracker";
            this.buttonRaceTracker.Size = new System.Drawing.Size(55, 23);
            this.buttonRaceTracker.TabIndex = 17;
            this.toolTip1.SetToolTip(this.buttonRaceTracker, "Open the Race Manager\r\nCreate, manage and track SRV races");
            this.buttonRaceTracker.UseVisualStyleBackColor = false;
            this.buttonRaceTracker.Click += new System.EventHandler(this.buttonRaceTracker_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonExit.Image = global::SRVTracker.Properties.Resources.Close_red_16x;
            this.buttonExit.Location = new System.Drawing.Point(220, 71);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(38, 23);
            this.buttonExit.TabIndex = 11;
            this.toolTip1.SetToolTip(this.buttonExit, "Close the program");
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonRoutePlanner
            // 
            this.buttonRoutePlanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonRoutePlanner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRoutePlanner.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonRoutePlanner.Image = global::SRVTracker.Properties.Resources.Route_planner_16x16wb;
            this.buttonRoutePlanner.Location = new System.Drawing.Point(62, 44);
            this.buttonRoutePlanner.Name = "buttonRoutePlanner";
            this.buttonRoutePlanner.Size = new System.Drawing.Size(55, 23);
            this.buttonRoutePlanner.TabIndex = 10;
            this.toolTip1.SetToolTip(this.buttonRoutePlanner, "Open the Route Planner\r\nCreate/edit and replay routes (integrates with Locator)");
            this.buttonRoutePlanner.UseVisualStyleBackColor = false;
            this.buttonRoutePlanner.Click += new System.EventHandler(this.buttonRoutePlanner_Click);
            // 
            // buttonLocator
            // 
            this.buttonLocator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonLocator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLocator.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonLocator.Image = global::SRVTracker.Properties.Resources.Target_16x;
            this.buttonLocator.Location = new System.Drawing.Point(3, 44);
            this.buttonLocator.Name = "buttonLocator";
            this.buttonLocator.Size = new System.Drawing.Size(55, 23);
            this.buttonLocator.TabIndex = 9;
            this.toolTip1.SetToolTip(this.buttonLocator, "Open the Locator\r\nProvides directions to locations and other commanders.");
            this.buttonLocator.UseVisualStyleBackColor = false;
            this.buttonLocator.Click += new System.EventHandler(this.buttonLocator_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pictureBox1.Image = global::SRVTracker.Properties.Resources.MoveGlyph_16x;
            this.pictureBox1.Location = new System.Drawing.Point(160, 76);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "Click and drag here to move the form");
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxIncludeBetaUpdates);
            this.groupBox2.Controls.Add(this.checkBoxAutoUpdate);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(525, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 44);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Auto-update";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(40, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Last update processed:";
            // 
            // buttonToggleMiniView
            // 
            this.buttonToggleMiniView.BackColor = System.Drawing.Color.DimGray;
            this.buttonToggleMiniView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonToggleMiniView.ForeColor = System.Drawing.Color.Black;
            this.buttonToggleMiniView.Location = new System.Drawing.Point(247, 0);
            this.buttonToggleMiniView.Name = "buttonToggleMiniView";
            this.buttonToggleMiniView.Size = new System.Drawing.Size(13, 13);
            this.buttonToggleMiniView.TabIndex = 28;
            this.toolTip1.SetToolTip(this.buttonToggleMiniView, "Shrink/expand the window");
            this.buttonToggleMiniView.UseVisualStyleBackColor = false;
            this.buttonToggleMiniView.Click += new System.EventHandler(this.buttonToggleMiniView_Click);
            // 
            // buttonAlwaysOnTop
            // 
            this.buttonAlwaysOnTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAlwaysOnTop.Image = global::SRVTracker.Properties.Resources.PinnedItem_16x;
            this.buttonAlwaysOnTop.Location = new System.Drawing.Point(230, 44);
            this.buttonAlwaysOnTop.Name = "buttonAlwaysOnTop";
            this.buttonAlwaysOnTop.Size = new System.Drawing.Size(28, 23);
            this.buttonAlwaysOnTop.TabIndex = 29;
            this.toolTip1.SetToolTip(this.buttonAlwaysOnTop, "Pin window topmost");
            this.buttonAlwaysOnTop.UseVisualStyleBackColor = true;
            this.buttonAlwaysOnTop.Click += new System.EventHandler(this.buttonAlwaysOnTop_Click);
            // 
            // groupBoxSRVTracker
            // 
            this.groupBoxSRVTracker.Controls.Add(this.label1);
            this.groupBoxSRVTracker.Controls.Add(this.labelLastUpdateTime);
            this.groupBoxSRVTracker.ForeColor = System.Drawing.Color.White;
            this.groupBoxSRVTracker.Location = new System.Drawing.Point(6, 123);
            this.groupBoxSRVTracker.Name = "groupBoxSRVTracker";
            this.groupBoxSRVTracker.Size = new System.Drawing.Size(252, 54);
            this.groupBoxSRVTracker.TabIndex = 30;
            this.groupBoxSRVTracker.TabStop = false;
            this.groupBoxSRVTracker.Text = "SRVTracker";
            // 
            // trackerHUD1
            // 
            this.trackerHUD1.BackColor = System.Drawing.Color.Black;
            this.trackerHUD1.Location = new System.Drawing.Point(0, 0);
            this.trackerHUD1.Name = "trackerHUD1";
            this.trackerHUD1.Size = new System.Drawing.Size(260, 40);
            this.trackerHUD1.TabIndex = 20;
            // 
            // FormTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(925, 481);
            this.Controls.Add(this.groupBoxSRVTracker);
            this.Controls.Add(this.buttonAlwaysOnTop);
            this.Controls.Add(this.buttonToggleMiniView);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkBoxUseDirectionOfTravelAsHeading);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.trackerHUD1);
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
            this.Controls.Add(this.groupBoxStatusLocation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormTracker";
            this.Text = "Vehicle Tracker";
            this.groupBoxStatusLocation.ResumeLayout(false);
            this.groupBoxStatusLocation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusFileWatcher)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxSRVTracker.ResumeLayout(false);
            this.groupBoxSRVTracker.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxStatusLocation;
        private System.Windows.Forms.Button buttonBrowseStatusFile;
        private System.Windows.Forms.TextBox textBoxStatusFile;
        private System.IO.FileSystemWatcher statusFileWatcher;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Label labelLastUpdateTime;
        private System.Windows.Forms.TextBox textBoxSaveFile;
        private System.Windows.Forms.CheckBox checkBoxSaveToFile;
        private System.Windows.Forms.TextBox textBoxUploadServer;
        private System.Windows.Forms.CheckBox checkBoxUpload;
        private System.Windows.Forms.TextBox textBoxClientId;
        private System.Windows.Forms.CheckBox checkBoxShowLive;
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
        private System.Windows.Forms.ToolTip toolTip1;
        private TrackerHUD trackerHUD1;
        private System.Windows.Forms.CheckBox checkBoxAutoUpdate;
        private System.Windows.Forms.CheckBox checkBoxIncludeBetaUpdates;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxUseDirectionOfTravelAsHeading;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonToggleMiniView;
        private System.Windows.Forms.Button buttonAlwaysOnTop;
        private System.Windows.Forms.GroupBox groupBoxSRVTracker;
    }
}

