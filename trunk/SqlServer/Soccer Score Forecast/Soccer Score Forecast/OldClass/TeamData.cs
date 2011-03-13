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
//using Winista.Text.HtmlParser.Visitors;

//namespace Soccer_Score_Forecast
//{
//    class TeamData
//    {
//        public List<string> matchdata = null;
//        public TeamData(string urlCodeTxt)
//        {
//            matchdata = new List<string>();
//            string row = null;
//            string cellContent = null;
//            string cellLink = null;
//            Lexer lexer = new Lexer(urlCodeTxt);
//            Parser parser = new Parser(lexer);
//            NodeFilter tableFilter = new TagNameFilter("table");
//            NodeList nodeList = parser.ExtractAllNodesThatMatch(tableFilter);
//            INode[] nodes = nodeList.ToNodeArray();
//            for (int i = 0; i < nodes.Length; i++)
//            {
//                INode node = nodeList.ElementAt(i);
//                if (node.GetText().IndexOf("_tb") != -1 || node.GetText().IndexOf("live_Table") != -1)
//                {
//                    TableTag tag = (TableTag)node;
//                    TableRow[] rows = tag.Rows;
//                    for (int j = 0; j < rows.Length; j++)
//                    {
//                        row = null;
//                        cellContent = null;
//                        cellLink = null;
//                        TableRow tr = (TableRow)rows[j];
//                        TableColumn[] td = tr.Columns;
//                        for (int k = 0; k < td.Length; k++)
//                        {
//                            cellContent = td[k].ToHtml();
//                            Lexer rowLexer = new Lexer(cellContent);
//                            Parser rowParser = new Parser(rowLexer);
//                            NodeFilter linkFilter = new TagNameFilter("A");
//                            NodeList linkNodelist = rowParser.ExtractAllNodesThatMatch(linkFilter);
//                            for (int l = 0; l < linkNodelist.Size(); l++)
//                            {
//                                INode links = linkNodelist.ElementAt(l);
//                                ATag link = (ATag)links;
//                                cellLink += "," + link.GetAttribute("href");
//                            }
//                            row += "," + td[k].ToPlainTextString();
//                        }
//                        if (row != null)
//                        {
//                            row = parseHtml( row + cellLink);
//                            matchdata.Add(row);
//                        }
//                    }
//                }
//            }
//        }
//        private string parseHtml(string html)
//        {
//            html = html.Replace("&nbsp;", " ");
//            html = html.Replace("javascript:", "");
//            return html.Replace("'", " ");
//        }
//    }
//}
    

