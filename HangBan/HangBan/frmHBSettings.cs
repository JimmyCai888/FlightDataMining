using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HangBan
{
    public partial class frmHBSettings : Form
    {
        public frmHBSettings()
        {
            InitializeComponent();
        }

        private void frmHBSettings_Load(object sender, EventArgs e)
        {
            txt_dbaddr.Text = Program.MySQL_ADDR;
            txt_dbuser.Text = Program.MySQL_USER;
            txt_dbpwd.Text = Program.MySQL_PWD;
            txt_dbname.Text = Program.MySQL_DBNAME;
            txt_tblname.Text = Program.MySQL_TBLNAME;
            chk_autoscratch.Checked = Program.Scratch_Auto;
            txt_scratchinterval.Text = Program.Scratch_Interval.ToString();

            this.AcceptButton = btnOK;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Program.MySQL_ADDR = txt_dbaddr.Text;
            Program.MySQL_USER = txt_dbuser.Text;
            Program.MySQL_PWD = txt_dbpwd.Text;
            Program.MySQL_DBNAME = txt_dbname.Text;
            Program.MySQL_TBLNAME = txt_tblname.Text;
            Program.Scratch_Auto = chk_autoscratch.Checked;
            try
            {
                Program.Scratch_Interval = int.Parse(txt_scratchinterval.Text);

                if (Program.Scratch_Interval == 0)
                {
                    Program.Scratch_Interval = 3;
                }
            }
            catch (System.Exception ex)
            {
                Program.Scratch_Interval = 3;
            }

            Program.mINIFileManager.SetIniValue(Program.INI_SQLTAB, "ADDR", Program.MySQL_ADDR, Program.INI_FILE_PATH);
            Program.mINIFileManager.SetIniValue(Program.INI_SQLTAB, "USER", Program.MySQL_USER, Program.INI_FILE_PATH);
            Program.mINIFileManager.SetIniValue(Program.INI_SQLTAB, "PWD", Program.MySQL_PWD, Program.INI_FILE_PATH);
            Program.mINIFileManager.SetIniValue(Program.INI_SQLTAB, "DBNAME", Program.MySQL_DBNAME, Program.INI_FILE_PATH);
            Program.mINIFileManager.SetIniValue(Program.INI_SQLTAB, "TBLNAME", Program.MySQL_TBLNAME, Program.INI_FILE_PATH);
            Program.mINIFileManager.SetIniValue(Program.INI_SCRATCHTAB, "IsAutoScratch", Program.Scratch_Auto == true ? "1" : "0", Program.INI_FILE_PATH);
            Program.mINIFileManager.SetIniValue(Program.INI_SCRATCHTAB, "ScratchInterval", Program.Scratch_Interval.ToString(), Program.INI_FILE_PATH);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBManager dbManage = new DBManager();

            string dbstatus = dbManage.InitConnection(1);

            if (String.IsNullOrEmpty(dbstatus))
            {
                MessageBox.Show("连接成功！");
            }
        }
    }
}
