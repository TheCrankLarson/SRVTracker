namespace Race_Manager
{
    partial class FormTimerControls
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
            this.buttonMove = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonMove
            // 
            this.buttonMove.Image = global::Race_Manager.Properties.Resources.MoveGlyph_16x;
            this.buttonMove.Location = new System.Drawing.Point(80, 0);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(20, 20);
            this.buttonMove.TabIndex = 9;
            this.buttonMove.UseVisualStyleBackColor = true;
            this.buttonMove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonMove_MouseDown);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Image = global::Race_Manager.Properties.Resources.Stop_16x;
            this.buttonStop.Location = new System.Drawing.Point(40, 0);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(20, 20);
            this.buttonStop.TabIndex = 8;
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Enabled = false;
            this.buttonPause.Image = global::Race_Manager.Properties.Resources.Pause_16x;
            this.buttonPause.Location = new System.Drawing.Point(20, 0);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(20, 20);
            this.buttonPause.TabIndex = 7;
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Image = global::Race_Manager.Properties.Resources.Run_16x;
            this.buttonPlay.Location = new System.Drawing.Point(0, 0);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(20, 20);
            this.buttonPlay.TabIndex = 6;
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // FormTimerControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(100, 20);
            this.Controls.Add(this.buttonMove);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonPlay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(100, 20);
            this.MinimumSize = new System.Drawing.Size(100, 20);
            this.Name = "FormTimerControls";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormTimerControls";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonMove;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.Button buttonPlay;
    }
}