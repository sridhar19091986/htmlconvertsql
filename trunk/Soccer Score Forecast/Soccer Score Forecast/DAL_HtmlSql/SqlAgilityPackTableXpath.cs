using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MathWorks.MATLAB.NET.Arrays;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using mshtml;
using System.Net;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Linq.Expressions;
using System.ComponentModel;
using HtmlAgilityPack;
using Soccer_Score_Forecast.LinqSql;
using System.Linq;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;

namespace Soccer_Score_Forecast
{
    class SqlAgilityPackTableXpath
    {
        private string uri_host;
        private string table_id_value;
        public SqlAgilityPackTableXpath(string uri_host)
        {
            this.uri_host = uri_host;
            init_table_id_value();
        }
        public string tbTag
        {
            get
            {
                return "//table[@id='" + table_id_value + "']";
            }
        }
        private void init_table_id_value()
        {
            using (SoccerScoreSqlite match = new SoccerScoreSqlite(Conn.cnn))
            {
                var uri = match.MatchTableXPath.Where(e => e.UriHost == uri_host).FirstOrDefault();
                if (uri.MaXTableIDValue.Length > 1)
                    table_id_value = uri.MaXTableIDValue;
                else
                {
                    if (uri.SecondTableIDValue.Length > 1)
                        table_id_value = uri.SecondTableIDValue;
                    else
                        table_id_value = uri.MaXTableXPath;
                }
            }
        }
    }
}
