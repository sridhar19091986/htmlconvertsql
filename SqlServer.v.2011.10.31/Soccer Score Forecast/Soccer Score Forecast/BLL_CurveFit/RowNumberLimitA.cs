using System;
using System.Collections.Generic;
using SoccerScore.Compact.Linq;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Soccer_Score_Forecast
{
    public partial class RowNumberLimit : IDisposable
    {
        public int live_id;
        public int? home_team_big;
        public int? away_team_big;
        public string home_team;
        public string away_team;
        public DateTime? matchtime;
        public string matchtype;
        public int Top20Count;

        //public IEnumerable<Result_tb_lib> Top20;

        public List<Result_tb_lib> Top20;

        public RowNumberLimit(int liveid)
        {
            ForeCastInit(liveid);
        }
        private void ForeCastInit(int liveid)
        {
                this.live_id = liveid;
                var l = dMatch.liveTables[live_id].First();
                home_team_big = l.Home_team_big;
                away_team_big = l.Away_team_big;
                home_team = l.Home_team;
                away_team = l.Away_team;
                matchtime = l.Match_time;

                //修正把比赛类型搞进去  2011.6.17
                matchtype = l.Match_type;

                var top20h = dMatch.dHome[home_team_big].Union(dMatch.dHome[away_team_big]).
                    Union(dMatch.dAway[home_team_big]).Union(dMatch.dAway[away_team_big]);

                //修正把比赛日期搞进去了 2011.6.14
                var top20hh = top20h.Where(e => e.Match_time.Value.Date < matchtime.Value.Date);

                //修正把比赛类型搞进去  2011.6.17
                Top20 = top20hh.Where(e => e.Match_type == matchtype).OrderByDescending(e => e.Match_time).Take(40).ToList();

                // .ToList();

                //var top20h = matches.result_tb_lib.Where(e => e.home_team_big == l.home_team_big || e.away_team_big == l.away_team_big);
                //var top20a = matches.result_tb_lib.Where(e => e.home_team_big == l.away_team_big || e.away_team_big == l.home_team_big);
                //Top20 = top20h.Union(top20a).Where(e => e.match_time < matchtime).OrderByDescending(e => e.match_time).Take(40).ToList();
                Top20Count = Top20.Count();
            }    
    }
}
