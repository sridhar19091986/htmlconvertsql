﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SoccerScore.Compact.Linq;
using System.Linq;

namespace Soccer_Score_Forecast
{
    public class AuditForecastAlgorithm
    {
        //private DataClassesMatchDataContext matches = new DataClassesMatchDataContext();
        public List<int> idExc;
        //private DateTime? todaytime;
        public AuditForecastAlgorithm(int daysDiff)
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                idExc = matches.Live_Table_lib
                   .Where(e => e.Match_time.Value.Date >= DateTime.Now.AddDays(daysDiff).Date)
                   .Select(e => e.Live_table_lib_id)
                   .ToList();
            }
        }
        public void top20Algorithm()
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                int i = 0;
                foreach (int liveid in idExc)
                {
                    i++;
                    ProgressBarDelegate.DoSendPMessage(i);
                    Application.DoEvents();
                    RowNumberLimit r = new RowNumberLimit(liveid);
                    r.initCurveFit();
                    //match_analysis_result mar = new match_analysis_result();
                    var mar = matches.Match_analysis_result.Where(e => e.Live_table_lib_id == liveid).First();//查找需要更新的数据
                    mar.Live_table_lib_id = r.live_id;
                    mar.Pre_match_count = r.Top20Count;
                    mar.Home_goals = r.HomeGoals;
                    mar.Away_goals = r.AwayGoals;
                    mar.Home_w = r.hWin;
                    mar.Home_d = r.hDraw;
                    mar.Home_l = r.hLose;
                    mar.Fit_win_loss = r.CureFitWinLoss();
                    mar.Fit_goals = r.CureFitGoals();
                    mar.Fit_odd_even = r.CureFitOddEven();//直接赋值，修改完成

                    //2011.6.16
                    //【交战+概率1+拟合+进球+概率30】
                    mar.Pre_algorithm =
                        r.LastJZ + ":" +
                        ForecastD(mar.Home_w, mar.Home_d, mar.Home_l) + ":" +
                        ForecastWL(mar.Fit_win_loss, mar.Home_goals, mar.Away_goals, mar.Home_w, mar.Home_l);

                }
                matches.SubmitChanges();
            }
        }

        //2011.6.17  算法更新，交战，预测，概率

        private string ForecastD(int? w, int? d, int? l)
        {
            int?[] wdl = { w, d, l };
            if (d == wdl.Min() && d != w && d != l) return "30";
            else return "1";
        }

        //2011.6.17 算法更新，预测，进球，概率

        private string ForecastWL(double? Fit_win_loss, double? Home_goals, double? Away_goals, int? Home_w, int? Home_l)
        {
            string forecast = "";

            if (Fit_win_loss > 0) forecast += "3:";
            if (Fit_win_loss == 0) forecast += "1:";
            if (Fit_win_loss < 0) forecast += "0:";

            if (Home_goals - Away_goals > 0) forecast += "3:";
            if (Home_goals - Away_goals == 0) forecast += "1:";
            if (Home_goals - Away_goals < 0) forecast += "0:";

            if (Home_w - Home_l > 0) forecast += "3";
            if (Home_w - Home_l == 0) forecast += "1";
            if (Home_w - Home_l < 0) forecast += "0";

            return forecast;
        }
    }
}
