﻿using System;
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

namespace Soccer_Score_Forecast
{
    public class LoadDataToTree
    {
        //private  SoccerScoreCompact match;
        private List<Live_Table_lib> ltlAll;
        private List<Result_tb_lib> rtlAll;
        private List<Match_analysis_result> marAll;
        private List<Live_Aibo> loAll;
        private IEnumerable<Live_Table_lib> ltls;
        //private IEnumerable<Match_analysis_result> mars;
        private Result_tb_lib rtl;
        private Match_analysis_result mar;
        private string strNode;
        //private  TreeNode _treeViewmatch;
        public LoadDataToTree(int daysDiff)
        {
            initTreeNode(daysDiff);
        }
        public void initTreeNode(int daysDiff)
        {
            //这个连接不能放到class中，不然取的还是缓存的数据？？？？？？？？？？？
            //对象和数据库之间会存在不能更新的问题？？？？？？？？？？？
            //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))

            //using (SoccerScoreSqlite match = new SoccerScoreSqlite(cnn))
            {
                ltlAll = Conn.match.Live_Table_lib.Where(m => m.Match_time.Value.Date >= DateTime.Now.AddDays(daysDiff).Date).OrderBy(m => m.Match_time).ToList();
                rtlAll = Conn.match.Result_tb_lib.Where(m => m.Match_time.Value.Date >= DateTime.Now.AddDays(daysDiff).Date).ToList();
                marAll = Conn.match.Match_analysis_result.Where(e => e.Live_table_lib_id > 0).ToList();
                loAll = Conn.match.Live_Aibo.Where(e => e.Live_Aibo_id > 0).ToList();
            }
        }

        public void TreeViewmatch(TreeView tv, string strType)
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
            //选定Match_type过滤
            var mt = ltlAll.Select(e => e.Match_type).Distinct();

            //类型遍历
            foreach (var m in mt)
            {
                TreeNode tn = new TreeNode(m);
                root.Nodes.Add(tn);
                ltls = ltlAll.Where(p => p.Match_type == m);
                TreeNodeLoad(tn);
            }
        }
        private void LoadTimeTree(TreeView tv)
        {
            TreeNode root = new TreeNode("Soccer Score Forecast");
            tv.Nodes.Add(root);
            //选定Match_type过滤
            var mt = ltlAll.Select(e => e.Match_time).Distinct();

            //类型遍历
            foreach (var m in mt)
            {
                TreeNode tn = new TreeNode(m.ToString());
                root.Nodes.Add(tn);
                ltls = ltlAll.Where(p => p.Match_time == m);
                TreeNodeLoad(tn);
            }
        }
        #region 相同循环体  ？？  ltl.Match_type
        private void TreeNodeLoad(TreeNode tn)
        {
            foreach (var ltl in ltls)
            {
                double? fit = 0, goals = 0, wdl = 0;
                //加入Live_Table数据
                strNode = ltl.Live_table_lib_id + "," + ltl.Match_type + "," + ltl.Match_time + "::" + ltl.Home_team + "::" + ltl.Away_team + "::" + ltl.Status;
                mar = marAll.Where(o => o.Live_table_lib_id == ltl.Live_table_lib_id).OrderByDescending(o => o.Analysis_result_id).FirstOrDefault();
                if (mar != null)  //有运行过算法
                {
                    //加入match_analysis数据
                    strNode += "||" + mar.Result_fit + "::" + mar.Result_goals + "::" + mar.Result_wdl + "::" + mar.Fit_win_loss + "::" +
                                    mar.Home_goals + "::" + mar.Away_goals + "::" + (mar.Home_goals - mar.Away_goals) + "::" +
                                    mar.Home_w.ToString() + "::" + mar.Home_d.ToString() + "::" + mar.Home_l.ToString();
                    if (mar.Result_tb_lib_id != null)  //有导入了结果
                    {
                        //加入result_tb数据
                        rtl = rtlAll.Where(e => e.Result_tb_lib_id == mar.Result_tb_lib_id).FirstOrDefault();
                        strNode += "||" + rtl.Match_time.Value.ToShortDateString() + "::" +
                                            rtl.Full_home_goals.ToString() + "-" + rtl.Full_away_goals.ToString() + "::" +
                                            rtl.Odds + "::" + rtl.Win_loss_big + "::" + rtl.Home_team + "::" + rtl.Away_team;
                    }
                    fit = mar.Fit_win_loss;
                    goals = mar.Home_goals - mar.Away_goals;
                    wdl = mar.Home_w - mar.Home_l;
                }
                //加入bj单场数据
                foreach (var lo in loAll)
                    if (ltl.Home_team.Contains(lo.MatchOrder1_HomeName) || ltl.Away_team.Contains(lo.MatchOrder1_AwayName))   //有匹配bj单场的数据
                        strNode += "********" + lo.Value + ">>" + lo.MatchOrder1_HandicapNumber;
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



