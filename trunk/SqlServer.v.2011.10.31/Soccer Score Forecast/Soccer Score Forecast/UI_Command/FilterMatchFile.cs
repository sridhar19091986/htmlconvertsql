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
        private void button16_Click(object sender, EventArgs c)
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                var idExc = matches.Live_Table_lib
                   .Where(e => e.Match_time.Value.Date >= DateTime.Now.AddDays(ViewMatchOverDays).Date)
                   .Select(e => e.Match_type).Distinct();
                foreach (string matchtype in idExc)
                    treeView6.Nodes.Add(matchtype);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filterMatchPath, true, System.Text.Encoding.Default))
            {
                file.WriteLine("\n");
                file.WriteLine(treeView6.SelectedNode.Text);
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now.AddDays(-1);
            string strDT = dt.ToString("yyyy-MM-dd");//日历组建日期字符串格式化方法
            checkBox8.Text = "http://ms.7m.cn/result_big.shtml?dt=" + strDT;
            textBox3.Text = checkBox8.Text;
            Thread.Sleep(500);//达不到要求的效果，后台axWebBrowser1，主程序全部暂停
            //button17.PerformClick();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Text = "http://ms.7m.cn/default_big.shtml";
            Thread.Sleep(500);//达不到要求的效果，后台axWebBrowser1，主程序全部暂停
            //button17.PerformClick();
        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Text = "http://ms.7m.cn/played_big.shtml";
            Thread.Sleep(500);//达不到要求的效果，后台axWebBrowser1，主程序全部暂停
            //button17.PerformClick();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Text = "http://1x2.bet007.com/";
            Thread.Sleep(500);//达不到要求的效果，后台axWebBrowser1，主程序全部暂停
            //button16.PerformClick();
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Text = "http://1x2.bet007.com/bet007history.aspx";
            Thread.Sleep(500);//达不到要求的效果，后台axWebBrowser1，主程序全部暂停
            //button16.PerformClick();
        }
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now.AddDays(-1);
            string strDT = dt.ToString("yyyy-MM-dd");//日历组建日期字符串格式化方法
            checkBox7.Text = "http://1x2.bet007.com/bet007history.aspx?id=&company=&matchdate=" + strDT;
            textBox3.Text = checkBox7.Text;
            Thread.Sleep(500);//达不到要求的效果，后台axWebBrowser1，主程序全部暂停
            //button16.PerformClick();
        }
    
    }
}
