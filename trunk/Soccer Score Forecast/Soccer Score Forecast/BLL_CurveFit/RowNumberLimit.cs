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
using Soccer_Score_Forecast.LinqSql;
using System.Linq;
using System.Collections;

namespace Soccer_Score_Forecast
{
    public class RowNumberLimit
    {
        public decimal id;
        public int? HomeTeamBig;
        public int? AwayTeamBig;
        public DateTime? matchtime;
        public int Top20Count;
        //public List<result_tb_lib> Top20;
        public List<ResultTBLib> Top20;
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
        //private DataClassesMatchDataContext matches = new DataClassesMatchDataContext();

        public RowNumberLimit(decimal id)
        {
            using ( SoccerScoreSqlite matches = new SoccerScoreSqlite(Conn.cnn))
            //using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext())
            {
               if(dMatch.dHome==null||dMatch.dAway==null)
                {
                //    dMatch.dHome = matches.result_tb_lib.ToLookup(e => e.HomeTeamBig);
                //    dMatch.dAway = matches.result_tb_lib.ToLookup(e => e.AwayTeamBig);
                    dMatch.dHome = matches.ResultTBLib.ToLookup(e => e.HomeTeamBig);
                    dMatch.dAway = matches.ResultTBLib.ToLookup(e => e.AwayTeamBig);
                }
                this.id = id;
                //var l = matches.live_Table_lib.Where(e => e.live_table_lib_id == id).First();
                var l = matches.LiveTableLib.Where(e => e.LiveTableLibID == id).First();
                //HomeTeamBig = l.HomeTeamBig;
                //AwayTeamBig = l.AwayTeamBig;
                //matchtime = l.MatchTime;
                //var top20h = dMatch.dHome[HomeTeamBig].Union(dMatch.dHome[AwayTeamBig]).
                //    Union(dMatch.dAway[HomeTeamBig]).Union(dMatch.dAway[AwayTeamBig]);
                //Top20 = top20h.Where(e => e.MatchTime < matchtime).OrderByDescending(e => e.MatchTime).Take(40).ToList();

                HomeTeamBig = l.HomeTeamBig;
                AwayTeamBig = l.AwayTeamBig;
                matchtime = l.MatchTime;
                IEnumerable<ResultTBLib>  t1=dMatch.dHome[HomeTeamBig];
                IEnumerable<ResultTBLib>  t2=dMatch.dAway[AwayTeamBig];
                IEnumerable<ResultTBLib>  t3=dMatch.dHome[AwayTeamBig];
                IEnumerable<ResultTBLib>  t4=dMatch.dAway[HomeTeamBig];
                IEnumerable<ResultTBLib> top20h=t1.Union(t2).Union(t3).Union(t4);
                //var top20h = (dMatch.dHome[HomeTeamBig]).Union(dMatch.dHome[AwayTeamBig]). Union(dMatch.dAway[HomeTeamBig]).Union(dMatch.dAway[AwayTeamBig]);
                Top20 = top20h.Where(e => e.MatchTime< matchtime).OrderByDescending(e => e.MatchTime).Take(40).ToList();

                //var top20h = matches.result_tb_lib.Where(e => e.HomeTeamBig == l.HomeTeamBig || e.AwayTeamBig == l.AwayTeamBig);
                //var top20a = matches.result_tb_lib.Where(e => e.HomeTeamBig == l.AwayTeamBig || e.AwayTeamBig == l.HomeTeamBig);
                //Top20 = top20h.Union(top20a).Where(e => e.MatchTime < matchtime).OrderByDescending(e => e.MatchTime).Take(40).ToList();
                Top20Count = Top20.Count();
            }
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

                    var hg1 = Top20.Where(e => e.HomeTeamBig == HomeTeamBig).Sum(e => e.FullHomeGoals);
                    var hg2 = Top20.Where(e => e.AwayTeamBig == HomeTeamBig).Sum(e => e.FullAwayGoals);
                    var hg3 = Top20.Where(e => e.HomeTeamBig == AwayTeamBig).Sum(e => e.FullAwayGoals);
                    var hg4 = Top20.Where(e => e.AwayTeamBig == AwayTeamBig).Sum(e => e.FullHomeGoals);
                    var hg5 = Top20.Where(e => e.AwayTeamBig == AwayTeamBig && e.HomeTeamBig == HomeTeamBig).Sum(e => e.FullHomeGoals);
                    var hg6 = Top20.Where(e => e.AwayTeamBig == HomeTeamBig && e.HomeTeamBig == AwayTeamBig).Sum(e => e.FullAwayGoals);
                    _homeGoals = Convert.ToDouble((hg1 == null ? 0 : hg1) +
                                        (hg2 == null ? 0 : hg2) +
                                        (hg3 == null ? 0 : hg3) +
                                        (hg4 == null ? 0 : hg4) -
                                        (hg5 == null ? 0 : hg5) -
                                        (hg6 == null ? 0 : hg6)) / Top20Count;
                }
                return _homeGoals;
            }
            //set { _homeGoals = value; }
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

                    var ag1 = Top20.Where(e => e.HomeTeamBig == HomeTeamBig).Sum(e => e.FullAwayGoals);
                    var ag2 = Top20.Where(e => e.AwayTeamBig == HomeTeamBig).Sum(e => e.FullHomeGoals);
                    var ag3 = Top20.Where(e => e.HomeTeamBig == AwayTeamBig).Sum(e => e.FullHomeGoals);
                    var ag4 = Top20.Where(e => e.AwayTeamBig == AwayTeamBig).Sum(e => e.FullAwayGoals);
                    var ag5 = Top20.Where(e => e.AwayTeamBig == AwayTeamBig && e.HomeTeamBig == HomeTeamBig).Sum(e => e.FullAwayGoals);
                    var ag6 = Top20.Where(e => e.AwayTeamBig == HomeTeamBig && e.HomeTeamBig == AwayTeamBig).Sum(e => e.FullHomeGoals);
                    _awayGoals = Convert.ToDouble((ag1 == null ? 0 : ag1) +
                                         (ag2 == null ? 0 : ag2) +
                                         (ag3 == null ? 0 : ag3) +
                                         (ag4 == null ? 0 : ag4) -
                                         (ag5 == null ? 0 : ag5) -
                                         (ag6 == null ? 0 : ag6)) / Top20Count;
                }
                return _awayGoals;
            }
            //set { _awayGoals = value; }
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

                    var w1 = Top20.Where(e => e.HomeTeamBig == HomeTeamBig).Where(e => e.FullHomeGoals > e.FullAwayGoals);
                    var w2 = Top20.Where(e => e.AwayTeamBig == HomeTeamBig).Where(e => e.FullAwayGoals > e.FullHomeGoals);
                    var w3 = Top20.Where(e => e.HomeTeamBig == AwayTeamBig).Where(e => e.FullAwayGoals > e.FullHomeGoals);
                    var w4 = Top20.Where(e => e.AwayTeamBig == AwayTeamBig).Where(e => e.FullHomeGoals > e.FullAwayGoals);
                    var w5 = Top20.Where(e => e.AwayTeamBig == AwayTeamBig && e.HomeTeamBig == HomeTeamBig).Where(e => e.FullHomeGoals > e.FullAwayGoals);
                    var w6 = Top20.Where(e => e.AwayTeamBig == HomeTeamBig && e.HomeTeamBig == AwayTeamBig).Where(e => e.FullAwayGoals > e.FullHomeGoals);
                    _hWin = w1.Count() + w2.Count() + w3.Count() + w4.Count() - w5.Count() - w6.Count();
                }
                return _hWin;
            }
            //set { _hWin = value; }
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

                    _hDraw = Top20.Where(e => e.FullHomeGoals == e.FullAwayGoals).Count();
                }
                return _hDraw;
            }
            //set { _hDraw = value; }
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

                    var l1 = Top20.Where(e => e.HomeTeamBig == HomeTeamBig).Where(e => e.FullHomeGoals < e.FullAwayGoals);
                    var l2 = Top20.Where(e => e.AwayTeamBig == HomeTeamBig).Where(e => e.FullAwayGoals < e.FullHomeGoals);
                    var l3 = Top20.Where(e => e.HomeTeamBig == AwayTeamBig).Where(e => e.FullAwayGoals < e.FullHomeGoals);
                    var l4 = Top20.Where(e => e.AwayTeamBig == AwayTeamBig).Where(e => e.FullHomeGoals < e.FullAwayGoals);
                    var l5 = Top20.Where(e => e.AwayTeamBig == AwayTeamBig && e.HomeTeamBig == HomeTeamBig).Where(e => e.FullHomeGoals < e.FullAwayGoals);
                    var l6 = Top20.Where(e => e.AwayTeamBig == HomeTeamBig && e.HomeTeamBig == AwayTeamBig).Where(e => e.FullAwayGoals < e.FullHomeGoals);
                    _hLose = l1.Count() + l2.Count() + l3.Count() + l4.Count() - l5.Count() - l6.Count();
                }
                return _hLose;
            }
            //set { _hLose = value; }
        }
        private List<MatchPoint<float>> _curveFit;
        public List<MatchPoint<float>> CurveFit
        {
            get
            {
                if (_curveFit == null)
                {
                    List<MatchPoint<float>> fit = new List<MatchPoint<float>>();
                    fit = CsharpMatlab.ployfitSeries(ListMatchPointData, NowMatchTimeDiff);
                    _curveFit = fit;
                }
                return _curveFit;
            }
            //set { curveFit = value; }
        }
        private MatchPoint<float> _curveFitValue;
        public MatchPoint<float> CurveFitValue
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
            //set { _curveFitValue = value; }
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
                    //double curvefit = CsharpMatlab.ployfitNowWDL(ListMatchPointData, NowMatchTimeDiff);
                    double curvefit = CurveFitValue.LastMatchWDL;
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
            //set { _curveFitWinLoss = value; }
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
                    double curvefit = CurveFitValue.LastMatchGoals;
                    //double curvefit = CsharpMatlab.ployfitNowGoals (ListMatchPointData, NowMatchTimeDiff);
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
            //set { _curveFitGoals = value; }
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
                    double curvefit = CurveFitValue.LastMatchOddEven;
                    //double curvefit = CsharpMatlab.ployfitNowOE (ListMatchPointData, NowMatchTimeDiff);
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
            //set { _curveFitOddEven = value; }
        }
        //最早的时间值
        private DateTime? _firstMatchTime;
        public DateTime? firstMatchTime
        {
            get
            {
                if (_firstMatchTime == null)
                {
                    DateTime? dt = Top20.Min(o => o.MatchTime);
                    if (dt == null)
                        _firstMatchTime = DateTime.Now;
                    else
                        _firstMatchTime = dt;
                }
                return _firstMatchTime;
            }
            //set { _firstMatchTime = value; }
        }
        //最大日期差
        private int _nowMatchTimeDiff;
        public int NowMatchTimeDiff
        {
            get
            {
                if (_nowMatchTimeDiff == 0)
                {
                    TimeSpan nowDif = matchtime.Value - firstMatchTime.Value;
                    _nowMatchTimeDiff = nowDif.Days;
                }
                return _nowMatchTimeDiff;
            }
            //set { _nowMatchTimeDiff = value; }
        }
        //预测输入数据源
        private List<MatchPoint<int>> _listMatchPointData;
        public List<MatchPoint<int>> ListMatchPointData
        {
            get
            {
                if (_listMatchPointData == null)
                {
                    List<MatchPoint<int>> matchpoints = new List<MatchPoint<int>>();
                    var reTOP20 = Top20.OrderBy(o => o.MatchTime);
                    foreach (var m in reTOP20)
                    {
                        MatchPoint<int> p = new MatchPoint<int>();
                        TimeSpan minDif = m.MatchTime.Value - firstMatchTime.Value;
                        p.LastMatchOverTime = minDif.Days;//时间差赋值，横坐标
                        p.matchTime = m.MatchTime.Value; //比赛时间
                        p.LastMatchGoals = m.FullHomeGoals.Value + m.FullAwayGoals.Value; //进球总数
                        p.LastMatchOddEven = ConvertGoalsToOE(p.LastMatchGoals);   //进球单双
                        if (m.HomeTeamBig == HomeTeamBig || m.AwayTeamBig == AwayTeamBig)
                            p.LastMatchScore = m.FullHomeGoals.Value - m.FullAwayGoals.Value; //胜负
                        else
                            p.LastMatchScore = m.FullAwayGoals.Value - m.FullHomeGoals.Value;
                        p.LastMatchWDL = ConvertGoalsToWDL(p.LastMatchScore);  //进球多少
                        //记录提示
                        p.matchDetail = m.HomeTeam + "<------->" + m.AwayTeam + "\r\n" +
                            m.Odds + "<------->" + m.WinLossBig+ "<------->" + m.MatchType + "\r\n" +
                            m.FullHomeGoals.ToString() + "<------->" + m.FullAwayGoals.ToString() + "\r\n";
                        matchpoints.Add(p);
                    }
                    _listMatchPointData = matchpoints;
                }
                return _listMatchPointData;
            }
            //set { _listMatchPointData = value; }
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
                    //using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext())
                    using (SoccerScoreSqlite matches = new SoccerScoreSqlite(Conn.cnn))
                    {
                        var jz = matches.ResultTBLib.Where(e => e.HomeTeamBig == HomeTeamBig && e.AwayTeamBig == AwayTeamBig).OrderByDescending(e => e.MatchTime);
                        foreach (var m in jz)
                        {
                            jzText += m.MatchTime.Value.ToShortDateString() + "::" +
                                m.FullHomeGoals.ToString() + "-" + m.FullAwayGoals.ToString() + "::" +
                                m.Odds+ "::" + m.WinLossBig + "::" + m.HomeTeam + "::" + m.AwayTeam+ "\r\n";
                        }
                    }
                    _listLastJZ = jzText;
                }
                return _listLastJZ;

            }
            //set { _listLastJZ = value; }
        }
    }
}
