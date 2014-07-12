using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Drawing;

namespace HangBan
{
    #region Models
    public class HBQueryInfo
    {
        public string flightno { get; set; }
        public string sourcepos { get; set; }
        public string destpos { get; set; }
        public string takeoffplace { get; set; }
        public string arriveplace { get; set; }
        public string leaveplan { get; set; }
        public string arriveplan { get; set; }
        public string leavetime { get; set; }
        public string arrivetime { get; set; }
        public string estimatedleavetime { get; set; }
        public string estimatedarrivetime { get; set; }
        public string gate { get; set; }
        public string status { get; set; }
        public string flighttime { get; set; }
    }

    public class HBQueryRst
    {
        public string airlineName { get; set; }
        public string flightNumber { get; set; }
        public List<SegmentDetail> segmentDetails { get; set; }
    }

    public class SegmentDetail
    {
        public string actualArrivalTime { get; set; }
        public string actualDepartureTime { get; set; }
        public string arrivalCity { get; set; }
        public string arrivalCityAirportName { get; set; }
        public string arrivalTerminal { get; set; }
        public string arrivalTimeDifference { get; set; }
        public string arrivalTimeDifferenceFlag { get; set; }
        public string departureCity { get; set; }
        public string departureCityAirportName { get; set; }
        public string departureTerminal { get; set; }
        public string departureTimeDifference { get; set; }
        public string departureTimeDifferenceFlag { get; set; }
        public string estimatedArrivalTime { get; set; }
        public string estimatedDepartureTime { get; set; }
        public string flightDurationRatio { get; set; }
        public string flightStatus { get; set; }
        public string gate { get; set; }
        public string scheduledArrivalTime { get; set; }
        public string scheduledDepartureDate { get; set; }
        public string scheduledDepartureTime { get; set; }
    }

    #endregion

    public class HBQueryModel
    {
        #region delgates
        /// <summary>
        /// 자료수집을 시작하기전에 UI상태바에 알려주기 위한 Delegate
        /// </summary>
        public delegate void InitStartQueryHandler();
        public event InitStartQueryHandler OnStartQuery;

        /// <summary>
        /// 한 항반번호의 scratch작업을 끝낼때마다 UI스레드에 통보
        /// </summary>
        /// <param name="pagenum">현재 페지번호</param>
        public delegate void FinishScratchARowHandler(int rownum, HBQueryInfo scratchinfo);
        public event FinishScratchARowHandler OnFinishScratchARow;

        public delegate void FinishScratchAllPageHandler();
        public event FinishScratchAllPageHandler OnFinishScratchAllPage;
        #endregion

        #region Member variables
        public frmMain mainFormInst = null;
        public WebHeaderCollection mCurrHeader;
        public CookieCollection mCurrCookie;
        public string mfirstCookieStr;
        public string mSecondCookieStr;
        public bool isRunning = false;
        public DateTime queryDate;
        public List<HBQueryInfo> hblist = new List<HBQueryInfo>();
        public List<string> flightlist = new List<string>();
        #endregion

        public void LoadHangBanData()
        {
            Dictionary<string, string> welcomeparams = new Dictionary<string, string>();

            OnStartQuery();
            int i = 0;
            foreach (var item in flightlist)
            {
                try
                {
                    HBQueryInfo rst = new HBQueryInfo();
                    rst.flightno = item.ToUpper();

                    RequestFirstPage(rst.flightno);
                    if (!isRunning)
                    {
                        return;
                    }

                    var secondresp = RequestSecondPage(rst.flightno);

                    HBQueryRst qrst = JsonConvert.DeserializeObject<HBQueryRst>(secondresp);

                    if (qrst == null || qrst.segmentDetails == null) {
                        
                    }

                    rst.sourcepos = qrst.segmentDetails[0].departureCity;
                    rst.destpos = qrst.segmentDetails[0].arrivalCity;
                    rst.takeoffplace = qrst.segmentDetails[0].departureCityAirportName;
                    rst.arriveplace = qrst.segmentDetails[0].arrivalCityAirportName;
                    rst.status = qrst.segmentDetails[0].flightStatus;

                    string estDepTime = RetrieveEstimatedDepartureTime(rst.flightno, qrst.segmentDetails[0].estimatedDepartureTime);
                    string schdDepTime = RetrieveScheduledDepartureTime(rst.flightno, qrst.segmentDetails[0].scheduledDepartureTime);
                    string actDepTime = RetrieveActualDepartureTime(rst.flightno, qrst.segmentDetails[0].actualDepartureTime);
                    string gate = RetrieveGate(rst.flightno, qrst.segmentDetails[0].gate);
                    string estArriveTime = RetrieveEstimatedArriveTime(rst.flightno, qrst.segmentDetails[0].scheduledArrivalTime);
                    string schdArriveTime = RetrieveScheduledArriveTime(rst.flightno, qrst.segmentDetails[0].scheduledDepartureTime);
                    string actArriveTime = RetrieveActualArriveTime(rst.flightno, qrst.segmentDetails[0].actualArrivalTime);

                    rst.leaveplan = estDepTime.Replace('.', ':');
                    rst.leavetime = actDepTime.Replace('.', ':');
                    rst.arriveplan = estArriveTime.Replace('.', ':');
                    rst.arrivetime = actArriveTime.Replace('.', ':');
                    rst.estimatedleavetime = schdDepTime.Replace('.', ':');
                    rst.estimatedarrivetime = schdArriveTime.Replace('.', ':');
                    rst.gate = gate.Replace('.', ':');

                    OnFinishScratchARow(i, rst);
                }
                catch (System.Exception ex)
                {
                	
                }
                i++;
            }
            OnFinishScratchAllPage();
        }

        public string RequestFirstPage(string flightno)
        {
            string url = String.Format("http://comp.umetrip.com/umeweb/flyStatusDetail.html?flightNo={0}&date={1}", flightno, String.Format("{0:yyyy-MM-dd}",queryDate) );

            // creates the post data for the POST request

            WebRequest request = WebRequest.Create(url);
            ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.110 Safari/537.36 CoolNovo/2.0.9.20";

            // Set the Method property of the request to POST.
            request.Method = "GET";
            ((HttpWebRequest)request).KeepAlive = true;
            ((HttpWebRequest)request).UnsafeAuthenticatedConnectionSharing = true;
            ((HttpWebRequest)request).AllowAutoRedirect = true;
            //request.ContentType = "application/x-www-form-urlencoded";

            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            //request.ContentLength = byteArray.Length;
            //Stream dataStream = request.GetRequestStream();
            //dataStream.Write(byteArray, 0, byteArray.Length);
            //dataStream.Close();

            //  This actually does the request and gets the response back
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

            string responseData = string.Empty;

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
                    mfirstCookieStr = tmpCookie[0];// resp.Headers["Set-cookie"].ToString();
                }
            }

            return responseData;
        }

        public string RequestSecondPage(string flightno)
        {
            string refererurl = String.Format("http://comp.umetrip.com/umeweb/flyStatusDetail.html?flightNo={0}&date={1}", flightno, String.Format("{0:yyyy-MM-dd}",queryDate) );
            string url = String.Format("http://comp.umetrip.com/umeweb/fs/s.do?str={0}~{1}", flightno, String.Format("{0:yyyy-MM-dd}", queryDate));
            string responseData = string.Empty;

            // creates the post data for the POST request

            try
            {
                WebRequest request = WebRequest.Create(url);
                ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.110 Safari/537.36 CoolNovo/2.0.9.20";

                // Set the Method property of the request to POST.
                request.Method = "POST";
                ((HttpWebRequest)request).KeepAlive = true;
                ((HttpWebRequest)request).UnsafeAuthenticatedConnectionSharing = true;
                ((HttpWebRequest)request).AllowAutoRedirect = true;
                ((HttpWebRequest)request).Referer = refererurl;

                ((HttpWebRequest)request).Accept = "application/json, text/javascript";
                //             request.Headers[HttpRequestHeader.AcceptLanguage] = "en-US,en;q=0.8";
                //             request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip,deflate,sdch";

                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("Cookie", mfirstCookieStr);
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                request.Headers.Add("Cache-Control", "no-cache");
                request.Headers.Add("Pragma", "no-cache");
                string postData = "";
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
                    mCurrHeader = resp.Headers;
                    mCurrCookie = resp.Cookies;
                    //mCurrCookieContainer.Add(resp.Cookies);
                    if (resp.Headers["Set-cookie"] != null)
                    {
                        var tmpCookie = resp.Headers["Set-cookie"].ToString().Split(';');
                        mSecondCookieStr = resp.Headers["Set-cookie"].ToString();
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }

            return responseData;
        }

        public string RetrieveEstimatedDepartureTime(string flightno, string departuretime)
        {
            string refererurl = String.Format("http://comp.umetrip.com/umeweb/flyStatusDetail.html?flightNo={0}&date={1}", flightno, String.Format("{0:yyyy-MM-dd}", queryDate));
            string url = String.Format("http://comp.umetrip.com/umeweb/graphic.do?str={0}&width=70&height=20&front=142,142,142&back=255,255,255&xpos=10&ypos=20&size=16", departuretime);
            string responseData = string.Empty;
            string engpath = Program.PROGRAM_PATH + "\\tessdata";

            try
            {
                WebRequest request = WebRequest.Create(url);
                ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.110 Safari/537.36 CoolNovo/2.0.9.20";

                // Set the Method property of the request to POST.
                request.Method = "GET";

                //쿠키정보를 추가한다.
                request.Headers.Add("Cookie", mfirstCookieStr + ";" + mSecondCookieStr);
                //request.Headers.Add("Cookie", mSecondCookieStr);

                ((HttpWebRequest)request).KeepAlive = true;
                ((HttpWebRequest)request).UnsafeAuthenticatedConnectionSharing = true;
                ((HttpWebRequest)request).AllowAutoRedirect = true;
                ((HttpWebRequest)request).Referer = refererurl;

                //  This actually does the request and gets the response back
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                using (Image image = Image.FromStream(resp.GetResponseStream()))
                {
                    responseData = OCREngine.GetOCRText(engpath, image);
                }
            }
            catch (System.Exception ex)
            {
                ScratchModel.WriteLogFile("----ERR----", "RetrieveEstimatedDepartureTime", ex.ToString());
            }

            return responseData;
        }

        public string RetrieveScheduledDepartureTime(string flightno, string departuretime)
        {
            string refererurl = String.Format("http://comp.umetrip.com/umeweb/flyStatusDetail.html?flightNo={0}&date={1}", flightno, String.Format("{0:yyyy-MM-dd}", queryDate));
            //string url = String.Format("http://comp.umetrip.com/umeweb/graphic.do?str={0}&width=70&height=20&front=142,142,142&back=255,255,255&xpos=10&ypos=20&size=16", departuretime);
            string url = String.Format("http://comp.umetrip.com/umeweb/graphic.do?str={0}&width=70&height=20&front=75,180,0&back=255,255,255&xpos=2&ypos=20&size=20", departuretime);
        
            string responseData = string.Empty;
            string engpath = Program.PROGRAM_PATH + "\\tessdata";

            try
            {
                WebRequest request = WebRequest.Create(url);
                ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.110 Safari/537.36 CoolNovo/2.0.9.20";

                // Set the Method property of the request to POST.
                request.Method = "GET";

                //쿠키정보를 추가한다.
                request.Headers.Add("Cookie", mfirstCookieStr + ";" + mSecondCookieStr);
                //request.Headers.Add("Cookie", mSecondCookieStr);

                ((HttpWebRequest)request).KeepAlive = true;
                ((HttpWebRequest)request).UnsafeAuthenticatedConnectionSharing = true;
                ((HttpWebRequest)request).AllowAutoRedirect = true;
                ((HttpWebRequest)request).Referer = refererurl;

                //  This actually does the request and gets the response back
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                using (Image image = Image.FromStream(resp.GetResponseStream()))
                {
                    responseData = OCREngine.GetOCRText(engpath, image);
                }
            }
            catch (System.Exception ex)
            {
            	
            }

            return responseData;
        }

        public string RetrieveActualDepartureTime(string flightno, string departuretime)
        {
            string refererurl = String.Format("http://comp.umetrip.com/umeweb/flyStatusDetail.html?flightNo={0}&date={1}", flightno, String.Format("{0:yyyy-MM-dd}", queryDate));
            string url = String.Format("http://comp.umetrip.com/umeweb/graphic.do?str={0}&width=70&height=20&front=75,180,0&back=255,255,255&xpos=2&ypos=20&size=20", departuretime);
            string responseData = string.Empty;
            string engpath = Program.PROGRAM_PATH + "\\tessdata";

            try
            {
                WebRequest request = WebRequest.Create(url);
                ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.110 Safari/537.36 CoolNovo/2.0.9.20";

                // Set the Method property of the request to POST.
                request.Method = "GET";

                //쿠키정보를 추가한다.
                request.Headers.Add("Cookie", mfirstCookieStr + ";" + mSecondCookieStr);
                //request.Headers.Add("Cookie", mSecondCookieStr);

                ((HttpWebRequest)request).KeepAlive = true;
                ((HttpWebRequest)request).UnsafeAuthenticatedConnectionSharing = true;
                ((HttpWebRequest)request).AllowAutoRedirect = true;
                ((HttpWebRequest)request).Referer = refererurl;

                //  This actually does the request and gets the response back
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                using (Image image = Image.FromStream(resp.GetResponseStream()))
                {
                    responseData = OCREngine.GetOCRText(engpath, image);
                }
            }
            catch (System.Exception ex)
            {
                ScratchModel.WriteLogFile("HBQueryModel", "RetrieveActualDepartureTime", ex.ToString());
            }

            return responseData;
        }

        public string RetrieveGate(string flightno, string gate)
        {
            string refererurl = String.Format("http://comp.umetrip.com/umeweb/flyStatusDetail.html?flightNo={0}&date={1}", flightno, String.Format("{0:yyyy-MM-dd}", queryDate));
            string url = String.Format("http://comp.umetrip.com/umeweb/graphic.do?str={0}&width=70&height=20&front=142,142,142&back=255,255,255&xpos=0&ypos=20&size=20", gate);
            string responseData = string.Empty;
            string engpath = Program.PROGRAM_PATH + "\\tessdata";

            try
            {
                WebRequest request = WebRequest.Create(url);
                ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.110 Safari/537.36 CoolNovo/2.0.9.20";

                // Set the Method property of the request to POST.
                request.Method = "GET";

                //쿠키정보를 추가한다.
                request.Headers.Add("Cookie", mfirstCookieStr + ";" + mSecondCookieStr);
                //request.Headers.Add("Cookie", mSecondCookieStr);

                ((HttpWebRequest)request).KeepAlive = true;
                ((HttpWebRequest)request).UnsafeAuthenticatedConnectionSharing = true;
                ((HttpWebRequest)request).AllowAutoRedirect = true;
                ((HttpWebRequest)request).Referer = refererurl;

                //  This actually does the request and gets the response back
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                using (Image image = Image.FromStream(resp.GetResponseStream()))
                {
                    responseData = OCREngine.GetOCRText(engpath, image);
                }
            }
            catch (System.Exception ex)
            {
                ScratchModel.WriteLogFile("HBQueryModel", "RetrieveGate", ex.ToString());            	
            }

            return responseData;
        }

        public string RetrieveEstimatedArriveTime(string flightno, string schdarrtime)
        {
            string refererurl = String.Format("http://comp.umetrip.com/umeweb/flyStatusDetail.html?flightNo={0}&date={1}", flightno, String.Format("{0:yyyy-MM-dd}", queryDate));
            string url = String.Format("http://comp.umetrip.com/umeweb/graphic.do?str={0}&width=70&height=20&front=142,142,142&back=255,255,255&xpos=10&ypos=20&size=16", schdarrtime);
            string responseData = string.Empty;
            string engpath = Program.PROGRAM_PATH + "\\tessdata";

            try
            {
                WebRequest request = WebRequest.Create(url);
                ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.110 Safari/537.36 CoolNovo/2.0.9.20";

                // Set the Method property of the request to POST.
                request.Method = "GET";

                //쿠키정보를 추가한다.
                request.Headers.Add("Cookie", mfirstCookieStr + ";" + mSecondCookieStr);
                //request.Headers.Add("Cookie", mSecondCookieStr);

                ((HttpWebRequest)request).KeepAlive = true;
                ((HttpWebRequest)request).UnsafeAuthenticatedConnectionSharing = true;
                ((HttpWebRequest)request).AllowAutoRedirect = true;
                ((HttpWebRequest)request).Referer = refererurl;

                //  This actually does the request and gets the response back
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                using (Image image = Image.FromStream(resp.GetResponseStream()))
                {
                    responseData = OCREngine.GetOCRText(engpath, image);
                }
            }
            catch (System.Exception ex)
            {
                ScratchModel.WriteLogFile("HBQueryModel", "RetrieveEstimatedArriveTime", ex.ToString());
            }

            return responseData;
        }

        public string RetrieveActualArriveTime(string flightno, string actarrtime)
        {
            string refererurl = String.Format("http://comp.umetrip.com/umeweb/flyStatusDetail.html?flightNo={0}&date={1}", flightno, String.Format("{0:yyyy-MM-dd}", queryDate));
            string url = String.Format("http://comp.umetrip.com/umeweb/graphic.do?str={0}&width=70&height=20&front=75,180,0&back=255,255,255&xpos=2&ypos=20&size=20", actarrtime);
            string responseData = string.Empty;
            string engpath = Program.PROGRAM_PATH + "\\tessdata";

            try
            {
                WebRequest request = WebRequest.Create(url);
                ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.110 Safari/537.36 CoolNovo/2.0.9.20";

                // Set the Method property of the request to POST.
                request.Method = "GET";

                //쿠키정보를 추가한다.
                request.Headers.Add("Cookie", mfirstCookieStr + ";" + mSecondCookieStr);
                //request.Headers.Add("Cookie", mSecondCookieStr);

                ((HttpWebRequest)request).KeepAlive = true;
                ((HttpWebRequest)request).UnsafeAuthenticatedConnectionSharing = true;
                ((HttpWebRequest)request).AllowAutoRedirect = true;
                ((HttpWebRequest)request).Referer = refererurl;

                //  This actually does the request and gets the response back
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                using (Image image = Image.FromStream(resp.GetResponseStream()))
                {
                    responseData = OCREngine.GetOCRText(engpath, image);
                }
            }
            catch (System.Exception ex)
            {
                ScratchModel.WriteLogFile("HBQueryModel", "RetrieveActualArriveTime", ex.ToString());            	
            }

            return responseData;
        }

        public string RetrieveScheduledArriveTime(string flightno, string actarrtime)
        {
            string refererurl = String.Format("http://comp.umetrip.com/umeweb/flyStatusDetail.html?flightNo={0}&date={1}", flightno, String.Format("{0:yyyy-MM-dd}", queryDate));
            string url = String.Format("http://comp.umetrip.com/umeweb/graphic.do?str={0}&width=70&height=20&front=75,180,0&back=255,255,255&xpos=2&ypos=20&size=20", actarrtime);
            string responseData = string.Empty;
            string engpath = Program.PROGRAM_PATH + "\\tessdata";

            try
            {
                WebRequest request = WebRequest.Create(url);
                ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.110 Safari/537.36 CoolNovo/2.0.9.20";

                // Set the Method property of the request to POST.
                request.Method = "GET";

                //쿠키정보를 추가한다.
                request.Headers.Add("Cookie", mfirstCookieStr + ";" + mSecondCookieStr);
                //request.Headers.Add("Cookie", mSecondCookieStr);

                ((HttpWebRequest)request).KeepAlive = true;
                ((HttpWebRequest)request).UnsafeAuthenticatedConnectionSharing = true;
                ((HttpWebRequest)request).AllowAutoRedirect = true;
                ((HttpWebRequest)request).Referer = refererurl;

                //  This actually does the request and gets the response back
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                using (Image image = Image.FromStream(resp.GetResponseStream()))
                {
                    responseData = OCREngine.GetOCRText(engpath, image);
                }
            }
            catch (System.Exception ex)
            {
                ScratchModel.WriteLogFile("HBQueryModel", "RetrieveScheduledArriveTime", ex.ToString());            	
            }

            return responseData;
        }

    }
}
