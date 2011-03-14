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
    public class SevenmLiveToSql : ElementParserFunction
    {
        private HtmlAgilityPackGeneric SevenmLive;
        public SevenmLiveToSql(string _html)
        {
            SqlAgilityPackTableXpath tbTag = new SqlAgilityPackTableXpath("live2.7m.cn");
            SevenmLive = new HtmlAgilityPackGeneric(_html, tbTag.tbTag, 0);
            //SevenmLive = new HtmlAgilityPackGeneric(_html, "//table[@id='live_Table']", 0); 
        }

        public decimal InsertLiveHtmlTableToDB()
        {
            DataTable dt = SevenmLive.GetHtmlTable();

            SoccerScoreCompact match = new SoccerScoreCompact(Conn.cnn);

            var result = match.live_Table.Where(r => r.live_table_id > 0);
            match.live_Table.DeleteAllOnSubmit(result);
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
                    live_Table lt = new live_Table();
                    lt.html_position = aa[0].ToString();
                    lt.match_type = HtmlTextToStr(aa[2].ToString());
                    lt.s_time = HtmlTextToStr(aa[3].ToString());
                    lt.home_team = HtmlTextToStr(aa[5].ToString());
                    lt.full_time_score = HtmlTextToStr(aa[6].ToString());
                    lt.away_team = HtmlTextToStr(aa[7].ToString());
                    lt.half_time_score = HtmlTextToStr(aa[8].ToString());
                    lt.s_date = ddate;
                    lt.home_team_big = HtmlHrefToStr(aa[5].ToString());
                    lt.away_team_big = HtmlHrefToStr(aa[7].ToString());

                    match.live_Table.InsertOnSubmit(lt);
                    match.SubmitChanges();
                    
                }
            }
            
            return match.live_Table.Select(e => e.live_table_id).Max();
        }

        //设置一个空的构造函数，调用后面的方法
        public SevenmLiveToSql()
        {
            
        }

        private string temp_date = null;
        public void UpdateTodayMatch()
        {
            SoccerScoreCompact matches = new SoccerScoreCompact(Conn.cnn);
            var lt = matches.live_Table.OrderBy(o => o.s_date).ThenBy(p => p.s_time);//用lambda表达式简洁

            foreach (var m in lt)
            {
                if (m.home_team_big != null)
                {
                    //一一对应生成
                    live_Table_lib ltl = new live_Table_lib();
                    ltl.html_position = Int32.Parse(m.html_position);
                    ltl.home_team_big = Int32.Parse(GetNumber(m.home_team_big));
                    ltl.away_team_big = Int32.Parse(GetNumber(m.away_team_big));
                    ltl.match_type = m.match_type.Trim();
                    if (m.s_date.IndexOf("-") != -1)
                        temp_date = m.s_date.Substring(0, 10);
                    ltl.match_time = DateTime.Parse(temp_date + " " + m.s_time);
                    ltl.status = m.full_time_score.Replace("&nbsp;", "").Trim();
                    ltl.home_team = m.home_team.Trim();
                    ltl.away_team = m.away_team.Trim();
                    ltl.home_red_card = StringCount(m.home_team, "&nbsp;", 0);
                    ltl.away_red_card = StringCount(m.away_team, "&nbsp;", 0);
                    ltl.full_home_goals = null;
                    ltl.full_away_goals = null;
                    if (m.half_time_score.IndexOf("-") > 0)
                    {
                        ltl.half_home_goals = Int32.Parse(m.half_time_score.Substring(0, m.half_time_score.IndexOf("-")));
                        ltl.half_away_goals = Int32.Parse(m.half_time_score.Substring(m.half_time_score.IndexOf("-") + 1, m.half_time_score.Length - m.half_time_score.IndexOf("-") - 1));
                    }

                    var rtExist = matches.live_Table_lib.Where(p => p.home_team_big == ltl.home_team_big && p.away_team_big == ltl.away_team_big);
                    //let关键字，匿名类型
                    var rtUpdateExist = from p in rtExist
                                        let timeDiff = ltl.match_time.Value - p.match_time.Value
                                        where timeDiff.Days <= 1
                                        where timeDiff.Days >= -1
                                        select p;

                    //存在记录的则做更新，必须确认是最新数据，即时间差不超过1天
                    if (rtUpdateExist.Any())
                    {
                        var rtUpate = rtUpdateExist.First();
                        rtUpate.status = ltl.status;
                        rtUpate.home_team = ltl.home_team;
                        rtUpate.away_team = ltl.away_team;
                        rtUpate.home_red_card = ltl.home_red_card;
                        rtUpate.away_red_card = ltl.away_red_card;
                        rtUpate.half_home_goals = ltl.half_home_goals;
                        rtUpate.half_away_goals = ltl.half_away_goals;
                        //matches.SubmitChanges();
                    }
                    //不存在记录的此处做插入
                    else
                    {
                        matches.live_Table_lib.InsertOnSubmit(ltl);
                        matches.SubmitChanges();
                    }
                }
            }

            matches.live_Table.DeleteAllOnSubmit(lt);//更新后删除
            matches.SubmitChanges();
            MessageBox.Show("OK");
            //dataGridView1.DataSource = matches.live_Table_lib;
        }

    }
}
