namespace Race_Manager
{
    partial class FormTrackedTargetExports
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
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "Target name",
            "Tracking-Name.txt"}, -1);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
            "Current leaderboard position",
            "Tracking-Pos.txt"}, -1);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
            "Target current speed",
            "Tracking-Speed.txt"}, -1);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
            "Target maximum speed",
            "Tracking-Max.txt"}, -1);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
            "Target average speed",
            "Tracking-AvSpeed.txt"}, -1);
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
            "Number of pitstops",
            "Tracking-Pit.txt"}, -1);
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem(new string[] {
            "Current hull status",
            "Tracking-Hull.txt"}, -1);
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem(new string[] {
            "Total distance left",
            "Tracking-Distance.txt"}, -1);
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem(new string[] {
            "Current lap",
            "Tracking-Lap.txt"}, -1);
            this.groupBoxTrackTarget = new System.Windows.Forms.GroupBox();
            this.textBoxExportTargetLapNumber = new System.Windows.Forms.TextBox();
            this.checkBoxExportTrackedTargetLapNumber = new System.Windows.Forms.CheckBox();
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
            this.checkBoxExportTrackedTarget = new System.Windows.Forms.CheckBox();
            this.textBoxExportTargetFile = new System.Windows.Forms.TextBox();
            this.checkBoxClosestPlayerTarget = new System.Windows.Forms.CheckBox();
            this.listViewExportSettings = new System.Windows.Forms.ListView();
            this.columnExportDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnExportFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxClosestTarget = new System.Windows.Forms.ComboBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBoxTrackTarget.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxTrackTarget
            // 
            this.groupBoxTrackTarget.Controls.Add(this.textBoxExportTargetLapNumber);
            this.groupBoxTrackTarget.Controls.Add(this.checkBoxExportTrackedTargetLapNumber);
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
            this.groupBoxTrackTarget.Location = new System.Drawing.Point(500, 158);
            this.groupBoxTrackTarget.Name = "groupBoxTrackTarget";
            this.groupBoxTrackTarget.Size = new System.Drawing.Size(245, 258);
            this.groupBoxTrackTarget.TabIndex = 10;
            this.groupBoxTrackTarget.TabStop = false;
            this.groupBoxTrackTarget.Text = "Track target";
            // 
            // textBoxExportTargetLapNumber
            // 
            this.textBoxExportTargetLapNumber.Location = new System.Drawing.Point(134, 224);
            this.textBoxExportTargetLapNumber.Name = "textBoxExportTargetLapNumber";
            this.textBoxExportTargetLapNumber.Size = new System.Drawing.Size(105, 20);
            this.textBoxExportTargetLapNumber.TabIndex = 30;
            this.textBoxExportTargetLapNumber.Text = "Tracking-Lap.txt";
            // 
            // checkBoxExportTrackedTargetLapNumber
            // 
            this.checkBoxExportTrackedTargetLapNumber.AutoSize = true;
            this.checkBoxExportTrackedTargetLapNumber.Location = new System.Drawing.Point(6, 226);
            this.checkBoxExportTrackedTargetLapNumber.Name = "checkBoxExportTrackedTargetLapNumber";
            this.checkBoxExportTrackedTargetLapNumber.Size = new System.Drawing.Size(114, 17);
            this.checkBoxExportTrackedTargetLapNumber.TabIndex = 29;
            this.checkBoxExportTrackedTargetLapNumber.Text = "Export lap number:";
            this.checkBoxExportTrackedTargetLapNumber.UseVisualStyleBackColor = true;
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
            // 
            // textBoxExportTargetMaxSpeedFile
            // 
            this.textBoxExportTargetMaxSpeedFile.Location = new System.Drawing.Point(134, 86);
            this.textBoxExportTargetMaxSpeedFile.Name = "textBoxExportTargetMaxSpeedFile";
            this.textBoxExportTargetMaxSpeedFile.Size = new System.Drawing.Size(105, 20);
            this.textBoxExportTargetMaxSpeedFile.TabIndex = 13;
            this.textBoxExportTargetMaxSpeedFile.Text = "Tracking-Max.txt";
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
            // 
            // textBoxExportTargetFile
            // 
            this.textBoxExportTargetFile.Location = new System.Drawing.Point(134, 40);
            this.textBoxExportTargetFile.Name = "textBoxExportTargetFile";
            this.textBoxExportTargetFile.Size = new System.Drawing.Size(105, 20);
            this.textBoxExportTargetFile.TabIndex = 11;
            this.textBoxExportTargetFile.Text = "Tracking-Name.txt";
            // 
            // checkBoxClosestPlayerTarget
            // 
            this.checkBoxClosestPlayerTarget.AutoSize = true;
            this.checkBoxClosestPlayerTarget.Checked = true;
            this.checkBoxClosestPlayerTarget.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxClosestPlayerTarget.Location = new System.Drawing.Point(6, 41);
            this.checkBoxClosestPlayerTarget.Name = "checkBoxClosestPlayerTarget";
            this.checkBoxClosestPlayerTarget.Size = new System.Drawing.Size(231, 17);
            this.checkBoxClosestPlayerTarget.TabIndex = 18;
            this.checkBoxClosestPlayerTarget.Text = "Always export closest online commander to:";
            this.checkBoxClosestPlayerTarget.UseVisualStyleBackColor = true;
            // 
            // listViewExportSettings
            // 
            this.listViewExportSettings.CheckBoxes = true;
            this.listViewExportSettings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnExportDescription,
            this.columnExportFile});
            this.listViewExportSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewExportSettings.HideSelection = false;
            listViewItem10.StateImageIndex = 0;
            listViewItem11.StateImageIndex = 0;
            listViewItem12.StateImageIndex = 0;
            listViewItem13.StateImageIndex = 0;
            listViewItem14.StateImageIndex = 0;
            listViewItem15.StateImageIndex = 0;
            listViewItem16.StateImageIndex = 0;
            listViewItem17.StateImageIndex = 0;
            listViewItem18.StateImageIndex = 0;
            this.listViewExportSettings.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16,
            listViewItem17,
            listViewItem18});
            this.listViewExportSettings.Location = new System.Drawing.Point(3, 16);
            this.listViewExportSettings.Name = "listViewExportSettings";
            this.listViewExportSettings.Size = new System.Drawing.Size(411, 191);
            this.listViewExportSettings.TabIndex = 11;
            this.listViewExportSettings.UseCompatibleStateImageBehavior = false;
            this.listViewExportSettings.View = System.Windows.Forms.View.Details;
            // 
            // columnExportDescription
            // 
            this.columnExportDescription.Text = "Telemetry";
            this.columnExportDescription.Width = 200;
            // 
            // columnExportFile
            // 
            this.columnExportFile.Text = "Export to file";
            this.columnExportFile.Width = 200;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxClosestTarget);
            this.groupBox1.Controls.Add(this.checkBoxClosestPlayerTarget);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 66);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export Options";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listViewExportSettings);
            this.groupBox2.Location = new System.Drawing.Point(12, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(417, 210);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Export Settings";
            // 
            // comboBoxClosestTarget
            // 
            this.comboBoxClosestTarget.FormattingEnabled = true;
            this.comboBoxClosestTarget.Location = new System.Drawing.Point(243, 39);
            this.comboBoxClosestTarget.Name = "comboBoxClosestTarget";
            this.comboBoxClosestTarget.Size = new System.Drawing.Size(168, 21);
            this.comboBoxClosestTarget.TabIndex = 20;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(174, 300);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 14;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // FormTrackedTargetExports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxTrackTarget);
            this.Name = "FormTrackedTargetExports";
            this.Text = "Tracked Target Export Settings";
            this.groupBoxTrackTarget.ResumeLayout(false);
            this.groupBoxTrackTarget.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxTrackTarget;
        private System.Windows.Forms.TextBox textBoxExportTargetLapNumber;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTargetLapNumber;
        private System.Windows.Forms.TextBox textBoxExportTargetAverageSpeedFile;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTargetAverageSpeed;
        private System.Windows.Forms.TextBox textBoxExportTargetDistance;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTargetDistance;
        private System.Windows.Forms.TextBox textBoxExportTargetHull;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTargetHull;
        private System.Windows.Forms.TextBox textBoxExportTargetPosition;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTargetPosition;
        private System.Windows.Forms.TextBox textBoxExportTargetSpeedFile;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTargetSpeed;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTargetPitstops;
        private System.Windows.Forms.TextBox textBoxExportTargetPitstopsFile;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTargetMaxSpeed;
        private System.Windows.Forms.TextBox textBoxExportTargetMaxSpeedFile;
        private System.Windows.Forms.CheckBox checkBoxExportTrackedTarget;
        private System.Windows.Forms.TextBox textBoxExportTargetFile;
        private System.Windows.Forms.CheckBox checkBoxClosestPlayerTarget;
        private System.Windows.Forms.ListView listViewExportSettings;
        private System.Windows.Forms.ColumnHeader columnExportDescription;
        private System.Windows.Forms.ColumnHeader columnExportFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxClosestTarget;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonClose;
    }
}