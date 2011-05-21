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
        public List<int> idExc;
        public ForecastAlgorithm()
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                var idLive = matches.Live_Table_lib.Select(e => e.Live_table_lib_id);
                var idAnalysis = matches.Match_analysis_result.Select(e =>(int) e.Live_table_lib_id);
                idExc = idLive.Except(idAnalysis).ToList();   //except序列A有的元素序列B没有
            }
        }
        public void top20Algorithm()
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                int i = 0;
                foreach (var id in idExc)
                {
                    i++;
                    ProgressBarDelegate.DoSendPMessage(i);
                    Application.DoEvents();
                    RowNumberLimit r = new RowNumberLimit(id);
                    Match_analysis_result mar = new Match_analysis_result();
                    mar.Live_table_lib_id = r.id;
                    mar.Pre_algorithm = "top20";
                    mar.Pre_match_count = r.Top20Count;
                    mar.Home_goals = r.HomeGoals;
                    mar.Away_goals = r.AwayGoals;
                    mar.Home_w = r.hWin;
                    mar.Home_d = r.hDraw;
                    mar.Home_l = r.hLose;
                    mar.Fit_win_loss = r.CureFitWinLoss ;
                    mar.Fit_goals = r.CureFitGoals ;
                    mar.Fit_odd_even = r.CureFitOddEven ;
                    matches.Match_analysis_result.InsertOnSubmit(mar);
                   
                }
                matches.SubmitChanges();
            }
        }
    }
}
