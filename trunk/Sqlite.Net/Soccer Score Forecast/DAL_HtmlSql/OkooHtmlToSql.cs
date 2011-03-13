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
using Soccer_Score_Forecast.LinqSql;
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
            using (SoccerScoreSqlite match = new SoccerScoreSqlite(Conn.cnn))
            //using (DataClassesMatchDataContext match = new DataClassesMatchDataContext())
            {
                var ll = match.LiveOkOO.Where(e => e.LiveOkOOID > 0);
                match.LiveOkOO.DeleteAllOnSubmit(ll);//更新后删除
                match.SubmitChanges();

                foreach (DataRow aa in dt.Rows)
                {
                    if (HtmlTextToStr(aa[15].ToString()) != null)
                    {
                        LiveOkOO lo = new LiveOkOO();
                        lo.Value = HtmlValueToInt(aa[0].ToString());
                        lo.LeagueName = HtmlTextToStr(aa[1].ToString());
                        lo.MatchTime = HtmlTextToStr(aa[2].ToString());
                        lo.MatchOrder1hOmeName = TeamName(aa[3].ToString());
                        lo.MatchOrder1hAndicapNumber = HtmlTextToStr(aa[4].ToString());
                        lo.MatchOrder1aWayName = TeamName(aa[5].ToString());
                        lo.Ok10 = HtmlTextToStr(aa[6].ToString());
                        lo.Ok11 = HtmlTextToStr(aa[7].ToString());
                        lo.Ok12 = HtmlTextToStr(aa[8].ToString());
                        lo.MatchInfo = HtmlTextToStr(aa[11].ToString());
                        lo.Match1wIn = HtmlTextToStr(aa[12].ToString());
                        lo.Match1dRawn = HtmlTextToStr(aa[13].ToString());
                        lo.Match1lOst = HtmlTextToStr(aa[14].ToString());
                        match.LiveOkOO.InsertOnSubmit(lo);
                        match.SubmitChanges();
                    }
                }
                return match.LiveOkOO.Select(e => e.LiveOkOOID).Max();
            }
        }
    }
}
