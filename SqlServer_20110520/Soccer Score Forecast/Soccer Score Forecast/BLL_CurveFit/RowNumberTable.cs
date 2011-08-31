using System.Collections.Generic;
using SoccerScore.Compact.Linq;
using System.Linq;
using System.Reflection;
using System.Data;

namespace Soccer_Score_Forecast
{
    static class MatlabMatch
    {
        public static DataTable matchover = null;
        public static DataTable matchnow = null;
        public static List<StatMatch> statmatch = null;
    }
    class StatMatch
    {
        public string Result_wdl;
        public string Result_fit;
        public string Result_goals;
        public string Match_type;
    }
    class RowNumberTable
    {
        
        private string matchtype;
        private DataTable _winRate;
        public DataTable WinRate
        {
            get
            {
                if (_winRate == null)
                {
                    using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
                    {
                        var mar = from a in matches.Match_analysis_result
                                  join b in matches.Live_Table_lib on a.Live_table_lib_id equals b.Live_table_lib_id
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
                        _winRate = winrate.ToDataTable();
                    }
                }
                return _winRate;
            }
            set
            {
                _winRate = value;
            }

        }
        public RowNumberTable()
        {
        }
        public RowNumberTable(string matchtype)
        {
            this.matchtype = matchtype;
            InitMatchOver();
            InitMatchNow();
        }

        public void InitMatchOver()
        {
            if (MatlabMatch.matchover == null)
            {
                using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
                {
                    var matchover = from p in matches.Match_analysis_result
                                    join q in matches.Result_tb_lib on p.Result_tb_lib_id equals q.Result_tb_lib_id
                                    join t in matches.Live_Table_lib on p.Live_table_lib_id equals t.Live_table_lib_id
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
                                        p.Recent_scores,
                                        p.Cross_goals,
                                        p.Fit_win_loss,
                                        Lottery_Ticket = q.Full_home_goals - q.Full_away_goals,
                                        q.Full_home_goals,
                                        q.Full_away_goals,
                                        Dodds = ConvertOdd(t.Home_team, q.Odds)
                                    };
                    MatlabMatch.matchover = matchover.ToDataTable();
                }

            }
        }

        public void InitMatchNow()
        {
            if (MatlabMatch.matchnow == null)
            {
                using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
                {
                    var matchnowf = from p in matches.Match_analysis_result
                                   join t in matches.Live_Table_lib on p.Live_table_lib_id equals t.Live_table_lib_id
                                   where p.Result_tb_lib_id == null
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
                                       p.Recent_scores,
                                       p.Cross_goals,
                                       p.Fit_win_loss,
                                       Dodds = ConvertOdd(t.Home_team, t.Status)
                                   };
                    MatlabMatch.matchnow = matchnowf.ToDataTable();
                }
            }
        }
        private DataTable _matchOverf;
        public DataTable matchOverf
        {
            get
            {
                if (_matchOverf == null)
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
                if (_matchNowf == null)
                    _matchNowf = FilterDataTable(MatlabMatch.matchnow, matchtype);

                return _matchNowf;
            }
            set { _matchNowf = value; }
        }

        private DataTable FilterDataTable(DataTable dataSource, string matchtypes)
        {
            DataView dv = dataSource.DefaultView;
            dv.RowFilter = "Match_type = '" + matchtypes + "'";
            DataTable newTable1 = dv.ToTable();
            return newTable1;
        }

        public List<StatMatch> statmatch()
        {
            if (MatlabMatch.statmatch == null)
            {
                List<StatMatch> lsm = new List<StatMatch>();
                using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
                {
                    var mar = from a in matches.Match_analysis_result
                              join b in matches.Live_Table_lib on a.Live_table_lib_id equals b.Live_table_lib_id
                              select new
                              {
                                  a.Result_wdl,
                                  a.Result_fit,
                                  a.Result_goals,
                                  b.Match_type
                              };
                    foreach (var m in mar)
                    {
                        StatMatch sm = new StatMatch();
                        sm.Match_type = m.Match_type;
                        sm.Result_fit = m.Result_fit;
                        sm.Result_goals = m.Result_goals;
                        sm.Result_wdl = m.Result_wdl;
                       lsm.Add(sm);
                    }
                }
                MatlabMatch.statmatch = lsm;
            }
            return MatlabMatch.statmatch;
        }

        private DataTable _typeRate;
        public int MaxW;
        public DataTable typeRate
        {
            get
            {
                if (_typeRate == null)
                {

                        var winrate = from p in statmatch()
                                      where p.Match_type == matchtype
                                      group p by p.Match_type into q
                                      select new
                                      {
                                          q.Key,
                                          fitW = q.Where(e => e.Result_fit == "W").Count(),
                                          fitL = q.Where(e => e.Result_fit == "L").Count(),
                                          goalsW = q.Where(e => e.Result_goals == "W").Count(),
                                          goalsL = q.Where(e => e.Result_goals == "L").Count(),
                                          交战_概率1_拟合_进球_概率30W = q.Where(e => e.Result_wdl == "W").Count(),
                                          交战_概率1_拟合_进球_概率30L = q.Where(e => e.Result_wdl == "L").Count(),
                                      };
                        var maxwin = winrate.FirstOrDefault();
                        int[] maxw = { maxwin.fitW, maxwin.fitL, maxwin.goalsW, maxwin.goalsL, 
                                         maxwin.交战_概率1_拟合_进球_概率30W, 
                                         maxwin.交战_概率1_拟合_进球_概率30L };
                        MaxW = maxw.Max();
                        _typeRate = winrate.ToDataTable();
                    
                }
                return _typeRate;
            }
            set { _typeRate = value; }
        }
        private double ConvertOdd(string hometeam, string odds)
        {
            double Dodds = 0;
            string todds = odds.Trim();
            todds = todds.Replace("-","");
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


