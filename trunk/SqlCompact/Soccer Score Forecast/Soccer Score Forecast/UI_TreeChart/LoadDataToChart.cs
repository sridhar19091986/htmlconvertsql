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
using System.Windows.Forms.DataVisualization.Charting;
//using System.Windows.Forms.DataVisualization.Charting;
namespace Soccer_Score_Forecast
{
    public class LoadDataToChart
    {
        static List<matchPoint<int>> matchpoints = new List<matchPoint<int>>();
        static List<matchPoint<float>> fit= new List<matchPoint<float>>();
        public static string ForeCast(Chart chart1, int id, string title)
        {
            foreach (Series ser in chart1.Series) ser.Points.Clear(); //清理chart1上的数据
            string forecast = null;

            RowNumberLimit rnl = new RowNumberLimit(id);
            matchpoints = rnl.ListmatchPointData;  //生成历史数据
            fit = rnl.CurveFit;//生成预测数据

            for (int i = 0; i < matchpoints.Count(); i++)
            {
                chart1.Series["RealScore"].Points.AddXY(matchpoints[i].matchTime, matchpoints[i].LastmatchScore);
                //WinLoss
                chart1.Series["RealWDL"].Points.AddXY(matchpoints[i].matchTime, matchpoints[i].LastmatchWDL);
                chart1.Series["RealGoals"].Points.AddXY(matchpoints[i].matchTime, matchpoints[i].LastmatchGoals);
                //chart1.Series["RealOddEven"].Points.AddXY(matchpoints[i].matchTime, matchpoints[i].LastmatchOddEven);
                //WinLoss forecast
                if (i != matchpoints.Count() - 1)
                {
                    chart1.Series["ForeWDL"].Points.AddXY(matchpoints[i + 1].matchTime, fit[i].LastmatchWDL);
                }
                else
                {
                    chart1.Series["ForeWDL"].Points.AddXY(rnl.matchtime, fit[i].LastmatchWDL);
                    forecast = fit[i].LastmatchWDL.ToString();
                    chart1.Series["ForeWDL"].Points[i].Label = forecast + "::WDL";
                    chart1.Series["ForeWDL"].Points[i].LabelBorderColor = Color.Red; ;
                }
                //Goals forecast
                if (i != matchpoints.Count() - 1)
                {
                    chart1.Series["ForeGoals"].Points.AddXY(matchpoints[i + 1].matchTime, fit[i].LastmatchGoals);
                }
                else
                {
                    chart1.Series["ForeGoals"].Points.AddXY(rnl.matchtime, fit[i].LastmatchGoals);
                    forecast = fit[i].LastmatchGoals.ToString();
                    chart1.Series["ForeGoals"].Points[i].Label = forecast + "::Goals";
                    chart1.Series["ForeGoals"].Points[i].LabelBorderColor = Color.Red;
                }
                //OddEven forecast
                //if (i != matchpoints.Count() - 1)
                //    chart1.Series["ForeOddEven"].Points.AddXY(matchpoints[i + 1].matchTime, fitOE[i].LastmatchOddEven);
                //else
                //    chart1.Series["ForeOddEven"].Points.AddXY(rnl.matchtime, fitOE[i].LastmatchOddEven);
            }
            //chart1.Titles["Title1"].Text = title;
            BaseChartFormat(chart1);

            return rnl.ListLastJZ;
        }
        public static void BaseChartFormat(Chart chart1)
        {
            MarkerStyle marker = MarkerStyle.Square;
            foreach (Series ser in chart1.Series)  //绘制线条
            {
                ser.ShadowOffset = 1;
                ser.BorderWidth = 2;
                ser.ChartType = SeriesChartType.Line;
                ser.MarkerSize = 6;
                ser.MarkerStyle = marker;
                ser.MarkerBorderColor = Color.FromArgb(64, 64, 64);
                ser.Font = new Font("Trebuchet MS", 8, FontStyle.Regular);
                marker++;
            }
            //chart1.Series["Series3"].YAxisType = AxisType.Secondary;
            chart1.ChartAreas["Default"].CursorX.IsUserEnabled = true; //Enable range selection
            chart1.ChartAreas["Default"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["Default"].AxisX.ScaleView.Zoomable = true;//zooming end user interface
            chart1.ChartAreas["Default"].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas["Default"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            //chart1.ChartAreas["Default"].AxisX.LineColor = Color.FromArgb(164, 164, 164);
        }
        public static void LabelmatchDetail(Chart chart1, string strKey)
        {
            //Label
            //chart1.Series["Series2"].Points[i].Label = fitWDL[i].LastmatchOverTime.ToString();
            if (strKey == "ClearLabel")
                for (int i = 0; i < matchpoints.Count(); i++)
                    foreach (Series ser in chart1.Series)
                        if (ser.Points.Count > i)
                            ser.Points[i].Label = "";
            if (strKey == "LastScore")
                for (int i = 0; i < matchpoints.Count(); i++)
                {
                    chart1.Series["RealScore"].Points[i].Label = matchpoints[i].matchDetail;
                    chart1.Series["RealScore"].Points[i].LabelForeColor = Color.Red;
                    chart1.Series["RealScore"].Points[i].LabelBorderColor = Color.Red;
                }
            if (strKey == "LastGoals")
                for (int i = 0; i < matchpoints.Count(); i++)
                {
                    chart1.Series["RealGoals"].Points[i].Label = matchpoints[i].LastmatchGoals.ToString();
                    chart1.Series["RealGoals"].Points[i].LabelForeColor = Color.Red;
                    chart1.Series["RealGoals"].Points[i].LabelBorderColor = Color.Red;
                }
            if (strKey == "ScoreForecast")
                for (int i = 0; i < fit.Count(); i++)
                {
                    chart1.Series["ForeWDL"].Points[i].Label = fit[i].LastmatchWDL.ToString();
                    chart1.Series["ForeWDL"].Points[i].LabelForeColor = Color.Red;
                }
            if (strKey == "GoalsForecast")
                for (int i = 0; i < fit.Count(); i++)
                {
                    chart1.Series["ForeGoals"].Points[i].Label = fit[i].LastmatchGoals.ToString();
                    chart1.Series["ForeGoals"].Points[i].LabelForeColor = Color.Red;
                }
        }
    }
}
