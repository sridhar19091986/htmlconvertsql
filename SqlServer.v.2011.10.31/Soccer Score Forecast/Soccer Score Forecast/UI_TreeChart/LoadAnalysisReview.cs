using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SoccerScore.Compact.Linq;
using System.Linq;
using System.Text;
using SoccerScore.Compact.Linq.Review;
using System.Data.SqlClient;
using Soccer_Score_Forecast.BulkSql;

namespace Soccer_Score_Forecast.Handle
{
    public class LoadAnalysisReview : LoadDataMethod
    {
        //string fDraw = "";
        //string fjz = "";
        string myfit = "";
        string macau = "";
        //string comp = "";
        string smp = "";
        //string pnncheck = "";
        //int pnngrnncomp = 1;
        //记录北京单场盘口
        double bjpk = 0;
        //int grnncheck = -1;
        //计算是否有利于投资 1
        //int goalnumcheck = 0;
        //int fitgrnncomp = -1;
        //double preresult=0;

        //string result = "";
        //private  DataClassesMatchDataContext dcmdc=new DataClassesMatchDataContext (Conn.conn);
        private IDictionary<int, Live_Table_lib> ltlAll { get; set; }
        private IDictionary<int, Result_tb_lib> rtlAll { get; set; }
        private ILookup<int?, Match_analysis_result> marAll { get; set; }
        private ILookup<string, MacauPredication> mpAll { get; set; }
        //private List<Live_okoo> loAll { get; set; }
        private ILookup<string, Live_Single> lsAll { get; set; }
        private IEnumerable<Live_Table_lib> ltls { get; set; }
        //private IEnumerable<match_analysis_result> mars;
        private Result_tb_lib rtl { get; set; }
        private Match_analysis_result mar { get; set; }
        private string strNode { get; set; }
        private MacauPredication mp { get; set; }
        //private  TreeNode _treeViewMatch;

        //DataClassesMatchDataContext2 dcmdc2 = new DataClassesMatchDataContext2(Conn.conn);

        public LoadAnalysisReview()
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                marAll = matches.Match_analysis_result
                    .Where(e => e.Pre_algorithm != "top20").ToLookup(e => e.Live_table_lib_id);
                ltlAll = matches.Live_Table_lib.ToDictionary(e => e.Live_table_lib_id);
                lsAll = matches.Live_Single.ToLookup(e => e.Home_team_big + "-" + e.Away_team_big);
                mpAll = matches.MacauPredication.OrderByDescending(e => e.MacauPredication_id)
                    .Where(e => e.Home_team != null && e.Away_team != null)
                    .ToLookup(e => e.Home_team);
                rtlAll = matches.Result_tb_lib.ToDictionary(e => e.Result_tb_lib_id);
            }
        }
        public void insertSQL()
        {
            Conn.CreateTable(typeof(Analysis_Review));
            InsertSQL(UpdateARdata());
            MessageBox.Show("OK");
        }

        public int MarCount()
        {
            return marAll.Count();

        }
        private void InsertSQL(IEnumerable<Analysis_Review> ar)
        {

            using (SqlConnection con = new SqlConnection(Conn.conn))
            {
                con.Open();
                using (SqlTransaction tran = con.BeginTransaction())
                {
                    var newOrders = ar;
                    SqlBulkCopy bc = new SqlBulkCopy(con,
                        //SqlBulkCopyOptions.CheckConstraints |
                        //SqlBulkCopyOptions.FireTriggers |
                      SqlBulkCopyOptions.KeepNulls, tran);
                    bc.BulkCopyTimeout = 36000;
                    bc.BatchSize = 10000;
                    bc.DestinationTableName = "Analysis_Review";
                    bc.WriteToServer(newOrders.AsDataReader());
                    tran.Commit();
                }
                con.Close();
            }
        }
        private IEnumerable<Analysis_Review> UpdateARdata()
        {
            int i = 0;
            var ltlmars = from p in ltlAll
                          join q in marAll on p.Key equals q.Key
                          select p;

            foreach (var ltls in ltlmars)
            {
                i++;
                ProgressBarDelegate.DoSendPMessage(i);
                Application.DoEvents();

                var ltl = ltls.Value;
                Analysis_Review arevew = new Analysis_Review();
                myfit = "";
                macau = "";
                bjpk = 0;
                arevew.Live_table_lib_id = ltl.Live_table_lib_id;
                arevew.Match_time = ltl.Match_time;
                arevew.Match_type = ltl.Match_type;
                arevew.Home_team = ltl.Home_team;
                arevew.Away_team = ltl.Away_team;
                arevew.Home_team_big = ltl.Home_team_big;
                arevew.Away_team_big = ltl.Away_team_big;
                arevew.asia_odds = ltl.Status;
                var sg = lsAll[ltl.Home_team_big.ToString() + "-" + ltl.Away_team_big.ToString()]
                    .OrderByDescending(e => e.Live_Single_id).FirstOrDefault();
                if (sg != null)
                {
                    arevew.bj_host_odds = sg.Status;
                    arevew.bj_match_number = sg.Html_position;
                    double.TryParse(sg.Status, out bjpk);
                }

                mar = marAll[ltl.Live_table_lib_id].OrderByDescending(o => o.Analysis_result_id).FirstOrDefault();

                if (mar != null)
                    if (mar.Fit_win_loss != null)
                    {
                        smp = mpAll.Where(e => ltl.Home_team.Replace("(", "").Replace(")", "").IndexOf(e.Key) != -1)
                           .Select(e => e.Key).FirstOrDefault();
                        if (smp != null)
                        {
                            mp = mpAll[smp]
                             .Where(e => ltl.Away_team.Replace("(", "").Replace(")", "").IndexOf(e.Away_team) != -1)
                             .OrderByDescending(e => e.MacauPredication_id).FirstOrDefault();

                            if (mp != null)
                                macau = mp.Predication;
                        }
                        arevew.Analysis_result_id = mar.Analysis_result_id;
                        arevew.Pnn_fit = mar.Pnn_fit;
                        arevew.Grnn_fit = mar.Grnn_fit;
                        arevew.result_grnn = mar.Result_fit;
                        arevew.composite_fit = mar.Myfit;
                        arevew.Macau_predication = macau;
                        myfit = mar.Fit_win_loss.ToString();
                        myfit = myfit.Length < 5 ? myfit : myfit.Substring(0, 5);
                        arevew.Result_goals = mar.Result_goals;
                        arevew.Result_wdl = mar.Result_wdl;
                        arevew.ls_fit = double.Parse(myfit);
                        arevew.Home_goals = mar.Home_goals;
                        arevew.Away_goals = mar.Away_goals;
                        arevew.Home_w = mar.Home_w;
                        arevew.Home_d = mar.Home_d;
                        arevew.Home_l = mar.Home_l;
                        if (mar.Result_tb_lib_id != null)
                        {
                            if (mar.Result_tb_lib_id != null)
                            {
                                rtl = rtlAll[(int)mar.Result_tb_lib_id];
                                arevew.Result_tb_lib_id = rtl.Result_tb_lib_id;
                                arevew.full_home_goals = rtl.Full_home_goals;
                                arevew.full_away_goals = rtl.Full_away_goals;
                                arevew.asia_odds = rtl.Odds;
                            }
                        }
                    }
                int beijing = 0;
                SelectBjMatch(mar.Grnn_fit, bjpk, mar.Result_fit, ref beijing);
                arevew.bj_select = beijing;

                yield return arevew;
                //dcmdc2.Analysis_Review.InsertOnSubmit(arevew);
                //dcmdc2.SubmitChanges();
            }
        }
    }
}
