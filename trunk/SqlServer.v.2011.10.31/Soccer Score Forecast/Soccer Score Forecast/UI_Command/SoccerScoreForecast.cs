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
using SoccerScore.Compact.Linq.Review;
using Soccer_Score_Forecast.Handle;


namespace Soccer_Score_Forecast
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ProgressBarDelegate.sendPEvent += new ProgressBarDelegate.SendPMessage(this.fileConvertListProgress);
            ViewMatchOverDays = -1;
            appPath = Application.StartupPath.ToString();
            filterMatchPath = appPath + @"\FilterMatch.sdf";
            loaddatatree = new LoadDataToTree(ViewMatchOverDays, false);
        }
        string appPath;
        string textboxDate;
        bool liveLib;
        bool insertComplete;
        int ViewMatchOverDays;
        LoadDataToTree loaddatatree;
        string filterMatchPath;
        private void initTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ViewMatchOverDays.ToString());

            loaddatatree.initTreeNode(ViewMatchOverDays, false, false);
        }
        #region 软件加密模块1,检验注册码
        private void Form1_Load(object sender, EventArgs ee)
        {
            

            注册LicenseCheck();
            dataGridView5.Visible = false;
            toolStripStatusLabel2.Text = dateTimePicker2.Value.ToString("yyyy-MM-dd");//日历组建日期字符串格式化方法
            DateTime? dt, dt1;
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                dt = matches.Result_tb_lib.Max(p => p.Match_time);
                if (dt != null)
                    toolStripStatusLabel3.Text = dt.ToString();
                else
                    toolStripStatusLabel3.Text = "2002.1.1";

                dt1 = matches.Live_Table_lib.Max(p => p.Match_time);
                toolStripStatusLabel4.Text = dt1.HasValue ? dt1.Value.ToString() : null;
            }
            treeView5.Nodes.Clear();
            loaddatatree.TreeViewMatch(treeView5, "type");
            dateTimePicker1.Value = DateTime.Parse(toolStripStatusLabel3.Text);
            textBox2.Text = "1";

        }
        LincenseString ls = new LincenseString();
        LicenseCheck lc = new LicenseCheck();
        private void 注册LicenseCheck()
        {
            string appPath = Application.StartupPath.ToString();
            string licPath = appPath + "\\machine.lic";
            LicenseReadLib lr = new LicenseReadLib(ls, lc);
            if (lr.ReadLicense(licPath))
            {
                if (lr.CheckLicenseUse())
                {
                    if (lr.CheckLicenseSeries())
                    {
                        if (lr.CheckLicenseDate())
                        {
                            if (lr.CheckLicenseTimes()) { lr.WriteLicense(licPath); }
                            else { this.Close(); Application.ExitThread(); Application.Exit(); }
                        }
                        else
                        { this.Close(); Application.ExitThread(); Application.Exit(); }
                    }
                    else
                    { this.Close(); Application.ExitThread(); Application.Exit(); }
                }
                else
                { this.Close(); Application.ExitThread(); Application.Exit(); }
            }
            else
            { this.Close(); Application.ExitThread(); Application.Exit(); }
        }
        private void 校验注册号代码Btn_Click()
        {
            //检验注册号，把检验结果找一内存保存起来。不要在校验结果代码附近做任何影响程序正常工作的处理，这样不易被跟踪。
            string regTail3 = lc.GetRegTail3ByMac(ls.regDateFile);
            if (string.Compare(regTail3, 0, ls.regLicense, 12, 17) == 0)
                ls.bRegOK = true;
            else
                ls.bRegOK = false;
        }
        private int 核心功能代码代表Btn_Click()
        {
            int result;
            if (ls.bRegOK)
            {
                result = 10 + 10;
            }
            else
            {
                result = 10 + 10 + 1;  //发现注册码正常的标记为false时,对核心功能代码进行不易发觉的修改，导致结果无法使用，注：不要弹出易被跟踪的消息事件等。
            }
            return result;
        }

        private void WebLiveDataGet()
        {
            校验注册号代码Btn_Click();
            toolStripStatusLabel1.Text = "download table......";
            liveLib = true;

            //澳门盘口
            textBox1.Text = "http://live2.7m.cn/cpk_ft.aspx?view=all&amp;match=&amp;line=no&amp;ordType=";

            //立博盘口
            //textBox1.Text = "http://live2.7m.cn/lbpk_ft.aspx?view=all&amp;match=&amp;line=no&amp;ordType=";
        }
        #endregion
        bool datahtml;
        private void WebHistoryDataGet()
        {
            datahtml = false;
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
            datahtml = true;
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

        private void initAnalysisReview()
        {
            DialogResult result; //Messagebox所属于的类
            result = MessageBox.Show(this, "YesOrNo", "你确定要删除分析库？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)//Messagebox返回的值
            {
                LoadAnalysisReview lar = new LoadAnalysisReview();

                int pb = lar.MarCount();
                toolStripProgressBar1.Maximum = pb;

                lar.insertSQL();
            }
        }

        private void button3_Click(object sender, EventArgs c)
        {
            initAnalysisReview();
        }

        //获取数据库的数据结构
        private void button6_Click(object sender, EventArgs e)
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                gridControl1.DataSource = matches.Match_table_xpath;
            }
        }
        private void filterMatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //重新处理  2011.6.16
            treeView5.Nodes.Clear();
            loaddatatree = new LoadDataToTree(ViewMatchOverDays, true);
            loaddatatree.TreeViewMatch(treeView5, "type");
            GC.Collect(); GC.Collect(); Application.DoEvents();
        }
        //treeView过滤操作的方法
        private void todayMatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView5.Nodes.Clear();
            loaddatatree.TreeViewMatch(treeView5, "type");
            treeView5.ExpandAll();
        }
        private void toolStripButton_iniLast_Click(object sender, EventArgs e)
        {
            try
            {
                this.tabControl1.SelectedTab = this.tabPage1;


                if (Conn.CreateTable(typeof(Result_tb)))
                    Conn.CompressCompact();


                #region

                Application.DoEvents();

                datahtml = false;

                WebHistoryDataGet(); //button2.PerformClick();//执行历史网页采集

                while (!datahtml) Application.DoEvents();

                #endregion

                using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
                {
                    int pb = matches.Result_tb.Count();
                    toolStripProgressBar1.Maximum = pb;
                    MessageBox.Show(pb.ToString());
                }
                SevenmResultToSql sevenm = new SevenmResultToSql();
                sevenm.UpdateLastMatch();

                //执行分析数据的更新

                UpdateAnalysisResult_today();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void fileConvertListProgress(int i)
        {
            toolStripProgressBar1.Value = i;
            toolStripLabel2.Text = i.ToString();
        }
        #region 软件加密模块2，注册验证码不正确不能入库
        private void toolStripButton_iniToday_Click(object sender, EventArgs e)
        {
            try
            {
                this.tabControl1.SelectedTab = this.tabPage1;

                insertComplete = false;

                WebLiveDataGet();
                //button1.PerformClick(); //执行及时网页采集

                while (!insertComplete) Application.DoEvents();

                if (ls.bRegOK)
                {
                    int pb = Int32.Parse(toolStripLabel2.Text);
                    toolStripProgressBar1.Maximum = pb;

                    SevenmLiveToSql sevenm = new SevenmLiveToSql();
                    sevenm.UpdateTodayMatch();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion
        private void toolStripButton_exitSystem_Click(object sender, EventArgs e)
        {
            //this.Dispose();
            //this.Close();

            //2011.8.9  修正程序完成退出的问题
            Application.ExitThread();
            Application.Exit();

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
            //listView2.Items.Add("");

            listView2.Items.Add("http://live.win.7m.cn/?view=all");

            listView2.Items.Add("http://live.win.7m.cn/?view=jc");

            listView2.Items.Add("http://live.win.7m.cn/?view=sg");

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
            try
            {
                this.tabControl1.SelectedTab = this.tabPage12;

           

                ForecastAlgorithm f = new ForecastAlgorithm();
                f.DeleteRedundancy();

                int pb = f.idExc.Count();
                MessageBox.Show(pb.ToString());
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

            MessageBox.Show("OK");

        }

        private void UpdateAnalysisResult_today()
        {
            this.tabControl1.SelectedTab = this.tabPage12;

            //修正完场数据入库后不能修正错误的问题 2011.6.14

            UpdateAnalysisResult u = new UpdateAnalysisResult();

            int pb = u.ExecUpateCount;
            MessageBox.Show(pb.ToString());
            toolStripProgressBar1.Maximum = pb;
            u.ExecUpdate();

            MessageBox.Show("OK");
        }

        private void toolStripButton_todayEvaluate_Click(object sender, EventArgs e)
        {
            BatchExcuteSim(ViewMatchOverDays);
        }

        private void todayMatchTimeToolStripMenuItem_Click(object sender, EventArgs c)
        {
            treeView5.Nodes.Clear();
            loaddatatree.TreeViewMatch(treeView5, "time");
            treeView5.ExpandAll();
        }
        //把html生成tree的方法
        private void button8_Click(object sender, EventArgs e)
        {
            treeView3.Nodes.Clear();
            LoadHtmlToTree lhtt = new LoadHtmlToTree(webBrowser2.Document.Body.InnerHtml);
            lhtt.ConvertHtmlToTree(treeView3);
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



        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Text = "http://live.aibo123.com/bifen/indexBeiDan.shtml?lang=&isc=";
            Thread.Sleep(500);//达不到要求的效果，后台axWebBrowser1，主程序全部暂停
        }

        private void treeExpandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView5.ExpandAll();
        }

        private void treeColaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView5.CollapseAll();
        }
        private void todayAnalysisReview()
        {
            LoadDataMethod ldm = new LoadDataMethod();
            using (DataClassesMatchDataContext2 matches = new DataClassesMatchDataContext2(Conn.conn))
            {
                var tables = matches.Analysis_Review
                    .Where(e => e.Match_time.Value > DateTime.Now.AddDays(-1))
                    .Where(e => ldm.yesornorate.Contains(e.Match_type))
                    .Where(e => e.bj_select == 1)
                    .OrderBy(e => e.Match_time);
                gridControl1.DataSource = tables;
                gridControl1.Refresh();
                gridView1.PopulateColumns();
                DevExpress.XtraGrid.Columns.GridColumn col = gridView1.Columns[19];
                col.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
                col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                gridView1.OptionsView.ColumnAutoWidth = false;
                gridView1.BestFitColumns();
                //设置并制作Foot下面的统计信息
                gridView1.Columns[19].SummaryItem.DisplayFormat = "{0}";
                //gridView1.Columns[0].SummaryItem.FieldName = "analysis_review_id";
                gridView1.Columns[19].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            }
        }

        private void button19_Click(object sender, EventArgs eee)
        {
            todayAnalysisReview();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DialogResult result; //Messagebox所属于的类
            result = MessageBox.Show(this, "YesOrNo", "你确定要删除分析库？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)//Messagebox返回的值
            {

                UpateResult();

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
            try
            {
                WebRequest req = WebRequest.Create("http://buy.okooo.com/Lottery06/BJBetIndex.php?LotteryType=WDL");
                req.Proxy = null;
                OkooHtmlToSql okoo = null;
                using (WebResponse res = req.GetResponse())
                using (Stream s = res.GetResponseStream())
                using (StreamReader sr = new StreamReader(s, System.Text.Encoding.Default))
                    okoo = new OkooHtmlToSql(sr.ReadToEnd());
                toolStripLabel2.Text = okoo.updateLiveOkoo().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

                MessageBox.Show("OK");
            }
        }
        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                AiboLiveToSql aibo = new AiboLiveToSql(webBrowser2.Document.Body.InnerHtml);
                toolStripLabel2.Text = aibo.updateLiveAibo().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MessageBox.Show("OK");
            }
        }
        private void button22_Click(object sender, EventArgs e)
        {
            webBrowser2.Navigate("http://live.aibo123.com/bifen/indexBeiDan.shtml?lang=&isc=");
        }

        private void toolStripButton2_Click(object sender, EventArgs ee)
        {
            try
            {
                this.tabControl1.SelectedTab = this.tabPage12;
                MessageBox.Show(ViewMatchOverDays.ToString());
                AuditForecastAlgorithm f = new AuditForecastAlgorithm(ViewMatchOverDays);
                int pb = f.idExc.Count();
                MessageBox.Show(pb.ToString());
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
            finally
            {
                MessageBox.Show("OK");
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
        private void oddsCollectionOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage10;
        }
        private void dataEvaluateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage2;
            //button19.PerformClick();
            todayAnalysisReview();
        }
        private void scoreForecastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage3;
        }
        private void winRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage11;
            //button18.PerformClick();
        }

        private void button26_Click(object sender, EventArgs eee)
        {
            using (DataClassesMatchDataContext2 matches = new DataClassesMatchDataContext2(Conn.conn))
            {
                var match = from p in matches.Analysis_Review
                            //where p.Match_time.Value > DateTime.Now
                            where p.result_grnn != null
                            where p.bj_select == 1
                            group p by p.Match_type into ttt
                            select new
                            {
                                ttt.Key,
                                yes = ttt.Where(e => e.result_grnn == "yes").Count(),
                                no = ttt.Where(e => e.result_grnn == "no").Count(),
                                yesrate = 100.0 * ttt.Where(e => e.result_grnn == "yes").Count() / ttt.Count(),
                            };
                gridControl1.DataSource = match.OrderBy(e => e.yesrate);
                gridControl1.Refresh();
                gridView1.PopulateColumns();
                gridView1.OptionsView.ColumnAutoWidth = false;
                gridView1.BestFitColumns();
                //设置并制作Foot下面的统计信息
                gridView1.Columns[0].SummaryItem.DisplayFormat = "{0}";
                //gridView1.Columns[0].SummaryItem.FieldName = "yesrate";
                gridView1.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                gridControl1.DataSource = matches.Live_Table_lib;
            }
        }
        private void button30_Click(object sender, EventArgs e)
        {
            DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn);
            gridControl1.DataSource = matches.Result_tb_lib;
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
                toolStripLabel2.Text = Update_live_Table(richTextBox1.Text).ToString();
                if (toolStripLabel2.Text == "0") textBox1.Text = "about:blank";
            }
            else
            {
                toolStripLabel2.Text = Update_result_tb(richTextBox1.Text).ToString();
            }
            insertComplete = true;
            toolStripStatusLabel1.Text = "update table complete!";
        }
        private int Update_live_Table(string html)
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
            try
            {
                if (c.Node.Level == 1)
                {
                    return;
                    // ComputeFitRate(c.Node.Text); OutToMatlab(c.Node.Text, 1);
                    //button2.PerformClick();
                    //button11.PerformClick();

                }
                if (c.Node.Level != 2) { return; }
                dataGridView5.Visible = false;
                string selectMatch = c.Node.Text.ToString();
                string[] ar = selectMatch.Split(Convert.ToChar(','));
                int id = Int32.Parse(ar[0].ToString());
                label3.Text = LoadDataToChart.ForeCast(chart1, id, selectMatch);
                LoadDataToChart.LabelMatchDetail(chart1, PointLabelsList.GetItemText(PointLabelsList.SelectedItem));

                OutMatchDetail(id);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void OutMatchDetail(int matchid)
        {
            RowNumberDetail rnd = new RowNumberDetail(matchid);
            dataGridView6.DataSource = rnd.crossOver;
            dataGridView7.DataSource = rnd.homeTop20;
            dataGridView8.DataSource = rnd.awayTop20;

        }



        private void treeView5_MouseDown(object sender, MouseEventArgs e)
        {
            label3.Location = new Point(e.Location.X + 100, e.Location.Y + 50);
            dataGridView5.Location = new Point(e.Location.X + 30, e.Location.Y - 20);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            listBox1.Text = listBox1.SelectedItem.ToString();
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ViewMatchOverDays = -(int)numericUpDown1.Value;
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
            LoadDataToChart.LabelMatchDetail(chart1, PointLabelsList.GetItemText(PointLabelsList.SelectedItem));
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
            using (DataClassesMatchDataContext match = new DataClassesMatchDataContext(Conn.conn))
            {
                var eUri = match.Match_table_xpath.Where(e => e.Uri_host == host).FirstOrDefault();
                if (eUri == null)
                {
                    Match_table_xpath nUri = new Match_table_xpath();
                    nUri.Uri_host = host.ToString();
                    nUri.Max_table_xpath = textBox4.Text;
                    nUri.Second_table_xpath = textBox6.Text;
                    nUri.Max_table_id_value = textBox7.Text;
                    nUri.Second_table_id_value = textBox8.Text;
                    match.Match_table_xpath.InsertOnSubmit(nUri);
                }
                else
                {
                    eUri.Uri_host = host.ToString();
                    eUri.Max_table_xpath = textBox4.Text;
                    eUri.Second_table_xpath = textBox6.Text;
                    eUri.Max_table_id_value = textBox7.Text;
                    eUri.Second_table_id_value = textBox8.Text;
                    //match.match_table_xpath.InsertOnSubmit(eUri);
                }
                match.SubmitChanges();
            }
            MessageBox.Show("OK");
        }


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

        #region 自动升级系统
        private void toolStripButton_autoUpateSystem_Click(object sender, EventArgs e)
        {
            //AutoUpdate.FrmUpdate autoupdateform = new AutoUpdate.FrmUpdate();
            //autoupdateform.ShowDialog();

            this.tabControl1.SelectedTab = this.tabPage2;

            Application.DoEvents();

            //button3.PerformClick();

            initAnalysisReview();
        }
        #endregion

        private void importCompactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Conn.ImportSdfFile();
        }

        private void importUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Conn.ImportUpdateFile();
        }

        private void button18_Click(object sender, EventArgs eee)
        {
            BatchExcuteSim(ViewMatchOverDays);
        }

        #region html option 花我整1上午时间
        MacauslotToSql macau;
        private List<string> ParserMacau()
        {
            List<string> theList = new List<string>();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            HtmlNode.ElementsFlags.Remove("option");
            doc.LoadHtml(webBrowser2.Document.Body.InnerHtml);
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//select[@name='menu']//option"))
                if (node.InnerText.IndexOf("參考") == -1)
                    theList.Add(node.InnerText);
            return theList;
        }
        private void Macauslot_Click(object sender, EventArgs e)
        {
            try
            {
                int select = Int32.Parse(textBox2.Text); //只能通过这个textbox来循环，其他循环html的脚本有问题
                HtmlElement theList = webBrowser2.Document.GetElementsByTagName("select")[0];  //select 节点
                int optionmax = theList.GetElementsByTagName("option").Count;  //option 节点数量
                theList.Focus();
                if (select > optionmax) return;
                theList.GetElementsByTagName("option")[select].SetAttribute("selected", "true");//选定option
                theList.InvokeMember("onchange");    //不调用该方法网页将不刷新
                Application.DoEvents();
                macau = new MacauslotToSql(webBrowser2.Document.Body.InnerHtml);
                toolStripLabel2.Text = macau.updateMacauslot().ToString();
                select++;
                textBox2.Text = select.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(webBrowser2.Document.Body.InnerHtml);
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                gridControl1.DataSource = matches.MacauPredication;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                gridControl1.DataSource = matches.Live_Single;
            }
        }
        private void Livesg_Click(object sender, EventArgs e)
        {
            //try
            //{
            SevenmLiveSingleToSql sg = new SevenmLiveSingleToSql();
            toolStripLabel2.Text = sg.InsertLiveHtmlTableToDB(webBrowser2.Document.Body.OuterHtml).ToString();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(webBrowser2.Document.Body.InnerHtml);
            //    MessageBox.Show(ex.ToString());
            //}
        }

    }
}