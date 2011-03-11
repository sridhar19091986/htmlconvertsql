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
    class AiboLiveToSql : ElementParserFunction
    {
        private HtmlAgilityPackGeneric AiboHtml;
        //public AiboHtmlToSql(string _html)
         public AiboLiveToSql (string _html)
        {
            SqlAgilityPackTableXpath tbTag = new SqlAgilityPackTableXpath("live.aibo123.com");
            AiboHtml = new HtmlAgilityPackGeneric(_html, tbTag.tbTag, 0);
            //AiboHtml = new HtmlAgilityPackGeneric(_html, "//table[@id='TableBorder']", 0);
        }
        public decimal updateLiveAibo()
        {
            DataTable dt = AiboHtml.GetTableOutHtml();
            using (DataClassesMatchDataContext match = new DataClassesMatchDataContext())
            {
                var ll = match.live_Aibo.Where(e => e.live_Aibo_id > 0);
                match.live_Aibo.DeleteAllOnSubmit(ll);//更新后删除
                match.SubmitChanges();

                foreach (DataRow aa in dt.Rows)
                {
                    if (HtmlTextToStr(aa[3].ToString()) != null) // && HtmlTextToStr(aa[11].ToString()) == null
                    {
                        live_Aibo lo = new live_Aibo();
                        lo.value = HtmlTextToStr(aa[1].ToString());
                        lo.LeagueName = HtmlTextToStr(aa[2].ToString());
                        lo.match_time = HtmlTextToStr(aa[3].ToString());
                        lo.MatchOrder1_HomeName = AiboTeamName(aa[5].ToString());
                        lo.MatchOrder1_HandicapNumber = HtmlTextToStr(aa[6].ToString());
                        lo.MatchOrder1_AwayName = AiboTeamName(aa[7].ToString());

                        match.live_Aibo.InsertOnSubmit(lo);
                       
                    }
                }
                match.SubmitChanges();
                return match.live_Aibo.Max(e => e.live_Aibo_id);
            }
        }
    }
}
