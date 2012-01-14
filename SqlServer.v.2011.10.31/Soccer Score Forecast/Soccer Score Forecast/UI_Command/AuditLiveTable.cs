using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using SoccerScore.Compact.Linq;
using System.Linq;
using System.Data.SqlServerCe;
using HtmlAgilityPack;
using System.Data.SqlClient;
using Soccer_Score_Forecast.BulkSql;

namespace Soccer_Score_Forecast
{
    //完成2个工作：

    //1.数据完成恢复

    //2.数据定时更新恢复

    //重新再定义一个class  ????


    public partial class Form1 : Form
    {
        private DateTime? UpdateResultDateTime;
        private void UpateResult()
        {
            int overday = 0;

            DeleteLiveData();
            UpdateResultData();
            ForecastAlgorithm();

            if (UpdateResultDateTime.Value.Day == DateTime.Now.Day)
            {
                overday = -1;
            }
            else
            {
                TimeSpan ts = DateTime.Now.Subtract(UpdateResultDateTime.Value);
                overday = -1 * ts.Days - 1;//这里是负数     
            }

            AuditForecastAlgorithm(overday);
            BatchExcuteSim(overday);
            UpdateAnalysisResult();

        }

        private void ReleaseMemory()
        {
            GC.Collect();
            GC.Collect();
            Application.DoEvents();

        }
        //1.修正计算，live_id中查出特征，删除live_id,删除analysis_id.删除  双表
        //Full_home_goalsΞΞ Full_away_goalsΞΞ Half_home_goalsΞΞ Half_away_goals  is null
        private void DeleteLiveData()
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                int deleteid = 0;
                var ltls = matches.Live_Table_lib.Where(e => e.Full_home_goals == null);

                if (ltls.FirstOrDefault() != null)
                    deleteid = matches.Live_Table_lib.Where(e => e.Full_home_goals == null).Min(e => e.Live_table_lib_id);
                else
                    deleteid = matches.Live_Table_lib.Max(e => e.Live_table_lib_id);

                matches.ExecuteCommand("delete from Live_Table_lib where Live_table_lib_id>=" + deleteid);
                matches.ExecuteCommand("delete from Match_analysis_result where Live_table_lib_id>=" + deleteid);
            }
            /*
            if (Conn.CreateTable(typeof(Match_analysis_result))
             && Conn.CreateTable(typeof(Live_Table_lib)))
            { }
             * */
        }

        //2.更新 live_id to Live_Table_lib
        private void UpdateResultData()
        {
            DateTime? dt = DateTime.Now;
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                dt = matches.Live_Table_lib.Max(e => e.Match_time);
            }

            //提取去年的数据
            //DateTime dt = DateTime.Now.AddYears(-1);

            UpdateResultDateTime = dt;

            List<Live_Table_lib> ltls = new List<Live_Table_lib>();

            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                var result_tb = matches.Result_tb_lib.Where(e => e.Match_time > dt).OrderBy(e => e.Match_time);
                foreach (var r in result_tb)
                {
                    Live_Table_lib ltl = new Live_Table_lib();
                    ltl.Match_time = r.Match_time;
                    ltl.Html_position = r.Html_position;
                    ltl.Match_type = r.Match_type;
                    ltl.Home_team_big = r.Home_team_big;
                    ltl.Home_team = r.Home_team;
                    ltl.Away_red_card = r.Away_red_card;
                    ltl.Away_team_big = r.Away_team_big;
                    ltl.Away_team = r.Away_team;
                    ltl.Away_red_card = r.Away_red_card;
                    ltl.Full_home_goals = r.Full_home_goals;
                    ltl.Full_away_goals = r.Full_away_goals;
                    ltl.Half_home_goals = r.Half_home_goals;
                    ltl.Half_away_goals = r.Half_away_goals;
                    ltl.Status = r.Odds;
                    //matches.Live_Table_lib.InsertOnSubmit(ltl);
                    ltls.Add(ltl);
                }
                //matches.SubmitChanges();
            }
            using (SqlConnection con = new SqlConnection(Conn.conn))
            {
                con.Open();
                using (SqlTransaction tran = con.BeginTransaction())
                {
                    var newOrders = ltls;
                    SqlBulkCopy bc = new SqlBulkCopy(con,
                        //SqlBulkCopyOptions.CheckConstraints |
                        //SqlBulkCopyOptions.FireTriggers |
                      SqlBulkCopyOptions.KeepNulls, tran);
                    bc.BulkCopyTimeout = 36000;
                    bc.BatchSize = 10000;
                    bc.DestinationTableName = "Live_Table_lib";
                    bc.WriteToServer(newOrders.AsDataReader());
                    tran.Commit();
                }
                con.Close();
            }
            //MessageBox.Show("UpdateResultData...OK");
            toolStripLabel2.Text = "UpdateResultData...OK"; Thread.Sleep(1);
        }

        //3.更新 live_id to Match_analysis_result
        private void ForecastAlgorithm()
        {
            try
            {
                //dMatch.dNew = false;
                //dMatch.LoadMatchData(true);

                ForecastAlgorithm f = new ForecastAlgorithm();
                //f.DeleteRedundancy();

                int pb = f.idExc.Count();
                //MessageBox.Show(pb.ToString());
                toolStripLabel2.Text = pb.ToString(); Thread.Sleep(1);
                if (pb != 0)
                {
                    toolStripProgressBar1.Maximum = pb;
                    f.top20Algorithm();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //MessageBox.Show("ForecastAlgorithm...OK");
            toolStripLabel2.Text = "ForecastAlgorithm...OK"; Thread.Sleep(1);
        }

        //4.重新运算 Match_analysis_result
        private void AuditForecastAlgorithm(int overday)
        {
            //int overday = -370;
            try
            {
                //dMatch.dNew = false;
                //dMatch.LoadMatchData(true);

                //MessageBox.Show(overday.ToString());
                toolStripLabel2.Text = overday.ToString(); Thread.Sleep(1);

                AuditForecastAlgorithm f = new AuditForecastAlgorithm(overday);
                int pb = f.idExc.Count();
                //MessageBox.Show(pb.ToString());
                toolStripLabel2.Text = pb.ToString(); Thread.Sleep(1);
                if (pb != 0)
                {


                    toolStripProgressBar1.Maximum = pb;
                    f.top20Algorithm();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //MessageBox.Show("AuditForecastAlgorithm...OK");
            toolStripLabel2.Text = "AuditForecastAlgorithm...OK"; Thread.Sleep(1);
        }

        //5.仿真
        private void BatchExcuteSim(int overday)
        {
            //模拟点击必须打开当前页面 2011.11.29

            this.tabControl1.SelectedTab = this.tabPage3;

            Application.DoEvents();

            //BatchExcuteSim();
            List<string> mtlist = new List<string>();

            //这里必须重新初始化表，否则不会提取最新的数据库

            RowNumberTable rnt = new RowNumberTable();

            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                mtlist = matches.Live_Table_lib.Where(m => m.Match_time.Value.Date >= DateTime.Now.AddDays(overday).Date)
                .Select(e => e.Match_type).Distinct().ToList();
            }

            toolStripProgressBar1.Maximum = mtlist.Count();

            int i = 0;

            foreach (string matchtype in mtlist)
            {
                i++;
                ProgressBarDelegate.DoSendPMessage(i);
                label9.Text = matchtype;
                Application.DoEvents();

                if (OutToMatlab(matchtype, 1) == false) continue;

                Application.DoEvents();
                SimulinkGRNN();

                Application.DoEvents();
                SimulinkPNN();

                //Application.DoEvents();
                //GC.Collect(); GC.Collect();  Application.DoEvents();
            }
        }

        //6.更新计算结果
        //修正 Match_analysis_result
        private void UpdateAnalysisResult()
        {
            //int overday = -370;
            try
            {
                UpdateAnalysisResult u = new UpdateAnalysisResult();
                int pb = u.ExecUpateCount;
                //MessageBox.Show(pb.ToString());
                toolStripLabel2.Text = pb.ToString(); Thread.Sleep(1);

                if (pb != 0)
                {
                    toolStripProgressBar1.Maximum = pb;
                    u.ExecUpdate();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //MessageBox.Show("UpdateAnalysisResult...OK");
            toolStripLabel2.Text = "UpdateAnalysisResult...OK"; Thread.Sleep(1);
        }
    }
}
