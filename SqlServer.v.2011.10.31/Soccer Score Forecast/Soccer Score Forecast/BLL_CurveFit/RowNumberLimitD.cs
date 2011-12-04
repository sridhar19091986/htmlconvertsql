using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soccer_Score_Forecast
{
    public partial class RowNumberLimit : IDisposable
    {
        //修改于 2011.6.16   选比赛策略

        private string _listLastJZ;
        public string ListLastJZ
        {
            get
            {
                if (_listLastJZ == null)
                {
                    string jzText = null;


                    //提升速率不少   2011.6.16


                    //using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
                    //{
                    var jz = dMatch.dHome[home_team_big]
                        .Where(e => e.Away_team_big == away_team_big)
                        .OrderByDescending(e => e.Match_time);
                    //var jz = matches.Result_tb_lib.Where(e => e.Home_team_big == home_team_big && e.Away_team_big == away_team_big).OrderByDescending(e => e.Match_time);
                    foreach (var m in jz)
                    {
                        jzText += m.Match_time.Value.ToShortDateString() + "::" +
                            m.Full_home_goals.ToString() + "-" + m.Full_away_goals.ToString() + "::" +
                            m.Odds + "::" + m.Win_loss_big + "::" + m.Home_team + "::" + m.Away_team + "\r\n";
                    }
                    //}
                    _listLastJZ = jzText;
                }
                return _listLastJZ;

            }
            set { _listLastJZ = value; }
        }
        //private string _LastJZ;
        //public string LastJZ
        //{
        //    get
        //    {
        //        if (_LastJZ == null)
        //        {
        //            string jzText = null;

        //            var jz = dMatch.dHome[home_team_big]
        //                           .Where(e => e.Away_team_big == away_team_big)
        //                           .Where(e => e.Match_time.Value.Date < matchtime.Value.Date)   //这里很关键  2011.6.16
        //                           .OrderByDescending(e => e.Match_time).FirstOrDefault();

        //            if (jz == null) jzText = "1";
        //            else
        //            {
        //                if (jz.Full_home_goals > jz.Full_away_goals) jzText = "3";
        //                if (jz.Full_home_goals == jz.Full_away_goals) jzText = "1";
        //                if (jz.Full_home_goals < jz.Full_away_goals) jzText = "0";
        //            }

        //            _LastJZ = jzText;
        //        }
        //        return _LastJZ;
        //    }
        //    set { _LastJZ = value; }
        //}



        #region  matlab仿真用数据

        //把交战记录的平均净胜球计算到matlab
        //当前参与计算的成员：Home_w	Home_d	Home_l	Home_goals	Away_goals

        private double _CrossGoals;
        public double CrossGoals
        {
            get
            {
                if (_CrossGoals == 0)
                {
                    var hCross = dMatch.dHome[home_team_big]
                                   .Where(e => e.Away_team_big == away_team_big)
                                   .Where(e => e.Match_time.Value.Date < matchtime.Value.Date)
                                   .Sum(e => e.Full_home_goals - e.Full_away_goals);

                    _CrossGoals = ConvertDoubleP(hCross);

                    /*
                    var aCross = dMatch.dHome[away_team_big]
                                   .Where(e => e.Away_team_big == home_team_big)
                                   .Where(e => e.Match_time.Value.Date < matchtime.Value.Date)
                                    .Average(e => e.Full_home_goals - e.Full_away_goals);

                    _CrossGoals = (ConvertDoubleP(hCross) - ConvertDoubleP(aCross)) / 2;
                     * */
                }
                return _CrossGoals;
            }
            set { _CrossGoals = value; }
        }
        private double ConvertDoubleP(double? ddd)
        {
            return ddd ?? 0;

            //if (ddd == null) return 0;
            //else return (double)ddd;
        }
        //private string hostX;
        //private string awayX;
        //private string hostawayX;
        //private List<Result_tb_lib> hostSeriesX;
        //private List<Result_tb_lib> awaySeriesX;
        //private List<Result_tb_lib> hostawaySeriesX;
        #endregion

    }


}
