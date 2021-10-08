namespace SRVTracker
{
    partial class FormVRMatrixEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVRMatrixEditor));
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.checkBoxAutoApply = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numericUpDownOverlayWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxMatrixName = new System.Windows.Forms.TextBox();
            this.listBoxMatrices = new System.Windows.Forms.ListBox();
            this.checkBoxMatrixIsRelative = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBarPositionZ = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBarPositionY = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarPositionX = new System.Windows.Forms.TrackBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBarRotationZ = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.trackBarRotationY = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.trackBarRotationX = new System.Windows.Forms.TrackBar();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownZ = new System.Windows.Forms.NumericUpDown();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonResetRotation = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOverlayWidth)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPositionZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPositionX)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotationZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotationY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotationX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZ)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(419, 331);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(78, 23);
            this.buttonApply.TabIndex = 1;
            this.buttonApply.Text = "Apply";
            this.toolTip1.SetToolTip(this.buttonApply, "Apply changes");
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(777, 53);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(57, 23);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // checkBoxAutoApply
            // 
            this.checkBoxAutoApply.AutoSize = true;
            this.checkBoxAutoApply.Checked = true;
            this.checkBoxAutoApply.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoApply.Location = new System.Drawing.Point(365, 335);
            this.checkBoxAutoApply.Name = "checkBoxAutoApply";
            this.checkBoxAutoApply.Size = new System.Drawing.Size(48, 17);
            this.checkBoxAutoApply.TabIndex = 4;
            this.checkBoxAutoApply.Text = "Auto";
            this.toolTip1.SetToolTip(this.checkBoxAutoApply, "When checked, changes to the\r\nmatrix are applied automatically");
            this.checkBoxAutoApply.UseVisualStyleBackColor = true;
            this.checkBoxAutoApply.CheckedChanged += new System.EventHandler(this.checkBoxAutoApply_CheckedChanged);
            // 
            // numericUpDownOverlayWidth
            // 
            this.numericUpDownOverlayWidth.DecimalPlaces = 2;
            this.numericUpDownOverlayWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownOverlayWidth.Location = new System.Drawing.Point(407, 237);
            this.numericUpDownOverlayWidth.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownOverlayWidth.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.numericUpDownOverlayWidth.Name = "numericUpDownOverlayWidth";
            this.numericUpDownOverlayWidth.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownOverlayWidth.TabIndex = 5;
            this.numericUpDownOverlayWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.numericUpDownOverlayWidth, "HUD panel width in metres.\r\nMust be applied manually.");
            this.numericUpDownOverlayWidth.Value = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            this.numericUpDownOverlayWidth.ValueChanged += new System.EventHandler(this.numericUpDownOverlayWidth_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(306, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Panel Width:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(470, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "m";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxMatrixName);
            this.groupBox2.Controls.Add(this.buttonSave);
            this.groupBox2.Controls.Add(this.buttonDelete);
            this.groupBox2.Controls.Add(this.buttonAdd);
            this.groupBox2.Controls.Add(this.listBoxMatrices);
            this.groupBox2.Location = new System.Drawing.Point(303, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 206);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Saved Positions";
            // 
            // textBoxMatrixName
            // 
            this.textBoxMatrixName.Enabled = false;
            this.textBoxMatrixName.Location = new System.Drawing.Point(90, 180);
            this.textBoxMatrixName.Name = "textBoxMatrixName";
            this.textBoxMatrixName.Size = new System.Drawing.Size(104, 20);
            this.textBoxMatrixName.TabIndex = 4;
            this.textBoxMatrixName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxMatrixName_Validating);
            // 
            // listBoxMatrices
            // 
            this.listBoxMatrices.FormattingEnabled = true;
            this.listBoxMatrices.Location = new System.Drawing.Point(6, 19);
            this.listBoxMatrices.Name = "listBoxMatrices";
            this.listBoxMatrices.Size = new System.Drawing.Size(188, 147);
            this.listBoxMatrices.TabIndex = 0;
            this.listBoxMatrices.SelectedIndexChanged += new System.EventHandler(this.listBoxMatrices_SelectedIndexChanged);
            // 
            // checkBoxMatrixIsRelative
            // 
            this.checkBoxMatrixIsRelative.AutoSize = true;
            this.checkBoxMatrixIsRelative.Location = new System.Drawing.Point(365, 459);
            this.checkBoxMatrixIsRelative.Name = "checkBoxMatrixIsRelative";
            this.checkBoxMatrixIsRelative.Size = new System.Drawing.Size(251, 17);
            this.checkBoxMatrixIsRelative.TabIndex = 9;
            this.checkBoxMatrixIsRelative.Text = "Matrix is relative to HMD (otherwise, is absolute)";
            this.checkBoxMatrixIsRelative.UseVisualStyleBackColor = true;
            this.checkBoxMatrixIsRelative.CheckedChanged += new System.EventHandler(this.checkBoxMatrixIsRelative_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDownZ);
            this.groupBox3.Controls.Add(this.numericUpDownY);
            this.groupBox3.Controls.Add(this.numericUpDownX);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.trackBarPositionZ);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.trackBarPositionY);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.trackBarPositionX);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(285, 170);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Panel Position";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Z:";
            // 
            // trackBarPositionZ
            // 
            this.trackBarPositionZ.LargeChange = 50;
            this.trackBarPositionZ.Location = new System.Drawing.Point(90, 121);
            this.trackBarPositionZ.Maximum = 1000;
            this.trackBarPositionZ.Minimum = -1000;
            this.trackBarPositionZ.Name = "trackBarPositionZ";
            this.trackBarPositionZ.Size = new System.Drawing.Size(189, 45);
            this.trackBarPositionZ.TabIndex = 4;
            this.trackBarPositionZ.TickFrequency = 100;
            this.trackBarPositionZ.Scroll += new System.EventHandler(this.trackBarPositionZ_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Y:";
            // 
            // trackBarPositionY
            // 
            this.trackBarPositionY.LargeChange = 50;
            this.trackBarPositionY.Location = new System.Drawing.Point(90, 70);
            this.trackBarPositionY.Maximum = 1000;
            this.trackBarPositionY.Minimum = -1000;
            this.trackBarPositionY.Name = "trackBarPositionY";
            this.trackBarPositionY.Size = new System.Drawing.Size(189, 45);
            this.trackBarPositionY.TabIndex = 2;
            this.trackBarPositionY.TickFrequency = 100;
            this.trackBarPositionY.Scroll += new System.EventHandler(this.trackBarPositionY_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "X:";
            // 
            // trackBarPositionX
            // 
            this.trackBarPositionX.LargeChange = 50;
            this.trackBarPositionX.Location = new System.Drawing.Point(90, 19);
            this.trackBarPositionX.Maximum = 1000;
            this.trackBarPositionX.Minimum = -1000;
            this.trackBarPositionX.Name = "trackBarPositionX";
            this.trackBarPositionX.Size = new System.Drawing.Size(189, 45);
            this.trackBarPositionX.TabIndex = 0;
            this.trackBarPositionX.TickFrequency = 100;
            this.trackBarPositionX.Scroll += new System.EventHandler(this.trackBarPositionX_Scroll);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.trackBarRotationZ);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.trackBarRotationY);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.trackBarRotationX);
            this.groupBox4.Location = new System.Drawing.Point(12, 188);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(285, 170);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Panel Rotation";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Z:";
            // 
            // trackBarRotationZ
            // 
            this.trackBarRotationZ.LargeChange = 45;
            this.trackBarRotationZ.Location = new System.Drawing.Point(29, 121);
            this.trackBarRotationZ.Maximum = 180;
            this.trackBarRotationZ.Minimum = -180;
            this.trackBarRotationZ.Name = "trackBarRotationZ";
            this.trackBarRotationZ.Size = new System.Drawing.Size(250, 45);
            this.trackBarRotationZ.TabIndex = 4;
            this.trackBarRotationZ.TickFrequency = 45;
            this.trackBarRotationZ.Scroll += new System.EventHandler(this.trackBarRotationZ_Scroll);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Y:";
            // 
            // trackBarRotationY
            // 
            this.trackBarRotationY.LargeChange = 45;
            this.trackBarRotationY.Location = new System.Drawing.Point(29, 70);
            this.trackBarRotationY.Maximum = 180;
            this.trackBarRotationY.Minimum = -180;
            this.trackBarRotationY.Name = "trackBarRotationY";
            this.trackBarRotationY.Size = new System.Drawing.Size(250, 45);
            this.trackBarRotationY.TabIndex = 2;
            this.trackBarRotationY.TickFrequency = 45;
            this.trackBarRotationY.Scroll += new System.EventHandler(this.trackBarRotationY_Scroll);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "X:";
            // 
            // trackBarRotationX
            // 
            this.trackBarRotationX.LargeChange = 45;
            this.trackBarRotationX.Location = new System.Drawing.Point(29, 19);
            this.trackBarRotationX.Maximum = 180;
            this.trackBarRotationX.Minimum = -180;
            this.trackBarRotationX.Name = "trackBarRotationX";
            this.trackBarRotationX.Size = new System.Drawing.Size(250, 45);
            this.trackBarRotationX.TabIndex = 0;
            this.trackBarRotationX.TickFrequency = 45;
            this.trackBarRotationX.Scroll += new System.EventHandler(this.trackBarRotationX_Scroll);
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.DecimalPlaces = 2;
            this.numericUpDownX.Location = new System.Drawing.Point(29, 23);
            this.numericUpDownX.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownX.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownX.TabIndex = 6;
            this.numericUpDownX.ValueChanged += new System.EventHandler(this.numericUpDownX_ValueChanged);
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.DecimalPlaces = 2;
            this.numericUpDownY.Location = new System.Drawing.Point(29, 74);
            this.numericUpDownY.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownY.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownY.TabIndex = 7;
            this.numericUpDownY.ValueChanged += new System.EventHandler(this.numericUpDownY_ValueChanged);
            // 
            // numericUpDownZ
            // 
            this.numericUpDownZ.DecimalPlaces = 2;
            this.numericUpDownZ.Location = new System.Drawing.Point(29, 125);
            this.numericUpDownZ.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownZ.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownZ.Name = "numericUpDownZ";
            this.numericUpDownZ.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownZ.TabIndex = 8;
            this.numericUpDownZ.ValueChanged += new System.EventHandler(this.numericUpDownZ_ValueChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Image = global::SRVTracker.Properties.Resources.Save_16x;
            this.buttonSave.Location = new System.Drawing.Point(58, 178);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(26, 23);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Image = global::SRVTracker.Properties.Resources.Remove_color_16x;
            this.buttonDelete.Location = new System.Drawing.Point(32, 178);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(26, 23);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Image = global::SRVTracker.Properties.Resources.Add_16x;
            this.buttonAdd.Location = new System.Drawing.Point(6, 178);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(26, 23);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonResetRotation
            // 
            this.buttonResetRotation.Location = new System.Drawing.Point(309, 280);
            this.buttonResetRotation.Name = "buttonResetRotation";
            this.buttonResetRotation.Size = new System.Drawing.Size(104, 23);
            this.buttonResetRotation.TabIndex = 12;
            this.buttonResetRotation.Text = "Reset Rotation";
            this.buttonResetRotation.UseVisualStyleBackColor = true;
            this.buttonResetRotation.Click += new System.EventHandler(this.buttonResetRotation_Click);
            // 
            // FormVRMatrixEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 369);
            this.Controls.Add(this.buttonResetRotation);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.checkBoxMatrixIsRelative);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownOverlayWidth);
            this.Controls.Add(this.checkBoxAutoApply);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(503, 293);
            this.Name = "FormVRMatrixEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Panel Position Editor";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOverlayWidth)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPositionZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPositionX)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotationZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotationY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotationX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.CheckBox checkBoxAutoApply;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NumericUpDown numericUpDownOverlayWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxMatrixName;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ListBox listBoxMatrices;
        private System.Windows.Forms.CheckBox checkBoxMatrixIsRelative;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBarPositionZ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBarPositionY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBarPositionX;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trackBarRotationZ;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar trackBarRotationY;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar trackBarRotationX;
        private System.Windows.Forms.NumericUpDown numericUpDownZ;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.Button buttonResetRotation;
    }
}