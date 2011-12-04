using System.Collections.Generic;
using SoccerScore.Compact.Linq;
using System.Linq;
using System.Reflection;
using System.Data;

namespace Soccer_Score_Forecast
{
    public static class MatlabMatch
    {
        public static DataTable matchover = null;
        public static DataTable matchnow = null;
        public static List<StatMatch> statmatch = null;
    }
    public class StatMatch
    {
        public string Result_wdl;
        public string Result_fit;
        public string Result_goals;
        public string Match_type;
    }
    public class RowNumberTable
    {

        private string matchtype = null;



        public RowNumberTable()
        {
            InitMatchOver();
            InitMatchNow();
        }

        public RowNumberTable(string matchtype)
        {
            this.matchtype = matchtype;
        }

        public void InitMatchOver()
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                var matchover = from p in matches.Match_analysis_result
                                join q in matches.Result_tb_lib on p.Result_tb_lib_id equals q.Result_tb_lib_id
                                join t in matches.Live_Table_lib on p.Live_table_lib_id equals t.Live_table_lib_id
                                where p.Result_tb_lib_id != null && p.Result_fit != "no forecast"
                                select new
                                {
                                    t.Match_time,
                                    t.Match_type,
                                    t.Home_team,
                                    t.Away_team,
                                    q.Odds,
                                    p.Home_w,
                                    p.Home_d,
                                    p.Home_l,

                                    p.Home_goals,
                                    p.Away_goals,

                                    p.Cross_goals,

                                    p.Fit_win_loss,

                                    p.Recent_scores,
                                    p.Recent_2scores,
                                    p.Recent_3scores,
                                    p.Recent_4scores,
                                    p.Recent_5scores,
                                    p.Recent_6scores,

                                    Lottery_Ticket = q.Full_home_goals - q.Full_away_goals,
                                    q.Full_home_goals,
                                    q.Full_away_goals,
                                    Dodds = ConvertOdd(t.Home_team, q.Odds)
                                };

                //避免干扰  Where(e=>e.Home_w+e.Home_d+e.Home_l>2)
                MatlabMatch.matchover = matchover.Where(e => e.Home_w + e.Home_d + e.Home_l > 2).ToDataTable();

            }
        }

        public void InitMatchNow()
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                var matchnowf = from p in matches.Match_analysis_result
                                join t in matches.Live_Table_lib on p.Live_table_lib_id equals t.Live_table_lib_id
                                where p.Result_tb_lib_id == null || p.Result_fit == "no forecast"
                                select new
                                {
                                    p.Analysis_result_id,
                                    t.Match_time,
                                    t.Match_type,
                                    t.Home_team,
                                    t.Away_team,
                                    t.Status,

                                    p.Home_w,
                                    p.Home_d,
                                    p.Home_l,
                                    p.Home_goals,
                                    p.Away_goals,

                                    p.Cross_goals,
                                    p.Fit_win_loss,

                                    p.Recent_scores,
                                    p.Recent_2scores,
                                    p.Recent_3scores,
                                    p.Recent_4scores,
                                    p.Recent_5scores,
                                    p.Recent_6scores,

                                    Dodds = ConvertOdd(t.Home_team, t.Status)
                                };
                MatlabMatch.matchnow = matchnowf.ToDataTable();
            }
        }

        private DataTable _matchOverf;
        public DataTable matchOverf
        {
            get
            {
                if (_matchOverf == null && matchtype != null)
                    _matchOverf = FilterDataTable(MatlabMatch.matchover, matchtype);
                return _matchOverf;
            }
            set
            {
                _matchOverf = value;
            }
        }

        private DataTable _matchNowf;
        public DataTable matchNowf
        {
            get
            {
                if (_matchNowf == null && matchtype != null)
                    _matchNowf = FilterDataTable(MatlabMatch.matchnow, matchtype);
                return _matchNowf;
            }
            set { _matchNowf = value; }
        }

        //??????????? 2011.9.20

        #region 计算过滤

        private DataTable FilterDataTable(DataTable dataSource, string matchtypes)
        {
            DataView dv = dataSource.DefaultView;
            dv.RowFilter = "Match_type = '" + matchtypes + "'";
            //dv.Sort = "Match_time desc";
            //DataView dv1 = SelectView(dv, 18 * 6);
            DataTable newTable1 = dv.ToTable();
            return newTable1;
        }
        public DataView SelectView(DataView dv, int TopValue)
        {
            DataTable Dtable = dv.Table.Clone(); //克隆DataTable 的结构，包括所有DataTable 架构和约束。
            for (int i = 0; i < dv.Count; i++)
            {
                if (i >= TopValue)
                {
                    break;
                }
                Dtable.ImportRow(dv[i].Row); //取前TopValue行，其他的不添加至DataTable
            }
            return new DataView(Dtable);
        }

        #endregion


        private double ConvertOdd(string hometeam, string odds)
        {
            double Dodds = 0;
            string todds = odds.Trim();
            todds = todds.Replace("-", "");
            switch (todds)
            {
                case "0": Dodds = 0; break;
                case "0.5": Dodds = 0.5; break;
                case "0.5/1": Dodds = 0.75; break;
                case "0/0.5": Dodds = 0.25; break;
                case "1": Dodds = 1; break;
                case "1.5": Dodds = 1.5; break;
                case "1.5/2": Dodds = 1.75; break;
                case "1/1.5": Dodds = 1.25; break;
                case "12.5": Dodds = 12.5; break;
                case "13": Dodds = 13; break;
                case "14.5": Dodds = 14.5; break;
                case "2": Dodds = 2; break;
                case "2.5": Dodds = 2.5; break;
                case "2.5/3": Dodds = 2.75; break;
                case "2/2.5": Dodds = 2.25; break;
                case "3": Dodds = 3; break;
                case "3.5": Dodds = 3.5; break;
                case "3.5/4": Dodds = 3.75; break;
                case "3/3.5": Dodds = 3.25; break;
                case "4": Dodds = 4; break;
                case "4.5": Dodds = 4.5; break;
                case "4.5/5": Dodds = 4.75; break;
                case "4/4.5": Dodds = 4.25; break;
                case "5": Dodds = 5; break;
                case "5.5": Dodds = 5.5; break;
                case "5.5/6": Dodds = 5.75; break;
                case "5/5.5": Dodds = 5.25; break;
                case "6.5": Dodds = 6.5; break;
                case "6.5/7": Dodds = 6.75; break;
                case "6/6.5": Dodds = 6.25; break;
                case "7": Dodds = 7; break;
                case "7.5": Dodds = 7.5; break;
                case "7.5/8": Dodds = 7.75; break;
                case "7/7.5": Dodds = 7.25; break;
                case "8.5": Dodds = 8.5; break;
                case "8.5/9": Dodds = 8.75; break;
                case "9.5": Dodds = 9.5; break;
                case "一/半": Dodds = 1.25; break;
                case "一球": Dodds = 1; break;
                case "七/七半": Dodds = 7.25; break;
                case "七半": Dodds = 7.5; break;
                case "七半/八": Dodds = 7.75; break;
                case "七球": Dodds = 7; break;
                case "三/三半": Dodds = 3.25; break;
                case "三/半": Dodds = 3.5; break;
                case "三半": Dodds = 3.5; break;
                case "三半/四": Dodds = 3.75; break;
                case "三球": Dodds = 3; break;
                case "二/半": Dodds = 2.25; break;
                case "二半": Dodds = 2.5; break;
                case "二半/三": Dodds = 2.75; break;
                case "二球": Dodds = 2; break;
                case "五": Dodds = 5; break;
                case "五/五半": Dodds = 5.25; break;
                case "五半": Dodds = 5.5; break;
                case "五半/六": Dodds = 5.75; break;
                case "兩/兩半": Dodds = 2.25; break;
                case "兩半": Dodds = 2.5; break;
                case "兩半/三": Dodds = 2.75; break;
                case "兩球": Dodds = 2; break;
                case "八半/九": Dodds = 8.75; break;
                case "六/六半": Dodds = 6.25; break;
                case "六半": Dodds = 6.5; break;
                case "六半/七": Dodds = 6.75; break;
                case "六球": Dodds = 6; break;
                case "六球半": Dodds = 6.5; break;
                case "十二球半": Dodds = 12.5; break;
                case "半/一": Dodds = 0.75; break;
                case "半球": Dodds = 0.5; break;
                case "四/半": Dodds = 4.25; break;
                case "四/四半": Dodds = 4.25; break;
                case "四半": Dodds = 4.5; break;
                case "四半/五": Dodds = 4.75; break;
                case "四球": Dodds = 4; break;
                case "平/半": Dodds = 0.25; break;
                case "平手": Dodds = 0; break;
                case "球半": Dodds = 1.5; break;
                case "球半/二": Dodds = 1.75; break;
                case "球半/兩": Dodds = 1.75; break;
                default: Dodds = 0; break;
            }
            if (hometeam.IndexOf("*") == -1) Dodds = -1 * Dodds;
            return Dodds;
        }
    }
}


