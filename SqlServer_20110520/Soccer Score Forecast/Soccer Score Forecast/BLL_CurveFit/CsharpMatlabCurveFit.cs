using System;
using System.Collections.Generic;
using System.Reflection;
using SoccerScore.Compact.Linq;
using System.Linq;

namespace Soccer_Score_Forecast
{
    public class MatchPoint<T>
    {
        private T lastmatchovertime_;
        public T LastMatchOverTime { get { return lastmatchovertime_; } set { lastmatchovertime_ = value; } }
        private T lastmatchscore_;
        public T LastMatchScore { get { return lastmatchscore_; } set { lastmatchscore_ = value; } }

        private T lastmatchwdl_;
        public T LastMatchWDL { get { return lastmatchwdl_; } set { lastmatchwdl_ = value; } }
        private T lastmatchgoals_;
        public T LastMatchGoals { get { return lastmatchgoals_; } set { lastmatchgoals_ = value; } }
        private T lastmatchoddeven_;
        public T LastMatchOddEven { get { return lastmatchoddeven_; } set { lastmatchoddeven_ = value; } }

        //public T RealScore;
        private DateTime? matchtime_;
        public DateTime? matchTime { get { return matchtime_; } set { matchtime_ = value; } }
        private string matchdetail_;
        public string matchDetail { get { return matchdetail_; } set { matchdetail_ = value; } }
        //public string Sdate;
    }
    public static class dMatch
    {
        public static ILookup<int?, Result_tb_lib> dHome = null;
        public static ILookup<int?, Result_tb_lib> dAway = null;
        public static ILookup<string, MacauPredication> macauPre= null;
        public static ILookup<int, Live_Table_lib> liveTables = null;
        public static bool dNew=false;
    }

    //public class MatlabNet
    //{
    //    public DateTime? Match_time;
    //    public string Match_type;
    //    public string Home_team;
    //    public string Away_team;
    //    public int? Home_w;
    //    public int? Home_d;
    //    public int? Home_l;
    //    public double? Home_goals;
    //    public double? Away_goals;
    //    public int? Full_home_goals;
    //    public int? Full_away_goals;
    //}

    public class CsharpMatlab
    {
        #region   //Matlab和Csharp混合编程的方法，利用Matlab运算得出想要的一个单数据
        public static float ployfitNowValue(List<MatchPoint<int>> result, int LastNowDiff, string key)
        {
            int size = result.Count();
            double[] Y = new double[size];
            double[] X = new double[size];
            for (int i = 0; i < size; i++)
            {
                if (key == "wdl") Y[i] = Convert.ToDouble(result[i].LastMatchWDL);
                if (key == "goals") Y[i] = Convert.ToDouble(result[i].LastMatchGoals);
                if (key == "oddeven") Y[i] = Convert.ToDouble(result[i].LastMatchOddEven);
                X[i] = Convert.ToDouble(result[i].LastMatchOverTime);
            }
            myCurveFitclass mmm = new myCurveFitclass(X, Y);
            mmm.CurvefitValue(LastNowDiff);
            return (float)mmm.PolyfitValue;
        }
        #endregion
        #region Matlab和Csharp混合编程的方法，利用Matlab运算得出想要的一系列数据 之前数据
        // 反射的经典实现，变量的反复抽象，把变化部分减少到最小，增强稳定部分，有利于扩展
        public static List<MatchPoint<float>> ployfitSeries(List<MatchPoint<int>> result, int LastNowDiff)
        {
            List<MatchPoint<float>> fitseries = new List<MatchPoint<float>>();
            //在此反射数据系列，计算需要拟合的成员，由于输入和输出的数据类型不同，使用泛型<>，T
            FieldInfo[] field = typeof(MatchPoint<>).GetFields();
            int size = result.Count();
            var fisfilter = field.Where(e => e.Name == "LastMatchWDL" || e.Name == "LastMatchGoals" || e.Name == "LastMatchOddEven");
            foreach (FieldInfo fi in fisfilter)
            {
                double[] Y = new double[size];
                double[] X = new double[size];
                double[] X1 = new double[size];
                for (int i = 0; i < size; i++)
                {
                    //在此指定<>类型是整数
                    FieldInfo fiIn = typeof(MatchPoint<int>).GetField(fi.Name);
                    //反射获取值
                    Y[i] = Convert.ToDouble(fiIn.GetValue(result[i]));
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
                FieldInfo fiOut = typeof(MatchPoint<float>).GetField(fi.Name);
                //反射设定值 
                for (int j = 0; j < mmm.PredictionsNew.Length; j++) { fiOut.SetValue(fitseries[j], (float)mmm.PredictionsNew[j]); }
            }
            return fitseries;
        }
        #endregion
    }
}
