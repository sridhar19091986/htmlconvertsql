using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MathWorks.MATLAB.NET.Arrays;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using mshtml;
using System.Net;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Linq.Expressions;
using System.ComponentModel;
using HtmlAgilityPack;
using Soccer_Score_Forecast.LinqSql;
using System.Linq;
using System.Collections;

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

        public UpdateAnalysisResult()
        {
            //mar=matches.match_analysis_result.Where(e => e.analysis_result_id == null);
            mar = matches.Match_analysis_result.Where(e => e.Result_tb_lib_id == null);
            ExecUpateCount = mar.Count();

        }
        public void ExecUpdate()
        {

            int i = 0;
            //var lvls = matches.live_Table_lib.ToDictionary(e => e.live_table_lib_id);
            var lvls = matches.Live_Table_lib.ToDictionary(e => e.Live_table_lib_id);
            //var rtls = matches.result_tb_lib.ToDictionary(e => e.match_time.ToString() + "-" + e.home_team_big + "-" + e.away_team_big);
            var rtls = matches.Result_tb_lib.ToDictionary(e => e.Match_time.ToString() + "-" + e.Home_team_big + "-" + e.Away_team_big);

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
                if (rtls.ContainsKey(match_time.ToString() + "-" + home_team_big + "-" + away_team_big))
                {
                    var rtl = rtls[match_time.ToString() + "-" + home_team_big + "-" + away_team_big];
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

                    if ((rtl.Full_home_goals - rtl.Full_away_goals) * (m.Home_w - m.Home_l) > 0)
                        m.Result_wdl = "W";
                    else
                        m.Result_wdl = "L";
                }



                //    }
                //}
            }
            matches.SubmitChanges();
        }
    }
}

