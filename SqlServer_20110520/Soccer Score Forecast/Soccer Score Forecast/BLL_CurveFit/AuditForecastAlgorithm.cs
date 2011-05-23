using System;
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
                //todaytime = DateTime.Now;
                var idExc = matches.Live_Table_lib
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
                    //match_analysis_result mar = new match_analysis_result();
                    var mar = matches.Match_analysis_result.Where(e => e.Live_table_lib_id == liveid).First();//查找需要更新的数据
                    mar.Live_table_lib_id = r.live_id;
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
