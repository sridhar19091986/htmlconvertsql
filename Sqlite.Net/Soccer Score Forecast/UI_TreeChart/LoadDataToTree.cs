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
        private List<LiveTableLib> ltlAll;
        private List<ResultTBLib> rtlAll;
        private List<MatchAnalysisResult> marAll;
        private List<LiveAibo> loAll;
        private IEnumerable<LiveTableLib> ltls;
        //private IEnumerable<MatchAnalysisResult> mars;
        private ResultTBLib rtl;
        private MatchAnalysisResult mar;
        private string strNode;
        //private  TreeNode _treeViewMatch;
        public LoadDataToTree(int daysDiff)
        {
            initTreeNode(daysDiff);
        }
        public void initTreeNode(int daysDiff)
        {
            //这个连接不能放到class中，不然取的还是缓存的数据？？？？？？？？？？？
            //对象和数据库之间会存在不能更新的问题？？？？？？？？？？？
            //DataClassesMatchDataContext matches = new DataClassesMatchDataContext();
            DateTime dt = DateTime.Now.AddDays(daysDiff).Date;

            using (SoccerScoreSqlite matches = new SoccerScoreSqlite(Conn.cnn))
            {
                ltlAll = matches.LiveTableLib.Where(m => m.MatchTime.Value.Date >= dt).OrderBy(m => m.MatchTime).ToList();
                rtlAll = matches.ResultTBLib.Where(m => m.MatchTime.Value.Date >= dt).ToList();
                marAll = matches.MatchAnalysisResult.Where(e => e.LiveTableLibID > 0).ToList();
                loAll = matches.LiveAibo .Where(e => e.LiveAiboID  > 0).ToList();
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
            //选定MatchType过滤
            var mt = ltlAll.Select(e => e.MatchType).Distinct();

            //类型遍历
            foreach (var m in mt)
            {
                TreeNode tn = new TreeNode(m);
                root.Nodes.Add(tn);
                ltls = ltlAll.Where(p => p.MatchType == m);
                TreeNodeLoad(tn);
            }
        }
        private void LoadTimeTree(TreeView tv)
        {
            TreeNode root = new TreeNode("Soccer Score Forecast");
            tv.Nodes.Add(root);
            //选定MatchType过滤
            var mt = ltlAll.Select(e => e.MatchTime).Distinct();

            //类型遍历
            foreach (var m in mt)
            {
                TreeNode tn = new TreeNode(m.ToString());
                root.Nodes.Add(tn);
                ltls = ltlAll.Where(p => p.MatchTime == m);
                TreeNodeLoad(tn);
            }
        }
        #region 相同循环体  ？？  ltl.MatchType
        private void TreeNodeLoad(TreeNode tn)
        {
            foreach (var ltl in ltls)
            {
                double? fit = 0, goals = 0, wdl = 0;
                //加入live_table数据
                strNode = ltl.LiveTableLibID + "," + ltl.MatchType + "," + ltl.MatchTime + "::" + ltl.HomeTeam + "::" + ltl.AwayTeam + "::" + ltl.Status;
                mar = marAll.Where(o => o.LiveTableLibID == ltl.LiveTableLibID).OrderByDescending(o => o.AnalysisResultID).FirstOrDefault();
                if (mar != null)  //有运行过算法
                {
                    //加入match_analysis数据
                    strNode += "||" + mar.ResultFit + "::" + mar.ResultGoals + "::" + mar.ResultWDL + "::" + mar.FitWinLoss + "::" +
                                    mar.HomeGoals + "::" + mar.AwayGoals + "::" + (mar.HomeGoals - mar.AwayGoals) + "::" +
                                    mar.HomeW.ToString() + "::" + mar.HomeD.ToString() + "::" + mar.HomeL.ToString();
                    if (mar.ResultTBLibID != null)  //有导入了结果
                    {
                        //加入result_tb数据
                        rtl = rtlAll.Where(e => e.ResultTBLibID== mar.ResultTBLibID).FirstOrDefault();
                        strNode += "||" + rtl.MatchTime.Value.ToShortDateString() + "::" +
                                            rtl.FullHomeGoals.ToString() + "-" + rtl.FullAwayGoals.ToString() + "::" +
                                            rtl.Odds + "::" + rtl.WinLossBig + "::" + rtl.HomeTeam + "::" + rtl.AwayTeam;
                    }
                    fit = mar.FitWinLoss;
                    goals = mar.HomeGoals - mar.AwayGoals;
                    wdl = mar.HomeW - mar.HomeL;
                }
                //加入bj单场数据
                foreach (var lo in loAll)
                    if (ltl.HomeTeam.Contains(lo.MatchOrder1hOmeName) || ltl.AwayTeam.Contains(lo.MatchOrder1aWayName))   //有匹配bj单场的数据
                        strNode += "********" + lo.Value + ">>" + lo.MatchOrder1hAndicapNumber;
                TreeNode child = new TreeNode(strNode);
                tn.Nodes.Add(child);
                //颜色处理
                if (fit < 0) child.ForeColor = Color.Blue;
                if (goals < 0) child.BackColor = Color.Orange;
                if (wdl < 0) child.NodeFont = new Font("Trebuchet MS", 10, FontStyle.Bold);
                if (strNode.Contains(">>")) child.Parent.ForeColor = Color.Red;
            }
        }
        #endregion
    }
}



