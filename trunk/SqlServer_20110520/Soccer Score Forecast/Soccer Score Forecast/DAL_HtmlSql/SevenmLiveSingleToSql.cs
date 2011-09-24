using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SoccerScore.Compact.Linq;
using System.Windows.Forms;

namespace Soccer_Score_Forecast
{
    public class SevenmLiveSingleToSql : ElementParserFunction
    {
        private string status = null;
        public SevenmLiveSingleToSql() { }
        public int InsertLiveHtmlTableToDB(string _html)
        {
            SqlAgilityPackTableXpath tbTag = new SqlAgilityPackTableXpath("live.win.7m.cn");
            HtmlAgilityPackGeneric SevenmLivesg = new HtmlAgilityPackGeneric(_html, tbTag.tbTag, 0);//容易出错，主要路径的转换
            DataTable dt = SevenmLivesg.GetTableOutHtml(); //容易出错，注意方法的转换
            DataClassesMatchDataContext match = new DataClassesMatchDataContext(Conn.conn);
            //if (!Conn.CreateTable(typeof(Live_Single))) { return 0; }
            foreach (DataRow aa in dt.Rows)
            {
                if (aa[8] != null)
                    if (aa[8].ToString() != "")
                        if (aa[8].ToString().Trim() != "")
                        {
                            status = HtmlTextToStr(aa[8].ToString()).Trim();

                            //北京单场
                            if (status != "让球")
                            {
                                if (status.Length >= 1 && status.Length <= 2)
                                {
                                    Live_Single ls = new Live_Single();
                                    ls.Html_position = GetNumber(HtmlTextToStr(aa[0].ToString()));
                                    ls.Home_team_big = GetNumber(HtmlHrefToStr(aa[4].ToString()));
                                    ls.Away_team_big = GetNumber(HtmlHrefToStr(aa[6].ToString()));
                                    ls.Status = status;
                                    match.Live_Single.InsertOnSubmit(ls);
                                }
                            }

                            //竞猜
                            if (status.IndexOf(".") != -1)
                            {
                                Live_Single ls = new Live_Single();
                                ls.Html_position = GetNumber(HtmlTextToStr(aa[0].ToString()));
                                ls.Home_team_big = GetNumber(HtmlHrefToStr(aa[4].ToString()));
                                ls.Away_team_big = GetNumber(HtmlHrefToStr(aa[6].ToString()));
                                ls.Status = status;
                                match.Live_Single.InsertOnSubmit(ls);
                            }
                        }
            }
            match.SubmitChanges();
            return match.Live_Single.Max(e => e.Live_Single_id);
        }
    }
}
