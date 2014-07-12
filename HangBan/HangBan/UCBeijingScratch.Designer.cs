namespace HangBan
{
    partial class UCBeijingScratch
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCBeijingScratch));
            this.hbgrid = new System.Windows.Forms.DataGridView();
            this.leaveplan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flightno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.airlines = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.terminal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkincounter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bordinggate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.prgLoading = new System.Windows.Forms.ProgressBar();
            this.lbl_lefttime = new System.Windows.Forms.Label();
            this.lbl_autoscrttitle = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerScratch = new System.Windows.Forms.Timer(this.components);
            this.timerCounter = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnLoadData = new System.Windows.Forms.ToolStripButton();
            this.btnCancelLoad = new System.Windows.Forms.ToolStripButton();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.btnSaveToDB = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.hbgrid)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hbgrid
            // 
            this.hbgrid.AllowUserToAddRows = false;
            this.hbgrid.AllowUserToDeleteRows = false;
            this.hbgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.hbgrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.leaveplan,
            this.flightno,
            this.airlines,
            this.terminal,
            this.destination,
            this.chkincounter,
            this.bordinggate,
            this.status});
            this.hbgrid.Location = new System.Drawing.Point(196, 61);
            this.hbgrid.Name = "hbgrid";
            this.hbgrid.ReadOnly = true;
            this.hbgrid.Size = new System.Drawing.Size(839, 459);
            this.hbgrid.TabIndex = 6;
            // 
            // leaveplan
            // 
            this.leaveplan.HeaderText = "计划离港";
            this.leaveplan.Name = "leaveplan";
            this.leaveplan.ReadOnly = true;
            // 
            // flightno
            // 
            this.flightno.HeaderText = "航班号";
            this.flightno.Name = "flightno";
            this.flightno.ReadOnly = true;
            // 
            // airlines
            // 
            this.airlines.HeaderText = "航空公司";
            this.airlines.Name = "airlines";
            this.airlines.ReadOnly = true;
            // 
            // terminal
            // 
            this.terminal.HeaderText = "候机楼";
            this.terminal.Name = "terminal";
            this.terminal.ReadOnly = true;
            // 
            // destination
            // 
            this.destination.HeaderText = "目的地";
            this.destination.Name = "destination";
            this.destination.ReadOnly = true;
            // 
            // chkincounter
            // 
            this.chkincounter.HeaderText = "值机柜台";
            this.chkincounter.Name = "chkincounter";
            this.chkincounter.ReadOnly = true;
            // 
            // bordinggate
            // 
            this.bordinggate.HeaderText = "登机口";
            this.bordinggate.Name = "bordinggate";
            this.bordinggate.ReadOnly = true;
            // 
            // status
            // 
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "北京航班"});
            this.listBox1.Location = new System.Drawing.Point(12, 61);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(178, 459);
            this.listBox1.TabIndex = 5;
            // 
            // prgLoading
            // 
            this.prgLoading.Location = new System.Drawing.Point(3, 554);
            this.prgLoading.Name = "prgLoading";
            this.prgLoading.Size = new System.Drawing.Size(1047, 16);
            this.prgLoading.TabIndex = 9;
            // 
            // lbl_lefttime
            // 
            this.lbl_lefttime.AutoSize = true;
            this.lbl_lefttime.ForeColor = System.Drawing.Color.Red;
            this.lbl_lefttime.Location = new System.Drawing.Point(136, 533);
            this.lbl_lefttime.Name = "lbl_lefttime";
            this.lbl_lefttime.Size = new System.Drawing.Size(49, 13);
            this.lbl_lefttime.TabIndex = 11;
            this.lbl_lefttime.Text = "00:00:00";
            // 
            // lbl_autoscrttitle
            // 
            this.lbl_autoscrttitle.AutoSize = true;
            this.lbl_autoscrttitle.ForeColor = System.Drawing.Color.Red;
            this.lbl_autoscrttitle.Location = new System.Drawing.Point(15, 533);
            this.lbl_autoscrttitle.Name = "lbl_autoscrttitle";
            this.lbl_autoscrttitle.Size = new System.Drawing.Size(115, 13);
            this.lbl_autoscrttitle.TabIndex = 10;
            this.lbl_autoscrttitle.Text = "自动抓取剩下时间：";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 577);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1048, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(187, 17);
            this.lblStatus.Text = "请选择机场之后点击获取数据按钮";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLoadData,
            this.btnCancelLoad,
            this.btnExcel,
            this.btnSaveToDB});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1048, 58);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // btnLoadData
            // 
            this.btnLoadData.AutoSize = false;
            this.btnLoadData.Image = global::HangBan.Properties.Resources.refresh;
            this.btnLoadData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(80, 55);
            this.btnLoadData.Text = "采集航班信息";
            this.btnLoadData.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLoadData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // btnCancelLoad
            // 
            this.btnCancelLoad.AutoSize = false;
            this.btnCancelLoad.Enabled = false;
            this.btnCancelLoad.Image = global::HangBan.Properties.Resources.cancel;
            this.btnCancelLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelLoad.Name = "btnCancelLoad";
            this.btnCancelLoad.Size = new System.Drawing.Size(60, 55);
            this.btnCancelLoad.Text = "取消抓取";
            this.btnCancelLoad.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelLoad.Click += new System.EventHandler(this.btnCancelLoad_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(61, 55);
            this.btnExcel.Text = "导出Excel";
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExcel.Visible = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnSaveToDB
            // 
            this.btnSaveToDB.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveToDB.Image")));
            this.btnSaveToDB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveToDB.Name = "btnSaveToDB";
            this.btnSaveToDB.Size = new System.Drawing.Size(83, 55);
            this.btnSaveToDB.Text = "保存到数据库";
            this.btnSaveToDB.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaveToDB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveToDB.Click += new System.EventHandler(this.btnSaveToDB_Click);
            // 
            // UCBeijingScratch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.prgLoading);
            this.Controls.Add(this.lbl_lefttime);
            this.Controls.Add(this.lbl_autoscrttitle);
            this.Controls.Add(this.hbgrid);
            this.Controls.Add(this.listBox1);
            this.Name = "UCBeijingScratch";
            this.Size = new System.Drawing.Size(1048, 599);
            this.Load += new System.EventHandler(this.UCBeijingScratch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.hbgrid)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView hbgrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn leaveplan;
        private System.Windows.Forms.DataGridViewTextBoxColumn flightno;
        private System.Windows.Forms.DataGridViewTextBoxColumn airlines;
        private System.Windows.Forms.DataGridViewTextBoxColumn terminal;
        private System.Windows.Forms.DataGridViewTextBoxColumn destination;
        private System.Windows.Forms.DataGridViewTextBoxColumn chkincounter;
        private System.Windows.Forms.DataGridViewTextBoxColumn bordinggate;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ProgressBar prgLoading;
        private System.Windows.Forms.Label lbl_lefttime;
        private System.Windows.Forms.Label lbl_autoscrttitle;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Timer timerScratch;
        private System.Windows.Forms.Timer timerCounter;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnLoadData;
        private System.Windows.Forms.ToolStripButton btnCancelLoad;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private System.Windows.Forms.ToolStripButton btnSaveToDB;

    }
}
