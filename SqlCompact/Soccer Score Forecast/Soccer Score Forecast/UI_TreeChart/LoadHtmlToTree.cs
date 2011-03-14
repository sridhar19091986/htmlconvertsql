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
using SoccerScore.Compact.Linq;
using System.Linq;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;

namespace Soccer_Score_Forecast
{
    class LoadHtmlToTree
    {
        private HtmlNode rootDomNode;
        public LoadHtmlToTree(string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            rootDomNode = doc.DocumentNode;
        }
        public void ConvertHtmlToTree(TreeView tv)
        {
            TreeNode root = tv.Nodes.Add("HTML");
            InsertDOMNodes(rootDomNode, root);
        }
        //运用递归过程把HTML转成DOC，然后生成treeView
        private void InsertDOMNodes(HtmlNode parentnode, TreeNode tree_node)
        {
            if (parentnode.HasChildNodes)
            {
                HtmlNodeCollection allchild = parentnode.ChildNodes;
                int length = allchild.Count();
                for (int i = 0; i < length; i++)
                {
                    HtmlNode child_node = allchild.ElementAt(i);
                    TreeNode tempnode = tree_node.Nodes.Add(child_node.XPath + "::" + child_node.Name + "::" + child_node.InnerText);
                    InsertDOMNodes(child_node, tempnode);
                }
            }
        }
    }
}
