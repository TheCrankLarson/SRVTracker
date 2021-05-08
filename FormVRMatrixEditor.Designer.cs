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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackBarEditMatrixValue = new System.Windows.Forms.TrackBar();
            this.numericUpDownm11 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownm10 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownm9 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownm8 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownm7 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownm6 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownm5 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownm4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownm3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownm2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownm1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownm0 = new System.Windows.Forms.NumericUpDown();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.checkBoxAutoApply = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numericUpDownOverlayWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxMatrixName = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.listBoxMatrices = new System.Windows.Forms.ListBox();
            this.checkBoxMatrixIsRelative = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEditMatrixValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOverlayWidth)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackBarEditMatrixValue);
            this.groupBox1.Controls.Add(this.numericUpDownm11);
            this.groupBox1.Controls.Add(this.numericUpDownm10);
            this.groupBox1.Controls.Add(this.numericUpDownm9);
            this.groupBox1.Controls.Add(this.numericUpDownm8);
            this.groupBox1.Controls.Add(this.numericUpDownm7);
            this.groupBox1.Controls.Add(this.numericUpDownm6);
            this.groupBox1.Controls.Add(this.numericUpDownm5);
            this.groupBox1.Controls.Add(this.numericUpDownm4);
            this.groupBox1.Controls.Add(this.numericUpDownm3);
            this.groupBox1.Controls.Add(this.numericUpDownm2);
            this.groupBox1.Controls.Add(this.numericUpDownm1);
            this.groupBox1.Controls.Add(this.numericUpDownm0);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 150);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transform matrix";
            // 
            // trackBarEditMatrixValue
            // 
            this.trackBarEditMatrixValue.LargeChange = 1000;
            this.trackBarEditMatrixValue.Location = new System.Drawing.Point(7, 97);
            this.trackBarEditMatrixValue.Maximum = 10000;
            this.trackBarEditMatrixValue.Minimum = -10000;
            this.trackBarEditMatrixValue.Name = "trackBarEditMatrixValue";
            this.trackBarEditMatrixValue.Size = new System.Drawing.Size(245, 45);
            this.trackBarEditMatrixValue.SmallChange = 100;
            this.trackBarEditMatrixValue.TabIndex = 12;
            this.trackBarEditMatrixValue.TickFrequency = 100;
            this.toolTip1.SetToolTip(this.trackBarEditMatrixValue, "Use the slider to adjust the currently selected matrix value");
            this.trackBarEditMatrixValue.Scroll += new System.EventHandler(this.trackBarEditMatrixValue_Scroll);
            // 
            // numericUpDownm11
            // 
            this.numericUpDownm11.DecimalPlaces = 2;
            this.numericUpDownm11.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownm11.Location = new System.Drawing.Point(195, 71);
            this.numericUpDownm11.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownm11.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownm11.Name = "numericUpDownm11";
            this.numericUpDownm11.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownm11.TabIndex = 11;
            this.numericUpDownm11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.numericUpDownm11, "-z offset");
            this.numericUpDownm11.ValueChanged += new System.EventHandler(this.numericUpDownm11_ValueChanged);
            this.numericUpDownm11.Enter += new System.EventHandler(this.numericUpDownm11_Enter);
            // 
            // numericUpDownm10
            // 
            this.numericUpDownm10.DecimalPlaces = 2;
            this.numericUpDownm10.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownm10.Location = new System.Drawing.Point(6, 71);
            this.numericUpDownm10.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownm10.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownm10.Name = "numericUpDownm10";
            this.numericUpDownm10.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownm10.TabIndex = 10;
            this.numericUpDownm10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownm10.ValueChanged += new System.EventHandler(this.numericUpDownm10_ValueChanged);
            this.numericUpDownm10.Enter += new System.EventHandler(this.numericUpDownm10_Enter);
            // 
            // numericUpDownm9
            // 
            this.numericUpDownm9.DecimalPlaces = 2;
            this.numericUpDownm9.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownm9.Location = new System.Drawing.Point(132, 19);
            this.numericUpDownm9.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownm9.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownm9.Name = "numericUpDownm9";
            this.numericUpDownm9.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownm9.TabIndex = 9;
            this.numericUpDownm9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownm9.ValueChanged += new System.EventHandler(this.numericUpDownm9_ValueChanged);
            this.numericUpDownm9.Enter += new System.EventHandler(this.numericUpDownm9_Enter);
            // 
            // numericUpDownm8
            // 
            this.numericUpDownm8.DecimalPlaces = 2;
            this.numericUpDownm8.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownm8.Location = new System.Drawing.Point(132, 45);
            this.numericUpDownm8.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownm8.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownm8.Name = "numericUpDownm8";
            this.numericUpDownm8.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownm8.TabIndex = 8;
            this.numericUpDownm8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownm8.ValueChanged += new System.EventHandler(this.numericUpDownm8_ValueChanged);
            this.numericUpDownm8.Enter += new System.EventHandler(this.numericUpDownm8_Enter);
            // 
            // numericUpDownm7
            // 
            this.numericUpDownm7.DecimalPlaces = 2;
            this.numericUpDownm7.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownm7.Location = new System.Drawing.Point(195, 45);
            this.numericUpDownm7.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownm7.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownm7.Name = "numericUpDownm7";
            this.numericUpDownm7.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownm7.TabIndex = 7;
            this.numericUpDownm7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.numericUpDownm7, "y offset");
            this.numericUpDownm7.ValueChanged += new System.EventHandler(this.numericUpDownm7_ValueChanged);
            this.numericUpDownm7.Enter += new System.EventHandler(this.numericUpDownm7_Enter);
            // 
            // numericUpDownm6
            // 
            this.numericUpDownm6.DecimalPlaces = 2;
            this.numericUpDownm6.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownm6.Location = new System.Drawing.Point(69, 19);
            this.numericUpDownm6.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownm6.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownm6.Name = "numericUpDownm6";
            this.numericUpDownm6.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownm6.TabIndex = 6;
            this.numericUpDownm6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownm6.ValueChanged += new System.EventHandler(this.numericUpDownm6_ValueChanged);
            this.numericUpDownm6.Enter += new System.EventHandler(this.numericUpDownm6_Enter);
            // 
            // numericUpDownm5
            // 
            this.numericUpDownm5.DecimalPlaces = 2;
            this.numericUpDownm5.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownm5.Location = new System.Drawing.Point(6, 45);
            this.numericUpDownm5.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownm5.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownm5.Name = "numericUpDownm5";
            this.numericUpDownm5.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownm5.TabIndex = 5;
            this.numericUpDownm5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownm5.ValueChanged += new System.EventHandler(this.numericUpDownm5_ValueChanged);
            this.numericUpDownm5.Enter += new System.EventHandler(this.numericUpDownm5_Enter);
            // 
            // numericUpDownm4
            // 
            this.numericUpDownm4.DecimalPlaces = 2;
            this.numericUpDownm4.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownm4.Location = new System.Drawing.Point(132, 71);
            this.numericUpDownm4.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownm4.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownm4.Name = "numericUpDownm4";
            this.numericUpDownm4.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownm4.TabIndex = 4;
            this.numericUpDownm4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownm4.ValueChanged += new System.EventHandler(this.numericUpDownm4_ValueChanged);
            this.numericUpDownm4.Enter += new System.EventHandler(this.numericUpDownm4_Enter);
            // 
            // numericUpDownm3
            // 
            this.numericUpDownm3.DecimalPlaces = 2;
            this.numericUpDownm3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownm3.Location = new System.Drawing.Point(195, 19);
            this.numericUpDownm3.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownm3.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownm3.Name = "numericUpDownm3";
            this.numericUpDownm3.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownm3.TabIndex = 3;
            this.numericUpDownm3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.numericUpDownm3, "x offset");
            this.numericUpDownm3.ValueChanged += new System.EventHandler(this.numericUpDownm3_ValueChanged);
            this.numericUpDownm3.Enter += new System.EventHandler(this.numericUpDownm3_Enter);
            // 
            // numericUpDownm2
            // 
            this.numericUpDownm2.DecimalPlaces = 2;
            this.numericUpDownm2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownm2.Location = new System.Drawing.Point(69, 45);
            this.numericUpDownm2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownm2.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownm2.Name = "numericUpDownm2";
            this.numericUpDownm2.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownm2.TabIndex = 2;
            this.numericUpDownm2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownm2.ValueChanged += new System.EventHandler(this.numericUpDownm2_ValueChanged);
            this.numericUpDownm2.Enter += new System.EventHandler(this.numericUpDownm2_Enter);
            // 
            // numericUpDownm1
            // 
            this.numericUpDownm1.DecimalPlaces = 2;
            this.numericUpDownm1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownm1.Location = new System.Drawing.Point(69, 71);
            this.numericUpDownm1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownm1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownm1.Name = "numericUpDownm1";
            this.numericUpDownm1.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownm1.TabIndex = 1;
            this.numericUpDownm1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownm1.ValueChanged += new System.EventHandler(this.numericUpDownm1_ValueChanged);
            this.numericUpDownm1.Enter += new System.EventHandler(this.numericUpDownm1_Enter);
            // 
            // numericUpDownm0
            // 
            this.numericUpDownm0.DecimalPlaces = 2;
            this.numericUpDownm0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownm0.Location = new System.Drawing.Point(6, 19);
            this.numericUpDownm0.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownm0.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownm0.Name = "numericUpDownm0";
            this.numericUpDownm0.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownm0.TabIndex = 0;
            this.numericUpDownm0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownm0.ValueChanged += new System.EventHandler(this.numericUpDownm0_ValueChanged);
            this.numericUpDownm0.Enter += new System.EventHandler(this.numericUpDownm0_Enter);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(284, 224);
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
            this.buttonClose.Location = new System.Drawing.Point(421, 224);
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
            this.checkBoxAutoApply.Location = new System.Drawing.Point(230, 228);
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
            this.numericUpDownOverlayWidth.Location = new System.Drawing.Point(113, 191);
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
            this.label1.Location = new System.Drawing.Point(12, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "HUD Panel Width:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 193);
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
            this.groupBox2.Location = new System.Drawing.Point(278, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 206);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Saved Matrices";
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
            this.checkBoxMatrixIsRelative.Location = new System.Drawing.Point(12, 168);
            this.checkBoxMatrixIsRelative.Name = "checkBoxMatrixIsRelative";
            this.checkBoxMatrixIsRelative.Size = new System.Drawing.Size(251, 17);
            this.checkBoxMatrixIsRelative.TabIndex = 9;
            this.checkBoxMatrixIsRelative.Text = "Matrix is relative to HMD (otherwise, is absolute)";
            this.checkBoxMatrixIsRelative.UseVisualStyleBackColor = true;
            this.checkBoxMatrixIsRelative.CheckedChanged += new System.EventHandler(this.checkBoxMatrixIsRelative_CheckedChanged);
            // 
            // FormVRMatrixEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 254);
            this.Controls.Add(this.checkBoxMatrixIsRelative);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownOverlayWidth);
            this.Controls.Add(this.checkBoxAutoApply);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(503, 293);
            this.Name = "FormVRMatrixEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Matrix Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEditMatrixValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownm0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOverlayWidth)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownm11;
        private System.Windows.Forms.NumericUpDown numericUpDownm10;
        private System.Windows.Forms.NumericUpDown numericUpDownm9;
        private System.Windows.Forms.NumericUpDown numericUpDownm8;
        private System.Windows.Forms.NumericUpDown numericUpDownm7;
        private System.Windows.Forms.NumericUpDown numericUpDownm6;
        private System.Windows.Forms.NumericUpDown numericUpDownm5;
        private System.Windows.Forms.NumericUpDown numericUpDownm4;
        private System.Windows.Forms.NumericUpDown numericUpDownm3;
        private System.Windows.Forms.NumericUpDown numericUpDownm2;
        private System.Windows.Forms.NumericUpDown numericUpDownm1;
        private System.Windows.Forms.NumericUpDown numericUpDownm0;
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
        private System.Windows.Forms.TrackBar trackBarEditMatrixValue;
        private System.Windows.Forms.CheckBox checkBoxMatrixIsRelative;
    }
}