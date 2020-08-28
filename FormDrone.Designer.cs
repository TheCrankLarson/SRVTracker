namespace SRVTracker
{
    partial class FormDrone
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
            this.buttonW = new System.Windows.Forms.Button();
            this.buttonA = new System.Windows.Forms.Button();
            this.buttonS = new System.Windows.Forms.Button();
            this.buttonD = new System.Windows.Forms.Button();
            this.buttonQ = new System.Windows.Forms.Button();
            this.buttonE = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.checkBoxSetSpeed = new System.Windows.Forms.CheckBox();
            this.numericUpDownTargetSpeed = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numericUpDownTargetAltitude = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTargetSpeed)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTargetAltitude)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonW
            // 
            this.buttonW.Location = new System.Drawing.Point(43, 19);
            this.buttonW.Name = "buttonW";
            this.buttonW.Size = new System.Drawing.Size(31, 23);
            this.buttonW.TabIndex = 0;
            this.buttonW.Text = "W";
            this.buttonW.UseVisualStyleBackColor = true;
            this.buttonW.Click += new System.EventHandler(this.buttonW_Click);
            // 
            // buttonA
            // 
            this.buttonA.Location = new System.Drawing.Point(6, 48);
            this.buttonA.Name = "buttonA";
            this.buttonA.Size = new System.Drawing.Size(31, 23);
            this.buttonA.TabIndex = 1;
            this.buttonA.Text = "A";
            this.buttonA.UseVisualStyleBackColor = true;
            this.buttonA.Click += new System.EventHandler(this.buttonA_Click);
            // 
            // buttonS
            // 
            this.buttonS.Location = new System.Drawing.Point(43, 48);
            this.buttonS.Name = "buttonS";
            this.buttonS.Size = new System.Drawing.Size(31, 23);
            this.buttonS.TabIndex = 2;
            this.buttonS.Text = "S";
            this.buttonS.UseVisualStyleBackColor = true;
            this.buttonS.Click += new System.EventHandler(this.buttonS_Click);
            // 
            // buttonD
            // 
            this.buttonD.Location = new System.Drawing.Point(80, 48);
            this.buttonD.Name = "buttonD";
            this.buttonD.Size = new System.Drawing.Size(31, 23);
            this.buttonD.TabIndex = 3;
            this.buttonD.Text = "D";
            this.buttonD.UseVisualStyleBackColor = true;
            this.buttonD.Click += new System.EventHandler(this.buttonD_Click);
            // 
            // buttonQ
            // 
            this.buttonQ.Location = new System.Drawing.Point(6, 19);
            this.buttonQ.Name = "buttonQ";
            this.buttonQ.Size = new System.Drawing.Size(31, 23);
            this.buttonQ.TabIndex = 4;
            this.buttonQ.Text = "Q";
            this.buttonQ.UseVisualStyleBackColor = true;
            this.buttonQ.Click += new System.EventHandler(this.buttonQ_Click);
            // 
            // buttonE
            // 
            this.buttonE.Location = new System.Drawing.Point(80, 19);
            this.buttonE.Name = "buttonE";
            this.buttonE.Size = new System.Drawing.Size(31, 23);
            this.buttonE.TabIndex = 5;
            this.buttonE.Text = "E";
            this.buttonE.UseVisualStyleBackColor = true;
            this.buttonE.Click += new System.EventHandler(this.buttonE_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(251, 97);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 6;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // checkBoxSetSpeed
            // 
            this.checkBoxSetSpeed.AutoSize = true;
            this.checkBoxSetSpeed.Location = new System.Drawing.Point(6, 19);
            this.checkBoxSetSpeed.Name = "checkBoxSetSpeed";
            this.checkBoxSetSpeed.Size = new System.Drawing.Size(92, 17);
            this.checkBoxSetSpeed.TabIndex = 7;
            this.checkBoxSetSpeed.Text = "Target speed:";
            this.checkBoxSetSpeed.UseVisualStyleBackColor = true;
            this.checkBoxSetSpeed.CheckedChanged += new System.EventHandler(this.checkBoxSetSpeed_CheckedChanged);
            // 
            // numericUpDownTargetSpeed
            // 
            this.numericUpDownTargetSpeed.Location = new System.Drawing.Point(104, 18);
            this.numericUpDownTargetSpeed.Name = "numericUpDownTargetSpeed";
            this.numericUpDownTargetSpeed.Size = new System.Drawing.Size(74, 20);
            this.numericUpDownTargetSpeed.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonQ);
            this.groupBox1.Controls.Add(this.buttonW);
            this.groupBox1.Controls.Add(this.buttonA);
            this.groupBox1.Controls.Add(this.buttonS);
            this.groupBox1.Controls.Add(this.buttonE);
            this.groupBox1.Controls.Add(this.buttonD);
            this.groupBox1.Location = new System.Drawing.Point(207, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(119, 79);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 42);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(97, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Target altitude:";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // numericUpDownTargetAltitude
            // 
            this.numericUpDownTargetAltitude.Location = new System.Drawing.Point(104, 41);
            this.numericUpDownTargetAltitude.Name = "numericUpDownTargetAltitude";
            this.numericUpDownTargetAltitude.Size = new System.Drawing.Size(74, 20);
            this.numericUpDownTargetAltitude.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxSetSpeed);
            this.groupBox2.Controls.Add(this.numericUpDownTargetAltitude);
            this.groupBox2.Controls.Add(this.numericUpDownTargetSpeed);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(189, 72);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cruise control";
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Location = new System.Drawing.Point(28, 127);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.Size = new System.Drawing.Size(298, 20);
            this.textBoxStatus.TabIndex = 13;
            // 
            // FormDrone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 165);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonStop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDrone";
            this.Text = "Vehicle Control";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTargetSpeed)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTargetAltitude)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonW;
        private System.Windows.Forms.Button buttonA;
        private System.Windows.Forms.Button buttonS;
        private System.Windows.Forms.Button buttonD;
        private System.Windows.Forms.Button buttonQ;
        private System.Windows.Forms.Button buttonE;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.CheckBox checkBoxSetSpeed;
        private System.Windows.Forms.NumericUpDown numericUpDownTargetSpeed;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownTargetAltitude;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxStatus;
    }
}