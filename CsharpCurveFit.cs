using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace Soccer_Score_Forecast
{
    public class Regression
    {
        private double[] coefficients;
        private double[] predicted;

        public double[] Coefficients
        {
            get { return coefficients; }
        }

        public double[] Predictions
        {
            get { return predicted; }
        }

        public Regression(double[] xdata, double[] ydata, int degree)
        {
            int numberOfSamples = xdata.Length;
            //Define the results
            coefficients = new double[degree + 1];
            predicted = new double[numberOfSamples];
            //Set up X and Y Matricies
            Matrix YMatrix = new Matrix(ydata, numberOfSamples);
            Matrix XMatrix = new Matrix(numberOfSamples, degree + 1);
            XMatrix.SetColumnVector(new Vector(numberOfSamples, 1.0), 0);
            for (int i = 0; i < degree; i++)
            {
                XMatrix.SetColumnVector(new Vector(RaisePower(xdata, i + 1)), i + 1);
            }
            //Do the calcs
            Matrix XTranspose = Matrix.Transpose(XMatrix);
            Matrix Temp = (XTranspose * XMatrix).Inverse();
            Matrix hat = XMatrix.Multiply(Temp).Multiply(XTranspose);
            Matrix result = Temp.Multiply(XTranspose * YMatrix);
            //Get the coefficients
            coefficients = result.GetColumnVector(0);
            //Calculate the predicted values
            Matrix Y = new Matrix(ydata, ydata.Count());
            predicted = hat.Multiply(Y).GetColumnVector(0);
        }

        private double[] RaisePower(double[] data, int power)
        {
            double[] result = new double[data.GetLength(0)];
            for (int i = 0; i < data.GetLength(0); i++)
            {
                result[i] = Math.Pow(data[i], power);
            }
            return result;
        }
    }
    public class myCurveFitclass
    {
        private const int degree = 4;
        private List<double> Coefficients;
        private Regression testRegression;
        pu double PolyfitValue;
        public List<double> Predictions;
        public List<double> NewPredictions;
        public myCurveFitclass(double[] x, double[] y)
        {
            testRegression = new Regression(x, y, degree);
            Coefficients = testRegression.Coefficients.ToList();
            Predictions = testRegression.Predictions.ToList();

        }
        public void CurvefitValue(double x)
        {
            for (int i = 0; i < Coefficients.Count(); i++)
                PolyfitValue += Coefficients[i] * Math.Pow(x, i);
            NewPredictions = Predictions;
            NewPredictions.Remove(0);
            NewPredictions.Add(PolyfitValue);
        }
    }
}
