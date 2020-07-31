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
            this.checkBoxAutoScroll = new System.Windows.Forms.CheckBox();
            this.checkBoxSendLocationOnly = new System.Windows.Forms.CheckBox();
            this.checkBoxShowLive = new System.Windows.Forms.CheckBox();
            this.textBoxClientId = new System.Windows.Forms.TextBox();
            this.textBoxSaveFile = new System.Windows.Forms.TextBox();
            this.checkBoxSaveToFile = new System.Windows.Forms.CheckBox();
            this.textBoxUploadServer = new System.Windows.Forms.TextBox();
            this.checkBoxUpload = new System.Windows.Forms.CheckBox();
            this.buttonLocator = new System.Windows.Forms.Button();
            this.buttonRoutePlanner = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonShowConfig = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusFileWatcher)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBrowseStatusFile);
            this.groupBox1.Controls.Add(this.textBoxStatusFile);
            this.groupBox1.Location = new System.Drawing.Point(281, 12);
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
            this.buttonTrack.Location = new System.Drawing.Point(200, 144);
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
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 60);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SRV Status";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(174, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Heading:";
            // 
            // textBoxSRVHeading
            // 
            this.textBoxSRVHeading.Location = new System.Drawing.Point(177, 32);
            this.textBoxSRVHeading.MaxLength = 20;
            this.textBoxSRVHeading.Name = "textBoxSRVHeading";
            this.textBoxSRVHeading.Size = new System.Drawing.Size(78, 20);
            this.textBoxSRVHeading.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Latitude:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Longitude:";
            // 
            // textBoxSRVLatitude
            // 
            this.textBoxSRVLatitude.Location = new System.Drawing.Point(93, 32);
            this.textBoxSRVLatitude.MaxLength = 20;
            this.textBoxSRVLatitude.Name = "textBoxSRVLatitude";
            this.textBoxSRVLatitude.Size = new System.Drawing.Size(78, 20);
            this.textBoxSRVLatitude.TabIndex = 1;
            // 
            // textBoxSRVLongitude
            // 
            this.textBoxSRVLongitude.Location = new System.Drawing.Point(9, 32);
            this.textBoxSRVLongitude.MaxLength = 20;
            this.textBoxSRVLongitude.Name = "textBoxSRVLongitude";
            this.textBoxSRVLongitude.Size = new System.Drawing.Size(78, 20);
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
            this.groupBox3.Location = new System.Drawing.Point(12, 78);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(263, 60);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ship Status";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(174, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Heading:";
            // 
            // textBoxShipHeading
            // 
            this.textBoxShipHeading.Location = new System.Drawing.Point(174, 32);
            this.textBoxShipHeading.MaxLength = 20;
            this.textBoxShipHeading.Name = "textBoxShipHeading";
            this.textBoxShipHeading.Size = new System.Drawing.Size(78, 20);
            this.textBoxShipHeading.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(87, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Latitude:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Longitude:";
            // 
            // textBoxShipLatitude
            // 
            this.textBoxShipLatitude.Location = new System.Drawing.Point(90, 32);
            this.textBoxShipLatitude.MaxLength = 20;
            this.textBoxShipLatitude.Name = "textBoxShipLatitude";
            this.textBoxShipLatitude.Size = new System.Drawing.Size(78, 20);
            this.textBoxShipLatitude.TabIndex = 5;
            // 
            // textBoxShipLongitude
            // 
            this.textBoxShipLongitude.Location = new System.Drawing.Point(6, 32);
            this.textBoxShipLongitude.MaxLength = 20;
            this.textBoxShipLongitude.Name = "textBoxShipLongitude";
            this.textBoxShipLongitude.Size = new System.Drawing.Size(78, 20);
            this.textBoxShipLongitude.TabIndex = 4;
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(12, 221);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(700, 147);
            this.listBoxLog.TabIndex = 4;
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(140, 173);
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
            this.label7.Location = new System.Drawing.Point(154, 199);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Last update:";
            // 
            // labelLastUpdateTime
            // 
            this.labelLastUpdateTime.AutoSize = true;
            this.labelLastUpdateTime.Location = new System.Drawing.Point(226, 199);
            this.labelLastUpdateTime.Name = "labelLastUpdateTime";
            this.labelLastUpdateTime.Size = new System.Drawing.Size(49, 13);
            this.labelLastUpdateTime.TabIndex = 7;
            this.labelLastUpdateTime.Text = "00:00:00";
            // 
            // checkBoxAutoScroll
            // 
            this.checkBoxAutoScroll.AutoSize = true;
            this.checkBoxAutoScroll.Location = new System.Drawing.Point(542, 202);
            this.checkBoxAutoScroll.Name = "checkBoxAutoScroll";
            this.checkBoxAutoScroll.Size = new System.Drawing.Size(75, 17);
            this.checkBoxAutoScroll.TabIndex = 8;
            this.checkBoxAutoScroll.Text = "Auto-scroll";
            this.checkBoxAutoScroll.UseVisualStyleBackColor = true;
            // 
            // checkBoxSendLocationOnly
            // 
            this.checkBoxSendLocationOnly.AutoSize = true;
            this.checkBoxSendLocationOnly.Checked = true;
            this.checkBoxSendLocationOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSendLocationOnly.Location = new System.Drawing.Point(6, 19);
            this.checkBoxSendLocationOnly.Name = "checkBoxSendLocationOnly";
            this.checkBoxSendLocationOnly.Size = new System.Drawing.Size(160, 17);
            this.checkBoxSendLocationOnly.TabIndex = 7;
            this.checkBoxSendLocationOnly.Text = "Log/send location data Only";
            this.checkBoxSendLocationOnly.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowLive
            // 
            this.checkBoxShowLive.AutoSize = true;
            this.checkBoxShowLive.Location = new System.Drawing.Point(623, 202);
            this.checkBoxShowLive.Name = "checkBoxShowLive";
            this.checkBoxShowLive.Size = new System.Drawing.Size(89, 17);
            this.checkBoxShowLive.TabIndex = 6;
            this.checkBoxShowLive.Text = "Show live log";
            this.checkBoxShowLive.UseVisualStyleBackColor = true;
            // 
            // textBoxClientId
            // 
            this.textBoxClientId.Location = new System.Drawing.Point(6, 19);
            this.textBoxClientId.MaxLength = 25;
            this.textBoxClientId.Name = "textBoxClientId";
            this.textBoxClientId.Size = new System.Drawing.Size(171, 20);
            this.textBoxClientId.TabIndex = 4;
            this.textBoxClientId.TextChanged += new System.EventHandler(this.textBoxClientId_TextChanged);
            // 
            // textBoxSaveFile
            // 
            this.textBoxSaveFile.Location = new System.Drawing.Point(94, 19);
            this.textBoxSaveFile.Name = "textBoxSaveFile";
            this.textBoxSaveFile.Size = new System.Drawing.Size(142, 20);
            this.textBoxSaveFile.TabIndex = 3;
            this.textBoxSaveFile.Text = "tracking.log";
            // 
            // checkBoxSaveToFile
            // 
            this.checkBoxSaveToFile.AutoSize = true;
            this.checkBoxSaveToFile.Location = new System.Drawing.Point(6, 21);
            this.checkBoxSaveToFile.Name = "checkBoxSaveToFile";
            this.checkBoxSaveToFile.Size = new System.Drawing.Size(82, 17);
            this.checkBoxSaveToFile.TabIndex = 2;
            this.checkBoxSaveToFile.Text = "Save to file:";
            this.checkBoxSaveToFile.UseVisualStyleBackColor = true;
            // 
            // textBoxUploadServer
            // 
            this.textBoxUploadServer.Location = new System.Drawing.Point(6, 42);
            this.textBoxUploadServer.MaxLength = 100;
            this.textBoxUploadServer.Name = "textBoxUploadServer";
            this.textBoxUploadServer.Size = new System.Drawing.Size(186, 20);
            this.textBoxUploadServer.TabIndex = 1;
            this.textBoxUploadServer.Text = "srvtracker.darkbytes.co.uk";
            // 
            // checkBoxUpload
            // 
            this.checkBoxUpload.AutoSize = true;
            this.checkBoxUpload.Location = new System.Drawing.Point(6, 19);
            this.checkBoxUpload.Name = "checkBoxUpload";
            this.checkBoxUpload.Size = new System.Drawing.Size(98, 17);
            this.checkBoxUpload.TabIndex = 0;
            this.checkBoxUpload.Text = "Send to server:";
            this.checkBoxUpload.UseVisualStyleBackColor = true;
            // 
            // buttonLocator
            // 
            this.buttonLocator.Location = new System.Drawing.Point(119, 144);
            this.buttonLocator.Name = "buttonLocator";
            this.buttonLocator.Size = new System.Drawing.Size(75, 23);
            this.buttonLocator.TabIndex = 9;
            this.buttonLocator.Text = "Locator...";
            this.buttonLocator.UseVisualStyleBackColor = true;
            this.buttonLocator.Click += new System.EventHandler(this.buttonLocator_Click);
            // 
            // buttonRoutePlanner
            // 
            this.buttonRoutePlanner.Enabled = false;
            this.buttonRoutePlanner.Location = new System.Drawing.Point(12, 144);
            this.buttonRoutePlanner.Name = "buttonRoutePlanner";
            this.buttonRoutePlanner.Size = new System.Drawing.Size(101, 23);
            this.buttonRoutePlanner.TabIndex = 10;
            this.buttonRoutePlanner.Text = "Route Planner";
            this.buttonRoutePlanner.UseVisualStyleBackColor = true;
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(200, 173);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 11;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonShowConfig
            // 
            this.buttonShowConfig.Location = new System.Drawing.Point(12, 173);
            this.buttonShowConfig.Name = "buttonShowConfig";
            this.buttonShowConfig.Size = new System.Drawing.Size(75, 23);
            this.buttonShowConfig.TabIndex = 12;
            this.buttonShowConfig.Text = "Show Config";
            this.buttonShowConfig.UseVisualStyleBackColor = true;
            this.buttonShowConfig.Click += new System.EventHandler(this.buttonShowConfig_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBoxClientId);
            this.groupBox5.Location = new System.Drawing.Point(281, 66);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(183, 50);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Client Id";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.checkBoxSaveToFile);
            this.groupBox6.Controls.Add(this.textBoxSaveFile);
            this.groupBox6.Location = new System.Drawing.Point(470, 66);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(242, 50);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Local Log";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.checkBoxUpload);
            this.groupBox7.Controls.Add(this.textBoxUploadServer);
            this.groupBox7.Location = new System.Drawing.Point(281, 122);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(198, 74);
            this.groupBox7.TabIndex = 15;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Server Upload";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.checkBoxSendLocationOnly);
            this.groupBox8.Location = new System.Drawing.Point(485, 122);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(227, 74);
            this.groupBox8.TabIndex = 16;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Data Settings";
            // 
            // FormTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 219);
            this.Controls.Add(this.checkBoxAutoScroll);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.checkBoxShowLive);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.buttonShowConfig);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonRoutePlanner);
            this.Controls.Add(this.buttonLocator);
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
            this.Name = "FormTracker";
            this.Text = "Vehicle Tracker";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusFileWatcher)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
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
        private System.Windows.Forms.TextBox textBoxSaveFile;
        private System.Windows.Forms.CheckBox checkBoxSaveToFile;
        private System.Windows.Forms.TextBox textBoxUploadServer;
        private System.Windows.Forms.CheckBox checkBoxUpload;
        private System.Windows.Forms.TextBox textBoxClientId;
        private System.Windows.Forms.CheckBox checkBoxShowLive;
        private System.Windows.Forms.CheckBox checkBoxSendLocationOnly;
        private System.Windows.Forms.CheckBox checkBoxAutoScroll;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonShowConfig;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonRoutePlanner;
        private System.Windows.Forms.Button buttonLocator;
    }
}

