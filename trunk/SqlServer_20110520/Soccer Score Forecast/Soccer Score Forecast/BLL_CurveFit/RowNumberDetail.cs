using System;
using System.Collections.Generic;
using SoccerScore.Compact.Linq;
using System.Linq;
using System.Reflection;
using System.Data;

namespace Soccer_Score_Forecast
{
    public class RowNumberDetail
    {

        /*
        Func<T, TResult> 委托
        在此似乎没有实用价值
         **/
        private TResult Sum<T, TResult>(IEnumerable<T> sequence, TResult total, Func<T, TResult, TResult> accumulator)
        {
            foreach (T item in sequence)
                total = accumulator(item, total);
            return total;
        }
        private int live_id;
        private int? home_team_big;
        private int? away_team_big;
        private DateTime? matchtime;
        private string matchtype;
        private List<Result_tb_lib> Top20;
        public RowNumberDetail(int liveid)
        {
            FindMatchDetail(liveid);
            var top20h = dMatch.dHome[home_team_big].Union(dMatch.dHome[away_team_big]).
                   Union(dMatch.dAway[home_team_big]).Union(dMatch.dAway[away_team_big]);
            var top20hh = top20h.Where(e => e.Match_time.Value.Date < matchtime.Value.Date);
            Top20 = top20hh.Where(e => e.Match_type == matchtype).
                OrderByDescending(e => e.Match_time).Take(40).ToList();
        }
        private void FindMatchDetail(int liveid)
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                if (dMatch.dHome == null || dMatch.dAway == null)
                {
                    dMatch.dHome = matches.Result_tb_lib.ToLookup(e => e.Home_team_big);
                    dMatch.dAway = matches.Result_tb_lib.ToLookup(e => e.Away_team_big);
                }
                this.live_id = liveid;
                var l = matches.Live_Table_lib.Where(e => e.Live_table_lib_id == liveid).First();
                home_team_big = l.Home_team_big;
                away_team_big = l.Away_team_big;
                matchtime = l.Match_time;
                matchtype = l.Match_type;
            }
        }
        private DataTable _crossOver;
        public DataTable crossOver
        {
            get
            {
                if (_crossOver == null)
                {

                    var crossOvert = dMatch.dHome[home_team_big].Where(e => e.Away_team_big == away_team_big).
                               Union(dMatch.dHome[away_team_big].Where(e => e.Away_team_big == home_team_big));
                    var crossOvertt = crossOvert.Where(e => e.Match_time.Value.Date < matchtime.Value.Date).
                        OrderByDescending(e => e.Match_time);
                    var crossOverttt = from p in crossOvertt
                                       select new
                                       {
                                           p.Match_time,
                                           p.Match_type,
                                           p.Home_team,
                                           p.Away_team,
                                           FullScore = p.Full_home_goals + " - " + p.Full_away_goals,
                                           HalfScroe = p.Half_home_goals + " - " + p.Half_away_goals,
                                           homeCross = p.Home_team_big == home_team_big ?
                                           (p.Full_home_goals - p.Full_away_goals) :
                                           (p.Full_away_goals - p.Full_home_goals)
                                       };
                    _crossOver = crossOverttt.ToDataTable();
                }
                return _crossOver;
            }
            set
            {
                _crossOver = value;
            }
        }
        private DataTable _homeTop20;
        public DataTable homeTop20
        {
            get
            {
                if (_homeTop20 == null)
                {
                    var homeTop20t = Top20.Where(e => e.Home_team_big == home_team_big);
                    var homeTop20tt = Top20.Where(e => e.Away_team_big == home_team_big);
                    var homeTop20ttt = from p in homeTop20tt.Union(homeTop20t)
                                       select new
                                       {
                                           p.Match_time,
                                           p.Match_type,
                                           p.Home_team,
                                           p.Away_team,
                                           FullScore = p.Full_home_goals + " - " + p.Full_away_goals,
                                           HalfScroe = p.Half_home_goals + " - " + p.Half_away_goals,
                                           homeCross = p.Home_team_big == home_team_big ?
                                           (p.Full_home_goals > p.Full_away_goals ? "3"
                                           : (p.Full_home_goals == p.Full_away_goals ? "1" : "0")) :
                                           (p.Full_home_goals < p.Full_away_goals ? "3"
                                           : (p.Full_home_goals == p.Full_away_goals ? "1" : "0"))
                                       };
                    _homeTop20 = homeTop20ttt.ToDataTable();
                }
                return _homeTop20;
            }
            set
            {
                _homeTop20 = value;
            }
        }
        private DataTable _awayTop20;
        public DataTable awayTop20
        {
            get
            {
                if (_awayTop20 == null)
                {
                    var awayTop20t = Top20.Where(e => e.Home_team_big == away_team_big);
                    var awayTop20tt = Top20.Where(e => e.Away_team_big == away_team_big);
                    var awayTop20ttt = from p in awayTop20tt.Union(awayTop20t)
                                       select new
                                       {
                                           p.Match_time,
                                           p.Match_type,
                                           p.Home_team,
                                           p.Away_team,
                                           FullScore = p.Full_home_goals + " - " + p.Full_away_goals,
                                           HalfScroe = p.Half_home_goals + " - " + p.Half_away_goals,
                                           homeCross = p.Away_team_big == away_team_big ?
                                           (p.Full_home_goals > p.Full_away_goals ? "3"
                                           : (p.Full_home_goals == p.Full_away_goals ? "1" : "0")) :
                                           (p.Full_home_goals < p.Full_away_goals ? "3"
                                           : (p.Full_home_goals == p.Full_away_goals ? "1" : "0"))
                                       };
                    _awayTop20 = awayTop20ttt.ToDataTable();
                }
                return _awayTop20;
            }
            set { _awayTop20 = value; }
        }
    }
}
