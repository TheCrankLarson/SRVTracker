namespace SRVTracker
{
    partial class FormRaceMonitor
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxWaypoints = new System.Windows.Forms.ListBox();
            this.textBoxRouteName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxRaceName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPlanet = new System.Windows.Forms.TextBox();
            this.textBoxSystem = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBoxAddCommander = new System.Windows.Forms.GroupBox();
            this.comboBoxAddCommander = new System.Windows.Forms.ComboBox();
            this.listViewParticipants = new System.Windows.Forms.ListView();
            this.columnHeaderPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDistanceToWaypoint = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBoxShowDetailedStatus = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoAddCommanders = new System.Windows.Forms.CheckBox();
            this.checkBoxExportDistance = new System.Windows.Forms.CheckBox();
            this.numericUpDownLeaderboardPadding = new System.Windows.Forms.NumericUpDown();
            this.checkBoxExportLeaderboard = new System.Windows.Forms.CheckBox();
            this.checkBoxEliminationOnDestruction = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.checkBoxStreamInfo = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowPitstops = new System.Windows.Forms.CheckBox();
            this.checkBoxSRVRace = new System.Windows.Forms.CheckBox();
            this.checkBoxClosestPlayerTarget = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownTargetPadding = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownStatusPadding = new System.Windows.Forms.NumericUpDown();
            this.textBoxPaddingChar = new System.Windows.Forms.TextBox();
            this.checkBoxPaddingCharacters = new System.Windows.Forms.CheckBox();
            this.textBoxExportTargetFile = new System.Windows.Forms.TextBox();
            this.checkBoxExportTarget = new System.Windows.Forms.CheckBox();
            this.textBoxExportStatusFile = new System.Windows.Forms.TextBox();
            this.textBoxExportLeaderboardFile = new System.Windows.Forms.TextBox();
            this.checkBoxExportStatus = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numericUpDownSpeedPadding = new System.Windows.Forms.NumericUpDown();
            this.textBoxExportSpeedFile = new System.Windows.Forms.TextBox();
            this.checkBoxExportSpeed = new System.Windows.Forms.CheckBox();
            this.groupBoxTextExport = new System.Windows.Forms.GroupBox();
            this.groupBoxHTMLExport = new System.Windows.Forms.GroupBox();
            this.textBoxHTMLTemplateFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxExportHTMLTo = new System.Windows.Forms.TextBox();
            this.checkBoxExportAsHTML = new System.Windows.Forms.CheckBox();
            this.buttonRaceHistory = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonEditStatusMessages = new System.Windows.Forms.Button();
            this.buttonAddCommander = new System.Windows.Forms.Button();
            this.buttonStopRace = new System.Windows.Forms.Button();
            this.buttonStartRace = new System.Windows.Forms.Button();
            this.buttonTrackParticipant = new System.Windows.Forms.Button();
            this.buttonRemoveParticipant = new System.Windows.Forms.Button();
            this.buttonAddParticipant = new System.Windows.Forms.Button();
            this.buttonLoadRace = new System.Windows.Forms.Button();
            this.buttonSaveRaceAs = new System.Windows.Forms.Button();
            this.buttonSaveRace = new System.Windows.Forms.Button();
            this.buttonLoadRoute = new System.Windows.Forms.Button();
            this.checkBoxIncludeMaxSpeed = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxAddCommander.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeaderboardPadding)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTargetPadding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStatusPadding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeedPadding)).BeginInit();
            this.groupBoxTextExport.SuspendLayout();
            this.groupBoxHTMLExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxWaypoints);
            this.groupBox1.Controls.Add(this.buttonLoadRoute);
            this.groupBox1.Controls.Add(this.textBoxRouteName);
            this.groupBox1.Location = new System.Drawing.Point(414, 91);
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
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(396, 48);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Event";
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
            this.groupBox3.Location = new System.Drawing.Point(12, 66);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(396, 347);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Participants";
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
            // listViewParticipants
            // 
            this.listViewParticipants.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPosition,
            this.columnHeaderName,
            this.columnHeaderStatus,
            this.columnHeaderDistanceToWaypoint});
            this.listViewParticipants.FullRowSelect = true;
            this.listViewParticipants.GridLines = true;
            this.listViewParticipants.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewParticipants.HideSelection = false;
            this.listViewParticipants.LabelWrap = false;
            this.listViewParticipants.Location = new System.Drawing.Point(6, 19);
            this.listViewParticipants.MultiSelect = false;
            this.listViewParticipants.Name = "listViewParticipants";
            this.listViewParticipants.Size = new System.Drawing.Size(349, 322);
            this.listViewParticipants.Sorting = System.Windows.Forms.SortOrder.Ascending;
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
            this.columnHeaderStatus.Width = 150;
            // 
            // columnHeaderDistanceToWaypoint
            // 
            this.columnHeaderDistanceToWaypoint.Text = "WP";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBoxShowDetailedStatus);
            this.groupBox4.Controls.Add(this.checkBoxAutoAddCommanders);
            this.groupBox4.Location = new System.Drawing.Point(414, 349);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(300, 70);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Race Management";
            // 
            // checkBoxShowDetailedStatus
            // 
            this.checkBoxShowDetailedStatus.AutoSize = true;
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
            this.checkBoxExportDistance.Checked = true;
            this.checkBoxExportDistance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxExportDistance.Location = new System.Drawing.Point(8, 216);
            this.checkBoxExportDistance.Name = "checkBoxExportDistance";
            this.checkBoxExportDistance.Size = new System.Drawing.Size(189, 17);
            this.checkBoxExportDistance.TabIndex = 6;
            this.checkBoxExportDistance.Text = "Export waypoint distance as status";
            this.checkBoxExportDistance.UseVisualStyleBackColor = true;
            this.checkBoxExportDistance.CheckedChanged += new System.EventHandler(this.checkBoxExportDistance_CheckedChanged);
            // 
            // numericUpDownLeaderboardPadding
            // 
            this.numericUpDownLeaderboardPadding.Location = new System.Drawing.Point(179, 42);
            this.numericUpDownLeaderboardPadding.Name = "numericUpDownLeaderboardPadding";
            this.numericUpDownLeaderboardPadding.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownLeaderboardPadding.TabIndex = 3;
            this.numericUpDownLeaderboardPadding.Value = new decimal(new int[] {
            38,
            0,
            0,
            0});
            // 
            // checkBoxExportLeaderboard
            // 
            this.checkBoxExportLeaderboard.AutoSize = true;
            this.checkBoxExportLeaderboard.Location = new System.Drawing.Point(6, 19);
            this.checkBoxExportLeaderboard.Name = "checkBoxExportLeaderboard";
            this.checkBoxExportLeaderboard.Size = new System.Drawing.Size(118, 17);
            this.checkBoxExportLeaderboard.TabIndex = 1;
            this.checkBoxExportLeaderboard.Text = "Export leaderboard:";
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
            this.groupBox5.Location = new System.Drawing.Point(414, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(300, 73);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Location";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.checkBoxStreamInfo);
            this.groupBox6.Controls.Add(this.checkBoxAllowPitstops);
            this.groupBox6.Controls.Add(this.checkBoxSRVRace);
            this.groupBox6.Controls.Add(this.checkBoxEliminationOnDestruction);
            this.groupBox6.Location = new System.Drawing.Point(414, 280);
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
            this.checkBoxAllowPitstops.Location = new System.Drawing.Point(109, 42);
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
            this.checkBoxSRVRace.Size = new System.Drawing.Size(94, 17);
            this.checkBoxSRVRace.TabIndex = 1;
            this.checkBoxSRVRace.Text = "SRV only race";
            this.toolTip1.SetToolTip(this.checkBoxSRVRace, "If enabled, competitors will be eliminated if they use their ship");
            this.checkBoxSRVRace.UseVisualStyleBackColor = true;
            this.checkBoxSRVRace.CheckedChanged += new System.EventHandler(this.checkBoxSRVRace_CheckedChanged);
            // 
            // checkBoxClosestPlayerTarget
            // 
            this.checkBoxClosestPlayerTarget.AutoSize = true;
            this.checkBoxClosestPlayerTarget.Checked = true;
            this.checkBoxClosestPlayerTarget.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxClosestPlayerTarget.Location = new System.Drawing.Point(8, 264);
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
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Padding";
            // 
            // numericUpDownTargetPadding
            // 
            this.numericUpDownTargetPadding.Location = new System.Drawing.Point(179, 139);
            this.numericUpDownTargetPadding.Name = "numericUpDownTargetPadding";
            this.numericUpDownTargetPadding.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownTargetPadding.TabIndex = 16;
            this.numericUpDownTargetPadding.Value = new decimal(new int[] {
            38,
            0,
            0,
            0});
            // 
            // numericUpDownStatusPadding
            // 
            this.numericUpDownStatusPadding.Location = new System.Drawing.Point(179, 90);
            this.numericUpDownStatusPadding.Name = "numericUpDownStatusPadding";
            this.numericUpDownStatusPadding.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownStatusPadding.TabIndex = 15;
            this.numericUpDownStatusPadding.Value = new decimal(new int[] {
            38,
            0,
            0,
            0});
            // 
            // textBoxPaddingChar
            // 
            this.textBoxPaddingChar.Location = new System.Drawing.Point(167, 239);
            this.textBoxPaddingChar.Name = "textBoxPaddingChar";
            this.textBoxPaddingChar.Size = new System.Drawing.Size(30, 20);
            this.textBoxPaddingChar.TabIndex = 13;
            this.textBoxPaddingChar.Text = " ";
            // 
            // checkBoxPaddingCharacters
            // 
            this.checkBoxPaddingCharacters.AutoSize = true;
            this.checkBoxPaddingCharacters.Checked = true;
            this.checkBoxPaddingCharacters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPaddingCharacters.Location = new System.Drawing.Point(7, 241);
            this.checkBoxPaddingCharacters.Name = "checkBoxPaddingCharacters";
            this.checkBoxPaddingCharacters.Size = new System.Drawing.Size(154, 17);
            this.checkBoxPaddingCharacters.TabIndex = 12;
            this.checkBoxPaddingCharacters.Text = "Enable padding, character:";
            this.checkBoxPaddingCharacters.UseVisualStyleBackColor = true;
            this.checkBoxPaddingCharacters.CheckedChanged += new System.EventHandler(this.checkBoxPaddingCharacters_CheckedChanged);
            // 
            // textBoxExportTargetFile
            // 
            this.textBoxExportTargetFile.Location = new System.Drawing.Point(25, 139);
            this.textBoxExportTargetFile.Name = "textBoxExportTargetFile";
            this.textBoxExportTargetFile.Size = new System.Drawing.Size(148, 20);
            this.textBoxExportTargetFile.TabIndex = 11;
            this.textBoxExportTargetFile.Text = "Tracking-Name.txt";
            // 
            // checkBoxExportTarget
            // 
            this.checkBoxExportTarget.AutoSize = true;
            this.checkBoxExportTarget.Location = new System.Drawing.Point(6, 116);
            this.checkBoxExportTarget.Name = "checkBoxExportTarget";
            this.checkBoxExportTarget.Size = new System.Drawing.Size(89, 17);
            this.checkBoxExportTarget.TabIndex = 10;
            this.checkBoxExportTarget.Text = "Export target:";
            this.checkBoxExportTarget.UseVisualStyleBackColor = true;
            this.checkBoxExportTarget.CheckedChanged += new System.EventHandler(this.checkBoxExportTarget_CheckedChanged);
            // 
            // textBoxExportStatusFile
            // 
            this.textBoxExportStatusFile.Location = new System.Drawing.Point(24, 90);
            this.textBoxExportStatusFile.Name = "textBoxExportStatusFile";
            this.textBoxExportStatusFile.Size = new System.Drawing.Size(149, 20);
            this.textBoxExportStatusFile.TabIndex = 9;
            this.textBoxExportStatusFile.Text = "Timing-Stats.txt";
            // 
            // textBoxExportLeaderboardFile
            // 
            this.textBoxExportLeaderboardFile.Location = new System.Drawing.Point(25, 42);
            this.textBoxExportLeaderboardFile.Name = "textBoxExportLeaderboardFile";
            this.textBoxExportLeaderboardFile.Size = new System.Drawing.Size(148, 20);
            this.textBoxExportLeaderboardFile.TabIndex = 8;
            this.textBoxExportLeaderboardFile.Text = "Timing-Names.txt";
            // 
            // checkBoxExportStatus
            // 
            this.checkBoxExportStatus.AutoSize = true;
            this.checkBoxExportStatus.Location = new System.Drawing.Point(6, 67);
            this.checkBoxExportStatus.Name = "checkBoxExportStatus";
            this.checkBoxExportStatus.Size = new System.Drawing.Size(90, 17);
            this.checkBoxExportStatus.TabIndex = 7;
            this.checkBoxExportStatus.Text = "Export status:";
            this.checkBoxExportStatus.UseVisualStyleBackColor = true;
            this.checkBoxExportStatus.CheckedChanged += new System.EventHandler(this.checkBoxExportStatus_CheckedChanged);
            // 
            // numericUpDownSpeedPadding
            // 
            this.numericUpDownSpeedPadding.Location = new System.Drawing.Point(179, 188);
            this.numericUpDownSpeedPadding.Name = "numericUpDownSpeedPadding";
            this.numericUpDownSpeedPadding.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownSpeedPadding.TabIndex = 21;
            this.numericUpDownSpeedPadding.Value = new decimal(new int[] {
            38,
            0,
            0,
            0});
            // 
            // textBoxExportSpeedFile
            // 
            this.textBoxExportSpeedFile.Location = new System.Drawing.Point(25, 188);
            this.textBoxExportSpeedFile.Name = "textBoxExportSpeedFile";
            this.textBoxExportSpeedFile.Size = new System.Drawing.Size(148, 20);
            this.textBoxExportSpeedFile.TabIndex = 20;
            this.textBoxExportSpeedFile.Text = "Speeds.txt";
            // 
            // checkBoxExportSpeed
            // 
            this.checkBoxExportSpeed.AutoSize = true;
            this.checkBoxExportSpeed.Location = new System.Drawing.Point(7, 165);
            this.checkBoxExportSpeed.Name = "checkBoxExportSpeed";
            this.checkBoxExportSpeed.Size = new System.Drawing.Size(91, 17);
            this.checkBoxExportSpeed.TabIndex = 19;
            this.checkBoxExportSpeed.Text = "Export speed:";
            this.checkBoxExportSpeed.UseVisualStyleBackColor = true;
            this.checkBoxExportSpeed.CheckedChanged += new System.EventHandler(this.checkBoxExportSpeed_CheckedChanged);
            // 
            // groupBoxTextExport
            // 
            this.groupBoxTextExport.Controls.Add(this.checkBoxIncludeMaxSpeed);
            this.groupBoxTextExport.Controls.Add(this.checkBoxExportLeaderboard);
            this.groupBoxTextExport.Controls.Add(this.numericUpDownLeaderboardPadding);
            this.groupBoxTextExport.Controls.Add(this.checkBoxClosestPlayerTarget);
            this.groupBoxTextExport.Controls.Add(this.numericUpDownSpeedPadding);
            this.groupBoxTextExport.Controls.Add(this.checkBoxExportDistance);
            this.groupBoxTextExport.Controls.Add(this.textBoxExportSpeedFile);
            this.groupBoxTextExport.Controls.Add(this.checkBoxExportStatus);
            this.groupBoxTextExport.Controls.Add(this.checkBoxExportSpeed);
            this.groupBoxTextExport.Controls.Add(this.textBoxExportLeaderboardFile);
            this.groupBoxTextExport.Controls.Add(this.textBoxExportStatusFile);
            this.groupBoxTextExport.Controls.Add(this.label4);
            this.groupBoxTextExport.Controls.Add(this.checkBoxExportTarget);
            this.groupBoxTextExport.Controls.Add(this.numericUpDownTargetPadding);
            this.groupBoxTextExport.Controls.Add(this.textBoxExportTargetFile);
            this.groupBoxTextExport.Controls.Add(this.numericUpDownStatusPadding);
            this.groupBoxTextExport.Controls.Add(this.checkBoxPaddingCharacters);
            this.groupBoxTextExport.Controls.Add(this.textBoxPaddingChar);
            this.groupBoxTextExport.Location = new System.Drawing.Point(720, 12);
            this.groupBoxTextExport.Name = "groupBoxTextExport";
            this.groupBoxTextExport.Size = new System.Drawing.Size(245, 295);
            this.groupBoxTextExport.TabIndex = 7;
            this.groupBoxTextExport.TabStop = false;
            this.groupBoxTextExport.Text = "Export as text options";
            // 
            // groupBoxHTMLExport
            // 
            this.groupBoxHTMLExport.Controls.Add(this.checkBoxExportAsHTML);
            this.groupBoxHTMLExport.Controls.Add(this.textBoxExportHTMLTo);
            this.groupBoxHTMLExport.Controls.Add(this.label5);
            this.groupBoxHTMLExport.Controls.Add(this.label1);
            this.groupBoxHTMLExport.Controls.Add(this.textBoxHTMLTemplateFile);
            this.groupBoxHTMLExport.Location = new System.Drawing.Point(720, 313);
            this.groupBoxHTMLExport.Name = "groupBoxHTMLExport";
            this.groupBoxHTMLExport.Size = new System.Drawing.Size(245, 106);
            this.groupBoxHTMLExport.TabIndex = 8;
            this.groupBoxHTMLExport.TabStop = false;
            this.groupBoxHTMLExport.Text = "Export as HTML options";
            // 
            // textBoxHTMLTemplateFile
            // 
            this.textBoxHTMLTemplateFile.Location = new System.Drawing.Point(99, 40);
            this.textBoxHTMLTemplateFile.Name = "textBoxHTMLTemplateFile";
            this.textBoxHTMLTemplateFile.Size = new System.Drawing.Size(140, 20);
            this.textBoxHTMLTemplateFile.TabIndex = 0;
            this.textBoxHTMLTemplateFile.Text = "Leaderboard Template.html";
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Export to:";
            // 
            // textBoxExportHTMLTo
            // 
            this.textBoxExportHTMLTo.Location = new System.Drawing.Point(64, 66);
            this.textBoxExportHTMLTo.Name = "textBoxExportHTMLTo";
            this.textBoxExportHTMLTo.Size = new System.Drawing.Size(175, 20);
            this.textBoxExportHTMLTo.TabIndex = 3;
            this.textBoxExportHTMLTo.Text = "leaderboard.html";
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
            this.buttonEditStatusMessages.Location = new System.Drawing.Point(361, 231);
            this.buttonEditStatusMessages.Name = "buttonEditStatusMessages";
            this.buttonEditStatusMessages.Size = new System.Drawing.Size(29, 23);
            this.buttonEditStatusMessages.TabIndex = 7;
            this.toolTip1.SetToolTip(this.buttonEditStatusMessages, "Edit status messages");
            this.buttonEditStatusMessages.UseVisualStyleBackColor = true;
            this.buttonEditStatusMessages.Click += new System.EventHandler(this.buttonEditStatusMessages_Click);
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
            // checkBoxIncludeMaxSpeed
            // 
            this.checkBoxIncludeMaxSpeed.AutoSize = true;
            this.checkBoxIncludeMaxSpeed.Location = new System.Drawing.Point(104, 165);
            this.checkBoxIncludeMaxSpeed.Name = "checkBoxIncludeMaxSpeed";
            this.checkBoxIncludeMaxSpeed.Size = new System.Drawing.Size(114, 17);
            this.checkBoxIncludeMaxSpeed.TabIndex = 22;
            this.checkBoxIncludeMaxSpeed.Text = "include max speed";
            this.checkBoxIncludeMaxSpeed.UseVisualStyleBackColor = true;
            // 
            // FormRaceMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 425);
            this.Controls.Add(this.groupBoxHTMLExport);
            this.Controls.Add(this.groupBoxTextExport);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormRaceMonitor";
            this.Text = "Race Monitor";
            this.Deactivate += new System.EventHandler(this.FormRaceMonitor_Deactivate);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBoxAddCommander.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeaderboardPadding)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTargetPadding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStatusPadding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeedPadding)).EndInit();
            this.groupBoxTextExport.ResumeLayout(false);
            this.groupBoxTextExport.PerformLayout();
            this.groupBoxHTMLExport.ResumeLayout(false);
            this.groupBoxHTMLExport.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBoxExportLeaderboard;
        private System.Windows.Forms.CheckBox checkBoxEliminationOnDestruction;
        private System.Windows.Forms.ListBox listBoxWaypoints;
        private System.Windows.Forms.NumericUpDown numericUpDownLeaderboardPadding;
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
        private System.Windows.Forms.CheckBox checkBoxPaddingCharacters;
        private System.Windows.Forms.TextBox textBoxExportTargetFile;
        private System.Windows.Forms.CheckBox checkBoxExportTarget;
        private System.Windows.Forms.TextBox textBoxExportStatusFile;
        private System.Windows.Forms.TextBox textBoxExportLeaderboardFile;
        private System.Windows.Forms.CheckBox checkBoxExportStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownTargetPadding;
        private System.Windows.Forms.NumericUpDown numericUpDownStatusPadding;
        private System.Windows.Forms.TextBox textBoxPaddingChar;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBoxStreamInfo;
        private System.Windows.Forms.CheckBox checkBoxClosestPlayerTarget;
        private System.Windows.Forms.Button buttonRaceHistory;
        private System.Windows.Forms.NumericUpDown numericUpDownSpeedPadding;
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
    }
}