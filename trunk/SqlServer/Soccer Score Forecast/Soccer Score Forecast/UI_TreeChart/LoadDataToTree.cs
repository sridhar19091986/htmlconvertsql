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
using Soccer_Score_Forecast.LinqSql;
using System.Linq;
using System.Collections;

namespace Soccer_Score_Forecast
{
    public class LoadDataToTree
    {
        //private  DataClassesMatchDataContext matches;
        private List<live_Table_lib> ltlAll;
        private List<result_tb_lib> rtlAll;
        private List<match_analysis_result> marAll;
        private List<live_okoo> loAll;
        private IEnumerable<live_Table_lib> ltls;
        //private IEnumerable<match_analysis_result> mars;
        private result_tb_lib rtl;
        private match_analysis_result mar;
        private string strNode;
        //private  TreeNode _treeViewMatch;
        public LoadDataToTree(int daysDiff)
        {
            List<string> matchlist = new List<string>();
            matchlist.Add("西甲"); matchlist.Add("英超"); matchlist.Add("德甲"); matchlist.Add("意甲"); matchlist.Add("法甲");
            initTreeNode(daysDiff,matchlist);
        }
        public void initTreeNode(int daysDiff,List<string> matchlist)
        {
            //这个连接不能放到class中，不然取的还是缓存的数据？？？？？？？？？？？
            //对象和数据库之间会存在不能更新的问题？？？？？？？？？？？
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext())

            //using (SoccerScoreSqlite matches = new SoccerScoreSqlite(cnn))
            {
                ltlAll = matches.live_Table_lib
                    .Where(e=>matchlist.Contains(e.match_type))
                    .Where(m => m.match_time.Value.Date >= DateTime.Now.AddDays(daysDiff).Date)
                    .OrderBy(m => m.match_time).ToList();
                rtlAll = matches.result_tb_lib.Where(m => m.match_time.Value.Date >= DateTime.Now.AddDays(daysDiff).Date).ToList();
                marAll = matches.match_analysis_result.Where(e => e.live_table_lib_id > 0).ToList();
                loAll = matches.live_okoo.Where(e => e.live_okoo_id > 0).ToList();
            }
        }

        public void TreeViewMatch(TreeView tv, string strType)
        {
            //Action<T> 委托
            Action<TreeView> treeTarget;
            if (strType == "type")
                treeTarget = LoadTypeTree;
            else
                treeTarget = LoadTimeTree;
            treeTarget(tv);
        }

        private void LoadTypeTree(TreeView tv)
        {
            TreeNode root = new TreeNode("Soccer Score Forecast");
            tv.Nodes.Add(root);
            //选定match_type过滤
            var mt = ltlAll.Select(e => e.match_type).Distinct();

            //类型遍历
            foreach (var m in mt)
            {
                TreeNode tn = new TreeNode(m);
                root.Nodes.Add(tn);
                ltls = ltlAll.Where(p => p.match_type == m);
                TreeNodeLoad(tn);
            }
        }
        private void LoadTimeTree(TreeView tv)
        {
            TreeNode root = new TreeNode("Soccer Score Forecast");
            tv.Nodes.Add(root);
            //选定match_type过滤
            var mt = ltlAll.Select(e => e.match_time).Distinct();

            //类型遍历
            foreach (var m in mt)
            {
                TreeNode tn = new TreeNode(m.ToString());
                root.Nodes.Add(tn);
                ltls = ltlAll.Where(p => p.match_time == m);
                TreeNodeLoad(tn);
            }
        }
        #region 相同循环体  ？？  ltl.match_type
        private void TreeNodeLoad(TreeNode tn)
        {
            foreach (var ltl in ltls)
            {
                string fDraw="";
                double? fit = 0, goals = 0, wdl = 0;
                //加入live_table数据
                strNode = ltl.live_table_lib_id + "," + ltl.match_type + "," + ltl.match_time + "::" + ltl.home_team + "::" + ltl.away_team + "::" + ltl.status;
                mar = marAll.Where(o => o.live_table_lib_id == ltl.live_table_lib_id).OrderByDescending(o => o.analysis_result_id).FirstOrDefault();
                if (mar != null)  //有运行过算法
                {
                    //加入match_analysis数据
                    strNode += "||" + mar.result_fit + "::" + mar.result_goals + "::" + mar.result_wdl + "::" + mar.fit_win_loss + "::" +
                                    mar.home_goals + "::" + mar.away_goals + "::" + (mar.home_goals - mar.away_goals) + "::" +
                                    mar.home_w.ToString() + "::" + mar.home_d.ToString() + "::" + mar.home_l.ToString();
                    fDraw=ForecastDraw(mar.home_w,mar.home_d,mar.home_l);
                    if (mar.result_tb_lib_id != null)  //有导入了结果
                    {
                        //加入result_tb数据
                        rtl = rtlAll.Where(e => e.result_tb_lib_id == mar.result_tb_lib_id).FirstOrDefault();
                        strNode += "||" + rtl.match_time.Value.ToShortDateString() + "::" +
                                            rtl.full_home_goals.ToString() + "-" + rtl.full_away_goals.ToString() + "::" +
                                            rtl.odds + "::" + rtl.win_loss_big + "::" + rtl.home_team + "::" + rtl.away_team;
                    }
                    fit = mar.fit_win_loss;
                    goals = mar.home_goals - mar.away_goals;
                    wdl = mar.home_w - mar.home_l;
                }

                //加入bj单场数据
                foreach (var lo in loAll)
                    if (ltl.home_team.Contains(lo.MatchOrder1_HomeName) || ltl.away_team.Contains(lo.MatchOrder1_AwayName))   //有匹配bj单场的数据
                        strNode += "{" + lo.value + "}" + lo.MatchOrder1_HandicapNumber + "***【赔率+拟合】";
                if (ltl.home_team.IndexOf("*") != -1) strNode += "++++++{3";
                else strNode += "++++++{0";

                strNode += fDraw;

                if (fit < 0) strNode += "0}";
                else strNode += "3}";
                
                TreeNode child = new TreeNode(strNode);
                tn.Nodes.Add(child);
                //颜色处理
                if (fit < 0) child.ForeColor = Color.Blue;
                if (goals < 0) child.BackColor = Color.Orange;
                if (wdl < 0) child.NodeFont = new Font("Trebuchet MS", 10, FontStyle.Bold);
                if (strNode.Contains("***")) child.Parent.ForeColor = Color.Red;
            }
        }
        #endregion

        //List<double> minOdds = new List<double>();
        //double minOdd = 0;
        //minOdds.Add(ExtractDigital(lo.ok_1_0));
        //minOdds.Add(ExtractDigital(lo.ok_1_1));
        //minOdds.Add(ExtractDigital(lo.ok_1_2));
        //minOdd = minOdds.Min();
        //if (minOdds[0] == minOdd) strNode += "3";
        //if (minOdds[1] == minOdd) strNode += "1";
        //if (minOdds[2] == minOdd) strNode += "0";
        private double ExtractDigital(string str)
        {
            //Console.WriteLine("请输入一个字符串：");
            //string str = Console.ReadLine();
            string number = null;
            foreach (char item in str)
            {
                if (item >= 48 && item <= 58)
                {
                    number += item;
                }
                else
                {
                    break;
                }
            }
            //return Int32.Parse(number); 
            return double.Parse(number);
            // Console.WriteLine(number);
        }
        private string ForecastDraw(int? w, int? d, int? l) {
            int?[] wdl = { w, d, l };
            if (d == wdl.Min() && d!=w && d!=l) return "";
            else  return "1";
        }
    }

}



