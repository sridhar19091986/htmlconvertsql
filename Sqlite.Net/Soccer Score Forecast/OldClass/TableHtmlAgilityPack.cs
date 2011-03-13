//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;
//using System.IO;
//using System.Data.Linq;
//using System.Data.Linq.Mapping;
//using System.Reflection;
//using System.Linq.Expressions;
//using System.ComponentModel;
//using HtmlAgilityPack;
//using Soccer_Score_Forecast.LinqSql;

//namespace Soccer_Score_Forecast
//{
//    class TableHtmlAgilityPack
//    {
//        public static DataTable GetHtmlTable(string _html, string _tableTag, int _rowSkip)
//        {
//            DataTable dt = new DataTable();

//            //string readText = File.ReadAllText(_filePath);
//            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
//            doc.LoadHtml(_html);
//            try
//            {
//                #region 提取html网页中指定的table,行的集合,格式是html
//                var query = from html in doc.DocumentNode.SelectNodes(_tableTag).Cast<HtmlNode>()
//                            from table in html.SelectNodes("tbody").Cast<HtmlNode>()
//                            from row in table.SelectNodes("tr").Cast<HtmlNode>()
//                            select row;
//                #endregion
//                #region 生成表的列名和列数

//                var r1 = query.Skip(_rowSkip).FirstOrDefault();
//                int i = 0;
//                dt.Columns.Add(i.ToString());
//                var query2 = from col in r1.SelectNodes("td").Cast<HtmlNode>()
//                             select col;
//                foreach (var c in query2)
//                {
//                    i++;
//                    dt.Columns.Add(i.ToString());
//                }
//                #endregion
//                #region 生成表的行号和行数
//                foreach (var r in query)
//                {
//                    i = 0;
//                    DataRow dataRow = dt.NewRow();
//                    dataRow[i.ToString()] = r.Line;
//                    var query3 = from col in r.SelectNodes("td").Cast<HtmlNode>()
//                                 select col;
//                    foreach (var c in query3)
//                    {
//                        i++;
//                        dataRow[i.ToString()] = c.OuterHtml;
//                    }
//                    dt.Rows.Add(dataRow);
//                }

//                #endregion
//            }
//            catch { }
//            return dt;

//        }

//        public static decimal  InsertLiveHtmlTableToDB(DataTable dt)
//        {
//            DataClassesMatchDataContext match = new DataClassesMatchDataContext();

//            var result = match.live_Table.Where(r => r.live_table_id > 0);
//            match.live_Table.DeleteAllOnSubmit(result);
//            match.SubmitChanges();

//            string ddate = null;
//            Queue<string> todayDate = new Queue<string>();

//            foreach (DataRow aa in dt.Rows)
//            {
//                if (HtmlDateToStrLive(aa[1].ToString()) != null)
//                {
//                    if (HtmlDateToStrLive(aa[1].ToString()).IndexOf("最新賽果") == -1)
//                    {
//                        ddate = HtmlDateToStrLive(aa[1].ToString());
//                        ddate = ddate.Replace("年", "-").Replace("月", "-").Replace("日", " ");
//                        todayDate.Enqueue(ddate);
//                    }
//                    else
//                    {
//                        ddate = todayDate.Dequeue();
//                    }
//                }

//                if (HtmlTextToStr(aa[3].ToString()) != null)
//                {
//                    live_Table lt = new live_Table();
//                    lt.html_position = aa[0].ToString();
//                    lt.match_type = HtmlTextToStr(aa[2].ToString());
//                    lt.s_time = HtmlTextToStr(aa[3].ToString());
//                    lt.home_team = HtmlTextToStr(aa[5].ToString());
//                    lt.full_time_score = HtmlTextToStr(aa[6].ToString());
//                    lt.away_team = HtmlTextToStr(aa[7].ToString());
//                    lt.half_time_score = HtmlTextToStr(aa[8].ToString());
//                    lt.s_date = ddate;
//                    lt.home_team_big = HtmlHrefToStr(aa[5].ToString());
//                    lt.away_team_big = HtmlHrefToStr(aa[7].ToString());

//                    match.live_Table.InsertOnSubmit(lt);
//                    match.SubmitChanges();
//                }
//            }

//            return match.live_Table.Select(e => e.live_table_id).Max();
//        }

//        public static decimal  InsertLastHtmlTableToDB(DataTable dt)
//        {
//            DataClassesMatchDataContext match = new DataClassesMatchDataContext();

//            foreach (DataRow aa in dt.Rows)
//            {
//                if (HtmlTextToStr(aa[3].ToString()) != null)
//                {
//                    result_tb rt = new result_tb();
//                    rt.html_position = aa[0].ToString();
//                    rt.match_type = HtmlTextToStr(aa[1].ToString());
//                    rt.s_time = HtmlTextToStr(aa[2].ToString());
//                    rt.home_team = HtmlTextToStr(aa[3].ToString());
//                    rt.full_time_score = HtmlTextToStr(aa[4].ToString());
//                    rt.away_team = HtmlTextToStr(aa[5].ToString());
//                    rt.half_time_score = HtmlTextToStr(aa[6].ToString());
//                    rt.odds = HtmlTextToStr(aa[7].ToString());
//                    rt.win_loss_big = HtmlTextToStr(aa[8].ToString());
//                    rt.s_date = HtmlDateToStrResult(aa[2].ToString());
//                    rt.home_team_big = HtmlHrefToStr(aa[3].ToString());
//                    rt.away_team_big = HtmlHrefToStr(aa[5].ToString());

//                    match.result_tb.InsertOnSubmit(rt);
//                    match.SubmitChanges();
//                }
//            }

//            return match.result_tb.Select(e => e.result_tb_id ).Max();
//        }

//        public static string HtmlTextToStr(string html)
//        {
//            string text = null;
//            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
//            doc.LoadHtml(html);
//            var a = doc.DocumentNode.SelectSingleNode("td");
//            if (a == null) return text;
//            text = a.InnerText;
//            return text;
//        }
//        public static string HtmlHrefToStr(string html)
//        {
//            string text = null;
//            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
//            doc.LoadHtml(html);
//            var a = doc.DocumentNode.SelectSingleNode("td").SelectSingleNode("a[@href]");
//            if (a == null) return text;
//            text = a.Attributes["href"].Value;
//            text = text.Replace("javascript:", "").Trim();
//            return text;
//        }
//        public static string HtmlDateToStrResult(string html)
//        {
//            string text = null;
//            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
//            doc.LoadHtml(html);
//            var a = doc.DocumentNode.SelectSingleNode("td").SelectSingleNode("div[@title]");
//            if (a == null) return text;
//            text = a.Attributes["title"].Value;
//            return text;
//        }
//        public static string HtmlDateToStrLive(string html)
//        {
//            string text = null;
//            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
//            doc.LoadHtml(html);
//            var a = doc.DocumentNode.SelectSingleNode("td[@class='date']");
//            if (a == null) return text;
//            text = a.InnerText;
//            return text;
//        }
//        public static string GetNumber(String str)
//        {
//            string ss = "";
//            for (int i = 0; i < str.Length; i++)
//            {
//                if (Char.IsNumber(str, i) == true)
//                {
//                    ss += str.Substring(i, 1);
//                }
//                //else
//                //{
//                //    if (str.Substring(i, 1) == ",")
//                //    {
//                //        ss += str.Substring(i, 1);
//                //    }
//                //}
//            }
//            return ss;
//        }
//        public static int StringCount(string string1, string czstring, int count)
//        {
//            string1 = "find" + string1;
//            if (string1.IndexOf(czstring) > 0)
//            {
//                count = count + 1;
//                string string2 = string1.Remove(0, string1.IndexOf(czstring)+czstring.Length );
//                StringCount(string2, czstring, count);
//            }
//            return count;
//        }
//    }
//}
