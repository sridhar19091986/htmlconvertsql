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

            DataClassesMatchDataContext match = new DataClassesMatchDataContext();

            foreach (DataRow aa in dt.Rows)
            {
                if (HtmlTextToStr(aa[3].ToString()) != null)
                {
                    result_tb rt = new result_tb();
                    rt.html_position = aa[0].ToString();
                    rt.match_type = HtmlTextToStr(aa[1].ToString());
                    rt.s_time = HtmlTextToStr(aa[2].ToString());
                    rt.home_team = HtmlTextToStr(aa[3].ToString());
                    rt.full_time_score = HtmlTextToStr(aa[4].ToString());
                    rt.away_team = HtmlTextToStr(aa[5].ToString());
                    rt.half_time_score = HtmlTextToStr(aa[6].ToString());
                    rt.odds = HtmlTextToStr(aa[7].ToString());
                    rt.win_loss_big = HtmlTextToStr(aa[8].ToString());
                    rt.s_date = HtmlDateToStrResult(aa[2].ToString());
                    rt.home_team_big = HtmlHrefToStr(aa[3].ToString());
                    rt.away_team_big = HtmlHrefToStr(aa[5].ToString());

                    match.result_tb.InsertOnSubmit(rt);
                    match.SubmitChanges();
                    
                }
            }
           
            return match.result_tb.Select(e => e.result_tb_id).Max();
        }
        //设置一个空的构造函数，调用后面的方法
        public SevenmResultToSql()
        {
     
        }
        public  void UpdateLastMatch()
        {
            int i = 0;
            DataClassesMatchDataContext matches = new DataClassesMatchDataContext();
            var rt = matches.result_tb.OrderBy(o => o.s_date).ThenBy(p => p.s_time);//用lambda表达式简洁

            //取临时变量监视
            DateTime lib_max_match_time = matches.result_tb_lib.Select(p => p.match_time).Max().Value.AddDays(-2);
            foreach (var m in rt)
            {
                i++;
                ProgressBarDelegate.DoSendPMessage(i);
                Application.DoEvents();
                if (m.home_team_big != null)
                {
                    result_tb_lib rtl = new result_tb_lib();
                    rtl.html_position = Int32.Parse(m.html_position);
                    rtl.home_team_big = Int32.Parse(GetNumber(m.home_team_big));
                    rtl.away_team_big = Int32.Parse(GetNumber(m.away_team_big));
                    rtl.match_type = m.match_type.Trim();
                    rtl.match_time = DateTime.Parse(m.s_date.Substring(0, 10) + " " + m.s_time);
                    rtl.odds = m.odds.Trim();
                    rtl.win_loss_big = m.win_loss_big.Trim();
                    rtl.home_team = m.home_team.Trim();
                    rtl.away_team = m.away_team.Trim();
                    rtl.home_red_card = StringCount(m.home_team, "&nbsp;", 0);
                    rtl.away_red_card =StringCount(m.away_team, "&nbsp;", 0);
                    string bf = m.full_time_score.Replace("&nbsp;", "").Replace("&nbsp;", "");
                    if (m.full_time_score.IndexOf("-") > 0)
                    {
                        rtl.full_home_goals = Int32.Parse(bf.Substring(0, bf.IndexOf("-")));
                        rtl.full_away_goals = Int32.Parse(bf.Substring(bf.IndexOf("-") + 1, bf.Length - bf.IndexOf("-") - 1));
                    }
                    if (m.half_time_score.IndexOf("-") > 0)
                    {
                        rtl.half_home_goals = Int32.Parse(m.half_time_score.Substring(0, m.half_time_score.IndexOf("-")));
                        rtl.half_away_goals = Int32.Parse(m.half_time_score.Substring(m.half_time_score.IndexOf("-") + 1, m.half_time_score.Length - m.half_time_score.IndexOf("-") - 1));
                    }

                    //如果库中文件的日期太小，直接删除
                    if (rtl.match_time > lib_max_match_time)
                    {
                        //数据分区，层次化查询
                        var rtExist = from p in matches.result_tb_lib
                                      where p.match_time == rtl.match_time
                                      where p.home_team_big == rtl.home_team_big
                                      where p.away_team_big == rtl.away_team_big
                                      select p;

                        //库中没有记录直接插入
                        if (!rtExist.Any())
                        {
                            matches.result_tb_lib.InsertOnSubmit(rtl);
                            matches.SubmitChanges();
                        }
                    }

                    //更新后删除
                    matches.result_tb.DeleteOnSubmit(m);
                    matches.SubmitChanges();
                    
                }
            }
            MessageBox.Show("OK");
            //dataGridView1.DataSource = matches.result_tb_lib;
        }
    }
}
