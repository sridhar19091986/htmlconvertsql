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
    public class SevenmResultToSql : ElementParserFunction
    {
        private HtmlAgilityPackGeneric SevenmResult;
        public SevenmResultToSql(string _html)
        {
            SqlAgilityPackTableXpath tbTag = new SqlAgilityPackTableXpath("data2.7m.cn");
            SevenmResult = new HtmlAgilityPackGeneric(_html, tbTag.tbTag, 1);
            //SevenmResult = new HtmlAgilityPackGeneric(_html, "//table[@id='result_tb']", 1);
        }
        public decimal InsertLastHtmlTableToDB()
        {
            DataTable dt = SevenmResult.GetHtmlTable();

            //DataClassesMatchDataContext match = new DataClassesMatchDataContext();
            SoccerScoreSqlite match = new SoccerScoreSqlite(Conn.cnn);

            foreach (DataRow aa in dt.Rows)
            {
                if (HtmlTextToStr(aa[3].ToString()) != null)
                {
                    ResultTB rt = new ResultTB();
                    rt.HTMLPosition = aa[0].ToString();
                  
                    rt.MatchType = HtmlTextToStr(aa[1].ToString());
                   
                   rt.STime= HtmlTextToStr(aa[2].ToString());
                   
                rt.HomeTeam = HtmlTextToStr(aa[3].ToString());
                    
                  rt.FullTimeScore = HtmlTextToStr(aa[4].ToString());
                    
                 rt.AwayTeam = HtmlTextToStr(aa[5].ToString());
                    
                  rt.HalfTimeScore = HtmlTextToStr(aa[6].ToString());
                   
                  rt.Odds = HtmlTextToStr(aa[7].ToString());
                    
                   rt.WinLossBig = HtmlTextToStr(aa[8].ToString());
                    
                    rt.SDate = HtmlDateToStrResult(aa[2].ToString());
                    
                   rt.HomeTeamBig = HtmlHrefToStr(aa[3].ToString());
                   
                  rt.AwayTeamBig = HtmlHrefToStr(aa[5].ToString());
                   

                    match.ResultTB.InsertOnSubmit(rt);
                    match.SubmitChanges();
                    
                }
            }
           
            return match.ResultTB.Select(e => e.ResultTBID).Max();
        }
        //设置一个空的构造函数，调用后面的方法
        public SevenmResultToSql()
        {
     
        }
        private string temp_time = null;
        private int last_line = 0;
        public  void UpdateLastMatch()
        {
            int i = 0;
           // DataClassesMatchDataContext matches = new DataClassesMatchDataContext();
            SoccerScoreSqlite matches = new SoccerScoreSqlite(Conn.cnn);
            var rt = matches.ResultTB.OrderBy(o => o.SDate).ThenBy(p => p.STime);//用lambda表达式简洁

            //取临时变量监视
            DateTime lib_max_match_time = matches.ResultTBLib.Select(p => p.MatchTime).Max().Value.AddDays(-2);
            foreach (var m in rt)
            {
                i++;
                ProgressBarDelegate.DoSendPMessage(i);
                Application.DoEvents();
                if (m.HomeTeamBig != null)
                {
                    ResultTBLib rtl = new ResultTBLib();
                     rtl.HTMLPosition = Int32.Parse(m.HTMLPosition);
                   
                    rtl.HomeTeamBig= Int32.Parse(GetNumber(m.HomeTeamBig));
                    
                      rtl.AwayTeamBig = Int32.Parse(GetNumber(m.AwayTeamBig));
                  
                   rtl.MatchType= m.MatchType.Trim();
                    
                    last_line = m.STime.LastIndexOf("\n");
                    temp_time=m.STime.Substring(last_line,m.STime.Length-last_line-1);
                     rtl.MatchTime = DateTime.Parse(m.SDate.Substring(0, 10) + " " + temp_time);
                   
                   rtl.Odds= m.Odds.Trim();
                    
                   rtl.WinLossBig = m.WinLossBig.Trim();
                    
                      rtl.HomeTeam = m.HomeTeam.Trim();
                
                 rtl.AwayTeam= m.AwayTeam.Trim();
                    
                   rtl.HomeRedCard= StringCount(m.HomeTeam, "&nbsp;", 0);

                   rtl.AwayRedCard = StringCount(m.AwayTeam, "&nbsp;", 0);
                   
                    string bf = m.FullTimeScore.Replace("&nbsp;", "").Replace("&nbsp;", "");
                    if (m.FullTimeScore.IndexOf("-") > 0)
                    {
                        rtl.FullHomeGoals= Int32.Parse(bf.Substring(0, bf.IndexOf("-")));
                        rtl.FullAwayGoals= Int32.Parse(bf.Substring(bf.IndexOf("-") + 1, bf.Length - bf.IndexOf("-") - 1));
                    }
                    if (m.HalfTimeScore.IndexOf("-") > 0)
                    {
                          rtl.HalfHomeGoals= Int32.Parse(m.HalfTimeScore.Substring(0, m.HalfTimeScore.IndexOf("-")));

                          rtl.HalfAwayGoals = Int32.Parse(m.HalfTimeScore.Substring(m.HalfTimeScore.IndexOf("-") + 1, m.HalfTimeScore.Length - m.HalfTimeScore.IndexOf("-") - 1));
                   
                    }

                    //如果库中文件的日期太小，直接删除
                    if (rtl.MatchTime > lib_max_match_time)
                    {
                        //数据分区，层次化查询
                        var rtExist = from p in matches.ResultTBLib
                                      where p.MatchTime== rtl.MatchTime
                                      where p.HomeTeamBig== rtl.HomeTeamBig
                                      where p.AwayTeamBig== rtl.AwayTeamBig
                                      select p;

                        //库中没有记录直接插入
                        if (!rtExist.Any())
                        {
                            matches.ResultTBLib.InsertOnSubmit(rtl);
                            matches.SubmitChanges();
                        }
                    }

                    //更新后删除
                    matches.ResultTB.DeleteOnSubmit(m);
                    matches.SubmitChanges();
                    
                }
            }
            MessageBox.Show("OK");
            //dataGridView1.DataSource = matches.result_tb_lib;
        }
    }
}
