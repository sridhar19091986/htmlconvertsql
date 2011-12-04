using System.Windows.Forms;
using HtmlAgilityPack;
using System.Linq;
using SoccerScore.Compact.Linq.Review;

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
