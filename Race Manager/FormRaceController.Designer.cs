namespace Race_Manager
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.checkBoxAllowMainShip = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowFighter = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxLappedRace = new System.Windows.Forms.CheckBox();
            this.numericUpDownLapCount = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAllowPitstops = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowSRV = new System.Windows.Forms.CheckBox();
            this.checkBoxEliminationOnDestruction = new System.Windows.Forms.CheckBox();
            this.buttonEditStatusMessages = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSystem = new System.Windows.Forms.TextBox();
            this.textBoxPlanet = new System.Windows.Forms.TextBox();
            this.checkBoxAutoAddCommanders = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBoxAddCommander = new System.Windows.Forms.GroupBox();
            this.comboBoxAddCommander = new System.Windows.Forms.ComboBox();
            this.buttonAddCommander = new System.Windows.Forms.Button();
            this.listBoxParticipants = new System.Windows.Forms.ListBox();
            this.buttonUneliminate = new System.Windows.Forms.Button();
            this.buttonTrackParticipant = new System.Windows.Forms.Button();
            this.buttonRemoveParticipant = new System.Windows.Forms.Button();
            this.buttonAddParticipant = new System.Windows.Forms.Button();
            this.buttonRaceHistory = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonStopRace = new System.Windows.Forms.Button();
            this.buttonStartRace = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonLoadRace = new System.Windows.Forms.Button();
            this.buttonSaveRaceAs = new System.Windows.Forms.Button();
            this.buttonSaveRace = new System.Windows.Forms.Button();
            this.textBoxRaceName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxWaypoints = new System.Windows.Forms.ListBox();
            this.buttonLoadRoute = new System.Windows.Forms.Button();
            this.textBoxRouteName = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonCommanderTelemetryExportSettings = new System.Windows.Forms.Button();
            this.buttonRaceTelemetryExportSettings = new System.Windows.Forms.Button();
            this.checkBoxExportTargetTelemetry = new System.Windows.Forms.CheckBox();
            this.checkBoxExportRaceTelemetry = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.radioButtonUseCustomServer = new System.Windows.Forms.RadioButton();
            this.radioButtonUseDefaultServer = new System.Windows.Forms.RadioButton();
            this.textBoxUploadServer = new System.Windows.Forms.TextBox();
            this.groupBoxServerInfo = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxServerRaceGuid = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxRaceStatusServerUrl = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.comboBoxTarget = new System.Windows.Forms.ComboBox();
            this.checkBoxShowTargetTelemetry = new System.Windows.Forms.CheckBox();
            this.checkBoxShowRaceTelemetry = new System.Windows.Forms.CheckBox();
            this.timerDownloadRaceTelemetry = new System.Windows.Forms.Timer(this.components);
            this.buttonTest = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.checkBoxTargetClosestTo = new System.Windows.Forms.CheckBox();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLapCount)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxAddCommander.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBoxServerInfo.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.checkBoxAllowMainShip);
            this.groupBox6.Controls.Add(this.checkBoxAllowFighter);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.checkBoxLappedRace);
            this.groupBox6.Controls.Add(this.numericUpDownLapCount);
            this.groupBox6.Controls.Add(this.checkBoxAllowPitstops);
            this.groupBox6.Controls.Add(this.checkBoxAllowSRV);
            this.groupBox6.Controls.Add(this.checkBoxEliminationOnDestruction);
            this.groupBox6.Controls.Add(this.buttonEditStatusMessages);
            this.groupBox6.Location = new System.Drawing.Point(6, 245);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(449, 146);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Race Settings";
            // 
            // checkBoxAllowMainShip
            // 
            this.checkBoxAllowMainShip.AutoSize = true;
            this.checkBoxAllowMainShip.Location = new System.Drawing.Point(219, 19);
            this.checkBoxAllowMainShip.Name = "checkBoxAllowMainShip";
            this.checkBoxAllowMainShip.Size = new System.Drawing.Size(71, 17);
            this.checkBoxAllowMainShip.TabIndex = 10;
            this.checkBoxAllowMainShip.Text = "Main ship";
            this.checkBoxAllowMainShip.UseVisualStyleBackColor = true;
            // 
            // checkBoxAllowFighter
            // 
            this.checkBoxAllowFighter.AutoSize = true;
            this.checkBoxAllowFighter.Location = new System.Drawing.Point(155, 19);
            this.checkBoxAllowFighter.Name = "checkBoxAllowFighter";
            this.checkBoxAllowFighter.Size = new System.Drawing.Size(58, 17);
            this.checkBoxAllowFighter.TabIndex = 9;
            this.checkBoxAllowFighter.Text = "Fighter";
            this.checkBoxAllowFighter.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Allowed vehicles:";
            // 
            // checkBoxLappedRace
            // 
            this.checkBoxLappedRace.AutoSize = true;
            this.checkBoxLappedRace.Location = new System.Drawing.Point(6, 43);
            this.checkBoxLappedRace.Name = "checkBoxLappedRace";
            this.checkBoxLappedRace.Size = new System.Drawing.Size(52, 17);
            this.checkBoxLappedRace.TabIndex = 6;
            this.checkBoxLappedRace.Text = "Laps:";
            this.checkBoxLappedRace.UseVisualStyleBackColor = true;
            // 
            // numericUpDownLapCount
            // 
            this.numericUpDownLapCount.Location = new System.Drawing.Point(64, 42);
            this.numericUpDownLapCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLapCount.Name = "numericUpDownLapCount";
            this.numericUpDownLapCount.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownLapCount.TabIndex = 5;
            this.numericUpDownLapCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // checkBoxAllowPitstops
            // 
            this.checkBoxAllowPitstops.AutoSize = true;
            this.checkBoxAllowPitstops.Checked = true;
            this.checkBoxAllowPitstops.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAllowPitstops.Location = new System.Drawing.Point(188, 43);
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
            this.checkBoxAllowSRV.Location = new System.Drawing.Point(101, 19);
            this.checkBoxAllowSRV.Name = "checkBoxAllowSRV";
            this.checkBoxAllowSRV.Size = new System.Drawing.Size(48, 17);
            this.checkBoxAllowSRV.TabIndex = 1;
            this.checkBoxAllowSRV.Text = "SRV";
            this.checkBoxAllowSRV.UseVisualStyleBackColor = true;
            // 
            // checkBoxEliminationOnDestruction
            // 
            this.checkBoxEliminationOnDestruction.AutoSize = true;
            this.checkBoxEliminationOnDestruction.Checked = true;
            this.checkBoxEliminationOnDestruction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEliminationOnDestruction.Location = new System.Drawing.Point(6, 68);
            this.checkBoxEliminationOnDestruction.Name = "checkBoxEliminationOnDestruction";
            this.checkBoxEliminationOnDestruction.Size = new System.Drawing.Size(175, 17);
            this.checkBoxEliminationOnDestruction.TabIndex = 0;
            this.checkBoxEliminationOnDestruction.Text = "Eliminate on vehicle destruction";
            this.checkBoxEliminationOnDestruction.UseVisualStyleBackColor = true;
            // 
            // buttonEditStatusMessages
            // 
            this.buttonEditStatusMessages.Image = global::Race_Manager.Properties.Resources.Text_16x;
            this.buttonEditStatusMessages.Location = new System.Drawing.Point(296, 15);
            this.buttonEditStatusMessages.Name = "buttonEditStatusMessages";
            this.buttonEditStatusMessages.Size = new System.Drawing.Size(29, 23);
            this.buttonEditStatusMessages.TabIndex = 7;
            this.buttonEditStatusMessages.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.textBoxSystem);
            this.groupBox5.Controls.Add(this.textBoxPlanet);
            this.groupBox5.Location = new System.Drawing.Point(397, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(384, 48);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Location";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(195, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Planet:";
            // 
            // textBoxSystem
            // 
            this.textBoxSystem.Location = new System.Drawing.Point(56, 19);
            this.textBoxSystem.Name = "textBoxSystem";
            this.textBoxSystem.ReadOnly = true;
            this.textBoxSystem.Size = new System.Drawing.Size(133, 20);
            this.textBoxSystem.TabIndex = 0;
            // 
            // textBoxPlanet
            // 
            this.textBoxPlanet.Location = new System.Drawing.Point(241, 19);
            this.textBoxPlanet.Name = "textBoxPlanet";
            this.textBoxPlanet.ReadOnly = true;
            this.textBoxPlanet.Size = new System.Drawing.Size(133, 20);
            this.textBoxPlanet.TabIndex = 1;
            // 
            // checkBoxAutoAddCommanders
            // 
            this.checkBoxAutoAddCommanders.AutoSize = true;
            this.checkBoxAutoAddCommanders.Checked = true;
            this.checkBoxAutoAddCommanders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoAddCommanders.Location = new System.Drawing.Point(6, 162);
            this.checkBoxAutoAddCommanders.Name = "checkBoxAutoAddCommanders";
            this.checkBoxAutoAddCommanders.Size = new System.Drawing.Size(246, 17);
            this.checkBoxAutoAddCommanders.TabIndex = 4;
            this.checkBoxAutoAddCommanders.Text = "Automatically add commanders that are at start";
            this.checkBoxAutoAddCommanders.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBoxAddCommander);
            this.groupBox3.Controls.Add(this.listBoxParticipants);
            this.groupBox3.Controls.Add(this.buttonUneliminate);
            this.groupBox3.Controls.Add(this.checkBoxAutoAddCommanders);
            this.groupBox3.Controls.Add(this.buttonTrackParticipant);
            this.groupBox3.Controls.Add(this.buttonRemoveParticipant);
            this.groupBox3.Controls.Add(this.buttonAddParticipant);
            this.groupBox3.Location = new System.Drawing.Point(6, 60);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(264, 183);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Participants";
            // 
            // groupBoxAddCommander
            // 
            this.groupBoxAddCommander.Controls.Add(this.comboBoxAddCommander);
            this.groupBoxAddCommander.Controls.Add(this.buttonAddCommander);
            this.groupBoxAddCommander.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBoxAddCommander.Location = new System.Drawing.Point(54, 3);
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
            this.listBoxParticipants.Location = new System.Drawing.Point(6, 19);
            this.listBoxParticipants.Name = "listBoxParticipants";
            this.listBoxParticipants.Size = new System.Drawing.Size(217, 134);
            this.listBoxParticipants.TabIndex = 11;
            this.listBoxParticipants.SelectedIndexChanged += new System.EventHandler(this.listBoxParticipants_SelectedIndexChanged);
            // 
            // buttonUneliminate
            // 
            this.buttonUneliminate.Image = global::Race_Manager.Properties.Resources.AdvancedBreakpointDisabled_16x;
            this.buttonUneliminate.Location = new System.Drawing.Point(229, 130);
            this.buttonUneliminate.Name = "buttonUneliminate";
            this.buttonUneliminate.Size = new System.Drawing.Size(29, 23);
            this.buttonUneliminate.TabIndex = 10;
            this.buttonUneliminate.UseVisualStyleBackColor = true;
            // 
            // buttonTrackParticipant
            // 
            this.buttonTrackParticipant.Image = global::Race_Manager.Properties.Resources.Target_16x;
            this.buttonTrackParticipant.Location = new System.Drawing.Point(229, 77);
            this.buttonTrackParticipant.Name = "buttonTrackParticipant";
            this.buttonTrackParticipant.Size = new System.Drawing.Size(29, 23);
            this.buttonTrackParticipant.TabIndex = 3;
            this.buttonTrackParticipant.UseVisualStyleBackColor = true;
            // 
            // buttonRemoveParticipant
            // 
            this.buttonRemoveParticipant.Image = global::Race_Manager.Properties.Resources.Remove_color_16x;
            this.buttonRemoveParticipant.Location = new System.Drawing.Point(229, 48);
            this.buttonRemoveParticipant.Name = "buttonRemoveParticipant";
            this.buttonRemoveParticipant.Size = new System.Drawing.Size(29, 23);
            this.buttonRemoveParticipant.TabIndex = 2;
            this.buttonRemoveParticipant.UseVisualStyleBackColor = true;
            this.buttonRemoveParticipant.Click += new System.EventHandler(this.buttonRemoveParticipant_Click);
            // 
            // buttonAddParticipant
            // 
            this.buttonAddParticipant.Image = global::Race_Manager.Properties.Resources.Add_16x;
            this.buttonAddParticipant.Location = new System.Drawing.Point(229, 19);
            this.buttonAddParticipant.Name = "buttonAddParticipant";
            this.buttonAddParticipant.Size = new System.Drawing.Size(29, 23);
            this.buttonAddParticipant.TabIndex = 1;
            this.buttonAddParticipant.UseVisualStyleBackColor = true;
            this.buttonAddParticipant.Click += new System.EventHandler(this.buttonAddParticipant_Click);
            // 
            // buttonRaceHistory
            // 
            this.buttonRaceHistory.Enabled = false;
            this.buttonRaceHistory.Image = global::Race_Manager.Properties.Resources.History_16x;
            this.buttonRaceHistory.Location = new System.Drawing.Point(613, 350);
            this.buttonRaceHistory.Name = "buttonRaceHistory";
            this.buttonRaceHistory.Size = new System.Drawing.Size(29, 23);
            this.buttonRaceHistory.TabIndex = 9;
            this.buttonRaceHistory.UseVisualStyleBackColor = true;
            // 
            // buttonReset
            // 
            this.buttonReset.Enabled = false;
            this.buttonReset.Image = global::Race_Manager.Properties.Resources.Restart_16x;
            this.buttonReset.Location = new System.Drawing.Point(76, 19);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(29, 23);
            this.buttonReset.TabIndex = 8;
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonStopRace
            // 
            this.buttonStopRace.Enabled = false;
            this.buttonStopRace.Image = global::Race_Manager.Properties.Resources.Stop_16x;
            this.buttonStopRace.Location = new System.Drawing.Point(41, 19);
            this.buttonStopRace.Name = "buttonStopRace";
            this.buttonStopRace.Size = new System.Drawing.Size(29, 23);
            this.buttonStopRace.TabIndex = 6;
            this.buttonStopRace.UseVisualStyleBackColor = true;
            this.buttonStopRace.Click += new System.EventHandler(this.buttonStopRace_Click);
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
            // groupBox2
            // 
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
            // buttonLoadRace
            // 
            this.buttonLoadRace.Image = global::Race_Manager.Properties.Resources.OpenFolder_16x;
            this.buttonLoadRace.Location = new System.Drawing.Point(280, 17);
            this.buttonLoadRace.Name = "buttonLoadRace";
            this.buttonLoadRace.Size = new System.Drawing.Size(29, 23);
            this.buttonLoadRace.TabIndex = 10;
            this.buttonLoadRace.UseVisualStyleBackColor = true;
            this.buttonLoadRace.Click += new System.EventHandler(this.buttonLoadRace_Click);
            // 
            // buttonSaveRaceAs
            // 
            this.buttonSaveRaceAs.Image = global::Race_Manager.Properties.Resources.SaveAs_16x;
            this.buttonSaveRaceAs.Location = new System.Drawing.Point(315, 17);
            this.buttonSaveRaceAs.Name = "buttonSaveRaceAs";
            this.buttonSaveRaceAs.Size = new System.Drawing.Size(29, 23);
            this.buttonSaveRaceAs.TabIndex = 9;
            this.buttonSaveRaceAs.UseVisualStyleBackColor = true;
            this.buttonSaveRaceAs.Click += new System.EventHandler(this.buttonSaveRaceAs_Click);
            // 
            // buttonSaveRace
            // 
            this.buttonSaveRace.Image = global::Race_Manager.Properties.Resources.Save_16x;
            this.buttonSaveRace.Location = new System.Drawing.Point(350, 17);
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
            this.textBoxRaceName.Size = new System.Drawing.Size(268, 20);
            this.textBoxRaceName.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxWaypoints);
            this.groupBox1.Controls.Add(this.buttonLoadRoute);
            this.groupBox1.Controls.Add(this.textBoxRouteName);
            this.groupBox1.Location = new System.Drawing.Point(276, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 183);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Route";
            // 
            // listBoxWaypoints
            // 
            this.listBoxWaypoints.FormattingEnabled = true;
            this.listBoxWaypoints.Location = new System.Drawing.Point(6, 45);
            this.listBoxWaypoints.Name = "listBoxWaypoints";
            this.listBoxWaypoints.Size = new System.Drawing.Size(201, 134);
            this.listBoxWaypoints.TabIndex = 2;
            // 
            // buttonLoadRoute
            // 
            this.buttonLoadRoute.Image = global::Race_Manager.Properties.Resources.OpenFolder_16x;
            this.buttonLoadRoute.Location = new System.Drawing.Point(169, 17);
            this.buttonLoadRoute.Name = "buttonLoadRoute";
            this.buttonLoadRoute.Size = new System.Drawing.Size(38, 23);
            this.buttonLoadRoute.TabIndex = 1;
            this.buttonLoadRoute.UseVisualStyleBackColor = true;
            this.buttonLoadRoute.Click += new System.EventHandler(this.buttonLoadRoute_Click);
            // 
            // textBoxRouteName
            // 
            this.textBoxRouteName.Location = new System.Drawing.Point(6, 19);
            this.textBoxRouteName.Name = "textBoxRouteName";
            this.textBoxRouteName.ReadOnly = true;
            this.textBoxRouteName.Size = new System.Drawing.Size(157, 20);
            this.textBoxRouteName.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonCommanderTelemetryExportSettings);
            this.groupBox4.Controls.Add(this.checkBoxShowTargetTelemetry);
            this.groupBox4.Controls.Add(this.buttonRaceTelemetryExportSettings);
            this.groupBox4.Controls.Add(this.checkBoxShowRaceTelemetry);
            this.groupBox4.Controls.Add(this.checkBoxExportTargetTelemetry);
            this.groupBox4.Controls.Add(this.checkBoxExportRaceTelemetry);
            this.groupBox4.Location = new System.Drawing.Point(495, 110);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(170, 66);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Telemetry";
            // 
            // buttonCommanderTelemetryExportSettings
            // 
            this.buttonCommanderTelemetryExportSettings.Image = global::Race_Manager.Properties.Resources.Settings_16x;
            this.buttonCommanderTelemetryExportSettings.Location = new System.Drawing.Point(143, 38);
            this.buttonCommanderTelemetryExportSettings.Name = "buttonCommanderTelemetryExportSettings";
            this.buttonCommanderTelemetryExportSettings.Size = new System.Drawing.Size(22, 22);
            this.buttonCommanderTelemetryExportSettings.TabIndex = 3;
            this.buttonCommanderTelemetryExportSettings.UseVisualStyleBackColor = true;
            this.buttonCommanderTelemetryExportSettings.Click += new System.EventHandler(this.buttonCommanderTelemetryExportSettings_Click);
            // 
            // buttonRaceTelemetryExportSettings
            // 
            this.buttonRaceTelemetryExportSettings.Image = global::Race_Manager.Properties.Resources.Settings_16x;
            this.buttonRaceTelemetryExportSettings.Location = new System.Drawing.Point(143, 15);
            this.buttonRaceTelemetryExportSettings.Name = "buttonRaceTelemetryExportSettings";
            this.buttonRaceTelemetryExportSettings.Size = new System.Drawing.Size(22, 22);
            this.buttonRaceTelemetryExportSettings.TabIndex = 2;
            this.buttonRaceTelemetryExportSettings.UseVisualStyleBackColor = true;
            this.buttonRaceTelemetryExportSettings.Click += new System.EventHandler(this.buttonRaceTelemetryExportSettings_Click);
            // 
            // checkBoxExportTargetTelemetry
            // 
            this.checkBoxExportTargetTelemetry.AutoSize = true;
            this.checkBoxExportTargetTelemetry.Location = new System.Drawing.Point(81, 42);
            this.checkBoxExportTargetTelemetry.Name = "checkBoxExportTargetTelemetry";
            this.checkBoxExportTargetTelemetry.Size = new System.Drawing.Size(56, 17);
            this.checkBoxExportTargetTelemetry.TabIndex = 1;
            this.checkBoxExportTargetTelemetry.Text = "Export";
            this.checkBoxExportTargetTelemetry.UseVisualStyleBackColor = true;
            // 
            // checkBoxExportRaceTelemetry
            // 
            this.checkBoxExportRaceTelemetry.AutoSize = true;
            this.checkBoxExportRaceTelemetry.Location = new System.Drawing.Point(81, 19);
            this.checkBoxExportRaceTelemetry.Name = "checkBoxExportRaceTelemetry";
            this.checkBoxExportRaceTelemetry.Size = new System.Drawing.Size(56, 17);
            this.checkBoxExportRaceTelemetry.TabIndex = 0;
            this.checkBoxExportRaceTelemetry.Text = "Export";
            this.checkBoxExportRaceTelemetry.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.buttonStartRace);
            this.groupBox7.Controls.Add(this.buttonStopRace);
            this.groupBox7.Controls.Add(this.buttonReset);
            this.groupBox7.Location = new System.Drawing.Point(579, 265);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(115, 51);
            this.groupBox7.TabIndex = 13;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Race Control";
            // 
            // radioButtonUseCustomServer
            // 
            this.radioButtonUseCustomServer.AutoSize = true;
            this.radioButtonUseCustomServer.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radioButtonUseCustomServer.Location = new System.Drawing.Point(71, 18);
            this.radioButtonUseCustomServer.Name = "radioButtonUseCustomServer";
            this.radioButtonUseCustomServer.Size = new System.Drawing.Size(63, 17);
            this.radioButtonUseCustomServer.TabIndex = 3;
            this.radioButtonUseCustomServer.Text = "Custom:";
            this.toolTip1.SetToolTip(this.radioButtonUseCustomServer, "Select to upload to a custom server");
            this.radioButtonUseCustomServer.UseVisualStyleBackColor = true;
            // 
            // radioButtonUseDefaultServer
            // 
            this.radioButtonUseDefaultServer.AutoSize = true;
            this.radioButtonUseDefaultServer.Checked = true;
            this.radioButtonUseDefaultServer.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radioButtonUseDefaultServer.Location = new System.Drawing.Point(6, 18);
            this.radioButtonUseDefaultServer.Name = "radioButtonUseDefaultServer";
            this.radioButtonUseDefaultServer.Size = new System.Drawing.Size(59, 17);
            this.radioButtonUseDefaultServer.TabIndex = 2;
            this.radioButtonUseDefaultServer.TabStop = true;
            this.radioButtonUseDefaultServer.Tag = "srvtracker.darkbytes.co.uk";
            this.radioButtonUseDefaultServer.Text = "Default";
            this.toolTip1.SetToolTip(this.radioButtonUseDefaultServer, "Select to upload to the default server");
            this.radioButtonUseDefaultServer.UseVisualStyleBackColor = true;
            // 
            // textBoxUploadServer
            // 
            this.textBoxUploadServer.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxUploadServer.Enabled = false;
            this.textBoxUploadServer.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxUploadServer.Location = new System.Drawing.Point(140, 17);
            this.textBoxUploadServer.MaxLength = 100;
            this.textBoxUploadServer.Name = "textBoxUploadServer";
            this.textBoxUploadServer.Size = new System.Drawing.Size(140, 20);
            this.textBoxUploadServer.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textBoxUploadServer, "The server to send the status updates to");
            // 
            // groupBoxServerInfo
            // 
            this.groupBoxServerInfo.Controls.Add(this.label8);
            this.groupBoxServerInfo.Controls.Add(this.textBoxServerRaceGuid);
            this.groupBoxServerInfo.Controls.Add(this.label7);
            this.groupBoxServerInfo.Controls.Add(this.textBoxRaceStatusServerUrl);
            this.groupBoxServerInfo.Location = new System.Drawing.Point(690, 60);
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
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.radioButtonUseCustomServer);
            this.groupBox8.Controls.Add(this.radioButtonUseDefaultServer);
            this.groupBox8.Controls.Add(this.textBoxUploadServer);
            this.groupBox8.ForeColor = System.Drawing.Color.Black;
            this.groupBox8.Location = new System.Drawing.Point(495, 60);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(286, 44);
            this.groupBox8.TabIndex = 16;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Server";
            // 
            // comboBoxTarget
            // 
            this.comboBoxTarget.FormattingEnabled = true;
            this.comboBoxTarget.Location = new System.Drawing.Point(6, 19);
            this.comboBoxTarget.Name = "comboBoxTarget";
            this.comboBoxTarget.Size = new System.Drawing.Size(187, 21);
            this.comboBoxTarget.TabIndex = 2;
            // 
            // checkBoxShowTargetTelemetry
            // 
            this.checkBoxShowTargetTelemetry.AutoSize = true;
            this.checkBoxShowTargetTelemetry.Location = new System.Drawing.Point(5, 42);
            this.checkBoxShowTargetTelemetry.Name = "checkBoxShowTargetTelemetry";
            this.checkBoxShowTargetTelemetry.Size = new System.Drawing.Size(57, 17);
            this.checkBoxShowTargetTelemetry.TabIndex = 1;
            this.checkBoxShowTargetTelemetry.Text = "Target";
            this.checkBoxShowTargetTelemetry.UseVisualStyleBackColor = true;
            this.checkBoxShowTargetTelemetry.CheckedChanged += new System.EventHandler(this.checkBoxShowTargetTelemetry_CheckedChanged);
            // 
            // checkBoxShowRaceTelemetry
            // 
            this.checkBoxShowRaceTelemetry.AutoSize = true;
            this.checkBoxShowRaceTelemetry.Location = new System.Drawing.Point(6, 19);
            this.checkBoxShowRaceTelemetry.Name = "checkBoxShowRaceTelemetry";
            this.checkBoxShowRaceTelemetry.Size = new System.Drawing.Size(52, 17);
            this.checkBoxShowRaceTelemetry.TabIndex = 0;
            this.checkBoxShowRaceTelemetry.Text = "Race";
            this.checkBoxShowRaceTelemetry.UseVisualStyleBackColor = true;
            this.checkBoxShowRaceTelemetry.CheckedChanged += new System.EventHandler(this.checkBoxShowRaceTelemetry_CheckedChanged);
            // 
            // timerDownloadRaceTelemetry
            // 
            this.timerDownloadRaceTelemetry.Interval = 700;
            this.timerDownloadRaceTelemetry.Tick += new System.EventHandler(this.timerDownloadRaceTelemetry_Tick);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(485, 350);
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
            this.groupBox10.Location = new System.Drawing.Point(495, 182);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(199, 71);
            this.groupBox10.TabIndex = 19;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Target";
            // 
            // checkBoxTargetClosestTo
            // 
            this.checkBoxTargetClosestTo.AutoSize = true;
            this.checkBoxTargetClosestTo.Location = new System.Drawing.Point(44, 46);
            this.checkBoxTargetClosestTo.Name = "checkBoxTargetClosestTo";
            this.checkBoxTargetClosestTo.Size = new System.Drawing.Size(149, 17);
            this.checkBoxTargetClosestTo.TabIndex = 3;
            this.checkBoxTargetClosestTo.Text = "Closest to this commander";
            this.checkBoxTargetClosestTo.UseVisualStyleBackColor = true;
            // 
            // FormRaceController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 403);
            this.Controls.Add(this.buttonRaceHistory);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBoxServerInfo);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormRaceController";
            this.Text = "Form1";
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLapCount)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBoxAddCommander.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBoxServerInfo.ResumeLayout(false);
            this.groupBoxServerInfo.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox checkBoxLappedRace;
        private System.Windows.Forms.NumericUpDown numericUpDownLapCount;
        private System.Windows.Forms.CheckBox checkBoxAllowPitstops;
        private System.Windows.Forms.CheckBox checkBoxAllowSRV;
        private System.Windows.Forms.CheckBox checkBoxEliminationOnDestruction;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSystem;
        private System.Windows.Forms.TextBox textBoxPlanet;
        private System.Windows.Forms.CheckBox checkBoxAutoAddCommanders;
        private System.Windows.Forms.GroupBox groupBox3;
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxWaypoints;
        private System.Windows.Forms.Button buttonLoadRoute;
        private System.Windows.Forms.TextBox textBoxRouteName;
        private System.Windows.Forms.GroupBox groupBox4;
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
        private System.Windows.Forms.GroupBox groupBox8;
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
    }
}

