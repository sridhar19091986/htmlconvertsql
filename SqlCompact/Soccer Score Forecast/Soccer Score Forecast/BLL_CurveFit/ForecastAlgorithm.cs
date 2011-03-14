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
    public class ForecastAlgorithm
    {
        //private DataClassesMatchDataContext matches = new DataClassesMatchDataContext();
        public List<Decimal> idExc;
        public ForecastAlgorithm()
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext())
            {
                var idLive = matches.live_Table_lib.Select(e => e.live_table_lib_id);
                var idAnalysis = matches.match_analysis_result.Select(e => e.live_table_lib_id);
                idExc = idLive.Except(idAnalysis).ToList();   //except序列A有的元素序列B没有
            }
        }
        public void top20Algorithm()
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext())
            {
                int i = 0;
                foreach (var id in idExc)
                {
                    i++;
                    ProgressBarDelegate.DoSendPMessage(i);
                    Application.DoEvents();
                    RowNumberLimit r = new RowNumberLimit(id);
                    match_analysis_result mar = new match_analysis_result();
                    mar.live_table_lib_id = r.id;
                    mar.pre_algorithm = "top20";
                    mar.pre_match_count = r.Top20Count;
                    mar.home_goals = r.HomeGoals;
                    mar.away_goals = r.AwayGoals;
                    mar.home_w = r.hWin;
                    mar.home_d = r.hDraw;
                    mar.home_l = r.hLose;
                    mar.fit_win_loss = r.CureFitWinLoss ;
                    mar.fit_goals = r.CureFitGoals ;
                    mar.fit_odd_even = r.CureFitOddEven ;
                    matches.match_analysis_result.InsertOnSubmit(mar);
                   
                }
                matches.SubmitChanges();
            }
        }
    }
}
