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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewExportSettings = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBrowseExportFolder = new System.Windows.Forms.Button();
            this.textBoxExportFolder = new System.Windows.Forms.TextBox();
            this.radioButtonExportToOtherFolder = new System.Windows.Forms.RadioButton();
            this.radioButtonExportToApplicationFolder = new System.Windows.Forms.RadioButton();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExportSettings)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dataGridViewExportSettings);
            this.groupBox2.Location = new System.Drawing.Point(12, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(590, 372);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Available reports";
            // 
            // dataGridViewExportSettings
            // 
            this.dataGridViewExportSettings.AllowUserToAddRows = false;
            this.dataGridViewExportSettings.AllowUserToDeleteRows = false;
            this.dataGridViewExportSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExportSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewExportSettings.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewExportSettings.MultiSelect = false;
            this.dataGridViewExportSettings.Name = "dataGridViewExportSettings";
            this.dataGridViewExportSettings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewExportSettings.ShowEditingIcon = false;
            this.dataGridViewExportSettings.Size = new System.Drawing.Size(584, 353);
            this.dataGridViewExportSettings.TabIndex = 12;
            this.dataGridViewExportSettings.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewExportSettings_CellContentClick);
            this.dataGridViewExportSettings.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewExportSettings_CellEndEdit);
            this.dataGridViewExportSettings.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewExportSettings_CellValueChanged);
            this.dataGridViewExportSettings.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewExportSettings_CurrentCellDirtyStateChanged);
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
            this.groupBox1.Text = "Export location (text files of telemetry exported here)";
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
            this.buttonClose.Location = new System.Drawing.Point(527, 456);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 16;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // FormTelemetrySettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 489);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExportSettings)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxExportFolder;
        private System.Windows.Forms.RadioButton radioButtonExportToOtherFolder;
        private System.Windows.Forms.RadioButton radioButtonExportToApplicationFolder;
        private System.Windows.Forms.Button buttonBrowseExportFolder;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridView dataGridViewExportSettings;
    }
}