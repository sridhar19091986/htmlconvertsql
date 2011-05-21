using System.Linq;
using System.Data;
using SoccerScore.Compact.Linq;


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
            using (DataClassesMatchDataContext match = new DataClassesMatchDataContext(Conn.conn))
            {
                var ll = match.Live_Aibo.Where(e => e.Live_Aibo_id > 0);
                match.Live_Aibo.DeleteAllOnSubmit(ll);//更新后删除
                match.SubmitChanges();

                foreach (DataRow aa in dt.Rows)
                {
                    if (HtmlTextToStr(aa[3].ToString()) != null) // && HtmlTextToStr(aa[11].ToString()) == null
                    {
                        Live_Aibo lo = new Live_Aibo();
                        lo.KeyValue = HtmlTextToStr(aa[1].ToString());
                        lo.LeagueName = HtmlTextToStr(aa[2].ToString());
                        lo.Match_time = HtmlTextToStr(aa[3].ToString());
                        lo.MatchOrder1_HomeName = AiboTeamName(aa[5].ToString());
                        lo.MatchOrder1_HandicapNumber = HtmlTextToStr(aa[6].ToString());
                        lo.MatchOrder1_AwayName = AiboTeamName(aa[7].ToString());

                        match.Live_Aibo.InsertOnSubmit(lo);
                       
                    }
                }
                match.SubmitChanges();
                return match.Live_Aibo.Max(e => e.Live_Aibo_id);
            }
        }
    }
}
