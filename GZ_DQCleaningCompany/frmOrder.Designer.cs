﻿namespace GZ_DQCleaningCompany
{
    partial class frmOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrder));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.pbStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.filterButton = new System.Windows.Forms.Button();
            this.stockInDateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.stockOutDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.newButton = new System.Windows.Forms.Button();
            this.delScheduleButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.xuhao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hetongbianhao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kehuxingming = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kehudizhi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lianxidianhua = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hetongdaoqishijian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hetongshichang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shishou = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yuejia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mianji = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tixingshoufeishijian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.meiyuanci = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zongcishu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shengyushu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chaboli = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kehuyaoqiu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.beizhu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xinzeng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Input_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.moveDayDownButton = new System.Windows.Forms.Button();
            this.moveDayUpButton = new System.Windows.Forms.Button();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // pbStatus
            // 
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(100, 22);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbStatus,
            this.toolStripLabel1,
            this.toolStripLabel2,
            this.toolStripLabel3});
            this.toolStrip2.Location = new System.Drawing.Point(3, 513);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(862, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel2.ForeColor = System.Drawing.Color.Red;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(180, 22);
            this.toolStripLabel2.Text = "*普通权限只可以维护剩余列信息";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "actn020.gif");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(471, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 15);
            this.label1.TabIndex = 90;
            this.label1.Text = "*如果查找所有请填写\"所有\"";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(15, 65);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(73, 20);
            this.label24.TabIndex = 89;
            this.label24.Text = "查找内容";
            // 
            // textBox8
            // 
            this.textBox8.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.Location = new System.Drawing.Point(109, 63);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(356, 26);
            this.textBox8.TabIndex = 8;
            // 
            // filterButton
            // 
            this.filterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filterButton.Location = new System.Drawing.Point(744, 26);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(101, 41);
            this.filterButton.TabIndex = 78;
            this.filterButton.Text = "查找";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // stockInDateTimePicker1
            // 
            this.stockInDateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stockInDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.stockInDateTimePicker1.Location = new System.Drawing.Point(356, 22);
            this.stockInDateTimePicker1.Name = "stockInDateTimePicker1";
            this.stockInDateTimePicker1.Size = new System.Drawing.Size(109, 26);
            this.stockInDateTimePicker1.TabIndex = 5;
            this.stockInDateTimePicker1.Value = new System.DateTime(2018, 1, 31, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(263, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 20);
            this.label4.TabIndex = 76;
            this.label4.Text = "结束时间";
            // 
            // stockOutDateTimePicker
            // 
            this.stockOutDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stockOutDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.stockOutDateTimePicker.Location = new System.Drawing.Point(109, 22);
            this.stockOutDateTimePicker.Name = "stockOutDateTimePicker";
            this.stockOutDateTimePicker.Size = new System.Drawing.Size(114, 26);
            this.stockOutDateTimePicker.TabIndex = 4;
            this.stockOutDateTimePicker.Value = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "开始时间";
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.textBox8);
            this.groupBox2.Controls.Add(this.filterButton);
            this.groupBox2.Controls.Add(this.stockInDateTimePicker1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.stockOutDateTimePicker);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(848, 92);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Action";
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(99, 26);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 874F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(955, 547);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip1.Size = new System.Drawing.Size(81, 547);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(79, 25);
            this.toolStripButton3.Text = "新增";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(79, 25);
            this.toolStripButton1.Text = "保存";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(79, 25);
            this.toolStripButton2.Text = "下载";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(79, 25);
            this.toolStripButton4.Text = "导入";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.toolStrip2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.groupBox1.Location = new System.Drawing.Point(84, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(868, 541);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(3, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(862, 497);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(854, 470);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "主页";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.moveDayDownButton);
            this.groupBox3.Controls.Add(this.moveDayUpButton);
            this.groupBox3.Controls.Add(this.newButton);
            this.groupBox3.Controls.Add(this.delScheduleButton);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Location = new System.Drawing.Point(3, 426);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(848, 41);
            this.groupBox3.TabIndex = 43;
            this.groupBox3.TabStop = false;
            // 
            // newButton
            // 
            this.newButton.Location = new System.Drawing.Point(6, 9);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(35, 25);
            this.newButton.TabIndex = 38;
            this.newButton.Text = "+";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // delScheduleButton
            // 
            this.delScheduleButton.Location = new System.Drawing.Point(47, 9);
            this.delScheduleButton.Name = "delScheduleButton";
            this.delScheduleButton.Size = new System.Drawing.Size(35, 25);
            this.delScheduleButton.TabIndex = 39;
            this.delScheduleButton.Text = "-";
            this.delScheduleButton.UseVisualStyleBackColor = true;
            this.delScheduleButton.Click += new System.EventHandler(this.delScheduleButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.xuhao,
            this.Message,
            this.hetongbianhao,
            this.kehuxingming,
            this.kehudizhi,
            this.lianxidianhua,
            this.hetongdaoqishijian,
            this.hetongshichang,
            this.shishou,
            this.yuejia,
            this.mianji,
            this.tixingshoufeishijian,
            this.meiyuanci,
            this.zongcishu,
            this.shengyushu,
            this.chaboli,
            this.kehuyaoqiu,
            this.beizhu,
            this.xinzeng,
            this.order_id,
            this.Input_Date});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.Location = new System.Drawing.Point(3, 95);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(848, 325);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // xuhao
            // 
            this.xuhao.HeaderText = "序号";
            this.xuhao.Name = "xuhao";
            // 
            // Message
            // 
            this.Message.DataPropertyName = "Message";
            this.Message.HeaderText = "提醒";
            this.Message.Name = "Message";
            // 
            // hetongbianhao
            // 
            this.hetongbianhao.DataPropertyName = "hetongbianhao";
            this.hetongbianhao.HeaderText = "合同编号";
            this.hetongbianhao.Name = "hetongbianhao";
            // 
            // kehuxingming
            // 
            this.kehuxingming.DataPropertyName = "kehuxingming";
            this.kehuxingming.HeaderText = "客户姓名";
            this.kehuxingming.Name = "kehuxingming";
            // 
            // kehudizhi
            // 
            this.kehudizhi.DataPropertyName = "kehudizhi";
            this.kehudizhi.HeaderText = "客户住址";
            this.kehudizhi.Name = "kehudizhi";
            // 
            // lianxidianhua
            // 
            this.lianxidianhua.DataPropertyName = "lianxidianhua";
            this.lianxidianhua.HeaderText = "联系电话";
            this.lianxidianhua.Name = "lianxidianhua";
            // 
            // hetongdaoqishijian
            // 
            this.hetongdaoqishijian.DataPropertyName = "hetongdaoqishijian";
            this.hetongdaoqishijian.HeaderText = "合同到期时间";
            this.hetongdaoqishijian.Name = "hetongdaoqishijian";
            // 
            // hetongshichang
            // 
            this.hetongshichang.DataPropertyName = "hetongshichang";
            this.hetongshichang.HeaderText = "合同时长";
            this.hetongshichang.Name = "hetongshichang";
            // 
            // shishou
            // 
            this.shishou.DataPropertyName = "shishou";
            this.shishou.HeaderText = "实收/元";
            this.shishou.Name = "shishou";
            // 
            // yuejia
            // 
            this.yuejia.DataPropertyName = "yuejia";
            this.yuejia.HeaderText = "月价/元";
            this.yuejia.Name = "yuejia";
            // 
            // mianji
            // 
            this.mianji.DataPropertyName = "mianji";
            this.mianji.HeaderText = "面积/m²";
            this.mianji.Name = "mianji";
            // 
            // tixingshoufeishijian
            // 
            this.tixingshoufeishijian.DataPropertyName = "tixingshoufeishijian";
            this.tixingshoufeishijian.HeaderText = "提醒收费时间";
            this.tixingshoufeishijian.Name = "tixingshoufeishijian";
            // 
            // meiyuanci
            // 
            this.meiyuanci.DataPropertyName = "meiyuanci";
            this.meiyuanci.HeaderText = "每月次";
            this.meiyuanci.Name = "meiyuanci";
            // 
            // zongcishu
            // 
            this.zongcishu.DataPropertyName = "zongcishu";
            this.zongcishu.HeaderText = "总次数";
            this.zongcishu.Name = "zongcishu";
            // 
            // shengyushu
            // 
            this.shengyushu.DataPropertyName = "shengyushu";
            this.shengyushu.HeaderText = "剩余数";
            this.shengyushu.Name = "shengyushu";
            // 
            // chaboli
            // 
            this.chaboli.DataPropertyName = "chaboli";
            this.chaboli.HeaderText = "擦玻璃";
            this.chaboli.Name = "chaboli";
            // 
            // kehuyaoqiu
            // 
            this.kehuyaoqiu.DataPropertyName = "kehuyaoqiu";
            this.kehuyaoqiu.HeaderText = "客户要求";
            this.kehuyaoqiu.Name = "kehuyaoqiu";
            // 
            // beizhu
            // 
            this.beizhu.DataPropertyName = "beizhu";
            this.beizhu.HeaderText = "备注";
            this.beizhu.Name = "beizhu";
            // 
            // xinzeng
            // 
            this.xinzeng.DataPropertyName = "xinzeng";
            this.xinzeng.HeaderText = "新增";
            this.xinzeng.Name = "xinzeng";
            this.xinzeng.Visible = false;
            // 
            // order_id
            // 
            this.order_id.DataPropertyName = "order_id";
            this.order_id.HeaderText = "order_id";
            this.order_id.Name = "order_id";
            this.order_id.Visible = false;
            // 
            // Input_Date
            // 
            this.Input_Date.DataPropertyName = "Input_Date";
            this.Input_Date.HeaderText = "添加时间";
            this.Input_Date.Name = "Input_Date";
            // 
            // moveDayDownButton
            // 
            this.moveDayDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveDayDownButton.Location = new System.Drawing.Point(806, 13);
            this.moveDayDownButton.Name = "moveDayDownButton";
            this.moveDayDownButton.Size = new System.Drawing.Size(36, 25);
            this.moveDayDownButton.TabIndex = 41;
            this.moveDayDownButton.Text = "▽";
            this.moveDayDownButton.UseVisualStyleBackColor = true;
            this.moveDayDownButton.Click += new System.EventHandler(this.moveDayDownButton_Click);
            // 
            // moveDayUpButton
            // 
            this.moveDayUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveDayUpButton.Location = new System.Drawing.Point(764, 13);
            this.moveDayUpButton.Name = "moveDayUpButton";
            this.moveDayUpButton.Size = new System.Drawing.Size(36, 25);
            this.moveDayUpButton.TabIndex = 40;
            this.moveDayUpButton.Text = "△";
            this.moveDayUpButton.UseVisualStyleBackColor = true;
            this.moveDayUpButton.Click += new System.EventHandler(this.moveDayUpButton_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(0, 22);
            // 
            // frmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 547);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmOrder";
            this.Text = "订单管理";
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.ToolStripProgressBar pbStatus;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.DateTimePicker stockInDateTimePicker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker stockOutDateTimePicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Button delScheduleButton;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.DataGridViewTextBoxColumn xuhao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.DataGridViewTextBoxColumn hetongbianhao;
        private System.Windows.Forms.DataGridViewTextBoxColumn kehuxingming;
        private System.Windows.Forms.DataGridViewTextBoxColumn kehudizhi;
        private System.Windows.Forms.DataGridViewTextBoxColumn lianxidianhua;
        private System.Windows.Forms.DataGridViewTextBoxColumn hetongdaoqishijian;
        private System.Windows.Forms.DataGridViewTextBoxColumn hetongshichang;
        private System.Windows.Forms.DataGridViewTextBoxColumn shishou;
        private System.Windows.Forms.DataGridViewTextBoxColumn yuejia;
        private System.Windows.Forms.DataGridViewTextBoxColumn mianji;
        private System.Windows.Forms.DataGridViewTextBoxColumn tixingshoufeishijian;
        private System.Windows.Forms.DataGridViewTextBoxColumn meiyuanci;
        private System.Windows.Forms.DataGridViewTextBoxColumn zongcishu;
        private System.Windows.Forms.DataGridViewTextBoxColumn shengyushu;
        private System.Windows.Forms.DataGridViewTextBoxColumn chaboli;
        private System.Windows.Forms.DataGridViewTextBoxColumn kehuyaoqiu;
        private System.Windows.Forms.DataGridViewTextBoxColumn beizhu;
        private System.Windows.Forms.DataGridViewTextBoxColumn xinzeng;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Input_Date;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.Button moveDayDownButton;
        private System.Windows.Forms.Button moveDayUpButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
    }
}