using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Linq.Expressions;
using System.ComponentModel;
using HtmlAgilityPack;
using SoccerScore.Compact.Linq;
using System.Windows.Forms;

namespace Soccer_Score_Forecast
{
    public class OkooHtmlToSql : ElementParserFunction
    {
        private HtmlAgilityPackGeneric OkooHtml;
        public OkooHtmlToSql(string _html)
        {
            SqlAgilityPackTableXpath tbTag = new SqlAgilityPackTableXpath("buy.okooo.com");
            OkooHtml = new HtmlAgilityPackGeneric(_html, tbTag.tbTag, 0);
            //OkooHtml = new HtmlAgilityPackGeneric(_html, "//table[@id='TableBorder']", 0);
        }
        public decimal updateLiveOkoo()
        {
            DataTable dt = OkooHtml.GetTableOutHtml();
            //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))
            //{
                var ll = Conn.match.Live_okoo.Where(e => e.Live_okoo_id > 0);
                Conn.match.Live_okoo.DeleteAllOnSubmit(ll);//更新后删除
                Conn.match.SubmitChanges();

                foreach (DataRow aa in dt.Rows)
                {
                    if (HtmlTextToStr(aa[15].ToString()) != null)
                    {
                        Live_okoo lo = new Live_okoo();
                        lo.Value = HtmlValueToInt(aa[0].ToString());
                        lo.LeagueName = HtmlTextToStr(aa[1].ToString());
                        lo.Match_time = HtmlTextToStr(aa[2].ToString());
                        lo.MatchOrder1_HomeName = TeamName(aa[3].ToString());
                        lo.MatchOrder1_HandicapNumber = HtmlTextToStr(aa[4].ToString());
                        lo.MatchOrder1_AwayName = TeamName(aa[5].ToString());
                        lo.Ok_1_0 = HtmlTextToStr(aa[6].ToString());
                        lo.Ok_1_1 = HtmlTextToStr(aa[7].ToString());
                        lo.Ok_1_2 = HtmlTextToStr(aa[8].ToString());
                        lo.MatchInfo = HtmlTextToStr(aa[11].ToString());
                        lo.Match_1_Win = HtmlTextToStr(aa[12].ToString());
                        lo.Match_1_Drawn = HtmlTextToStr(aa[13].ToString());
                        lo.Match_1_Lost = HtmlTextToStr(aa[14].ToString());

                        Conn.match.Live_okoo.InsertOnSubmit(lo);
                        Conn.match.SubmitChanges();
                    }
                }
                    return Conn.match.Live_okoo.Max(e => e.Live_okoo_id);
            //}
        }
    }
}
