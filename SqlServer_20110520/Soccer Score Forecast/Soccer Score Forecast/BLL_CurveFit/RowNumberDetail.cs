using System;
using System.Collections.Generic;
using SoccerScore.Compact.Linq;
using System.Linq;
using System.Reflection;

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
        public List<Result_tb_lib> crossOver;
        public List<Result_tb_lib> homeTop20;
        public List<Result_tb_lib> awayTop20;
        public RowNumberDetail(int liveid)
        {
            FindMatchDetail(liveid);
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

                var crossOvert = dMatch.dHome[home_team_big].Where(e => e.Away_team_big == away_team_big).
                    Union(dMatch.dHome[away_team_big].Where(e => e.Away_team_big == home_team_big));
                crossOver = crossOvert.Where(e => e.Match_time.Value.Date < matchtime.Value.Date).
                    OrderByDescending(e => e.Match_time).ToList();

                var homeTop20t = dMatch.dHome[home_team_big].Union(dMatch.dAway[home_team_big]);
                homeTop20 = homeTop20t.
                    Where(e => e.Match_time.Value.Date < matchtime.Value.Date).
                    Where(e => e.Match_type == matchtype).
                    OrderByDescending(e => e.Match_time).Take(20).ToList();

                var awayTop20t = dMatch.dHome[away_team_big].Union(dMatch.dAway[away_team_big]);
                awayTop20 = awayTop20t.
                    Where(e => e.Match_time.Value.Date < matchtime.Value.Date).
                      Where(e => e.Match_type == matchtype).
                    OrderByDescending(e => e.Match_time).Take(20).ToList();
            }
        }
    }
}
