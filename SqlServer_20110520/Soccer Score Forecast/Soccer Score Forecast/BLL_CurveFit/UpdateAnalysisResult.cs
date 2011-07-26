using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SoccerScore.Compact.Linq;
using System.Linq;

namespace Soccer_Score_Forecast
{
    public class UpdateAnalysisResult
    {
        DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn);
        //DataClassesMatchDataContext matches = new DataClassesMatchDataContext();
        public int ExecUpateCount;
        public IEnumerable<Match_analysis_result> mar;
        private DateTime? match_time;
        private int? home_team_big;
        private int? away_team_big;
        //private decimal live_table_lib_id;
        private int result_tb_lib_id;

        public UpdateAnalysisResult(int overday)
        {
            //mar=matches.match_analysis_result.Where(e => e.analysis_result_id == null);

            //修正完场数据入库后不能修正错误的问题 2011.6.14

            if (overday == -1)
                mar = matches.Match_analysis_result.Where(e => e.Result_tb_lib_id == null);
            else
                mar = matches.Match_analysis_result;

            ExecUpateCount = mar.Count();

        }

        public void ExecUpdate()
        {

            int i = 0;
            //var lvls = matches.live_Table_lib.ToDictionary(e => e.live_table_lib_id);
            var lvls = matches.Live_Table_lib.ToDictionary(e => e.Live_table_lib_id);
            //var rtls = matches.result_tb_lib.ToDictionary(e => e.match_time.ToString() + "-" + e.home_team_big + "-" + e.away_team_big);

            //修改无法更新完场数据的问题 2011.6.11
            var rtls = matches.Result_tb_lib.ToLookup(e => ((DateTime)e.Match_time).ToShortDateString() 
                + "-" + e.Home_team_big + "-" + e.Away_team_big);

            string result = null;

            foreach (var m in mar)
            {
                i++;
                ProgressBarDelegate.DoSendPMessage(i);
                Application.DoEvents();
                //根据lvl的id
                //live_table_lib_id = m.live_table_lib_id;

                //if (lvls.Any())
                //{
                //    //得出match time home away

                #region  删除Live_Table_lib已经删除的数据
                if (!lvls.Keys.Contains((int)m.Live_table_lib_id))
                {
                    var marid = matches.Match_analysis_result
                        .Where(e => e.Live_table_lib_id == m.Live_table_lib_id)
                        .FirstOrDefault();
                    matches.Match_analysis_result.DeleteOnSubmit(marid);
                    continue;
                }
                #endregion

                var lvl = lvls[(int)m.Live_table_lib_id];

                match_time = lvl.Match_time;
                home_team_big = lvl.Home_team_big;
                away_team_big = lvl.Away_team_big;
                //var rtls = matches.result_tb_lib.Where(e => e.match_time == match_time)
                //    .Where(e => e.home_team_big == home_team_big)
                //    .Where(e => e.away_team_big == away_team_big);



                //if (rtls.Any())
                //{
                //得出result中的id
                //var rtl = rtls.First();

                //修改无法更新完场数据的问题 2011.6.11

                string comparekeyA = ((DateTime)match_time).ToShortDateString()
                    + "-" + home_team_big + "-" + away_team_big;

                //前后2天修正
                string comparekeyB = ((DateTime)match_time.Value.AddDays(1)).ToShortDateString()
                    + "-" + home_team_big + "-" + away_team_big;
                string comparekeyC = ((DateTime)match_time.Value.AddDays(-1)).ToShortDateString()
                    + "-" + home_team_big + "-" + away_team_big;

                if (rtls.Contains(comparekeyA )|| rtls.Contains(comparekeyB )|| rtls.Contains(comparekeyC ))
                {
                    var rtl = rtls[comparekeyA].FirstOrDefault(); //修改无法更新完场数据的问题 2011.6.11

                    //前后2天修正
                    if (rtls.Contains(comparekeyB) ) rtl = rtls[comparekeyB].FirstOrDefault();
                    if (rtls.Contains(comparekeyC) ) rtl = rtls[comparekeyC].FirstOrDefault();

                    result_tb_lib_id = rtl.Result_tb_lib_id;

                    m.Result_tb_lib_id = result_tb_lib_id;

                    if ((rtl.Full_home_goals - rtl.Full_away_goals) * m.Fit_win_loss > 0)
                        m.Result_fit = "W";
                    else
                        m.Result_fit = "L";

                    if ((rtl.Full_home_goals - rtl.Full_away_goals) * (m.Home_goals - m.Away_goals) > 0)
                        m.Result_goals = "W";
                    else
                        m.Result_goals = "L";


                    //if ((rtl.Full_home_goals - rtl.Full_away_goals) * (m.Home_w - m.Home_l) > 0)
                    //    m.Result_wdl = "W";
                    //else
                    //    m.Result_wdl = "L";

                    //此处需要增加一些预测字段  2011.6.19

                    if (rtl.Full_home_goals > rtl.Full_away_goals) result = "3";
                    if (rtl.Full_home_goals == rtl.Full_away_goals) result = "1";
                    if (rtl.Full_home_goals < rtl.Full_away_goals) result = "0";

                    //这里导致运算错误  2011.7.26
                    if(m.Myfit.IndexOf(result) !=-1)
                        m.Result_wdl = "W";
                    else
                        m.Result_wdl = "L";

                    //if ((rtl.Full_home_goals - rtl.Full_away_goals) * (m.Home_w - m.Home_l) > 0)
                    //    m.Result_wdl = "W";
                    //else
                    //    m.Result_wdl = "L";
                }



                //    }
                //}
            }
            matches.SubmitChanges();
        }
    }
}

