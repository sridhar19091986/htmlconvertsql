﻿using System;
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
        DataClassesMatchDataContext matches = new DataClassesMatchDataContext();
        //DataClassesMatchDataContext matches = new DataClassesMatchDataContext();
        public int ExecUpateCount;
        public IEnumerable<match_analysis_result> mar;
        private DateTime? match_time;
        private int? home_team_big;
        private int? away_team_big;
        //private decimal live_table_lib_id;
        private decimal result_tb_lib_id;

        public UpdateAnalysisResult()
        {
            //mar=matches.match_analysis_result.Where(e => e.analysis_result_id == null);
            mar = matches.match_analysis_result.Where(e => e.result_tb_lib_id == null);
            ExecUpateCount = mar.Count();

        }
        public void ExecUpdate()
        {

            int i = 0;
            //var lvls = matches.live_Table_lib.ToDictionary(e => e.live_table_lib_id);
            var lvls = matches.live_Table_lib.ToDictionary(e => e.live_table_lib_id);
            //var rtls = matches.result_tb_lib.ToDictionary(e => e.match_time.ToString() + "-" + e.home_team_big + "-" + e.away_team_big);
            var rtls = matches.result_tb_lib.ToDictionary(e => e.match_time.ToString() + "-" + e.home_team_big + "-" + e.away_team_big);

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
                var lvl = lvls[m.live_table_lib_id];
                match_time = lvl.match_time;
                home_team_big = lvl.home_team_big;
                away_team_big = lvl.away_team_big;
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
                    result_tb_lib_id = rtl.result_tb_lib_id;
                    m.result_tb_lib_id = result_tb_lib_id;

                    if ((rtl.full_home_goals - rtl.full_away_goals) * m.fit_win_loss > 0)
                        m.result_fit = "W";
                    else
                        m.result_fit = "L";

                    if ((rtl.full_home_goals - rtl.full_away_goals) * (m.home_goals - m.away_goals) > 0)
                        m.result_goals = "W";
                    else
                        m.result_goals = "L";

                    if ((rtl.full_home_goals - rtl.full_away_goals) * (m.home_w - m.home_l) > 0)
                        m.result_wdl = "W";
                    else
                        m.result_wdl = "L";
                }



                //    }
                //}
            }
            matches.SubmitChanges();
        }
    }
}

