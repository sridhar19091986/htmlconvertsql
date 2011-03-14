using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Linq.Expressions;
using System.ComponentModel;
using HtmlAgilityPack;
using SoccerScore.Compact.Linq;
using System.Windows.Forms;

namespace Soccer_Score_Forecast
{
    //函数写这么长，显然需要分解，另外，功能要求多，也不利于扩展，稳定性差，需要修改的地方太多
    //关于数据采集入库的问题，显然需要分解到各地方，
    //封装对象是最好的办法，实现 显示  ，入库  ，解析HTML，把各程序分解
    public class HtmlAgilityPackGeneric
    {
        private string tableTag;
        private string html;
        private int rowSkip;
        public HtmlAgilityPackGeneric(string _html, string _tableTag, int _rowSkip)
        {
            tableTag = _tableTag;
            html = _html;
            rowSkip = _rowSkip;
        }
        //此处封装作为临时解决办法
        //一般不使用该方法
        public  DataTable GetHtmlTable()
        {
            DataTable dt = new DataTable();

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            try
            {
                #region 提取html网页中指定的table,行的集合,格式是html
                var query = from _html in doc.DocumentNode.SelectNodes(tableTag).Cast<HtmlNode>()
                            from table in _html.SelectNodes("tbody").Cast<HtmlNode>()
                            from row in table.SelectNodes("tr").Cast<HtmlNode>()
                            select row;
                #endregion
                #region 生成表的列名和列数

                var r1 = query.Skip(rowSkip).FirstOrDefault();
                int i = 0;
                dt.Columns.Add(i.ToString());
                var query2 = from col in r1.SelectNodes("td").Cast<HtmlNode>()
                             select col;
                foreach (var c in query2)
                {
                    i++;
                    dt.Columns.Add(i.ToString());
                }
                #endregion
                #region 生成表的行号和行数
                foreach (var r in query)
                {
                    i = 0;
                    DataRow dataRow = dt.NewRow();
                    dataRow[i.ToString()] = r.Line;
                    var query3 = from col in r.SelectNodes("td").Cast<HtmlNode>()
                                 select col;
                    foreach (var c in query3)
                    {
                        i++;
                        dataRow[i.ToString()] = c.OuterHtml;
                    }
                    dt.Rows.Add(dataRow);
                }

                #endregion
            }
            catch { }
            return dt;

        }
        public DataTable GetTableOutHtml()
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            DataTable dt = new DataTable();
            try
            {
                HtmlNodeCollection tables = doc.DocumentNode.SelectNodes(tableTag);  // Get all tables in the document
                HtmlNodeCollection rows = tables[rowSkip].SelectNodes(".//tr");// Iterate all rows in the first table
                for (int i = 0; i < rows.Count; ++i)
                {
                    DataRow dataRow = dt.NewRow();
                    HtmlNodeCollection cols = rows[i].SelectNodes(".//td");// Iterate all columns in this row
                    for (int j = 0; j < cols.Count; ++j)
                    {
                        if (dt.Columns.Count == j) dt.Columns.Add(j.ToString());
                        dataRow[j.ToString()] = cols[j].OuterHtml;// Get the value of the column and print it
                    }
                    dt.Rows.Add(dataRow);
                }
            }
            catch { }
            return dt;
        }
        public DataTable GetTableInnerHtml()
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            DataTable dt = new DataTable();
            try
            {
                HtmlNodeCollection tables = doc.DocumentNode.SelectNodes(tableTag);  // Get all tables in the document
                HtmlNodeCollection rows = tables[rowSkip].SelectNodes(".//tr");// Iterate all rows in the first table
                for (int i = 0; i < rows.Count; ++i)
                {
                    DataRow dataRow = dt.NewRow();
                    HtmlNodeCollection cols = rows[i].SelectNodes(".//td");// Iterate all columns in this row
                    for (int j = 0; j < cols.Count; ++j)
                    {
                        if (dt.Columns.Count == j) dt.Columns.Add(j.ToString());
                        dataRow[j.ToString()] = cols[j].InnerHtml;// Get the value of the column and print it
                    }
                    dt.Rows.Add(dataRow);
                }
            }
            catch { }
            return dt;
        }
        public DataTable GetTableInnerText()
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            DataTable dt = new DataTable();
            try
            {
                HtmlNodeCollection tables = doc.DocumentNode.SelectNodes(tableTag);  // Get all tables in the document
                HtmlNodeCollection rows = tables[rowSkip].SelectNodes(".//tr");// Iterate all rows in the first table
                for (int i = 0; i < rows.Count; ++i)
                {
                    DataRow dataRow = dt.NewRow();
                    HtmlNodeCollection cols = rows[i].SelectNodes(".//td");// Iterate all columns in this row
                    for (int j = 0; j < cols.Count; ++j)
                    {
                        if (dt.Columns.Count == j) dt.Columns.Add(j.ToString());
                        dataRow[j.ToString()] = cols[j].InnerText;// Get the value of the column and print it
                    }
                    dt.Rows.Add(dataRow);
                }
            }
            catch { }
            return dt;
        }
    }
}
