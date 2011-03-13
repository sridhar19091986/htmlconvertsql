//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
//using System.IO;
//using System.Text;
//using System.Threading;
//using System.Windows.Forms;
//using MathWorks.MATLAB.NET.Arrays;
//using System.Runtime.InteropServices;
//using System.Text.RegularExpressions;
//using mshtml;
//using System.Net;
//using System.Data.Linq;
//using System.Data.Linq.Mapping;
//using System.Reflection;
//using System.Linq.Expressions;
//using System.ComponentModel;
//using HtmlAgilityPack;
//using Soccer_Score_Forecast.LinqSql;
//using System.Linq;
//using System.Collections;

//namespace Soccer_Score_Forecast
//{
//    ////GDI预测分析图线制作和结果呈现
//    //class DrawMatchScore
//    //{
//    //    //保存一个变量的值 以 在其他地方调用
//    //    private static  int _maxMatchTimeDiff;
//    //    //根据id生成坐标数据
//    //    public static List<MatchPoint<int>> RealMatch(int lvl_id)
//    //    {
//    //        List<MatchPoint<int>> matchpoints = new List<MatchPoint<int>>();
//    //        RowNumberLimit r = new RowNumberLimit(lvl_id);
//    //        _maxMatchTimeDiff = r.NowMatchTimeDiff;
//    //        matchpoints = r.ListMatchPointData;
//    //        return matchpoints;
//    //    }
//    //    //根据坐标数据生成点?转换精度
//    //    //GDI描绘坐标系统
//    //    public static void DrawCoordinate(PictureBox pb, List<MatchPoint<int >> matchpoint)
//    //    {
//    //        pb.Refresh(); //画布清理
//    //        Graphics g = pb.CreateGraphics();   //用画板的CreateGraphics方法得到名为 pictureBox1的Graphics对象    
//    //        g.TranslateTransform(0, pb.Height / 2); //平移变换,将Y坐标下移 pictureBox1的高度  
//    //        g.ScaleTransform(1, -1);                     //缩放变换,Y坐标缩放比例为-1以完成Y的反向  

//    //        int nowdiff = _maxMatchTimeDiff;

//    //        int mx = nowdiff;
//    //        int my1 = matchpoint.Max(o => o.LastMatchScore);
//    //        int my2 = matchpoint.Min(o => o.LastMatchScore);
//    //        int my = Math.Abs(my1) > Math.Abs(my2) ? Math.Abs(my1) : Math.Abs(my2);

//    //        using (Pen blackPen = new Pen(Color.Gray, 1))
//    //        {
//    //            PointF p1 = new Point(0, 0);
//    //            g.DrawLine(blackPen, p1, new Point(pb.Width, 0));
//    //            foreach (var m in matchpoint)
//    //            {
//    //                int mX = m.LastMatchOverTime;
//    //                int mY = m.LastMatchScore;
//    //                float X = pb.Width * mX / mx;         //在此必须要做类型定义
//    //                float Y = (pb.Height / 2) * mY / my;  //???????
//    //                PointF p = new PointF(X, Y);

//    //                g.ScaleTransform(1, -1);                     //缩放变换,Y坐标缩放比例为-1以完成Y的反向  
//    //                //g.DrawString(m.Sdate, new Font("Verdana", 8), Brushes.Red, p.X, -p.Y-10);
//    //                //g.DrawString(m.Score  , new Font("Verdana", 8), Brushes.Blue,p.X ,-p.Y );
//    //                g.ScaleTransform(1, -1);                     //缩放变换,Y坐标缩放比例为-1以完成Y的反向  

//    //                if (p.X == 0) { p1 = p; }
//    //                using (Pen orangePen = new Pen(Color.Blue, 3))
//    //                {
//    //                    g.DrawLine(orangePen, p1, p);
//    //                }
//    //                p1 = p;
//    //            }
//    //        }
//    //        using (Pen orangePen = new Pen(Color.OrangeRed, 3))
//    //        {
//    //            List<MatchPoint<float>> fit = CsharpMatlab.ployfitSeriesWDL (matchpoint, nowdiff );
//    //            int i = 0;
//    //            PointF p1 = new Point(0, 0);
//    //            foreach (var m in fit)
//    //            {
//    //                i++;
//    //                float mX = m.LastMatchOverTime;
//    //                float mY = m.LastMatchScore;
//    //                float X = pb.Width * mX / mx;         //在此必须要做类型定义
//    //                float Y = (pb.Height / 2) * mY / my;  //???????
//    //                PointF p = new PointF(X, Y);
//    //                if (i == 0) { p1 = p; }
//    //                g.DrawLine(orangePen, p1, p);
//    //                p1 = p;
//    //            }
//    //        }
//    //    }

//        ////根据坐标数据生成点?转换精度
//        ////GDI描绘坐标系统
//        //public static void DrawCurveFit(PictureBox pb, List<MatchPoint<int>> matchpoint, int lvl_id)
//        //{
//        //    Graphics g = pb.CreateGraphics();
//        //    List<MatchPoint<float>> fit = CsharpMatlab.ployfitSeries(matchpoint, lvl_id);
//        //    float mx = fit.Max(o => o.LastMatchOverTime);
//        //    float my1 = fit.Max(o => o.LastMatchScore);
//        //    float my2 = fit.Min(o => o.LastMatchScore);
//        //    float my = Math.Abs(my1) > Math.Abs(my2) ? Math.Abs(my1) : Math.Abs(my2);
//        //    PointF p1 = new Point(0, 0);
//        //    int i = 0;
//        //    foreach (var m in fit)
//        //    {
//        //        i++;
//        //        float mX = m.LastMatchOverTime;
//        //        float mY = m.LastMatchScore;
//        //        float X = pb.Width * mX / mx;         //在此必须要做类型定义
//        //        float Y = (pb.Height / 2) * mY / my;  //???????
//        //        PointF p = new PointF(X, Y);
//        //        if (i  == 0) { p1 = p; }
//        //        using (Pen orangePen = new Pen(Color.OrangeRed, 3))
//        //        {
//        //            g.DrawLine(orangePen, p1, p);
//        //        }
//        //        p1 = p;
//        //    }
//        //}
//        //根据id生成坐标数据
//        //public static int NowDiff(int lvl_id)
//        //{
//        //    int nowdiff = 0;
//        //    List<MatchPoint<int>> matchpoints = new List<MatchPoint<int>>();
//        //    DataClassesMatchDataContext matches = new DataClassesMatchDataContext();
//        //    var l = matches.live_Table_lib.Where(e => e.live_table_lib_id == lvl_id).First();
//        //    var top20h = matches.result_tb_lib.Where(e => e.home_team_big == l.home_team_big || e.away_team_big == l.away_team_big);
//        //    var top20a = matches.result_tb_lib.Where(e => e.home_team_big == l.away_team_big || e.away_team_big == l.home_team_big);
//        //    var top20 = top20h.Union(top20a).OrderByDescending(e => e.match_time).Take(40);
//        //    var reTOP20 = top20.OrderBy(o => o.match_time);
//        //    var minDT = reTOP20.Select(o => o.match_time).First();
//        //    MatchPoint<int> p = new MatchPoint<int>();
//        //    TimeSpan minDif = l.match_time.Value - minDT.Value;
//        //    nowdiff = minDif.Days;
//        //    return nowdiff;
//        //}

//        //GDI描绘历史实际结果的折线图走势3
//        //private void DrawRealLastMatch(PictureBox pb, Graphics g, List<MatchPoint> matchpoint)
//        //{
//        //    using (Pen greenPen = new Pen(Color.Orange, 1))
//        //    {
//        //        Point p1 = new Point(0, 0);

//        //        for (int x = 0; x < matchpoint.Count ; x++)
//        //        {
//        //            Point p2 = new Point(x * (pb.Width / 40), y * (pb.Height / 10));
//        //            if (x == 0) { p1 = p2; }
//        //            g.DrawLine(greenPen, p1, p2);
//        //        }
//        //    }

//        //}
//        //private void DrawForecastLastMatch(PictureBox pb, Graphics g, MatchPoint[] forecastpoint)
//        //{
//        //    //GDI描绘历史实际结果的光滑曲线走势

//        //    string strResultA = textBox2.Text;//历史实际结果字符串
//        //    richTextBox3.Text = CsharpMatlab.ployfitStr(textBox2.Text);
//        //    string strToday = CsharpMatlab.ployfitVal(textBox2.Text);//本次预测结果字符串-------------------------------------------------->
//        //    toolStripTextBox3.Text = "本次预测:" + strToday.Substring(0, 5);
//        //    if (strToday.IndexOf("-") != -1)
//        //    { toolStripTextBox3.ForeColor = Color.Blue; }
//        //    else { toolStripTextBox3.ForeColor = Color.Red; }

//        //    using (Pen orangePen = new Pen(Color.Blue, 3))
//        //    {
//        //        PointF p1 = new PointF(0, 0);
//        //        string[] yS = richTextBox3.Text.Trim().Split();
//        //        int size = yS.Length;
//        //        for (int j = 0; j < size; j++)
//        //        {
//        //            double dby = Convert.ToDouble(yS[j]);
//        //            dby = 1000 * dby;
//        //            float y = (float)dby;
//        //            PointF p2 = new PointF((j) * (pictureBox1.Width / 40), (y / 1000) * (pictureBox1.Height / 10));
//        //            if (j == 0) { p1 = p2; }
//        //            g.DrawLine(orangePen, p1, p2);
//        //            p1 = p2;
//        //        }
//        //    }
//    }
//    //private void ccc()
//    //{

//    //    //GDI描绘历史预测的光滑曲线走势

//    //    string revForcast = null;
//    //    for (int x = 0; x < dt1.Rows.Count; x++)
//    //    {
//    //        string homeONaway = dt1.Rows[x]["AwayTeam_big"].ToString();//主在客
//    //        string awayONhome = dt1.Rows[x]["HomeTeam_big"].ToString();//客在主
//    //        string forecastNum = dt1.Rows[x]["ShowAnalyse_big"].ToString();//数字做颠倒
//    //        if (forecastNum.IndexOf("big") == -1)
//    //        {
//    //            if (homeONaway == toolStripStatusLabel1.Text || awayONhome == toolStripStatusLabel2.Text)
//    //            {
//    //                double dby = Convert.ToDouble(forecastNum);
//    //                dby = -dby;
//    //                forecastNum = dby.ToString();
//    //            }
//    //            revForcast += forecastNum + " ";
//    //        }
//    //        else
//    //        {
//    //            revForcast += "0" + " ";
//    //        }
//    //    }

//    //    string strResultB = revForcast;//历史预测结果字符串
//    //    textBox2.Text = revForcast;
//    //    richTextBox3.Text = CsharpMatlab.ployfitStr(textBox2.Text);

//    //    using (Pen redPen = new Pen(Color.Red, 2))
//    //    {
//    //        PointF p1 = new PointF(0, 0);
//    //        string[] yS = richTextBox3.Text.Trim().Split();
//    //        int size = yS.Length;
//    //        for (int j = 0; j < size; j++)
//    //        {
//    //            double dby = Convert.ToDouble(yS[j]);
//    //            dby = 1000 * dby;
//    //            float y = (float)dby;
//    //            PointF p2 = new PointF((j) * (pictureBox1.Width / 40), (y / 1000) * (pictureBox1.Height / 10));
//    //            if (j == 0) { p1 = p2; }
//    //            g.DrawLine(redPen, p1, p2);
//    //            p1 = p2;
//    //        }
//    //        double dby2 = Convert.ToDouble(strToday);
//    //        dby2 = 1000 * dby2;
//    //        float y2 = (float)dby2;
//    //        PointF p22 = new PointF((size) * (pictureBox1.Width / 40), (y2 / 1000) * (pictureBox1.Height / 10));
//    //        g.DrawLine(redPen, p1, p22);
//    //        g.ScaleTransform(1, -1);                     //缩放变换,Y坐标缩放比例为-1以完成Y的反向  
//    //        g.DrawString(toolStripStatusLabel3.Text, new Font("Verdana", 8), Brushes.Blue, 0, pictureBox1.Height / 3);
//    //        g.ScaleTransform(1, -1);                     //缩放变换,Y坐标缩放比例为-1以完成Y的反向  
//    //    }
//    //    g.Dispose();

//    //}
//    //private void cccd()
//    //{
//    //    //计算预测的胜率

//    //    int w = 0;
//    //    int l = 0;
//    //    int d = 0;
//    //    string[] ra = strResultA.Trim().Split();
//    //    string[] rb = strResultB.Trim().Split();
//    //    int s = ra.Length;
//    //    for (int j = 0; j < s; j++)
//    //    {
//    //        int a = Convert.ToInt16(ra[j]);
//    //        double b = Convert.ToDouble(rb[j]);
//    //        double c = a * b;
//    //        if (c > 0)
//    //        {
//    //            w++;
//    //        }
//    //        else
//    //        {
//    //            if (c == 0)
//    //            { d++; }
//    //            else
//    //            { l++; }

//    //        }
//    //    }
//    //    richTextBox3.Text = w.ToString() + "_" + d.ToString() + "_" + l.ToString();
//    //    int wdl = w + d + l;
//    //    int ww = 100 * w / wdl;
//    //    //if (ww>40)
//    //    //{
//    //    //e.Node.ForeColor  = Color.Blue;//标识颜色
//    //    Acc.RunNoQuery("update MatchToday  set Odds='[" + toolStripTextBox3.Text
//    //    + "]' where HomeTeam_big='" + toolStripStatusLabel1.Text
//    //    + "' or AwayTeam_big='" + toolStripStatusLabel2.Text + "'");   //数据库更新预测结果
//    //    //}
//    //    textBox2.Text = "预测正确率:" + ww.ToString() + "%";
//    //    Acc.RunNoQuery("update MatchToday  set WinDLoss='[" + textBox2.Text
//    //       + "]' where HomeTeam_big='" + toolStripStatusLabel1.Text
//    //       + "' or AwayTeam_big='" + toolStripStatusLabel2.Text + "'");   //数据库更新预测结果




//    //}
//    //private void ccce()
//    //{
//    //    //读入SP值数据

//    //    try
//    //    {
//    //        listBox2.Items.Clear();
//    //        DataTable dtLiveOdds = Acc.RunQuery("select DDate,HomeTeam,AwayTeam,LiveOdds from LiveOddsTeamName where instr(1,'"
//    //                                + toolStripStatusLabel3.Text + "',HomeTeam)>2 and instr(1, '%"
//    //                                + toolStripStatusLabel3.Text + "',AwayTeam)>2   order by id desc");

//    //        int rOdds = dtLiveOdds.Rows.Count;
//    //        string strOdds = null;
//    //        string strO = "+";
//    //        for (int i = 0; i < rOdds; i++)
//    //        {
//    //            strO = dtLiveOdds.Rows[i][3].ToString();
//    //            strOdds = dtLiveOdds.Rows[i][0].ToString() + "," + dtLiveOdds.Rows[i][1].ToString()
//    //                + "," + dtLiveOdds.Rows[i][2].ToString() + "," + dtLiveOdds.Rows[i][3].ToString();
//    //            listBox2.Items.Add(strOdds);
//    //        }
//    //        for (int i = 0; i < rOdds; i++)
//    //        {
//    //            if (dtLiveOdds.Rows[i][3].ToString().IndexOf("-") != -1)
//    //            {
//    //                strO = dtLiveOdds.Rows[i][3].ToString();//重新读一遍，取有结果的数据
//    //                break;
//    //            }
//    //        }
//    //        Acc.RunNoQuery("update MatchToday  set HalfTime='" + strO
//    //        + "' where HomeTeam_big='" + toolStripStatusLabel1.Text
//    //        + "' or AwayTeam_big='" + toolStripStatusLabel2.Text + "'");  //赔率数据更新    
//    //    }
//    //    catch { }

//    //    tToolStripMenuItem.PerformClick();//执行历史查询

//    //    //保存预测结果到图片

//    //    copyBmpToFile();
//    //}

////}