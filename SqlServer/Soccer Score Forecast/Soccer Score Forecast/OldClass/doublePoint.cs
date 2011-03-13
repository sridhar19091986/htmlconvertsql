//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Drawing;

//namespace Soccer_Score_Forecast
//{
//    interface IPoint
//    {
//        double X
//        {
//            get;
//            set;
//        }
//        double Y
//        {
//            get;
//            set;
//        }
//    }

//    //结构也可以从接口继承
//    struct Point : IPoint
//    {
//        private double x, y;
//        //结构也可以增加构造函数
//        public Point(double X, double Y)
//        {
//            this.x = X;
//            this.y = Y;
//        }
//        public double X
//        {
//            get { return x; }
//            set { x = value; }
//        }
//        public double Y
//        {
//            get { return x; }
//            set { x = value; }
//        }
//    }

//}
