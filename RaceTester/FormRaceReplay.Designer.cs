namespace RaceTester
{
    partial class FormRaceReplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRaceReplay));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonOpenRaceFolder = new System.Windows.Forms.Button();
            this.textBoxRaceDataFolder = new System.Windows.Forms.TextBox();
            this.listBoxParticipants = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownPlaybackSpeed = new System.Windows.Forms.NumericUpDown();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxReplayedEvents = new System.Windows.Forms.TextBox();
            this.textBoxTotalNumberOfEvents = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRaceName = new System.Windows.Forms.TextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.timerPlaybackEvents = new System.Windows.Forms.Timer(this.components);
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.radioButtonUseCustomServer = new System.Windows.Forms.RadioButton();
            this.radioButtonUseDefaultServer = new System.Windows.Forms.RadioButton();
            this.textBoxUploadServer = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPlaybackSpeed)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonOpenRaceFolder);
            this.groupBox1.Controls.Add(this.textBoxRaceDataFolder);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(469, 49);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Race Data Location";
            // 
            // buttonOpenRaceFolder
            // 
            this.buttonOpenRaceFolder.Image = global::RaceTester.Properties.Resources.OpenFolder_16x;
            this.buttonOpenRaceFolder.Location = new System.Drawing.Point(432, 15);
            this.buttonOpenRaceFolder.Name = "buttonOpenRaceFolder";
            this.buttonOpenRaceFolder.Size = new System.Drawing.Size(31, 26);
            this.buttonOpenRaceFolder.TabIndex = 1;
            this.buttonOpenRaceFolder.UseVisualStyleBackColor = true;
            this.buttonOpenRaceFolder.Click += new System.EventHandler(this.buttonOpenRaceFolder_Click);
            // 
            // textBoxRaceDataFolder
            // 
            this.textBoxRaceDataFolder.Enabled = false;
            this.textBoxRaceDataFolder.Location = new System.Drawing.Point(6, 19);
            this.textBoxRaceDataFolder.Name = "textBoxRaceDataFolder";
            this.textBoxRaceDataFolder.Size = new System.Drawing.Size(420, 20);
            this.textBoxRaceDataFolder.TabIndex = 0;
            // 
            // listBoxParticipants
            // 
            this.listBoxParticipants.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxParticipants.FormattingEnabled = true;
            this.listBoxParticipants.Location = new System.Drawing.Point(3, 16);
            this.listBoxParticipants.Name = "listBoxParticipants";
            this.listBoxParticipants.Size = new System.Drawing.Size(215, 260);
            this.listBoxParticipants.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxParticipants);
            this.groupBox2.Location = new System.Drawing.Point(12, 67);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(221, 279);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Participants";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.numericUpDownPlaybackSpeed);
            this.groupBox3.Controls.Add(this.buttonStop);
            this.groupBox3.Controls.Add(this.buttonPause);
            this.groupBox3.Controls.Add(this.buttonPlay);
            this.groupBox3.Location = new System.Drawing.Point(245, 263);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(133, 80);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Playback";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Speed:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(114, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "x";
            // 
            // numericUpDownPlaybackSpeed
            // 
            this.numericUpDownPlaybackSpeed.Location = new System.Drawing.Point(53, 48);
            this.numericUpDownPlaybackSpeed.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDownPlaybackSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPlaybackSpeed.Name = "numericUpDownPlaybackSpeed";
            this.numericUpDownPlaybackSpeed.Size = new System.Drawing.Size(58, 20);
            this.numericUpDownPlaybackSpeed.TabIndex = 18;
            this.numericUpDownPlaybackSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownPlaybackSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Image = global::RaceTester.Properties.Resources.Stop_16x;
            this.buttonStop.Location = new System.Drawing.Point(90, 19);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(36, 23);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Enabled = false;
            this.buttonPause.Image = global::RaceTester.Properties.Resources.Pause_16x;
            this.buttonPause.Location = new System.Drawing.Point(48, 19);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(36, 23);
            this.buttonPause.TabIndex = 1;
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Image = global::RaceTester.Properties.Resources.Run_16x;
            this.buttonPlay.Location = new System.Drawing.Point(6, 19);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(36, 23);
            this.buttonPlay.TabIndex = 0;
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.textBoxReplayedEvents);
            this.groupBox4.Controls.Add(this.textBoxTotalNumberOfEvents);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.textBoxRaceName);
            this.groupBox4.Location = new System.Drawing.Point(239, 144);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(242, 113);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Race Info";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Number of replayed events:";
            // 
            // textBoxReplayedEvents
            // 
            this.textBoxReplayedEvents.Enabled = false;
            this.textBoxReplayedEvents.Location = new System.Drawing.Point(172, 82);
            this.textBoxReplayedEvents.Name = "textBoxReplayedEvents";
            this.textBoxReplayedEvents.Size = new System.Drawing.Size(64, 20);
            this.textBoxReplayedEvents.TabIndex = 4;
            this.textBoxReplayedEvents.Text = "0";
            this.textBoxReplayedEvents.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxTotalNumberOfEvents
            // 
            this.textBoxTotalNumberOfEvents.Enabled = false;
            this.textBoxTotalNumberOfEvents.Location = new System.Drawing.Point(172, 56);
            this.textBoxTotalNumberOfEvents.Name = "textBoxTotalNumberOfEvents";
            this.textBoxTotalNumberOfEvents.Size = new System.Drawing.Size(64, 20);
            this.textBoxTotalNumberOfEvents.TabIndex = 3;
            this.textBoxTotalNumberOfEvents.Text = "0";
            this.textBoxTotalNumberOfEvents.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Total number of tracking events:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // textBoxRaceName
            // 
            this.textBoxRaceName.Location = new System.Drawing.Point(50, 19);
            this.textBoxRaceName.Name = "textBoxRaceName";
            this.textBoxRaceName.Size = new System.Drawing.Size(186, 20);
            this.textBoxRaceName.TabIndex = 0;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(406, 320);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // timerPlaybackEvents
            // 
            this.timerPlaybackEvents.Tick += new System.EventHandler(this.timerPlaybackEvents_Tick);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.radioButtonUseCustomServer);
            this.groupBox8.Controls.Add(this.radioButtonUseDefaultServer);
            this.groupBox8.Controls.Add(this.textBoxUploadServer);
            this.groupBox8.ForeColor = System.Drawing.Color.Black;
            this.groupBox8.Location = new System.Drawing.Point(239, 67);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(243, 71);
            this.groupBox8.TabIndex = 17;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Replay to Server";
            // 
            // radioButtonUseCustomServer
            // 
            this.radioButtonUseCustomServer.AutoSize = true;
            this.radioButtonUseCustomServer.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radioButtonUseCustomServer.Location = new System.Drawing.Point(6, 41);
            this.radioButtonUseCustomServer.Name = "radioButtonUseCustomServer";
            this.radioButtonUseCustomServer.Size = new System.Drawing.Size(63, 17);
            this.radioButtonUseCustomServer.TabIndex = 3;
            this.radioButtonUseCustomServer.Text = "Custom:";
            this.radioButtonUseCustomServer.UseVisualStyleBackColor = true;
            this.radioButtonUseCustomServer.CheckedChanged += new System.EventHandler(this.radioButtonUseCustomServer_CheckedChanged);
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
            this.radioButtonUseDefaultServer.UseVisualStyleBackColor = true;
            this.radioButtonUseDefaultServer.CheckedChanged += new System.EventHandler(this.radioButtonUseDefaultServer_CheckedChanged);
            // 
            // textBoxUploadServer
            // 
            this.textBoxUploadServer.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxUploadServer.Enabled = false;
            this.textBoxUploadServer.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxUploadServer.Location = new System.Drawing.Point(75, 40);
            this.textBoxUploadServer.MaxLength = 100;
            this.textBoxUploadServer.Name = "textBoxUploadServer";
            this.textBoxUploadServer.Size = new System.Drawing.Size(161, 20);
            this.textBoxUploadServer.TabIndex = 1;
            // 
            // FormRaceReplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 356);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormRaceReplay";
            this.Text = "Replay Race";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPlaybackSpeed)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonOpenRaceFolder;
        private System.Windows.Forms.TextBox textBoxRaceDataFolder;
        private System.Windows.Forms.ListBox listBoxParticipants;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRaceName;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxReplayedEvents;
        private System.Windows.Forms.TextBox textBoxTotalNumberOfEvents;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timerPlaybackEvents;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton radioButtonUseCustomServer;
        private System.Windows.Forms.RadioButton radioButtonUseDefaultServer;
        private System.Windows.Forms.TextBox textBoxUploadServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownPlaybackSpeed;
    }
}

