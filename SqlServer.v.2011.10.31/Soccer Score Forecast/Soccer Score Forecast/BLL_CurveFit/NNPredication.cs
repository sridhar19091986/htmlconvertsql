using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RbfPredication;
using MathWorks.MATLAB.NET.Arrays;

namespace Soccer_Score_Forecast
{
    class NNPredication
    {
        private RbfPredicationclass rbf = new RbfPredicationclass();
        public string tempx = @"xite.sdf";
        public string tempy = @"yn.sdf";
        public string NewPnn()
        {
            return NewPnn(tempx, tempy);
        }
        public string NewGrnn()
        {
            return NewGrnn(tempx, tempy);
        }
        public string NewffWavelet()
        {
            return NewffWavelet(tempx, tempy);
        }
        public string NewffWavelet(string x, string y)
        {
            MWArray xx = (MWArray)x;
            MWArray yy = (MWArray)y;
            var wnn = rbf.WaveletPredication(xx, yy);
            string newwnn = wnn.ToString();
            return newwnn;
        }
        public string NewPnn(string x, string y)
        {
            MWArray xx = (MWArray)x;
            MWArray yy = (MWArray)y;
            var pnn = rbf.NewpnnPredication(xx, yy,0.7);//default = 0.1
            string newpnn = pnn.ToString();
            return newpnn;
        }
        public string NewGrnn(string x, string y)
        {
            MWArray xx = (MWArray)x;
            MWArray yy = (MWArray)y;
            var grnn = rbf.NewgrnnPredication(xx, yy,1.0); //default = 1.0
            string newgrnn = grnn.ToString();
            return newgrnn;
        }
        public NNPredication()
        {

        }
    }
}
