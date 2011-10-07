using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soccer_Score_Forecast
{
    public partial class RowNumberLimit : IDisposable
    {


        #region 利用积分进行运算

        private int _RecentScores;
        public int RecentScores
        {
            get
            {
                if (_RecentScores == 0)
                    _RecentScores = ComputeRecentScores(0);
                return _RecentScores;
            }
            set { _RecentScores = value; }
        }

        private int _Recent2Scores;
        public int Recent2Scores
        {
            get
            {
                if (_Recent2Scores == 0)
                    _Recent2Scores = ComputeRecentScores(1) + RecentScores;
                return _Recent2Scores;
            }
            set { _Recent2Scores = value; }
        }

        private int _Recent3Scores;
        public int Recent3Scores
        {
            get
            {
                if (_Recent3Scores == 0)
                    _Recent3Scores = ComputeRecentScores(2) + Recent2Scores;
                return _Recent3Scores;
            }
            set { _Recent3Scores = value; }
        }

        private int _Recent4Scores;
        public int Recent4Scores
        {
            get
            {
                if (_Recent4Scores == 0)
                    _Recent4Scores = ComputeRecentScores(3) + Recent3Scores;
                return _Recent4Scores;
            }
            set { _Recent4Scores = value; }
        }

        private int _Recent5Scores;
        public int Recent5Scores
        {
            get
            {
                if (_Recent5Scores == 0)
                    _Recent5Scores = ComputeRecentScores(4) + Recent4Scores;
                return _Recent5Scores;
            }
            set { _Recent5Scores = value; }
        }

        private int _Recent6Scores;
        public int Recent6Scores
        {
            get
            {
                if (_Recent6Scores == 0)
                    _Recent6Scores = ComputeRecentScores(5) + Recent5Scores;
                return _Recent6Scores;
            }
            set { _Recent6Scores = value; }
        }

        private int ComputeRecentScores(int skip)
        {
            int rs = 0;

            var homeTop20t = Top20.Where(e => e.Home_team_big == home_team_big);
            var homeTop20tt = Top20.Where(e => e.Away_team_big == home_team_big);
            var homeTop20ttt = homeTop20tt.Union(homeTop20t);
            var hCross = homeTop20ttt.OrderByDescending(e => e.Match_time).Skip(skip).FirstOrDefault();

            var awayTop20t = Top20.Where(e => e.Home_team_big == away_team_big);
            var awayTop20tt = Top20.Where(e => e.Away_team_big == away_team_big);
            var awayTop20ttt = awayTop20tt.Union(awayTop20t);
            var aCross = awayTop20ttt.OrderByDescending(e => e.Match_time).Skip(skip).FirstOrDefault();

            if (hCross != null && aCross != null)
            {
                int h = hCross.Home_team_big == home_team_big ? (hCross.Full_home_goals - hCross.Full_away_goals > 0 ? 3
                    : (hCross.Full_home_goals - hCross.Full_away_goals == 0 ? 1 : 0))
                    : (hCross.Full_home_goals - hCross.Full_away_goals < 0 ? 3
                    : (hCross.Full_home_goals - hCross.Full_away_goals == 0 ? 1 : 0));
                int a = aCross.Away_team_big == away_team_big ? (aCross.Full_home_goals - aCross.Full_away_goals > 0 ? 3
                    : (aCross.Full_home_goals - aCross.Full_away_goals == 0 ? 1 : 0))
                    : (aCross.Full_home_goals - aCross.Full_away_goals < 0 ? 3
                    : (aCross.Full_home_goals - aCross.Full_away_goals == 0 ? 1 : 0));

                rs = h + a;
            }
            return rs;
        }

        #endregion

 

    }
}
