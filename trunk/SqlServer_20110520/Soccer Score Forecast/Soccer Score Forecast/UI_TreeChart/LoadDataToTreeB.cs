using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Soccer_Score_Forecast
{
  public partial  class LoadDataToTree
    {
      #region 相同循环体  ？？  ltl.match_type
      private void TreeNodeLoad(TreeNode tn)
      {
          var ltlmars = from p in ltls
                        join q in marAll on p.Live_table_lib_id equals q.Key
                        //orderby q.Pnn_fit
                        select p;

          foreach (var ltl in ltlmars)
          {
              //string fDraw = "";
              fit = 0;
              goals = 0;
              wdl = 0;
              //string fjz = "";

              myfit = "";

              macau = "";

              comp = "";

              //int pnngrnncomp = 1;

              //记录北京单场盘口
              bjpk = 0;

              grnncheck = -1;

              //计算是否有利于投资 1
              goalnumcheck = 0;
              goalsnum = 0;

              pnncheck = "";

              fitgrnncomp = -1;

              //sb = null;

              //double preresult=0;

              //string result = "";

              //加入live_table数据
              strNode = ltl.Live_table_lib_id + "," + ltl.Match_type + "," + ltl.Match_time + "::" + ltl.Home_team + "::" + ltl.Away_team + "::" + ltl.Status;

              //????
              sb = new StringBuilder(strNode);
              //sb.Append(strNode);

              //var sg = lsAll.Where(e => Int32.Parse(e.Home_team_big) == ltl.Home_team_big).FirstOrDefault();
              var sg = lsAll[ltl.Home_team_big.ToString() + "-" + ltl.Away_team_big.ToString()].OrderByDescending(e => e.Live_Single_id).FirstOrDefault();

              if (sg != null)
              {
                  strNode = "{" + sg.Status + "}{" + sg.Html_position + "}";
                  //????
                  sb.Append(strNode);

                  if (sg.Status.IndexOf(".") == -1)
                  {
                      goalnumcheck = Int32.Parse(sg.Status);
                      bjpk = goalnumcheck;
                  }
                  else
                      goalnumcheck = 0;
              }

              mar = marAll[ltl.Live_table_lib_id].OrderByDescending(o => o.Analysis_result_id).FirstOrDefault();

              if (mar != null)  //有运行过算法
                  if (mar.Fit_win_loss != null)
                  {
                      //加入match_analysis数据

                      /*
                      //2011.6.16数据修正  澳门预测显示的问题
                      //2011.8.11 修正()替换
                      macau = mpAll
                          .Where(e => e.Home_team != null && e.Away_team != null)
                          .Where(e => ltl.Home_team.Replace("(", "").Replace(")", "").IndexOf(e.Home_team) != -1)
                          .Where(e => ltl.Away_team.Replace("(", "").Replace(")", "").IndexOf(e.Away_team) != -1)
                          .Select(e => e.Predication).FirstOrDefault();
                       * */

                      smp = mpAll.Where(e => ltl.Home_team.Replace("(", "").Replace(")", "").IndexOf(e.Key) != -1)
                         .Select(e => e.Key).FirstOrDefault();

                      if (smp != null)
                      {
                          mp = mpAll[smp]
                           .Where(e => ltl.Away_team.Replace("(", "").Replace(")", "").IndexOf(e.Away_team) != -1)
                           .OrderByDescending(e => e.MacauPredication_id).FirstOrDefault();

                          if (mp != null)
                              macau = mp.Predication;
                      }

                      strNode = "【" + mar.Pnn_fit + "】【"
                          + mar.Grnn_fit + "】【" + mar.Myfit + "】{" + macau + "}{交战+概率1+拟合+进球+概率30}";

                      //????
                      sb.Append(strNode);


                      //pnngrnncomp = ComparePnnGrnn(mar.Pnn_fit, mar.Grnn_fit);
                      pnncheck = mar.Pnn_fit;

                      grnncheck = (int)GrnnCheck(mar.Grnn_fit);

                      comp = grnncheck.ToString();

                      fitgrnncomp = (int)CompareMyfitGrnn(mar.Myfit, comp);

                      //计算是否有利于投资 2
                      goalnumcheck = goalsnum + goalnumcheck;

                      //修正显示的问题  2011.6.15

                      myfit = mar.Fit_win_loss.ToString();
                      myfit = myfit.Length < 5 ? myfit : myfit.Substring(0, 5);

                      strNode = "||" + mar.Result_fit
                          + "::" + mar.Result_goals
                          + "::" + mar.Result_wdl
                          + "::FitReslut:" + myfit    //2011.6.17
                          + "::hGoals:" + mar.Home_goals
                          + "::aGoals:" + mar.Away_goals
                          + "::wGoals:" + (mar.Home_goals - mar.Away_goals)
                          + "::MyWDL:" + mar.Home_w.ToString()
                          + "::" + mar.Home_d.ToString()
                          + "::" + mar.Home_l.ToString()

                          + "::CrossGoals:" + mar.Cross_goals;

                      //????
                      sb.Append(strNode);


                      //fjz = mar.Pre_algorithm;

                      //fDraw = ForecastDraw(mar.Home_w, mar.Home_d, mar.Home_l);



                      if (mar.Result_tb_lib_id != null)  //有导入了结果
                      {
                          //加入result_tb数据
                          rtl = rtlAll.Where(e => e.Result_tb_lib_id == mar.Result_tb_lib_id).FirstOrDefault();

                          if (rtl == null) continue;

                          strNode = "||" + rtl.Match_time.Value.ToShortDateString() + "::" +
                                              rtl.Full_home_goals.ToString() + "-" + rtl.Full_away_goals.ToString() + "::" +
                                              rtl.Odds + "::" + rtl.Win_loss_big + "::" + rtl.Home_team + "::" + rtl.Away_team;

                          //????
                          sb.Append(strNode);




                          //if (rtl.Full_home_goals > rtl.Full_away_goals) result = "3";
                          //if (rtl.Full_home_goals == rtl.Full_away_goals) result = "1";
                          //if (rtl.Full_home_goals < rtl.Full_away_goals) result = "0";



                      }

                      fit = mar.Fit_win_loss;
                      goals = mar.Home_goals - mar.Away_goals;
                      wdl = mar.Home_w - mar.Home_l;


                  }

              //加入bj单场数据

              //由于单场玩法，去掉赔率这块，2011.6.16

              /*
              foreach (var lo in loAll)
                  if (ltl.Home_team.Contains(lo.MatchOrder1_HomeName) || ltl.Away_team.Contains(lo.MatchOrder1_AwayName))   //有匹配bj单场的数据
                      strNode += "{" + lo.KeyValue + "}" + lo.MatchOrder1_HandicapNumber + "***【赔率+拟合】";
              if (ltl.Home_team.IndexOf("*") != -1) strNode += "++++++{3";
              else strNode += "++++++{0";
               * */

              //strNode += "【交战】【概率+拟合】++++++{" + fjz + "}{";

              //strNode += fDraw;

              //if (fit < 0) strNode += "0}";
              //else strNode += "3}";



              //TreeNode child = new TreeNode(strNode);
              TreeNode child = new TreeNode(sb.ToString());
              tn.Nodes.Add(child);
              //颜色处理
              //if (fit < 0) child.ForeColor = Color.Blue;
              //if (goals < 0) child.BackColor = Color.Orange;
              //if (wdl < 0) child.NodeFont = new Font("Trebuchet MS", 10, FontStyle.Italic);
              //if (strNode.Contains("***")) child.Parent.ForeColor = Color.Red;


              /*
              if (grnncheck >= 0) child.ForeColor = Color.Green;
              if (grnncheck >= 0 && fitgrnncomp > 0)
              {
                  if (goalnumcheck != 0)
                  {
                      if (ltl.Home_team.IndexOf("*") != -1 && goalnumcheck > 0 ||
                          ltl.Away_team.IndexOf("*") != -1 && goalnumcheck < 0)
                      {
                          child.ForeColor = Color.Red;  //上盘概率高，有利于投资
                      }
                      else
                      {
                          child.ForeColor = Color.Lavender;  //下盘概率低，看情况而定
                      }
                  }
                  else
                      child.ForeColor = Color.Blue;
              }
              //if (grnncheck >= 0 && fitgrnncomp >= 10) child.ForeColor = Color.Blue;
              //if (pnngrnncomp == 0 && grnncheck == 0) child.ForeColor = Color.Blue;
              if (pnncheck == "-99") child.ForeColor = Color.Black;

              //结果验证
              if (mar.Result_tb_lib_id != null)
                  //if (mar.Myfit != null)
                  //if (mar.Myfit.IndexOf(result) != -1) 
                  //child.ForeColor = Color.Red;
                  child.BackColor = Color.Gray;

               * */

              TreeNodeBrushColor(child,
                  ltl.Home_team, ltl.Away_team, mar.Result_tb_lib_id,
                  grnncheck, pnncheck, fitgrnncomp, goalnumcheck, bjpk);
          }
      }
      #endregion

    }
}
