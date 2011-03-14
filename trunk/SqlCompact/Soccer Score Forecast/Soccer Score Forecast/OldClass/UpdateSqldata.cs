//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
//using System.IO;
//using System.Text;
//using System.Threading;
//using System.Windows.Forms;
//using MathWorks.MATLAB.NET.Arrays;
//using System.Runtime.InteropServices;
//using System.Text.RegularExpressions;
//using mshtml;
//using System.Net;
//using System.Data.Linq;
//using System.Data.Linq.Mapping;
//using System.Reflection;
//using System.Linq.Expressions;
//using System.ComponentModel;
//using HtmlAgilityPack;
//using Soccer_Score_Forecast.LinqSql;
//using System.Linq;
//using System.Collections;

//namespace Soccer_Score_Forecast
//{
//    class OldUpdateSqldata
//    {
//        public static void UpdateTodayMatch()
//        {
//            DataClassesMatchDataContext matches = new DataClassesMatchDataContext();
//            var lt = matches.live_Table.OrderBy(o => o.s_date).ThenBy(p => p.s_time);//用lambda表达式简洁

//            foreach (var m in lt)
//            {
//                if (m.home_team_big != null)
//                {
//                    //一一对应生成
//                    live_Table_lib ltl = new live_Table_lib();
//                    ltl.html_position = Int32.Parse(m.html_position);
//                    ltl.home_team_big = Int32.Parse(TableHtmlAgilityPack.GetNumber(m.home_team_big));
//                    ltl.away_team_big = Int32.Parse(TableHtmlAgilityPack.GetNumber(m.away_team_big));
//                    ltl.match_type = m.match_type.Trim();
//                    ltl.match_time = DateTime.Parse(m.s_date.Substring(0, 10) + " " + m.s_time);
//                    ltl.status = m.full_time_score.Replace("&nbsp;", "").Trim();
//                    ltl.home_team = m.home_team.Trim();
//                    ltl.away_team = m.away_team.Trim();
//                    ltl.home_red_card = TableHtmlAgilityPack.StringCount(m.home_team, "&nbsp;", 0);
//                    ltl.away_red_card = TableHtmlAgilityPack.StringCount(m.away_team, "&nbsp;", 0);
//                    ltl.full_home_goals = null;
//                    ltl.full_away_goals = null;
//                    if (m.half_time_score.IndexOf("-") > 0)
//                    {
//                        ltl.half_home_goals = Int32.Parse(m.half_time_score.Substring(0, m.half_time_score.IndexOf("-")));
//                        ltl.half_away_goals = Int32.Parse(m.half_time_score.Substring(m.half_time_score.IndexOf("-") + 1, m.half_time_score.Length - m.half_time_score.IndexOf("-") - 1));
//                    }
   
//                    var rtExist =matches.live_Table_lib.Where (p=> p.home_team_big == ltl.home_team_big&& p.away_team_big == ltl.away_team_big);
//                    //let关键字，匿名类型
//                    var rtUpdateExist = from p in rtExist 
//                                        let timeDiff = ltl.match_time.Value - p.match_time.Value
//                                        where timeDiff.Days <= 1
//                                        where timeDiff.Days >= -1
//                                        select p;

//                     //存在记录的则做更新，必须确认是最新数据，即时间差不超过1天
//                    if (rtUpdateExist.Any())
//                    {
//                        var rtUpate = rtUpdateExist.First();
//                        rtUpate.status = ltl.status;
//                        rtUpate.home_team = ltl.home_team;
//                        rtUpate.away_team = ltl.away_team;
//                        rtUpate.home_red_card = ltl.home_red_card;
//                        rtUpate.away_red_card = ltl.away_red_card;
//                        rtUpate.half_home_goals = ltl.half_home_goals;
//                        rtUpate.half_away_goals = ltl.half_away_goals;
//                        matches.SubmitChanges();
//                    }
//                    //不存在记录的此处做插入
//                    else
//                    {
//                        matches.live_Table_lib.InsertOnSubmit(ltl);
//                        matches.SubmitChanges();
//                    }
//                }
//            }
//            matches.live_Table.DeleteAllOnSubmit(lt);//更新后删除
//            matches.SubmitChanges();
//            MessageBox.Show("OK");
//            //dataGridView1.DataSource = matches.live_Table_lib;
//        }

//        public static void UpdateLastMatch()
//        {

//            int i = 0;
//            DataClassesMatchDataContext matches = new DataClassesMatchDataContext();
//            var rt = matches.result_tb.OrderBy(o => o.s_date).ThenBy(p => p.s_time);//用lambda表达式简洁

//            //取临时变量监视
//            DateTime lib_max_match_time = matches.result_tb_lib.Select(p => p.match_time).Max().Value.AddDays(-2);
//            foreach (var m in rt)
//            {
//                i++;
//                ProgressBarDelegate.DoSendPMessage(i);
//                Application.DoEvents();
//                if (m.home_team_big != null)
//                {
//                    result_tb_lib rtl = new result_tb_lib();
//                    rtl.html_position = Int32.Parse(m.html_position);
//                    rtl.home_team_big = Int32.Parse(TableHtmlAgilityPack.GetNumber(m.home_team_big));
//                    rtl.away_team_big = Int32.Parse(TableHtmlAgilityPack.GetNumber(m.away_team_big));
//                    rtl.match_type = m.match_type.Trim();
//                    rtl.match_time = DateTime.Parse(m.s_date.Substring(0, 10) + " " + m.s_time);
//                    rtl.odds = m.odds.Trim();
//                    rtl.win_loss_big = m.win_loss_big.Trim();
//                    rtl.home_team = m.home_team.Trim();
//                    rtl.away_team = m.away_team.Trim();
//                    rtl.home_red_card = TableHtmlAgilityPack.StringCount(m.home_team, "&nbsp;", 0);
//                    rtl.away_red_card = TableHtmlAgilityPack.StringCount(m.away_team, "&nbsp;", 0);
//                    string bf = m.full_time_score.Replace("&nbsp;", "").Replace("&nbsp;", "");
//                    if (m.full_time_score.IndexOf("-") > 0)
//                    {
//                        rtl.full_home_goals = Int32.Parse(bf.Substring(0, bf.IndexOf("-")));
//                        rtl.full_away_goals = Int32.Parse(bf.Substring(bf.IndexOf("-") + 1, bf.Length - bf.IndexOf("-") - 1));
//                    }
//                    if (m.half_time_score.IndexOf("-") > 0)
//                    {
//                        rtl.half_home_goals = Int32.Parse(m.half_time_score.Substring(0, m.half_time_score.IndexOf("-")));
//                        rtl.half_away_goals = Int32.Parse(m.half_time_score.Substring(m.half_time_score.IndexOf("-") + 1, m.half_time_score.Length - m.half_time_score.IndexOf("-") - 1));
//                    }

//                    //如果库中文件的日期太小，直接删除
//                    if (rtl.match_time > lib_max_match_time)
//                    {
//                        //数据分区，层次化查询
//                        var rtExist = from p in matches.result_tb_lib
//                                      where p.match_time == rtl.match_time
//                                      where p.home_team_big == rtl.home_team_big
//                                      where p.away_team_big == rtl.away_team_big
//                                      select p;

//                        //库中没有记录直接插入
//                        if (!rtExist.Any())
//                        {
//                            matches.result_tb_lib.InsertOnSubmit(rtl);
//                        }
//                    }

//                    //更新后删除
//                    matches.result_tb.DeleteOnSubmit(m);
//                    matches.SubmitChanges();
//                }
//            }
//            MessageBox.Show("OK");
//            //dataGridView1.DataSource = matches.result_tb_lib;
//        }
//    }
//}
