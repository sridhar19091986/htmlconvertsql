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
using SoccerScore.Compact.Linq;
using System.Linq;
using System.Collections;

namespace Soccer_Score_Forecast
{
    public class UpdateAnalysisResult
    {
        //SoccerScoreCompact match = new SoccerScoreCompact(cnn);
        //SoccerScoreCompact match = new SoccerScoreCompact(cnn);
        public int ExecUpateCount;
        public IEnumerable<Match_analysis_result> mar;
        private DateTime? Match_time;
        private int? Home_team_big;
        private int? Away_team_big;
        //private decimal Live_table_lib_id;
        private decimal Result_tb_lib_id;

        public UpdateAnalysisResult()
        {
            mar = Conn.match.Match_analysis_result.Where(e => e.Analysis_result_id == null);
            //mar = match.Match_analysis_result.Where(e => e.Result_tb_lib_id == null);
            ExecUpateCount = mar.Count();

        }
        public void ExecUpdate()
        {

            int i = 0;
            //var lvls = match.Live_Table_lib.ToDictionary(e => e.Live_table_lib_id);
            var lvls = Conn.match.Live_Table_lib.ToDictionary(e => e.Live_table_lib_id);
            //var rtls = match.Result_tb_lib.ToDictionary(e => e.Match_time.ToString() + "-" + e.Home_team_big + "-" + e.Away_team_big);
            var rtls = Conn.match.Result_tb_lib.ToDictionary(e => e.Match_time.ToString() + "-" + e.Home_team_big + "-" + e.Away_team_big);

            foreach (var m in mar)
            {
                i++;
                ProgressBarDelegate.DoSendPMessage(i);
                Application.DoEvents();
                //根据lvl的id
                //Live_table_lib_id = m.Live_table_lib_id;

                //if (lvls.Any())
                //{
                //    //得出match time home away
                var lvl = lvls[m.Live_table_lib_id];
                Match_time = lvl.Match_time;
                Home_team_big = lvl.Home_team_big;
                Away_team_big = lvl.Away_team_big;
                //var rtls = match.Result_tb_lib.Where(e => e.Match_time == Match_time)
                //    .Where(e => e.Home_team_big == Home_team_big)
                //    .Where(e => e.Away_team_big == Away_team_big);



                //if (rtls.Any())
                //{
                //得出result中的id
                //var rtl = rtls.First();
                if (rtls.ContainsKey(Match_time.ToString() + "-" + Home_team_big + "-" + Away_team_big))
                {
                    var rtl = rtls[Match_time.ToString() + "-" + Home_team_big + "-" + Away_team_big];
                    Result_tb_lib_id = rtl.Result_tb_lib_id;
                    m.Result_tb_lib_id = Result_tb_lib_id;

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
            Conn.match.SubmitChanges();
        }
    }
}

