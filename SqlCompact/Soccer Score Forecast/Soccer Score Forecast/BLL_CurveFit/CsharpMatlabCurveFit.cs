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
    class CsharpMatlab
    {
        #region  之前数据
        //Matlab和Csharp混合编程的方法，利用Matlab运算得出想要的一系列数据
        public static string ployfitStr(string result)
        {
            myCurveFit.myCurveFitclass mmm = new myCurveFit.myCurveFitclass();
            string[] yS = result.Trim().Split();
            int size = yS.Length;
            double[] Y = new double[size];
            double[] X = new double[size];
            double[] X1 = new double[size];
            for (int i = 0; i < size; i++)
            {
                Y[i] = Convert.ToDouble(yS[i]); X[i] = i; X1[i] = i;
            }
            MWNumericArray mx_x1 = X1;
            MWNumericArray mx_X = X;
            MWNumericArray mx_Y = Y;
            MWNumericArray mx_T = 4;
            MWArray[] mx_B = mmm.mypolyfit(1, mx_X, mx_Y, mx_T);//Matlab函数polyfit拟合
            MWArray[] mx_F = mmm.mypolyval(1, mx_B[0], mx_x1);//Matlab函数polyval预测
            MWNumericArray mx_y1 = (MWNumericArray)mx_F[0];
            double[,] csArray = (double[,])mx_y1.ToArray(MWArrayComponent.Real);
            string cs = null;
            for (int j = 0; j < csArray.Length; j++)
            {
                cs = cs + " " + csArray[0, j].ToString();
            }
            return cs;
        }
        //Matlab和Csharp混合编程的方法，利用Matlab运算得出想要的一个单数据
        public static string ployfitVal(string result)
        {
            myCurveFit.myCurveFitclass mmm = new myCurveFit.myCurveFitclass();
            string[] yS = result.Trim().Split();
            int size = yS.Length;
            double[] Y = new double[size];
            double[] X = new double[size];
            double[] X1 = new double[size];
            for (int i = 0; i < size; i++)
            {
                Y[i] = Convert.ToDouble(yS[i]);
                X[i] = i;
                X1[i] = i + 1;
            }
            MWNumericArray mx_x1 = X1;
            MWNumericArray mx_X = X;
            MWNumericArray mx_Y = Y;
            MWNumericArray mx_T = 4;
            MWArray[] mx_B = mmm.mypolyfit(1, mx_X, mx_Y, mx_T);//Matlab函数polyfit拟合
            MWArray[] mx_F = mmm.mypolyval(1, mx_B[0], mx_x1);//Matlab函数polyval预测
            MWNumericArray mx_y1 = (MWNumericArray)mx_F[0];
            double[,] csArray = (double[,])mx_y1.ToArray(MWArrayComponent.Real);
            return csArray[0, csArray.Length - 1].ToString();
        }
        #endregion

        #region   //Matlab和Csharp混合编程的方法，利用Matlab运算得出想要的一个单数据
        public static float ployfitNowWDL(List<MatchPoint<int>> result, int LastNowDiff)
        {
            List<MatchPoint<float>> fitseries = new List<MatchPoint<float>>();
            myCurveFit.myCurveFitclass mmm = new myCurveFit.myCurveFitclass();
            //string[] yS = result.Trim().Split();
            int size = result.Count();
            double[] Y = new double[size];
            double[] X = new double[size];
            double[] X1 = new double[size];
            for (int i = 0; i < size; i++)
            {

                Y[i] = Convert.ToDouble(result[i].LastMatchWDL);
                X[i] = Convert.ToDouble(result[i].LastMatchOverTime);
                if (i != size - 1)
                {
                    X1[i] = Convert.ToDouble(result[i + 1].LastMatchOverTime);
                }
                else
                {
                    X1[i] = Convert.ToDouble(LastNowDiff);
                }
                MatchPoint<float> f = new MatchPoint<float>();
                f.LastMatchOverTime = (float)X1[i];
                fitseries.Add(f);
            }
            MWNumericArray mx_x1 = X1;
            MWNumericArray mx_X = X;
            MWNumericArray mx_Y = Y;
            MWNumericArray mx_T = 4;
            MWArray[] mx_B = mmm.mypolyfit(1, mx_X, mx_Y, mx_T);//Matlab函数polyfit拟合
            MWArray[] mx_F = mmm.mypolyval(1, mx_B[0], mx_x1);//Matlab函数polyval预测
            MWNumericArray mx_y1 = (MWNumericArray)mx_F[0];
            double[,] csArray = (double[,])mx_y1.ToArray(MWArrayComponent.Real);
            return (float)csArray[0, csArray.Length - 1];
        }
        public static float ployfitNowGoals(List<MatchPoint<int>> result, int LastNowDiff)
        {
            List<MatchPoint<float>> fitseries = new List<MatchPoint<float>>();
            myCurveFit.myCurveFitclass mmm = new myCurveFit.myCurveFitclass();
            //string[] yS = result.Trim().Split();
            int size = result.Count();
            double[] Y = new double[size];
            double[] X = new double[size];
            double[] X1 = new double[size];
            for (int i = 0; i < size; i++)
            {

                Y[i] = Convert.ToDouble(result[i].LastMatchGoals);
                X[i] = Convert.ToDouble(result[i].LastMatchOverTime);
                if (i != size - 1)
                {
                    X1[i] = Convert.ToDouble(result[i + 1].LastMatchOverTime);
                }
                else
                {
                    X1[i] = Convert.ToDouble(LastNowDiff);
                }
                MatchPoint<float> f = new MatchPoint<float>();
                f.LastMatchOverTime = (float)X1[i];
                fitseries.Add(f);
            }
            MWNumericArray mx_x1 = X1;
            MWNumericArray mx_X = X;
            MWNumericArray mx_Y = Y;
            MWNumericArray mx_T = 4;
            MWArray[] mx_B = mmm.mypolyfit(1, mx_X, mx_Y, mx_T);//Matlab函数polyfit拟合
            MWArray[] mx_F = mmm.mypolyval(1, mx_B[0], mx_x1);//Matlab函数polyval预测
            MWNumericArray mx_y1 = (MWNumericArray)mx_F[0];
            double[,] csArray = (double[,])mx_y1.ToArray(MWArrayComponent.Real);
            return (float)csArray[0, csArray.Length - 1];
        }
        public static float ployfitNowOE(List<MatchPoint<int>> result, int LastNowDiff)
        {
            List<MatchPoint<float>> fitseries = new List<MatchPoint<float>>();
            myCurveFit.myCurveFitclass mmm = new myCurveFit.myCurveFitclass();
            //string[] yS = result.Trim().Split();
            int size = result.Count();
            double[] Y = new double[size];
            double[] X = new double[size];
            double[] X1 = new double[size];
            for (int i = 0; i < size; i++)
            {

                Y[i] = Convert.ToDouble(result[i].LastMatchOddEven);
                X[i] = Convert.ToDouble(result[i].LastMatchOverTime);
                if (i != size - 1)
                {
                    X1[i] = Convert.ToDouble(result[i + 1].LastMatchOverTime);
                }
                else
                {
                    X1[i] = Convert.ToDouble(LastNowDiff);
                }
                MatchPoint<float> f = new MatchPoint<float>();
                f.LastMatchOverTime = (float)X1[i];
                fitseries.Add(f);
            }
            MWNumericArray mx_x1 = X1;
            MWNumericArray mx_X = X;
            MWNumericArray mx_Y = Y;
            MWNumericArray mx_T = 4;
            MWArray[] mx_B = mmm.mypolyfit(1, mx_X, mx_Y, mx_T);//Matlab函数polyfit拟合
            MWArray[] mx_F = mmm.mypolyval(1, mx_B[0], mx_x1);//Matlab函数polyval预测
            MWNumericArray mx_y1 = (MWNumericArray)mx_F[0];
            double[,] csArray = (double[,])mx_y1.ToArray(MWArrayComponent.Real);
            return (float)csArray[0, csArray.Length - 1];
        }
        #endregion
        #region Matlab和Csharp混合编程的方法，利用Matlab运算得出想要的一系列数据 之前数据
        public static List<MatchPoint<float>> ployfitSeriesWDL(List<MatchPoint<int>> result, int LastNowDiff)
        {
            List<MatchPoint<float>> fitseries = new List<MatchPoint<float>>();
            myCurveFit.myCurveFitclass mmm = new myCurveFit.myCurveFitclass();
            //string[] yS = result.Trim().Split();
            int size = result.Count();
            double[] Y = new double[size];
            double[] X = new double[size];
            double[] X1 = new double[size];
            for (int i = 0; i < size; i++)
            {

                Y[i] = Convert.ToDouble(result[i].LastMatchWDL);
                X[i] = Convert.ToDouble(result[i].LastMatchOverTime);
                if (i != size - 1)
                {
                    X1[i] = Convert.ToDouble(result[i + 1].LastMatchOverTime);
                }
                else
                {
                    X1[i] = Convert.ToDouble(LastNowDiff);
                }
                MatchPoint<float> f = new MatchPoint<float>();
                f.LastMatchOverTime = (float)X1[i];
                fitseries.Add(f);
            }
            MWNumericArray mx_x1 = X1;
            MWNumericArray mx_X = X;
            MWNumericArray mx_Y = Y;
            MWNumericArray mx_T = 4;
            MWArray[] mx_B = mmm.mypolyfit(1, mx_X, mx_Y, mx_T);//Matlab函数polyfit拟合
            MWArray[] mx_F = mmm.mypolyval(1, mx_B[0], mx_x1);//Matlab函数polyval预测
            MWNumericArray mx_y1 = (MWNumericArray)mx_F[0];
            double[,] csArray = (double[,])mx_y1.ToArray(MWArrayComponent.Real);
            for (int j = 0; j < csArray.Length; j++)
            {
                fitseries[j].LastMatchWDL = (float)csArray[0, j];
            }
            return fitseries;
        }

        public static List<MatchPoint<float>> ployfitSeriesGoals(List<MatchPoint<int>> result, int LastNowDiff)
        {
            List<MatchPoint<float>> fitseries = new List<MatchPoint<float>>();
            myCurveFit.myCurveFitclass mmm = new myCurveFit.myCurveFitclass();
            //string[] yS = result.Trim().Split();
            int size = result.Count();
            double[] Y = new double[size];
            double[] X = new double[size];
            double[] X1 = new double[size];
            for (int i = 0; i < size; i++)
            {

                Y[i] = Convert.ToDouble(result[i].LastMatchGoals);
                X[i] = Convert.ToDouble(result[i].LastMatchOverTime);
                if (i != size - 1)
                {
                    X1[i] = Convert.ToDouble(result[i + 1].LastMatchOverTime);
                }
                else
                {
                    X1[i] = Convert.ToDouble(LastNowDiff);
                }
                MatchPoint<float> f = new MatchPoint<float>();
                f.LastMatchOverTime = (float)X1[i];
                fitseries.Add(f);
            }
            MWNumericArray mx_x1 = X1;
            MWNumericArray mx_X = X;
            MWNumericArray mx_Y = Y;
            MWNumericArray mx_T = 4;
            MWArray[] mx_B = mmm.mypolyfit(1, mx_X, mx_Y, mx_T);//Matlab函数polyfit拟合
            MWArray[] mx_F = mmm.mypolyval(1, mx_B[0], mx_x1);//Matlab函数polyval预测
            MWNumericArray mx_y1 = (MWNumericArray)mx_F[0];
            double[,] csArray = (double[,])mx_y1.ToArray(MWArrayComponent.Real);
            for (int j = 0; j < csArray.Length; j++)
            {
                fitseries[j].LastMatchGoals = (float)csArray[0, j];
            }
            return fitseries;
        }

        public static List<MatchPoint<float>> ployfitSeriesOE(List<MatchPoint<int>> result, int LastNowDiff)
        {
            List<MatchPoint<float>> fitseries = new List<MatchPoint<float>>();
            myCurveFit.myCurveFitclass mmm = new myCurveFit.myCurveFitclass();
            //string[] yS = result.Trim().Split();
            int size = result.Count();
            double[] Y = new double[size];
            double[] X = new double[size];
            double[] X1 = new double[size];
            for (int i = 0; i < size; i++)
            {

                Y[i] = Convert.ToDouble(result[i].LastMatchOddEven);
                X[i] = Convert.ToDouble(result[i].LastMatchOverTime);
                if (i != size - 1)
                {
                    X1[i] = Convert.ToDouble(result[i + 1].LastMatchOverTime);
                }
                else
                {
                    X1[i] = Convert.ToDouble(LastNowDiff);
                }
                MatchPoint<float> f = new MatchPoint<float>();
                f.LastMatchOverTime = (float)X1[i];
                fitseries.Add(f);
            }
            MWNumericArray mx_x1 = X1;
            MWNumericArray mx_X = X;
            MWNumericArray mx_Y = Y;
            MWNumericArray mx_T = 4;
            MWArray[] mx_B = mmm.mypolyfit(1, mx_X, mx_Y, mx_T);//Matlab函数polyfit拟合
            MWArray[] mx_F = mmm.mypolyval(1, mx_B[0], mx_x1);//Matlab函数polyval预测
            MWNumericArray mx_y1 = (MWNumericArray)mx_F[0];
            double[,] csArray = (double[,])mx_y1.ToArray(MWArrayComponent.Real);
            for (int j = 0; j < csArray.Length; j++)
            {
                fitseries[j].LastMatchOddEven = (float)csArray[0, j];
            }
            return fitseries;
        }

        #endregion

        #region  反射的经典实现，变量的反复抽象，把变化部分减少到最小，增强稳定部分，有利于扩展
        public static List<MatchPoint<float>> ployfitSeries(List<MatchPoint<int>> result, int LastNowDiff)
        {
            myCurveFit.myCurveFitclass mmm = new myCurveFit.myCurveFitclass();
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
                    if(fitseries.Count ==i) fitseries.Add(f);  //如果累加就不行，这种方式，最大值不超过i
                }
                MWNumericArray mx_x1 = X1;
                MWNumericArray mx_X = X;
                MWNumericArray mx_Y = Y;
                MWNumericArray mx_T = 4;
                MWArray[] mx_B = mmm.mypolyfit(1, mx_X, mx_Y, mx_T);//Matlab函数polyfit拟合
                MWArray[] mx_F = mmm.mypolyval(1, mx_B[0], mx_x1);//Matlab函数polyval预测
                MWNumericArray mx_y1 = (MWNumericArray)mx_F[0];
                double[,] csArray = (double[,])mx_y1.ToArray(MWArrayComponent.Real);
                //在此指定<>类型是小数
                FieldInfo fiOut = typeof(MatchPoint<float>).GetField(fi.Name);
                //反射设定值 
                for (int j = 0; j < csArray.Length; j++) { fiOut.SetValue(fitseries[j], (float)csArray[0, j]); }
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
        public static ILookup<int?, result_tb_lib> dHome;
        public static ILookup<int?, result_tb_lib> dAway;

    }
}
