using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace HangBan
{
    public partial class frmMinConfirm : Form
    {
        public int nExitAction;
        public bool confirmResult;
        private Boolean m_bDragging = false;
        private Point m_ptCur;
        private Point m_ptFormPoint;
        public bool bNotRetryPrompt;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect,
                                                            int nTopRect,
                                                            int nRightRect,
                                                            int nBottomRect,
                                                            int nWidthElipse,
                                                            int nHeightElipse);

        public frmMinConfirm()
        {
            InitializeComponent();

            groupBoxAction.Text = "关闭动作";
            radioDirectExit.Text = "直接退出程序";
            radioMinimizeToTray.Text = "最小化到托盘";
            checkNoRetry.Text = "不再提示";
            btnOK.Text = "确定";
            btnCancel.Text = "取消";
            checkNoRetry.Checked = bNotRetryPrompt;
            this.AcceptButton = btnOK;

            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(2, 2, Width, Height, 10, 10));
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            confirmResult = true;            
            Program.mINIFileManager.SetIniValue("Program",
                Program.CHECK_PROGRAM_EXIT_KEY, radioDirectExit.Checked.ToString(), Program.INI_FILE_PATH);

            Program.mINIFileManager.SetIniValue("Program",
                Program.CHECK_NOT_RETRY_PROMPT, checkNoRetry.Checked.ToString(), Program.INI_FILE_PATH);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            confirmResult = false;
            this.Close();
        }

        private void frmMinConfirm_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            //g.FillRoundedRectangle(new SolidBrush(ControlPaint.Light(SystemColors.InactiveBorder, 1.0f)), 5, 5, Width - 10, Height - 10, 5);
        }

        private void frmMinConfirm_MouseDown(object sender, MouseEventArgs e)
        {
            m_bDragging = true;
            m_ptCur = Cursor.Position;
            m_ptFormPoint = this.Location;
        }

        private void frmMinConfirm_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_bDragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(m_ptCur));
                this.Location = Point.Add(m_ptFormPoint, new Size(diff));
            }
        }

        private void frmMinConfirm_MouseUp(object sender, MouseEventArgs e)
        {
            m_bDragging = false;
        }

        private void frmMinConfirm_Load(object sender, EventArgs e)
        {

        }

    }
}
