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
            this.buttonTest = new System.Windows.Forms.Button();
            this.labelLastUpdateTime = new System.Windows.Forms.Label();
            this.textBoxCommanderName = new System.Windows.Forms.TextBox();
            this.textBoxTelemetryFolder = new System.Windows.Forms.TextBox();
            this.checkBoxSaveTelemetryFolder = new System.Windows.Forms.CheckBox();
            this.textBoxUploadServer = new System.Windows.Forms.TextBox();
            this.checkBoxUpload = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonUpdateName = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioButtonShipTelemetry = new System.Windows.Forms.RadioButton();
            this.radioButtonSRVTelemetry = new System.Windows.Forms.RadioButton();
            this.buttonNewSession = new System.Windows.Forms.Button();
            this.buttonBrowseTelemetryFolder = new System.Windows.Forms.Button();
            this.checkBoxExportSRVTelemetry = new System.Windows.Forms.CheckBox();
            this.buttonSRVTelemetryExportSettings = new System.Windows.Forms.Button();
            this.checkBoxCaptureSRVTelemetry = new System.Windows.Forms.CheckBox();
            this.checkBoxShowSRVTelemetry = new System.Windows.Forms.CheckBox();
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
            this.textBoxClientId = new System.Windows.Forms.TextBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonShowConfig = new System.Windows.Forms.Button();
            this.buttonRaceTracker = new System.Windows.Forms.Button();
            this.buttonRoutePlanner = new System.Windows.Forms.Button();
            this.buttonLocator = new System.Windows.Forms.Button();
            this.checkBoxUseSmartHeadingOnlyWhenOver = new System.Windows.Forms.CheckBox();
            this.numericUpDownUseSmartHeadingOnlyWhenOver = new System.Windows.Forms.NumericUpDown();
            this.checkBoxUseSmartHeadingOnlyWhenInSRV = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowMischief = new System.Windows.Forms.CheckBox();
            this.buttonRaceTelemetrySettings = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxSRVTracker = new System.Windows.Forms.GroupBox();
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPageCommander = new System.Windows.Forms.TabPage();
            this.buttonRacerProfile = new System.Windows.Forms.Button();
            this.checkBoxShowRaceTelemetry = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageMonitoring = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPageServer = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownUdpUploadPort = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPageLogging = new System.Windows.Forms.TabPage();
            this.tabPageUpdate = new System.Windows.Forms.TabPage();
            this.groupBoxStatusLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusFileWatcher)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUseSmartHeadingOnlyWhenOver)).BeginInit();
            this.groupBoxSRVTracker.SuspendLayout();
            this.tabControlSettings.SuspendLayout();
            this.tabPageCommander.SuspendLayout();
            this.tabPageMonitoring.SuspendLayout();
            this.tabPageServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUdpUploadPort)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPageLogging.SuspendLayout();
            this.tabPageUpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxStatusLocation
            // 
            this.groupBoxStatusLocation.Controls.Add(this.buttonBrowseStatusFile);
            this.groupBoxStatusLocation.Controls.Add(this.textBoxStatusFile);
            this.groupBoxStatusLocation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxStatusLocation.Location = new System.Drawing.Point(6, 6);
            this.groupBoxStatusLocation.Name = "groupBoxStatusLocation";
            this.groupBoxStatusLocation.Size = new System.Drawing.Size(306, 48);
            this.groupBoxStatusLocation.TabIndex = 0;
            this.groupBoxStatusLocation.TabStop = false;
            this.groupBoxStatusLocation.Text = "Status.json location";
            // 
            // buttonBrowseStatusFile
            // 
            this.buttonBrowseStatusFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonBrowseStatusFile.Image = global::SRVTracker.Properties.Resources.FolderOpened_16x;
            this.buttonBrowseStatusFile.Location = new System.Drawing.Point(273, 17);
            this.buttonBrowseStatusFile.Name = "buttonBrowseStatusFile";
            this.buttonBrowseStatusFile.Size = new System.Drawing.Size(27, 23);
            this.buttonBrowseStatusFile.TabIndex = 1;
            this.toolTip1.SetToolTip(this.buttonBrowseStatusFile, "Browse to locate Status.Json file");
            this.buttonBrowseStatusFile.UseVisualStyleBackColor = true;
            this.buttonBrowseStatusFile.Click += new System.EventHandler(this.buttonBrowseStatusFile_Click);
            // 
            // textBoxStatusFile
            // 
            this.textBoxStatusFile.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxStatusFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.textBoxStatusFile.Location = new System.Drawing.Point(6, 19);
            this.textBoxStatusFile.Name = "textBoxStatusFile";
            this.textBoxStatusFile.Size = new System.Drawing.Size(261, 20);
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
            // buttonTest
            // 
            this.buttonTest.BackColor = System.Drawing.SystemColors.Control;
            this.buttonTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTest.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTest.Location = new System.Drawing.Point(68, 119);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(136, 23);
            this.buttonTest.TabIndex = 5;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = false;
            this.buttonTest.Visible = false;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // labelLastUpdateTime
            // 
            this.labelLastUpdateTime.AutoSize = true;
            this.labelLastUpdateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLastUpdateTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelLastUpdateTime.Location = new System.Drawing.Point(152, 24);
            this.labelLastUpdateTime.Name = "labelLastUpdateTime";
            this.labelLastUpdateTime.Size = new System.Drawing.Size(49, 13);
            this.labelLastUpdateTime.TabIndex = 7;
            this.labelLastUpdateTime.Text = "00:00:00";
            this.toolTip1.SetToolTip(this.labelLastUpdateTime, "Time last status update was received");
            // 
            // textBoxCommanderName
            // 
            this.textBoxCommanderName.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxCommanderName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.textBoxCommanderName.Location = new System.Drawing.Point(6, 19);
            this.textBoxCommanderName.MaxLength = 25;
            this.textBoxCommanderName.Name = "textBoxCommanderName";
            this.textBoxCommanderName.Size = new System.Drawing.Size(229, 20);
            this.textBoxCommanderName.TabIndex = 4;
            this.toolTip1.SetToolTip(this.textBoxCommanderName, "Commander name.\r\nWill be registered to client Id on server upload.");
            this.textBoxCommanderName.TextChanged += new System.EventHandler(this.textBoxCommanderName_TextChanged);
            // 
            // textBoxTelemetryFolder
            // 
            this.textBoxTelemetryFolder.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxTelemetryFolder.Enabled = false;
            this.textBoxTelemetryFolder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.textBoxTelemetryFolder.Location = new System.Drawing.Point(45, 88);
            this.textBoxTelemetryFolder.Name = "textBoxTelemetryFolder";
            this.textBoxTelemetryFolder.ReadOnly = true;
            this.textBoxTelemetryFolder.Size = new System.Drawing.Size(222, 20);
            this.textBoxTelemetryFolder.TabIndex = 3;
            this.textBoxTelemetryFolder.Text = "Session Telemetry";
            this.toolTip1.SetToolTip(this.textBoxTelemetryFolder, "The folder to which session telemetry will be saved");
            this.textBoxTelemetryFolder.Validated += new System.EventHandler(this.textBoxTelemetryFolder_Validated);
            // 
            // checkBoxSaveTelemetryFolder
            // 
            this.checkBoxSaveTelemetryFolder.AutoSize = true;
            this.checkBoxSaveTelemetryFolder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxSaveTelemetryFolder.Location = new System.Drawing.Point(27, 65);
            this.checkBoxSaveTelemetryFolder.Name = "checkBoxSaveTelemetryFolder";
            this.checkBoxSaveTelemetryFolder.Size = new System.Drawing.Size(95, 17);
            this.checkBoxSaveTelemetryFolder.TabIndex = 2;
            this.checkBoxSaveTelemetryFolder.Text = "Save to folder:";
            this.toolTip1.SetToolTip(this.checkBoxSaveTelemetryFolder, "If enabled, session data will be save to the specified folder\r\nThis occurs on ses" +
        "sion reset or when the tracker is closed.");
            this.checkBoxSaveTelemetryFolder.UseVisualStyleBackColor = true;
            this.checkBoxSaveTelemetryFolder.CheckedChanged += new System.EventHandler(this.checkBoxSaveToFile_CheckedChanged);
            // 
            // textBoxUploadServer
            // 
            this.textBoxUploadServer.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxUploadServer.Enabled = false;
            this.textBoxUploadServer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.textBoxUploadServer.Location = new System.Drawing.Point(75, 41);
            this.textBoxUploadServer.MaxLength = 100;
            this.textBoxUploadServer.Name = "textBoxUploadServer";
            this.textBoxUploadServer.Size = new System.Drawing.Size(228, 20);
            this.textBoxUploadServer.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textBoxUploadServer, "The server to send the status updates to");
            this.textBoxUploadServer.TextChanged += new System.EventHandler(this.textBoxUploadServer_TextChanged);
            // 
            // checkBoxUpload
            // 
            this.checkBoxUpload.AutoSize = true;
            this.checkBoxUpload.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxUpload.Location = new System.Drawing.Point(219, 84);
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
            this.groupBox5.Controls.Add(this.buttonUpdateName);
            this.groupBox5.Controls.Add(this.textBoxCommanderName);
            this.groupBox5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(306, 50);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Name";
            // 
            // buttonUpdateName
            // 
            this.buttonUpdateName.Enabled = false;
            this.buttonUpdateName.Location = new System.Drawing.Point(241, 17);
            this.buttonUpdateName.Name = "buttonUpdateName";
            this.buttonUpdateName.Size = new System.Drawing.Size(59, 23);
            this.buttonUpdateName.TabIndex = 5;
            this.buttonUpdateName.Text = "Update";
            this.toolTip1.SetToolTip(this.buttonUpdateName, "If you have registered on a server (e.g. for a race),\r\nyou can use this to update" +
        " the commander name\r\non the server.  Only works if the commander name\r\nis not re" +
        "gistered to anyone else.");
            this.buttonUpdateName.UseVisualStyleBackColor = true;
            this.buttonUpdateName.Click += new System.EventHandler(this.buttonUpdateName_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radioButtonShipTelemetry);
            this.groupBox6.Controls.Add(this.radioButtonSRVTelemetry);
            this.groupBox6.Controls.Add(this.buttonNewSession);
            this.groupBox6.Controls.Add(this.buttonBrowseTelemetryFolder);
            this.groupBox6.Controls.Add(this.checkBoxExportSRVTelemetry);
            this.groupBox6.Controls.Add(this.buttonSRVTelemetryExportSettings);
            this.groupBox6.Controls.Add(this.checkBoxCaptureSRVTelemetry);
            this.groupBox6.Controls.Add(this.checkBoxSaveTelemetryFolder);
            this.groupBox6.Controls.Add(this.textBoxTelemetryFolder);
            this.groupBox6.Controls.Add(this.checkBoxShowSRVTelemetry);
            this.groupBox6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox6.Location = new System.Drawing.Point(6, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(306, 131);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Journey Telemetry";
            // 
            // radioButtonShipTelemetry
            // 
            this.radioButtonShipTelemetry.AutoSize = true;
            this.radioButtonShipTelemetry.Location = new System.Drawing.Point(176, 18);
            this.radioButtonShipTelemetry.Name = "radioButtonShipTelemetry";
            this.radioButtonShipTelemetry.Size = new System.Drawing.Size(46, 17);
            this.radioButtonShipTelemetry.TabIndex = 23;
            this.radioButtonShipTelemetry.Text = "Ship";
            this.toolTip1.SetToolTip(this.radioButtonShipTelemetry, "Capture ship telemetry only (SRV events are ignored)");
            this.radioButtonShipTelemetry.UseVisualStyleBackColor = true;
            this.radioButtonShipTelemetry.CheckedChanged += new System.EventHandler(this.radioButtonShipTelemetry_CheckedChanged);
            // 
            // radioButtonSRVTelemetry
            // 
            this.radioButtonSRVTelemetry.AutoSize = true;
            this.radioButtonSRVTelemetry.Checked = true;
            this.radioButtonSRVTelemetry.Location = new System.Drawing.Point(123, 18);
            this.radioButtonSRVTelemetry.Name = "radioButtonSRVTelemetry";
            this.radioButtonSRVTelemetry.Size = new System.Drawing.Size(47, 17);
            this.radioButtonSRVTelemetry.TabIndex = 22;
            this.radioButtonSRVTelemetry.TabStop = true;
            this.radioButtonSRVTelemetry.Text = "SRV";
            this.toolTip1.SetToolTip(this.radioButtonSRVTelemetry, "Capture SRV telemetry only (ship events are ignored)");
            this.radioButtonSRVTelemetry.UseVisualStyleBackColor = true;
            this.radioButtonSRVTelemetry.CheckedChanged += new System.EventHandler(this.radioButtonSRVTelemetry_CheckedChanged);
            // 
            // buttonNewSession
            // 
            this.buttonNewSession.Location = new System.Drawing.Point(246, 38);
            this.buttonNewSession.Name = "buttonNewSession";
            this.buttonNewSession.Size = new System.Drawing.Size(54, 22);
            this.buttonNewSession.TabIndex = 21;
            this.buttonNewSession.Text = "Reset";
            this.toolTip1.SetToolTip(this.buttonNewSession, "Reset session (all telemetry is cleared).\r\nWill save old session if save enabled." +
        "");
            this.buttonNewSession.UseVisualStyleBackColor = true;
            this.buttonNewSession.Click += new System.EventHandler(this.buttonNewSession_Click);
            // 
            // buttonBrowseTelemetryFolder
            // 
            this.buttonBrowseTelemetryFolder.Enabled = false;
            this.buttonBrowseTelemetryFolder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonBrowseTelemetryFolder.Image = global::SRVTracker.Properties.Resources.FolderOpened_16x;
            this.buttonBrowseTelemetryFolder.Location = new System.Drawing.Point(273, 86);
            this.buttonBrowseTelemetryFolder.Name = "buttonBrowseTelemetryFolder";
            this.buttonBrowseTelemetryFolder.Size = new System.Drawing.Size(27, 23);
            this.buttonBrowseTelemetryFolder.TabIndex = 17;
            this.toolTip1.SetToolTip(this.buttonBrowseTelemetryFolder, "Browse to select the session telemetry save folder");
            this.buttonBrowseTelemetryFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseTelemetryFolder.Click += new System.EventHandler(this.buttonBrowseTelemetryFolder_Click);
            // 
            // checkBoxExportSRVTelemetry
            // 
            this.checkBoxExportSRVTelemetry.AutoSize = true;
            this.checkBoxExportSRVTelemetry.Location = new System.Drawing.Point(27, 42);
            this.checkBoxExportSRVTelemetry.Name = "checkBoxExportSRVTelemetry";
            this.checkBoxExportSRVTelemetry.Size = new System.Drawing.Size(56, 17);
            this.checkBoxExportSRVTelemetry.TabIndex = 16;
            this.checkBoxExportSRVTelemetry.Text = "Export";
            this.toolTip1.SetToolTip(this.checkBoxExportSRVTelemetry, "If checked, any reports enabled in telemetry options will be exported");
            this.checkBoxExportSRVTelemetry.UseVisualStyleBackColor = true;
            // 
            // buttonSRVTelemetryExportSettings
            // 
            this.buttonSRVTelemetryExportSettings.Image = global::SRVTracker.Properties.Resources.Settings_16x;
            this.buttonSRVTelemetryExportSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSRVTelemetryExportSettings.Location = new System.Drawing.Point(228, 15);
            this.buttonSRVTelemetryExportSettings.Name = "buttonSRVTelemetryExportSettings";
            this.buttonSRVTelemetryExportSettings.Size = new System.Drawing.Size(72, 22);
            this.buttonSRVTelemetryExportSettings.TabIndex = 15;
            this.buttonSRVTelemetryExportSettings.Text = "Settings";
            this.buttonSRVTelemetryExportSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.buttonSRVTelemetryExportSettings, "Edit telemetry display/export settings");
            this.buttonSRVTelemetryExportSettings.UseVisualStyleBackColor = true;
            this.buttonSRVTelemetryExportSettings.Click += new System.EventHandler(this.buttonSRVTelemetryExportSettings_Click);
            // 
            // checkBoxCaptureSRVTelemetry
            // 
            this.checkBoxCaptureSRVTelemetry.AutoSize = true;
            this.checkBoxCaptureSRVTelemetry.Location = new System.Drawing.Point(6, 19);
            this.checkBoxCaptureSRVTelemetry.Name = "checkBoxCaptureSRVTelemetry";
            this.checkBoxCaptureSRVTelemetry.Size = new System.Drawing.Size(111, 17);
            this.checkBoxCaptureSRVTelemetry.TabIndex = 4;
            this.checkBoxCaptureSRVTelemetry.Text = "Capture telemetry:";
            this.toolTip1.SetToolTip(this.checkBoxCaptureSRVTelemetry, "If selected, session telemetry will be collected");
            this.checkBoxCaptureSRVTelemetry.UseVisualStyleBackColor = true;
            this.checkBoxCaptureSRVTelemetry.CheckedChanged += new System.EventHandler(this.checkBoxCaptureSRVTelemetry_CheckedChanged);
            // 
            // checkBoxShowSRVTelemetry
            // 
            this.checkBoxShowSRVTelemetry.AutoSize = true;
            this.checkBoxShowSRVTelemetry.Location = new System.Drawing.Point(89, 42);
            this.checkBoxShowSRVTelemetry.Name = "checkBoxShowSRVTelemetry";
            this.checkBoxShowSRVTelemetry.Size = new System.Drawing.Size(60, 17);
            this.checkBoxShowSRVTelemetry.TabIndex = 14;
            this.checkBoxShowSRVTelemetry.Text = "Display";
            this.toolTip1.SetToolTip(this.checkBoxShowSRVTelemetry, "If enabled, the telemetry window is displayed");
            this.checkBoxShowSRVTelemetry.UseVisualStyleBackColor = true;
            this.checkBoxShowSRVTelemetry.CheckedChanged += new System.EventHandler(this.checkBoxShowSRVTelemetry_CheckedChanged);
            // 
            // radioButtonUseCustomServer
            // 
            this.radioButtonUseCustomServer.AutoSize = true;
            this.radioButtonUseCustomServer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButtonUseCustomServer.Location = new System.Drawing.Point(6, 42);
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
            this.radioButtonUseDefaultServer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButtonUseDefaultServer.Location = new System.Drawing.Point(6, 19);
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
            this.groupBox8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox8.Location = new System.Drawing.Point(467, 13);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(303, 80);
            this.groupBox8.TabIndex = 16;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Monitor method";
            // 
            // radioButtonWatchStatusFile
            // 
            this.radioButtonWatchStatusFile.AutoSize = true;
            this.radioButtonWatchStatusFile.Enabled = false;
            this.radioButtonWatchStatusFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButtonWatchStatusFile.Location = new System.Drawing.Point(6, 20);
            this.radioButtonWatchStatusFile.Name = "radioButtonWatchStatusFile";
            this.radioButtonWatchStatusFile.Size = new System.Drawing.Size(297, 17);
            this.radioButtonWatchStatusFile.TabIndex = 9;
            this.radioButtonWatchStatusFile.Text = "Register for folder update notifications (not recommended)";
            this.toolTip1.SetToolTip(this.radioButtonWatchStatusFile, resources.GetString("radioButtonWatchStatusFile.ToolTip"));
            this.radioButtonWatchStatusFile.UseVisualStyleBackColor = true;
            this.radioButtonWatchStatusFile.CheckedChanged += new System.EventHandler(this.radioButtonWatchStatusFile_CheckedChanged);
            // 
            // radioButtonUseTimer
            // 
            this.radioButtonUseTimer.AutoSize = true;
            this.radioButtonUseTimer.Checked = true;
            this.radioButtonUseTimer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButtonUseTimer.Location = new System.Drawing.Point(6, 43);
            this.radioButtonUseTimer.Name = "radioButtonUseTimer";
            this.radioButtonUseTimer.Size = new System.Drawing.Size(51, 17);
            this.radioButtonUseTimer.TabIndex = 8;
            this.radioButtonUseTimer.TabStop = true;
            this.radioButtonUseTimer.Text = "Timer";
            this.toolTip1.SetToolTip(this.radioButtonUseTimer, "This method of Status.json monitoring checks whether the file\r\nhas been updated e" +
        "very 700ms.  If it has, it reads and processes\r\nit.");
            this.radioButtonUseTimer.UseVisualStyleBackColor = true;
            this.radioButtonUseTimer.CheckedChanged += new System.EventHandler(this.radioButtonUseTimer_CheckedChanged);
            // 
            // checkBoxTrack
            // 
            this.checkBoxTrack.AutoSize = true;
            this.checkBoxTrack.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxTrack.Location = new System.Drawing.Point(148, 84);
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
            this.checkBoxAutoUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxAutoUpdate.Location = new System.Drawing.Point(13, 83);
            this.checkBoxAutoUpdate.Name = "checkBoxAutoUpdate";
            this.checkBoxAutoUpdate.Size = new System.Drawing.Size(181, 17);
            this.checkBoxAutoUpdate.TabIndex = 21;
            this.checkBoxAutoUpdate.Text = "Enable update check on start-up";
            this.toolTip1.SetToolTip(this.checkBoxAutoUpdate, "If enabled, will automatically check for update on start-up.");
            this.checkBoxAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeBetaUpdates
            // 
            this.checkBoxIncludeBetaUpdates.AutoSize = true;
            this.checkBoxIncludeBetaUpdates.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxIncludeBetaUpdates.Location = new System.Drawing.Point(13, 106);
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
            this.checkBoxUseDirectionOfTravelAsHeading.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxUseDirectionOfTravelAsHeading.Location = new System.Drawing.Point(12, 60);
            this.checkBoxUseDirectionOfTravelAsHeading.Name = "checkBoxUseDirectionOfTravelAsHeading";
            this.checkBoxUseDirectionOfTravelAsHeading.Size = new System.Drawing.Size(244, 17);
            this.checkBoxUseDirectionOfTravelAsHeading.TabIndex = 24;
            this.checkBoxUseDirectionOfTravelAsHeading.Text = "Use direction of travel as heading (Smart Alec)";
            this.toolTip1.SetToolTip(this.checkBoxUseDirectionOfTravelAsHeading, "When selected, direction of travel will be calculated and\r\nused as heading instea" +
        "d of direction vehicle is facing\r\n(which is what E: D gives us).\r\nNamed in honou" +
        "r of Alec Turner!");
            this.checkBoxUseDirectionOfTravelAsHeading.UseVisualStyleBackColor = true;
            this.checkBoxUseDirectionOfTravelAsHeading.CheckedChanged += new System.EventHandler(this.checkBoxUseDirectionOfTravelAsHeading_CheckedChanged);
            // 
            // textBoxClientId
            // 
            this.textBoxClientId.Location = new System.Drawing.Point(56, 62);
            this.textBoxClientId.Name = "textBoxClientId";
            this.textBoxClientId.ReadOnly = true;
            this.textBoxClientId.Size = new System.Drawing.Size(256, 20);
            this.textBoxClientId.TabIndex = 14;
            this.toolTip1.SetToolTip(this.textBoxClientId, resources.GetString("textBoxClientId.ToolTip"));
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(98, 32);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(114, 23);
            this.buttonUpdate.TabIndex = 23;
            this.buttonUpdate.Text = "Check for update";
            this.toolTip1.SetToolTip(this.buttonUpdate, "Checks whether any program updates are availble");
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonShowConfig
            // 
            this.buttonShowConfig.BackColor = System.Drawing.SystemColors.Control;
            this.buttonShowConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShowConfig.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonShowConfig.Image = global::SRVTracker.Properties.Resources.Settings_16x;
            this.buttonShowConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonShowConfig.Location = new System.Drawing.Point(12, 99);
            this.buttonShowConfig.Name = "buttonShowConfig";
            this.buttonShowConfig.Size = new System.Drawing.Size(78, 23);
            this.buttonShowConfig.TabIndex = 12;
            this.buttonShowConfig.Text = "Settings";
            this.buttonShowConfig.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.buttonShowConfig, "Show Settings");
            this.buttonShowConfig.UseVisualStyleBackColor = false;
            this.buttonShowConfig.Click += new System.EventHandler(this.buttonShowConfig_Click);
            // 
            // buttonRaceTracker
            // 
            this.buttonRaceTracker.BackColor = System.Drawing.SystemColors.Control;
            this.buttonRaceTracker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRaceTracker.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonRaceTracker.Image = global::SRVTracker.Properties.Resources.race_flag16x16;
            this.buttonRaceTracker.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRaceTracker.Location = new System.Drawing.Point(12, 70);
            this.buttonRaceTracker.Name = "buttonRaceTracker";
            this.buttonRaceTracker.Size = new System.Drawing.Size(78, 23);
            this.buttonRaceTracker.TabIndex = 17;
            this.buttonRaceTracker.Text = "Racing";
            this.buttonRaceTracker.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.buttonRaceTracker, "Open the Race Manager\r\nCreate, manage and track SRV races");
            this.buttonRaceTracker.UseVisualStyleBackColor = false;
            this.buttonRaceTracker.Click += new System.EventHandler(this.buttonRaceTracker_Click);
            // 
            // buttonRoutePlanner
            // 
            this.buttonRoutePlanner.BackColor = System.Drawing.SystemColors.Control;
            this.buttonRoutePlanner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRoutePlanner.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonRoutePlanner.Image = global::SRVTracker.Properties.Resources.Route_planner_16x16bw;
            this.buttonRoutePlanner.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRoutePlanner.Location = new System.Drawing.Point(12, 41);
            this.buttonRoutePlanner.Name = "buttonRoutePlanner";
            this.buttonRoutePlanner.Size = new System.Drawing.Size(78, 23);
            this.buttonRoutePlanner.TabIndex = 10;
            this.buttonRoutePlanner.Text = "Router";
            this.buttonRoutePlanner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.buttonRoutePlanner, "Open the Route Planner\r\nCreate/edit and replay routes (integrates with Locator)");
            this.buttonRoutePlanner.UseVisualStyleBackColor = false;
            this.buttonRoutePlanner.Click += new System.EventHandler(this.buttonRoutePlanner_Click);
            // 
            // buttonLocator
            // 
            this.buttonLocator.BackColor = System.Drawing.SystemColors.Control;
            this.buttonLocator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLocator.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonLocator.Image = global::SRVTracker.Properties.Resources.Target_16x;
            this.buttonLocator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLocator.Location = new System.Drawing.Point(12, 12);
            this.buttonLocator.Name = "buttonLocator";
            this.buttonLocator.Size = new System.Drawing.Size(78, 23);
            this.buttonLocator.TabIndex = 9;
            this.buttonLocator.Text = "Locator";
            this.buttonLocator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.buttonLocator, "Open the Locator\r\nProvides directions to locations and other commanders.");
            this.buttonLocator.UseVisualStyleBackColor = false;
            this.buttonLocator.Click += new System.EventHandler(this.buttonLocator_Click);
            // 
            // checkBoxUseSmartHeadingOnlyWhenOver
            // 
            this.checkBoxUseSmartHeadingOnlyWhenOver.AutoSize = true;
            this.checkBoxUseSmartHeadingOnlyWhenOver.Checked = true;
            this.checkBoxUseSmartHeadingOnlyWhenOver.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseSmartHeadingOnlyWhenOver.Location = new System.Drawing.Point(33, 83);
            this.checkBoxUseSmartHeadingOnlyWhenOver.Name = "checkBoxUseSmartHeadingOnlyWhenOver";
            this.checkBoxUseSmartHeadingOnlyWhenOver.Size = new System.Drawing.Size(157, 17);
            this.checkBoxUseSmartHeadingOnlyWhenOver.TabIndex = 25;
            this.checkBoxUseSmartHeadingOnlyWhenOver.Text = "Only when travelling above:";
            this.toolTip1.SetToolTip(this.checkBoxUseSmartHeadingOnlyWhenOver, resources.GetString("checkBoxUseSmartHeadingOnlyWhenOver.ToolTip"));
            this.checkBoxUseSmartHeadingOnlyWhenOver.UseVisualStyleBackColor = true;
            // 
            // numericUpDownUseSmartHeadingOnlyWhenOver
            // 
            this.numericUpDownUseSmartHeadingOnlyWhenOver.Location = new System.Drawing.Point(192, 82);
            this.numericUpDownUseSmartHeadingOnlyWhenOver.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownUseSmartHeadingOnlyWhenOver.Name = "numericUpDownUseSmartHeadingOnlyWhenOver";
            this.numericUpDownUseSmartHeadingOnlyWhenOver.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownUseSmartHeadingOnlyWhenOver.TabIndex = 26;
            this.numericUpDownUseSmartHeadingOnlyWhenOver.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.numericUpDownUseSmartHeadingOnlyWhenOver, resources.GetString("numericUpDownUseSmartHeadingOnlyWhenOver.ToolTip"));
            this.numericUpDownUseSmartHeadingOnlyWhenOver.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // checkBoxUseSmartHeadingOnlyWhenInSRV
            // 
            this.checkBoxUseSmartHeadingOnlyWhenInSRV.AutoSize = true;
            this.checkBoxUseSmartHeadingOnlyWhenInSRV.Checked = true;
            this.checkBoxUseSmartHeadingOnlyWhenInSRV.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseSmartHeadingOnlyWhenInSRV.Location = new System.Drawing.Point(33, 106);
            this.checkBoxUseSmartHeadingOnlyWhenInSRV.Name = "checkBoxUseSmartHeadingOnlyWhenInSRV";
            this.checkBoxUseSmartHeadingOnlyWhenInSRV.Size = new System.Drawing.Size(112, 17);
            this.checkBoxUseSmartHeadingOnlyWhenInSRV.TabIndex = 28;
            this.checkBoxUseSmartHeadingOnlyWhenInSRV.Text = "Only when in SRV";
            this.toolTip1.SetToolTip(this.checkBoxUseSmartHeadingOnlyWhenInSRV, "If enabled, direction of travel only calculated when in SRV.");
            this.checkBoxUseSmartHeadingOnlyWhenInSRV.UseVisualStyleBackColor = true;
            // 
            // checkBoxAllowMischief
            // 
            this.checkBoxAllowMischief.AutoSize = true;
            this.checkBoxAllowMischief.Location = new System.Drawing.Point(97, 107);
            this.checkBoxAllowMischief.Name = "checkBoxAllowMischief";
            this.checkBoxAllowMischief.Size = new System.Drawing.Size(92, 17);
            this.checkBoxAllowMischief.TabIndex = 7;
            this.checkBoxAllowMischief.Text = "Allow mischief";
            this.toolTip1.SetToolTip(this.checkBoxAllowMischief, "Sometimes the imps need to let off steam.");
            this.checkBoxAllowMischief.UseVisualStyleBackColor = true;
            // 
            // buttonRaceTelemetrySettings
            // 
            this.buttonRaceTelemetrySettings.Image = global::SRVTracker.Properties.Resources.Settings_16x;
            this.buttonRaceTelemetrySettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRaceTelemetrySettings.Location = new System.Drawing.Point(240, 88);
            this.buttonRaceTelemetrySettings.Name = "buttonRaceTelemetrySettings";
            this.buttonRaceTelemetrySettings.Size = new System.Drawing.Size(72, 22);
            this.buttonRaceTelemetrySettings.TabIndex = 17;
            this.buttonRaceTelemetrySettings.Text = "Settings";
            this.buttonRaceTelemetrySettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.buttonRaceTelemetrySettings, "Edit telemetry display/export settings");
            this.buttonRaceTelemetrySettings.UseVisualStyleBackColor = true;
            this.buttonRaceTelemetrySettings.Click += new System.EventHandler(this.buttonRaceTelemetrySettings_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(28, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Last update processed:";
            // 
            // groupBoxSRVTracker
            // 
            this.groupBoxSRVTracker.Controls.Add(this.label1);
            this.groupBoxSRVTracker.Controls.Add(this.labelLastUpdateTime);
            this.groupBoxSRVTracker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxSRVTracker.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxSRVTracker.Location = new System.Drawing.Point(106, 10);
            this.groupBoxSRVTracker.Name = "groupBoxSRVTracker";
            this.groupBoxSRVTracker.Size = new System.Drawing.Size(232, 54);
            this.groupBoxSRVTracker.TabIndex = 30;
            this.groupBoxSRVTracker.TabStop = false;
            this.groupBoxSRVTracker.Text = "SRVTracker";
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Controls.Add(this.tabPageCommander);
            this.tabControlSettings.Controls.Add(this.tabPageMonitoring);
            this.tabControlSettings.Controls.Add(this.tabPageServer);
            this.tabControlSettings.Controls.Add(this.tabPageLogging);
            this.tabControlSettings.Controls.Add(this.tabPageUpdate);
            this.tabControlSettings.Location = new System.Drawing.Point(12, 128);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(326, 171);
            this.tabControlSettings.TabIndex = 31;
            // 
            // tabPageCommander
            // 
            this.tabPageCommander.Controls.Add(this.buttonRacerProfile);
            this.tabPageCommander.Controls.Add(this.buttonRaceTelemetrySettings);
            this.tabPageCommander.Controls.Add(this.checkBoxShowRaceTelemetry);
            this.tabPageCommander.Controls.Add(this.label2);
            this.tabPageCommander.Controls.Add(this.textBoxClientId);
            this.tabPageCommander.Controls.Add(this.groupBox5);
            this.tabPageCommander.Controls.Add(this.buttonTest);
            this.tabPageCommander.Location = new System.Drawing.Point(4, 22);
            this.tabPageCommander.Name = "tabPageCommander";
            this.tabPageCommander.Size = new System.Drawing.Size(318, 145);
            this.tabPageCommander.TabIndex = 3;
            this.tabPageCommander.Text = "Commander";
            this.tabPageCommander.UseVisualStyleBackColor = true;
            // 
            // buttonRacerProfile
            // 
            this.buttonRacerProfile.Location = new System.Drawing.Point(240, 116);
            this.buttonRacerProfile.Name = "buttonRacerProfile";
            this.buttonRacerProfile.Size = new System.Drawing.Size(72, 23);
            this.buttonRacerProfile.TabIndex = 18;
            this.buttonRacerProfile.Text = "Profile...";
            this.buttonRacerProfile.UseVisualStyleBackColor = true;
            this.buttonRacerProfile.Click += new System.EventHandler(this.buttonRacerProfile_Click);
            // 
            // checkBoxShowRaceTelemetry
            // 
            this.checkBoxShowRaceTelemetry.AutoSize = true;
            this.checkBoxShowRaceTelemetry.Location = new System.Drawing.Point(112, 92);
            this.checkBoxShowRaceTelemetry.Name = "checkBoxShowRaceTelemetry";
            this.checkBoxShowRaceTelemetry.Size = new System.Drawing.Size(122, 17);
            this.checkBoxShowRaceTelemetry.TabIndex = 16;
            this.checkBoxShowRaceTelemetry.Text = "Show race telemetry";
            this.checkBoxShowRaceTelemetry.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Client id:";
            // 
            // tabPageMonitoring
            // 
            this.tabPageMonitoring.Controls.Add(this.checkBoxUseSmartHeadingOnlyWhenInSRV);
            this.tabPageMonitoring.Controls.Add(this.label4);
            this.tabPageMonitoring.Controls.Add(this.numericUpDownUseSmartHeadingOnlyWhenOver);
            this.tabPageMonitoring.Controls.Add(this.checkBoxUseSmartHeadingOnlyWhenOver);
            this.tabPageMonitoring.Controls.Add(this.groupBoxStatusLocation);
            this.tabPageMonitoring.Controls.Add(this.checkBoxUseDirectionOfTravelAsHeading);
            this.tabPageMonitoring.Location = new System.Drawing.Point(4, 22);
            this.tabPageMonitoring.Name = "tabPageMonitoring";
            this.tabPageMonitoring.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMonitoring.Size = new System.Drawing.Size(318, 145);
            this.tabPageMonitoring.TabIndex = 0;
            this.tabPageMonitoring.Text = "Monitoring";
            this.tabPageMonitoring.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(242, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "m/s";
            // 
            // tabPageServer
            // 
            this.tabPageServer.Controls.Add(this.checkBoxAllowMischief);
            this.tabPageServer.Controls.Add(this.label3);
            this.tabPageServer.Controls.Add(this.numericUpDownUdpUploadPort);
            this.tabPageServer.Controls.Add(this.groupBox1);
            this.tabPageServer.Location = new System.Drawing.Point(4, 22);
            this.tabPageServer.Name = "tabPageServer";
            this.tabPageServer.Size = new System.Drawing.Size(318, 145);
            this.tabPageServer.TabIndex = 4;
            this.tabPageServer.Text = "Server";
            this.tabPageServer.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(94, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "UDP Upload Port:";
            // 
            // numericUpDownUdpUploadPort
            // 
            this.numericUpDownUdpUploadPort.Location = new System.Drawing.Point(192, 81);
            this.numericUpDownUdpUploadPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownUdpUploadPort.Name = "numericUpDownUdpUploadPort";
            this.numericUpDownUdpUploadPort.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownUdpUploadPort.TabIndex = 5;
            this.numericUpDownUdpUploadPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownUdpUploadPort.Value = new decimal(new int[] {
            11939,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonUseDefaultServer);
            this.groupBox1.Controls.Add(this.textBoxUploadServer);
            this.groupBox1.Controls.Add(this.radioButtonUseCustomServer);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 69);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Race Server";
            // 
            // tabPageLogging
            // 
            this.tabPageLogging.Controls.Add(this.groupBox6);
            this.tabPageLogging.Location = new System.Drawing.Point(4, 22);
            this.tabPageLogging.Name = "tabPageLogging";
            this.tabPageLogging.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLogging.Size = new System.Drawing.Size(318, 145);
            this.tabPageLogging.TabIndex = 1;
            this.tabPageLogging.Text = "Telemetry";
            this.tabPageLogging.UseVisualStyleBackColor = true;
            // 
            // tabPageUpdate
            // 
            this.tabPageUpdate.Controls.Add(this.buttonUpdate);
            this.tabPageUpdate.Controls.Add(this.checkBoxIncludeBetaUpdates);
            this.tabPageUpdate.Controls.Add(this.checkBoxAutoUpdate);
            this.tabPageUpdate.Location = new System.Drawing.Point(4, 22);
            this.tabPageUpdate.Name = "tabPageUpdate";
            this.tabPageUpdate.Size = new System.Drawing.Size(318, 145);
            this.tabPageUpdate.TabIndex = 2;
            this.tabPageUpdate.Text = "Update";
            this.tabPageUpdate.UseVisualStyleBackColor = true;
            // 
            // FormTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(779, 325);
            this.Controls.Add(this.tabControlSettings);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBoxSRVTracker);
            this.Controls.Add(this.checkBoxTrack);
            this.Controls.Add(this.checkBoxUpload);
            this.Controls.Add(this.buttonShowConfig);
            this.Controls.Add(this.buttonRaceTracker);
            this.Controls.Add(this.buttonRoutePlanner);
            this.Controls.Add(this.buttonLocator);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormTracker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Vehicle Tracker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTracker_FormClosing);
            this.groupBoxStatusLocation.ResumeLayout(false);
            this.groupBoxStatusLocation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusFileWatcher)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUseSmartHeadingOnlyWhenOver)).EndInit();
            this.groupBoxSRVTracker.ResumeLayout(false);
            this.groupBoxSRVTracker.PerformLayout();
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageCommander.ResumeLayout(false);
            this.tabPageCommander.PerformLayout();
            this.tabPageMonitoring.ResumeLayout(false);
            this.tabPageMonitoring.PerformLayout();
            this.tabPageServer.ResumeLayout(false);
            this.tabPageServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUdpUploadPort)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageLogging.ResumeLayout(false);
            this.tabPageUpdate.ResumeLayout(false);
            this.tabPageUpdate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxStatusLocation;
        private System.Windows.Forms.Button buttonBrowseStatusFile;
        private System.Windows.Forms.TextBox textBoxStatusFile;
        private System.IO.FileSystemWatcher statusFileWatcher;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Label labelLastUpdateTime;
        private System.Windows.Forms.TextBox textBoxTelemetryFolder;
        private System.Windows.Forms.CheckBox checkBoxSaveTelemetryFolder;
        private System.Windows.Forms.TextBox textBoxUploadServer;
        private System.Windows.Forms.CheckBox checkBoxUpload;
        private System.Windows.Forms.TextBox textBoxCommanderName;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonShowConfig;
        private System.Windows.Forms.Button buttonRoutePlanner;
        private System.Windows.Forms.Button buttonLocator;
        private System.Windows.Forms.RadioButton radioButtonUseCustomServer;
        private System.Windows.Forms.RadioButton radioButtonUseDefaultServer;
        private System.Windows.Forms.Button buttonRaceTracker;
        private System.Windows.Forms.CheckBox checkBoxTrack;
        private System.Windows.Forms.RadioButton radioButtonWatchStatusFile;
        private System.Windows.Forms.RadioButton radioButtonUseTimer;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBoxAutoUpdate;
        private System.Windows.Forms.CheckBox checkBoxIncludeBetaUpdates;
        private System.Windows.Forms.CheckBox checkBoxUseDirectionOfTravelAsHeading;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxSRVTracker;
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabPageMonitoring;
        private System.Windows.Forms.TabPage tabPageLogging;
        private System.Windows.Forms.TabPage tabPageUpdate;
        private System.Windows.Forms.TabPage tabPageCommander;
        private System.Windows.Forms.TabPage tabPageServer;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxCaptureSRVTelemetry;
        private System.Windows.Forms.Button buttonNewSession;
        private System.Windows.Forms.Button buttonBrowseTelemetryFolder;
        private System.Windows.Forms.CheckBox checkBoxExportSRVTelemetry;
        private System.Windows.Forms.Button buttonSRVTelemetryExportSettings;
        private System.Windows.Forms.CheckBox checkBoxShowSRVTelemetry;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxClientId;
        private System.Windows.Forms.Button buttonUpdateName;
        private System.Windows.Forms.RadioButton radioButtonShipTelemetry;
        private System.Windows.Forms.RadioButton radioButtonSRVTelemetry;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownUdpUploadPort;
        private System.Windows.Forms.CheckBox checkBoxUseSmartHeadingOnlyWhenInSRV;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownUseSmartHeadingOnlyWhenOver;
        private System.Windows.Forms.CheckBox checkBoxUseSmartHeadingOnlyWhenOver;
        private System.Windows.Forms.CheckBox checkBoxAllowMischief;
        private System.Windows.Forms.Button buttonRaceTelemetrySettings;
        private System.Windows.Forms.CheckBox checkBoxShowRaceTelemetry;
        private System.Windows.Forms.Button buttonRacerProfile;
    }
}

