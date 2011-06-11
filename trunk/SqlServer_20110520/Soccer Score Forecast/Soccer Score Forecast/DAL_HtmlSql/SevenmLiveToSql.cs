using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
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
        public int InsertLiveHtmlTableToDB()
        {
            DataTable dt = SevenmLive.GetHtmlTable();

            DataClassesMatchDataContext match = new DataClassesMatchDataContext(Conn.conn);

            if (!Conn.CreateTable(typeof(Live_Table))) { return 0; }
            //var result = match.Live_Table.Where(r => r.Live_table_id > 0);
            //match.Live_Table.DeleteAllOnSubmit(result);
            //match.SubmitChanges();

            string ddate = null;
            string ndate = null; //修改当天完场的问题  2011.6.11
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
                        todayDate.Dequeue();//删除第1条  2011.6.11 
                        ndate = todayDate.Peek();//取第1个元素  2011.6.11
                        ddate = ndate;
                    }
                }

                if (HtmlTextToStr(aa[3].ToString()) != null)
                {
                    Live_Table lt = new Live_Table();
                    lt.Html_position = aa[0].ToString();
                    lt.Match_type = HtmlTextToStr(aa[2].ToString());
                    lt.S_time = HtmlTextToStr(aa[3].ToString());
                    lt.Home_team = HtmlTextToStr(aa[5].ToString());
                    lt.Full_time_score = HtmlTextToStr(aa[6].ToString());
                    lt.Away_team = HtmlTextToStr(aa[7].ToString());
                    lt.Half_time_score = HtmlTextToStr(aa[8].ToString());
                    lt.S_date = ddate;
                    lt.Home_team_big = HtmlHrefToStr(aa[5].ToString());
                    lt.Away_team_big = HtmlHrefToStr(aa[7].ToString());

                    match.Live_Table.InsertOnSubmit(lt);


                }
            }
            match.SubmitChanges();
            return match.Live_Table.Max(e => e.Live_table_id);
        }

        //设置一个空的构造函数，调用后面的方法
        public SevenmLiveToSql()
        {

        }

        private string temp_date = null;
        public void UpdateTodayMatch()
        {
            DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn);
            var lt = matches.Live_Table.OrderBy(o => o.S_date).ThenBy(p => p.S_time);//用lambda表达式简洁

            foreach (var m in lt)
            {
                if (m.Home_team_big != null)
                {
                    //一一对应生成
                    Live_Table_lib ltl = new Live_Table_lib();
                    ltl.Html_position = Int32.Parse(m.Html_position);
                    ltl.Home_team_big = Int32.Parse(GetNumber(m.Home_team_big));
                    ltl.Away_team_big = Int32.Parse(GetNumber(m.Away_team_big));
                    ltl.Match_type = m.Match_type.Trim();
                    if (m.S_date.IndexOf("-") != -1)
                        temp_date = m.S_date.Substring(0, 10);
                    ltl.Match_time = DateTime.Parse(temp_date + " " + m.S_time);
                    ltl.Status = m.Full_time_score.Replace("&nbsp;", "").Trim();
                    ltl.Home_team = m.Home_team.Trim();
                    ltl.Away_team = m.Away_team.Trim();
                    ltl.Home_red_card = StringCount(m.Home_team, "&nbsp;", 0);
                    ltl.Away_red_card = StringCount(m.Away_team, "&nbsp;", 0);
                    ltl.Full_home_goals = null;
                    ltl.Full_away_goals = null;
                    if (m.Half_time_score.IndexOf("-") > 0)
                    {
                        ltl.Half_home_goals = Int32.Parse(m.Half_time_score.Substring(0, m.Half_time_score.IndexOf("-")));
                        ltl.Half_away_goals = Int32.Parse(m.Half_time_score.Substring(m.Half_time_score.IndexOf("-") + 1, m.Half_time_score.Length - m.Half_time_score.IndexOf("-") - 1));
                    }

                    var rtExist = matches.Live_Table_lib.Where(p => p.Home_team_big == ltl.Home_team_big && p.Away_team_big == ltl.Away_team_big);
                    //let关键字，匿名类型
                    var rtUpdateExist = from p in rtExist
                                        let timeDiff = ltl.Match_time.Value - p.Match_time.Value
                                        where timeDiff.Days <= 1
                                        where timeDiff.Days >= -1
                                        select p;

                    //存在记录的则做更新，必须确认是最新数据，即时间差不超过1天
                    if (rtUpdateExist.Any())
                    {
                        var rtUpate = rtUpdateExist.First();
                        rtUpate.Status = ltl.Status;
                        rtUpate.Home_team = ltl.Home_team;
                        rtUpate.Away_team = ltl.Away_team;
                        rtUpate.Home_red_card = ltl.Home_red_card;
                        rtUpate.Away_red_card = ltl.Away_red_card;
                        rtUpate.Half_home_goals = ltl.Half_home_goals;
                        rtUpate.Half_away_goals = ltl.Half_away_goals;
                        //matches.SubmitChanges();
                    }
                    //不存在记录的此处做插入
                    else
                    {
                        matches.Live_Table_lib.InsertOnSubmit(ltl);
                        //matches.SubmitChanges();
                    }
                }
            }

            matches.Live_Table.DeleteAllOnSubmit(lt);//更新后删除
            matches.SubmitChanges();
            MessageBox.Show("OK");
            //dataGridView1.DataSource = matches.live_Table_lib;
        }

    }
}
