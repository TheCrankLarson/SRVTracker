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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxWaypoints = new System.Windows.Forms.ListBox();
            this.buttonLoadRoute = new System.Windows.Forms.Button();
            this.textBoxRouteName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonLoadRace = new System.Windows.Forms.Button();
            this.buttonSaveRaceAs = new System.Windows.Forms.Button();
            this.buttonSaveRace = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRaceName = new System.Windows.Forms.TextBox();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPlanet = new System.Windows.Forms.TextBox();
            this.textBoxSystem = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBoxShowDetailedStatus = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoAddCommanders = new System.Windows.Forms.CheckBox();
            this.numericUpDownPaddingChars = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxExportLeaderboard = new System.Windows.Forms.CheckBox();
            this.checkBoxEliminationOnDestruction = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxAddCommander.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPaddingChars)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxWaypoints);
            this.groupBox1.Controls.Add(this.buttonLoadRoute);
            this.groupBox1.Controls.Add(this.textBoxRouteName);
            this.groupBox1.Location = new System.Drawing.Point(12, 91);
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
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxRaceName);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(335, 73);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Event";
            // 
            // buttonLoadRace
            // 
            this.buttonLoadRace.Image = global::SRVTracker.Properties.Resources.OpenFile_16x;
            this.buttonLoadRace.Location = new System.Drawing.Point(230, 36);
            this.buttonLoadRace.Name = "buttonLoadRace";
            this.buttonLoadRace.Size = new System.Drawing.Size(29, 23);
            this.buttonLoadRace.TabIndex = 10;
            this.buttonLoadRace.UseVisualStyleBackColor = true;
            this.buttonLoadRace.Click += new System.EventHandler(this.buttonLoadRace_Click);
            // 
            // buttonSaveRaceAs
            // 
            this.buttonSaveRaceAs.Image = global::SRVTracker.Properties.Resources.SaveAs_16x;
            this.buttonSaveRaceAs.Location = new System.Drawing.Point(265, 36);
            this.buttonSaveRaceAs.Name = "buttonSaveRaceAs";
            this.buttonSaveRaceAs.Size = new System.Drawing.Size(29, 23);
            this.buttonSaveRaceAs.TabIndex = 9;
            this.buttonSaveRaceAs.UseVisualStyleBackColor = true;
            this.buttonSaveRaceAs.Click += new System.EventHandler(this.buttonSaveRaceAs_Click);
            // 
            // buttonSaveRace
            // 
            this.buttonSaveRace.Image = global::SRVTracker.Properties.Resources.Save_16x;
            this.buttonSaveRace.Location = new System.Drawing.Point(300, 36);
            this.buttonSaveRace.Name = "buttonSaveRace";
            this.buttonSaveRace.Size = new System.Drawing.Size(29, 23);
            this.buttonSaveRace.TabIndex = 8;
            this.buttonSaveRace.UseVisualStyleBackColor = true;
            this.buttonSaveRace.Click += new System.EventHandler(this.buttonSaveRace_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name:";
            // 
            // textBoxRaceName
            // 
            this.textBoxRaceName.Location = new System.Drawing.Point(9, 38);
            this.textBoxRaceName.Name = "textBoxRaceName";
            this.textBoxRaceName.Size = new System.Drawing.Size(215, 20);
            this.textBoxRaceName.TabIndex = 2;
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Location = new System.Drawing.Point(220, 19);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(135, 20);
            this.dateTimePickerStart.TabIndex = 6;
            this.dateTimePickerStart.ValueChanged += new System.EventHandler(this.dateTimePickerStart_ValueChanged);
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
            this.textBoxPlanet.Size = new System.Drawing.Size(158, 20);
            this.textBoxPlanet.TabIndex = 1;
            // 
            // textBoxSystem
            // 
            this.textBoxSystem.Location = new System.Drawing.Point(56, 19);
            this.textBoxSystem.Name = "textBoxSystem";
            this.textBoxSystem.ReadOnly = true;
            this.textBoxSystem.Size = new System.Drawing.Size(158, 20);
            this.textBoxSystem.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBoxAddCommander);
            this.groupBox3.Controls.Add(this.buttonStopRace);
            this.groupBox3.Controls.Add(this.buttonStartRace);
            this.groupBox3.Controls.Add(this.buttonTrackParticipant);
            this.groupBox3.Controls.Add(this.buttonRemoveParticipant);
            this.groupBox3.Controls.Add(this.buttonAddParticipant);
            this.groupBox3.Controls.Add(this.listViewParticipants);
            this.groupBox3.Location = new System.Drawing.Point(318, 91);
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
            this.buttonStopRace.Location = new System.Drawing.Point(361, 318);
            this.buttonStopRace.Name = "buttonStopRace";
            this.buttonStopRace.Size = new System.Drawing.Size(29, 23);
            this.buttonStopRace.TabIndex = 6;
            this.buttonStopRace.UseVisualStyleBackColor = true;
            this.buttonStopRace.Click += new System.EventHandler(this.buttonStopRace_Click);
            // 
            // buttonStartRace
            // 
            this.buttonStartRace.Enabled = false;
            this.buttonStartRace.Image = global::SRVTracker.Properties.Resources.Run_16x;
            this.buttonStartRace.Location = new System.Drawing.Point(361, 289);
            this.buttonStartRace.Name = "buttonStartRace";
            this.buttonStartRace.Size = new System.Drawing.Size(29, 23);
            this.buttonStartRace.TabIndex = 5;
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
            this.buttonAddParticipant.UseVisualStyleBackColor = true;
            this.buttonAddParticipant.Click += new System.EventHandler(this.buttonAddParticipant_Click);
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
            this.groupBox4.Controls.Add(this.numericUpDownPaddingChars);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.checkBoxExportLeaderboard);
            this.groupBox4.Controls.Add(this.checkBoxEliminationOnDestruction);
            this.groupBox4.Location = new System.Drawing.Point(12, 280);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(300, 158);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Race Management";
            // 
            // checkBoxShowDetailedStatus
            // 
            this.checkBoxShowDetailedStatus.AutoSize = true;
            this.checkBoxShowDetailedStatus.Checked = true;
            this.checkBoxShowDetailedStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowDetailedStatus.Location = new System.Drawing.Point(6, 88);
            this.checkBoxShowDetailedStatus.Name = "checkBoxShowDetailedStatus";
            this.checkBoxShowDetailedStatus.Size = new System.Drawing.Size(128, 17);
            this.checkBoxShowDetailedStatus.TabIndex = 5;
            this.checkBoxShowDetailedStatus.Text = "Show Detailed Status";
            this.checkBoxShowDetailedStatus.UseVisualStyleBackColor = true;
            this.checkBoxShowDetailedStatus.CheckedChanged += new System.EventHandler(this.checkBoxShowDetailedStatus_CheckedChanged);
            // 
            // checkBoxAutoAddCommanders
            // 
            this.checkBoxAutoAddCommanders.AutoSize = true;
            this.checkBoxAutoAddCommanders.Checked = true;
            this.checkBoxAutoAddCommanders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoAddCommanders.Location = new System.Drawing.Point(6, 65);
            this.checkBoxAutoAddCommanders.Name = "checkBoxAutoAddCommanders";
            this.checkBoxAutoAddCommanders.Size = new System.Drawing.Size(287, 17);
            this.checkBoxAutoAddCommanders.TabIndex = 4;
            this.checkBoxAutoAddCommanders.Text = "Automatically add commanders that are at first waypoint";
            this.checkBoxAutoAddCommanders.UseVisualStyleBackColor = true;
            // 
            // numericUpDownPaddingChars
            // 
            this.numericUpDownPaddingChars.Location = new System.Drawing.Point(121, 41);
            this.numericUpDownPaddingChars.Name = "numericUpDownPaddingChars";
            this.numericUpDownPaddingChars.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownPaddingChars.TabIndex = 3;
            this.numericUpDownPaddingChars.Value = new decimal(new int[] {
            38,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(177, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "padding characters";
            // 
            // checkBoxExportLeaderboard
            // 
            this.checkBoxExportLeaderboard.AutoSize = true;
            this.checkBoxExportLeaderboard.Location = new System.Drawing.Point(6, 42);
            this.checkBoxExportLeaderboard.Name = "checkBoxExportLeaderboard";
            this.checkBoxExportLeaderboard.Size = new System.Drawing.Size(115, 17);
            this.checkBoxExportLeaderboard.TabIndex = 1;
            this.checkBoxExportLeaderboard.Text = "Export leaderboard";
            this.checkBoxExportLeaderboard.UseVisualStyleBackColor = true;
            // 
            // checkBoxEliminationOnDestruction
            // 
            this.checkBoxEliminationOnDestruction.AutoSize = true;
            this.checkBoxEliminationOnDestruction.Checked = true;
            this.checkBoxEliminationOnDestruction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEliminationOnDestruction.Location = new System.Drawing.Point(6, 19);
            this.checkBoxEliminationOnDestruction.Name = "checkBoxEliminationOnDestruction";
            this.checkBoxEliminationOnDestruction.Size = new System.Drawing.Size(183, 17);
            this.checkBoxEliminationOnDestruction.TabIndex = 0;
            this.checkBoxEliminationOnDestruction.Text = "Elimination on vehicle destruction";
            this.checkBoxEliminationOnDestruction.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dateTimePickerStartTime);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.dateTimePickerStart);
            this.groupBox5.Controls.Add(this.textBoxSystem);
            this.groupBox5.Controls.Add(this.textBoxPlanet);
            this.groupBox5.Location = new System.Drawing.Point(353, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(361, 73);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Time and Place";
            // 
            // dateTimePickerStartTime
            // 
            this.dateTimePickerStartTime.CustomFormat = "HH:mm";
            this.dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerStartTime.Location = new System.Drawing.Point(220, 45);
            this.dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            this.dateTimePickerStartTime.ShowUpDown = true;
            this.dateTimePickerStartTime.Size = new System.Drawing.Size(135, 20);
            this.dateTimePickerStartTime.TabIndex = 7;
            this.dateTimePickerStartTime.ValueChanged += new System.EventHandler(this.dateTimePickerStartTime_ValueChanged);
            // 
            // FormRaceMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 450);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPaddingChars)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonLoadRoute;
        private System.Windows.Forms.TextBox textBoxRouteName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.NumericUpDown numericUpDownPaddingChars;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonStartRace;
        private System.Windows.Forms.CheckBox checkBoxAutoAddCommanders;
        private System.Windows.Forms.Button buttonStopRace;
        private System.Windows.Forms.Button buttonSaveRaceAs;
        private System.Windows.Forms.Button buttonSaveRace;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime;
        private System.Windows.Forms.Button buttonLoadRace;
        private System.Windows.Forms.GroupBox groupBoxAddCommander;
        private System.Windows.Forms.Button buttonAddCommander;
        private System.Windows.Forms.ComboBox comboBoxAddCommander;
        private System.Windows.Forms.CheckBox checkBoxShowDetailedStatus;
    }
}