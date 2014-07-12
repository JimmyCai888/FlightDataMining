namespace HangBan
{
    partial class UCHBList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCHBList));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnBack = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.btnSaveHBList = new System.Windows.Forms.ToolStripButton();
            this.dgHBList = new System.Windows.Forms.DataGridView();
            this.flightno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusUC = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHBList)).BeginInit();
            this.statusUC.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBack,
            this.toolStripButton1,
            this.btnSaveHBList});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1048, 38);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnBack
            // 
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(72, 35);
            this.btnBack.Text = "<-航班查询";
            this.btnBack.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(71, 35);
            this.toolStripButton1.Text = "导入航班号";
            this.toolStripButton1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnSaveHBList
            // 
            this.btnSaveHBList.AutoSize = false;
            this.btnSaveHBList.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveHBList.Image")));
            this.btnSaveHBList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveHBList.Name = "btnSaveHBList";
            this.btnSaveHBList.Size = new System.Drawing.Size(70, 35);
            this.btnSaveHBList.Text = "保存";
            this.btnSaveHBList.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaveHBList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveHBList.Click += new System.EventHandler(this.btnSaveHBList_Click);
            // 
            // dgHBList
            // 
            this.dgHBList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHBList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.flightno});
            this.dgHBList.Location = new System.Drawing.Point(3, 69);
            this.dgHBList.Name = "dgHBList";
            this.dgHBList.Size = new System.Drawing.Size(1042, 505);
            this.dgHBList.TabIndex = 1;
            this.dgHBList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgHBList_CellValueChanged);
            this.dgHBList.NewRowNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgHBList_NewRowNeeded);
            this.dgHBList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgHBList_RowsAdded);
            // 
            // flightno
            // 
            this.flightno.HeaderText = "航班号";
            this.flightno.Name = "flightno";
            // 
            // statusUC
            // 
            this.statusUC.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusUC.Location = new System.Drawing.Point(0, 577);
            this.statusUC.Name = "statusUC";
            this.statusUC.Size = new System.Drawing.Size(1048, 22);
            this.statusUC.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(140, 17);
            this.lblStatus.Text = "请选择xls文件导入航班号";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(10, 43);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 20);
            this.lblTitle.TabIndex = 3;
            // 
            // UCHBList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.statusUC);
            this.Controls.Add(this.dgHBList);
            this.Controls.Add(this.toolStrip1);
            this.Name = "UCHBList";
            this.Size = new System.Drawing.Size(1048, 599);
            this.Load += new System.EventHandler(this.UCHBList_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHBList)).EndInit();
            this.statusUC.ResumeLayout(false);
            this.statusUC.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.DataGridView dgHBList;
        private System.Windows.Forms.StatusStrip statusUC;
        private System.Windows.Forms.DataGridViewTextBoxColumn flightno;
        private System.Windows.Forms.ToolStripButton btnSaveHBList;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripButton btnBack;
        private System.Windows.Forms.Label lblTitle;
    }
}
