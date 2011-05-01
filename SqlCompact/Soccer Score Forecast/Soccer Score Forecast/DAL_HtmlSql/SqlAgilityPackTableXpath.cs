﻿using System;
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
using SoccerScore.Compact.Linq;
using System.Linq;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;

namespace Soccer_Score_Forecast
{
    class SqlAgilityPackTableXpath
    {
        private string Uri_host;
        private string table_id_value;
        public SqlAgilityPackTableXpath(string Uri_host)
        {
            this.Uri_host = Uri_host;
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
            //using (SoccerScoreCompact match = new SoccerScoreCompact(cnn))
            //{
                var uri = Conn.match.Match_table_xpath.Where(e => e.Uri_host == Uri_host).FirstOrDefault();
                if (uri.Max_table_id_value.Length > 1)
                    table_id_value = uri.Max_table_id_value;
                else
                {
                    if (uri.Second_table_id_value.Length > 1)
                        table_id_value = uri.Second_table_id_value;
                    else
                        table_id_value = uri.Max_table_xpath;
                }
            //}
        }
    }
}