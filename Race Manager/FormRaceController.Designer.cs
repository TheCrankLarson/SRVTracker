﻿namespace Race_Manager
{
    partial class FormRaceController
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
            this.radioButtonRaceTypeTimeTrial = new System.Windows.Forms.RadioButton();
            this.radioButtonRaceTypeStandard = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxCustomStatusMessages = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowMainShip = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowFighter = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxAllowPitstops = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowSRV = new System.Windows.Forms.CheckBox();
            this.checkBoxEliminationOnDestruction = new System.Windows.Forms.CheckBox();
            this.checkBoxLappedRace = new System.Windows.Forms.CheckBox();
            this.numericUpDownLapCount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSystem = new System.Windows.Forms.TextBox();
            this.textBoxPlanet = new System.Windows.Forms.TextBox();
            this.checkBoxAutoAddCommanders = new System.Windows.Forms.CheckBox();
            this.groupBoxAddCommander = new System.Windows.Forms.GroupBox();
            this.comboBoxAddCommander = new System.Windows.Forms.ComboBox();
            this.buttonAddCommander = new System.Windows.Forms.Button();
            this.listBoxParticipants = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxConnectToRace = new System.Windows.Forms.ComboBox();
            this.buttonConnectToRace = new System.Windows.Forms.Button();
            this.buttonLoadRace = new System.Windows.Forms.Button();
            this.buttonSaveRaceAs = new System.Windows.Forms.Button();
            this.buttonSaveRace = new System.Windows.Forms.Button();
            this.textBoxRaceName = new System.Windows.Forms.TextBox();
            this.listBoxWaypoints = new System.Windows.Forms.ListBox();
            this.textBoxRouteName = new System.Windows.Forms.TextBox();
            this.checkBoxShowTargetTelemetry = new System.Windows.Forms.CheckBox();
            this.checkBoxShowRaceTelemetry = new System.Windows.Forms.CheckBox();
            this.checkBoxExportTargetTelemetry = new System.Windows.Forms.CheckBox();
            this.checkBoxExportRaceTelemetry = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.comboBoxTimeTrialRacer = new System.Windows.Forms.ComboBox();
            this.buttonPause = new System.Windows.Forms.Button();
            this.buttonStartRace = new System.Windows.Forms.Button();
            this.buttonStopRace = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.radioButtonUseCustomServer = new System.Windows.Forms.RadioButton();
            this.radioButtonUseDefaultServer = new System.Windows.Forms.RadioButton();
            this.textBoxUploadServer = new System.Windows.Forms.TextBox();
            this.comboBoxTarget = new System.Windows.Forms.ComboBox();
            this.checkBoxTargetClosestTo = new System.Windows.Forms.CheckBox();
            this.checkBoxShowTimeTrialTelemetry = new System.Windows.Forms.CheckBox();
            this.checkBoxExportTimeTrialTelemetry = new System.Windows.Forms.CheckBox();
            this.checkBoxStartRaceTimerAtFirstWaypoint = new System.Windows.Forms.CheckBox();
            this.buttonEditStatusMessages = new System.Windows.Forms.Button();
            this.buttonTimeTrialTelemetryExportSettings = new System.Windows.Forms.Button();
            this.buttonCommanderTelemetryExportSettings = new System.Windows.Forms.Button();
            this.buttonRaceTelemetryExportSettings = new System.Windows.Forms.Button();
            this.buttonRaceHistory = new System.Windows.Forms.Button();
            this.groupBoxServerInfo = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxServerRaceGuid = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxRaceStatusServerUrl = new System.Windows.Forms.TextBox();
            this.timerDownloadRaceTelemetry = new System.Windows.Forms.Timer(this.components);
            this.buttonTest = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.timerTrackTarget = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageParticipants = new System.Windows.Forms.TabPage();
            this.buttonAddParticipant = new System.Windows.Forms.Button();
            this.buttonUneliminate = new System.Windows.Forms.Button();
            this.buttonRemoveParticipant = new System.Windows.Forms.Button();
            this.buttonTrackParticipant = new System.Windows.Forms.Button();
            this.tabPageRoute = new System.Windows.Forms.TabPage();
            this.checkBoxAnyWaypointOrder = new System.Windows.Forms.CheckBox();
            this.checkBoxLapCustomWaypoints = new System.Windows.Forms.CheckBox();
            this.numericUpDownLapEndWaypoint = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLapStartWaypoint = new System.Windows.Forms.NumericUpDown();
            this.buttonLoadRoute = new System.Windows.Forms.Button();
            this.tabPageStartSettings = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonAudioTest = new System.Windows.Forms.Button();
            this.checkBoxAudioRandomiseStartPause = new System.Windows.Forms.CheckBox();
            this.comboBoxAudioStartStart = new System.Windows.Forms.ComboBox();
            this.comboBoxAudioStartAnnouncement = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownAudioStartPause = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAudioStartStart = new System.Windows.Forms.CheckBox();
            this.checkBoxAudioStartPause = new System.Windows.Forms.CheckBox();
            this.checkBoxAudioStartAnnouncement = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableAudioStart = new System.Windows.Forms.CheckBox();
            this.tabPageRaceSettings = new System.Windows.Forms.TabPage();
            this.checkBoxAllowFeet = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowAnyLocomotion = new System.Windows.Forms.CheckBox();
            this.tabPageTelemetrySettings = new System.Windows.Forms.TabPage();
            this.checkBoxShowRaceTimer = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPageServerSettings = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLapCount)).BeginInit();
            this.groupBoxAddCommander.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBoxServerInfo.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageParticipants.SuspendLayout();
            this.tabPageRoute.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLapEndWaypoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLapStartWaypoint)).BeginInit();
            this.tabPageStartSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAudioStartPause)).BeginInit();
            this.tabPageRaceSettings.SuspendLayout();
            this.tabPageTelemetrySettings.SuspendLayout();
            this.tabPageServerSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonRaceTypeTimeTrial
            // 
            this.radioButtonRaceTypeTimeTrial.AutoSize = true;
            this.radioButtonRaceTypeTimeTrial.Enabled = false;
            this.radioButtonRaceTypeTimeTrial.Location = new System.Drawing.Point(167, 6);
            this.radioButtonRaceTypeTimeTrial.Name = "radioButtonRaceTypeTimeTrial";
            this.radioButtonRaceTypeTimeTrial.Size = new System.Drawing.Size(67, 17);
            this.radioButtonRaceTypeTimeTrial.TabIndex = 14;
            this.radioButtonRaceTypeTimeTrial.Text = "Time trial";
            this.radioButtonRaceTypeTimeTrial.UseVisualStyleBackColor = true;
            // 
            // radioButtonRaceTypeStandard
            // 
            this.radioButtonRaceTypeStandard.AutoSize = true;
            this.radioButtonRaceTypeStandard.Checked = true;
            this.radioButtonRaceTypeStandard.Location = new System.Drawing.Point(69, 6);
            this.radioButtonRaceTypeStandard.Name = "radioButtonRaceTypeStandard";
            this.radioButtonRaceTypeStandard.Size = new System.Drawing.Size(92, 17);
            this.radioButtonRaceTypeStandard.TabIndex = 13;
            this.radioButtonRaceTypeStandard.TabStop = true;
            this.radioButtonRaceTypeStandard.Text = "Standard race";
            this.radioButtonRaceTypeStandard.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Race type:";
            // 
            // checkBoxCustomStatusMessages
            // 
            this.checkBoxCustomStatusMessages.AutoSize = true;
            this.checkBoxCustomStatusMessages.Location = new System.Drawing.Point(7, 92);
            this.checkBoxCustomStatusMessages.Name = "checkBoxCustomStatusMessages";
            this.checkBoxCustomStatusMessages.Size = new System.Drawing.Size(142, 17);
            this.checkBoxCustomStatusMessages.TabIndex = 11;
            this.checkBoxCustomStatusMessages.Text = "Custom status messages";
            this.toolTip1.SetToolTip(this.checkBoxCustomStatusMessages, "Enable custom status messages (for events such as completed)");
            this.checkBoxCustomStatusMessages.UseVisualStyleBackColor = true;
            this.checkBoxCustomStatusMessages.CheckedChanged += new System.EventHandler(this.checkBoxCustomStatusMessages_CheckedChanged);
            // 
            // checkBoxAllowMainShip
            // 
            this.checkBoxAllowMainShip.AutoSize = true;
            this.checkBoxAllowMainShip.Location = new System.Drawing.Point(57, 42);
            this.checkBoxAllowMainShip.Name = "checkBoxAllowMainShip";
            this.checkBoxAllowMainShip.Size = new System.Drawing.Size(71, 17);
            this.checkBoxAllowMainShip.TabIndex = 10;
            this.checkBoxAllowMainShip.Text = "Main ship";
            this.toolTip1.SetToolTip(this.checkBoxAllowMainShip, "Main ship is allowed to be used during race.");
            this.checkBoxAllowMainShip.UseVisualStyleBackColor = true;
            this.checkBoxAllowMainShip.CheckedChanged += new System.EventHandler(this.checkBoxAllowMainShip_CheckedChanged);
            // 
            // checkBoxAllowFighter
            // 
            this.checkBoxAllowFighter.AutoSize = true;
            this.checkBoxAllowFighter.Location = new System.Drawing.Point(134, 42);
            this.checkBoxAllowFighter.Name = "checkBoxAllowFighter";
            this.checkBoxAllowFighter.Size = new System.Drawing.Size(58, 17);
            this.checkBoxAllowFighter.TabIndex = 9;
            this.checkBoxAllowFighter.Text = "Fighter";
            this.toolTip1.SetToolTip(this.checkBoxAllowFighter, "Fighter is allowed to be used during race.");
            this.checkBoxAllowFighter.UseVisualStyleBackColor = true;
            this.checkBoxAllowFighter.CheckedChanged += new System.EventHandler(this.checkBoxAllowFighter_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Allowed:";
            // 
            // checkBoxAllowPitstops
            // 
            this.checkBoxAllowPitstops.AutoSize = true;
            this.checkBoxAllowPitstops.Checked = true;
            this.checkBoxAllowPitstops.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAllowPitstops.Location = new System.Drawing.Point(240, 65);
            this.checkBoxAllowPitstops.Name = "checkBoxAllowPitstops";
            this.checkBoxAllowPitstops.Size = new System.Drawing.Size(102, 17);
            this.checkBoxAllowPitstops.TabIndex = 2;
            this.checkBoxAllowPitstops.Text = "Require pitstops";
            this.toolTip1.SetToolTip(this.checkBoxAllowPitstops, "If pitstops are required, then racers can only refuel while\r\nunder their ship, an" +
        "d repair must be by boarding ship.\r\nAny synthesis detected will result in elimin" +
        "ation.");
            this.checkBoxAllowPitstops.UseVisualStyleBackColor = true;
            // 
            // checkBoxAllowSRV
            // 
            this.checkBoxAllowSRV.AutoSize = true;
            this.checkBoxAllowSRV.Checked = true;
            this.checkBoxAllowSRV.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAllowSRV.Location = new System.Drawing.Point(198, 42);
            this.checkBoxAllowSRV.Name = "checkBoxAllowSRV";
            this.checkBoxAllowSRV.Size = new System.Drawing.Size(48, 17);
            this.checkBoxAllowSRV.TabIndex = 1;
            this.checkBoxAllowSRV.Text = "SRV";
            this.toolTip1.SetToolTip(this.checkBoxAllowSRV, "SRV is allowed to be used during race.");
            this.checkBoxAllowSRV.UseVisualStyleBackColor = true;
            this.checkBoxAllowSRV.CheckedChanged += new System.EventHandler(this.checkBoxAllowSRV_CheckedChanged);
            // 
            // checkBoxEliminationOnDestruction
            // 
            this.checkBoxEliminationOnDestruction.AutoSize = true;
            this.checkBoxEliminationOnDestruction.Checked = true;
            this.checkBoxEliminationOnDestruction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEliminationOnDestruction.Location = new System.Drawing.Point(7, 65);
            this.checkBoxEliminationOnDestruction.Name = "checkBoxEliminationOnDestruction";
            this.checkBoxEliminationOnDestruction.Size = new System.Drawing.Size(175, 17);
            this.checkBoxEliminationOnDestruction.TabIndex = 0;
            this.checkBoxEliminationOnDestruction.Text = "Eliminate on vehicle destruction";
            this.toolTip1.SetToolTip(this.checkBoxEliminationOnDestruction, "Contestant is eliminated if the vehicle they are in is destroyed.");
            this.checkBoxEliminationOnDestruction.UseVisualStyleBackColor = true;
            // 
            // checkBoxLappedRace
            // 
            this.checkBoxLappedRace.AutoSize = true;
            this.checkBoxLappedRace.Location = new System.Drawing.Point(213, 91);
            this.checkBoxLappedRace.Name = "checkBoxLappedRace";
            this.checkBoxLappedRace.Size = new System.Drawing.Size(52, 17);
            this.checkBoxLappedRace.TabIndex = 6;
            this.checkBoxLappedRace.Text = "Laps:";
            this.toolTip1.SetToolTip(this.checkBoxLappedRace, "Whether this is a lapped race or not.\r\nFor a lapped race, the first waypoint is b" +
        "oth\r\nstart and finish.");
            this.checkBoxLappedRace.UseVisualStyleBackColor = true;
            this.checkBoxLappedRace.CheckedChanged += new System.EventHandler(this.checkBoxLappedRace_CheckedChanged);
            // 
            // numericUpDownLapCount
            // 
            this.numericUpDownLapCount.Location = new System.Drawing.Point(317, 88);
            this.numericUpDownLapCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLapCount.Name = "numericUpDownLapCount";
            this.numericUpDownLapCount.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownLapCount.TabIndex = 5;
            this.toolTip1.SetToolTip(this.numericUpDownLapCount, "Total number of laps in the race");
            this.numericUpDownLapCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownLapCount.ValueChanged += new System.EventHandler(this.numericUpDownLapCount_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "System:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Planet:";
            // 
            // textBoxSystem
            // 
            this.textBoxSystem.Location = new System.Drawing.Point(260, 36);
            this.textBoxSystem.Name = "textBoxSystem";
            this.textBoxSystem.ReadOnly = true;
            this.textBoxSystem.Size = new System.Drawing.Size(111, 20);
            this.textBoxSystem.TabIndex = 0;
            // 
            // textBoxPlanet
            // 
            this.textBoxPlanet.Location = new System.Drawing.Point(259, 62);
            this.textBoxPlanet.Name = "textBoxPlanet";
            this.textBoxPlanet.ReadOnly = true;
            this.textBoxPlanet.Size = new System.Drawing.Size(112, 20);
            this.textBoxPlanet.TabIndex = 1;
            // 
            // checkBoxAutoAddCommanders
            // 
            this.checkBoxAutoAddCommanders.AutoSize = true;
            this.checkBoxAutoAddCommanders.Checked = true;
            this.checkBoxAutoAddCommanders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoAddCommanders.Location = new System.Drawing.Point(125, 143);
            this.checkBoxAutoAddCommanders.Name = "checkBoxAutoAddCommanders";
            this.checkBoxAutoAddCommanders.Size = new System.Drawing.Size(246, 17);
            this.checkBoxAutoAddCommanders.TabIndex = 4;
            this.checkBoxAutoAddCommanders.Text = "Automatically add commanders that are at start";
            this.toolTip1.SetToolTip(this.checkBoxAutoAddCommanders, "Add any commanders to the race that are detected at the first waypoint");
            this.checkBoxAutoAddCommanders.UseVisualStyleBackColor = true;
            // 
            // groupBoxAddCommander
            // 
            this.groupBoxAddCommander.Controls.Add(this.comboBoxAddCommander);
            this.groupBoxAddCommander.Controls.Add(this.buttonAddCommander);
            this.groupBoxAddCommander.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBoxAddCommander.Location = new System.Drawing.Point(167, -13);
            this.groupBoxAddCommander.Name = "groupBoxAddCommander";
            this.groupBoxAddCommander.Size = new System.Drawing.Size(211, 51);
            this.groupBoxAddCommander.TabIndex = 5;
            this.groupBoxAddCommander.TabStop = false;
            this.groupBoxAddCommander.Text = "Add Commander";
            // 
            // comboBoxAddCommander
            // 
            this.comboBoxAddCommander.FormattingEnabled = true;
            this.comboBoxAddCommander.Location = new System.Drawing.Point(6, 18);
            this.comboBoxAddCommander.Name = "comboBoxAddCommander";
            this.comboBoxAddCommander.Size = new System.Drawing.Size(163, 21);
            this.comboBoxAddCommander.TabIndex = 9;
            this.comboBoxAddCommander.SelectedIndexChanged += new System.EventHandler(this.comboBoxAddCommander_SelectedIndexChanged);
            this.comboBoxAddCommander.Leave += new System.EventHandler(this.comboBoxAddCommander_Leave);
            // 
            // buttonAddCommander
            // 
            this.buttonAddCommander.Image = global::Race_Manager.Properties.Resources.Add_16x;
            this.buttonAddCommander.Location = new System.Drawing.Point(175, 16);
            this.buttonAddCommander.Name = "buttonAddCommander";
            this.buttonAddCommander.Size = new System.Drawing.Size(29, 23);
            this.buttonAddCommander.TabIndex = 8;
            this.buttonAddCommander.UseVisualStyleBackColor = true;
            this.buttonAddCommander.Click += new System.EventHandler(this.buttonAddCommander_Click);
            // 
            // listBoxParticipants
            // 
            this.listBoxParticipants.FormattingEnabled = true;
            this.listBoxParticipants.Location = new System.Drawing.Point(6, 3);
            this.listBoxParticipants.Name = "listBoxParticipants";
            this.listBoxParticipants.Size = new System.Drawing.Size(330, 134);
            this.listBoxParticipants.TabIndex = 11;
            this.listBoxParticipants.SelectedIndexChanged += new System.EventHandler(this.listBoxParticipants_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxConnectToRace);
            this.groupBox2.Controls.Add(this.buttonConnectToRace);
            this.groupBox2.Controls.Add(this.buttonLoadRace);
            this.groupBox2.Controls.Add(this.buttonSaveRaceAs);
            this.groupBox2.Controls.Add(this.buttonSaveRace);
            this.groupBox2.Controls.Add(this.textBoxRaceName);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(385, 48);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Event";
            // 
            // comboBoxConnectToRace
            // 
            this.comboBoxConnectToRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxConnectToRace.FormattingEnabled = true;
            this.comboBoxConnectToRace.Location = new System.Drawing.Point(177, 18);
            this.comboBoxConnectToRace.Name = "comboBoxConnectToRace";
            this.comboBoxConnectToRace.Size = new System.Drawing.Size(174, 21);
            this.comboBoxConnectToRace.TabIndex = 23;
            this.comboBoxConnectToRace.Visible = false;
            this.comboBoxConnectToRace.SelectedIndexChanged += new System.EventHandler(this.comboBoxConnectToRace_SelectedIndexChanged);
            this.comboBoxConnectToRace.Leave += new System.EventHandler(this.comboBoxConnectToRace_Leave);
            // 
            // buttonConnectToRace
            // 
            this.buttonConnectToRace.Image = global::Race_Manager.Properties.Resources.Connect_16x;
            this.buttonConnectToRace.Location = new System.Drawing.Point(350, 17);
            this.buttonConnectToRace.Name = "buttonConnectToRace";
            this.buttonConnectToRace.Size = new System.Drawing.Size(29, 23);
            this.buttonConnectToRace.TabIndex = 22;
            this.toolTip1.SetToolTip(this.buttonConnectToRace, "Connect to race that is already running\r\n(requires a race to have been started on" +
        " the server)");
            this.buttonConnectToRace.UseVisualStyleBackColor = true;
            this.buttonConnectToRace.Click += new System.EventHandler(this.buttonConnectToRace_Click);
            // 
            // buttonLoadRace
            // 
            this.buttonLoadRace.Image = global::Race_Manager.Properties.Resources.OpenFolder_16x;
            this.buttonLoadRace.Location = new System.Drawing.Point(263, 17);
            this.buttonLoadRace.Name = "buttonLoadRace";
            this.buttonLoadRace.Size = new System.Drawing.Size(29, 23);
            this.buttonLoadRace.TabIndex = 10;
            this.buttonLoadRace.UseVisualStyleBackColor = true;
            this.buttonLoadRace.Click += new System.EventHandler(this.buttonLoadRace_Click);
            // 
            // buttonSaveRaceAs
            // 
            this.buttonSaveRaceAs.Image = global::Race_Manager.Properties.Resources.SaveAs_16x;
            this.buttonSaveRaceAs.Location = new System.Drawing.Point(292, 17);
            this.buttonSaveRaceAs.Name = "buttonSaveRaceAs";
            this.buttonSaveRaceAs.Size = new System.Drawing.Size(29, 23);
            this.buttonSaveRaceAs.TabIndex = 9;
            this.buttonSaveRaceAs.UseVisualStyleBackColor = true;
            this.buttonSaveRaceAs.Click += new System.EventHandler(this.buttonSaveRaceAs_Click);
            // 
            // buttonSaveRace
            // 
            this.buttonSaveRace.Image = global::Race_Manager.Properties.Resources.Save_16x;
            this.buttonSaveRace.Location = new System.Drawing.Point(321, 17);
            this.buttonSaveRace.Name = "buttonSaveRace";
            this.buttonSaveRace.Size = new System.Drawing.Size(29, 23);
            this.buttonSaveRace.TabIndex = 8;
            this.buttonSaveRace.UseVisualStyleBackColor = true;
            this.buttonSaveRace.Click += new System.EventHandler(this.buttonSaveRace_Click);
            // 
            // textBoxRaceName
            // 
            this.textBoxRaceName.Location = new System.Drawing.Point(6, 19);
            this.textBoxRaceName.Name = "textBoxRaceName";
            this.textBoxRaceName.Size = new System.Drawing.Size(251, 20);
            this.textBoxRaceName.TabIndex = 2;
            // 
            // listBoxWaypoints
            // 
            this.listBoxWaypoints.FormattingEnabled = true;
            this.listBoxWaypoints.Location = new System.Drawing.Point(3, 6);
            this.listBoxWaypoints.Name = "listBoxWaypoints";
            this.listBoxWaypoints.Size = new System.Drawing.Size(201, 134);
            this.listBoxWaypoints.TabIndex = 2;
            // 
            // textBoxRouteName
            // 
            this.textBoxRouteName.Location = new System.Drawing.Point(210, 10);
            this.textBoxRouteName.Name = "textBoxRouteName";
            this.textBoxRouteName.ReadOnly = true;
            this.textBoxRouteName.Size = new System.Drawing.Size(117, 20);
            this.textBoxRouteName.TabIndex = 0;
            // 
            // checkBoxShowTargetTelemetry
            // 
            this.checkBoxShowTargetTelemetry.AutoSize = true;
            this.checkBoxShowTargetTelemetry.Location = new System.Drawing.Point(247, 73);
            this.checkBoxShowTargetTelemetry.Name = "checkBoxShowTargetTelemetry";
            this.checkBoxShowTargetTelemetry.Size = new System.Drawing.Size(60, 17);
            this.checkBoxShowTargetTelemetry.TabIndex = 1;
            this.checkBoxShowTargetTelemetry.Text = "Display";
            this.toolTip1.SetToolTip(this.checkBoxShowTargetTelemetry, "Enable target telemetry display.\r\nTelemetry is shown in its own window.\r\n");
            this.checkBoxShowTargetTelemetry.UseVisualStyleBackColor = true;
            this.checkBoxShowTargetTelemetry.CheckedChanged += new System.EventHandler(this.checkBoxShowTargetTelemetry_CheckedChanged);
            // 
            // checkBoxShowRaceTelemetry
            // 
            this.checkBoxShowRaceTelemetry.AutoSize = true;
            this.checkBoxShowRaceTelemetry.Location = new System.Drawing.Point(247, 45);
            this.checkBoxShowRaceTelemetry.Name = "checkBoxShowRaceTelemetry";
            this.checkBoxShowRaceTelemetry.Size = new System.Drawing.Size(60, 17);
            this.checkBoxShowRaceTelemetry.TabIndex = 0;
            this.checkBoxShowRaceTelemetry.Text = "Display";
            this.toolTip1.SetToolTip(this.checkBoxShowRaceTelemetry, "Enable race telemetry display.\r\nTelemetry is shown in its own window.\r\nThe Race T" +
        "elemetry window is unavailable when the \r\nRace Telemetry Settings window is open" +
        ".");
            this.checkBoxShowRaceTelemetry.UseVisualStyleBackColor = true;
            this.checkBoxShowRaceTelemetry.CheckedChanged += new System.EventHandler(this.checkBoxShowRaceTelemetry_CheckedChanged);
            // 
            // checkBoxExportTargetTelemetry
            // 
            this.checkBoxExportTargetTelemetry.AutoSize = true;
            this.checkBoxExportTargetTelemetry.Location = new System.Drawing.Point(185, 73);
            this.checkBoxExportTargetTelemetry.Name = "checkBoxExportTargetTelemetry";
            this.checkBoxExportTargetTelemetry.Size = new System.Drawing.Size(56, 17);
            this.checkBoxExportTargetTelemetry.TabIndex = 1;
            this.checkBoxExportTargetTelemetry.Text = "Export";
            this.toolTip1.SetToolTip(this.checkBoxExportTargetTelemetry, "If selected, any enabled target telemetry will be exported to text files.");
            this.checkBoxExportTargetTelemetry.UseVisualStyleBackColor = true;
            // 
            // checkBoxExportRaceTelemetry
            // 
            this.checkBoxExportRaceTelemetry.AutoSize = true;
            this.checkBoxExportRaceTelemetry.Location = new System.Drawing.Point(185, 45);
            this.checkBoxExportRaceTelemetry.Name = "checkBoxExportRaceTelemetry";
            this.checkBoxExportRaceTelemetry.Size = new System.Drawing.Size(56, 17);
            this.checkBoxExportRaceTelemetry.TabIndex = 0;
            this.checkBoxExportRaceTelemetry.Text = "Export";
            this.toolTip1.SetToolTip(this.checkBoxExportRaceTelemetry, "If selected, any enabled race telemetry will be exported to text files.");
            this.checkBoxExportRaceTelemetry.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.comboBoxTimeTrialRacer);
            this.groupBox7.Controls.Add(this.buttonPause);
            this.groupBox7.Controls.Add(this.buttonStartRace);
            this.groupBox7.Controls.Add(this.buttonStopRace);
            this.groupBox7.Controls.Add(this.buttonReset);
            this.groupBox7.Location = new System.Drawing.Point(415, 60);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(148, 80);
            this.groupBox7.TabIndex = 13;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Race Control";
            // 
            // comboBoxTimeTrialRacer
            // 
            this.comboBoxTimeTrialRacer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimeTrialRacer.Enabled = false;
            this.comboBoxTimeTrialRacer.FormattingEnabled = true;
            this.comboBoxTimeTrialRacer.Location = new System.Drawing.Point(6, 48);
            this.comboBoxTimeTrialRacer.Name = "comboBoxTimeTrialRacer";
            this.comboBoxTimeTrialRacer.Size = new System.Drawing.Size(133, 21);
            this.comboBoxTimeTrialRacer.TabIndex = 3;
            this.toolTip1.SetToolTip(this.comboBoxTimeTrialRacer, "For time trial races, this selects the racer being tracked");
            // 
            // buttonPause
            // 
            this.buttonPause.Enabled = false;
            this.buttonPause.Image = global::Race_Manager.Properties.Resources.Pause_16x;
            this.buttonPause.Location = new System.Drawing.Point(41, 19);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(28, 23);
            this.buttonPause.TabIndex = 9;
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // buttonStartRace
            // 
            this.buttonStartRace.Enabled = false;
            this.buttonStartRace.Image = global::Race_Manager.Properties.Resources.Run_16x;
            this.buttonStartRace.Location = new System.Drawing.Point(6, 19);
            this.buttonStartRace.Name = "buttonStartRace";
            this.buttonStartRace.Size = new System.Drawing.Size(29, 23);
            this.buttonStartRace.TabIndex = 5;
            this.buttonStartRace.UseVisualStyleBackColor = true;
            this.buttonStartRace.Click += new System.EventHandler(this.buttonStartRace_Click);
            // 
            // buttonStopRace
            // 
            this.buttonStopRace.Enabled = false;
            this.buttonStopRace.Image = global::Race_Manager.Properties.Resources.Stop_16x;
            this.buttonStopRace.Location = new System.Drawing.Point(75, 19);
            this.buttonStopRace.Name = "buttonStopRace";
            this.buttonStopRace.Size = new System.Drawing.Size(29, 23);
            this.buttonStopRace.TabIndex = 6;
            this.buttonStopRace.UseVisualStyleBackColor = true;
            this.buttonStopRace.Click += new System.EventHandler(this.buttonStopRace_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Enabled = false;
            this.buttonReset.Image = global::Race_Manager.Properties.Resources.Restart_16x;
            this.buttonReset.Location = new System.Drawing.Point(110, 19);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(29, 23);
            this.buttonReset.TabIndex = 8;
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // radioButtonUseCustomServer
            // 
            this.radioButtonUseCustomServer.AutoSize = true;
            this.radioButtonUseCustomServer.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radioButtonUseCustomServer.Location = new System.Drawing.Point(57, 70);
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
            this.radioButtonUseDefaultServer.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radioButtonUseDefaultServer.Location = new System.Drawing.Point(57, 47);
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
            // textBoxUploadServer
            // 
            this.textBoxUploadServer.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxUploadServer.Enabled = false;
            this.textBoxUploadServer.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxUploadServer.Location = new System.Drawing.Point(57, 93);
            this.textBoxUploadServer.MaxLength = 100;
            this.textBoxUploadServer.Name = "textBoxUploadServer";
            this.textBoxUploadServer.Size = new System.Drawing.Size(254, 20);
            this.textBoxUploadServer.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textBoxUploadServer, "The server to send the status updates to");
            // 
            // comboBoxTarget
            // 
            this.comboBoxTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTarget.FormattingEnabled = true;
            this.comboBoxTarget.Location = new System.Drawing.Point(6, 32);
            this.comboBoxTarget.Name = "comboBoxTarget";
            this.comboBoxTarget.Size = new System.Drawing.Size(174, 21);
            this.comboBoxTarget.TabIndex = 2;
            this.toolTip1.SetToolTip(this.comboBoxTarget, "Target to display telemetry for");
            this.comboBoxTarget.SelectedIndexChanged += new System.EventHandler(this.comboBoxTarget_SelectedIndexChanged);
            // 
            // checkBoxTargetClosestTo
            // 
            this.checkBoxTargetClosestTo.AutoSize = true;
            this.checkBoxTargetClosestTo.Location = new System.Drawing.Point(108, 12);
            this.checkBoxTargetClosestTo.Name = "checkBoxTargetClosestTo";
            this.checkBoxTargetClosestTo.Size = new System.Drawing.Size(72, 17);
            this.checkBoxTargetClosestTo.TabIndex = 3;
            this.checkBoxTargetClosestTo.Text = "Closest to";
            this.toolTip1.SetToolTip(this.checkBoxTargetClosestTo, "Display telemetry for the commander closest to the target\r\n(instead of the target" +
        ")");
            this.checkBoxTargetClosestTo.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowTimeTrialTelemetry
            // 
            this.checkBoxShowTimeTrialTelemetry.AutoSize = true;
            this.checkBoxShowTimeTrialTelemetry.Enabled = false;
            this.checkBoxShowTimeTrialTelemetry.Location = new System.Drawing.Point(247, 101);
            this.checkBoxShowTimeTrialTelemetry.Name = "checkBoxShowTimeTrialTelemetry";
            this.checkBoxShowTimeTrialTelemetry.Size = new System.Drawing.Size(60, 17);
            this.checkBoxShowTimeTrialTelemetry.TabIndex = 8;
            this.checkBoxShowTimeTrialTelemetry.Text = "Display";
            this.toolTip1.SetToolTip(this.checkBoxShowTimeTrialTelemetry, "Enable target telemetry display.\r\nTelemetry is shown in its own window.\r\n");
            this.checkBoxShowTimeTrialTelemetry.UseVisualStyleBackColor = true;
            // 
            // checkBoxExportTimeTrialTelemetry
            // 
            this.checkBoxExportTimeTrialTelemetry.AutoSize = true;
            this.checkBoxExportTimeTrialTelemetry.Enabled = false;
            this.checkBoxExportTimeTrialTelemetry.Location = new System.Drawing.Point(185, 101);
            this.checkBoxExportTimeTrialTelemetry.Name = "checkBoxExportTimeTrialTelemetry";
            this.checkBoxExportTimeTrialTelemetry.Size = new System.Drawing.Size(56, 17);
            this.checkBoxExportTimeTrialTelemetry.TabIndex = 9;
            this.checkBoxExportTimeTrialTelemetry.Text = "Export";
            this.toolTip1.SetToolTip(this.checkBoxExportTimeTrialTelemetry, "If selected, any enabled target telemetry will be exported to text files.");
            this.checkBoxExportTimeTrialTelemetry.UseVisualStyleBackColor = true;
            // 
            // checkBoxStartRaceTimerAtFirstWaypoint
            // 
            this.checkBoxStartRaceTimerAtFirstWaypoint.AutoSize = true;
            this.checkBoxStartRaceTimerAtFirstWaypoint.Location = new System.Drawing.Point(7, 115);
            this.checkBoxStartRaceTimerAtFirstWaypoint.Name = "checkBoxStartRaceTimerAtFirstWaypoint";
            this.checkBoxStartRaceTimerAtFirstWaypoint.Size = new System.Drawing.Size(302, 17);
            this.checkBoxStartRaceTimerAtFirstWaypoint.TabIndex = 15;
            this.checkBoxStartRaceTimerAtFirstWaypoint.Text = "Start race timer when first participant reaches first waypoint";
            this.toolTip1.SetToolTip(this.checkBoxStartRaceTimerAtFirstWaypoint, "If selected, the race start time will be recorded as the time\r\nthat the first rac" +
        "er passes the first waypoint.\r\nOtherwise, timer starts as soon as race is starte" +
        "d.");
            this.checkBoxStartRaceTimerAtFirstWaypoint.UseVisualStyleBackColor = true;
            this.checkBoxStartRaceTimerAtFirstWaypoint.CheckedChanged += new System.EventHandler(this.checkBoxStartRaceTimerAtFirstWaypoint_CheckedChanged);
            // 
            // buttonEditStatusMessages
            // 
            this.buttonEditStatusMessages.Image = global::Race_Manager.Properties.Resources.Text_16x;
            this.buttonEditStatusMessages.Location = new System.Drawing.Point(155, 88);
            this.buttonEditStatusMessages.Name = "buttonEditStatusMessages";
            this.buttonEditStatusMessages.Size = new System.Drawing.Size(29, 23);
            this.buttonEditStatusMessages.TabIndex = 7;
            this.toolTip1.SetToolTip(this.buttonEditStatusMessages, "Edit the status messages");
            this.buttonEditStatusMessages.UseVisualStyleBackColor = true;
            this.buttonEditStatusMessages.Click += new System.EventHandler(this.buttonEditStatusMessages_Click);
            // 
            // buttonTimeTrialTelemetryExportSettings
            // 
            this.buttonTimeTrialTelemetryExportSettings.Enabled = false;
            this.buttonTimeTrialTelemetryExportSettings.Image = global::Race_Manager.Properties.Resources.Settings_16x;
            this.buttonTimeTrialTelemetryExportSettings.Location = new System.Drawing.Point(46, 97);
            this.buttonTimeTrialTelemetryExportSettings.Name = "buttonTimeTrialTelemetryExportSettings";
            this.buttonTimeTrialTelemetryExportSettings.Size = new System.Drawing.Size(22, 22);
            this.buttonTimeTrialTelemetryExportSettings.TabIndex = 6;
            this.toolTip1.SetToolTip(this.buttonTimeTrialTelemetryExportSettings, "Edit target telemetry collection settings");
            this.buttonTimeTrialTelemetryExportSettings.UseVisualStyleBackColor = true;
            // 
            // buttonCommanderTelemetryExportSettings
            // 
            this.buttonCommanderTelemetryExportSettings.Image = global::Race_Manager.Properties.Resources.Settings_16x;
            this.buttonCommanderTelemetryExportSettings.Location = new System.Drawing.Point(46, 69);
            this.buttonCommanderTelemetryExportSettings.Name = "buttonCommanderTelemetryExportSettings";
            this.buttonCommanderTelemetryExportSettings.Size = new System.Drawing.Size(22, 22);
            this.buttonCommanderTelemetryExportSettings.TabIndex = 3;
            this.toolTip1.SetToolTip(this.buttonCommanderTelemetryExportSettings, "Edit target telemetry collection settings");
            this.buttonCommanderTelemetryExportSettings.UseVisualStyleBackColor = true;
            this.buttonCommanderTelemetryExportSettings.Click += new System.EventHandler(this.buttonCommanderTelemetryExportSettings_Click);
            // 
            // buttonRaceTelemetryExportSettings
            // 
            this.buttonRaceTelemetryExportSettings.Image = global::Race_Manager.Properties.Resources.Settings_16x;
            this.buttonRaceTelemetryExportSettings.Location = new System.Drawing.Point(46, 41);
            this.buttonRaceTelemetryExportSettings.Name = "buttonRaceTelemetryExportSettings";
            this.buttonRaceTelemetryExportSettings.Size = new System.Drawing.Size(22, 22);
            this.buttonRaceTelemetryExportSettings.TabIndex = 2;
            this.toolTip1.SetToolTip(this.buttonRaceTelemetryExportSettings, "Edit race telemetry collection settings");
            this.buttonRaceTelemetryExportSettings.UseVisualStyleBackColor = true;
            this.buttonRaceTelemetryExportSettings.Click += new System.EventHandler(this.buttonRaceTelemetryExportSettings_Click);
            // 
            // buttonRaceHistory
            // 
            this.buttonRaceHistory.Enabled = false;
            this.buttonRaceHistory.Image = global::Race_Manager.Properties.Resources.History_16x;
            this.buttonRaceHistory.Location = new System.Drawing.Point(342, 87);
            this.buttonRaceHistory.Name = "buttonRaceHistory";
            this.buttonRaceHistory.Size = new System.Drawing.Size(29, 23);
            this.buttonRaceHistory.TabIndex = 9;
            this.toolTip1.SetToolTip(this.buttonRaceHistory, "Show race history for commanders\r\n(opens in new window)");
            this.buttonRaceHistory.UseVisualStyleBackColor = true;
            this.buttonRaceHistory.Click += new System.EventHandler(this.buttonRaceHistory_Click);
            // 
            // groupBoxServerInfo
            // 
            this.groupBoxServerInfo.Controls.Add(this.label8);
            this.groupBoxServerInfo.Controls.Add(this.textBoxServerRaceGuid);
            this.groupBoxServerInfo.Controls.Add(this.label7);
            this.groupBoxServerInfo.Controls.Add(this.textBoxRaceStatusServerUrl);
            this.groupBoxServerInfo.Location = new System.Drawing.Point(1004, 377);
            this.groupBoxServerInfo.Name = "groupBoxServerInfo";
            this.groupBoxServerInfo.Size = new System.Drawing.Size(245, 114);
            this.groupBoxServerInfo.TabIndex = 14;
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
            // timerDownloadRaceTelemetry
            // 
            this.timerDownloadRaceTelemetry.Interval = 700;
            this.timerDownloadRaceTelemetry.Tick += new System.EventHandler(this.timerDownloadRaceTelemetry_Tick);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(645, 108);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 18;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.checkBoxTargetClosestTo);
            this.groupBox10.Controls.Add(this.comboBoxTarget);
            this.groupBox10.Location = new System.Drawing.Point(397, 197);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(186, 61);
            this.groupBox10.TabIndex = 19;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Target";
            // 
            // timerTrackTarget
            // 
            this.timerTrackTarget.Interval = 500;
            this.timerTrackTarget.Tick += new System.EventHandler(this.timerTrackTarget_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageParticipants);
            this.tabControl1.Controls.Add(this.tabPageRoute);
            this.tabControl1.Controls.Add(this.tabPageStartSettings);
            this.tabControl1.Controls.Add(this.tabPageRaceSettings);
            this.tabControl1.Controls.Add(this.tabPageTelemetrySettings);
            this.tabControl1.Controls.Add(this.tabPageServerSettings);
            this.tabControl1.Location = new System.Drawing.Point(6, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(385, 198);
            this.tabControl1.TabIndex = 21;
            // 
            // tabPageParticipants
            // 
            this.tabPageParticipants.Controls.Add(this.groupBoxAddCommander);
            this.tabPageParticipants.Controls.Add(this.buttonRaceHistory);
            this.tabPageParticipants.Controls.Add(this.listBoxParticipants);
            this.tabPageParticipants.Controls.Add(this.buttonAddParticipant);
            this.tabPageParticipants.Controls.Add(this.buttonUneliminate);
            this.tabPageParticipants.Controls.Add(this.buttonRemoveParticipant);
            this.tabPageParticipants.Controls.Add(this.checkBoxAutoAddCommanders);
            this.tabPageParticipants.Controls.Add(this.buttonTrackParticipant);
            this.tabPageParticipants.Location = new System.Drawing.Point(4, 22);
            this.tabPageParticipants.Name = "tabPageParticipants";
            this.tabPageParticipants.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageParticipants.Size = new System.Drawing.Size(377, 172);
            this.tabPageParticipants.TabIndex = 0;
            this.tabPageParticipants.Text = "Participants";
            this.tabPageParticipants.UseVisualStyleBackColor = true;
            // 
            // buttonAddParticipant
            // 
            this.buttonAddParticipant.Image = global::Race_Manager.Properties.Resources.Add_16x;
            this.buttonAddParticipant.Location = new System.Drawing.Point(342, 3);
            this.buttonAddParticipant.Name = "buttonAddParticipant";
            this.buttonAddParticipant.Size = new System.Drawing.Size(29, 23);
            this.buttonAddParticipant.TabIndex = 1;
            this.buttonAddParticipant.UseVisualStyleBackColor = true;
            this.buttonAddParticipant.Click += new System.EventHandler(this.buttonAddParticipant_Click);
            // 
            // buttonUneliminate
            // 
            this.buttonUneliminate.Image = global::Race_Manager.Properties.Resources.AdvancedBreakpointDisabled_16x;
            this.buttonUneliminate.Location = new System.Drawing.Point(342, 114);
            this.buttonUneliminate.Name = "buttonUneliminate";
            this.buttonUneliminate.Size = new System.Drawing.Size(29, 23);
            this.buttonUneliminate.TabIndex = 10;
            this.buttonUneliminate.UseVisualStyleBackColor = true;
            this.buttonUneliminate.Click += new System.EventHandler(this.buttonUneliminate_Click);
            // 
            // buttonRemoveParticipant
            // 
            this.buttonRemoveParticipant.Image = global::Race_Manager.Properties.Resources.Remove_color_16x;
            this.buttonRemoveParticipant.Location = new System.Drawing.Point(342, 32);
            this.buttonRemoveParticipant.Name = "buttonRemoveParticipant";
            this.buttonRemoveParticipant.Size = new System.Drawing.Size(29, 23);
            this.buttonRemoveParticipant.TabIndex = 2;
            this.buttonRemoveParticipant.UseVisualStyleBackColor = true;
            this.buttonRemoveParticipant.Click += new System.EventHandler(this.buttonRemoveParticipant_Click);
            // 
            // buttonTrackParticipant
            // 
            this.buttonTrackParticipant.Image = global::Race_Manager.Properties.Resources.Target_16x;
            this.buttonTrackParticipant.Location = new System.Drawing.Point(342, 60);
            this.buttonTrackParticipant.Name = "buttonTrackParticipant";
            this.buttonTrackParticipant.Size = new System.Drawing.Size(29, 23);
            this.buttonTrackParticipant.TabIndex = 3;
            this.buttonTrackParticipant.UseVisualStyleBackColor = true;
            this.buttonTrackParticipant.Click += new System.EventHandler(this.buttonTrackParticipant_Click);
            // 
            // tabPageRoute
            // 
            this.tabPageRoute.Controls.Add(this.checkBoxAnyWaypointOrder);
            this.tabPageRoute.Controls.Add(this.checkBoxLapCustomWaypoints);
            this.tabPageRoute.Controls.Add(this.textBoxPlanet);
            this.tabPageRoute.Controls.Add(this.label3);
            this.tabPageRoute.Controls.Add(this.numericUpDownLapEndWaypoint);
            this.tabPageRoute.Controls.Add(this.listBoxWaypoints);
            this.tabPageRoute.Controls.Add(this.textBoxRouteName);
            this.tabPageRoute.Controls.Add(this.numericUpDownLapStartWaypoint);
            this.tabPageRoute.Controls.Add(this.label2);
            this.tabPageRoute.Controls.Add(this.buttonLoadRoute);
            this.tabPageRoute.Controls.Add(this.textBoxSystem);
            this.tabPageRoute.Controls.Add(this.numericUpDownLapCount);
            this.tabPageRoute.Controls.Add(this.checkBoxLappedRace);
            this.tabPageRoute.Location = new System.Drawing.Point(4, 22);
            this.tabPageRoute.Name = "tabPageRoute";
            this.tabPageRoute.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRoute.Size = new System.Drawing.Size(377, 172);
            this.tabPageRoute.TabIndex = 1;
            this.tabPageRoute.Text = "Route";
            this.tabPageRoute.UseVisualStyleBackColor = true;
            // 
            // checkBoxAnyWaypointOrder
            // 
            this.checkBoxAnyWaypointOrder.AutoSize = true;
            this.checkBoxAnyWaypointOrder.Location = new System.Drawing.Point(3, 146);
            this.checkBoxAnyWaypointOrder.Name = "checkBoxAnyWaypointOrder";
            this.checkBoxAnyWaypointOrder.Size = new System.Drawing.Size(203, 17);
            this.checkBoxAnyWaypointOrder.TabIndex = 10;
            this.checkBoxAnyWaypointOrder.Text = "Waypoints can be visited in any order";
            this.checkBoxAnyWaypointOrder.UseVisualStyleBackColor = true;
            this.checkBoxAnyWaypointOrder.CheckedChanged += new System.EventHandler(this.checkBoxAnyWaypointOrder_CheckedChanged);
            // 
            // checkBoxLapCustomWaypoints
            // 
            this.checkBoxLapCustomWaypoints.AutoSize = true;
            this.checkBoxLapCustomWaypoints.Location = new System.Drawing.Point(213, 114);
            this.checkBoxLapCustomWaypoints.Name = "checkBoxLapCustomWaypoints";
            this.checkBoxLapCustomWaypoints.Size = new System.Drawing.Size(153, 17);
            this.checkBoxLapCustomWaypoints.TabIndex = 9;
            this.checkBoxLapCustomWaypoints.Text = "Start/end waypoints for lap";
            this.checkBoxLapCustomWaypoints.UseVisualStyleBackColor = true;
            this.checkBoxLapCustomWaypoints.CheckedChanged += new System.EventHandler(this.checkBoxLapCustomWaypoints_CheckedChanged);
            // 
            // numericUpDownLapEndWaypoint
            // 
            this.numericUpDownLapEndWaypoint.Location = new System.Drawing.Point(319, 137);
            this.numericUpDownLapEndWaypoint.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownLapEndWaypoint.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLapEndWaypoint.Name = "numericUpDownLapEndWaypoint";
            this.numericUpDownLapEndWaypoint.Size = new System.Drawing.Size(52, 20);
            this.numericUpDownLapEndWaypoint.TabIndex = 8;
            this.numericUpDownLapEndWaypoint.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLapEndWaypoint.ValueChanged += new System.EventHandler(this.numericUpDownLapEndWaypoint_ValueChanged);
            this.numericUpDownLapEndWaypoint.Enter += new System.EventHandler(this.numericUpDownLapEndWaypoint_Enter);
            // 
            // numericUpDownLapStartWaypoint
            // 
            this.numericUpDownLapStartWaypoint.Location = new System.Drawing.Point(261, 137);
            this.numericUpDownLapStartWaypoint.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownLapStartWaypoint.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLapStartWaypoint.Name = "numericUpDownLapStartWaypoint";
            this.numericUpDownLapStartWaypoint.Size = new System.Drawing.Size(52, 20);
            this.numericUpDownLapStartWaypoint.TabIndex = 7;
            this.numericUpDownLapStartWaypoint.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLapStartWaypoint.ValueChanged += new System.EventHandler(this.numericUpDownLapStartWaypoint_ValueChanged);
            this.numericUpDownLapStartWaypoint.Enter += new System.EventHandler(this.numericUpDownLapStartWaypoint_Enter);
            // 
            // buttonLoadRoute
            // 
            this.buttonLoadRoute.Image = global::Race_Manager.Properties.Resources.OpenFolder_16x;
            this.buttonLoadRoute.Location = new System.Drawing.Point(333, 8);
            this.buttonLoadRoute.Name = "buttonLoadRoute";
            this.buttonLoadRoute.Size = new System.Drawing.Size(38, 23);
            this.buttonLoadRoute.TabIndex = 1;
            this.buttonLoadRoute.UseVisualStyleBackColor = true;
            this.buttonLoadRoute.Click += new System.EventHandler(this.buttonLoadRoute_Click);
            // 
            // tabPageStartSettings
            // 
            this.tabPageStartSettings.Controls.Add(this.groupBox1);
            this.tabPageStartSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageStartSettings.Name = "tabPageStartSettings";
            this.tabPageStartSettings.Size = new System.Drawing.Size(377, 172);
            this.tabPageStartSettings.TabIndex = 5;
            this.tabPageStartSettings.Text = "Audio";
            this.tabPageStartSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonAudioTest);
            this.groupBox1.Controls.Add(this.checkBoxAudioRandomiseStartPause);
            this.groupBox1.Controls.Add(this.comboBoxAudioStartStart);
            this.groupBox1.Controls.Add(this.comboBoxAudioStartAnnouncement);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericUpDownAudioStartPause);
            this.groupBox1.Controls.Add(this.checkBoxAudioStartStart);
            this.groupBox1.Controls.Add(this.checkBoxAudioStartPause);
            this.groupBox1.Controls.Add(this.checkBoxAudioStartAnnouncement);
            this.groupBox1.Controls.Add(this.checkBoxEnableAudioStart);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 115);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Audio";
            // 
            // buttonAudioTest
            // 
            this.buttonAudioTest.Enabled = false;
            this.buttonAudioTest.Location = new System.Drawing.Point(322, 11);
            this.buttonAudioTest.Name = "buttonAudioTest";
            this.buttonAudioTest.Size = new System.Drawing.Size(37, 23);
            this.buttonAudioTest.TabIndex = 1;
            this.buttonAudioTest.Text = "Test";
            this.buttonAudioTest.UseVisualStyleBackColor = true;
            this.buttonAudioTest.Click += new System.EventHandler(this.buttonAudioTest_Click);
            // 
            // checkBoxAudioRandomiseStartPause
            // 
            this.checkBoxAudioRandomiseStartPause.AutoSize = true;
            this.checkBoxAudioRandomiseStartPause.Location = new System.Drawing.Point(285, 65);
            this.checkBoxAudioRandomiseStartPause.Name = "checkBoxAudioRandomiseStartPause";
            this.checkBoxAudioRandomiseStartPause.Size = new System.Drawing.Size(74, 17);
            this.checkBoxAudioRandomiseStartPause.TabIndex = 8;
            this.checkBoxAudioRandomiseStartPause.Text = "randomize";
            this.checkBoxAudioRandomiseStartPause.UseVisualStyleBackColor = true;
            // 
            // comboBoxAudioStartStart
            // 
            this.comboBoxAudioStartStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAudioStartStart.FormattingEnabled = true;
            this.comboBoxAudioStartStart.Items.AddRange(new object[] {
            "Load audio file..."});
            this.comboBoxAudioStartStart.Location = new System.Drawing.Point(126, 86);
            this.comboBoxAudioStartStart.Name = "comboBoxAudioStartStart";
            this.comboBoxAudioStartStart.Size = new System.Drawing.Size(233, 21);
            this.comboBoxAudioStartStart.TabIndex = 7;
            this.comboBoxAudioStartStart.SelectedIndexChanged += new System.EventHandler(this.comboBoxAudioStartStart_SelectedIndexChanged);
            // 
            // comboBoxAudioStartAnnouncement
            // 
            this.comboBoxAudioStartAnnouncement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAudioStartAnnouncement.FormattingEnabled = true;
            this.comboBoxAudioStartAnnouncement.Items.AddRange(new object[] {
            "Load audio file..."});
            this.comboBoxAudioStartAnnouncement.Location = new System.Drawing.Point(126, 40);
            this.comboBoxAudioStartAnnouncement.Name = "comboBoxAudioStartAnnouncement";
            this.comboBoxAudioStartAnnouncement.Size = new System.Drawing.Size(233, 21);
            this.comboBoxAudioStartAnnouncement.TabIndex = 6;
            this.comboBoxAudioStartAnnouncement.SelectedIndexChanged += new System.EventHandler(this.comboBoxAudioStartAnnouncement_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(232, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "seconds";
            // 
            // numericUpDownAudioStartPause
            // 
            this.numericUpDownAudioStartPause.Location = new System.Drawing.Point(185, 64);
            this.numericUpDownAudioStartPause.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownAudioStartPause.Name = "numericUpDownAudioStartPause";
            this.numericUpDownAudioStartPause.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownAudioStartPause.TabIndex = 4;
            this.numericUpDownAudioStartPause.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // checkBoxAudioStartStart
            // 
            this.checkBoxAudioStartStart.AutoSize = true;
            this.checkBoxAudioStartStart.Location = new System.Drawing.Point(21, 88);
            this.checkBoxAudioStartStart.Name = "checkBoxAudioStartStart";
            this.checkBoxAudioStartStart.Size = new System.Drawing.Size(51, 17);
            this.checkBoxAudioStartStart.TabIndex = 3;
            this.checkBoxAudioStartStart.Text = "Start:";
            this.checkBoxAudioStartStart.UseVisualStyleBackColor = true;
            this.checkBoxAudioStartStart.CheckedChanged += new System.EventHandler(this.checkBoxAudioStartStart_CheckedChanged);
            // 
            // checkBoxAudioStartPause
            // 
            this.checkBoxAudioStartPause.AutoSize = true;
            this.checkBoxAudioStartPause.Location = new System.Drawing.Point(21, 65);
            this.checkBoxAudioStartPause.Name = "checkBoxAudioStartPause";
            this.checkBoxAudioStartPause.Size = new System.Drawing.Size(115, 17);
            this.checkBoxAudioStartPause.TabIndex = 2;
            this.checkBoxAudioStartPause.Text = "Pause before start:";
            this.checkBoxAudioStartPause.UseVisualStyleBackColor = true;
            this.checkBoxAudioStartPause.CheckedChanged += new System.EventHandler(this.checkBoxAudioStartPause_CheckedChanged);
            // 
            // checkBoxAudioStartAnnouncement
            // 
            this.checkBoxAudioStartAnnouncement.AutoSize = true;
            this.checkBoxAudioStartAnnouncement.Location = new System.Drawing.Point(21, 42);
            this.checkBoxAudioStartAnnouncement.Name = "checkBoxAudioStartAnnouncement";
            this.checkBoxAudioStartAnnouncement.Size = new System.Drawing.Size(101, 17);
            this.checkBoxAudioStartAnnouncement.TabIndex = 1;
            this.checkBoxAudioStartAnnouncement.Text = "Announcement:";
            this.checkBoxAudioStartAnnouncement.UseVisualStyleBackColor = true;
            this.checkBoxAudioStartAnnouncement.CheckedChanged += new System.EventHandler(this.checkBoxAudioStartAnnouncement_CheckedChanged);
            // 
            // checkBoxEnableAudioStart
            // 
            this.checkBoxEnableAudioStart.AutoSize = true;
            this.checkBoxEnableAudioStart.Location = new System.Drawing.Point(6, 19);
            this.checkBoxEnableAudioStart.Name = "checkBoxEnableAudioStart";
            this.checkBoxEnableAudioStart.Size = new System.Drawing.Size(126, 17);
            this.checkBoxEnableAudioStart.TabIndex = 0;
            this.checkBoxEnableAudioStart.Text = "Start race with sound";
            this.checkBoxEnableAudioStart.UseVisualStyleBackColor = true;
            this.checkBoxEnableAudioStart.CheckedChanged += new System.EventHandler(this.checkBoxEnableAudioStart_CheckedChanged);
            // 
            // tabPageRaceSettings
            // 
            this.tabPageRaceSettings.Controls.Add(this.checkBoxAllowFeet);
            this.tabPageRaceSettings.Controls.Add(this.checkBoxAllowAnyLocomotion);
            this.tabPageRaceSettings.Controls.Add(this.checkBoxStartRaceTimerAtFirstWaypoint);
            this.tabPageRaceSettings.Controls.Add(this.radioButtonRaceTypeTimeTrial);
            this.tabPageRaceSettings.Controls.Add(this.label1);
            this.tabPageRaceSettings.Controls.Add(this.radioButtonRaceTypeStandard);
            this.tabPageRaceSettings.Controls.Add(this.label4);
            this.tabPageRaceSettings.Controls.Add(this.checkBoxAllowMainShip);
            this.tabPageRaceSettings.Controls.Add(this.checkBoxAllowSRV);
            this.tabPageRaceSettings.Controls.Add(this.checkBoxCustomStatusMessages);
            this.tabPageRaceSettings.Controls.Add(this.checkBoxAllowFighter);
            this.tabPageRaceSettings.Controls.Add(this.checkBoxEliminationOnDestruction);
            this.tabPageRaceSettings.Controls.Add(this.checkBoxAllowPitstops);
            this.tabPageRaceSettings.Controls.Add(this.buttonEditStatusMessages);
            this.tabPageRaceSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageRaceSettings.Name = "tabPageRaceSettings";
            this.tabPageRaceSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRaceSettings.Size = new System.Drawing.Size(377, 172);
            this.tabPageRaceSettings.TabIndex = 2;
            this.tabPageRaceSettings.Text = "Race";
            this.tabPageRaceSettings.UseVisualStyleBackColor = true;
            // 
            // checkBoxAllowFeet
            // 
            this.checkBoxAllowFeet.AutoSize = true;
            this.checkBoxAllowFeet.Enabled = false;
            this.checkBoxAllowFeet.Location = new System.Drawing.Point(252, 42);
            this.checkBoxAllowFeet.Name = "checkBoxAllowFeet";
            this.checkBoxAllowFeet.Size = new System.Drawing.Size(47, 17);
            this.checkBoxAllowFeet.TabIndex = 17;
            this.checkBoxAllowFeet.Text = "Feet";
            this.checkBoxAllowFeet.UseVisualStyleBackColor = true;
            // 
            // checkBoxAllowAnyLocomotion
            // 
            this.checkBoxAllowAnyLocomotion.AutoSize = true;
            this.checkBoxAllowAnyLocomotion.Location = new System.Drawing.Point(305, 41);
            this.checkBoxAllowAnyLocomotion.Name = "checkBoxAllowAnyLocomotion";
            this.checkBoxAllowAnyLocomotion.Size = new System.Drawing.Size(44, 17);
            this.checkBoxAllowAnyLocomotion.TabIndex = 16;
            this.checkBoxAllowAnyLocomotion.Text = "Any";
            this.checkBoxAllowAnyLocomotion.UseVisualStyleBackColor = true;
            this.checkBoxAllowAnyLocomotion.CheckedChanged += new System.EventHandler(this.checkBoxAllowAnyLocomotion_CheckedChanged);
            // 
            // tabPageTelemetrySettings
            // 
            this.tabPageTelemetrySettings.Controls.Add(this.checkBoxShowRaceTimer);
            this.tabPageTelemetrySettings.Controls.Add(this.checkBoxShowTimeTrialTelemetry);
            this.tabPageTelemetrySettings.Controls.Add(this.checkBoxExportTimeTrialTelemetry);
            this.tabPageTelemetrySettings.Controls.Add(this.label10);
            this.tabPageTelemetrySettings.Controls.Add(this.label9);
            this.tabPageTelemetrySettings.Controls.Add(this.label6);
            this.tabPageTelemetrySettings.Controls.Add(this.checkBoxExportRaceTelemetry);
            this.tabPageTelemetrySettings.Controls.Add(this.checkBoxShowTargetTelemetry);
            this.tabPageTelemetrySettings.Controls.Add(this.checkBoxExportTargetTelemetry);
            this.tabPageTelemetrySettings.Controls.Add(this.checkBoxShowRaceTelemetry);
            this.tabPageTelemetrySettings.Controls.Add(this.buttonTimeTrialTelemetryExportSettings);
            this.tabPageTelemetrySettings.Controls.Add(this.buttonCommanderTelemetryExportSettings);
            this.tabPageTelemetrySettings.Controls.Add(this.buttonRaceTelemetryExportSettings);
            this.tabPageTelemetrySettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageTelemetrySettings.Name = "tabPageTelemetrySettings";
            this.tabPageTelemetrySettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTelemetrySettings.Size = new System.Drawing.Size(377, 172);
            this.tabPageTelemetrySettings.TabIndex = 3;
            this.tabPageTelemetrySettings.Text = "Telemetry";
            this.tabPageTelemetrySettings.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowRaceTimer
            // 
            this.checkBoxShowRaceTimer.AutoSize = true;
            this.checkBoxShowRaceTimer.Location = new System.Drawing.Point(185, 127);
            this.checkBoxShowRaceTimer.Name = "checkBoxShowRaceTimer";
            this.checkBoxShowRaceTimer.Size = new System.Drawing.Size(102, 17);
            this.checkBoxShowRaceTimer.TabIndex = 10;
            this.checkBoxShowRaceTimer.Text = "Show race timer";
            this.checkBoxShowRaceTimer.UseVisualStyleBackColor = true;
            this.checkBoxShowRaceTimer.CheckedChanged += new System.EventHandler(this.checkBoxShowRaceTimer_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(74, 102);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(105, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Time Trial Telemetry:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(74, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Target Telemetry:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(74, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Race Telemetry:";
            // 
            // tabPageServerSettings
            // 
            this.tabPageServerSettings.Controls.Add(this.textBoxUploadServer);
            this.tabPageServerSettings.Controls.Add(this.radioButtonUseCustomServer);
            this.tabPageServerSettings.Controls.Add(this.radioButtonUseDefaultServer);
            this.tabPageServerSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageServerSettings.Name = "tabPageServerSettings";
            this.tabPageServerSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageServerSettings.Size = new System.Drawing.Size(377, 172);
            this.tabPageServerSettings.TabIndex = 4;
            this.tabPageServerSettings.Text = "Server";
            this.tabPageServerSettings.UseVisualStyleBackColor = true;
            // 
            // FormRaceController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 264);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.groupBoxServerInfo);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormRaceController";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLapCount)).EndInit();
            this.groupBoxAddCommander.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBoxServerInfo.ResumeLayout(false);
            this.groupBoxServerInfo.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageParticipants.ResumeLayout(false);
            this.tabPageParticipants.PerformLayout();
            this.tabPageRoute.ResumeLayout(false);
            this.tabPageRoute.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLapEndWaypoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLapStartWaypoint)).EndInit();
            this.tabPageStartSettings.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAudioStartPause)).EndInit();
            this.tabPageRaceSettings.ResumeLayout(false);
            this.tabPageRaceSettings.PerformLayout();
            this.tabPageTelemetrySettings.ResumeLayout(false);
            this.tabPageTelemetrySettings.PerformLayout();
            this.tabPageServerSettings.ResumeLayout(false);
            this.tabPageServerSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBoxLappedRace;
        private System.Windows.Forms.NumericUpDown numericUpDownLapCount;
        private System.Windows.Forms.CheckBox checkBoxAllowPitstops;
        private System.Windows.Forms.CheckBox checkBoxAllowSRV;
        private System.Windows.Forms.CheckBox checkBoxEliminationOnDestruction;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSystem;
        private System.Windows.Forms.TextBox textBoxPlanet;
        private System.Windows.Forms.CheckBox checkBoxAutoAddCommanders;
        private System.Windows.Forms.Button buttonUneliminate;
        private System.Windows.Forms.Button buttonRaceHistory;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonEditStatusMessages;
        private System.Windows.Forms.GroupBox groupBoxAddCommander;
        private System.Windows.Forms.ComboBox comboBoxAddCommander;
        private System.Windows.Forms.Button buttonAddCommander;
        private System.Windows.Forms.Button buttonStopRace;
        private System.Windows.Forms.Button buttonStartRace;
        private System.Windows.Forms.Button buttonTrackParticipant;
        private System.Windows.Forms.Button buttonRemoveParticipant;
        private System.Windows.Forms.Button buttonAddParticipant;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonLoadRace;
        private System.Windows.Forms.Button buttonSaveRaceAs;
        private System.Windows.Forms.Button buttonSaveRace;
        private System.Windows.Forms.TextBox textBoxRaceName;
        private System.Windows.Forms.ListBox listBoxWaypoints;
        private System.Windows.Forms.Button buttonLoadRoute;
        private System.Windows.Forms.TextBox textBoxRouteName;
        private System.Windows.Forms.Button buttonCommanderTelemetryExportSettings;
        private System.Windows.Forms.Button buttonRaceTelemetryExportSettings;
        private System.Windows.Forms.CheckBox checkBoxExportTargetTelemetry;
        private System.Windows.Forms.CheckBox checkBoxExportRaceTelemetry;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBoxServerInfo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxServerRaceGuid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxRaceStatusServerUrl;
        private System.Windows.Forms.ListBox listBoxParticipants;
        private System.Windows.Forms.RadioButton radioButtonUseCustomServer;
        private System.Windows.Forms.RadioButton radioButtonUseDefaultServer;
        private System.Windows.Forms.TextBox textBoxUploadServer;
        private System.Windows.Forms.ComboBox comboBoxTarget;
        private System.Windows.Forms.CheckBox checkBoxShowTargetTelemetry;
        private System.Windows.Forms.CheckBox checkBoxShowRaceTelemetry;
        private System.Windows.Forms.CheckBox checkBoxAllowMainShip;
        private System.Windows.Forms.CheckBox checkBoxAllowFighter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerDownloadRaceTelemetry;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.CheckBox checkBoxTargetClosestTo;
        private System.Windows.Forms.CheckBox checkBoxCustomStatusMessages;
        private System.Windows.Forms.Timer timerTrackTarget;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.RadioButton radioButtonRaceTypeTimeTrial;
        private System.Windows.Forms.RadioButton radioButtonRaceTypeStandard;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxTimeTrialRacer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageParticipants;
        private System.Windows.Forms.TabPage tabPageRoute;
        private System.Windows.Forms.TabPage tabPageRaceSettings;
        private System.Windows.Forms.TabPage tabPageTelemetrySettings;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPageServerSettings;
        private System.Windows.Forms.CheckBox checkBoxShowTimeTrialTelemetry;
        private System.Windows.Forms.CheckBox checkBoxExportTimeTrialTelemetry;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonTimeTrialTelemetryExportSettings;
        private System.Windows.Forms.NumericUpDown numericUpDownLapEndWaypoint;
        private System.Windows.Forms.NumericUpDown numericUpDownLapStartWaypoint;
        private System.Windows.Forms.CheckBox checkBoxLapCustomWaypoints;
        private System.Windows.Forms.CheckBox checkBoxStartRaceTimerAtFirstWaypoint;
        private System.Windows.Forms.Button buttonConnectToRace;
        private System.Windows.Forms.ComboBox comboBoxConnectToRace;
        private System.Windows.Forms.CheckBox checkBoxShowRaceTimer;
        private System.Windows.Forms.TabPage tabPageStartSettings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxAudioStartStart;
        private System.Windows.Forms.ComboBox comboBoxAudioStartAnnouncement;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownAudioStartPause;
        private System.Windows.Forms.CheckBox checkBoxAudioStartStart;
        private System.Windows.Forms.CheckBox checkBoxAudioStartPause;
        private System.Windows.Forms.CheckBox checkBoxAudioStartAnnouncement;
        private System.Windows.Forms.CheckBox checkBoxEnableAudioStart;
        private System.Windows.Forms.CheckBox checkBoxAudioRandomiseStartPause;
        private System.Windows.Forms.Button buttonAudioTest;
        private System.Windows.Forms.CheckBox checkBoxAllowFeet;
        private System.Windows.Forms.CheckBox checkBoxAllowAnyLocomotion;
        private System.Windows.Forms.CheckBox checkBoxAnyWaypointOrder;
    }
}

