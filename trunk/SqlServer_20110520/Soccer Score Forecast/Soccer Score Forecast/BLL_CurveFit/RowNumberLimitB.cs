using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soccer_Score_Forecast
{
    public partial class RowNumberLimit : IDisposable
    {
        //主进
        private double _homeGoals;

        public double HomeGoals
        {
            get
            {
                if (_homeGoals == 0.0)
                {
                    //剔除没有记录的
                    if (Top20Count < 10) return 0;

                    var hg1 = Top20.Where(e => e.Home_team_big == home_team_big).Sum(e => e.Full_home_goals);
                    var hg2 = Top20.Where(e => e.Away_team_big == home_team_big).Sum(e => e.Full_away_goals);
                    var hg3 = Top20.Where(e => e.Home_team_big == away_team_big).Sum(e => e.Full_away_goals);
                    var hg4 = Top20.Where(e => e.Away_team_big == away_team_big).Sum(e => e.Full_home_goals);
                    var hg5 = Top20.Where(e => e.Away_team_big == away_team_big && e.Home_team_big == home_team_big).Sum(e => e.Full_home_goals);
                    var hg6 = Top20.Where(e => e.Away_team_big == home_team_big && e.Home_team_big == away_team_big).Sum(e => e.Full_away_goals);

                    /*
                     * 
                    _homeGoals = Convert.ToDouble((hg1 == null ? 0 : hg1) +
                                        (hg2 == null ? 0 : hg2) +
                                        (hg3 == null ? 0 : hg3) +
                                        (hg4 == null ? 0 : hg4) -
                                        (hg5 == null ? 0 : hg5) -
                                        (hg6 == null ? 0 : hg6)) / Top20Count;
                     * 
                     * */

                    _homeGoals = Convert.ToDouble((hg1 ?? 0) +
                    (hg2 ?? 0) +
                    (hg3 ?? 0) +
                    (hg4 ?? 0) -
                    (hg5 ?? 0) -
                    (hg6 ?? 0)) / Top20Count;
                }
                return _homeGoals;
            }
            set { _homeGoals = value; }
        }
        //客进
        private double _awayGoals;
        public double AwayGoals
        {
            get
            {
                if (_awayGoals == 0.0)
                {
                    //剔除没有记录的
                    if (Top20Count < 10) return 0;

                    var ag1 = Top20.Where(e => e.Home_team_big == home_team_big).Sum(e => e.Full_away_goals);
                    var ag2 = Top20.Where(e => e.Away_team_big == home_team_big).Sum(e => e.Full_home_goals);
                    var ag3 = Top20.Where(e => e.Home_team_big == away_team_big).Sum(e => e.Full_home_goals);
                    var ag4 = Top20.Where(e => e.Away_team_big == away_team_big).Sum(e => e.Full_away_goals);
                    var ag5 = Top20.Where(e => e.Away_team_big == away_team_big && e.Home_team_big == home_team_big).Sum(e => e.Full_away_goals);
                    var ag6 = Top20.Where(e => e.Away_team_big == home_team_big && e.Home_team_big == away_team_big).Sum(e => e.Full_home_goals);

                    /*
                     * 
                    _awayGoals = Convert.ToDouble((ag1 == null ? 0 : ag1) +
                                         (ag2 == null ? 0 : ag2) +
                                         (ag3 == null ? 0 : ag3) +
                                         (ag4 == null ? 0 : ag4) -
                                         (ag5 == null ? 0 : ag5) -
                                         (ag6 == null ? 0 : ag6)) / Top20Count;
                     * 
                     * */

                    _awayGoals = Convert.ToDouble((ag1 ?? 0) +
                      (ag2 ?? 0) +
                      (ag3 ?? 0) +
                      (ag4 ?? 0) -
                      (ag5 ?? 0) -
                      (ag6 ?? 0)) / Top20Count;

                }
                return _awayGoals;
            }
            set { _awayGoals = value; }
        }
        //胜
        private int _hWin;
        public int hWin
        {
            get
            {
                if (_hWin == 0)
                {
                    //剔除没有记录的
                    if (Top20Count < 10) return 0;

                    var w1 = Top20.Where(e => e.Home_team_big == home_team_big).Where(e => e.Full_home_goals > e.Full_away_goals);
                    var w2 = Top20.Where(e => e.Away_team_big == home_team_big).Where(e => e.Full_away_goals > e.Full_home_goals);
                    var w3 = Top20.Where(e => e.Home_team_big == away_team_big).Where(e => e.Full_away_goals > e.Full_home_goals);
                    var w4 = Top20.Where(e => e.Away_team_big == away_team_big).Where(e => e.Full_home_goals > e.Full_away_goals);
                    var w5 = Top20.Where(e => e.Away_team_big == away_team_big && e.Home_team_big == home_team_big).Where(e => e.Full_home_goals > e.Full_away_goals);
                    var w6 = Top20.Where(e => e.Away_team_big == home_team_big && e.Home_team_big == away_team_big).Where(e => e.Full_away_goals > e.Full_home_goals);
                    _hWin = w1.Count() + w2.Count() + w3.Count() + w4.Count() - w5.Count() - w6.Count();
                }
                return _hWin;
            }
            set { _hWin = value; }
        }
        //平
        private int _hDraw;
        public int hDraw
        {
            get
            {
                if (_hDraw == 0)
                {
                    //剔除没有记录的
                    if (Top20Count < 10) return 0;

                    _hDraw = Top20.Where(e => e.Full_home_goals == e.Full_away_goals).Count();
                }
                return _hDraw;
            }
            set { _hDraw = value; }
        }
        //负
        private int _hLose;
        public int hLose
        {
            get
            {
                if (_hLose == 0)
                {
                    //剔除没有记录的
                    if (Top20Count < 10) return 0;

                    var l1 = Top20.Where(e => e.Home_team_big == home_team_big).Where(e => e.Full_home_goals < e.Full_away_goals);
                    var l2 = Top20.Where(e => e.Away_team_big == home_team_big).Where(e => e.Full_away_goals < e.Full_home_goals);
                    var l3 = Top20.Where(e => e.Home_team_big == away_team_big).Where(e => e.Full_away_goals < e.Full_home_goals);
                    var l4 = Top20.Where(e => e.Away_team_big == away_team_big).Where(e => e.Full_home_goals < e.Full_away_goals);
                    var l5 = Top20.Where(e => e.Away_team_big == away_team_big && e.Home_team_big == home_team_big).Where(e => e.Full_home_goals < e.Full_away_goals);
                    var l6 = Top20.Where(e => e.Away_team_big == home_team_big && e.Home_team_big == away_team_big).Where(e => e.Full_away_goals < e.Full_home_goals);
                    _hLose = l1.Count() + l2.Count() + l3.Count() + l4.Count() - l5.Count() - l6.Count();
                }
                return _hLose;
            }
            set { _hLose = value; }
        }

    }
}
