using System;
using System.Linq;
using System.Collections;

namespace Soccer_Score_Forecast
{
    public class myCurveFitclass
    {
        private const int degree = 4;
        private double[] Coefficients;
        private Regression testRegression;
        public double PolyfitValue;
        private double[] Predictions;
        public double[] PredictionsNew;
        public myCurveFitclass(double[] x, double[] y)
        {
            testRegression = new Regression(x, y, degree);
            Coefficients = testRegression.Coefficients;
            Predictions = testRegression.Predictions;

        }
        public void CurvefitValue(int x)
        {
            for (int i = 0; i < Coefficients.Count(); i++)
                PolyfitValue += Coefficients[i] * Math.Pow(x, i);
            ArrayList al = new ArrayList(Predictions);
            al.RemoveAt(0);
            al.Add(PolyfitValue);
            PredictionsNew = (double[])al.ToArray(typeof(double));
        }
    }
}
