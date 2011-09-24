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
        //private IEnumerable<Match_analysis_result> mars;
        private ILookup<string,Live_Single> lss;
        //private DateTime? todaytime;
        private Match_analysis_result mar;
        private RowNumberLimit r;
        private Live_Single sg;
        private Live_Single sgerror;
        public AuditForecastAlgorithm(int daysDiff)
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                idExc = matches.Live_Table_lib
                   .Where(e => e.Match_time.Value.Date >= DateTime.Now.AddDays(daysDiff).Date)
                   .Select(e => e.Live_table_lib_id)
                   .ToList();
                //mars = matches.Match_analysis_result;   //这里不能放入list，否则更新不了数据， 2011.7.27
                lss = matches.Live_Single.ToLookup(e => e.Home_team_big + e.Away_team_big);
            }
            //dMatch.dNew = false;
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
                    r = new RowNumberLimit(liveid);
                    r.initCurveFit();
                    //match_analysis_result mar = new match_analysis_result();

                    mar = matches.Match_analysis_result
                        .Where(e => e.Live_table_lib_id == liveid)
                        .First();//查找需要更新的数据

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
                    mar.Fit_odd_even = r.CureFitOddEven();//直接赋值，修改完成

                    //2011.9.22   修改成利用积分进行运算
                    mar.Recent_scores = r.RecentScores; //增加最后一轮的分数
                    mar.Recent_2scores = r.Recent2Scores;
                    mar.Recent_3scores = r.Recent3Scores;
                    mar.Recent_4scores = r.Recent4Scores;
                    mar.Recent_5scores = r.Recent5Scores;
                    mar.Recent_6scores = r.Recent6Scores;


                    //2011.6.22
                    mar.Cross_goals = r.CrossGoals;


                    //2011.6.16
                    //【交战+概率1+拟合+进球+概率30】
                    mar.Myfit =
                        //交战
                        ForecastCross(r.CrossGoals) + ":" +
                        //概率1
                        ForecastD(mar.Home_w, mar.Home_d, mar.Home_l) + ":" +
                        //拟合+进球+概率30
                        ForecastWL(mar.Fit_win_loss, mar.Home_goals, mar.Away_goals, mar.Home_w, mar.Home_l);


                    //更新北京单场
                    sg = lss[r.home_team_big.ToString() + r.away_team_big.ToString()].FirstOrDefault();
                    if (sg != null)
                        mar.Pre_algorithm = sg.Html_position;

                    sgerror = lss[r.away_team_big.ToString() + r.home_team_big.ToString()].FirstOrDefault();
                    if (sgerror != null)
                        mar.Pre_algorithm = sgerror.Html_position;

                    //r.Close();

                    //缩短数据更新周期
                    if (i % 100 == 0)
                    {
                        matches.SubmitChanges(); GC.Collect(); GC.Collect(); Application.DoEvents();
                    }
                }
                matches.SubmitChanges();
            }
        }


        //2011.6.22
        private string ForecastCross(double crossgoals)
        {
            string cross = null;
            if (crossgoals > 0) cross = "3";
            if (crossgoals == 0) cross = "1";
            if (crossgoals < 0) cross = "0";
            return cross;
        }

        //2011.6.17  算法更新，交战，预测，概率

        private string ForecastD(int? w, int? d, int? l)
        {
            int?[] wdl = { w, d, l };
            if (d == wdl.Min() && d != w && d != l)
            {
                if (w > l) return "3";
                else
                {
                    if (w == l) return "1";
                    else return "0";
                }
            }
            else
            {
                return "1";
            }
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
