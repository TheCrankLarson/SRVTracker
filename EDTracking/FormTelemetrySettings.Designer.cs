namespace EDTracking
{
    partial class FormTelemetrySettings
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Contestants (in leaderboard order)",
            "Positions",
            "Names-Position.txt"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Current statuses of contestants",
            "Status",
            "Statuses.txt"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Distances to next waypoint",
            "DistanceToWaypoint",
            "WPDistances.txt"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "Total distances left",
            "TotalDistanceLeft",
            "DistancesLeft.txt"}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "Current hull statuses",
            "HullStrengths",
            "Hulls.txt"}, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "Current speeds",
            "Speeds",
            "Speeds.txt"}, -1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "Maximum speeds",
            "MaxSpeeds",
            "MaxSpeeds.txt"}, -1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "Average Speeds",
            "AverageSpeeds",
            "AvgSpeeds.txt"}, -1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "Current laps",
            "CurrentLap",
            "CurrentLaps.txt"}, -1);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listViewTextExportSettings = new System.Windows.Forms.ListView();
            this.columnExportDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderReportName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnExportFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBrowseExportFolder = new System.Windows.Forms.Button();
            this.textBoxExportFolder = new System.Windows.Forms.TextBox();
            this.radioButtonExportToOtherFolder = new System.Windows.Forms.RadioButton();
            this.radioButtonExportToApplicationFolder = new System.Windows.Forms.RadioButton();
            this.buttonClose = new System.Windows.Forms.Button();
            this.checkBoxSelectAll = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.listViewTextExportSettings);
            this.groupBox2.Location = new System.Drawing.Point(12, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(565, 210);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Available reports";
            // 
            // listViewTextExportSettings
            // 
            this.listViewTextExportSettings.CheckBoxes = true;
            this.listViewTextExportSettings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnExportDescription,
            this.columnHeaderReportName,
            this.columnExportFile});
            this.listViewTextExportSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewTextExportSettings.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewTextExportSettings.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            listViewItem5.StateImageIndex = 0;
            listViewItem6.StateImageIndex = 0;
            listViewItem7.StateImageIndex = 0;
            listViewItem8.StateImageIndex = 0;
            listViewItem9.StateImageIndex = 0;
            this.listViewTextExportSettings.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9});
            this.listViewTextExportSettings.Location = new System.Drawing.Point(3, 16);
            this.listViewTextExportSettings.Name = "listViewTextExportSettings";
            this.listViewTextExportSettings.Size = new System.Drawing.Size(559, 191);
            this.listViewTextExportSettings.TabIndex = 11;
            this.listViewTextExportSettings.UseCompatibleStateImageBehavior = false;
            this.listViewTextExportSettings.View = System.Windows.Forms.View.Details;
            this.listViewTextExportSettings.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewTextExportSettings_ItemChecked);
            // 
            // columnExportDescription
            // 
            this.columnExportDescription.Text = "Telemetry";
            this.columnExportDescription.Width = 200;
            // 
            // columnHeaderReportName
            // 
            this.columnHeaderReportName.Text = "Report Name";
            this.columnHeaderReportName.Width = 100;
            // 
            // columnExportFile
            // 
            this.columnExportFile.Text = "Export to file";
            this.columnExportFile.Width = 180;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBrowseExportFolder);
            this.groupBox1.Controls.Add(this.textBoxExportFolder);
            this.groupBox1.Controls.Add(this.radioButtonExportToOtherFolder);
            this.groupBox1.Controls.Add(this.radioButtonExportToApplicationFolder);
            this.groupBox1.Location = new System.Drawing.Point(13, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(564, 68);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export location";
            // 
            // buttonBrowseExportFolder
            // 
            this.buttonBrowseExportFolder.Image = global::EDTracking.Properties.Resources.FolderOpened_16x;
            this.buttonBrowseExportFolder.Location = new System.Drawing.Point(531, 39);
            this.buttonBrowseExportFolder.Name = "buttonBrowseExportFolder";
            this.buttonBrowseExportFolder.Size = new System.Drawing.Size(27, 23);
            this.buttonBrowseExportFolder.TabIndex = 3;
            this.buttonBrowseExportFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseExportFolder.Click += new System.EventHandler(this.buttonBrowseExportFolder_Click);
            // 
            // textBoxExportFolder
            // 
            this.textBoxExportFolder.Location = new System.Drawing.Point(148, 41);
            this.textBoxExportFolder.Name = "textBoxExportFolder";
            this.textBoxExportFolder.Size = new System.Drawing.Size(377, 20);
            this.textBoxExportFolder.TabIndex = 2;
            this.textBoxExportFolder.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxExportFolder_Validating);
            // 
            // radioButtonExportToOtherFolder
            // 
            this.radioButtonExportToOtherFolder.AutoSize = true;
            this.radioButtonExportToOtherFolder.Location = new System.Drawing.Point(6, 42);
            this.radioButtonExportToOtherFolder.Name = "radioButtonExportToOtherFolder";
            this.radioButtonExportToOtherFolder.Size = new System.Drawing.Size(136, 17);
            this.radioButtonExportToOtherFolder.TabIndex = 1;
            this.radioButtonExportToOtherFolder.Text = "Export to custom folder:";
            this.radioButtonExportToOtherFolder.UseVisualStyleBackColor = true;
            this.radioButtonExportToOtherFolder.CheckedChanged += new System.EventHandler(this.radioButtonExportToOtherFolder_CheckedChanged);
            // 
            // radioButtonExportToApplicationFolder
            // 
            this.radioButtonExportToApplicationFolder.AutoSize = true;
            this.radioButtonExportToApplicationFolder.Checked = true;
            this.radioButtonExportToApplicationFolder.Location = new System.Drawing.Point(6, 19);
            this.radioButtonExportToApplicationFolder.Name = "radioButtonExportToApplicationFolder";
            this.radioButtonExportToApplicationFolder.Size = new System.Drawing.Size(150, 17);
            this.radioButtonExportToApplicationFolder.TabIndex = 0;
            this.radioButtonExportToApplicationFolder.TabStop = true;
            this.radioButtonExportToApplicationFolder.Text = "Export to application folder";
            this.radioButtonExportToApplicationFolder.UseVisualStyleBackColor = true;
            this.radioButtonExportToApplicationFolder.CheckedChanged += new System.EventHandler(this.radioButtonExportToApplicationFolder_CheckedChanged);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(502, 294);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 16;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // checkBoxSelectAll
            // 
            this.checkBoxSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxSelectAll.AutoSize = true;
            this.checkBoxSelectAll.Location = new System.Drawing.Point(21, 291);
            this.checkBoxSelectAll.Name = "checkBoxSelectAll";
            this.checkBoxSelectAll.Size = new System.Drawing.Size(69, 17);
            this.checkBoxSelectAll.TabIndex = 17;
            this.checkBoxSelectAll.Text = "Select all";
            this.checkBoxSelectAll.UseVisualStyleBackColor = true;
            this.checkBoxSelectAll.CheckedChanged += new System.EventHandler(this.checkBoxSelectAll_CheckedChanged);
            // 
            // FormTelemetrySettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 327);
            this.Controls.Add(this.checkBoxSelectAll);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(605, 240);
            this.Name = "FormTelemetrySettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Race Telemetry Export Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFileExportSettings_FormClosing);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView listViewTextExportSettings;
        private System.Windows.Forms.ColumnHeader columnExportDescription;
        private System.Windows.Forms.ColumnHeader columnExportFile;
        private System.Windows.Forms.ColumnHeader columnHeaderReportName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxExportFolder;
        private System.Windows.Forms.RadioButton radioButtonExportToOtherFolder;
        private System.Windows.Forms.RadioButton radioButtonExportToApplicationFolder;
        private System.Windows.Forms.Button buttonBrowseExportFolder;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.CheckBox checkBoxSelectAll;
    }
}