namespace SRVTracker
{
    partial class UpdaterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdaterForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAvailableVersion = new System.Windows.Forms.TextBox();
            this.textBoxThisVersion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonYes = new System.Windows.Forms.Button();
            this.buttonNo = new System.Windows.Forms.Button();
            this.groupBoxUpdating = new System.Windows.Forms.GroupBox();
            this.textBoxUpdateProgress = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBoxUpdating.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxAvailableVersion);
            this.groupBox1.Controls.Add(this.textBoxThisVersion);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 81);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Update Available";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Available version:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "This version:";
            // 
            // textBoxAvailableVersion
            // 
            this.textBoxAvailableVersion.BackColor = System.Drawing.Color.PaleGreen;
            this.textBoxAvailableVersion.Location = new System.Drawing.Point(102, 45);
            this.textBoxAvailableVersion.Name = "textBoxAvailableVersion";
            this.textBoxAvailableVersion.ReadOnly = true;
            this.textBoxAvailableVersion.Size = new System.Drawing.Size(100, 20);
            this.textBoxAvailableVersion.TabIndex = 3;
            this.textBoxAvailableVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxThisVersion
            // 
            this.textBoxThisVersion.Location = new System.Drawing.Point(102, 19);
            this.textBoxThisVersion.Name = "textBoxThisVersion";
            this.textBoxThisVersion.ReadOnly = true;
            this.textBoxThisVersion.Size = new System.Drawing.Size(100, 20);
            this.textBoxThisVersion.TabIndex = 2;
            this.textBoxThisVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Update now?";
            // 
            // buttonYes
            // 
            this.buttonYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonYes.Location = new System.Drawing.Point(123, 99);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.Size = new System.Drawing.Size(50, 23);
            this.buttonYes.TabIndex = 0;
            this.buttonYes.Text = "Yes";
            this.buttonYes.UseVisualStyleBackColor = true;
            // 
            // buttonNo
            // 
            this.buttonNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonNo.Location = new System.Drawing.Point(179, 99);
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.Size = new System.Drawing.Size(50, 23);
            this.buttonNo.TabIndex = 1;
            this.buttonNo.Text = "No";
            this.buttonNo.UseVisualStyleBackColor = true;
            this.buttonNo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonNo_MouseClick);
            // 
            // groupBoxUpdating
            // 
            this.groupBoxUpdating.Controls.Add(this.textBoxUpdateProgress);
            this.groupBoxUpdating.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxUpdating.Location = new System.Drawing.Point(0, 0);
            this.groupBoxUpdating.Name = "groupBoxUpdating";
            this.groupBoxUpdating.Size = new System.Drawing.Size(239, 131);
            this.groupBoxUpdating.TabIndex = 2;
            this.groupBoxUpdating.TabStop = false;
            this.groupBoxUpdating.Text = "Updating";
            // 
            // textBoxUpdateProgress
            // 
            this.textBoxUpdateProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxUpdateProgress.Location = new System.Drawing.Point(3, 16);
            this.textBoxUpdateProgress.Multiline = true;
            this.textBoxUpdateProgress.Name = "textBoxUpdateProgress";
            this.textBoxUpdateProgress.ReadOnly = true;
            this.textBoxUpdateProgress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxUpdateProgress.Size = new System.Drawing.Size(233, 112);
            this.textBoxUpdateProgress.TabIndex = 0;
            // 
            // UpdaterForm
            // 
            this.AcceptButton = this.buttonYes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonNo;
            this.ClientSize = new System.Drawing.Size(239, 131);
            this.ControlBox = false;
            this.Controls.Add(this.groupBoxUpdating);
            this.Controls.Add(this.buttonNo);
            this.Controls.Add(this.buttonYes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdaterForm";
            this.Text = "Automatic Update";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxUpdating.ResumeLayout(false);
            this.groupBoxUpdating.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAvailableVersion;
        private System.Windows.Forms.TextBox textBoxThisVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonYes;
        private System.Windows.Forms.Button buttonNo;
        private System.Windows.Forms.GroupBox groupBoxUpdating;
        private System.Windows.Forms.TextBox textBoxUpdateProgress;
    }
}