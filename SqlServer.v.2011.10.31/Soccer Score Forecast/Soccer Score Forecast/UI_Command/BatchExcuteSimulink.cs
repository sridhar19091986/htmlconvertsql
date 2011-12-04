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
        private bool OutToMatlab(string matchtype, int rowCount)
        {
            RowNumberTable rnt = new RowNumberTable(matchtype);
            if (rnt.matchNowf.Rows.Count < rowCount || rnt.matchOverf.Rows.Count < rowCount) return false;
            dataGridView2.Columns.Clear();
            dataGridView3.Columns.Clear();
            dataGridView2.DataSource = rnt.matchOverf;
            dataGridView3.DataSource = rnt.matchNowf;
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExportToExcel.ExportForDataGridview(dataGridView3, "MyGRNN", true);
        }

        private void btnSimGRNN_Click(object sender, EventArgs eee)
        {
            SimulinkGRNN();
        }
        private void SimulinkGRNN()
        {
            try
            {
                string result = null;
                ExportToExcel.DataGridView2Txt(dataGridView2, nn.tempy, 5);
                ExportToExcel.DataGridView2Txt(dataGridView3, nn.tempx, 6);


                //exe文件方式
                //result = ExportToExcel.SimulinkNN(@"D:\My Documents\MATLAB\mygrnn.exe");

                //dll文件方式
                result = nn.NewGrnn();

                richTextBox3.Text = result;

                //dataGridView3.Columns.Add("...", "...");
                dataGridView3.Columns.Add("MyGRNN", "MyGRNN");

                int colx = dataGridView3.Columns.Count - 1;
                int ix = 0;
                string[] lines = result.Split(new char[] { '\r', '\n' });
                foreach (string line in lines)
                    if (line != null)
                        if (line.Trim().Length > 0)
                            if (line.IndexOf("=") == -1)
                            {
                                dataGridView3.Rows[ix].Cells[colx].Value = line; ix++;
                            }


                using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
                {
                    int resultid = 0;
                    int col = dataGridView3.Columns.Count - 1;
                    string grnnfit = null;
                    for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                    {
                        resultid = Int32.Parse(dataGridView3.Rows[i].Cells[0].Value.ToString());
                        grnnfit = dataGridView3.Rows[i].Cells[col].Value.ToString();
                        var mar = matches.Match_analysis_result
                            .Where(e => e.Analysis_result_id == resultid).First();//查找需要更新的数据
                        mar.Grnn_fit = grnnfit.Trim();
                    }
                    matches.SubmitChanges();
                }
                //MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void button10_Click(object sender, EventArgs e)
        {
            ExportToExcel.ExportForDataGridview(dataGridView6, "CrossDetail", true);
            ExportToExcel.ExportForDataGridview(dataGridView7, "HomeTeam", true);
            ExportToExcel.ExportForDataGridview(dataGridView8, "AwayTeam", true);
        }

        NNPredication nn = new NNPredication();
        private void btnSimPNN_Click(object sender, EventArgs eee)
        {
            SimulinkPNN();
        }
        private void SimulinkPNN()
        {
            try
            {
                string result = null;
                //ExportToExcel.DataGridView2Txt(dataGridView2, @"D:\My Documents\MATLAB\yn.txt", 5);
                //ExportToExcel.DataGridView2Txt(dataGridView3, @"D:\My Documents\MATLAB\xite.txt", 6);

                //exe文件方式
                //result = ExportToExcel.SimulinkNN(@"D:\My Documents\MATLAB\mypnn.exe");

                //dll文件方式
                result = nn.NewPnn();

                richTextBox3.Text = result;

                //dataGridView3.Columns.Add("...", "...");
                dataGridView3.Columns.Add("MyPNN", "MyPNN");

                int colx = dataGridView3.Columns.Count - 1;
                int ix = 0;
                string[] lines = result.Split(new char[] { '\r', '\n' });
                foreach (string line in lines)
                    if (line != null)
                        if (line.Trim().Length > 0)
                            if (line.IndexOf("=") == -1)
                            {
                                dataGridView3.Rows[ix].Cells[colx].Value = line; ix++;
                            }


                using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
                {
                    int resultid = 0;
                    int col = dataGridView3.Columns.Count - 1;
                    string pnnfit = null;
                    for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                    {
                        resultid = Int32.Parse(dataGridView3.Rows[i].Cells[0].Value.ToString());
                        pnnfit = dataGridView3.Rows[i].Cells[col].Value.ToString();
                        var mar = matches.Match_analysis_result
                            .Where(e => e.Analysis_result_id == resultid).First();//查找需要更新的数据
                        mar.Pnn_fit = pnnfit.Trim();
                    }
                    matches.SubmitChanges();
                }
                //MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSimWNN_Click(object sender, EventArgs e)
        {
            try
            {
                string result = null;
                //总是出现问题
                //result = nn.NewffWavelet();
                //richTextBox3.Text = result;

                //用exe的方式看看
                result = ExportToExcel.SimulinkNN(@"D:\My Documents\MATLAB\mywavelet.exe");
                richTextBox3.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /*
         * 这个取消  ，有重复
        private void BatchExcuteSim()
        {

            
            //重新实例化一个对象以更新数据
            treeView5.Nodes.Clear();
            loaddatatree.TreeViewMatch(treeView5, "type");

            //2011.8.9 修正需要重新启动程序的问题
            loaddatatree = new LoadDataToTree(ViewMatchOverDays, filterMatchPath, false);
            

            this.tabControl1.SelectedTab = this.tabPage3;

            Application.DoEvents();

            List<string> mtlist = new List<string>();

            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                mtlist = matches.Live_Table_lib.Where(m => m.Match_time.Value.Date >= DateTime.Now.AddDays(ViewMatchOverDays).Date)
                .Select(e => e.Match_type).Distinct().ToList();
            }

            int pb = f.idExc.Count();
            MessageBox.Show(pb.ToString());
            if (pb != 0)
            {
                toolStripProgressBar1.Maximum = pb;
                f.top20Algorithm();
            }

            foreach (string matchtype in mtlist)
            {
                label9.Text = matchtype;
                Application.DoEvents();

                if (OutToMatlab(matchtype, 1) == false) continue;

                Application.DoEvents();
                SimulinkGRNN();

                Application.DoEvents();
                SimulinkPNN();

                //Application.DoEvents();
                //GC.Collect(); GC.Collect(); Application.DoEvents();
            }
        }

*/
    }
}
