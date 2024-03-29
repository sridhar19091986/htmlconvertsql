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

            DataClassesMatchDataContext match = new DataClassesMatchDataContext(Conn.conn);

            foreach (DataRow aa in dt.Rows)
            {
                if (HtmlTextToStr(aa[3].ToString()) != null)
                {
                    Result_tb rt = new Result_tb();
                    rt.Html_position = aa[0].ToString();
                    rt.Match_type = HtmlTextToStr(aa[1].ToString());
                    rt.S_time = HtmlTextToStr(aa[2].ToString());
                    rt.Home_team = HtmlTextToStr(aa[3].ToString());
                    rt.Full_time_score = HtmlTextToStr(aa[4].ToString());
                    rt.Away_team = HtmlTextToStr(aa[5].ToString());
                    rt.Half_time_score = HtmlTextToStr(aa[6].ToString());
                    rt.Odds = HtmlTextToStr(aa[7].ToString());
                    rt.Win_loss_big = HtmlTextToStr(aa[8].ToString());
                    rt.S_date = HtmlDateToStrResult(aa[2].ToString());
                    rt.Home_team_big = HtmlHrefToStr(aa[3].ToString());
                    rt.Away_team_big = HtmlHrefToStr(aa[5].ToString());

                    match.Result_tb.InsertOnSubmit(rt);
                    match.SubmitChanges();
                    
                }
            }
           
            return match.Result_tb.Select(e => e.Result_tb_id).Max();
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
            DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn .conn );
            var rt = matches.Result_tb.OrderBy(o => o.S_date).ThenBy(p => p.S_time);//用lambda表达式简洁

            //取临时变量监视
            DateTime lib_max_match_time = matches.Result_tb_lib.Select(p => p.Match_time).Max().Value.AddDays(-2);
            foreach (var m in rt)
            {
                i++;
                ProgressBarDelegate.DoSendPMessage(i);
                Application.DoEvents();
                if (m.Home_team_big != null)
                {
                    Result_tb_lib rtl = new Result_tb_lib();
                    rtl.Html_position = Int32.Parse(m.Html_position);
                    rtl.Home_team_big = Int32.Parse(GetNumber(m.Home_team_big));
                    rtl.Away_team_big = Int32.Parse(GetNumber(m.Away_team_big));
                    rtl.Match_type = m.Match_type.Trim();
                    last_line = m.S_time.LastIndexOf("\n");
                    temp_time=m.S_time.Substring(last_line,m.S_time.Length-last_line-1);
                    rtl.Match_time = DateTime.Parse(m.S_date.Substring(0, 10) + " " + temp_time);
                    rtl.Odds = m.Odds.Trim();
                    rtl.Win_loss_big = m.Win_loss_big.Trim();
                    rtl.Home_team = m.Home_team.Trim();
                    rtl.Away_team = m.Away_team.Trim();
                    rtl.Home_red_card = StringCount(m.Home_team, "&nbsp;", 0);
                    rtl.Away_red_card =StringCount(m.Away_team, "&nbsp;", 0);
                    string bf = m.Full_time_score.Replace("&nbsp;", "").Replace("&nbsp;", "");
                    if (m.Full_time_score.IndexOf("-") > 0)
                    {
                        rtl.Full_home_goals = Int32.Parse(bf.Substring(0, bf.IndexOf("-")));
                        rtl.Full_away_goals = Int32.Parse(bf.Substring(bf.IndexOf("-") + 1, bf.Length - bf.IndexOf("-") - 1));
                    }
                    if (m.Half_time_score.IndexOf("-") > 0)
                    {
                        rtl.Half_home_goals = Int32.Parse(m.Half_time_score.Substring(0, m.Half_time_score.IndexOf("-")));
                        rtl.Half_away_goals = Int32.Parse(m.Half_time_score.Substring(m.Half_time_score.IndexOf("-") + 1, m.Half_time_score.Length - m.Half_time_score.IndexOf("-") - 1));
                    }

                    //如果库中文件的日期太小，直接删除
                    if (rtl.Match_time > lib_max_match_time)
                    {
                        //数据分区，层次化查询
                        var rtExist = from p in matches.Result_tb_lib
                                      where p.Match_time == rtl.Match_time
                                      where p.Home_team_big == rtl.Home_team_big
                                      where p.Away_team_big == rtl.Away_team_big
                                      select p;

                        //库中没有记录直接插入
                        if (!rtExist.Any())
                        {
                            matches.Result_tb_lib.InsertOnSubmit(rtl);
                            matches.SubmitChanges();
                        }
                    }

                    //更新后删除
                    matches.Result_tb.DeleteOnSubmit(m);
                    matches.SubmitChanges();
                    
                }
            }
            MessageBox.Show("OK");
            //dataGridView1.DataSource = matches.result_tb_lib;
        }
    }
}
