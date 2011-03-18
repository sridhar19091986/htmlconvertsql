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
using SoccerScore.Compact.Linq;
using System.Linq;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;


namespace Soccer_Score_Forecast
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ProgressBarDelegate.sendPEvent += new ProgressBarDelegate.SendPMessage(this.fileConvertListProgress);
        }
        string appPath = Application.StartupPath.ToString();
        string textboxDate;
        bool liveLib;
        bool insertComplete;
        static int ViewmatchOverDays = -1;
        LoadDataToTree loaddatatree = new LoadDataToTree(ViewmatchOverDays);
        private void initTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ViewmatchOverDays.ToString());
            loaddatatree.initTreeNode(ViewmatchOverDays);
        }
        private void Form1_Load(object sender, EventArgs ee)
        {
            dataGridView5.Visible = false;
            toolStripStatusLabel2.Text = dateTimePicker2.Value.ToString("yyyy-MM-dd");//日历组建日期字符串格式化方法
            //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))
            //{
            toolStripStatusLabel3.Text = Conn.match.Result_tb_lib.Max(p => p.Match_time).Value.ToString();
            var maxtime = Conn.match.Live_Table_lib.Max(p => p.Match_time);
                toolStripStatusLabel4.Text = maxtime.HasValue ? maxtime.Value.ToString() : null;
            //}
            treeView5.Nodes.Clear();
            loaddatatree.TreeViewmatch(treeView5, "type");
            dateTimePicker1.Value = DateTime.Parse(toolStripStatusLabel3.Text);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "download table......";
            liveLib = true;
            textBox1.Text = "http://live2.7m.cn/cpk_ft.aspx?view=all&amp;match=&amp;line=no&amp;ordType=";
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.Text = "download table......";
            //liveLib = false;
            //string strdp = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            //textBox1.Text = "http://data2.7m.cn/Result_data/default_big.shtml?date=" + strdp;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            liveLib = false;
            while (dateTimePicker1.Value.Date <= DateTime.Now.AddDays(-1).Date)
            {
                string strdp = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string dp = "http://data2.7m.cn/Result_data/default_big.shtml?date=" + strdp;
                listBox1.Items.Add(dp);
                dateTimePicker1.Value = dateTimePicker1.Value.AddDays(1);
            }
            toolStripProgressBar1.Maximum = listBox1.Items.Count;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                toolStripStatusLabel1.Text = "download table......";
                toolStripProgressBar1.Value = i;
                textBox1.Text = listBox1.Items[i].ToString();
                while (!insertComplete) Application.DoEvents();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            insertComplete = false;
            this.webBrowser1.Navigate(textBox1.Text);
            if (liveLib == true)
            {
                textboxDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                int p = textBox1.Text.IndexOf("=");
                textboxDate = textBox1.Text.Substring(p + 1, 10);
            }
        }
        private void button3_Click(object sender, EventArgs c)
        {
            //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))
            //{
            var mar = from a in Conn.match.Match_analysis_result
                      join b in Conn.match.Live_Table_lib on a.Live_table_lib_id equals b.Live_table_lib_id
                          select new
                          {
                              a.Result_wdl,
                              a.Result_fit,
                              a.Result_goals,
                              b.Match_type
                          };
                var winrate = from p in mar
                              group p by p.Match_type into q
                              select new
                              {
                                  q.Key,
                                  fitW = q.Where(e => e.Match_type == q.Key).Where(e => e.Result_fit == "W").Count(),
                                  fitL = q.Where(e => e.Match_type == q.Key).Where(e => e.Result_fit == "L").Count(),
                                  goalsW = q.Where(e => e.Match_type == q.Key).Where(e => e.Result_goals == "W").Count(),
                                  goalsL = q.Where(e => e.Match_type == q.Key).Where(e => e.Result_goals == "L").Count(),
                                  wdlW = q.Where(e => e.Match_type == q.Key).Where(e => e.Result_wdl == "W").Count(),
                                  wdlL = q.Where(e => e.Match_type == q.Key).Where(e => e.Result_wdl == "L").Count(),
                              };

                dataGridView1.DataSource = winrate;
            //}
        }
        //随着鼠标选择的节点做文件路径的索引，读出treeView节点指向的txt中的sql语句
        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                string selectFile = appPath + "\\" + e.Node.Parent.Text.ToString() + "\\" + e.Node.Text.ToString();
                StreamReader reader = new StreamReader(selectFile);
                richTextBox2.Text = reader.ReadToEnd();
                reader.Close();
            }
            catch { }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs c)
        {
        }
        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
        }
        //获取数据库的数据结构
        private void button6_Click(object sender, EventArgs e)
        {
            //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))
            //{
            dataGridView1.DataSource = Conn.match.Match_table_xpath;
            //}
        }
        private void filtermatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView5.Nodes.Clear();
            loaddatatree.TreeViewmatch(treeView5, "type");
        }
        //treeView过滤操作的方法
        private void todaymatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void toolStripButton_iniLast_Click(object sender, EventArgs e)
        {
            //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))
            //{
            int pb = Conn.match.Result_tb.Count();
                toolStripProgressBar1.Maximum = pb;
                MessageBox.Show(pb.ToString());
            //}
            SevenmResultToSql sevenm = new SevenmResultToSql();
            sevenm.UpdateLastmatch();
        }
        private void fileConvertListProgress(int i)
        {
            toolStripProgressBar1.Value = i;
            toolStripLabel2.Text = i.ToString();
        }
        private void toolStripButton_iniToday_Click(object sender, EventArgs e)
        {
            SevenmLiveToSql sevenm = new SevenmLiveToSql();
            sevenm.UpdateTodaymatch();
        }
        private void toolStripButton_exitSystem_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.listView2.View = View.Details;
            listView2.Items.Clear();
            listView2.Items.Add("http://data.7m.cn/lottery/WSfc_fixture.shtml");
            listView2.Items.Add("http://buy.okooo.com/Lottery06/BJBetIndex.php?LotteryType=WDL");
            listView2.Items.Add("http://www.totochina.com/happypool/sp/sp.jsp");
            listView2.Items.Add("http://data1.7m.cn/fixture_data/default_big.shtml?date=1");
            listView2.Items.Add("http://web.macauslot.com/soccer/html/predictions.html");
            //listView2.Items.Add("http://ms.7m.cn/default_big.shtml");
            listView2.Items.Add("http://live2.7m.cn/cpk_ft.aspx?view=all&amp;match=&amp;line=no&amp;ordType=");
            listView2.Items.Add("http://live.aibo123.com/bifen/indexBeiDan.shtml?lang=&isc=");
            listView2.Items.Add("http://1x2.bet007.com/");
            string strdp = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
            string dp = "http://data2.7m.cn/Result_data/default_big.shtml?date=" + strdp;
            listView2.Items.Add(dp);

            listView2.Columns[0].Width = 500;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox4.Text = webBrowser2.Document.Body.InnerHtml;
        }
        //操作记录写入LOG文件
        private void button7_Click(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "download table......";
            webBrowser2.Navigate(textBox3.Text);
        }
        //把listView选中的内容写入窗口
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox3.Text = listView2.SelectedItems[0].Text;
                Application.DoEvents();
            }
            catch { }
        }
        //运用DateTable逐步计算，从最近到最远进行拟合
        private void toolStripButton_resultEvaluate_Click(object sender, EventArgs c)
        {
            ForecastAlgorithm f = new ForecastAlgorithm();
            int pb = f.idExc.Count();
            MessageBox.Show(pb.ToString());
            if (pb != 0)
            {
                //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))
                //{
                dmatch.dHome = Conn.match.Result_tb_lib.ToLookup(e => e.Home_team_big);
                dmatch.dAway = Conn.match.Result_tb_lib.ToLookup(e => e.Away_team_big);
                //}
                toolStripProgressBar1.Maximum = pb;
                f.top20Algorithm();
            }
        }
        private void toolStripButton_todayEvaluate_Click(object sender, EventArgs e)
        {
            UpdateAnalysisResult u = new UpdateAnalysisResult();
            int pb = u.ExecUpateCount;
            MessageBox.Show(pb.ToString());
            toolStripProgressBar1.Maximum = pb;
            u.ExecUpdate();
        }
        private void selectmatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(toolStripStatusLabel3.Text);
        }
        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
        }
        private void todaymatchTimeToolStripMenuItem_Click(object sender, EventArgs c)
        {
            treeView5.Nodes.Clear();
            loaddatatree.TreeViewmatch(treeView5, "time");
        }
        //把html生成tree的方法
        private void button8_Click(object sender, EventArgs e)
        {
            treeView3.Nodes.Clear();
            LoadHtmlToTree lhtt = new LoadHtmlToTree(webBrowser2.Document.Body.InnerHtml);
            lhtt.ConvertHtmlToTree(treeView3);
        }
        private void button9_Click(object sender, EventArgs e)
        {
        }
        private void button10_Click(object sender, EventArgs e)
        {
        }
        private void button11_Click(object sender, EventArgs e)
        {
        }
        //C# 发送Http请求 - WebClient类 WebClient位于System.Net命名空间下，通过这个类可以方便的创建Http请求并获取返回内容。
        //一、用法1 - DownloadData
        private void button12_Click(object sender, EventArgs e)
        {
            string uri = "http://data1.7m.cn/Result_data/default_big.shtml?date=2003-01-01";
            System.Net.WebClient wc = new WebClient();
            byte[] bResponse = wc.DownloadData(uri);
            string strResponse = Encoding.ASCII.GetString(bResponse);
            richTextBox5.Text = strResponse;
        }
        //二、用法2 - OpenRead 
        private void button13_Click(object sender, EventArgs e)
        {
            string uri = "http://data1.7m.cn/Result_data/default_big.shtml?date=2003-01-01";

            WebClient wc = new WebClient();
            wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            Stream st = wc.OpenRead(uri);
            StreamReader sr = new StreamReader(st);
            string res = sr.ReadToEnd();
            sr.Close();
            st.Close();
            richTextBox5.Text = res;
        }
        private void button14_Click(object sender, EventArgs e)
        {
        }
        private void button15_Click(object sender, EventArgs e)
        {
        }
        private void button16_Click(object sender, EventArgs e)
        {
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Text = "http://1x2.bet007.com/";
            Thread.Sleep(500);//达不到要求的效果，后台axWebBrowser1，主程序全部暂停
            button16.PerformClick();
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Text = "http://1x2.bet007.com/bet007history.aspx";
            Thread.Sleep(500);//达不到要求的效果，后台axWebBrowser1，主程序全部暂停
            button16.PerformClick();
        }
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now.AddDays(-1);
            string strDT = dt.ToString("yyyy-MM-dd");//日历组建日期字符串格式化方法
            checkBox7.Text = "http://1x2.bet007.com/bet007history.aspx?id=&company=&matchdate=" + strDT;
            textBox3.Text = checkBox7.Text;
            Thread.Sleep(500);//达不到要求的效果，后台axWebBrowser1，主程序全部暂停
            button16.PerformClick();
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Text = "http://live.aibo123.com/bifen/indexBeiDan.shtml?lang=&isc=";
            Thread.Sleep(500);//达不到要求的效果，后台axWebBrowser1，主程序全部暂停
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Text = "http://ms.7m.cn/default_big.shtml";
            Thread.Sleep(500);//达不到要求的效果，后台axWebBrowser1，主程序全部暂停
            button17.PerformClick();
        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Text = "http://ms.7m.cn/played_big.shtml";
            Thread.Sleep(500);//达不到要求的效果，后台axWebBrowser1，主程序全部暂停
            button17.PerformClick();
        }
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now.AddDays(-1);
            string strDT = dt.ToString("yyyy-MM-dd");//日历组建日期字符串格式化方法
            checkBox8.Text = "http://ms.7m.cn/result_big.shtml?dt=" + strDT;
            textBox3.Text = checkBox8.Text;
            Thread.Sleep(500);//达不到要求的效果，后台axWebBrowser1，主程序全部暂停
            button17.PerformClick();
        }
        private void button17_Click(object sender, EventArgs e)
        {
        }
        private void button18_Click(object sender, EventArgs e)
        {
        }
        private void treeExpandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView5.ExpandAll();
        }
        private void treeColaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView5.CollapseAll();
        }
        private void tToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void button19_Click(object sender, EventArgs e)
        {
            //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))
            //{
            dataGridView1.DataSource = Conn.match.Live_Aibo;
            //}
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string sql = @"
                            if exists (select 1
		                                from  sysobjects
	                                   where  id = object_id('Match_analysis_result')
		                                and   type = 'U')
                               drop table Match_analysis_result
                            ;

                            CREATE TABLE [dbo].[Match_analysis_result](
                                [Analysis_result_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
                                [Live_table_lib_id] [decimal] NOT NULL,
                                [Result_tb_lib_id] [decimal] NULL,
                                [Pre_algorithm] [nvarchar](20) NULL,
                                [pre_match_count] [int] NULL,
                                [Home_w] [int] NULL,
                                [Home_d] [int] NULL,
                                [Home_l] [int] NULL,
                                [Home_goals] [float] NULL,
                                [Away_goals] [float] NULL,
                                [Fit_win_loss] [float] NULL,
                                [Fit_goals] [float] NULL,
                                [Fit_odd_even] [float] NULL,
[Result_fit] [nvarchar](20) NULL,
[Result_goals] [nvarchar](20) NULL,
[Result_wdl] [nvarchar](20) NULL,
                             CONSTRAINT [PK_Match_analysis_result] PRIMARY KEY NONCLUSTERED 
                            (
                                [Analysis_result_id] ASC
                            )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF,
                                 IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                            ) ON [PRIMARY]";
            //SoccerScoreCompact match = new SoccerScoreCompact(cnn);
            DialogResult result; //Messagebox所属于的类
            result = MessageBox.Show(this, "YesOrNo", "你确定要删除分析库？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)//Messagebox返回的值
            {
                Conn.match.ExecuteCommand(sql);
                MessageBox.Show("OK");
            }
        }
        private void deleteFile(DirectoryInfo directory)
        {
            FileInfo[] files = directory.GetFiles();
            foreach (FileInfo file in files)
            {
                file.Delete();
            }
        }
        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
        }
        private void button20_Click(object sender, EventArgs e)
        {
            WebRequest req = WebRequest.Create("http://buy.okooo.com/Lottery06/BJBetIndex.php?LotteryType=WDL");
            req.Proxy = null;
            OkooHtmlToSql okoo = null;
            using (WebResponse res = req.GetResponse())
            using (Stream s = res.GetResponseStream())
            using (StreamReader sr = new StreamReader(s, System.Text.Encoding.Default))
                okoo = new OkooHtmlToSql(sr.ReadToEnd());
            toolStripLabel2.Text = okoo.updateLiveOkoo().ToString();
            MessageBox.Show("OK");
        }
        private void button21_Click(object sender, EventArgs e)
        {
            AiboLiveToSql aibo = new AiboLiveToSql(webBrowser2.Document .Body .InnerHtml );
            toolStripLabel2.Text = aibo.updateLiveAibo().ToString();
            MessageBox.Show("OK");
        }
        private void button22_Click(object sender, EventArgs e)
        {
            webBrowser2.Navigate("http://live.aibo123.com/bifen/indexBeiDan.shtml?lang=&isc=");
        }
        private void splitContainer4_Panel2_Paint(object sender, PaintEventArgs e)
        {
        }
        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            System.Timers.Timer t = new System.Timers.Timer(100);//实例化Timer类，设置间隔时间为10000毫秒； 
            t.Elapsed += new System.Timers.ElapsedEventHandler(CrossThreadFlush);//到达时间的时候执行事件； 
            t.AutoReset = false;//设置是执行一次（false）还是一直执行(true)； 
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件； 
        }
        private delegate void FlushClient();//代理
        private void CrossThreadFlush(object source, System.Timers.ElapsedEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(1000);
                ThreadFunction();//将sleep和无限循环放在等待异步的外面
            }
        }
        private void ThreadFunction()
        {
            if (this.textBox2.InvokeRequired)//等待异步
            {
                FlushClient fc = new FlushClient(ThreadFunction);
                this.Invoke(fc);//通过代理调用刷新方法
            }
            else
            {
                this.textBox2.Text = DateTime.Now.ToString();
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs ee)
        {
            MessageBox.Show(ViewmatchOverDays.ToString());
            AuditForecastAlgorithm f = new AuditForecastAlgorithm(ViewmatchOverDays);
            int pb = f.idExc.Count();
            MessageBox.Show(pb.ToString());
            if (pb != 0)
            {
                //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))
                //{
                dmatch.dHome = Conn.match.Result_tb_lib.ToLookup(e => e.Home_team_big);
                dmatch.dAway = Conn.match.Result_tb_lib.ToLookup(e => e.Away_team_big);
                //}
                toolStripProgressBar1.Maximum = pb;
                f.top20Algorithm();
            }
        }
        private void exitSystemSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void dataCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage1;
        }
        private void OddsCollectionOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage10;
        }
        private void dataEvaluateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage2;
            button19.PerformClick();
        }
        private void scoreForecastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage3;
        }
        private void winRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage11;
            button18.PerformClick();
        }
        private void splitContainer9_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void caiPiaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void button24_Click(object sender, EventArgs e)
        {
        }
        private void button25_Click(object sender, EventArgs e)
        {
        }
        private void button26_Click(object sender, EventArgs e)
        {
            //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))
            //{
            dataGridView1.DataSource = Conn.match.Live_okoo;
            //}
        }
        private void button29_Click(object sender, EventArgs e)
        {
            //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))
            //{
            dataGridView1.DataSource = Conn.match.Live_Table_lib;
            //}
        }
        private void button30_Click(object sender, EventArgs e)
        {
            //SoccerScoreCompact match = new SoccerScoreCompact(cnn);
            dataGridView1.DataSource = Conn.match.Result_tb_lib;
        }
        private void button31_Click(object sender, EventArgs e)
        {
            string PageUrl = "http://data1.7m.cn/Result_data/default_big.shtml?date=2003-07-01"; //需要获取源代码的网页
            WebRequest request = WebRequest.Create(PageUrl); //WebRequest.Create方法，返回WebRequest的子类HttpWebRequest
            WebResponse response = request.GetResponse(); //WebRequest.GetResponse方法，返回对 Internet 请求的响应
            Stream resStream = response.GetResponseStream(); //WebResponse.GetResponseStream 方法，从 Internet 资源返回数据流。 
            Encoding enc = Encoding.GetEncoding("GB2312"); // 如果是乱码就改成 utf-8 / GB2312
            StreamReader sr = new StreamReader(resStream, enc); //命名空间:System.IO。 StreamReader 类实现一个 TextReader (TextReader类，表示可读取连续字符系列的读取器)，使其以一种特定的编码从字节流中读取字符。 
            string res = sr.ReadToEnd();
            sr.Close();
            richTextBox5.Text = res;
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //DAL层，主要是负责数据的采集工作，
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete) return;
            richTextBox1.Text = webBrowser1.Document.Body.InnerHtml;
            while (richTextBox1.Text == null) return;
            toolStripStatusLabel1.Text = "download table complete!";
            //在这里写入数据
            if (liveLib == true)
            {
                toolStripLabel2.Text = Update_Live_Table(richTextBox1.Text).ToString();
            }
            else
            {
                toolStripLabel2.Text = Update_result_tb(richTextBox1.Text).ToString();
            }
            insertComplete = true;
            toolStripStatusLabel1.Text = "update table complete!";
        }
        private decimal Update_Live_Table(string html)
        {
            SevenmLiveToSql sevenlive = new SevenmLiveToSql(html);
            return sevenlive.InsertLiveHtmlTableToDB();
        }
        private decimal Update_result_tb(string html)
        {
            SevenmResultToSql sevenresult = new SevenmResultToSql(html);
            return sevenresult.InsertLastHtmlTableToDB();
        }
        private void treeView5_AfterSelect(object sender, TreeViewEventArgs c)
        {
            if (c.Node.Level == 1)
            {
                dataGridView5.Visible = true;
                //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))
                //{
                    var mar = from a in Conn.match.Match_analysis_result
                              join b in Conn.match.Live_Table_lib on a.Live_table_lib_id equals b.Live_table_lib_id
                              select new
                              {
                                  a.Result_wdl,
                                  a.Result_fit,
                                  a.Result_goals,
                                  b.Match_type
                              };
                    var winrate = from p in mar
                                  where p.Match_type == c.Node.Text
                                  group p by p.Match_type into q
                                  select new
                                  {
                                      q.Key,
                                      fitW = q.Where(e => e.Result_fit == "W").Count(),
                                      fitL = q.Where(e => e.Result_fit == "L").Count(),
                                      goalsW = q.Where(e => e.Result_goals == "W").Count(),
                                      goalsL = q.Where(e => e.Result_goals == "L").Count(),
                                      wdlW = q.Where(e => e.Result_wdl == "W").Count(),
                                      wdlL = q.Where(e => e.Result_wdl == "L").Count(),
                                  };
                    dataGridView5.DataSource = winrate;
                    var maxwin = winrate.FirstOrDefault();
                    int[] maxw = { maxwin.fitW, maxwin.fitL, maxwin.goalsW, maxwin.goalsL, maxwin.wdlW, maxwin.wdlL };
                    label3.Text = maxw.Max().ToString();
                //}
            }
            if (c.Node.Level != 2) { return; }
            dataGridView5.Visible = false;
            string selectmatch = c.Node.Text.ToString();
            string[] ar = selectmatch.Split(Convert.ToChar(','));
            int id = Int32.Parse(ar[0].ToString());
            label3.Text = LoadDataToChart.ForeCast(chart1, id, selectmatch);
            LoadDataToChart.LabelmatchDetail(chart1, PointLabelsList.GetItemText(PointLabelsList.SelectedItem));
        }
        private void treeView5_MouseDown(object sender, MouseEventArgs e)
        {
            label3.Location = new Point(e.Location.X + 100, e.Location.Y+50);
            dataGridView5.Location = new Point(e.Location.X + 30, e.Location.Y -20);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            listBox1.Text = listBox1.SelectedItem.ToString();
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ViewmatchOverDays = -(int)numericUpDown1.Value;
        }
        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text = checkedListBox2.GetItemText(checkedListBox2.SelectedItem);
            textBox5.Text = checkedListBox2.GetItemText(checkedListBox2.SelectedIndex.ToString());
        }
        private void button35_Click(object sender, EventArgs e)
        {
            ExportToExcel.ExportForDataGridview(dataGridView4, "htmlTable", true);
        }
        private void button32_Click(object sender, EventArgs e)
        {
            HtmlAgilityPackGeneric t = new HtmlAgilityPackGeneric(webBrowser2.Document.Body.InnerHtml, textBox4.Text, Int32.Parse(textBox5.Text));
            dataGridView4.DataSource = t.GetTableInnerHtml();
        }
        private void button33_Click(object sender, EventArgs e)
        {
            HtmlAgilityPackGeneric t = new HtmlAgilityPackGeneric(webBrowser2.Document.Body.InnerHtml, textBox4.Text, Int32.Parse(textBox5.Text));
            dataGridView4.DataSource = t.GetTableOutHtml();
        }
        private void button34_Click(object sender, EventArgs e)
        {
            HtmlAgilityPackGeneric t = new HtmlAgilityPackGeneric(webBrowser2.Document.Body.InnerHtml, textBox4.Text, Int32.Parse(textBox5.Text));
            dataGridView4.DataSource = t.GetTableInnerText();
        }
        private void PointLabelsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataToChart.LabelmatchDetail(chart1, PointLabelsList.GetItemText(PointLabelsList.SelectedItem));
        }
        private void button36_Click(object sender, EventArgs e)
        {
            OkooHtmlToSql okoo = new OkooHtmlToSql(webBrowser2.Document.Body.InnerHtml);
            toolStripLabel2.Text = okoo.updateLiveOkoo().ToString();
            MessageBox.Show("OK");
        }

        private void treeView3_AfterSelect(object sender, TreeViewEventArgs e)
        {
            richTextBox6.Text = e.Node.Text;
        }

        private void button9_Click_1(object sender, EventArgs c)
        {
            Uri uri = new Uri(textBox3.Text);
            string host = uri.Host;
            //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))
            //{
                var eUri = Conn.match.Match_table_xpath.Where(e => e.Uri_host == host).FirstOrDefault();
                if (eUri == null)
                {
                    Match_table_xpath nUri = new Match_table_xpath();
                    nUri.Uri_host = host.ToString();
                    nUri.Max_table_xpath = textBox4.Text;
                    nUri.Second_table_xpath = textBox6.Text;
                    nUri.Max_table_id_value = textBox7.Text;
                    nUri.Second_table_id_value = textBox8.Text;
                    Conn.match.Match_table_xpath.InsertOnSubmit(nUri);
                }
                else
                {
                    eUri.Uri_host = host.ToString();
                    eUri.Max_table_xpath = textBox4.Text;
                    eUri.Second_table_xpath = textBox6.Text;
                    eUri.Max_table_id_value = textBox7.Text;
                    eUri.Second_table_id_value = textBox8.Text;
                    //match.Match_table_xpath.InsertOnSubmit(eUri);
                }
                Conn.match.SubmitChanges();
            //}
            MessageBox.Show("OK");
        }

        //bool web2docComplete = false;
        //private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //    while (webBrowser1.ReadyState != WebBrowserReadyState.Complete) return;
        //    web2docComplete = true;
        //}

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeNodeCollection tc = treeView3.Nodes;
            string lbTxt = listBox3.GetItemText(listBox3.SelectedItem);
            selectNode(tc, lbTxt);
        }
        //运用递归过程遍历treeView的所有的节点
        public void selectNode(TreeNodeCollection tc, string lbTxt)
        {
            foreach (TreeNode node in tc)
            {
                string[] tvNode = node.Text.Split(':');
                if (lbTxt == tvNode[0].Trim())
                {
                    richTextBox6.Text = node.Text;
                }
                selectNode(node.Nodes, lbTxt);
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            #region 代码测试 ,自动解析所有类型的网页的表格 ,写入table or sqlserver
            try
            {
                checkedListBox2.Items.Clear();
                listBox3.Items.Clear();
                HtmlAgilityPackTableXpath tableXpath = new HtmlAgilityPackTableXpath(webBrowser2.Document.Body.InnerHtml,
                  (int)numericUpDown2.Value);
                foreach (var d in tableXpath.htmlTableAttDic.OrderByDescending(v => v.Value))
                {
                    listBox3.Items.Add(d.Key);
                    listBox3.Items.Add(d.Value.ToString());
                    //if (d.Key == tableXpath.maxKey || d.Key == tableXpath.secondKey) listBox3.Items.Add("--->" + d.Key + "--->" + d.Value.ToString());
                    //else listBox3.Items.Add(d.Key + "--->" + d.Value.ToString());
                }
                textBox4.Text = tableXpath.maxKey;
                textBox6.Text = tableXpath.secondKey;
                textBox7.Text = tableXpath.maxValue;
                textBox8.Text = tableXpath.secondValue;
                checkedListBox2.Items.Add(tableXpath.maxKey);
                checkedListBox2.Items.Add(tableXpath.secondKey);
            }
            catch { }
            #endregion
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView5.Visible = false;
        }
    }
}
