using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Soccer_Score_Forecast
{
    public partial class RowNumberLimit : IDisposable
    {
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

    }
}
