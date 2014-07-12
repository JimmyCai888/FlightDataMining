using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace HangBan
{
    public partial class UCBeijingScratch : UserControl
    {
        public Thread mloadDataThread = null;
        public ScratchModel mScratchModel = new ScratchModel();
        public DBManager mDBManager = new DBManager();
        public BackgroundWorker m_installWorker;

        public long nleftSeconds = 0;
        public bool bScratchTicked = false;
        public static ScratchKind mLoadedKind;
        public bool bScrtTmrRunning = false;
        public bool bScratching = false;
        public string dbstatus = "";

        public delegate void OnTickScratchHandler();
        public event OnTickScratchHandler OnTickScratch;

        public UCBeijingScratch()
        {
            InitializeComponent();
            mScratchModel.OnStartScratch += new ScratchModel.InitStartScratchHandler(OnStartScratch);
            mScratchModel.OnScratchPageTotalCount += new ScratchModel.InitTblTotalCountHandler(OnScratchPageTotalCount);
            mScratchModel.OnScratchAPage += new ScratchModel.ScratchAPageHandler(OnScratchAPage);
            mScratchModel.OnFinishScratchAPage += new ScratchModel.FinishScratchAPageHandler(OnFinishScratchAPage);
            mScratchModel.OnFinishScratchAllPage += new ScratchModel.FinishScratchAllPageHandler(OnFinishScratchAllPage);
            timerScratch.Tick += new EventHandler(timerScratch_Tick);
            timerCounter.Tick += new EventHandler(timerCounter_Tick);

            InitConfig();
        }

        public void InitConfig()
        {
            try
            {
                if (!Program.Scratch_Auto)
                {
                    StopScratchTimer();
                }
                else
                {
                    StartScratchTimer();
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        public void StartScratchTimer()
        {
            lbl_autoscrttitle.Visible = true;
            lbl_lefttime.Visible = true;
            timerScratch.Interval = Program.Scratch_Interval * 60 * 1000;
            nleftSeconds = Program.Scratch_Interval * 60;
            timerScratch.Enabled = true;

            timerCounter.Interval = 1000;
            timerCounter.Enabled = true;
            bScrtTmrRunning = true;
        }

        public void StopScratchTimer()
        {
            lbl_autoscrttitle.Visible = false;
            lbl_lefttime.Visible = false;

            timerScratch.Stop();
            timerScratch.Enabled = false;
            timerCounter.Stop();
            timerCounter.Enabled = false;
            bScrtTmrRunning = false;
        }

        private void timerCounter_Tick(object sender, EventArgs e)
        {

            if (nleftSeconds == 0)
            {
                return;
            }

            string lefthour = (nleftSeconds / 3600).ToString();
            string leftminute = ((nleftSeconds % 3600) / 60).ToString();
            string leftsecond = (nleftSeconds % 60).ToString();

            if (lefthour.Count() == 1)
            {
                lefthour = "0" + lefthour;
            }

            if (leftminute.Count() == 1)
            {
                leftminute = "0" + leftminute;
            }
            if (leftsecond.Count() == 1)
            {
                leftsecond = "0" + leftsecond;
            }

            lbl_lefttime.Text = lefthour + ":" + leftminute + ":" + leftsecond;
            nleftSeconds--;
        }

        private void timerScratch_Tick(object sender, EventArgs e)
        {
            if (!bScratching)
            {
                bScratchTicked = true;
                StartScratch();
                OnTickScratch();
                nleftSeconds = Program.Scratch_Interval * 60;
            }

        }
        public void StartScratch()
        {
            mLoadedKind = (ScratchKind)listBox1.SelectedIndex;

            bScratching = true;
            btnLoadData.Enabled = false;
            btnExcel.Enabled = false;
            btnSaveToDB.Enabled = false;
            lblStatus.Text = "正在连接地址。。。";

            if (mloadDataThread != null)
            {
                mScratchModel.isRunning = false;
                mloadDataThread.Join();
            }

            mScratchModel.isRunning = true;
            mloadDataThread = new Thread(mScratchModel.LoadHangBanData);
            mloadDataThread.Start();

            btnCancelLoad.Enabled = true;

            return;
        }

        #region delegation
        void OnStartScratch()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    lblStatus.Text = "正在开始采集航班信息。。。";
                    for (int i = 0; i < hbgrid.Rows.Count; i++)
                    {
                        hbgrid.Rows.RemoveAt(i);
                    }

                }));
            }
        }

        void OnScratchPageTotalCount(int totalpage)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    lblStatus.Text = "总页面：" + totalpage.ToString();
                    prgLoading.Value = 0;
                }));
            }
        }

        void OnScratchAPage(int pagenum)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    lblStatus.Text = String.Format("正在获取航班信息(第{0}页 / 共计{1}页", pagenum.ToString(), mScratchModel.mPageCnt);
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
                    btnLoadData.Enabled = true;
                    btnExcel.Enabled = true;
                    btnSaveToDB.Enabled = true;
                    btnCancelLoad.Enabled = false;
                    bScratching = false;
                    prgLoading.Value = 0;

                    if (bScratchTicked)
                    {
                        bScratchTicked = false;
                        SaveToDatabase();
                    }

                }));
            }
        }

        void OnFinishScratchAPage(int pagenum, List<HangbanInfo> scratchlist)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    //lblStatus.Text = String.Format("正在获取航班信息(第{0}页 / 共计{1}页", pagenum.ToString(), mScratchModel.mPageCnt);
                    foreach (HangbanInfo item in scratchlist)
                    {
                        int index = hbgrid.Rows.Add();
                        hbgrid.Rows[index].Cells["leaveplan"].Value = item.leaveplan;
                        hbgrid.Rows[index].Cells["flightno"].Value = item.flightno;
                        hbgrid.Rows[index].Cells["airlines"].Value = item.airlines;
                        hbgrid.Rows[index].Cells["terminal"].Value = item.terminal;
                        hbgrid.Rows[index].Cells["destination"].Value = item.destination;
                        hbgrid.Rows[index].Cells["chkincounter"].Value = item.chkincounter;
                        hbgrid.Rows[index].Cells["bordinggate"].Value = item.bordinggate;
                        hbgrid.Rows[index].Cells["status"].Value = item.status;
                        hbgrid.Update();
                    }

                    prgLoading.Value = (int)Math.Round(((100 / (decimal)mScratchModel.mPageCnt) * pagenum));
                }));
            }
        }
        #endregion

        public void SaveToDatabase()
        {
            btnSaveToDB.Enabled = false;

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

            lblStatus.Text = "正在向数据库导入航班信息，请稍等。。。";
            mDBManager.InsertTodayList(mScratchModel.hblist);
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
                btnSaveToDB.Enabled = true;
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
            btnSaveToDB.Enabled = true;
        }

        public void UpdateUCBeijingView()
        {
            int currScrIntval = timerScratch.Interval / 60 / 1000;
            if (!Program.Scratch_Auto)
            {
                StopScratchTimer();
                StopScratchTimer();
            }
            else
            {
                if (!bScrtTmrRunning)
                {
                    StopScratchTimer();
                    StartScratchTimer();
                }
                else if (currScrIntval != Program.Scratch_Interval)
                {
                    StopScratchTimer();
                    StartScratchTimer();
                }
            }
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                StartScratch();
            }
            catch (System.Exception ex)
            {
                ScratchModel.WriteLogFile("------------BEIJING-------------", "btnLoadData_Click", ex.ToString());
            }

            return;
        }

        private void btnCancelLoad_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "取消中。。。";
            if (mloadDataThread != null)
            {
                mScratchModel.isRunning = false;
                mloadDataThread.Interrupt();
                mloadDataThread.Abort();
                mloadDataThread = null;
                //close connection
            }

            btnLoadData.Enabled = true;
            btnExcel.Enabled = true;
            btnSaveToDB.Enabled = true;
            btnCancelLoad.Enabled = false;
            bScratching = false;
            lblStatus.Text = "";
            prgLoading.Value = 0;

            return;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveToDB_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(mScratchModel.hblist.Count().ToString());
            SaveToDatabase();
        }

        private void UCBeijingScratch_Load(object sender, EventArgs e)
        {
            //listBox1.Items[0].Selected = true;
            listBox1.SelectedIndex = 0;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
