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
    public class AuditForecastAlgorithm
    {
        //private DataClassesMatchDataContext matches = new DataClassesMatchDataContext();
        public List<int> idExc;
        //private DateTime? todaytime;
        public AuditForecastAlgorithm(int daysDiff)
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                //todaytime = DateTime.Now;
                var idLive = matches.Live_Table_lib
                    .Where(e => e.Match_time.Value.Date >= DateTime.Now.AddDays(daysDiff).Date)
                    .Select(e => e.Live_table_lib_id);
                idExc = idLive.ToList();
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
                    //match_analysis_result mar = new match_analysis_result();
                    var mar = matches.Match_analysis_result.Where(e => e.Live_table_lib_id == id).First();//查找需要更新的数据
                    mar.Live_table_lib_id = r.id;
                    mar.Pre_algorithm = "top20";
                    mar.Pre_match_count  = r.Top20Count;
                    mar.Home_goals = r.HomeGoals;
                    mar.Away_goals = r.AwayGoals;
                    mar.Home_w = r.hWin;
                    mar.Home_d = r.hDraw;
                    mar.Home_l = r.hLose;
                    mar.Fit_win_loss = r.CureFitWinLoss ;
                    mar.Fit_goals = r.CureFitGoals ;
                    mar.Fit_odd_even = r.CureFitOddEven ;//直接赋值，修改完成
                    
                }
                matches.SubmitChanges();
            }
        }
    }
}
