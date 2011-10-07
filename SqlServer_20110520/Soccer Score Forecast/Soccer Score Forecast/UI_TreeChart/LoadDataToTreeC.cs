using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Soccer_Score_Forecast
{
   public partial class LoadDataToTree
    {
           private void TreeNodeBrushColor(TreeNode child,
            string home_team, string away_team, int? result_tb_lib_id,
            int grnncheck, string pnncheck, int fitgrnncomp, int goalnumcheck, int bjpk)
        {
            if (grnncheck >= 0) child.ForeColor = Color.Green;
            if (grnncheck >= 0 && fitgrnncomp > 0)
            {
                if (goalnumcheck != 0)
                {
                    if (home_team.IndexOf("*") != -1 && goalnumcheck > 0 ||
                        away_team.IndexOf("*") != -1 && goalnumcheck < 0)
                    {
                        child.ForeColor = Color.Red;  //上盘概率高，有利于投资
                    }
                    else
                    {
                        child.ForeColor = Color.Red;  //下盘概率低，看情况而定
                        child.BackColor = Color.Green;
                    }
                }
                else
                {
                    if (bjpk != 0)
                        child.ForeColor = Color.Blue;  //竞猜可用
                    else
                    {
                        child.ForeColor = Color.Blue;  //???
                        child.BackColor = Color.Green;
                    }
                }
            }
            //if (grnncheck >= 0 && fitgrnncomp >= 10) child.ForeColor = Color.Blue;
            //if (pnngrnncomp == 0 && grnncheck == 0) child.ForeColor = Color.Blue;
            //if (pnncheck == "-99") child.ForeColor = Color.Black;

            //结果验证
            if (result_tb_lib_id != null)
                //if (mar.Myfit != null)
                //if (mar.Myfit.IndexOf(result) != -1) 
                //child.ForeColor = Color.Red;
                child.NodeFont = new Font("Trebuchet MS", 10, FontStyle.Bold);
        }

        //List<double> minOdds = new List<double>();
        //double minOdd = 0;
        //minOdds.Add(ExtractDigital(lo.ok_1_0));
        //minOdds.Add(ExtractDigital(lo.ok_1_1));
        //minOdds.Add(ExtractDigital(lo.ok_1_2));
        //minOdd = minOdds.Min();
        //if (minOdds[0] == minOdd) strNode += "3";
        //if (minOdds[1] == minOdd) strNode += "1";
        //if (minOdds[2] == minOdd) strNode += "0";
        private double ExtractDigital(string str)
        {
            //Console.WriteLine("请输入一个字符串：");
            //string str = Console.ReadLine();
            string number = null;
            foreach (char item in str)
            {
                if (item >= 48 && item <= 58)
                {
                    number += item;
                }
                else
                {
                    break;
                }
            }
            //return Int32.Parse(number); 
            return double.Parse(number);
            // Console.WriteLine(number);
        }
        private string ForecastDraw(int? w, int? d, int? l)
        {
            int?[] wdl = { w, d, l };
            if (d == wdl.Min() && d != w && d != l) return "30";
            else return "1";
        }
        private int ComparePnnGrnn(string pnn, string grnn)
        {
            if (pnn == null || grnn == null) return 1;
            if (grnn.IndexOf(".") != -1) return 1;
            int comp = 0;
            string tgrnn = grnn.Substring(0, 3);
            //if(double.Parse(pnn) * double.Parse(tgrnn)<0) return 1;
            comp = Math.Abs(int.Parse(pnn) - int.Parse(tgrnn));
            return comp;
        }
        private GrnnResult CompareMyfitGrnn(string fit, string grnn)
        {
            if (fit == null || grnn == null) return GrnnResult.Nul;
            if (fit.IndexOf(grnn) != -1) return GrnnResult.Win;
            int temp = Int32.Parse(grnn) - 10;
            if (temp >= 0)
                if (fit.IndexOf(temp.ToString()) != -1)
                    return GrnnResult.pWin;
            return GrnnResult.Nul;
        }

        private int goalsnum = 0;

        private GrnnResult GrnnCheck(string grnn)
        {
            GrnnResult gr = GrnnResult.Nul;
            if (grnn == null) return gr;
            //if (grnn.IndexOf("-1") > 0) return GrnnResult.Lose;
            //if (grnn.IndexOf(".") != -1) return GrnnResult.Nul;
            List<int> goals = new List<int>();
            string[] lines = grnn.Split(new char[] { ' ' });
            foreach (string line in lines)
                if (line.Trim() != "")
                    goals.Add(Int32.Parse(line));
            if (goals.Count != 4) return gr;

            //if (Math.Abs(goals[1]) > 0) return GrnnResult.Nul;
            goalsnum = goals[1];

            if (goals[1] > 0) gr = GrnnResult.pWin;
            if (goals[1] < 0) gr = GrnnResult.pLose;

            if (goals[0] != goals[2] + goals[3]) return gr;
            if (goals[1] != goals[2] - goals[3]) return gr;

            if (goals[1] > 0) gr = GrnnResult.Win;
            if (goals[1] == 0) gr = GrnnResult.Draw;
            if (goals[1] < 0) gr = GrnnResult.Lose;

            return gr;

        }
        private enum GrnnResult
        {
            Win = 3,
            Draw = 1,
            Lose = 0,

            pWin = 3 + 10,
            pLose = 0 + 10,

            Nul = -1,
        }
    
    }
}
