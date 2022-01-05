
namespace SRVTracker
{
    partial class FormRacerProfile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRacerProfile));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonYouTubeStream = new System.Windows.Forms.RadioButton();
            this.textBoxYouTubeChannel = new System.Windows.Forms.TextBox();
            this.radioButtonTwitchStream = new System.Windows.Forms.RadioButton();
            this.textBoxTwitchChannel = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSaveShipImage = new System.Windows.Forms.Button();
            this.buttonSaveSLFImage = new System.Windows.Forms.Button();
            this.buttonSaveSRVImage = new System.Windows.Forms.Button();
            this.buttonLoadShipImage = new System.Windows.Forms.Button();
            this.buttonLoadSLFImage = new System.Windows.Forms.Button();
            this.buttonLoadSRVImage = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBoxShip = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBoxSLF = new System.Windows.Forms.PictureBox();
            this.pictureBoxSRV = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonUpdateProfile = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxProfile = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSLF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSRV)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonYouTubeStream);
            this.groupBox1.Controls.Add(this.textBoxYouTubeChannel);
            this.groupBox1.Controls.Add(this.radioButtonTwitchStream);
            this.groupBox1.Controls.Add(this.textBoxTwitchChannel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Streaming";
            // 
            // radioButtonYouTubeStream
            // 
            this.radioButtonYouTubeStream.AutoSize = true;
            this.radioButtonYouTubeStream.Location = new System.Drawing.Point(6, 46);
            this.radioButtonYouTubeStream.Name = "radioButtonYouTubeStream";
            this.radioButtonYouTubeStream.Size = new System.Drawing.Size(113, 17);
            this.radioButtonYouTubeStream.TabIndex = 3;
            this.radioButtonYouTubeStream.Text = "YouTube channel:";
            this.radioButtonYouTubeStream.UseVisualStyleBackColor = true;
            // 
            // textBoxYouTubeChannel
            // 
            this.textBoxYouTubeChannel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxYouTubeChannel.Enabled = false;
            this.textBoxYouTubeChannel.Location = new System.Drawing.Point(125, 45);
            this.textBoxYouTubeChannel.Name = "textBoxYouTubeChannel";
            this.textBoxYouTubeChannel.Size = new System.Drawing.Size(184, 20);
            this.textBoxYouTubeChannel.TabIndex = 2;
            this.toolTip1.SetToolTip(this.textBoxYouTubeChannel, "YouTube channel name (will be used for stream embedding).\r\nEnter the channel name" +
        " only.");
            // 
            // radioButtonTwitchStream
            // 
            this.radioButtonTwitchStream.AutoSize = true;
            this.radioButtonTwitchStream.Checked = true;
            this.radioButtonTwitchStream.Location = new System.Drawing.Point(6, 19);
            this.radioButtonTwitchStream.Name = "radioButtonTwitchStream";
            this.radioButtonTwitchStream.Size = new System.Drawing.Size(101, 17);
            this.radioButtonTwitchStream.TabIndex = 1;
            this.radioButtonTwitchStream.TabStop = true;
            this.radioButtonTwitchStream.Text = "Twitch channel:";
            this.radioButtonTwitchStream.UseVisualStyleBackColor = true;
            // 
            // textBoxTwitchChannel
            // 
            this.textBoxTwitchChannel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTwitchChannel.Location = new System.Drawing.Point(113, 19);
            this.textBoxTwitchChannel.Name = "textBoxTwitchChannel";
            this.textBoxTwitchChannel.Size = new System.Drawing.Size(196, 20);
            this.textBoxTwitchChannel.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textBoxTwitchChannel, "Twitch channel name (will be used for stream embedding).\r\nEnter the channel name " +
        "only.");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonSaveShipImage);
            this.groupBox2.Controls.Add(this.buttonSaveSLFImage);
            this.groupBox2.Controls.Add(this.buttonSaveSRVImage);
            this.groupBox2.Controls.Add(this.buttonLoadShipImage);
            this.groupBox2.Controls.Add(this.buttonLoadSLFImage);
            this.groupBox2.Controls.Add(this.buttonLoadSRVImage);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.pictureBoxShip);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.pictureBoxSLF);
            this.groupBox2.Controls.Add(this.pictureBoxSRV);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 125);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Vehicle Images";
            // 
            // buttonSaveShipImage
            // 
            this.buttonSaveShipImage.Image = global::SRVTracker.Properties.Resources.Save_16x;
            this.buttonSaveShipImage.Location = new System.Drawing.Point(162, 97);
            this.buttonSaveShipImage.Name = "buttonSaveShipImage";
            this.buttonSaveShipImage.Size = new System.Drawing.Size(23, 23);
            this.buttonSaveShipImage.TabIndex = 11;
            this.buttonSaveShipImage.UseVisualStyleBackColor = true;
            this.buttonSaveShipImage.Click += new System.EventHandler(this.buttonSaveShipImage_Click);
            // 
            // buttonSaveSLFImage
            // 
            this.buttonSaveSLFImage.Image = global::SRVTracker.Properties.Resources.Save_16x;
            this.buttonSaveSLFImage.Location = new System.Drawing.Point(92, 97);
            this.buttonSaveSLFImage.Name = "buttonSaveSLFImage";
            this.buttonSaveSLFImage.Size = new System.Drawing.Size(23, 23);
            this.buttonSaveSLFImage.TabIndex = 10;
            this.buttonSaveSLFImage.UseVisualStyleBackColor = true;
            this.buttonSaveSLFImage.Click += new System.EventHandler(this.buttonSaveSLFImage_Click);
            // 
            // buttonSaveSRVImage
            // 
            this.buttonSaveSRVImage.Image = global::SRVTracker.Properties.Resources.Save_16x;
            this.buttonSaveSRVImage.Location = new System.Drawing.Point(22, 97);
            this.buttonSaveSRVImage.Name = "buttonSaveSRVImage";
            this.buttonSaveSRVImage.Size = new System.Drawing.Size(23, 23);
            this.buttonSaveSRVImage.TabIndex = 9;
            this.buttonSaveSRVImage.UseVisualStyleBackColor = true;
            this.buttonSaveSRVImage.Click += new System.EventHandler(this.buttonSaveSRVImage_Click);
            // 
            // buttonLoadShipImage
            // 
            this.buttonLoadShipImage.Image = global::SRVTracker.Properties.Resources.FolderOpened_16x;
            this.buttonLoadShipImage.Location = new System.Drawing.Point(187, 97);
            this.buttonLoadShipImage.Name = "buttonLoadShipImage";
            this.buttonLoadShipImage.Size = new System.Drawing.Size(23, 23);
            this.buttonLoadShipImage.TabIndex = 8;
            this.buttonLoadShipImage.UseVisualStyleBackColor = true;
            this.buttonLoadShipImage.Click += new System.EventHandler(this.buttonLoadShipImage_Click);
            // 
            // buttonLoadSLFImage
            // 
            this.buttonLoadSLFImage.Image = global::SRVTracker.Properties.Resources.FolderOpened_16x;
            this.buttonLoadSLFImage.Location = new System.Drawing.Point(117, 97);
            this.buttonLoadSLFImage.Name = "buttonLoadSLFImage";
            this.buttonLoadSLFImage.Size = new System.Drawing.Size(23, 23);
            this.buttonLoadSLFImage.TabIndex = 7;
            this.buttonLoadSLFImage.UseVisualStyleBackColor = true;
            this.buttonLoadSLFImage.Click += new System.EventHandler(this.buttonLoadSLFImage_Click);
            // 
            // buttonLoadSRVImage
            // 
            this.buttonLoadSRVImage.Image = global::SRVTracker.Properties.Resources.FolderOpened_16x;
            this.buttonLoadSRVImage.Location = new System.Drawing.Point(47, 97);
            this.buttonLoadSRVImage.Name = "buttonLoadSRVImage";
            this.buttonLoadSRVImage.Size = new System.Drawing.Size(23, 23);
            this.buttonLoadSRVImage.TabIndex = 6;
            this.buttonLoadSRVImage.UseVisualStyleBackColor = true;
            this.buttonLoadSRVImage.Click += new System.EventHandler(this.buttonLoadSRVImage_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(164, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ship";
            // 
            // pictureBoxShip
            // 
            this.pictureBoxShip.Location = new System.Drawing.Point(146, 32);
            this.pictureBoxShip.Name = "pictureBoxShip";
            this.pictureBoxShip.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxShip.TabIndex = 4;
            this.pictureBoxShip.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxShip, "Ship image.  This will be used for any races you enter that are tracked.");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "SLF";
            // 
            // pictureBoxSLF
            // 
            this.pictureBoxSLF.Location = new System.Drawing.Point(76, 32);
            this.pictureBoxSLF.Name = "pictureBoxSLF";
            this.pictureBoxSLF.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxSLF.TabIndex = 2;
            this.pictureBoxSLF.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxSLF, "SLF image.  This will be used for any races you enter that are tracked.");
            // 
            // pictureBoxSRV
            // 
            this.pictureBoxSRV.Image = global::SRVTracker.Properties.Resources.SRV64;
            this.pictureBoxSRV.Location = new System.Drawing.Point(6, 32);
            this.pictureBoxSRV.Name = "pictureBoxSRV";
            this.pictureBoxSRV.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxSRV.TabIndex = 1;
            this.pictureBoxSRV.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxSRV, "SRV image.  This will be used for any races you enter that are tracked.");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SRV";
            // 
            // buttonUpdateProfile
            // 
            this.buttonUpdateProfile.Location = new System.Drawing.Point(252, 95);
            this.buttonUpdateProfile.Name = "buttonUpdateProfile";
            this.buttonUpdateProfile.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdateProfile.TabIndex = 2;
            this.buttonUpdateProfile.Text = "Update";
            this.toolTip1.SetToolTip(this.buttonUpdateProfile, "Send profile information to server");
            this.buttonUpdateProfile.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(252, 124);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxProfile);
            this.groupBox3.Location = new System.Drawing.Point(12, 226);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(315, 110);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Profile";
            // 
            // textBoxProfile
            // 
            this.textBoxProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxProfile.Location = new System.Drawing.Point(3, 16);
            this.textBoxProfile.Multiline = true;
            this.textBoxProfile.Name = "textBoxProfile";
            this.textBoxProfile.Size = new System.Drawing.Size(309, 91);
            this.textBoxProfile.TabIndex = 0;
            // 
            // FormRacerProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 348);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonUpdateProfile);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRacerProfile";
            this.Text = "Commander Profile";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSLF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSRV)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxTwitchChannel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonLoadSRVImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBoxShip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxSLF;
        private System.Windows.Forms.PictureBox pictureBoxSRV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLoadShipImage;
        private System.Windows.Forms.Button buttonLoadSLFImage;
        private System.Windows.Forms.Button buttonUpdateProfile;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxProfile;
        private System.Windows.Forms.RadioButton radioButtonYouTubeStream;
        private System.Windows.Forms.TextBox textBoxYouTubeChannel;
        private System.Windows.Forms.RadioButton radioButtonTwitchStream;
        private System.Windows.Forms.Button buttonSaveShipImage;
        private System.Windows.Forms.Button buttonSaveSLFImage;
        private System.Windows.Forms.Button buttonSaveSRVImage;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}