using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace HangBan
{
    public partial class UCHBQuery : UserControl
    {
        public DBManager mDBManager = new DBManager();
        public List<string> hblist = new List<string>();
        public List<HBQueryInfo> hbinfolist = new List<HBQueryInfo>();
        public Thread mloadDataThread = null;
        public DateTime scratchTime;
        public bool bScratchTicked = false;
        public bool bScratching = false;
        public int channel = 0;
        public string dbstatus = "";

        public HBQueryModel mQueryModel = new HBQueryModel();
        public BackgroundWorker m_installWorker;

        public delegate void InitSwitchPanelHandler(int ind, int kind);
        public event InitSwitchPanelHandler OnSwitchPanel;

        public UCHBQuery()
        {
            InitializeComponent();
            mQueryModel.OnStartQuery += new HBQueryModel.InitStartQueryHandler(OnStartQuery);
            //mQueryModel.OnScratchARow += new HBQueryModel.ScratchARowHandler(OnScratchARow);
            mQueryModel.OnFinishScratchARow += new HBQueryModel.FinishScratchARowHandler(OnFinishScratchARow);
            mQueryModel.OnFinishScratchAllPage += new HBQueryModel.FinishScratchAllPageHandler(OnFinishScratchAllPage);
            if (Program.isShowFlightMan)
            {
                btnSwitch.Visible = true;
                btnLoadflight.Visible = true;
            }

        }

        #region Delegations
        void OnStartQuery()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    hbinfolist.Clear();
                    lblStatus.Text = "正在开始采集航班信息。。。";
//                     for (int i = 0; i < flightgrid.Rows.Count; i++)
//                     {
//                         flightgrid.Rows.RemoveAt(i);
//                     }

                }));
            }
        }

        void OnScratchARow(int rownum)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    lblStatus.Text = String.Format("正在获取航班信息(第{0}页 / 共计{1}页", rownum.ToString(), flightgrid.RowCount.ToString());
                }));
            }
        }

        void OnFinishScratchAllPage()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    lblStatus.Text = String.Format("采集航班信息任务完成！");
                    progressBar1.Value = 0;
                    btnLoadflight.Enabled = true;
                    btnQuery.Enabled = true;
                    btnExcel.Enabled = true;
                    btnSaveRst.Enabled = true;
                    btnCancelQuery.Enabled = false;
                    bScratching = false;

                    if (bScratchTicked)
                    {
                        bScratchTicked = false;
                        SaveToDatabase();
                    }
                }));
            }
        }

        void OnFinishScratchARow(int rownum, HBQueryInfo scratchinfo)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    hbinfolist.Add(scratchinfo);
                    flightgrid.Rows[rownum].Cells["flightno"].Value = scratchinfo.flightno;
                    flightgrid.Rows[rownum].Cells["sourcepos"].Value = scratchinfo.sourcepos;
                    flightgrid.Rows[rownum].Cells["destpos"].Value = scratchinfo.destpos;
                    flightgrid.Rows[rownum].Cells["takeoffplace"].Value = scratchinfo.takeoffplace;
                    flightgrid.Rows[rownum].Cells["arriveplace"].Value = scratchinfo.arriveplace;
                    flightgrid.Rows[rownum].Cells["leaveplan"].Value = scratchinfo.leaveplan;
                    flightgrid.Rows[rownum].Cells["leavetime"].Value = scratchinfo.leavetime;
                    flightgrid.Rows[rownum].Cells["arriveplan"].Value = scratchinfo.arriveplan;
                    flightgrid.Rows[rownum].Cells["arrivetime"].Value = scratchinfo.arrivetime;
                    flightgrid.Rows[rownum].Cells["estimatedleavetime"].Value = scratchinfo.estimatedleavetime;
                    flightgrid.Rows[rownum].Cells["estimatedarrivetime"].Value = scratchinfo.estimatedarrivetime;
                    flightgrid.Rows[rownum].Cells["gate"].Value = scratchinfo.gate;
                    flightgrid.Rows[rownum].Cells["status"].Value = scratchinfo.status;
                    flightgrid.Rows[rownum].Cells["flighttime"].Value = String.Format("{0:yyyy年MM月dd日}", dateTimePicker1.Value);
                    flightgrid.Update();

                    progressBar1.Value = (int)Math.Round(((100 / (decimal)flightgrid.RowCount) * rownum));
                }));
            }
        }

        #endregion

        private void btnLoadflight_Click(object sender, EventArgs e)
        {
            LoadFlightList();
        }

        public void LoadFlightList()
        {
            try
            {
                dbstatus = mDBManager.InitConnection(0);

                if (!String.IsNullOrEmpty(dbstatus))
                {
                    lblStatus.Text = dbstatus;
                    return;
                }

                hblist = mDBManager.SelectFlightNoList();

                flightgrid.Rows.Clear();
                foreach (var item in hblist)
                {
                    int index = flightgrid.Rows.Add();
                    flightgrid.Rows[index].Cells["flightno"].Value = item.ToUpper();
                }
                flightgrid.Update();
                mDBManager.CloseConnection();
            }
            catch (System.Exception ex)
            {

            }
        }

        public void StartQuery()
        {
            if (bScratching)
            {
                return;
            }

            bScratching = true;
            btnLoadflight.Enabled = false;
            btnQuery.Enabled = false;
            btnExcel.Enabled = false;
            btnSaveRst.Enabled = false;
            lblStatus.Text = "正在连接地址。。。";
            scratchTime = dateTimePicker1.Value;

            if (mloadDataThread != null)
            {
                mQueryModel.isRunning = false;
                mloadDataThread.Interrupt();
                mloadDataThread.Abort();
                mloadDataThread = null;
            }

            mQueryModel.isRunning = true;
            mQueryModel.queryDate = dateTimePicker1.Value;
            mQueryModel.flightlist = hblist;
            mloadDataThread = new Thread(mQueryModel.LoadHangBanData);
            mloadDataThread.Start();

            btnCancelQuery.Enabled = true;

            return;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            StartQuery();
        }

        private void btnCancelQuery_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "取消中。。。";
            if (mloadDataThread != null)
            {
                mQueryModel.isRunning = false;
                mloadDataThread.Interrupt();
                mloadDataThread.Abort();
                mloadDataThread = null;
                //close connection
            }

            btnLoadflight.Enabled = true;
            btnQuery.Enabled = true;
            btnCancelQuery.Enabled = false;
            bScratching = false;
            progressBar1.Value = 0;
            lblStatus.Text = "";
        }

        private void btnSaveRst_Click(object sender, EventArgs e)
        {
            SaveToDatabase();
        }

        public void SaveToDatabase()
        {
            btnSaveRst.Enabled = false;
            lblStatus.Text = "正在连接数据库。。。";

            m_installWorker = new BackgroundWorker();
            m_installWorker.WorkerReportsProgress = true;
            m_installWorker.WorkerSupportsCancellation = true;
            m_installWorker.DoWork += new DoWorkEventHandler(SaveDB_DoWork);
            m_installWorker.ProgressChanged += new ProgressChangedEventHandler(SaveDB_ProgressChanged);
            m_installWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(SaveDB_RunWorkerCompleted);
            m_installWorker.RunWorkerAsync();
        }

        private void SaveDB_DoWork(object sender, DoWorkEventArgs e)
        {
            dbstatus = mDBManager.InitConnection(0);

            if (!String.IsNullOrEmpty(dbstatus))
            {
                e.Cancel = true;
                return;
            }

            mDBManager.tbl_hbquery = "";

            lblStatus.Text = "正在向数据库导入航班信息，请稍等。。。";
            mDBManager.InsertTodayQueryList(hbinfolist, scratchTime);

        }

        private void SaveDB_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //(int)Math.Round((100 / (decimal)sparam.apkpaths.Count) * i)
            //string.Format("Progress : {0} %", e.ProgressPercentage);
        }

        private void SaveDB_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                lblStatus.Text = dbstatus;
            }
            else if (e.Error != null)
            {
                //Program.mDeviceList.OnCompleted_DeviceApkInstall(m_device.deviceid);
            }
            else
            {
                lblStatus.Text = "导入信息完了！";
            }

            mDBManager.CloseConnection();
            btnSaveRst.Enabled = true;
        }


        private void btnExcel_Click(object sender, EventArgs e)
        {
//             SaveFileDialog fdlg = new SaveFileDialog();
//             fdlg.Title = "导出航班查询结果EXCEL";
//             fdlg.InitialDirectory = @"c:\";
//             fdlg.Filter = "Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
//             fdlg.FileName = "航班查询结果-" + String.Format("{0:yyyy-MM-dd}", scratchTime) + ".xls";
//             fdlg.FilterIndex = 1;
//             fdlg.RestoreDirectory = true;
//             if (fdlg.ShowDialog() == DialogResult.OK)
//             {
//                 using (FileStream ostr = (FileStream)fdlg.OpenFile())
//                 {
//                     if (ostr != null)
//                     {
//                         ostr.Write(noteTextArea.Text, 0, noteTextArea.TextLength);
// 
//                     }
//                 }
//             }

        }

        public void StartTickScratch()
        {
            if (!bScratching)
            {
                bScratchTicked = true;
                StartQuery();
            }
        }

        public void InitConfig(string strTitle, string querytable, string flighttable, int nChn)
        {
            mDBManager.tbl_hbquery = querytable;
            mDBManager.tbl_flightno = flighttable;
            lblChannel.Text = strTitle;
            channel = nChn;

            LoadFlightList();
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            OnSwitchPanel(channel, 1);
        }

        private void UCHBQuery_Load(object sender, EventArgs e)
        {

        }
    }
}
