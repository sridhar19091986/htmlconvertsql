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
            using (SoccerScoreSqlite match = new SoccerScoreSqlite(Conn.cnn))
            {
                var ll = match.LiveAibo.Where(e => e.LiveAiboID> 0);
                match.LiveAibo.DeleteAllOnSubmit(ll);//更新后删除
                match.SubmitChanges();

                foreach (DataRow aa in dt.Rows)
                {
                    if (HtmlTextToStr(aa[3].ToString()) != null) // && HtmlTextToStr(aa[11].ToString()) == null
                    {
                        LiveAibo lo = new LiveAibo();
                        lo.Value = HtmlTextToStr(aa[1].ToString());
                        lo.LeagueName = HtmlTextToStr(aa[2].ToString());
                        lo.MatchTime = HtmlTextToStr(aa[3].ToString());
                        lo.MatchOrder1hOmeName = AiboTeamName(aa[5].ToString());
                        lo.MatchOrder1hAndicapNumber= HtmlTextToStr(aa[6].ToString());
                        lo.MatchOrder1aWayName = AiboTeamName(aa[7].ToString());

                        match.LiveAibo.InsertOnSubmit(lo);
                       
                    }
                }
                match.SubmitChanges();
                return match.LiveAibo.Select(e => e.LiveAiboID).Max();
            }
        }
    }
}
