namespace SRVTracker
{
    partial class FormRaceManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRaceManager));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxWaypoints = new System.Windows.Forms.ListBox();
            this.buttonLoadRoute = new System.Windows.Forms.Button();
            this.textBoxRouteName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonLoadRace = new System.Windows.Forms.Button();
            this.buttonSaveRaceAs = new System.Windows.Forms.Button();
            this.buttonSaveRace = new System.Windows.Forms.Button();
            this.textBoxRaceName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPlanet = new System.Windows.Forms.TextBox();
            this.textBoxSystem = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonUneliminate = new System.Windows.Forms.Button();
            this.buttonRaceHistory = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonEditStatusMessages = new System.Windows.Forms.Button();
            this.groupBoxAddCommander = new System.Windows.Forms.GroupBox();
            this.comboBoxAddCommander = new System.Windows.Forms.ComboBox();
            this.buttonAddCommander = new System.Windows.Forms.Button();
            this.buttonStopRace = new System.Windows.Forms.Button();
            this.buttonStartRace = new System.Windows.Forms.Button();
            this.buttonTrackParticipant = new System.Windows.Forms.Button();
            this.buttonRemoveParticipant = new System.Windows.Forms.Button();
            this.buttonAddParticipant = new System.Windows.Forms.Button();
            this.listViewParticipants = new System.Windows.Forms.ListView();
            this.columnHeaderPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDistanceToWaypoint = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderHull = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxRaceManagement = new System.Windows.Forms.GroupBox();
            this.checkBoxServerMonitoring = new System.Windows.Forms.CheckBox();
            this.checkBoxShowDetailedStatus = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoAddCommanders = new System.Windows.Forms.CheckBox();
            this.checkBoxExportDistance = new System.Windows.Forms.CheckBox();
            this.numericUpDownLeaderboardMaxLength = new System.Windows.Forms.NumericUpDown();
            this.checkBoxExportLeaderboard = new System.Windows.Forms.CheckBox();
            this.checkBoxEliminationOnDestruction = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.checkBoxStreamInfo = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowPitstops = new System.Windows.Forms.CheckBox();
            this.checkBoxSRVRace = new System.Windows.Forms.CheckBox();
            this.checkBoxClosestPlayerTarget = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownStatusMaxLength = new System.Windows.Forms.NumericUpDown();
            this.textBoxExportTargetFile = new System.Windows.Forms.TextBox();
            this.checkBoxExportTrackedTarget = new System.Windows.Forms.CheckBox();
            this.textBoxExportStatusFile = new System.Windows.Forms.TextBox();
            this.textBoxExportLeaderboardFile = new System.Windows.Forms.TextBox();
            this.checkBoxExportStatus = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxExportSpeedFile = new System.Windows.Forms.TextBox();
            this.checkBoxExportSpeed = new System.Windows.Forms.CheckBox();
            this.groupBoxTextExport = new System.Windows.Forms.GroupBox();
            this.textBoxExportTotalDistanceLeftFile = new System.Windows.Forms.TextBox();
            this.checkBoxExportTotalDistanceLeft = new System.Windows.Forms.CheckBox();
            this.textBoxExportHullFile = new System.Windows.Forms.TextBox();
            this.checkBoxExportHull = new System.Windows.Forms.CheckBox();
            this.textBoxExportWaypointDistanceFile = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownNotableEventDuration = new System.Windows.Forms.NumericUpDown();
            this.textBoxNotableEventsFile = new System.Windows.Forms.TextBox();
            this.checkBoxExportNotableEvents = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeMaxSpeed = new System.Windows.Forms.CheckBox();
            this.groupBoxHTMLExport = new System.Windows.Forms.GroupBox();
            this.checkBoxExportAsHTML = new System.Windows.Forms.CheckBox();
            this.textBoxExportHTMLTo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxHTMLTemplateFile = new System.Windows.Forms.TextBox();
            this.groupBoxTrackTarget = new System.Windows.Forms.GroupBox();
            this.textBoxExportTargetAverageSpeedFile = new System.Windows.Forms.TextBox();
            this.checkBoxExportTrackedTargetAverageSpeed = new System.Windows.Forms.CheckBox();
            this.textBoxExportTargetDistance = new System.Windows.Forms.TextBox();
            this.checkBoxExportTrackedTargetDistance = new System.Windows.Forms.CheckBox();
            this.textBoxExportTargetHull = new System.Windows.Forms.TextBox();
            this.checkBoxExportTrackedTargetHull = new System.Windows.Forms.CheckBox();
            this.textBoxExportTargetPosition = new System.Windows.Forms.TextBox();
            this.checkBoxExportTrackedTargetPosition = new System.Windows.Forms.CheckBox();
            this.textBoxExportTargetSpeedFile = new System.Windows.Forms.TextBox();
            this.checkBoxExportTrackedTargetSpeed = new System.Windows.Forms.CheckBox();
            this.checkBoxExportTrackedTargetPitstops = new System.Windows.Forms.CheckBox();
            this.textBoxExportTargetPitstopsFile = new System.Windows.Forms.TextBox();
            this.checkBoxExportTrackedTargetMaxSpeed = new System.Windows.Forms.CheckBox();
            this.textBoxExportTargetMaxSpeedFile = new System.Windows.Forms.TextBox();
            this.timerRefreshFromServer = new System.Windows.Forms.Timer(this.components);
            this.groupBoxServerInfo = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxServerRaceGuid = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxRaceStatusServerUrl = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timerPreraceExport = new System.Windows.Forms.Timer(this.components);
            this.numericUpDownLapCount = new System.Windows.Forms.NumericUpDown();
            this.checkBoxLappedRace = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxAddCommander.SuspendLayout();
            this.groupBoxRaceManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeaderboardMaxLength)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStatusMaxLength)).BeginInit();
            this.groupBoxTextExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNotableEventDuration)).BeginInit();
            this.groupBoxHTMLExport.SuspendLayout();
            this.groupBoxTrackTarget.SuspendLayout();
            this.groupBoxServerInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLapCount)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxWaypoints);
            this.groupBox1.Controls.Add(this.buttonLoadRoute);
            this.groupBox1.Controls.Add(this.textBoxRouteName);
            this.groupBox1.Location = new System.Drawing.Point(408, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 183);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Route";
            // 
            // listBoxWaypoints
            // 
            this.listBoxWaypoints.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxWaypoints.FormattingEnabled = true;
            this.listBoxWaypoints.Location = new System.Drawing.Point(6, 45);
            this.listBoxWaypoints.Name = "listBoxWaypoints";
            this.listBoxWaypoints.Size = new System.Drawing.Size(288, 134);
            this.listBoxWaypoints.TabIndex = 2;
            this.listBoxWaypoints.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBoxWaypoints_DrawItem);
            // 
            // buttonLoadRoute
            // 
            this.buttonLoadRoute.Image = global::SRVTracker.Properties.Resources.OpenFile_16x;
            this.buttonLoadRoute.Location = new System.Drawing.Point(256, 17);
            this.buttonLoadRoute.Name = "buttonLoadRoute";
            this.buttonLoadRoute.Size = new System.Drawing.Size(38, 23);
            this.buttonLoadRoute.TabIndex = 1;
            this.toolTip1.SetToolTip(this.buttonLoadRoute, "Load route from file");
            this.buttonLoadRoute.UseVisualStyleBackColor = true;
            this.buttonLoadRoute.Click += new System.EventHandler(this.buttonLoadRoute_Click);
            // 
            // textBoxRouteName
            // 
            this.textBoxRouteName.Location = new System.Drawing.Point(6, 19);
            this.textBoxRouteName.Name = "textBoxRouteName";
            this.textBoxRouteName.ReadOnly = true;
            this.textBoxRouteName.Size = new System.Drawing.Size(244, 20);
            this.textBoxRouteName.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonLoadRace);
            this.groupBox2.Controls.Add(this.buttonSaveRaceAs);
            this.groupBox2.Controls.Add(this.buttonSaveRace);
            this.groupBox2.Controls.Add(this.textBoxRaceName);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(396, 48);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Event";
            // 
            // buttonLoadRace
            // 
            this.buttonLoadRace.Image = global::SRVTracker.Properties.Resources.OpenFile_16x;
            this.buttonLoadRace.Location = new System.Drawing.Point(291, 16);
            this.buttonLoadRace.Name = "buttonLoadRace";
            this.buttonLoadRace.Size = new System.Drawing.Size(29, 23);
            this.buttonLoadRace.TabIndex = 10;
            this.toolTip1.SetToolTip(this.buttonLoadRace, "Load race from file");
            this.buttonLoadRace.UseVisualStyleBackColor = true;
            this.buttonLoadRace.Click += new System.EventHandler(this.buttonLoadRace_Click);
            // 
            // buttonSaveRaceAs
            // 
            this.buttonSaveRaceAs.Image = global::SRVTracker.Properties.Resources.SaveAs_16x;
            this.buttonSaveRaceAs.Location = new System.Drawing.Point(326, 16);
            this.buttonSaveRaceAs.Name = "buttonSaveRaceAs";
            this.buttonSaveRaceAs.Size = new System.Drawing.Size(29, 23);
            this.buttonSaveRaceAs.TabIndex = 9;
            this.toolTip1.SetToolTip(this.buttonSaveRaceAs, "Save race as...");
            this.buttonSaveRaceAs.UseVisualStyleBackColor = true;
            this.buttonSaveRaceAs.Click += new System.EventHandler(this.buttonSaveRaceAs_Click);
            // 
            // buttonSaveRace
            // 
            this.buttonSaveRace.Image = global::SRVTracker.Properties.Resources.Save_16x;
            this.buttonSaveRace.Location = new System.Drawing.Point(361, 16);
            this.buttonSaveRace.Name = "buttonSaveRace";
            this.buttonSaveRace.Size = new System.Drawing.Size(29, 23);
            this.buttonSaveRace.TabIndex = 8;
            this.toolTip1.SetToolTip(this.buttonSaveRace, "Save race");
            this.buttonSaveRace.UseVisualStyleBackColor = true;
            this.buttonSaveRace.Click += new System.EventHandler(this.buttonSaveRace_Click);
            // 
            // textBoxRaceName
            // 
            this.textBoxRaceName.Location = new System.Drawing.Point(6, 19);
            this.textBoxRaceName.Name = "textBoxRaceName";
            this.textBoxRaceName.Size = new System.Drawing.Size(279, 20);
            this.textBoxRaceName.TabIndex = 2;
            this.textBoxRaceName.TextChanged += new System.EventHandler(this.textBoxRaceName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Planet:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "System:";
            // 
            // textBoxPlanet
            // 
            this.textBoxPlanet.Location = new System.Drawing.Point(56, 45);
            this.textBoxPlanet.Name = "textBoxPlanet";
            this.textBoxPlanet.ReadOnly = true;
            this.textBoxPlanet.Size = new System.Drawing.Size(237, 20);
            this.textBoxPlanet.TabIndex = 1;
            // 
            // textBoxSystem
            // 
            this.textBoxSystem.Location = new System.Drawing.Point(56, 19);
            this.textBoxSystem.Name = "textBoxSystem";
            this.textBoxSystem.ReadOnly = true;
            this.textBoxSystem.Size = new System.Drawing.Size(237, 20);
            this.textBoxSystem.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonUneliminate);
            this.groupBox3.Controls.Add(this.buttonRaceHistory);
            this.groupBox3.Controls.Add(this.buttonReset);
            this.groupBox3.Controls.Add(this.buttonEditStatusMessages);
            this.groupBox3.Controls.Add(this.groupBoxAddCommander);
            this.groupBox3.Controls.Add(this.buttonStopRace);
            this.groupBox3.Controls.Add(this.buttonStartRace);
            this.groupBox3.Controls.Add(this.buttonTrackParticipant);
            this.groupBox3.Controls.Add(this.buttonRemoveParticipant);
            this.groupBox3.Controls.Add(this.buttonAddParticipant);
            this.groupBox3.Controls.Add(this.listViewParticipants);
            this.groupBox3.Location = new System.Drawing.Point(6, 60);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(396, 347);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Participants";
            // 
            // buttonUneliminate
            // 
            this.buttonUneliminate.Image = global::SRVTracker.Properties.Resources.AdvancedBreakpointDisabled_16x;
            this.buttonUneliminate.Location = new System.Drawing.Point(361, 231);
            this.buttonUneliminate.Name = "buttonUneliminate";
            this.buttonUneliminate.Size = new System.Drawing.Size(29, 23);
            this.buttonUneliminate.TabIndex = 10;
            this.toolTip1.SetToolTip(this.buttonUneliminate, "Restore elimated player back into the race");
            this.buttonUneliminate.UseVisualStyleBackColor = true;
            this.buttonUneliminate.Click += new System.EventHandler(this.buttonUneliminate_Click);
            // 
            // buttonRaceHistory
            // 
            this.buttonRaceHistory.Enabled = false;
            this.buttonRaceHistory.Image = global::SRVTracker.Properties.Resources.History_16x;
            this.buttonRaceHistory.Location = new System.Drawing.Point(361, 202);
            this.buttonRaceHistory.Name = "buttonRaceHistory";
            this.buttonRaceHistory.Size = new System.Drawing.Size(29, 23);
            this.buttonRaceHistory.TabIndex = 9;
            this.toolTip1.SetToolTip(this.buttonRaceHistory, "Show race history");
            this.buttonRaceHistory.UseVisualStyleBackColor = true;
            this.buttonRaceHistory.Click += new System.EventHandler(this.buttonRaceHistory_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Enabled = false;
            this.buttonReset.Image = global::SRVTracker.Properties.Resources.Restart_16x;
            this.buttonReset.Location = new System.Drawing.Point(361, 318);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(29, 23);
            this.buttonReset.TabIndex = 8;
            this.toolTip1.SetToolTip(this.buttonReset, "Reset race");
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonEditStatusMessages
            // 
            this.buttonEditStatusMessages.Image = global::SRVTracker.Properties.Resources.Text_16x;
            this.buttonEditStatusMessages.Location = new System.Drawing.Point(361, 139);
            this.buttonEditStatusMessages.Name = "buttonEditStatusMessages";
            this.buttonEditStatusMessages.Size = new System.Drawing.Size(29, 23);
            this.buttonEditStatusMessages.TabIndex = 7;
            this.toolTip1.SetToolTip(this.buttonEditStatusMessages, "Edit status messages");
            this.buttonEditStatusMessages.UseVisualStyleBackColor = true;
            this.buttonEditStatusMessages.Click += new System.EventHandler(this.buttonEditStatusMessages_Click);
            // 
            // groupBoxAddCommander
            // 
            this.groupBoxAddCommander.Controls.Add(this.comboBoxAddCommander);
            this.groupBoxAddCommander.Controls.Add(this.buttonAddCommander);
            this.groupBoxAddCommander.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBoxAddCommander.Location = new System.Drawing.Point(106, 45);
            this.groupBoxAddCommander.Name = "groupBoxAddCommander";
            this.groupBoxAddCommander.Size = new System.Drawing.Size(284, 51);
            this.groupBoxAddCommander.TabIndex = 5;
            this.groupBoxAddCommander.TabStop = false;
            this.groupBoxAddCommander.Text = "Add Commander";
            // 
            // comboBoxAddCommander
            // 
            this.comboBoxAddCommander.FormattingEnabled = true;
            this.comboBoxAddCommander.Location = new System.Drawing.Point(6, 18);
            this.comboBoxAddCommander.Name = "comboBoxAddCommander";
            this.comboBoxAddCommander.Size = new System.Drawing.Size(237, 21);
            this.comboBoxAddCommander.TabIndex = 9;
            this.comboBoxAddCommander.SelectedIndexChanged += new System.EventHandler(this.comboBoxAddCommander_SelectedIndexChanged);
            this.comboBoxAddCommander.Leave += new System.EventHandler(this.comboBoxAddCommander_Leave);
            // 
            // buttonAddCommander
            // 
            this.buttonAddCommander.Image = global::SRVTracker.Properties.Resources.Return_16x;
            this.buttonAddCommander.Location = new System.Drawing.Point(249, 16);
            this.buttonAddCommander.Name = "buttonAddCommander";
            this.buttonAddCommander.Size = new System.Drawing.Size(29, 23);
            this.buttonAddCommander.TabIndex = 8;
            this.buttonAddCommander.UseVisualStyleBackColor = true;
            this.buttonAddCommander.Click += new System.EventHandler(this.buttonAddCommander_Click);
            // 
            // buttonStopRace
            // 
            this.buttonStopRace.Enabled = false;
            this.buttonStopRace.Image = global::SRVTracker.Properties.Resources.Stop_16x;
            this.buttonStopRace.Location = new System.Drawing.Point(361, 289);
            this.buttonStopRace.Name = "buttonStopRace";
            this.buttonStopRace.Size = new System.Drawing.Size(29, 23);
            this.buttonStopRace.TabIndex = 6;
            this.toolTip1.SetToolTip(this.buttonStopRace, "Stop race");
            this.buttonStopRace.UseVisualStyleBackColor = true;
            this.buttonStopRace.Click += new System.EventHandler(this.buttonStopRace_Click);
            // 
            // buttonStartRace
            // 
            this.buttonStartRace.Enabled = false;
            this.buttonStartRace.Image = global::SRVTracker.Properties.Resources.Run_16x;
            this.buttonStartRace.Location = new System.Drawing.Point(361, 260);
            this.buttonStartRace.Name = "buttonStartRace";
            this.buttonStartRace.Size = new System.Drawing.Size(29, 23);
            this.buttonStartRace.TabIndex = 5;
            this.toolTip1.SetToolTip(this.buttonStartRace, "Start race");
            this.buttonStartRace.UseVisualStyleBackColor = true;
            this.buttonStartRace.Click += new System.EventHandler(this.buttonStartRace_Click);
            // 
            // buttonTrackParticipant
            // 
            this.buttonTrackParticipant.Image = global::SRVTracker.Properties.Resources.Target_16x;
            this.buttonTrackParticipant.Location = new System.Drawing.Point(361, 77);
            this.buttonTrackParticipant.Name = "buttonTrackParticipant";
            this.buttonTrackParticipant.Size = new System.Drawing.Size(29, 23);
            this.buttonTrackParticipant.TabIndex = 3;
            this.toolTip1.SetToolTip(this.buttonTrackParticipant, "Track participant");
            this.buttonTrackParticipant.UseVisualStyleBackColor = true;
            this.buttonTrackParticipant.Click += new System.EventHandler(this.buttonTrackParticipant_Click);
            // 
            // buttonRemoveParticipant
            // 
            this.buttonRemoveParticipant.Image = global::SRVTracker.Properties.Resources.Remove_color_16x;
            this.buttonRemoveParticipant.Location = new System.Drawing.Point(361, 48);
            this.buttonRemoveParticipant.Name = "buttonRemoveParticipant";
            this.buttonRemoveParticipant.Size = new System.Drawing.Size(29, 23);
            this.buttonRemoveParticipant.TabIndex = 2;
            this.toolTip1.SetToolTip(this.buttonRemoveParticipant, "Remove participant");
            this.buttonRemoveParticipant.UseVisualStyleBackColor = true;
            this.buttonRemoveParticipant.Click += new System.EventHandler(this.buttonRemoveParticipant_Click);
            // 
            // buttonAddParticipant
            // 
            this.buttonAddParticipant.Image = global::SRVTracker.Properties.Resources.Add_16x;
            this.buttonAddParticipant.Location = new System.Drawing.Point(361, 19);
            this.buttonAddParticipant.Name = "buttonAddParticipant";
            this.buttonAddParticipant.Size = new System.Drawing.Size(29, 23);
            this.buttonAddParticipant.TabIndex = 1;
            this.toolTip1.SetToolTip(this.buttonAddParticipant, "Add commander");
            this.buttonAddParticipant.UseVisualStyleBackColor = true;
            this.buttonAddParticipant.Click += new System.EventHandler(this.buttonAddParticipant_Click);
            // 
            // listViewParticipants
            // 
            this.listViewParticipants.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPosition,
            this.columnHeaderName,
            this.columnHeaderStatus,
            this.columnHeaderDistanceToWaypoint,
            this.columnHeaderHull});
            this.listViewParticipants.FullRowSelect = true;
            this.listViewParticipants.GridLines = true;
            this.listViewParticipants.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewParticipants.HideSelection = false;
            this.listViewParticipants.LabelWrap = false;
            this.listViewParticipants.Location = new System.Drawing.Point(6, 19);
            this.listViewParticipants.MultiSelect = false;
            this.listViewParticipants.Name = "listViewParticipants";
            this.listViewParticipants.Size = new System.Drawing.Size(349, 322);
            this.listViewParticipants.TabIndex = 0;
            this.listViewParticipants.UseCompatibleStateImageBehavior = false;
            this.listViewParticipants.View = System.Windows.Forms.View.Details;
            this.listViewParticipants.SelectedIndexChanged += new System.EventHandler(this.listViewParticipants_SelectedIndexChanged);
            // 
            // columnHeaderPosition
            // 
            this.columnHeaderPosition.Text = "Pos";
            this.columnHeaderPosition.Width = 30;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Commander";
            this.columnHeaderName.Width = 100;
            // 
            // columnHeaderStatus
            // 
            this.columnHeaderStatus.Text = "Status";
            this.columnHeaderStatus.Width = 120;
            // 
            // columnHeaderDistanceToWaypoint
            // 
            this.columnHeaderDistanceToWaypoint.Text = "WP";
            this.columnHeaderDistanceToWaypoint.Width = 45;
            // 
            // columnHeaderHull
            // 
            this.columnHeaderHull.Text = "Hull";
            this.columnHeaderHull.Width = 45;
            // 
            // groupBoxRaceManagement
            // 
            this.groupBoxRaceManagement.Controls.Add(this.checkBoxServerMonitoring);
            this.groupBoxRaceManagement.Controls.Add(this.checkBoxShowDetailedStatus);
            this.groupBoxRaceManagement.Controls.Add(this.checkBoxAutoAddCommanders);
            this.groupBoxRaceManagement.Location = new System.Drawing.Point(408, 343);
            this.groupBoxRaceManagement.Name = "groupBoxRaceManagement";
            this.groupBoxRaceManagement.Size = new System.Drawing.Size(300, 70);
            this.groupBoxRaceManagement.TabIndex = 3;
            this.groupBoxRaceManagement.TabStop = false;
            this.groupBoxRaceManagement.Text = "Race Management";
            // 
            // checkBoxServerMonitoring
            // 
            this.checkBoxServerMonitoring.AutoSize = true;
            this.checkBoxServerMonitoring.Location = new System.Drawing.Point(140, 42);
            this.checkBoxServerMonitoring.Name = "checkBoxServerMonitoring";
            this.checkBoxServerMonitoring.Size = new System.Drawing.Size(125, 17);
            this.checkBoxServerMonitoring.TabIndex = 6;
            this.checkBoxServerMonitoring.Text = "Track race on server";
            this.toolTip1.SetToolTip(this.checkBoxServerMonitoring, "When selected, the race and participants are uploaded to the server on start.\r\nMo" +
        "nitoring occurs on the server, and the client updates twice per second.\r\n");
            this.checkBoxServerMonitoring.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowDetailedStatus
            // 
            this.checkBoxShowDetailedStatus.AutoSize = true;
            this.checkBoxShowDetailedStatus.Enabled = false;
            this.checkBoxShowDetailedStatus.Location = new System.Drawing.Point(6, 42);
            this.checkBoxShowDetailedStatus.Name = "checkBoxShowDetailedStatus";
            this.checkBoxShowDetailedStatus.Size = new System.Drawing.Size(128, 17);
            this.checkBoxShowDetailedStatus.TabIndex = 5;
            this.checkBoxShowDetailedStatus.Text = "Show Detailed Status";
            this.toolTip1.SetToolTip(this.checkBoxShowDetailedStatus, "Show info such as NV (night vision), LL (lights low)");
            this.checkBoxShowDetailedStatus.UseVisualStyleBackColor = true;
            this.checkBoxShowDetailedStatus.CheckedChanged += new System.EventHandler(this.checkBoxShowDetailedStatus_CheckedChanged);
            // 
            // checkBoxAutoAddCommanders
            // 
            this.checkBoxAutoAddCommanders.AutoSize = true;
            this.checkBoxAutoAddCommanders.Checked = true;
            this.checkBoxAutoAddCommanders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoAddCommanders.Location = new System.Drawing.Point(6, 19);
            this.checkBoxAutoAddCommanders.Name = "checkBoxAutoAddCommanders";
            this.checkBoxAutoAddCommanders.Size = new System.Drawing.Size(287, 17);
            this.checkBoxAutoAddCommanders.TabIndex = 4;
            this.checkBoxAutoAddCommanders.Text = "Automatically add commanders that are at first waypoint";
            this.checkBoxAutoAddCommanders.UseVisualStyleBackColor = true;
            // 
            // checkBoxExportDistance
            // 
            this.checkBoxExportDistance.AutoSize = true;
            this.checkBoxExportDistance.Location = new System.Drawing.Point(6, 134);
            this.checkBoxExportDistance.Name = "checkBoxExportDistance";
            this.checkBoxExportDistance.Size = new System.Drawing.Size(117, 17);
            this.checkBoxExportDistance.TabIndex = 6;
            this.checkBoxExportDistance.Text = "Waypoint distance:";
            this.checkBoxExportDistance.UseVisualStyleBackColor = true;
            this.checkBoxExportDistance.CheckedChanged += new System.EventHandler(this.checkBoxExportDistance_CheckedChanged);
            // 
            // numericUpDownLeaderboardMaxLength
            // 
            this.numericUpDownLeaderboardMaxLength.Location = new System.Drawing.Point(179, 37);
            this.numericUpDownLeaderboardMaxLength.Name = "numericUpDownLeaderboardMaxLength";
            this.numericUpDownLeaderboardMaxLength.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownLeaderboardMaxLength.TabIndex = 3;
            this.numericUpDownLeaderboardMaxLength.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // checkBoxExportLeaderboard
            // 
            this.checkBoxExportLeaderboard.AutoSize = true;
            this.checkBoxExportLeaderboard.Location = new System.Drawing.Point(6, 19);
            this.checkBoxExportLeaderboard.Name = "checkBoxExportLeaderboard";
            this.checkBoxExportLeaderboard.Size = new System.Drawing.Size(89, 17);
            this.checkBoxExportLeaderboard.TabIndex = 1;
            this.checkBoxExportLeaderboard.Text = "Leaderboard:";
            this.checkBoxExportLeaderboard.UseVisualStyleBackColor = true;
            this.checkBoxExportLeaderboard.CheckedChanged += new System.EventHandler(this.checkBoxExportLeaderboard_CheckedChanged);
            // 
            // checkBoxEliminationOnDestruction
            // 
            this.checkBoxEliminationOnDestruction.AutoSize = true;
            this.checkBoxEliminationOnDestruction.Checked = true;
            this.checkBoxEliminationOnDestruction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEliminationOnDestruction.Location = new System.Drawing.Point(9, 19);
            this.checkBoxEliminationOnDestruction.Name = "checkBoxEliminationOnDestruction";
            this.checkBoxEliminationOnDestruction.Size = new System.Drawing.Size(183, 17);
            this.checkBoxEliminationOnDestruction.TabIndex = 0;
            this.checkBoxEliminationOnDestruction.Text = "Elimination on vehicle destruction";
            this.toolTip1.SetToolTip(this.checkBoxEliminationOnDestruction, "Vehicle destruction results in end of race for that commander");
            this.checkBoxEliminationOnDestruction.UseVisualStyleBackColor = true;
            this.checkBoxEliminationOnDestruction.CheckedChanged += new System.EventHandler(this.checkBoxEliminationOnDestruction_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.textBoxSystem);
            this.groupBox5.Controls.Add(this.textBoxPlanet);
            this.groupBox5.Location = new System.Drawing.Point(408, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(300, 73);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Location";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.checkBoxLappedRace);
            this.groupBox6.Controls.Add(this.numericUpDownLapCount);
            this.groupBox6.Controls.Add(this.checkBoxStreamInfo);
            this.groupBox6.Controls.Add(this.checkBoxAllowPitstops);
            this.groupBox6.Controls.Add(this.checkBoxSRVRace);
            this.groupBox6.Controls.Add(this.checkBoxEliminationOnDestruction);
            this.groupBox6.Location = new System.Drawing.Point(408, 274);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(300, 63);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Race Settings";
            // 
            // checkBoxStreamInfo
            // 
            this.checkBoxStreamInfo.AutoSize = true;
            this.checkBoxStreamInfo.Location = new System.Drawing.Point(198, 19);
            this.checkBoxStreamInfo.Name = "checkBoxStreamInfo";
            this.checkBoxStreamInfo.Size = new System.Drawing.Size(79, 17);
            this.checkBoxStreamInfo.TabIndex = 3;
            this.checkBoxStreamInfo.Text = "Stream info";
            this.checkBoxStreamInfo.UseVisualStyleBackColor = true;
            this.checkBoxStreamInfo.CheckedChanged += new System.EventHandler(this.checkBoxStreamInfo_CheckedChanged);
            // 
            // checkBoxAllowPitstops
            // 
            this.checkBoxAllowPitstops.AutoSize = true;
            this.checkBoxAllowPitstops.Checked = true;
            this.checkBoxAllowPitstops.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAllowPitstops.Location = new System.Drawing.Point(85, 42);
            this.checkBoxAllowPitstops.Name = "checkBoxAllowPitstops";
            this.checkBoxAllowPitstops.Size = new System.Drawing.Size(90, 17);
            this.checkBoxAllowPitstops.TabIndex = 2;
            this.checkBoxAllowPitstops.Text = "Allow pitstops";
            this.toolTip1.SetToolTip(this.checkBoxAllowPitstops, "Allows commanders to board their ship for repairs during the race");
            this.checkBoxAllowPitstops.UseVisualStyleBackColor = true;
            this.checkBoxAllowPitstops.CheckedChanged += new System.EventHandler(this.checkBoxAllowPitstops_CheckedChanged);
            // 
            // checkBoxSRVRace
            // 
            this.checkBoxSRVRace.AutoSize = true;
            this.checkBoxSRVRace.Checked = true;
            this.checkBoxSRVRace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSRVRace.Enabled = false;
            this.checkBoxSRVRace.Location = new System.Drawing.Point(9, 42);
            this.checkBoxSRVRace.Name = "checkBoxSRVRace";
            this.checkBoxSRVRace.Size = new System.Drawing.Size(70, 17);
            this.checkBoxSRVRace.TabIndex = 1;
            this.checkBoxSRVRace.Text = "SRV only";
            this.toolTip1.SetToolTip(this.checkBoxSRVRace, "If enabled, competitors will be eliminated if they use their ship");
            this.checkBoxSRVRace.UseVisualStyleBackColor = true;
            this.checkBoxSRVRace.CheckedChanged += new System.EventHandler(this.checkBoxSRVRace_CheckedChanged);
            // 
            // checkBoxClosestPlayerTarget
            // 
            this.checkBoxClosestPlayerTarget.AutoSize = true;
            this.checkBoxClosestPlayerTarget.Checked = true;
            this.checkBoxClosestPlayerTarget.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxClosestPlayerTarget.Location = new System.Drawing.Point(6, 19);
            this.checkBoxClosestPlayerTarget.Name = "checkBoxClosestPlayerTarget";
            this.checkBoxClosestPlayerTarget.Size = new System.Drawing.Size(202, 17);
            this.checkBoxClosestPlayerTarget.TabIndex = 18;
            this.checkBoxClosestPlayerTarget.Text = "Always export closest player as target";
            this.checkBoxClosestPlayerTarget.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(176, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Max length";
            // 
            // numericUpDownStatusMaxLength
            // 
            this.numericUpDownStatusMaxLength.Location = new System.Drawing.Point(179, 64);
            this.numericUpDownStatusMaxLength.Name = "numericUpDownStatusMaxLength";
            this.numericUpDownStatusMaxLength.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownStatusMaxLength.TabIndex = 15;
            this.numericUpDownStatusMaxLength.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // textBoxExportTargetFile
            // 
            this.textBoxExportTargetFile.Location = new System.Drawing.Point(134, 40);
            this.textBoxExportTargetFile.Name = "textBoxExportTargetFile";
            this.textBoxExportTargetFile.Size = new System.Drawing.Size(105, 20);
            this.textBoxExportTargetFile.TabIndex = 11;
            this.textBoxExportTargetFile.Text = "Tracking-Name.txt";
            // 
            // checkBoxExportTrackedTarget
            // 
            this.checkBoxExportTrackedTarget.AutoSize = true;
            this.checkBoxExportTrackedTarget.Location = new System.Drawing.Point(6, 42);
            this.checkBoxExportTrackedTarget.Name = "checkBoxExportTrackedTarget";
            this.checkBoxExportTrackedTarget.Size = new System.Drawing.Size(89, 17);
            this.checkBoxExportTrackedTarget.TabIndex = 10;
            this.checkBoxExportTrackedTarget.Text = "Export target:";
            this.checkBoxExportTrackedTarget.UseVisualStyleBackColor = true;
            this.checkBoxExportTrackedTarget.CheckedChanged += new System.EventHandler(this.checkBoxExportTarget_CheckedChanged);
            // 
            // textBoxExportStatusFile
            // 
            this.textBoxExportStatusFile.Location = new System.Drawing.Point(66, 63);
            this.textBoxExportStatusFile.Name = "textBoxExportStatusFile";
            this.textBoxExportStatusFile.Size = new System.Drawing.Size(107, 20);
            this.textBoxExportStatusFile.TabIndex = 9;
            this.textBoxExportStatusFile.Text = "Statuses.txt";
            // 
            // textBoxExportLeaderboardFile
            // 
            this.textBoxExportLeaderboardFile.Location = new System.Drawing.Point(66, 37);
            this.textBoxExportLeaderboardFile.Name = "textBoxExportLeaderboardFile";
            this.textBoxExportLeaderboardFile.Size = new System.Drawing.Size(107, 20);
            this.textBoxExportLeaderboardFile.TabIndex = 8;
            this.textBoxExportLeaderboardFile.Text = "Names-Position.txt";
            // 
            // checkBoxExportStatus
            // 
            this.checkBoxExportStatus.AutoSize = true;
            this.checkBoxExportStatus.Location = new System.Drawing.Point(6, 65);
            this.checkBoxExportStatus.Name = "checkBoxExportStatus";
            this.checkBoxExportStatus.Size = new System.Drawing.Size(59, 17);
            this.checkBoxExportStatus.TabIndex = 7;
            this.checkBoxExportStatus.Text = "Status:";
            this.checkBoxExportStatus.UseVisualStyleBackColor = true;
            this.checkBoxExportStatus.CheckedChanged += new System.EventHandler(this.checkBoxExportStatus_CheckedChanged);
            // 
            // textBoxExportSpeedFile
            // 
            this.textBoxExportSpeedFile.Location = new System.Drawing.Point(125, 210);
            this.textBoxExportSpeedFile.Name = "textBoxExportSpeedFile";
            this.textBoxExportSpeedFile.Size = new System.Drawing.Size(104, 20);
            this.textBoxExportSpeedFile.TabIndex = 20;
            this.textBoxExportSpeedFile.Text = "Speeds.txt";
            // 
            // checkBoxExportSpeed
            // 
            this.checkBoxExportSpeed.AutoSize = true;
            this.checkBoxExportSpeed.Location = new System.Drawing.Point(6, 212);
            this.checkBoxExportSpeed.Name = "checkBoxExportSpeed";
            this.checkBoxExportSpeed.Size = new System.Drawing.Size(60, 17);
            this.checkBoxExportSpeed.TabIndex = 19;
            this.checkBoxExportSpeed.Text = "Speed:";
            this.checkBoxExportSpeed.UseVisualStyleBackColor = true;
            this.checkBoxExportSpeed.CheckedChanged += new System.EventHandler(this.checkBoxExportSpeed_CheckedChanged);
            // 
            // groupBoxTextExport
            // 
            this.groupBoxTextExport.Controls.Add(this.textBoxExportTotalDistanceLeftFile);
            this.groupBoxTextExport.Controls.Add(this.checkBoxExportTotalDistanceLeft);
            this.groupBoxTextExport.Controls.Add(this.textBoxExportHullFile);
            this.groupBoxTextExport.Controls.Add(this.checkBoxExportHull);
            this.groupBoxTextExport.Controls.Add(this.textBoxExportWaypointDistanceFile);
            this.groupBoxTextExport.Controls.Add(this.label6);
            this.groupBoxTextExport.Controls.Add(this.numericUpDownNotableEventDuration);
            this.groupBoxTextExport.Controls.Add(this.textBoxNotableEventsFile);
            this.groupBoxTextExport.Controls.Add(this.checkBoxExportNotableEvents);
            this.groupBoxTextExport.Controls.Add(this.checkBoxExportLeaderboard);
            this.groupBoxTextExport.Controls.Add(this.numericUpDownLeaderboardMaxLength);
            this.groupBoxTextExport.Controls.Add(this.checkBoxExportDistance);
            this.groupBoxTextExport.Controls.Add(this.textBoxExportSpeedFile);
            this.groupBoxTextExport.Controls.Add(this.checkBoxExportStatus);
            this.groupBoxTextExport.Controls.Add(this.textBoxExportLeaderboardFile);
            this.groupBoxTextExport.Controls.Add(this.textBoxExportStatusFile);
            this.groupBoxTextExport.Controls.Add(this.label4);
            this.groupBoxTextExport.Controls.Add(this.numericUpDownStatusMaxLength);
            this.groupBoxTextExport.Controls.Add(this.checkBoxIncludeMaxSpeed);
            this.groupBoxTextExport.Controls.Add(this.checkBoxExportSpeed);
            this.groupBoxTextExport.Location = new System.Drawing.Point(714, 6);
            this.groupBoxTextExport.Name = "groupBoxTextExport";
            this.groupBoxTextExport.Size = new System.Drawing.Size(245, 251);
            this.groupBoxTextExport.TabIndex = 7;
            this.groupBoxTextExport.TabStop = false;
            this.groupBoxTextExport.Text = "Export as text";
            // 
            // textBoxExportTotalDistanceLeftFile
            // 
            this.textBoxExportTotalDistanceLeftFile.Location = new System.Drawing.Point(125, 158);
            this.textBoxExportTotalDistanceLeftFile.Name = "textBoxExportTotalDistanceLeftFile";
            this.textBoxExportTotalDistanceLeftFile.Size = new System.Drawing.Size(104, 20);
            this.textBoxExportTotalDistanceLeftFile.TabIndex = 31;
            this.textBoxExportTotalDistanceLeftFile.Text = "DistancesLeft.txt";
            // 
            // checkBoxExportTotalDistanceLeft
            // 
            this.checkBoxExportTotalDistanceLeft.AutoSize = true;
            this.checkBoxExportTotalDistanceLeft.Location = new System.Drawing.Point(6, 160);
            this.checkBoxExportTotalDistanceLeft.Name = "checkBoxExportTotalDistanceLeft";
            this.checkBoxExportTotalDistanceLeft.Size = new System.Drawing.Size(113, 17);
            this.checkBoxExportTotalDistanceLeft.TabIndex = 30;
            this.checkBoxExportTotalDistanceLeft.Text = "Total distance left:";
            this.checkBoxExportTotalDistanceLeft.UseVisualStyleBackColor = true;
            this.checkBoxExportTotalDistanceLeft.CheckedChanged += new System.EventHandler(this.checkBoxExportTotalDistanceLeft_CheckedChanged);
            // 
            // textBoxExportHullFile
            // 
            this.textBoxExportHullFile.Location = new System.Drawing.Point(125, 184);
            this.textBoxExportHullFile.Name = "textBoxExportHullFile";
            this.textBoxExportHullFile.Size = new System.Drawing.Size(104, 20);
            this.textBoxExportHullFile.TabIndex = 29;
            this.textBoxExportHullFile.Text = "Hulls.txt";
            // 
            // checkBoxExportHull
            // 
            this.checkBoxExportHull.AutoSize = true;
            this.checkBoxExportHull.Location = new System.Drawing.Point(6, 186);
            this.checkBoxExportHull.Name = "checkBoxExportHull";
            this.checkBoxExportHull.Size = new System.Drawing.Size(95, 17);
            this.checkBoxExportHull.TabIndex = 28;
            this.checkBoxExportHull.Text = "Hull remaining:";
            this.checkBoxExportHull.UseVisualStyleBackColor = true;
            this.checkBoxExportHull.CheckedChanged += new System.EventHandler(this.checkBoxExportHull_CheckedChanged);
            // 
            // textBoxExportWaypointDistanceFile
            // 
            this.textBoxExportWaypointDistanceFile.Location = new System.Drawing.Point(125, 132);
            this.textBoxExportWaypointDistanceFile.Name = "textBoxExportWaypointDistanceFile";
            this.textBoxExportWaypointDistanceFile.Size = new System.Drawing.Size(104, 20);
            this.textBoxExportWaypointDistanceFile.TabIndex = 27;
            this.textBoxExportWaypointDistanceFile.Text = "WPDistances.txt";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(163, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Duration (ms):";
            // 
            // numericUpDownNotableEventDuration
            // 
            this.numericUpDownNotableEventDuration.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownNotableEventDuration.Location = new System.Drawing.Point(179, 106);
            this.numericUpDownNotableEventDuration.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDownNotableEventDuration.Name = "numericUpDownNotableEventDuration";
            this.numericUpDownNotableEventDuration.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownNotableEventDuration.TabIndex = 25;
            this.numericUpDownNotableEventDuration.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // textBoxNotableEventsFile
            // 
            this.textBoxNotableEventsFile.Location = new System.Drawing.Point(67, 105);
            this.textBoxNotableEventsFile.Name = "textBoxNotableEventsFile";
            this.textBoxNotableEventsFile.Size = new System.Drawing.Size(106, 20);
            this.textBoxNotableEventsFile.TabIndex = 24;
            this.textBoxNotableEventsFile.Text = "Events.txt";
            // 
            // checkBoxExportNotableEvents
            // 
            this.checkBoxExportNotableEvents.AutoSize = true;
            this.checkBoxExportNotableEvents.Location = new System.Drawing.Point(6, 88);
            this.checkBoxExportNotableEvents.Name = "checkBoxExportNotableEvents";
            this.checkBoxExportNotableEvents.Size = new System.Drawing.Size(101, 17);
            this.checkBoxExportNotableEvents.TabIndex = 23;
            this.checkBoxExportNotableEvents.Text = "Notable events:";
            this.checkBoxExportNotableEvents.UseVisualStyleBackColor = true;
            this.checkBoxExportNotableEvents.CheckedChanged += new System.EventHandler(this.checkBoxExportNotableEvents_CheckedChanged);
            // 
            // checkBoxIncludeMaxSpeed
            // 
            this.checkBoxIncludeMaxSpeed.AutoSize = true;
            this.checkBoxIncludeMaxSpeed.Location = new System.Drawing.Point(62, 212);
            this.checkBoxIncludeMaxSpeed.Name = "checkBoxIncludeMaxSpeed";
            this.checkBoxIncludeMaxSpeed.Size = new System.Drawing.Size(65, 17);
            this.checkBoxIncludeMaxSpeed.TabIndex = 22;
            this.checkBoxIncludeMaxSpeed.Text = "inc. max";
            this.checkBoxIncludeMaxSpeed.UseVisualStyleBackColor = true;
            // 
            // groupBoxHTMLExport
            // 
            this.groupBoxHTMLExport.Controls.Add(this.checkBoxExportAsHTML);
            this.groupBoxHTMLExport.Controls.Add(this.textBoxExportHTMLTo);
            this.groupBoxHTMLExport.Controls.Add(this.label5);
            this.groupBoxHTMLExport.Controls.Add(this.label1);
            this.groupBoxHTMLExport.Controls.Add(this.textBoxHTMLTemplateFile);
            this.groupBoxHTMLExport.Location = new System.Drawing.Point(714, 262);
            this.groupBoxHTMLExport.Name = "groupBoxHTMLExport";
            this.groupBoxHTMLExport.Size = new System.Drawing.Size(245, 106);
            this.groupBoxHTMLExport.TabIndex = 8;
            this.groupBoxHTMLExport.TabStop = false;
            this.groupBoxHTMLExport.Text = "Export as HTML";
            // 
            // checkBoxExportAsHTML
            // 
            this.checkBoxExportAsHTML.AutoSize = true;
            this.checkBoxExportAsHTML.Location = new System.Drawing.Point(6, 17);
            this.checkBoxExportAsHTML.Name = "checkBoxExportAsHTML";
            this.checkBoxExportAsHTML.Size = new System.Drawing.Size(152, 17);
            this.checkBoxExportAsHTML.TabIndex = 4;
            this.checkBoxExportAsHTML.Text = "Export HTML Leaderboard";
            this.checkBoxExportAsHTML.UseVisualStyleBackColor = true;
            // 
            // textBoxExportHTMLTo
            // 
            this.textBoxExportHTMLTo.Location = new System.Drawing.Point(64, 66);
            this.textBoxExportHTMLTo.Name = "textBoxExportHTMLTo";
            this.textBoxExportHTMLTo.Size = new System.Drawing.Size(175, 20);
            this.textBoxExportHTMLTo.TabIndex = 3;
            this.textBoxExportHTMLTo.Text = "leaderboard.html";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Export to:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "HTML Template:";
            // 
            // textBoxHTMLTemplateFile
            // 
            this.textBoxHTMLTemplateFile.Location = new System.Drawing.Point(99, 40);
            this.textBoxHTMLTemplateFile.Name = "textBoxHTMLTemplateFile";
            this.textBoxHTMLTemplateFile.Size = new System.Drawing.Size(140, 20);
            this.textBoxHTMLTemplateFile.TabIndex = 0;
            this.textBoxHTMLTemplateFile.Text = "Leaderboard Template.html";
            // 
            // groupBoxTrackTarget
            // 
            this.groupBoxTrackTarget.Controls.Add(this.textBoxExportTargetAverageSpeedFile);
            this.groupBoxTrackTarget.Controls.Add(this.checkBoxExportTrackedTargetAverageSpeed);
            this.groupBoxTrackTarget.Controls.Add(this.textBoxExportTargetDistance);
            this.groupBoxTrackTarget.Controls.Add(this.checkBoxExportTrackedTargetDistance);
            this.groupBoxTrackTarget.Controls.Add(this.textBoxExportTargetHull);
            this.groupBoxTrackTarget.Controls.Add(this.checkBoxExportTrackedTargetHull);
            this.groupBoxTrackTarget.Controls.Add(this.textBoxExportTargetPosition);
            this.groupBoxTrackTarget.Controls.Add(this.checkBoxExportTrackedTargetPosition);
            this.groupBoxTrackTarget.Controls.Add(this.textBoxExportTargetSpeedFile);
            this.groupBoxTrackTarget.Controls.Add(this.checkBoxExportTrackedTargetSpeed);
            this.groupBoxTrackTarget.Controls.Add(this.checkBoxExportTrackedTargetPitstops);
            this.groupBoxTrackTarget.Controls.Add(this.textBoxExportTargetPitstopsFile);
            this.groupBoxTrackTarget.Controls.Add(this.checkBoxExportTrackedTargetMaxSpeed);
            this.groupBoxTrackTarget.Controls.Add(this.textBoxExportTargetMaxSpeedFile);
            this.groupBoxTrackTarget.Controls.Add(this.checkBoxExportTrackedTarget);
            this.groupBoxTrackTarget.Controls.Add(this.textBoxExportTargetFile);
            this.groupBoxTrackTarget.Controls.Add(this.checkBoxClosestPlayerTarget);
            this.groupBoxTrackTarget.Location = new System.Drawing.Point(965, 6);
            this.groupBoxTrackTarget.Name = "groupBoxTrackTarget";
            this.groupBoxTrackTarget.Size = new System.Drawing.Size(245, 231);
            this.groupBoxTrackTarget.TabIndex = 9;
            this.groupBoxTrackTarget.TabStop = false;
            this.groupBoxTrackTarget.Text = "Track target";
            // 
            // textBoxExportTargetAverageSpeedFile
            // 
            this.textBoxExportTargetAverageSpeedFile.Location = new System.Drawing.Point(134, 201);
            this.textBoxExportTargetAverageSpeedFile.Name = "textBoxExportTargetAverageSpeedFile";
            this.textBoxExportTargetAverageSpeedFile.Size = new System.Drawing.Size(105, 20);
            this.textBoxExportTargetAverageSpeedFile.TabIndex = 28;
            this.textBoxExportTargetAverageSpeedFile.Text = "Tracking-AvSpeed.txt";
            // 
            // checkBoxExportTrackedTargetAverageSpeed
            // 
            this.checkBoxExportTrackedTargetAverageSpeed.AutoSize = true;
            this.checkBoxExportTrackedTargetAverageSpeed.Location = new System.Drawing.Point(6, 203);
            this.checkBoxExportTrackedTargetAverageSpeed.Name = "checkBoxExportTrackedTargetAverageSpeed";
            this.checkBoxExportTrackedTargetAverageSpeed.Size = new System.Drawing.Size(133, 17);
            this.checkBoxExportTrackedTargetAverageSpeed.TabIndex = 27;
            this.checkBoxExportTrackedTargetAverageSpeed.Text = "Export average speed:";
            this.checkBoxExportTrackedTargetAverageSpeed.UseVisualStyleBackColor = true;
            this.checkBoxExportTrackedTargetAverageSpeed.CheckedChanged += new System.EventHandler(this.checkBoxExportTrackedTargetAverageSpeed_CheckedChanged);
            // 
            // textBoxExportTargetDistance
            // 
            this.textBoxExportTargetDistance.Location = new System.Drawing.Point(134, 178);
            this.textBoxExportTargetDistance.Name = "textBoxExportTargetDistance";
            this.textBoxExportTargetDistance.Size = new System.Drawing.Size(105, 20);
            this.textBoxExportTargetDistance.TabIndex = 26;
            this.textBoxExportTargetDistance.Text = "Tracking-Distance.txt";
            // 
            // checkBoxExportTrackedTargetDistance
            // 
            this.checkBoxExportTrackedTargetDistance.AutoSize = true;
            this.checkBoxExportTrackedTargetDistance.Location = new System.Drawing.Point(6, 180);
            this.checkBoxExportTrackedTargetDistance.Name = "checkBoxExportTrackedTargetDistance";
            this.checkBoxExportTrackedTargetDistance.Size = new System.Drawing.Size(102, 17);
            this.checkBoxExportTrackedTargetDistance.TabIndex = 25;
            this.checkBoxExportTrackedTargetDistance.Text = "Export distance:";
            this.checkBoxExportTrackedTargetDistance.UseVisualStyleBackColor = true;
            this.checkBoxExportTrackedTargetDistance.CheckedChanged += new System.EventHandler(this.checkBoxExportTrackedTargetDistance_CheckedChanged);
            // 
            // textBoxExportTargetHull
            // 
            this.textBoxExportTargetHull.Location = new System.Drawing.Point(134, 155);
            this.textBoxExportTargetHull.Name = "textBoxExportTargetHull";
            this.textBoxExportTargetHull.Size = new System.Drawing.Size(105, 20);
            this.textBoxExportTargetHull.TabIndex = 24;
            this.textBoxExportTargetHull.Text = "Tracking-Hull.txt";
            // 
            // checkBoxExportTrackedTargetHull
            // 
            this.checkBoxExportTrackedTargetHull.AutoSize = true;
            this.checkBoxExportTrackedTargetHull.Location = new System.Drawing.Point(6, 157);
            this.checkBoxExportTrackedTargetHull.Name = "checkBoxExportTrackedTargetHull";
            this.checkBoxExportTrackedTargetHull.Size = new System.Drawing.Size(78, 17);
            this.checkBoxExportTrackedTargetHull.TabIndex = 23;
            this.checkBoxExportTrackedTargetHull.Text = "Export hull:";
            this.checkBoxExportTrackedTargetHull.UseVisualStyleBackColor = true;
            this.checkBoxExportTrackedTargetHull.CheckedChanged += new System.EventHandler(this.checkBoxExportTrackedTargetHull_CheckedChanged);
            // 
            // textBoxExportTargetPosition
            // 
            this.textBoxExportTargetPosition.Location = new System.Drawing.Point(134, 132);
            this.textBoxExportTargetPosition.Name = "textBoxExportTargetPosition";
            this.textBoxExportTargetPosition.Size = new System.Drawing.Size(105, 20);
            this.textBoxExportTargetPosition.TabIndex = 22;
            this.textBoxExportTargetPosition.Text = "Tracking-Pos.txt";
            // 
            // checkBoxExportTrackedTargetPosition
            // 
            this.checkBoxExportTrackedTargetPosition.AutoSize = true;
            this.checkBoxExportTrackedTargetPosition.Location = new System.Drawing.Point(6, 134);
            this.checkBoxExportTrackedTargetPosition.Name = "checkBoxExportTrackedTargetPosition";
            this.checkBoxExportTrackedTargetPosition.Size = new System.Drawing.Size(122, 17);
            this.checkBoxExportTrackedTargetPosition.TabIndex = 21;
            this.checkBoxExportTrackedTargetPosition.Text = "Export race position:";
            this.checkBoxExportTrackedTargetPosition.UseVisualStyleBackColor = true;
            this.checkBoxExportTrackedTargetPosition.CheckedChanged += new System.EventHandler(this.checkBoxExportTargetPosition_CheckedChanged);
            // 
            // textBoxExportTargetSpeedFile
            // 
            this.textBoxExportTargetSpeedFile.Location = new System.Drawing.Point(134, 63);
            this.textBoxExportTargetSpeedFile.Name = "textBoxExportTargetSpeedFile";
            this.textBoxExportTargetSpeedFile.Size = new System.Drawing.Size(105, 20);
            this.textBoxExportTargetSpeedFile.TabIndex = 20;
            this.textBoxExportTargetSpeedFile.Text = "Tracking-Speed.txt";
            // 
            // checkBoxExportTrackedTargetSpeed
            // 
            this.checkBoxExportTrackedTargetSpeed.AutoSize = true;
            this.checkBoxExportTrackedTargetSpeed.Location = new System.Drawing.Point(6, 65);
            this.checkBoxExportTrackedTargetSpeed.Name = "checkBoxExportTrackedTargetSpeed";
            this.checkBoxExportTrackedTargetSpeed.Size = new System.Drawing.Size(91, 17);
            this.checkBoxExportTrackedTargetSpeed.TabIndex = 19;
            this.checkBoxExportTrackedTargetSpeed.Text = "Export speed:";
            this.checkBoxExportTrackedTargetSpeed.UseVisualStyleBackColor = true;
            this.checkBoxExportTrackedTargetSpeed.CheckedChanged += new System.EventHandler(this.checkBoxExportTargetSpeed_CheckedChanged);
            // 
            // checkBoxExportTrackedTargetPitstops
            // 
            this.checkBoxExportTrackedTargetPitstops.AutoSize = true;
            this.checkBoxExportTrackedTargetPitstops.Location = new System.Drawing.Point(6, 111);
            this.checkBoxExportTrackedTargetPitstops.Name = "checkBoxExportTrackedTargetPitstops";
            this.checkBoxExportTrackedTargetPitstops.Size = new System.Drawing.Size(123, 17);
            this.checkBoxExportTrackedTargetPitstops.TabIndex = 14;
            this.checkBoxExportTrackedTargetPitstops.Text = "Export pitstop count:";
            this.checkBoxExportTrackedTargetPitstops.UseVisualStyleBackColor = true;
            this.checkBoxExportTrackedTargetPitstops.CheckedChanged += new System.EventHandler(this.checkBoxExportTargetPitstops_CheckedChanged);
            // 
            // textBoxExportTargetPitstopsFile
            // 
            this.textBoxExportTargetPitstopsFile.Location = new System.Drawing.Point(134, 109);
            this.textBoxExportTargetPitstopsFile.Name = "textBoxExportTargetPitstopsFile";
            this.textBoxExportTargetPitstopsFile.Size = new System.Drawing.Size(105, 20);
            this.textBoxExportTargetPitstopsFile.TabIndex = 15;
            this.textBoxExportTargetPitstopsFile.Text = "Tracking-Pit.txt";
            // 
            // checkBoxExportTrackedTargetMaxSpeed
            // 
            this.checkBoxExportTrackedTargetMaxSpeed.AutoSize = true;
            this.checkBoxExportTrackedTargetMaxSpeed.Location = new System.Drawing.Point(6, 88);
            this.checkBoxExportTrackedTargetMaxSpeed.Name = "checkBoxExportTrackedTargetMaxSpeed";
            this.checkBoxExportTrackedTargetMaxSpeed.Size = new System.Drawing.Size(113, 17);
            this.checkBoxExportTrackedTargetMaxSpeed.TabIndex = 12;
            this.checkBoxExportTrackedTargetMaxSpeed.Text = "Export max speed:";
            this.checkBoxExportTrackedTargetMaxSpeed.UseVisualStyleBackColor = true;
            this.checkBoxExportTrackedTargetMaxSpeed.CheckedChanged += new System.EventHandler(this.checkBoxExportTargetMaxSpeed_CheckedChanged);
            // 
            // textBoxExportTargetMaxSpeedFile
            // 
            this.textBoxExportTargetMaxSpeedFile.Location = new System.Drawing.Point(134, 86);
            this.textBoxExportTargetMaxSpeedFile.Name = "textBoxExportTargetMaxSpeedFile";
            this.textBoxExportTargetMaxSpeedFile.Size = new System.Drawing.Size(105, 20);
            this.textBoxExportTargetMaxSpeedFile.TabIndex = 13;
            this.textBoxExportTargetMaxSpeedFile.Text = "Tracking-Max.txt";
            // 
            // timerRefreshFromServer
            // 
            this.timerRefreshFromServer.Interval = 1000;
            this.timerRefreshFromServer.Tick += new System.EventHandler(this.timerRefreshFromServer_Tick);
            // 
            // groupBoxServerInfo
            // 
            this.groupBoxServerInfo.Controls.Add(this.label8);
            this.groupBoxServerInfo.Controls.Add(this.textBoxServerRaceGuid);
            this.groupBoxServerInfo.Controls.Add(this.label7);
            this.groupBoxServerInfo.Controls.Add(this.textBoxRaceStatusServerUrl);
            this.groupBoxServerInfo.Location = new System.Drawing.Point(965, 299);
            this.groupBoxServerInfo.Name = "groupBoxServerInfo";
            this.groupBoxServerInfo.Size = new System.Drawing.Size(245, 114);
            this.groupBoxServerInfo.TabIndex = 10;
            this.groupBoxServerInfo.TabStop = false;
            this.groupBoxServerInfo.Text = "Server Monitoring";
            this.groupBoxServerInfo.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Race Guid:";
            // 
            // textBoxServerRaceGuid
            // 
            this.textBoxServerRaceGuid.Location = new System.Drawing.Point(9, 74);
            this.textBoxServerRaceGuid.Name = "textBoxServerRaceGuid";
            this.textBoxServerRaceGuid.Size = new System.Drawing.Size(230, 20);
            this.textBoxServerRaceGuid.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Server status url:";
            // 
            // textBoxRaceStatusServerUrl
            // 
            this.textBoxRaceStatusServerUrl.Location = new System.Drawing.Point(9, 34);
            this.textBoxRaceStatusServerUrl.Name = "textBoxRaceStatusServerUrl";
            this.textBoxRaceStatusServerUrl.Size = new System.Drawing.Size(230, 20);
            this.textBoxRaceStatusServerUrl.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::SRVTracker.Properties.Resources.SRV_Targetted1;
            this.pictureBox1.Location = new System.Drawing.Point(1004, 241);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(175, 175);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // timerPreraceExport
            // 
            this.timerPreraceExport.Interval = 2000;
            this.timerPreraceExport.Tick += new System.EventHandler(this.timerPreraceExport_Tick);
            // 
            // numericUpDownLapCount
            // 
            this.numericUpDownLapCount.Location = new System.Drawing.Point(239, 41);
            this.numericUpDownLapCount.Name = "numericUpDownLapCount";
            this.numericUpDownLapCount.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownLapCount.TabIndex = 5;
            this.numericUpDownLapCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownLapCount.ValueChanged += new System.EventHandler(this.numericUpDownLapCount_ValueChanged);
            // 
            // checkBoxLappedRace
            // 
            this.checkBoxLappedRace.AutoSize = true;
            this.checkBoxLappedRace.Location = new System.Drawing.Point(181, 42);
            this.checkBoxLappedRace.Name = "checkBoxLappedRace";
            this.checkBoxLappedRace.Size = new System.Drawing.Size(52, 17);
            this.checkBoxLappedRace.TabIndex = 6;
            this.checkBoxLappedRace.Text = "Laps:";
            this.checkBoxLappedRace.UseVisualStyleBackColor = true;
            this.checkBoxLappedRace.CheckedChanged += new System.EventHandler(this.checkBoxLappedRace_CheckedChanged);
            // 
            // FormRaceManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1226, 429);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBoxServerInfo);
            this.Controls.Add(this.groupBoxTrackTarget);
            this.Controls.Add(this.groupBoxHTMLExport);
            this.Controls.Add(this.groupBoxTextExport);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBoxRaceManagement);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormRaceManager";
            this.Text = "Race Manager";
            this.Deactivate += new System.EventHandler(this.FormRaceMonitor_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRaceMonitor_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBoxAddCommander.ResumeLayout(false);
            this.groupBoxRaceManagement.ResumeLayout(false);
            this.groupBoxRaceManagement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeaderboardMaxLength)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStatusMaxLength)).EndInit();
            this.groupBoxTextExport.ResumeLayout(false);
            this.groupBoxTextExport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNotableEventDuration)).EndInit();
            this.groupBoxHTMLExport.ResumeLayout(false);
            this.groupBoxHTMLExport.PerformLayout();
            this.groupBoxTrackTarget.ResumeLayout(false);
            this.groupBoxTrackTarget.PerformLayout();
            this.groupBoxServerInfo.ResumeLayout(false);
            this.groupBoxServerInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLapCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonLoadRoute;
        private System.Windows.Forms.TextBox textBoxRouteName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRaceName;
        private System.Windows.Forms.TextBox textBoxPlanet;
        private System.Windows.Forms.TextBox textBoxSystem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView listViewParticipants;
        private System.Windows.Forms.ColumnHeader columnHeaderPosition;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderDistanceToWaypoint;
        private System.Windows.Forms.Button buttonTrackParticipant;
        private System.Windows.Forms.Button buttonRemoveParticipant;
        private System.Windows.Forms.Button buttonAddParticipant;
        private System.Windows.Forms.GroupBox groupBoxRaceManagement;
        private System.Windows.Forms.CheckBox checkBoxExportLeaderboard;
        private System.Windows.Forms.CheckBox checkBoxEliminationOnDestruction;
        private System.Windows.Forms.ListBox listBoxWaypoints;
        private System.Windows.Forms.NumericUpDown numericUpDownLeaderboardMaxLength;
        private System.Windows.Forms.Button buttonStartRace;
        private System.Windows.Forms.CheckBox checkBoxAutoAddCommanders;
        private System.Windows.Forms.Button buttonStopRace;
        private System.Windows.Forms.Button buttonSaveRaceAs;
        private System.Windows.Forms.Button buttonSaveRace;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonLoadRace;
        private System.Windows.Forms.GroupBox groupBoxAddCommander;
        private System.Windows.Forms.Button buttonAddCommander;
        private System.Windows.Forms.ComboBox comboBoxAddCommander;
        private System.Windows.Forms.CheckBox checkBoxShowDetailedStatus;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox checkBoxAllowPitstops;
        private System.Windows.Forms.CheckBox checkBoxSRVRace;
        private System.Windows.Forms.Button buttonEditStatusMessages;
        private System.Windows.Forms.CheckBox checkBoxExportDistance;
        private System.Windows.Forms.TextBox textBoxExportTargetFile;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTarget;
        private System.Windows.Forms.TextBox textBoxExportStatusFile;
        private System.Windows.Forms.TextBox textBoxExportLeaderboardFile;
        private System.Windows.Forms.CheckBox checkBoxExportStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownStatusMaxLength;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBoxStreamInfo;
        private System.Windows.Forms.CheckBox checkBoxClosestPlayerTarget;
        private System.Windows.Forms.Button buttonRaceHistory;
        private System.Windows.Forms.TextBox textBoxExportSpeedFile;
        private System.Windows.Forms.CheckBox checkBoxExportSpeed;
        private System.Windows.Forms.GroupBox groupBoxTextExport;
        private System.Windows.Forms.GroupBox groupBoxHTMLExport;
        private System.Windows.Forms.TextBox textBoxExportHTMLTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxHTMLTemplateFile;
        private System.Windows.Forms.CheckBox checkBoxExportAsHTML;
        private System.Windows.Forms.CheckBox checkBoxIncludeMaxSpeed;
        private System.Windows.Forms.TextBox textBoxNotableEventsFile;
        private System.Windows.Forms.CheckBox checkBoxExportNotableEvents;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownNotableEventDuration;
        private System.Windows.Forms.GroupBox groupBoxTrackTarget;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTargetPitstops;
        private System.Windows.Forms.TextBox textBoxExportTargetPitstopsFile;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTargetMaxSpeed;
        private System.Windows.Forms.TextBox textBoxExportTargetMaxSpeedFile;
        private System.Windows.Forms.CheckBox checkBoxServerMonitoring;
        private System.Windows.Forms.Timer timerRefreshFromServer;
        private System.Windows.Forms.GroupBox groupBoxServerInfo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxRaceStatusServerUrl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxServerRaceGuid;
        private System.Windows.Forms.Button buttonUneliminate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxExportTargetSpeedFile;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTargetSpeed;
        private System.Windows.Forms.TextBox textBoxExportTargetPosition;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTargetPosition;
        private System.Windows.Forms.TextBox textBoxExportWaypointDistanceFile;
        private System.Windows.Forms.ColumnHeader columnHeaderHull;
        private System.Windows.Forms.TextBox textBoxExportHullFile;
        private System.Windows.Forms.CheckBox checkBoxExportHull;
        private System.Windows.Forms.TextBox textBoxExportTargetHull;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTargetHull;
        private System.Windows.Forms.Timer timerPreraceExport;
        private System.Windows.Forms.TextBox textBoxExportTotalDistanceLeftFile;
        private System.Windows.Forms.CheckBox checkBoxExportTotalDistanceLeft;
        private System.Windows.Forms.TextBox textBoxExportTargetDistance;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTargetDistance;
        private System.Windows.Forms.TextBox textBoxExportTargetAverageSpeedFile;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTargetAverageSpeed;
        private System.Windows.Forms.CheckBox checkBoxLappedRace;
        private System.Windows.Forms.NumericUpDown numericUpDownLapCount;
    }
}