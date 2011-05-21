using System;
using System.Collections.Generic;
using System.Reflection;
using SoccerScore.Compact.Linq;
using System.Linq;

namespace Soccer_Score_Forecast
{
    class CsharpMatlab
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


    public class MatchPoint<T>
    {
        public T LastMatchOverTime;
        public T LastMatchScore;

        public T LastMatchWDL;
        public T LastMatchGoals;
        public T LastMatchOddEven;

        //public T RealScore;
        public DateTime? matchTime;
        public string matchDetail;
        //public string Sdate;
    }
    static class dMatch
    {
        public static ILookup<int?, Result_tb_lib> dHome;
        public static ILookup<int?, Result_tb_lib> dAway;

    }
}
