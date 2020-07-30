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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBrowseStatusFile = new System.Windows.Forms.Button();
            this.textBoxStatusFile = new System.Windows.Forms.TextBox();
            this.statusFileWatcher = new System.IO.FileSystemWatcher();
            this.buttonTrack = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSRVHeading = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSRVLatitude = new System.Windows.Forms.TextBox();
            this.textBoxSRVLongitude = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxShipHeading = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxShipLatitude = new System.Windows.Forms.TextBox();
            this.textBoxShipLongitude = new System.Windows.Forms.TextBox();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.buttonTest = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.labelLastUpdateTime = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBoxSendLocationOnly = new System.Windows.Forms.CheckBox();
            this.checkBoxShowLive = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxClientId = new System.Windows.Forms.TextBox();
            this.textBoxSaveFile = new System.Windows.Forms.TextBox();
            this.checkBoxSaveToFile = new System.Windows.Forms.CheckBox();
            this.textBoxUploadServer = new System.Windows.Forms.TextBox();
            this.checkBoxUpload = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoScroll = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusFileWatcher)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBrowseStatusFile);
            this.groupBox1.Controls.Add(this.textBoxStatusFile);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 48);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status.json location";
            // 
            // buttonBrowseStatusFile
            // 
            this.buttonBrowseStatusFile.Location = new System.Drawing.Point(371, 17);
            this.buttonBrowseStatusFile.Name = "buttonBrowseStatusFile";
            this.buttonBrowseStatusFile.Size = new System.Drawing.Size(54, 23);
            this.buttonBrowseStatusFile.TabIndex = 1;
            this.buttonBrowseStatusFile.Text = "Browse";
            this.buttonBrowseStatusFile.UseVisualStyleBackColor = true;
            this.buttonBrowseStatusFile.Click += new System.EventHandler(this.buttonBrowseStatusFile_Click);
            // 
            // textBoxStatusFile
            // 
            this.textBoxStatusFile.Location = new System.Drawing.Point(6, 19);
            this.textBoxStatusFile.Name = "textBoxStatusFile";
            this.textBoxStatusFile.Size = new System.Drawing.Size(359, 20);
            this.textBoxStatusFile.TabIndex = 0;
            // 
            // statusFileWatcher
            // 
            this.statusFileWatcher.EnableRaisingEvents = true;
            this.statusFileWatcher.SynchronizingObject = this;
            this.statusFileWatcher.Changed += new System.IO.FileSystemEventHandler(this.statusFileWatcher_Changed);
            // 
            // buttonTrack
            // 
            this.buttonTrack.Location = new System.Drawing.Point(510, 12);
            this.buttonTrack.Name = "buttonTrack";
            this.buttonTrack.Size = new System.Drawing.Size(75, 23);
            this.buttonTrack.TabIndex = 1;
            this.buttonTrack.Text = "Track";
            this.buttonTrack.UseVisualStyleBackColor = true;
            this.buttonTrack.Click += new System.EventHandler(this.buttonTrack_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxSRVHeading);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxSRVLatitude);
            this.groupBox2.Controls.Add(this.textBoxSRVLongitude);
            this.groupBox2.Location = new System.Drawing.Point(12, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(574, 52);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SRV Status";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(338, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Heading:";
            // 
            // textBoxSRVHeading
            // 
            this.textBoxSRVHeading.Location = new System.Drawing.Point(394, 19);
            this.textBoxSRVHeading.MaxLength = 20;
            this.textBoxSRVHeading.Name = "textBoxSRVHeading";
            this.textBoxSRVHeading.Size = new System.Drawing.Size(100, 20);
            this.textBoxSRVHeading.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Latitude:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Longitude:";
            // 
            // textBoxSRVLatitude
            // 
            this.textBoxSRVLatitude.Location = new System.Drawing.Point(232, 19);
            this.textBoxSRVLatitude.MaxLength = 20;
            this.textBoxSRVLatitude.Name = "textBoxSRVLatitude";
            this.textBoxSRVLatitude.Size = new System.Drawing.Size(100, 20);
            this.textBoxSRVLatitude.TabIndex = 1;
            // 
            // textBoxSRVLongitude
            // 
            this.textBoxSRVLongitude.Location = new System.Drawing.Point(69, 19);
            this.textBoxSRVLongitude.MaxLength = 20;
            this.textBoxSRVLongitude.Name = "textBoxSRVLongitude";
            this.textBoxSRVLongitude.Size = new System.Drawing.Size(100, 20);
            this.textBoxSRVLongitude.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textBoxShipHeading);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.textBoxShipLatitude);
            this.groupBox3.Controls.Add(this.textBoxShipLongitude);
            this.groupBox3.Location = new System.Drawing.Point(12, 201);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(574, 51);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ship Status";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(338, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Heading:";
            // 
            // textBoxShipHeading
            // 
            this.textBoxShipHeading.Location = new System.Drawing.Point(393, 19);
            this.textBoxShipHeading.MaxLength = 20;
            this.textBoxShipHeading.Name = "textBoxShipHeading";
            this.textBoxShipHeading.Size = new System.Drawing.Size(100, 20);
            this.textBoxShipHeading.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(175, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Latitude:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Longitude:";
            // 
            // textBoxShipLatitude
            // 
            this.textBoxShipLatitude.Location = new System.Drawing.Point(232, 19);
            this.textBoxShipLatitude.MaxLength = 20;
            this.textBoxShipLatitude.Name = "textBoxShipLatitude";
            this.textBoxShipLatitude.Size = new System.Drawing.Size(100, 20);
            this.textBoxShipLatitude.TabIndex = 5;
            // 
            // textBoxShipLongitude
            // 
            this.textBoxShipLongitude.Location = new System.Drawing.Point(69, 19);
            this.textBoxShipLongitude.MaxLength = 20;
            this.textBoxShipLongitude.Name = "textBoxShipLongitude";
            this.textBoxShipLongitude.Size = new System.Drawing.Size(100, 20);
            this.textBoxShipLongitude.TabIndex = 4;
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(12, 263);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(573, 147);
            this.listBoxLog.TabIndex = 4;
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(450, 12);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(54, 23);
            this.buttonTest.TabIndex = 5;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(464, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Last update:";
            // 
            // labelLastUpdateTime
            // 
            this.labelLastUpdateTime.AutoSize = true;
            this.labelLastUpdateTime.Location = new System.Drawing.Point(536, 48);
            this.labelLastUpdateTime.Name = "labelLastUpdateTime";
            this.labelLastUpdateTime.Size = new System.Drawing.Size(49, 13);
            this.labelLastUpdateTime.TabIndex = 7;
            this.labelLastUpdateTime.Text = "00:00:00";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBoxAutoScroll);
            this.groupBox4.Controls.Add(this.checkBoxSendLocationOnly);
            this.groupBox4.Controls.Add(this.checkBoxShowLive);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.textBoxClientId);
            this.groupBox4.Controls.Add(this.textBoxSaveFile);
            this.groupBox4.Controls.Add(this.checkBoxSaveToFile);
            this.groupBox4.Controls.Add(this.textBoxUploadServer);
            this.groupBox4.Controls.Add(this.checkBoxUpload);
            this.groupBox4.Location = new System.Drawing.Point(12, 64);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(574, 73);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Data Collection";
            // 
            // checkBoxSendLocationOnly
            // 
            this.checkBoxSendLocationOnly.AutoSize = true;
            this.checkBoxSendLocationOnly.Checked = true;
            this.checkBoxSendLocationOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSendLocationOnly.Location = new System.Drawing.Point(257, 44);
            this.checkBoxSendLocationOnly.Name = "checkBoxSendLocationOnly";
            this.checkBoxSendLocationOnly.Size = new System.Drawing.Size(117, 17);
            this.checkBoxSendLocationOnly.TabIndex = 7;
            this.checkBoxSendLocationOnly.Text = "Location Data Only";
            this.checkBoxSendLocationOnly.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowLive
            // 
            this.checkBoxShowLive.AutoSize = true;
            this.checkBoxShowLive.Location = new System.Drawing.Point(398, 44);
            this.checkBoxShowLive.Name = "checkBoxShowLive";
            this.checkBoxShowLive.Size = new System.Drawing.Size(89, 17);
            this.checkBoxShowLive.TabIndex = 6;
            this.checkBoxShowLive.Text = "Show live log";
            this.checkBoxShowLive.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Client Id:";
            // 
            // textBoxClientId
            // 
            this.textBoxClientId.Location = new System.Drawing.Point(80, 42);
            this.textBoxClientId.MaxLength = 25;
            this.textBoxClientId.Name = "textBoxClientId";
            this.textBoxClientId.Size = new System.Drawing.Size(171, 20);
            this.textBoxClientId.TabIndex = 4;
            this.textBoxClientId.TextChanged += new System.EventHandler(this.textBoxClientId_TextChanged);
            // 
            // textBoxSaveFile
            // 
            this.textBoxSaveFile.Location = new System.Drawing.Point(375, 16);
            this.textBoxSaveFile.Name = "textBoxSaveFile";
            this.textBoxSaveFile.Size = new System.Drawing.Size(193, 20);
            this.textBoxSaveFile.TabIndex = 3;
            this.textBoxSaveFile.Text = "tracking.log";
            // 
            // checkBoxSaveToFile
            // 
            this.checkBoxSaveToFile.AutoSize = true;
            this.checkBoxSaveToFile.Location = new System.Drawing.Point(287, 18);
            this.checkBoxSaveToFile.Name = "checkBoxSaveToFile";
            this.checkBoxSaveToFile.Size = new System.Drawing.Size(82, 17);
            this.checkBoxSaveToFile.TabIndex = 2;
            this.checkBoxSaveToFile.Text = "Save to file:";
            this.checkBoxSaveToFile.UseVisualStyleBackColor = true;
            // 
            // textBoxUploadServer
            // 
            this.textBoxUploadServer.Location = new System.Drawing.Point(110, 16);
            this.textBoxUploadServer.MaxLength = 100;
            this.textBoxUploadServer.Name = "textBoxUploadServer";
            this.textBoxUploadServer.Size = new System.Drawing.Size(171, 20);
            this.textBoxUploadServer.TabIndex = 1;
            this.textBoxUploadServer.Text = "srvtracker.darkbytes.co.uk";
            // 
            // checkBoxUpload
            // 
            this.checkBoxUpload.AutoSize = true;
            this.checkBoxUpload.Checked = true;
            this.checkBoxUpload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUpload.Location = new System.Drawing.Point(6, 19);
            this.checkBoxUpload.Name = "checkBoxUpload";
            this.checkBoxUpload.Size = new System.Drawing.Size(98, 17);
            this.checkBoxUpload.TabIndex = 0;
            this.checkBoxUpload.Text = "Send to server:";
            this.checkBoxUpload.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoScroll
            // 
            this.checkBoxAutoScroll.AutoSize = true;
            this.checkBoxAutoScroll.Location = new System.Drawing.Point(493, 44);
            this.checkBoxAutoScroll.Name = "checkBoxAutoScroll";
            this.checkBoxAutoScroll.Size = new System.Drawing.Size(75, 17);
            this.checkBoxAutoScroll.TabIndex = 8;
            this.checkBoxAutoScroll.Text = "Auto-scroll";
            this.checkBoxAutoScroll.UseVisualStyleBackColor = true;
            // 
            // FormTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 425);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.labelLastUpdateTime);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonTrack);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTracker";
            this.Text = "Vehicle Tracker";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusFileWatcher)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonBrowseStatusFile;
        private System.Windows.Forms.TextBox textBoxStatusFile;
        private System.IO.FileSystemWatcher statusFileWatcher;
        private System.Windows.Forms.Button buttonTrack;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxShipLatitude;
        private System.Windows.Forms.TextBox textBoxShipLongitude;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSRVLatitude;
        private System.Windows.Forms.TextBox textBoxSRVLongitude;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxShipHeading;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSRVHeading;
        private System.Windows.Forms.Label labelLastUpdateTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxSaveFile;
        private System.Windows.Forms.CheckBox checkBoxSaveToFile;
        private System.Windows.Forms.TextBox textBoxUploadServer;
        private System.Windows.Forms.CheckBox checkBoxUpload;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxClientId;
        private System.Windows.Forms.CheckBox checkBoxShowLive;
        private System.Windows.Forms.CheckBox checkBoxSendLocationOnly;
        private System.Windows.Forms.CheckBox checkBoxAutoScroll;
    }
}

