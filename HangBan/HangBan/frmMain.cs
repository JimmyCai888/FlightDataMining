using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace HangBan
{
    public partial class frmMain : Form
    {
        private UCBeijingScratch mUCBJScratch = new UCBeijingScratch();
        private UCHBList mUCHBList = new UCHBList();
        private UCHBList mUCHBList1 = new UCHBList();

        private UCHBQuery mUCHBQuery = new UCHBQuery();
        private UCHBQuery mUCHBQuery1 = new UCHBQuery();

        private frmMinConfirm exitConfimForm = new frmMinConfirm();
        public bool bExitFromTray = false;
        public bool bPassExitConfirm = false;
        public int nOpenChannel = 1;

        public frmMain()
        {
            InitializeComponent();

            mUCBJScratch.OnTickScratch += new UCBeijingScratch.OnTickScratchHandler(OnTickScratch);
            mUCHBQuery.OnSwitchPanel += new UCHBQuery.InitSwitchPanelHandler(OnSwitchPanel);
            mUCHBQuery1.OnSwitchPanel += new UCHBQuery.InitSwitchPanelHandler(OnSwitchPanel);
            mUCHBList.OnSwitchPanel += new UCHBList.InitSwitchPanelHandler(OnSwitchPanel);
            mUCHBList1.OnSwitchPanel += new UCHBList.InitSwitchPanelHandler(OnSwitchPanel);

            InitConfig();
        }

        public void InitConfig()
        {
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool chkProgramExit, chkNotRetryPrompt;
            String strCheckProgramExit = "false", strCheckNotRetryPrompt = "false";

            if (bExitFromTray || bPassExitConfirm)
            {
                return;
            }

            try
            {
                strCheckNotRetryPrompt = Program.mINIFileManager.GetINIValue("Program",
                Program.CHECK_NOT_RETRY_PROMPT, Program.INI_FILE_PATH);

                if (String.IsNullOrEmpty(strCheckNotRetryPrompt))
                {
                    Program.mINIFileManager.SetIniValue("Program",
                        Program.CHECK_NOT_RETRY_PROMPT, "False", Program.INI_FILE_PATH);
                    strCheckNotRetryPrompt = "False";
                }
                chkNotRetryPrompt = Convert.ToBoolean(strCheckNotRetryPrompt);

                if (chkNotRetryPrompt == false)
                {
                    exitConfimForm.StartPosition = FormStartPosition.CenterScreen;
                    exitConfimForm.bNotRetryPrompt = chkNotRetryPrompt;
                    exitConfimForm.ShowDialog();
                    if (exitConfimForm.confirmResult == false)
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                strCheckProgramExit = Program.mINIFileManager.GetINIValue("Program",
                    Program.CHECK_PROGRAM_EXIT_KEY, Program.INI_FILE_PATH);
                if (String.IsNullOrEmpty(strCheckProgramExit))
                {
                    Program.mINIFileManager.SetIniValue("Program",
                        Program.CHECK_PROGRAM_EXIT_KEY, "True", Program.INI_FILE_PATH);
                    strCheckProgramExit = "0";
                }
                chkProgramExit = Convert.ToBoolean(strCheckProgramExit);

                if (chkProgramExit)
                {
                    bPassExitConfirm = true;
                    ExitHangban();
                }
                else
                {
                    e.Cancel = true;
                    MinimizeToTray();
                    this.Hide();
                }

            }
            catch (System.Exception ex)
            {
            	
            }
        }

        public void ExitHangban()
        {
            if (mUCBJScratch.mloadDataThread != null)
            {
                //mloadDataThread.Join();
                mUCBJScratch.mScratchModel.isRunning = false;
                mUCBJScratch.mloadDataThread.Abort();

                mUCHBQuery.mQueryModel.isRunning = false;
                mUCHBQuery1.mQueryModel.isRunning = false;
                mUCHBQuery.mloadDataThread.Abort();
                mUCHBQuery1.mloadDataThread.Abort();

                try
                {
                    mUCBJScratch.mDBManager.CloseConnection();

                    mUCHBList.mDBManager.CloseConnection();
                    mUCHBList1.mDBManager.CloseConnection();
                    mUCHBQuery.mDBManager.CloseConnection();
                    mUCHBQuery1.mDBManager.CloseConnection();
                    //close connection
                }
                catch (System.Exception ex)
                {
                	
                }
            }

            Application.Exit();
        }

        public void MinimizeToTray()
        {
            try
            {
                notifyIconMainForm.Visible = true;
                notifyIconMainForm.ShowBalloonTip(500);
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnSettings_Click(object sender, EventArgs e)
        {
            frmHBSettings frmSettings = new frmHBSettings();
            DialogResult rst = frmSettings.ShowDialog();

            if (rst == DialogResult.OK)
            {
                mUCBJScratch.UpdateUCBeijingView();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            panelMain.Controls.Add(mUCBJScratch);

            mUCHBQuery.InitConfig("沈阳机场", "tbl_hbquery", "tbl_flightno", 0);
            mUCHBQuery1.InitConfig("广州机场", "tbl_hbquery_gz", "tbl_flightno_gz", 1);
            mUCHBList.InitConfig("沈阳机场", "tbl_hbquery", "tbl_flightno", 0);
            mUCHBList1.InitConfig("广州机场", "tbl_hbquery_gz", "tbl_flightno_gz", 1);

            nOpenChannel = 1;
        }

        private void toolStripMenuItemRestore_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            bExitFromTray = true;
            ExitHangban();
        }

        private void notifyIconMainForm_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    this.Show();
                    break;
                default:
                    break;
            }
        }

        private void btnChannel1_Click(object sender, EventArgs e)
        {
            if (nOpenChannel != 1)
            {
                panelMain.Controls.Clear();
                panelMain.Controls.Add(mUCBJScratch);
                nOpenChannel = 1;
            }
        }

        private void btnChannel3_Click(object sender, EventArgs e)
        {
            if (nOpenChannel != 3)
            {
                panelMain.Controls.Clear();
                panelMain.Controls.Add(mUCHBQuery);
                nOpenChannel = 3;
            }
        }

        void OnTickScratch()
        {
            mUCHBQuery.StartTickScratch();
            mUCHBQuery1.StartTickScratch();
        }

        private void btnChannel4_Click(object sender, EventArgs e)
        {
            if (nOpenChannel != 4)
            {
                panelMain.Controls.Clear();
                panelMain.Controls.Add(mUCHBQuery1);
                nOpenChannel = 4;
            }
        }

        public void OnSwitchPanel(int ind, int kind)
        {
            panelMain.Controls.Clear();
            if (ind == 0)
            {
                if (kind == 0)
                {
                    panelMain.Controls.Add(mUCHBQuery);
                }
                else if (kind == 1)
                {
                    panelMain.Controls.Add(mUCHBList);
                }
            }
            else if (ind == 1)
            {
                if (kind == 0)
                {
                    panelMain.Controls.Add(mUCHBQuery1);
                }
                else if (kind == 1)
                {
                    panelMain.Controls.Add(mUCHBList1);
                }
            }
        }
    }
}
