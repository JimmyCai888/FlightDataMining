using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace HangBan
{
    public partial class UCHBList : UserControl
    {
        string importexlname = "";
        public static List<string> hblist = new List<string>();
        public DBManager mDBManager = new DBManager();
        public int channel = 0;

        public delegate void InitSwitchPanelHandler(int ind, int kind);
        public event InitSwitchPanelHandler OnSwitchPanel;

        public UCHBList()
        {
            InitializeComponent();
        }

        public void InitConfig(string strTitle, string querytable, string flighttable, int nChn)
        {
            mDBManager.tbl_hbquery = querytable;
            mDBManager.tbl_flightno = flighttable;
            lblTitle.Text = strTitle;
            channel = nChn;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fdlg.FileName = importexlname;
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                importexlname = fdlg.FileName;
                Import();
                Application.DoEvents();
            }
        }

        private void Import()
        {
            if (importexlname.Trim() != string.Empty)
            {
                try
                {
                    //string[] strTables = GetTableExcel(importexlname);

                    //frmSelectTables objSelectTable = new frmSelectTables(strTables);
                    frmSelectTables objSelectTable = new frmSelectTables(importexlname);
                    if (objSelectTable.ShowDialog(this) == DialogResult.OK)
                    {
                        dgHBList.Rows.Clear();
                        foreach (var item in hblist)
                        {
                            int index = dgHBList.Rows.Add();
                            dgHBList.Rows[index].Cells["flightno"].Value = item;
                            dgHBList.Update();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnSaveHBList_Click(object sender, EventArgs e)
        {
            btnSaveHBList.Enabled = false;
            lblStatus.Text = "正在连接数据库。。。";
            string dbstatus = mDBManager.InitConnection(0);

            if (!String.IsNullOrEmpty(dbstatus))
            {
                lblStatus.Text = dbstatus;
                return;
            }

            lblStatus.Text = "正在向数据库保存航班号，请稍等。。。";

            List<string> flighnolist = new List<string>();

            for (int i = 0; i < dgHBList.RowCount; i++)
            {
                if (dgHBList.Rows[i].Cells["flightno"].Value != null)
                {
                    flighnolist.Add(dgHBList.Rows[i].Cells["flightno"].Value.ToString());
                }
            }

            mDBManager.InsertFlightNoList(flighnolist);
            mDBManager.CloseConnection();
            lblStatus.Text = "保存成功！";
            btnSaveHBList.Enabled = true;

        }

        private void UCHBList_Load(object sender, EventArgs e)
        {
            try
            {
                string dbstatus = mDBManager.InitConnection(0);

                if (!String.IsNullOrEmpty(dbstatus))
                {
                    lblStatus.Text = dbstatus;
                    return;
                }

                hblist = mDBManager.SelectFlightNoList();

                foreach (var item in hblist)
                {
                    int index = dgHBList.Rows.Add();
                    dgHBList.Rows[index].Cells["flightno"].Value = item;
                    dgHBList.Update();
                }
                mDBManager.CloseConnection();
            }
            catch (System.Exception ex)
            {
            	
            }

        }

        private void dgHBList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgHBList_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
        {
        }

        private void dgHBList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

//            hblist.Insert(e.RowIndex, dgHBList.Rows[e.RowIndex].Cells["flightno"].Value.ToString());
//             if (dgHBList.CurrentCell != null)
//             {
//                 int currInd = dgHBList.CurrentCell.RowIndex;
// 
//                 hblist[currInd] = dgHBList.CurrentCell.Value.ToString();
//             }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            OnSwitchPanel(channel, 0);
        }
    }
}
