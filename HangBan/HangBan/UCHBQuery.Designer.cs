namespace HangBan
{
    partial class UCHBQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCHBQuery));
            this.btnLoadFlightNo = new System.Windows.Forms.ToolStrip();
            this.btnSwitch = new System.Windows.Forms.ToolStripButton();
            this.btnLoadflight = new System.Windows.Forms.ToolStripButton();
            this.btnQuery = new System.Windows.Forms.ToolStripButton();
            this.btnCancelQuery = new System.Windows.Forms.ToolStripButton();
            this.btnSaveRst = new System.Windows.Forms.ToolStripButton();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.flightgrid = new System.Windows.Forms.DataGridView();
            this.flightno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sourcepos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destpos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.takeoffplace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arriveplace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leaveplan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estimatedleavetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leavetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arriveplan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estimatedarrivetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arrivetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flighttime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblChannel = new System.Windows.Forms.Label();
            this.btnLoadFlightNo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flightgrid)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadFlightNo
            // 
            this.btnLoadFlightNo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSwitch,
            this.btnLoadflight,
            this.btnQuery,
            this.btnCancelQuery,
            this.btnSaveRst,
            this.btnExcel});
            this.btnLoadFlightNo.Location = new System.Drawing.Point(0, 0);
            this.btnLoadFlightNo.Name = "btnLoadFlightNo";
            this.btnLoadFlightNo.Size = new System.Drawing.Size(1048, 38);
            this.btnLoadFlightNo.TabIndex = 0;
            this.btnLoadFlightNo.Text = "toolStrip1";
            // 
            // btnSwitch
            // 
            this.btnSwitch.Image = ((System.Drawing.Image)(resources.GetObject("btnSwitch.Image")));
            this.btnSwitch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(60, 35);
            this.btnSwitch.Text = "航班号->";
            this.btnSwitch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSwitch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSwitch.ToolTipText = "航班号->";
            this.btnSwitch.Visible = false;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // btnLoadflight
            // 
            this.btnLoadflight.AutoSize = false;
            this.btnLoadflight.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadflight.Image")));
            this.btnLoadflight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadflight.Name = "btnLoadflight";
            this.btnLoadflight.Size = new System.Drawing.Size(71, 35);
            this.btnLoadflight.Text = "加载航班号";
            this.btnLoadflight.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLoadflight.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLoadflight.Visible = false;
            this.btnLoadflight.Click += new System.EventHandler(this.btnLoadflight_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.AutoSize = false;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(83, 35);
            this.btnQuery.Text = "抓取航班信息";
            this.btnQuery.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnCancelQuery
            // 
            this.btnCancelQuery.AutoSize = false;
            this.btnCancelQuery.Enabled = false;
            this.btnCancelQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelQuery.Image")));
            this.btnCancelQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelQuery.Name = "btnCancelQuery";
            this.btnCancelQuery.Size = new System.Drawing.Size(83, 35);
            this.btnCancelQuery.Text = "取消查询";
            this.btnCancelQuery.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelQuery.Click += new System.EventHandler(this.btnCancelQuery_Click);
            // 
            // btnSaveRst
            // 
            this.btnSaveRst.AutoSize = false;
            this.btnSaveRst.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveRst.Image")));
            this.btnSaveRst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveRst.Name = "btnSaveRst";
            this.btnSaveRst.Size = new System.Drawing.Size(99, 35);
            this.btnSaveRst.Text = "保存到数据库";
            this.btnSaveRst.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaveRst.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveRst.Click += new System.EventHandler(this.btnSaveRst_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.AutoSize = false;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(84, 35);
            this.btnExcel.Text = "导出EXCEL";
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExcel.Visible = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // flightgrid
            // 
            this.flightgrid.AllowUserToAddRows = false;
            this.flightgrid.AllowUserToDeleteRows = false;
            this.flightgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.flightgrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.flightno,
            this.sourcepos,
            this.destpos,
            this.takeoffplace,
            this.arriveplace,
            this.leaveplan,
            this.estimatedleavetime,
            this.leavetime,
            this.arriveplan,
            this.gate,
            this.estimatedarrivetime,
            this.arrivetime,
            this.status,
            this.flighttime});
            this.flightgrid.Location = new System.Drawing.Point(3, 68);
            this.flightgrid.Name = "flightgrid";
            this.flightgrid.ReadOnly = true;
            this.flightgrid.Size = new System.Drawing.Size(1042, 477);
            this.flightgrid.TabIndex = 1;
            // 
            // flightno
            // 
            this.flightno.HeaderText = "航班号";
            this.flightno.Name = "flightno";
            this.flightno.ReadOnly = true;
            // 
            // sourcepos
            // 
            this.sourcepos.HeaderText = "出发地";
            this.sourcepos.Name = "sourcepos";
            this.sourcepos.ReadOnly = true;
            // 
            // destpos
            // 
            this.destpos.HeaderText = "目的地";
            this.destpos.Name = "destpos";
            this.destpos.ReadOnly = true;
            // 
            // takeoffplace
            // 
            this.takeoffplace.HeaderText = "起飞";
            this.takeoffplace.Name = "takeoffplace";
            this.takeoffplace.ReadOnly = true;
            // 
            // arriveplace
            // 
            this.arriveplace.HeaderText = "到达";
            this.arriveplace.Name = "arriveplace";
            this.arriveplace.ReadOnly = true;
            // 
            // leaveplan
            // 
            this.leaveplan.HeaderText = "出发计划";
            this.leaveplan.Name = "leaveplan";
            this.leaveplan.ReadOnly = true;
            // 
            // estimatedleavetime
            // 
            this.estimatedleavetime.HeaderText = "预计出发时间";
            this.estimatedleavetime.Name = "estimatedleavetime";
            this.estimatedleavetime.ReadOnly = true;
            this.estimatedleavetime.Width = 120;
            // 
            // leavetime
            // 
            this.leavetime.HeaderText = "实际出发时间";
            this.leavetime.Name = "leavetime";
            this.leavetime.ReadOnly = true;
            this.leavetime.Width = 130;
            // 
            // arriveplan
            // 
            this.arriveplan.HeaderText = "达到计划";
            this.arriveplan.Name = "arriveplan";
            this.arriveplan.ReadOnly = true;
            // 
            // gate
            // 
            this.gate.HeaderText = "登机口";
            this.gate.Name = "gate";
            this.gate.ReadOnly = true;
            // 
            // estimatedarrivetime
            // 
            this.estimatedarrivetime.HeaderText = "预计达到时间";
            this.estimatedarrivetime.Name = "estimatedarrivetime";
            this.estimatedarrivetime.ReadOnly = true;
            this.estimatedarrivetime.Width = 120;
            // 
            // arrivetime
            // 
            this.arrivetime.HeaderText = "实际达到时间";
            this.arrivetime.Name = "arrivetime";
            this.arrivetime.ReadOnly = true;
            this.arrivetime.Width = 130;
            // 
            // status
            // 
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // flighttime
            // 
            this.flighttime.HeaderText = "日期";
            this.flighttime.Name = "flighttime";
            this.flighttime.ReadOnly = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 577);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1048, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(235, 17);
            this.lblStatus.Text = "请点击加载航班号，以后点击抓取航班信息";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(96, 42);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 551);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1042, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChannel.Location = new System.Drawing.Point(12, 42);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(0, 20);
            this.lblChannel.TabIndex = 5;
            // 
            // UCHBQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblChannel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.flightgrid);
            this.Controls.Add(this.btnLoadFlightNo);
            this.Name = "UCHBQuery";
            this.Size = new System.Drawing.Size(1048, 599);
            this.Load += new System.EventHandler(this.UCHBQuery_Load);
            this.btnLoadFlightNo.ResumeLayout(false);
            this.btnLoadFlightNo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flightgrid)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip btnLoadFlightNo;
        private System.Windows.Forms.DataGridView flightgrid;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripButton btnLoadflight;
        private System.Windows.Forms.ToolStripButton btnQuery;
        private System.Windows.Forms.ToolStripButton btnCancelQuery;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripButton btnSaveRst;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn flightno;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourcepos;
        private System.Windows.Forms.DataGridViewTextBoxColumn destpos;
        private System.Windows.Forms.DataGridViewTextBoxColumn takeoffplace;
        private System.Windows.Forms.DataGridViewTextBoxColumn arriveplace;
        private System.Windows.Forms.DataGridViewTextBoxColumn leaveplan;
        private System.Windows.Forms.DataGridViewTextBoxColumn estimatedleavetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn leavetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn arriveplan;
        private System.Windows.Forms.DataGridViewTextBoxColumn gate;
        private System.Windows.Forms.DataGridViewTextBoxColumn estimatedarrivetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn arrivetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn flighttime;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.ToolStripButton btnSwitch;
    }
}
