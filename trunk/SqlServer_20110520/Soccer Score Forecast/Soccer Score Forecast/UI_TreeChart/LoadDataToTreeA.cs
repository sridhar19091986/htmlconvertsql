using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SoccerScore.Compact.Linq;
using System.Linq;
using System.Text;

namespace Soccer_Score_Forecast
{
    public partial class LoadDataToTree
    {
        //string fDraw = "";
        double? fit = 0;
        double? goals = 0;
        double? wdl = 0;
        //string fjz = "";
        string myfit = "";
        string macau = "";
        string comp = "";
        string smp = "";
        string pnncheck = "";
        //int pnngrnncomp = 1;
        //记录北京单场盘口
        int bjpk = 0;
        int grnncheck = -1;
        //计算是否有利于投资 1
        int goalnumcheck = 0;
        int fitgrnncomp = -1;
        //double preresult=0;
        StringBuilder sb = null;


        //string result = "";
        //private  DataClassesMatchDataContext dcmdc=new DataClassesMatchDataContext (Conn.conn);
        private List<Live_Table_lib> ltlAll { get; set; }
        private List<Result_tb_lib> rtlAll { get; set; }
        private ILookup<int?, Match_analysis_result> marAll { get; set; }
        private ILookup<string, MacauPredication> mpAll { get; set; }
        //private List<Live_okoo> loAll { get; set; }
        private ILookup<string, Live_Single> lsAll { get; set; }
        private IEnumerable<Live_Table_lib> ltls { get; set; }
        //private IEnumerable<match_analysis_result> mars;
        private Result_tb_lib rtl { get; set; }
        private Match_analysis_result mar { get; set; }
        private string strNode { get; set; }
        private MacauPredication mp { get; set; }
        //private  TreeNode _treeViewMatch;

        public LoadDataToTree(int daysDiff,  bool bj)
        {
            //string filterMatchPath = Application.StartupPath + @"\FilterMatch";
            //if (!File.Exists(filterMatchPath)) return;

            //List<string> matchlist = new List<string>();
            //using (StreamReader r = new StreamReader(filterMatchPath, System.Text.Encoding.Default))
            //{
            //    string line;
            //    while ((line = r.ReadLine()) != null)
            //        matchlist.Add(line);
            //}
            initTreeNode(daysDiff, false, bj);
        }

        public void initTreeNode(int daysDiff, bool ismath, bool bj)
        {
            //这个连接不能放到class中，不然取的还是缓存的数据？？？？？？？？？？？
            //对象和数据库之间会存在不能更新的问题？？？？？？？？？？？
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))

            //using (SoccerScoreSqlite matches = new SoccerScoreSqlite(cnn))
            {
                //if (ismath)
                //    ltlAll = matches.Live_Table_lib
                //        .Where(e => matchlist.Contains(e.Match_type))
                //        .Where(m => m.Match_time.Value.Date >= DateTime.Now.AddDays(daysDiff).Date)
                //        .OrderBy(m => m.Match_time).ToList();
                //else
                    ltlAll = matches.Live_Table_lib
                        .Where(m => m.Match_time.Value.Date >= DateTime.Now.AddDays(daysDiff).Date)
                        .OrderBy(m => m.Match_time).ToList();
                rtlAll = matches.Result_tb_lib.Where(m => m.Match_time.Value.Date >= DateTime.Now.AddDays(daysDiff).Date).ToList();
            
                //loAll = matches.Live_okoo.Where(e => e.Live_okoo_id > 0).ToList();
                //mpAll = matches.MacauPredication.OrderByDescending(e => e.MacauPredication_id).ToList();
                //lsAll = matches.Live_Single.ToList();

                lsAll = matches.Live_Single.ToLookup(e => e.Home_team_big + "-" + e.Away_team_big);
                mpAll = matches.MacauPredication.OrderByDescending(e => e.MacauPredication_id)
                    .Where(e => e.Home_team != null && e.Away_team != null)
                    .ToLookup(e => e.Home_team);

                //处理北京单场
                if (bj)
                    marAll = matches.Match_analysis_result
                        .Where(e => e.Pre_algorithm != "top20")
                        .Where(e => e.Live_table_lib_id  > 0).ToLookup(e => e.Live_table_lib_id);
                else
                    marAll = matches.Match_analysis_result.ToLookup(e => e.Live_table_lib_id);
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
            //选定match_type过滤
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


    }
}



