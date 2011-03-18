using System;
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
    public class RowNumberLimit
    {
        public decimal id;
        public int? Home_team_big;
        public int? Away_team_big;
        public DateTime? matchtime;
        public int Top20Count;
        public List<Result_tb_lib> Top20;
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
        //private SoccerScoreCompact match = new SoccerScoreCompact(cnn);

        public RowNumberLimit(decimal id)
        {
            //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))
            //{
               if(dmatch.dHome==null||dmatch.dAway==null)
                {
                    dmatch.dHome = Conn.match.Result_tb_lib.ToLookup(e => e.Home_team_big);
                    dmatch.dAway = Conn.match.Result_tb_lib.ToLookup(e => e.Away_team_big);
                }
                this.id = id;
                var l = Conn.match.Live_Table_lib.Where(e => e.Live_table_lib_id == id).First();
                Home_team_big = l.Home_team_big;
                Away_team_big = l.Away_team_big;
                matchtime = l.Match_time;
                var top20h = dmatch.dHome[Home_team_big].Union(dmatch.dHome[Away_team_big]).
                    Union(dmatch.dAway[Home_team_big]).Union(dmatch.dAway[Away_team_big]);
                Top20 = top20h.Where(e => e.Match_time < matchtime).OrderByDescending(e => e.Match_time).Take(40).ToList();

                //var top20h = match.Result_tb_lib.Where(e => e.Home_team_big == l.Home_team_big || e.Away_team_big == l.Away_team_big);
                //var top20a = match.Result_tb_lib.Where(e => e.Home_team_big == l.Away_team_big || e.Away_team_big == l.Home_team_big);
                //Top20 = top20h.Union(top20a).Where(e => e.Match_time < matchtime).OrderByDescending(e => e.Match_time).Take(40).ToList();
                Top20Count = Top20.Count();
            //}
        }
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

                    var hg1 = Top20.Where(e => e.Home_team_big == Home_team_big).Sum(e => e.Full_home_goals);
                    var hg2 = Top20.Where(e => e.Away_team_big == Home_team_big).Sum(e => e.Full_away_goals);
                    var hg3 = Top20.Where(e => e.Home_team_big == Away_team_big).Sum(e => e.Full_away_goals);
                    var hg4 = Top20.Where(e => e.Away_team_big == Away_team_big).Sum(e => e.Full_home_goals);
                    var hg5 = Top20.Where(e => e.Away_team_big == Away_team_big && e.Home_team_big == Home_team_big).Sum(e => e.Full_home_goals);
                    var hg6 = Top20.Where(e => e.Away_team_big == Home_team_big && e.Home_team_big == Away_team_big).Sum(e => e.Full_away_goals);
                    _homeGoals = Convert.ToDouble((hg1 == null ? 0 : hg1) +
                                        (hg2 == null ? 0 : hg2) +
                                        (hg3 == null ? 0 : hg3) +
                                        (hg4 == null ? 0 : hg4) -
                                        (hg5 == null ? 0 : hg5) -
                                        (hg6 == null ? 0 : hg6)) / Top20Count;
                }
                return _homeGoals;
            }
            //set { _homeGoals = Value; }
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

                    var ag1 = Top20.Where(e => e.Home_team_big == Home_team_big).Sum(e => e.Full_away_goals);
                    var ag2 = Top20.Where(e => e.Away_team_big == Home_team_big).Sum(e => e.Full_home_goals);
                    var ag3 = Top20.Where(e => e.Home_team_big == Away_team_big).Sum(e => e.Full_home_goals);
                    var ag4 = Top20.Where(e => e.Away_team_big == Away_team_big).Sum(e => e.Full_away_goals);
                    var ag5 = Top20.Where(e => e.Away_team_big == Away_team_big && e.Home_team_big == Home_team_big).Sum(e => e.Full_away_goals);
                    var ag6 = Top20.Where(e => e.Away_team_big == Home_team_big && e.Home_team_big == Away_team_big).Sum(e => e.Full_home_goals);
                    _awayGoals = Convert.ToDouble((ag1 == null ? 0 : ag1) +
                                         (ag2 == null ? 0 : ag2) +
                                         (ag3 == null ? 0 : ag3) +
                                         (ag4 == null ? 0 : ag4) -
                                         (ag5 == null ? 0 : ag5) -
                                         (ag6 == null ? 0 : ag6)) / Top20Count;
                }
                return _awayGoals;
            }
            //set { _awayGoals = Value; }
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

                    var w1 = Top20.Where(e => e.Home_team_big == Home_team_big).Where(e => e.Full_home_goals > e.Full_away_goals);
                    var w2 = Top20.Where(e => e.Away_team_big == Home_team_big).Where(e => e.Full_away_goals > e.Full_home_goals);
                    var w3 = Top20.Where(e => e.Home_team_big == Away_team_big).Where(e => e.Full_away_goals > e.Full_home_goals);
                    var w4 = Top20.Where(e => e.Away_team_big == Away_team_big).Where(e => e.Full_home_goals > e.Full_away_goals);
                    var w5 = Top20.Where(e => e.Away_team_big == Away_team_big && e.Home_team_big == Home_team_big).Where(e => e.Full_home_goals > e.Full_away_goals);
                    var w6 = Top20.Where(e => e.Away_team_big == Home_team_big && e.Home_team_big == Away_team_big).Where(e => e.Full_away_goals > e.Full_home_goals);
                    _hWin = w1.Count() + w2.Count() + w3.Count() + w4.Count() - w5.Count() - w6.Count();
                }
                return _hWin;
            }
            //set { _hWin = Value; }
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
            //set { _hDraw = Value; }
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

                    var l1 = Top20.Where(e => e.Home_team_big == Home_team_big).Where(e => e.Full_home_goals < e.Full_away_goals);
                    var l2 = Top20.Where(e => e.Away_team_big == Home_team_big).Where(e => e.Full_away_goals < e.Full_home_goals);
                    var l3 = Top20.Where(e => e.Home_team_big == Away_team_big).Where(e => e.Full_away_goals < e.Full_home_goals);
                    var l4 = Top20.Where(e => e.Away_team_big == Away_team_big).Where(e => e.Full_home_goals < e.Full_away_goals);
                    var l5 = Top20.Where(e => e.Away_team_big == Away_team_big && e.Home_team_big == Home_team_big).Where(e => e.Full_home_goals < e.Full_away_goals);
                    var l6 = Top20.Where(e => e.Away_team_big == Home_team_big && e.Home_team_big == Away_team_big).Where(e => e.Full_away_goals < e.Full_home_goals);
                    _hLose = l1.Count() + l2.Count() + l3.Count() + l4.Count() - l5.Count() - l6.Count();
                }
                return _hLose;
            }
            //set { _hLose = Value; }
        }
        private List<matchPoint<float>> _curveFit;
        public List<matchPoint<float>> CurveFit
        {
            get
            {
                if (_curveFit == null)
                {
                    List<matchPoint<float>> fit = new List<matchPoint<float>>();
                    fit = CsharpMatlab.ployfitSeries(ListmatchPointData, NowmatchTimeDiff);
                    _curveFit = fit;
                }
                return _curveFit;
            }
            //set { curveFit = Value; }
        }
        private matchPoint<float> _curveFitValue;
        public matchPoint<float> CurveFitValue
        {
            get
            {
                if (_curveFitValue == null)
                {
                    var curvefit = CurveFit.Last();
                    _curveFitValue = curvefit;
                }
                return _curveFitValue;
            }
            //set { _curveFitValue = Value; }
        }
        //预测值 胜平负
        private double _curveFitWinLoss;
        public double CureFitWinLoss
        {
            get
            {
                if (_curveFitWinLoss == 0.0)
                {
                    //剔除没有记录的
                    if (Top20Count < 10) return 0;
                    //double curvefit = CsharpMatlab.ployfitNowWDL(ListmatchPointData, NowmatchTimeDiff);
                    double curvefit = CurveFitValue.LastmatchWDL;
                    //double.NaN无穷大的问题
                    if (double.IsNaN(curvefit))
                    {
                        _curveFitWinLoss = 0;
                    }
                    else
                    {
                        _curveFitWinLoss = curvefit;
                    }
                }
                return _curveFitWinLoss;
            }
            //set { _curveFitWinLoss = Value; }
        }
        //预测值 进球数
        private double _curveFitGoals;
        public double CureFitGoals
        {
            get
            {
                if (_curveFitGoals == 0.0)
                {
                    //剔除没有记录的
                    if (Top20Count < 10) return 0;
                    double curvefit = CurveFitValue.LastmatchGoals;
                    //double curvefit = CsharpMatlab.ployfitNowGoals (ListmatchPointData, NowmatchTimeDiff);
                    //double.NaN无穷大的问题
                    if (double.IsNaN(curvefit))
                    {
                        _curveFitGoals = 0;
                    }
                    else
                    {
                        _curveFitGoals = curvefit;
                    }
                }
                return _curveFitGoals;
            }
            //set { _curveFitGoals = Value; }
        }
        //预测值 单双
        private double _curveFitOddEven;
        public double CureFitOddEven
        {
            get
            {
                if (_curveFitOddEven == 0.0)
                {
                    //剔除没有记录的
                    if (Top20Count < 10) return 0;
                    double curvefit = CurveFitValue.LastmatchOddEven;
                    //double curvefit = CsharpMatlab.ployfitNowOE (ListmatchPointData, NowmatchTimeDiff);
                    //double.NaN无穷大的问题
                    if (double.IsNaN(curvefit))
                    {
                        _curveFitOddEven = 0;
                    }
                    else
                    {
                        _curveFitOddEven = curvefit;
                    }
                }
                return _curveFitOddEven;
            }
            //set { _curveFitOddEven = Value; }
        }
        //最早的时间值
        private DateTime? _firstmatchTime;
        public DateTime? firstmatchTime
        {
            get
            {
                if (_firstmatchTime == null)
                {
                    DateTime? dt = Top20.Min(o => o.Match_time);
                    if (dt == null)
                        _firstmatchTime = DateTime.Now;
                    else
                        _firstmatchTime = dt;
                }
                return _firstmatchTime;
            }
            //set { _firstmatchTime = Value; }
        }
        //最大日期差
        private int _nowmatchTimeDiff;
        public int NowmatchTimeDiff
        {
            get
            {
                if (_nowmatchTimeDiff == 0)
                {
                    TimeSpan nowDif = matchtime.Value - firstmatchTime.Value;
                    _nowmatchTimeDiff = nowDif.Days;
                }
                return _nowmatchTimeDiff;
            }
            //set { _nowmatchTimeDiff = Value; }
        }
        //预测输入数据源
        private List<matchPoint<int>> _listmatchPointData;
        public List<matchPoint<int>> ListmatchPointData
        {
            get
            {
                if (_listmatchPointData == null)
                {
                    List<matchPoint<int>> matchpoints = new List<matchPoint<int>>();
                    var reTOP20 = Top20.OrderBy(o => o.Match_time);
                    foreach (var m in reTOP20)
                    {
                        matchPoint<int> p = new matchPoint<int>();
                        TimeSpan minDif = m.Match_time.Value - firstmatchTime.Value;
                        p.LastmatchOverTime = minDif.Days;//时间差赋值，横坐标
                        p.matchTime = m.Match_time.Value; //比赛时间
                        p.LastmatchGoals = m.Full_home_goals.Value + m.Full_away_goals.Value; //进球总数
                        p.LastmatchOddEven = ConvertGoalsToOE(p.LastmatchGoals);   //进球单双
                        if (m.Home_team_big == Home_team_big || m.Away_team_big == Away_team_big)
                            p.LastmatchScore = m.Full_home_goals.Value - m.Full_away_goals.Value; //胜负
                        else
                            p.LastmatchScore = m.Full_away_goals.Value - m.Full_home_goals.Value;
                        p.LastmatchWDL = ConvertGoalsToWDL(p.LastmatchScore);  //进球多少
                        //记录提示
                        p.matchDetail = m.Home_team + "<------->" + m.Away_team + "\r\n" +
                            m.Odds + "<------->" + m.Win_loss_big + "<------->" + m.Match_type + "\r\n" +
                            m.Full_home_goals.ToString() + "<------->" + m.Full_away_goals.ToString() + "\r\n";
                        matchpoints.Add(p);
                    }
                    _listmatchPointData = matchpoints;
                }
                return _listmatchPointData;
            }
            //set { _listmatchPointData = Value; }
        }
        private int ConvertGoalsToWDL(int goals)
        {
            int wdl = 0;
            if (goals > 0) wdl = 1;
            if (goals == 0) wdl = 0;
            if (goals < 0) wdl = -1;
            return wdl;
        }
        private int ConvertGoalsToOE(int goals)
        {
            int wdl = goals % 2;
            return wdl;
        }
        private string _listLastJZ;
        public string ListLastJZ
        {
            get
            {
                if (_listLastJZ == null)
                {
                    string jzText = null;
                    //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))
                    //{
                    var jz = Conn.match.Result_tb_lib.Where(e => e.Home_team_big == Home_team_big && e.Away_team_big == Away_team_big).OrderByDescending(e => e.Match_time);
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
            //set { _listLastJZ = Value; }
        }
    }
}
