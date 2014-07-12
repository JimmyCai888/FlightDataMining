using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace HangBan
{
    class ParseEngine
    {
        public static int GetPageCount(string respData)
        {
            int pagecnt = -1;

            try
            {
                Dictionary<string, string> retlist = new Dictionary<string, string>();
                List<HtmlAttribute> formAttributes = new List<HtmlAttribute>();
                HtmlNode.ElementsFlags.Remove("form");

                HtmlAgilityPack.HtmlDocument htdoc = new HtmlAgilityPack.HtmlDocument();
                htdoc.OptionOutputAsXml = true;
                htdoc.OptionAutoCloseOnEnd = true;
                htdoc.LoadHtml(respData);

                var forms = htdoc.DocumentNode.Descendants("form");

                HtmlNode form = htdoc.DocumentNode.SelectSingleNode(".//form");

                HtmlNodeCollection countnodes = form.SelectNodes(".//p");
                foreach (HtmlNode node in countnodes)
                {
                    if (node.InnerHtml.Contains("共计"))
                    {
                        HtmlNode totnumnode = node.SelectSingleNode(".//b");
                        string strcnt = totnumnode.InnerHtml;

                        pagecnt = int.Parse(strcnt);
                        return pagecnt;
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }

            return pagecnt;
        }

        public static List<HangbanInfo> ParseHBTable(string respData)
        {
            List<HangbanInfo> rst = new List<HangbanInfo>();

            try
            {
                Dictionary<string, string> retlist = new Dictionary<string, string>();
                List<HtmlAttribute> formAttributes = new List<HtmlAttribute>();

                HtmlAgilityPack.HtmlDocument htdoc = new HtmlAgilityPack.HtmlDocument();
                htdoc.OptionOutputAsXml = true;
                htdoc.OptionAutoCloseOnEnd = true;
                htdoc.LoadHtml(respData);

                var forms = htdoc.DocumentNode.Descendants("form");

                HtmlNode form = htdoc.DocumentNode.SelectSingleNode(".//table");

                HtmlNodeCollection trlist = form.SelectNodes(".//tr");
                foreach (HtmlNode trnode in trlist)
                {
                    HtmlNodeCollection tdlist = trnode.SelectNodes(".//td");

                    if (tdlist == null || (tdlist != null && tdlist.Count() != 8))
                    {
                        continue;
                    }

                    if (tdlist.ElementAt(1).InnerHtml.Contains("marquee"))
                    {
                        HtmlNode marque = tdlist.ElementAt(1).SelectSingleNode(".//marquee");
                        string[] mflist = marque.InnerHtml.Trim().Split(' ');

                        if (mflist.Count() > 0)
                        {
                            foreach (var item in mflist)
                            {
                                HangbanInfo newitem = new HangbanInfo();

                                newitem.leaveplan = tdlist.ElementAt(0).InnerHtml;
                                newitem.flightno = item;
                                newitem.airlines = tdlist.ElementAt(2).InnerHtml;
                                newitem.terminal = tdlist.ElementAt(3).InnerHtml;
                                newitem.destination = tdlist.ElementAt(4).InnerHtml;

                                HtmlNode info5 = tdlist.ElementAt(5).SelectSingleNode(".//marquee");

                                if (info5 != null) {
                                    newitem.chkincounter = info5.InnerHtml.Trim();

                                } else {
                                    newitem.chkincounter = tdlist.ElementAt(5).InnerHtml;
                                }

                                newitem.bordinggate = tdlist.ElementAt(6).InnerHtml;
                                newitem.status = tdlist.ElementAt(7).InnerHtml;
                                rst.Add(newitem);
                            }
                            continue;
                        }
                    }
                    else
                    {
                        HangbanInfo newitem = new HangbanInfo();

                        newitem.leaveplan = tdlist.ElementAt(0).InnerHtml;
                        newitem.flightno = tdlist.ElementAt(1).InnerHtml;
                        newitem.airlines = tdlist.ElementAt(2).InnerHtml;
                        newitem.terminal = tdlist.ElementAt(3).InnerHtml;
                        newitem.destination = tdlist.ElementAt(4).InnerHtml;

                        HtmlNode info5 = tdlist.ElementAt(5).SelectSingleNode(".//marquee");

                        if (info5 != null)
                        {
                            newitem.chkincounter = info5.InnerHtml.Trim();

                        }
                        else
                        {
                            newitem.chkincounter = tdlist.ElementAt(5).InnerHtml;
                        }

                        newitem.bordinggate = tdlist.ElementAt(6).InnerHtml;
                        newitem.status = tdlist.ElementAt(7).InnerHtml;
                        rst.Add(newitem);
                    }
                }
            }
            catch (System.Exception ex)
            {

            }

            return rst;
        }

        public static List<HBQueryInfo> ParseQueryInfo(string respData)
        {
            List<HBQueryInfo> rst = new List<HBQueryInfo>();

            return rst;
        }

    }
}
