﻿namespace DataCollator
{
    partial class Form1
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAPIUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.checkBoxDebug = new System.Windows.Forms.CheckBox();
            this.checkBoxVerboseDebug = new System.Windows.Forms.CheckBox();
            this.textBoxWebURL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxWebURL);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxAPIUrl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 97);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "API Url:";
            // 
            // textBoxAPIUrl
            // 
            this.textBoxAPIUrl.Location = new System.Drawing.Point(85, 45);
            this.textBoxAPIUrl.Name = "textBoxAPIUrl";
            this.textBoxAPIUrl.Size = new System.Drawing.Size(249, 20);
            this.textBoxAPIUrl.TabIndex = 2;
            this.textBoxAPIUrl.Text = "http://localhost:11938/";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "UDP Listen Port:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(95, 19);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {
            11939,
            0,
            0,
            0});
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(143, 128);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(304, 128);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(46, 23);
            this.buttonExit.TabIndex = 2;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // checkBoxDebug
            // 
            this.checkBoxDebug.AutoSize = true;
            this.checkBoxDebug.Location = new System.Drawing.Point(10, 132);
            this.checkBoxDebug.Name = "checkBoxDebug";
            this.checkBoxDebug.Size = new System.Drawing.Size(58, 17);
            this.checkBoxDebug.TabIndex = 3;
            this.checkBoxDebug.Text = "Debug";
            this.checkBoxDebug.UseVisualStyleBackColor = true;
            this.checkBoxDebug.CheckedChanged += new System.EventHandler(this.checkBoxDebug_CheckedChanged);
            // 
            // checkBoxVerboseDebug
            // 
            this.checkBoxVerboseDebug.AutoSize = true;
            this.checkBoxVerboseDebug.Location = new System.Drawing.Point(72, 132);
            this.checkBoxVerboseDebug.Name = "checkBoxVerboseDebug";
            this.checkBoxVerboseDebug.Size = new System.Drawing.Size(65, 17);
            this.checkBoxVerboseDebug.TabIndex = 4;
            this.checkBoxVerboseDebug.Text = "Verbose";
            this.checkBoxVerboseDebug.UseVisualStyleBackColor = true;
            this.checkBoxVerboseDebug.CheckedChanged += new System.EventHandler(this.checkBoxVerboseDebug_CheckedChanged);
            // 
            // textBoxWebURL
            // 
            this.textBoxWebURL.Location = new System.Drawing.Point(85, 71);
            this.textBoxWebURL.Name = "textBoxWebURL";
            this.textBoxWebURL.Size = new System.Drawing.Size(249, 20);
            this.textBoxWebURL.TabIndex = 4;
            this.textBoxWebURL.Text = "https://srvtracker.darkbytes.co.uk/";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Web URL:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 177);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxVerboseDebug);
            this.Controls.Add(this.checkBoxDebug);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Data Collator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAPIUrl;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.CheckBox checkBoxDebug;
        private System.Windows.Forms.CheckBox checkBoxVerboseDebug;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxWebURL;
    }
}

