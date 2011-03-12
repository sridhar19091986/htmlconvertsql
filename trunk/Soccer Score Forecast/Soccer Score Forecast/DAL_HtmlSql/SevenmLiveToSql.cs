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
    public class SevenmLiveToSql : ElementParserFunction
    {
        private HtmlAgilityPackGeneric SevenmLive;
        public SevenmLiveToSql(string _html)
        {
            SqlAgilityPackTableXpath tbTag = new SqlAgilityPackTableXpath("live2.7m.cn");
            SevenmLive = new HtmlAgilityPackGeneric(_html, tbTag.tbTag, 0);
            //SevenmLive = new HtmlAgilityPackGeneric(_html, "//table[@id='LiveTable']", 0); 
        }

        public decimal InsertLiveHtmlTableToDB()
        {
            DataTable dt = SevenmLive.GetHtmlTable();

            //DataClassesMatchDataContext match = new DataClassesMatchDataContext();
            SoccerScoreSqlite match = new SoccerScoreSqlite(Conn.cnn);

            var result = match.LiveTable.Where(r => r.LiveTableID> 0);
            match.LiveTable.DeleteAllOnSubmit(result);
            match.SubmitChanges();

            string ddate = null;
            Queue<string> todayDate = new Queue<string>();

            foreach (DataRow aa in dt.Rows)
            {
                if (HtmlDateToStrLive(aa[1].ToString()) != null)
                {
                    if (HtmlDateToStrLive(aa[1].ToString()).IndexOf("最新賽果") == -1)
                    {
                        ddate = HtmlDateToStrLive(aa[1].ToString());
                        ddate = ddate.Replace("年", "-").Replace("月", "-").Replace("日", " ");
                        todayDate.Enqueue(ddate);
                    }
                    else
                    {
                        ddate = todayDate.Dequeue();
                    }
                }

                if (HtmlTextToStr(aa[3].ToString()) != null)
                {
                    LiveTable lt = new LiveTable();
                    lt.HTMLPosition = aa[0].ToString();
                   
                  lt.MatchType= HtmlTextToStr(aa[2].ToString());
                    
                  lt.STime = HtmlTextToStr(aa[3].ToString());
                  
                   lt.HomeTeam= HtmlTextToStr(aa[5].ToString());
                    
                    lt.FullTimeScore = HtmlTextToStr(aa[6].ToString());
                   
             lt.AwayTeam = HtmlTextToStr(aa[7].ToString());
                    
                   lt.HalfTimeScore = HtmlTextToStr(aa[8].ToString());
                   
                  lt.SDate = ddate;
                    
                        lt.HomeTeamBig = HtmlHrefToStr(aa[5].ToString());
                
                lt.AwayTeamBig = HtmlHrefToStr(aa[7].ToString());
                    

                    match.LiveTable.InsertOnSubmit(lt);
                    match.SubmitChanges();
                    
                }
            }
            
            return match.LiveTable.Select(e => e.LiveTableID).Max();
        }

        //设置一个空的构造函数，调用后面的方法
        public SevenmLiveToSql()
        {
            
        }

        private string temp_date = null;
        public void UpdateTodayMatch()
        {
            //DataClassesMatchDataContext matches = new DataClassesMatchDataContext();

            SoccerScoreSqlite matches = new SoccerScoreSqlite(Conn.cnn);

            var lt = matches.LiveTable.OrderBy(o => o.SDate).ThenBy(p => p.STime);//用lambda表达式简洁

            foreach (var m in lt)
            {
                if (m.HomeTeamBig!= null)
                {
                    //一一对应生成
                    LiveTableLib ltl = new LiveTableLib();
                    ltl.HTMLPosition= Int32.Parse(m.HTMLPosition);
                    ltl.HomeTeamBig = Int32.Parse(GetNumber(m.HomeTeamBig));
                    ltl.AwayTeamBig = Int32.Parse(GetNumber(m.AwayTeamBig));
                    ltl.MatchType = m.MatchType.Trim();
                    if (m.SDate.IndexOf("-") != -1)
                        temp_date = m.SDate.Substring(0, 10);
                    ltl.MatchTime = DateTime.Parse(temp_date + " " + m.STime);
                    ltl.Status= m.FullTimeScore.Replace("&nbsp;", "").Trim();
                    ltl.HomeTeam = m.HomeTeam.Trim();
                    ltl.AwayTeam= m.AwayTeam.Trim();
                    ltl.HomeRedCard = StringCount(m.HomeTeam, "&nbsp;", 0);
                    ltl.AwayRedCard= StringCount(m.AwayTeam, "&nbsp;", 0);
                    ltl.FullHomeGoals = null;
                    ltl.FullAwayGoals = null;
                    if (m.HalfTimeScore.IndexOf("-") > 0)
                    {
                        ltl.HalfHomeGoals = Int32.Parse(m.HalfTimeScore.Substring(0, m.HalfTimeScore.IndexOf("-")));
                        ltl.HalfAwayGoals = Int32.Parse(m.HalfTimeScore.Substring(m.HalfTimeScore.IndexOf("-") + 1, m.HalfTimeScore.Length - m.HalfTimeScore.IndexOf("-") - 1));
                    }

                    var rtExist = matches.LiveTableLib.Where(p => p.HomeTeamBig == ltl.HomeTeamBig && p.AwayTeamBig == ltl.AwayTeamBig);
                    //let关键字，匿名类型
                    var rtUpdateExist = from p in rtExist
                                        let timeDiff = ltl.MatchTime.Value - p.MatchTime.Value
                                        where timeDiff.Days <= 1
                                        where timeDiff.Days >= -1
                                        select p;

                    //存在记录的则做更新，必须确认是最新数据，即时间差不超过1天
                    if (rtUpdateExist.Any())
                    {
                        var rtUpate = rtUpdateExist.First();
                        rtUpate.Status = ltl.Status;
                        rtUpate.HomeTeam= ltl.HomeTeam;
                        rtUpate.AwayTeam = ltl.AwayTeam;
                        rtUpate.HomeRedCard = ltl.HomeRedCard;
                        rtUpate.AwayRedCard = ltl.AwayRedCard;
                        rtUpate.HalfHomeGoals = ltl.HalfHomeGoals;
                        rtUpate.HalfAwayGoals = ltl.HalfAwayGoals;
                        //matches.SubmitChanges();
                    }
                    //不存在记录的此处做插入
                    else
                    {
                        matches.LiveTableLib.InsertOnSubmit(ltl);
                        matches.SubmitChanges();
                    }
                }
            }

            matches.LiveTable.DeleteAllOnSubmit(lt);//更新后删除
            matches.SubmitChanges();
            MessageBox.Show("OK");
            //dataGridView1.DataSource = matches.LiveTable_lib;
        }

    }
}
