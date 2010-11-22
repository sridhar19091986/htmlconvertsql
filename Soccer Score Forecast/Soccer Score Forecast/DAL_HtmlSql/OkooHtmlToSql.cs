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
            using (DataClassesMatchDataContext match = new DataClassesMatchDataContext())
            {
                var ll = match.live_okoo.Where(e => e.live_okoo_id > 0);
                match.live_okoo.DeleteAllOnSubmit(ll);//更新后删除
                match.SubmitChanges();

                foreach (DataRow aa in dt.Rows)
                {
                    if (HtmlTextToStr(aa[15].ToString()) != null)
                    {
                        live_okoo lo = new live_okoo();
                        lo.value = HtmlValueToInt(aa[0].ToString());
                        lo.LeagueName = HtmlTextToStr(aa[1].ToString());
                        lo.match_time = HtmlTextToStr(aa[2].ToString());
                        lo.MatchOrder1_HomeName = TeamName(aa[3].ToString());
                        lo.MatchOrder1_HandicapNumber = HtmlTextToStr(aa[4].ToString());
                        lo.MatchOrder1_AwayName = TeamName(aa[5].ToString());
                        lo.ok_1_0 = HtmlTextToStr(aa[6].ToString());
                        lo.ok_1_1 = HtmlTextToStr(aa[7].ToString());
                        lo.ok_1_2 = HtmlTextToStr(aa[8].ToString());
                        lo.MatchInfo = HtmlTextToStr(aa[11].ToString());
                        lo.Match_1_Win = HtmlTextToStr(aa[12].ToString());
                        lo.Match_1_Drawn = HtmlTextToStr(aa[13].ToString());
                        lo.Match_1_Lost = HtmlTextToStr(aa[14].ToString());

                        match.live_okoo.InsertOnSubmit(lo);
                        match.SubmitChanges();
                    }
                }
                return match.live_okoo.Max(e => e.live_okoo_id);
            }
        }
    }
}
