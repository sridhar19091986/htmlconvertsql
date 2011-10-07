using System.Collections.Generic;
using System.Windows.Forms;
using SoccerScore.Compact.Linq;
using System.Linq;

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
                var idLive = matches.Live_Table_lib.Select(e => e.Live_table_lib_id).ToList();//立即加载效率高？
                var idAnalysis = matches.Match_analysis_result.Select(e => e.Live_table_lib_id ?? 0).ToList();//可空类型转换
                idExc = idLive.Except(idAnalysis).ToList();   //except序列A有的元素序列B没有
            }

            dMatch.dNew = false;
            dMatch.LoadMatchData(true);
        }
        private List<int?> idDelete;
        public void DeleteRedundancy()
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                var idLive = from p in matches.Match_analysis_result
                             group p by p.Live_table_lib_id into ttt
                             select new { id = ttt.Key, count = ttt.Count() };
                idDelete = idLive.Where(e => e.count > 1).Select(e => e.id).ToList();
                if (idDelete == null) return;
                foreach (int del in idDelete)
                {
                    var delid = matches.Live_Table_lib.Where(e => e.Live_table_lib_id == del).FirstOrDefault();
                    matches.Live_Table_lib.DeleteOnSubmit(delid);
                    var delid2 = matches.Match_analysis_result.Where(e => e.Live_table_lib_id == del).FirstOrDefault();
                    matches.Match_analysis_result.DeleteOnSubmit(delid2);
                }
                matches.SubmitChanges();
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
                    r.initCurveFit();
                    Match_analysis_result mar = new Match_analysis_result();
                    mar.Live_table_lib_id = r.live_id;
                    mar.Pre_algorithm = "top20";
                    mar.Pre_match_count = r.Top20Count;
                    mar.Home_goals = r.HomeGoals;
                    mar.Away_goals = r.AwayGoals;
                    mar.Home_w = r.hWin;
                    mar.Home_d = r.hDraw;
                    mar.Home_l = r.hLose;
                    mar.Fit_win_loss = r.CureFitWinLoss();
                    mar.Fit_goals = r.CureFitGoals();
                    mar.Fit_odd_even = r.CureFitOddEven();
                    matches.Match_analysis_result.InsertOnSubmit(mar);
                }
                matches.SubmitChanges();
            }
        }
    }
}
