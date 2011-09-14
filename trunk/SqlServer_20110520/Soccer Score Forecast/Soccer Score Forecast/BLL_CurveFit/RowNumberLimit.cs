﻿using System;
using System.Collections.Generic;
using SoccerScore.Compact.Linq;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Soccer_Score_Forecast
{
    public class RowNumberLimit : IDisposable
    {
        public int live_id;
        public int? home_team_big;
        public int? away_team_big;
        public string home_team;
        public string away_team;
        public DateTime? matchtime;
        public string matchtype;
        public int Top20Count;
        public IEnumerable<Result_tb_lib> Top20;

        public RowNumberLimit(int liveid)
        {
            ForeCastInit(liveid);
        }
        private void ForeCastInit(int liveid)
        {
            using (DataClassesMatchDataContext matches = new DataClassesMatchDataContext(Conn.conn))
            {
                //if (dMatch.dNew == false)
                //{
                //    dMatch.dHome = matches.Result_tb_lib.ToLookup(e => e.Home_team_big);
                //    dMatch.dAway = matches.Result_tb_lib.ToLookup(e => e.Away_team_big);
                //    dMatch.macauPre = matches.MacauPredication.ToLookup(e => e.Home_team);
                //    dMatch.liveTables= matches.Live_Table_lib.ToLookup(e => e.Live_table_lib_id);
                //    dMatch.dNew = true;
                //}

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
                Top20 = top20hh.Where(e => e.Match_type == matchtype).OrderByDescending(e => e.Match_time).Take(40);

                // .ToList();

                //var top20h = matches.result_tb_lib.Where(e => e.home_team_big == l.home_team_big || e.away_team_big == l.away_team_big);
                //var top20a = matches.result_tb_lib.Where(e => e.home_team_big == l.away_team_big || e.away_team_big == l.home_team_big);
                //Top20 = top20h.Union(top20a).Where(e => e.match_time < matchtime).OrderByDescending(e => e.match_time).Take(40).ToList();
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

                    var hg1 = Top20.Where(e => e.Home_team_big == home_team_big).Sum(e => e.Full_home_goals);
                    var hg2 = Top20.Where(e => e.Away_team_big == home_team_big).Sum(e => e.Full_away_goals);
                    var hg3 = Top20.Where(e => e.Home_team_big == away_team_big).Sum(e => e.Full_away_goals);
                    var hg4 = Top20.Where(e => e.Away_team_big == away_team_big).Sum(e => e.Full_home_goals);
                    var hg5 = Top20.Where(e => e.Away_team_big == away_team_big && e.Home_team_big == home_team_big).Sum(e => e.Full_home_goals);
                    var hg6 = Top20.Where(e => e.Away_team_big == home_team_big && e.Home_team_big == away_team_big).Sum(e => e.Full_away_goals);
                    _homeGoals = Convert.ToDouble((hg1 == null ? 0 : hg1) +
                                        (hg2 == null ? 0 : hg2) +
                                        (hg3 == null ? 0 : hg3) +
                                        (hg4 == null ? 0 : hg4) -
                                        (hg5 == null ? 0 : hg5) -
                                        (hg6 == null ? 0 : hg6)) / Top20Count;
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
                    _awayGoals = Convert.ToDouble((ag1 == null ? 0 : ag1) +
                                         (ag2 == null ? 0 : ag2) +
                                         (ag3 == null ? 0 : ag3) +
                                         (ag4 == null ? 0 : ag4) -
                                         (ag5 == null ? 0 : ag5) -
                                         (ag6 == null ? 0 : ag6)) / Top20Count;
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
        //最早的时间值
        private DateTime? _firstMatchTime;
        public DateTime? firstMatchTime
        {
            get
            {
                if (_firstMatchTime == null)
                {
                    DateTime? dt = Top20.Min(o => o.Match_time);
                    if (dt == null)
                        _firstMatchTime = DateTime.Now;
                    else
                        _firstMatchTime = dt;
                }
                return _firstMatchTime;
            }
            set { _firstMatchTime = value; }
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
            set { _nowMatchTimeDiff = value; }
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
                    var reTOP20 = Top20.OrderBy(o => o.Match_time);
                    foreach (var m in reTOP20)
                    {
                        MatchPoint<int> p = new MatchPoint<int>();
                        TimeSpan minDif = m.Match_time.Value - firstMatchTime.Value;
                        p.LastMatchOverTime = minDif.Days;//时间差赋值，横坐标
                        p.matchTime = m.Match_time.Value; //比赛时间
                        p.LastMatchGoals = m.Full_home_goals.Value + m.Full_away_goals.Value; //进球总数
                        p.LastMatchOddEven = ConvertGoalsToOE(p.LastMatchGoals);   //进球单双
                        if (m.Home_team_big == home_team_big || m.Away_team_big == away_team_big)
                            p.LastMatchScore = m.Full_home_goals.Value - m.Full_away_goals.Value; //胜负
                        else
                            p.LastMatchScore = m.Full_away_goals.Value - m.Full_home_goals.Value;
                        p.LastMatchWDL = ConvertGoalsToWDL(p.LastMatchScore);  //进球多少
                        //记录提示
                        p.matchDetail = m.Home_team + "<------->" + m.Away_team + "\r\n" +
                            m.Odds + "<------->" + m.Win_loss_big + "<------->" + m.Match_type + "\r\n" +
                            m.Full_home_goals.ToString() + "<------->" + m.Full_away_goals.ToString() + "\r\n";
                        matchpoints.Add(p);
                    }
                    _listMatchPointData = matchpoints;
                }
                return _listMatchPointData;
            }
            set { _listMatchPointData = value; }
        }
        public List<MatchPoint<float>> CurveFit;
        private MatchPoint<float> CurveFitValue;
        public void initCurveFit()
        {
            if (Top20Count > 10)
            {
                CurveFit = ployfitSeries(ListMatchPointData, NowMatchTimeDiff);
                CurveFitValue = CurveFit.Last();
            }
        }

        #region Matlab和Csharp混合编程的方法，利用Matlab运算得出想要的一系列数据 之前数据
        // 反射的经典实现，变量的反复抽象，把变化部分减少到最小，增强稳定部分，有利于扩展
        private List<MatchPoint<float>> ployfitSeries(List<MatchPoint<int>> result, int LastNowDiff)
        {
            List<MatchPoint<float>> fitseries = new List<MatchPoint<float>>();
            //在此反射数据系列，计算需要拟合的成员，由于输入和输出的数据类型不同，使用泛型<>，T
            PropertyInfo[] field = typeof(MatchPoint<>).GetProperties();
            int size = result.Count();
            var fisfilter = field.Where(e => e.Name == "LastMatchWDL" || e.Name == "LastMatchGoals" || e.Name == "LastMatchOddEven");
            foreach (PropertyInfo fi in fisfilter)
            {
                double[] Y = new double[size];
                double[] X = new double[size];
                double[] X1 = new double[size];
                for (int i = 0; i < size; i++)
                {
                    //在此指定<>类型是整数
                    PropertyInfo fiIn = typeof(MatchPoint<int>).GetProperty(fi.Name);
                    //反射获取值
                    Y[i] = Convert.ToDouble(fiIn.GetValue(result[i], null));
                    X[i] = Convert.ToDouble(result[i].LastMatchOverTime);
                    if (i != size - 1)
                        X1[i] = Convert.ToDouble(result[i + 1].LastMatchOverTime);
                    else
                        X1[i] = Convert.ToDouble(LastNowDiff);
                    MatchPoint<float> f = new MatchPoint<float>();
                    f.LastMatchOverTime = (float)X1[i];
                    if (fitseries.Count == i) fitseries.Add(f);  //如果累加就不行，这种方式，最大值不超过i
                }
                myCurveFitclass mmm = new myCurveFitclass(X, Y);
                mmm.CurvefitValue(LastNowDiff);
                //在此指定<>类型是小数
                PropertyInfo fiOut = typeof(MatchPoint<float>).GetProperty(fi.Name); //这里改属性
                //反射设定值 
                for (int j = 0; j < mmm.PredictionsNew.Length; j++) { fiOut.SetValue(fitseries[j], (float)mmm.PredictionsNew[j], null); }
            }
            return fitseries;
        }
        #endregion

        //预测值 胜平负
        public double CureFitWinLoss()
        {
            //剔除没有记录的
            if (Top20Count < 10) return 0;
            if (CurveFitValue == null) return 0;
            //double curvefit = CsharpMatlab.ployfitNowWDL(ListMatchPointData, NowMatchTimeDiff);
            double curvefit = CurveFitValue.LastMatchWDL;
            //double.NaN无穷大的问题
            if (double.IsNaN(curvefit))
                return 0;
            else
                return curvefit;
        }
        //预测值 进球数
        public double CureFitGoals()
        {

            //剔除没有记录的
            if (Top20Count < 10) return 0;
            if (CurveFitValue == null) return 0;
            double curvefit = CurveFitValue.LastMatchGoals;
            //double curvefit = CsharpMatlab.ployfitNowGoals (ListMatchPointData, NowMatchTimeDiff);
            //double.NaN无穷大的问题
            if (double.IsNaN(curvefit))
                return 0;
            else
                return curvefit;
        }
        //预测值 单双
        public double CureFitOddEven()
        {
            //剔除没有记录的
            if (Top20Count < 10) return 0;
            if (CurveFitValue == null) return 0;
            double curvefit = CurveFitValue.LastMatchOddEven;
            //double curvefit = CsharpMatlab.ployfitNowOE (ListMatchPointData, NowMatchTimeDiff);
            //double.NaN无穷大的问题
            if (double.IsNaN(curvefit))
                return 0;
            else
                return curvefit;
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

        private string macaupre;
        public string MacauPre
        {
            get
            {
                if (macaupre == null)
                {
                    var hometeam = dMatch.macauPre
                        .Where(e => e.Key.Length > 1)
                        .Where(e => home_team.IndexOf(e.Key) != -1)
                        .Select(e => e.Key).FirstOrDefault();
                    if (hometeam != null)
                    {
                        //澳门预测做优化处理，主对频繁比赛，避免关系弄错
                        var macau = dMatch.macauPre[hometeam]
                                .Where(e => e.Away_team != null)
                                .Where(e => away_team.IndexOf(e.Away_team) != -1)
                                .OrderByDescending(e => e.MacauPredication_id)
                                .Select(e => e.Macauslot).FirstOrDefault();
                        macaupre = "\r\n" + macau + "\r\n";
                    }
                }
                return macaupre;
            }
            set
            {
                macaupre = value;
            }
        }

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
                                   .Average(e => e.Full_home_goals - e.Full_away_goals);

                    var aCross = dMatch.dHome[away_team_big]
                                   .Where(e => e.Away_team_big == home_team_big)
                                   .Where(e => e.Match_time.Value.Date < matchtime.Value.Date)
                                    .Average(e => e.Full_home_goals - e.Full_away_goals);

                    _CrossGoals = (ConvertDoubleP(hCross) - ConvertDoubleP(aCross)) / 2;
                }
                return _CrossGoals;
            }
            set { _CrossGoals = value; }
        }


        private int _RecentScores;
        public int RecentScores
        {
            get
            {
                if (_RecentScores == 0)
                {
                    var homeTop20t = Top20.Where(e => e.Home_team_big == home_team_big);
                    var homeTop20tt = Top20.Where(e => e.Away_team_big == home_team_big);
                    var homeTop20ttt = homeTop20tt.Union(homeTop20t);
                    var hCross = homeTop20ttt.OrderByDescending(e => e.Match_time).FirstOrDefault();

                    var awayTop20t = Top20.Where(e => e.Home_team_big == away_team_big);
                    var awayTop20tt = Top20.Where(e => e.Away_team_big == away_team_big);
                    var awayTop20ttt = awayTop20tt.Union(awayTop20t);
                    var aCross = awayTop20ttt.OrderByDescending(e => e.Match_time).FirstOrDefault();

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

                        _RecentScores = h + a;
                    }
                }
                return _RecentScores;
            }
            set { _RecentScores = value; }
        }
        private double ConvertDoubleP(double? ddd)
        {
            if (ddd == null) return 0;
            else return (double)ddd;
        }
        //private string hostX;
        //private string awayX;
        //private string hostawayX;
        //private List<Result_tb_lib> hostSeriesX;
        //private List<Result_tb_lib> awaySeriesX;
        //private List<Result_tb_lib> hostawaySeriesX;
        #endregion



        #region dispose

        // Pointer to an external unmanaged resource.
        private IntPtr handle;
        // Other managed resource this class uses.
       // private Component Components;
        // Track whether Dispose has been called.
        private bool disposed = false;

        // Constructor for the BaseResource object.
        //public BaseResource()
        //{
        //   // Insert appropriate constructor code here.
        //}

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // Take yourself off the Finalization queue 
            // to prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed.
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                //if (disposing)
                //{
                //    // Dispose managed resources.
                //    Components.Dispose();
                //}
                // Release unmanaged resources. If disposing is false, 
                // only the following code is executed.
                CloseHandle(handle);
                handle = IntPtr.Zero;
                // Note that this is not thread safe.
                // Another thread could start disposing the object
                // after the managed resources are disposed,
                // but before the disposed flag is set to true.
                // If thread safety is necessary, it must be
                // implemented by the client.

            }
            disposed = true;
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method 
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        //~BaseResource()      
        ~RowNumberLimit()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        // Allow your Dispose method to be called multiple times,
        // but throw an exception if the object has been disposed.
        // Whenever you do something with this class, 
        // check to see if it has been disposed.
        public void DoSomething()
        {
            if (this.disposed)
            {
                //throw new ObjectDisposedException();
            }
        }


        // Design pattern for a derived class.
        // Note that this derived class inherently implements the 
        // IDisposable interface because it is implemented in the base class.


        public void Close()
        {
            // Calls the Dispose method without parameters.
            Dispose();
        }

        #endregion
    }
}
