﻿using System;
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
            //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))
            //{
                var ll = Conn.match.Live_Aibo.Where(e => e.Live_Aibo_id > 0);
                Conn.match.Live_Aibo.DeleteAllOnSubmit(ll);//更新后删除
                Conn.match.SubmitChanges();

                foreach (DataRow aa in dt.Rows)
                {
                    if (HtmlTextToStr(aa[3].ToString()) != null) // && HtmlTextToStr(aa[11].ToString()) == null
                    {
                        Live_Aibo lo = new Live_Aibo();
                        lo.Value = HtmlTextToStr(aa[1].ToString());
                        lo.LeagueName = HtmlTextToStr(aa[2].ToString());
                        lo.Match_time = HtmlTextToStr(aa[3].ToString());
                        lo.MatchOrder1_HomeName = AiboTeamName(aa[5].ToString());
                        lo.MatchOrder1_HandicapNumber = HtmlTextToStr(aa[6].ToString());
                        lo.MatchOrder1_AwayName = AiboTeamName(aa[7].ToString());

                        Conn.match.Live_Aibo.InsertOnSubmit(lo);
                       
                    }
                }
                Conn.match.SubmitChanges();
                return Conn.match.Live_Aibo.Max(e => e.Live_Aibo_id);
            //}
        }
    }
}
