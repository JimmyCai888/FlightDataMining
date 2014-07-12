using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace HangBan
{
    #region Models
    public class HangbanInfo
    {
        public string leaveplan { get; set; }
        public string flightno { get; set; }
        public string airlines { get; set; }
        public string terminal { get; set; }
        public string destination { get; set; }
        public string chkincounter { get; set; }
        public string bordinggate { get; set; }
        public string status { get; set; }
    }


    public enum ScratchKind
    {
        BEIJING,
        BASKETBALL
    }
    #endregion

    public class ScratchModel
    {
        #region Member variables
        public frmMain mainFormInst = null;
        public WebHeaderCollection mCurrHeader;
        public CookieCollection mCurrCookie;
        public string mCookieStr;
        public int mPageCnt = 1;
        public bool isRunning = false;
        public List<HangbanInfo> hblist = new List<HangbanInfo>();
        #endregion

        #region delgates
        /// <summary>
        /// 자료수집을 시작하기전에 UI상태바에 알려주기 위한 Delegate
        /// </summary>
        public delegate void InitStartScratchHandler();
        public event InitStartScratchHandler OnStartScratch;

        /// <summary>
        /// 총페지수에 대한 정보를 UI스레드에 알려주기
        /// </summary>
        public delegate void InitTblTotalCountHandler(int totalcnt);
        public event InitTblTotalCountHandler OnScratchPageTotalCount;

        /// <summary>
        /// 한 페지의 scratch작업을 시작할때마다 UI스레드에 통보
        /// </summary>
        /// <param name="pagenum">현재 페지번호</param>
        public delegate void ScratchAPageHandler(int pagenum);
        public event ScratchAPageHandler OnScratchAPage;

        /// <summary>
        /// 한 페지의 scratch작업을 끝낼때마다 UI스레드에 통보
        /// </summary>
        /// <param name="pagenum">현재 페지번호</param>
        public delegate void FinishScratchAPageHandler(int pagenum, List<HangbanInfo> scratchlist);
        public event FinishScratchAPageHandler OnFinishScratchAPage;

        public delegate void FinishScratchAllPageHandler();
        public event FinishScratchAllPageHandler OnFinishScratchAllPage;

        #endregion

        #region Scratch functions
        public void LoadHangBanData()
        {
            Dictionary<string, string> welcomeparams = new Dictionary<string, string>();

            string respData = RequestFirstPage();

            if (!isRunning)
            {
                return;
            }
            OnStartScratch();

            mPageCnt = ParseEngine.GetPageCount(respData);

            if (!isRunning)
            {
                return;
            }
            
            OnScratchPageTotalCount(mPageCnt);

            RetrieveHangbanList();
            
        }

        public string RequestFirstPage()
        {
            string url = "http://www.bcia.com.cn/business/flightInfo.shtml";
            string responseData = string.Empty;

            try
            {
                // creates the post data for the POST request

                WebRequest request = WebRequest.Create(url);
                ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.110 Safari/537.36 CoolNovo/2.0.9.20";

                // Set the Method property of the request to POST.
                request.Method = "GET";
                ((HttpWebRequest)request).KeepAlive = true;
                ((HttpWebRequest)request).UnsafeAuthenticatedConnectionSharing = true;
                ((HttpWebRequest)request).AllowAutoRedirect = true;
                request.ContentType = "application/x-www-form-urlencoded";

                //byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                //request.ContentLength = byteArray.Length;
                //Stream dataStream = request.GetRequestStream();
                //dataStream.Write(byteArray, 0, byteArray.Length);
                //dataStream.Close();

                //  This actually does the request and gets the response back
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();


                using (StreamReader responseReader = new StreamReader(resp.GetResponseStream()))
                {
                    // dumps the HTML from the response into a string variable
                    responseData = responseReader.ReadToEnd();
                    mCurrHeader = resp.Headers;
                    mCurrCookie = resp.Cookies;
                    //mCurrCookieContainer.Add(resp.Cookies);
                    if (resp.Headers["Set-cookie"] != null)
                    {
                        var tmpCookie = resp.Headers["Set-cookie"].ToString().Split(';');
                        mCookieStr = resp.Headers["Set-cookie"].ToString();
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
            
            return responseData;
        }

        public void RetrieveHangbanList()
        {
            hblist.Clear();

            for(int i=0; i < mPageCnt; i++)
            {
                OnScratchAPage(i + 1);
                string respdata = RequestPageData(i + 1);
                List<HangbanInfo> parserst = ParseEngine.ParseHBTable(respdata);
                hblist.AddRange(parserst);
                OnFinishScratchAPage(i + 1, parserst);

                if (!isRunning)
                {
                    return;
                }
                //break;
            }

            OnFinishScratchAllPage();
        }

        public string RequestPageData(int pagenum)
        {
            string url = "http://www.bcia.com.cn/business/flightInfo.jspx?action=list&ajax=html";
            string responseData = string.Empty;

            try
            {
                // creates the post data for the POST request
                string postData = string.Format("language={0}&seaStruts={1}&flightNO={2}&flightCity={3}&airline={4}&pageInfo.pageIndex={5}&flightNO_1={6}&dayNo={7}&find={8}&flightNoType={9}&flightType={10}&flightCity_a={11}&airline_a={12}&day={13}&startTime={14}&endTime={15}&flightStatus={16}",
                    "zh", "2", "", "", "", pagenum.ToString(), "输入航班号", "1", "搜 索", "2", "0", "中文/拼音", "中文/拼音", "1", "99", "99", "0");

                WebRequest request = WebRequest.Create(url);
                ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.110 Safari/537.36 CoolNovo/2.0.9.20";

                // Set the Method property of the request to POST.
                request.Method = "POST";

                //쿠키정보를 추가한다.
                request.Headers.Add("Cookie", mCookieStr);

                ((HttpWebRequest)request).KeepAlive = true;
                ((HttpWebRequest)request).UnsafeAuthenticatedConnectionSharing = true;
                ((HttpWebRequest)request).AllowAutoRedirect = true;
                request.ContentType = "application/x-www-form-urlencoded";

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                //  This actually does the request and gets the response back
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();


                using (StreamReader responseReader = new StreamReader(resp.GetResponseStream()))
                {
                    // dumps the HTML from the response into a string variable
                    responseData = responseReader.ReadToEnd();
                }
            }
            catch (System.Exception ex)
            {
                ScratchModel.WriteLogFile("ScratchModel", "RequestPageData", ex.ToString());
            }

            return responseData;
        }
        #endregion

        public static void WriteLogFile(string fileName, string methodName, string message)
        {
            try
            {
                string filepath = Program.PROGRAM_PATH + "\\debug.log";
                if (!string.IsNullOrEmpty(message))
                {
                    using (FileStream file = new FileStream(filepath, File.Exists(filepath) ? FileMode.Append : FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        StreamWriter streamWriter = new StreamWriter(file);
                        streamWriter.WriteLine((((System.DateTime.Now + " - ") + fileName + " - ") + methodName + " - ") + message);
                        streamWriter.Close();
                    }
                }
            }
            catch
            {

            }
        }

    }
}