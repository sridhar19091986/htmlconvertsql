using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace Soccer_Score_Forecast
{
    public class LoadDataToChart
    {
        static List<MatchPoint<int>> matchpoints = new List<MatchPoint<int>>();
        static List<MatchPoint<float>> fit= new List<MatchPoint<float>>();
        public static RowNumberLimit rnl;
        public static string ForeCast(Chart chart1, int id, string title)
        {
            foreach (Series ser in chart1.Series) ser.Points.Clear(); //清理chart1上的数据
            string forecast = null;

            rnl = new RowNumberLimit(id);
            rnl.initCurveFit();
            matchpoints = rnl.ListMatchPointData;  //生成历史数据
            fit = rnl.CurveFit;//生成预测数据

            for (int i = 0; i < matchpoints.Count(); i++)
            {
                chart1.Series["RealScore"].Points.AddXY(matchpoints[i].matchTime, matchpoints[i].LastMatchScore);
                //WinLoss
                chart1.Series["RealWDL"].Points.AddXY(matchpoints[i].matchTime, matchpoints[i].LastMatchWDL);
                chart1.Series["RealGoals"].Points.AddXY(matchpoints[i].matchTime, matchpoints[i].LastMatchGoals);
                //chart1.Series["RealOddEven"].Points.AddXY(matchpoints[i].matchTime, matchpoints[i].LastMatchOddEven);
                //WinLoss forecast
                if (i != matchpoints.Count() - 1)
                {
                    chart1.Series["ForeWDL"].Points.AddXY(matchpoints[i + 1].matchTime, fit[i].LastMatchWDL);
                }
                else
                {
                    chart1.Series["ForeWDL"].Points.AddXY(rnl.matchtime, fit[i].LastMatchWDL);
                    forecast = fit[i].LastMatchWDL.ToString();
                    chart1.Series["ForeWDL"].Points[i].Label = forecast + "::WDL";
                    chart1.Series["ForeWDL"].Points[i].LabelBorderColor = Color.Red; ;
                }
                //Goals forecast
                if (i != matchpoints.Count() - 1)
                {
                    chart1.Series["ForeGoals"].Points.AddXY(matchpoints[i + 1].matchTime, fit[i].LastMatchGoals);
                }
                else
                {
                    chart1.Series["ForeGoals"].Points.AddXY(rnl.matchtime, fit[i].LastMatchGoals);
                    forecast = fit[i].LastMatchGoals.ToString();
                    chart1.Series["ForeGoals"].Points[i].Label = forecast + "::Goals";
                    chart1.Series["ForeGoals"].Points[i].LabelBorderColor = Color.Red;
                }
                //OddEven forecast
                //if (i != matchpoints.Count() - 1)
                //    chart1.Series["ForeOddEven"].Points.AddXY(matchpoints[i + 1].matchTime, fitOE[i].LastMatchOddEven);
                //else
                //    chart1.Series["ForeOddEven"].Points.AddXY(rnl.matchtime, fitOE[i].LastMatchOddEven);
            }
            //chart1.Titles["Title1"].Text = title;
            BaseChartFormat(chart1);

            return rnl.ListLastJZ +rnl.MacauPre;
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
        public static void LabelMatchDetail(Chart chart1, string strKey)
        {
            //Label
            //chart1.Series["Series2"].Points[i].Label = fitWDL[i].LastMatchOverTime.ToString();
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
                    chart1.Series["RealGoals"].Points[i].Label = matchpoints[i].LastMatchGoals.ToString();
                    chart1.Series["RealGoals"].Points[i].LabelForeColor = Color.Red;
                    chart1.Series["RealGoals"].Points[i].LabelBorderColor = Color.Red;
                }
            if (strKey == "ScoreForecast")
                for (int i = 0; i < fit.Count(); i++)
                {
                    chart1.Series["ForeWDL"].Points[i].Label = fit[i].LastMatchWDL.ToString();
                    chart1.Series["ForeWDL"].Points[i].LabelForeColor = Color.Red;
                }
            if (strKey == "GoalsForecast")
                for (int i = 0; i < fit.Count(); i++)
                {
                    chart1.Series["ForeGoals"].Points[i].Label = fit[i].LastMatchGoals.ToString();
                    chart1.Series["ForeGoals"].Points[i].LabelForeColor = Color.Red;
                }
        }
    }
}
