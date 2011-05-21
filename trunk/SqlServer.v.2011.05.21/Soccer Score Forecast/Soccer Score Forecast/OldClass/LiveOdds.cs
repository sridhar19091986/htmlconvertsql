//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Winista.Text.HtmlParser;
//using Winista.Text.HtmlParser.Tags;
//using Winista.Text.HtmlParser.Filters;
//using Winista.Text.HtmlParser.Lex;
//using Winista.Text.HtmlParser.Util;
//using Winista.Text.HtmlParser.Data;

//namespace Soccer_Score_Forecast
//{
//    class LiveOdds
//    {
//        public List<string> liveodds=null ;
//        public LiveOdds(string urlCodeTxt)
//        {
//            liveodds = new List<string>();
//            string row = null;
//            Lexer lexer = new Lexer(urlCodeTxt);
//            Parser parser = new Parser(lexer);
//            String filterStr = "table";
//            NodeFilter filter = new TagNameFilter(filterStr);//过滤这个标签 
//            NodeList nodeList = parser.ExtractAllNodesThatMatch(filter);//抽取所有table列表 
//            INode[] nodes = nodeList.ToNodeArray();
//            for (int i = 0; i < nodes.Length; i++)
//            {
//                INode node = nodeList.ElementAt(i);
//                TableTag tag = (TableTag)node;
//                TableRow[] rows = tag.Rows;
//                for (int j = 0; j < rows.Length; j++)
//                {
//                    TableRow tr = (TableRow)rows[j];
//                    TableColumn[] td = tr.Columns;
//                    row = null;
//                    for (int k = 0; k < td.Length; k++)
//                    {
//                        row += "," + td[k].ToPlainTextString(); //->td[k].ChildrenHTML.ToString()
//                    }
//                    row = parseHtml(row);
//                    if (row.IndexOf(":") == -1) 
//                    {
//                        //if (row.IndexOf("平局") != -1)
//                        //{
//                        //    liveodds.Add(node.StartPosition + "-" + "," + "," + row);
//                        //}
//                        //else
//                        //{
//                        //    liveodds.Add(node.StartPosition + "-" + "," + "," + ","+ row);
//                        //}
//                    }
//                    else{ liveodds.Add(node.StartPosition + "-" + row); }
//                }
//            }
//        }
//        private string parseHtml(string html)
//        {
//            html = html.Replace("&nbsp;", " ");
//            return html.Replace("'", " ");
//        }
//    }
//}
