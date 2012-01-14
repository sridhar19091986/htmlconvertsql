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
    public partial class Form1 : Form
    {

        #region Match_analysis_result和Live_Table_lib 数据恢复


        #endregion

        #region Result_tb_lib和Result_tb 数据恢复



        #endregion
        private void btnInitResult_Click(object sender, EventArgs e)
        {
            DialogResult result; //Messagebox所属于的类
            result = MessageBox.Show(this, "YesOrNo", "你确定要删除分析库？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)//Messagebox返回的值
            {
                try
                {


                    //Conn.CreateTable(typeof(Live_Aibo));
                    //Conn.CreateTable(typeof(Live_okoo));
                    //Conn.CreateTable(typeof(Live_Single));
                    //Conn.CreateTable(typeof(Live_Table));
                    //Conn.CreateTable(typeof(Live_Table_lib));
                    //Conn.CreateTable(typeof(MacauPredication));
                    //Conn.CreateTable(typeof(Match_analysis_collection));
                    //Conn.CreateTable(typeof(Match_analysis_result));
                    ////Conn.CreateTable(typeof(Match_table_xpath));
                    //Conn.CreateTable(typeof(Result_tb));
                    //Conn.CreateTable(typeof(Result_tb_lib));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            DialogResult result; //Messagebox所属于的类
            result = MessageBox.Show(this, "YesOrNo", "你确定要删除分析库？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)//Messagebox返回的值
            {
                try
                {
                    if (Conn.CreateTable(typeof(Match_analysis_result))
                        && Conn.CreateTable(typeof(Live_Table_lib)))
                        MessageBox.Show("OK");
                    Conn.CompressCompact();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }


        private void button24_Click_2(object sender, EventArgs eeee)
        {
            DialogResult result; //Messagebox所属于的类
            result = MessageBox.Show(this, "YesOrNo", "你确定要删除分析库？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)//Messagebox返回的值
            {
                //提取去年的数据
                DateTime dt = DateTime.Now.AddYears(-1);

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
                MessageBox.Show("OK");
            }
        }



        private void button25_Click_1(object sender, EventArgs eeee)
        {

            int overday = -370;

            //try
            //{
            MessageBox.Show(overday.ToString());
            AuditForecastAlgorithm f = new AuditForecastAlgorithm(overday);
            int pb = f.idExc.Count();
            MessageBox.Show(pb.ToString());
            if (pb != 0)
            {
                //dMatch.dNew = false;
                //dMatch.LoadMatchData(true);

                toolStripProgressBar1.Maximum = pb;
                f.top20Algorithm();
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

            GC.Collect(); GC.Collect(); Application.DoEvents();
            MessageBox.Show("OK");


            try
            {
                UpdateAnalysisResult u = new UpdateAnalysisResult();

                //int
                pb = u.ExecUpateCount;
                MessageBox.Show(pb.ToString());
                toolStripProgressBar1.Maximum = pb;
                u.ExecUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            MessageBox.Show("OK");

        }


        private void button28_Click(object sender, EventArgs e)
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                int pb = matches.Result_tb.Count();
                toolStripProgressBar1.Maximum = pb;
                MessageBox.Show(pb.ToString());
            }
            SevenmResultToSql sevenm = new SevenmResultToSql();
            sevenm.BatchUpdateLastMatch();
        }

        //此项目不用？？？？？
        //批量导入 Result_tb_lib数据，并且做修正  ？
        private void BatchUpdateLastMatch()
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                int pb = matches.Result_tb.Count();
                toolStripProgressBar1.Maximum = pb;
                MessageBox.Show(pb.ToString());
            }
            SevenmResultToSql sevenm = new SevenmResultToSql();
            sevenm.BatchUpdateLastMatch();
        }
    }
}
