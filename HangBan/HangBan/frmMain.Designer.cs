namespace HangBan
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMain = new System.Windows.Forms.Panel();
            this.trayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIconMainForm = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnChannel3 = new System.Windows.Forms.Button();
            this.btnChannel1 = new System.Windows.Forms.Button();
            this.btnChannel4 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.trayContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1047, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.修改密码ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 修改密码ToolStripMenuItem
            // 
            this.修改密码ToolStripMenuItem.Name = "修改密码ToolStripMenuItem";
            this.修改密码ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.修改密码ToolStripMenuItem.Text = "修改密码";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnSettings});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // mnSettings
            // 
            this.mnSettings.Name = "mnSettings";
            this.mnSettings.Size = new System.Drawing.Size(122, 22);
            this.mnSettings.Text = "系统设置";
            this.mnSettings.Click += new System.EventHandler(this.mnSettings_Click);
            // 
            // panelMain
            // 
            this.panelMain.Location = new System.Drawing.Point(0, 89);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1047, 606);
            this.panelMain.TabIndex = 1;
            // 
            // trayContextMenu
            // 
            this.trayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemRestore,
            this.toolStripMenuItemExit});
            this.trayContextMenu.Name = "trayContextMenu";
            this.trayContextMenu.Size = new System.Drawing.Size(99, 48);
            // 
            // toolStripMenuItemRestore
            // 
            this.toolStripMenuItemRestore.Name = "toolStripMenuItemRestore";
            this.toolStripMenuItemRestore.Size = new System.Drawing.Size(98, 22);
            this.toolStripMenuItemRestore.Text = "显示";
            this.toolStripMenuItemRestore.Click += new System.EventHandler(this.toolStripMenuItemRestore_Click);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(98, 22);
            this.toolStripMenuItemExit.Text = "退出";
            this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
            // 
            // notifyIconMainForm
            // 
            this.notifyIconMainForm.BalloonTipText = "系统正在后台运行";
            this.notifyIconMainForm.ContextMenuStrip = this.trayContextMenu;
            this.notifyIconMainForm.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconMainForm.Icon")));
            this.notifyIconMainForm.Text = "抓取航班信息";
            this.notifyIconMainForm.Visible = true;
            this.notifyIconMainForm.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconMainForm_MouseClick);
            this.btnChannel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnChannel3.Location = new System.Drawing.Point(81, 27);
            this.btnChannel3.Name = "btnChannel3";
            this.btnChannel3.Size = new System.Drawing.Size(72, 59);
            this.btnChannel3.TabIndex = 4;
            this.btnChannel3.Text = "沈阳机场";
            this.btnChannel3.UseVisualStyleBackColor = true;
            this.btnChannel3.Click += new System.EventHandler(this.btnChannel3_Click);
            // 
            // btnChannel1
            // 
            this.btnChannel1.BackgroundImage = global::HangBan.Properties.Resources.flight_tracker;
            this.btnChannel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnChannel1.Location = new System.Drawing.Point(4, 27);
            this.btnChannel1.Name = "btnChannel1";
            this.btnChannel1.Size = new System.Drawing.Size(71, 59);
            this.btnChannel1.TabIndex = 2;
            this.btnChannel1.UseVisualStyleBackColor = true;
            this.btnChannel1.Click += new System.EventHandler(this.btnChannel1_Click);
            // 
            // btnChannel4
            // 
            this.btnChannel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnChannel4.Location = new System.Drawing.Point(159, 27);
            this.btnChannel4.Name = "btnChannel4";
            this.btnChannel4.Size = new System.Drawing.Size(72, 59);
            this.btnChannel4.TabIndex = 5;
            this.btnChannel4.Text = "广州机场";
            this.btnChannel4.UseVisualStyleBackColor = true;
            this.btnChannel4.Click += new System.EventHandler(this.btnChannel4_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 692);
            this.Controls.Add(this.btnChannel4);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.btnChannel3);
            this.Controls.Add(this.btnChannel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "航班信息查询系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.trayContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnSettings;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ContextMenuStrip trayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRestore;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.NotifyIcon notifyIconMainForm;
        private System.Windows.Forms.Button btnChannel1;
        private System.Windows.Forms.Button btnChannel3;
        private System.Windows.Forms.Button btnChannel4;
    }
}

