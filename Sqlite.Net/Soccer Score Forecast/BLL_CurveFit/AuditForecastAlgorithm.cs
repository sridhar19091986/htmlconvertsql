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
            using (SoccerScoreSqlite matches = new SoccerScoreSqlite(Conn.cnn))
            {
                //todaytime = DateTime.Now;
                IEnumerable<int> idLive = matches.LiveTableLib
                    .Where(e => e.MatchTime.Value.Date >= DateTime.Now.AddDays(daysDiff).Date)
                    .Select(e => e.LiveTableLibID);
                idExc = idLive.ToList();
            }
        }
        public void top20Algorithm()
        {
            using (SoccerScoreSqlite matches = new SoccerScoreSqlite(Conn.cnn))
            {
                int i = 0;
                foreach (var id in idExc)
                {
                    i++;
                    ProgressBarDelegate.DoSendPMessage(i);
                    Application.DoEvents();
                    RowNumberLimit r = new RowNumberLimit(id);
                    //match_analysis_result mar = new match_analysis_result();
                    var mar = matches.MatchAnalysisResult.Where(e => e.LiveTableLibID == id).First();//查找需要更新的数据
                    mar.LiveTableLibID= r.id;
                    mar.PreAlgorithm = "top20";
                    mar.PreMatchCount = r.Top20Count;
                    mar.HomeGoals = (float)r.HomeGoals;
                    mar.AwayGoals = (float)r.AwayGoals;
                    mar.HomeW = r.hWin;
                    mar.HomeD = r.hDraw;
                    mar.HomeL= r.hLose;
                    mar.FitWinLoss = (float)r.CureFitWinLoss;
                    mar.FitGoals = (float)r.CureFitGoals;
                    mar.FitOddEven = (float)r.CureFitOddEven;//直接赋值，修改完成
                    
                }
                matches.SubmitChanges();
            }
        }
    }
}
