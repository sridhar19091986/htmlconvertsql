using System.Collections.Generic;
using HtmlAgilityPack;
using System.Linq;

namespace Soccer_Score_Forecast
{
    class HtmlAgilityPackTableXpath
    {
        private string htmlTableXpath;
        private int htmlTableLength;
        public Dictionary<string, int> htmlTableAttDic;
        private HtmlNode rootDomNode;
        public string maxValue;
        public string secondValue;
        public int skip;
        public HtmlAgilityPackTableXpath(string html, int skip)
        {
            htmlTableAttDic = new Dictionary<string, int>();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            this.skip = skip;
            doc.LoadHtml(html);
            rootDomNode = doc.DocumentNode;
            GetHtmlAllTableXpath(rootDomNode);
            maxValue = rootDomNode.SelectSingleNode(maxKey).GetAttributeValue("id", "");
            secondValue = rootDomNode.SelectSingleNode(secondKey).GetAttributeValue("id", "");
        }
        //运用递归过程把HTML转成DOC，得到所有table的Xpath
        private void GetHtmlAllTableXpath(HtmlNode parentnode)
        {
            if (parentnode.HasChildNodes)
            {
                HtmlNodeCollection allchild = parentnode.ChildNodes;
                int length = allchild.Count();
                for (int i = 0; i < length; i++)
                {
                    HtmlNode child_node = allchild.ElementAt(i);
                    if (child_node.Name == "table")
                    {
                        htmlTableXpath = child_node.XPath;
                        htmlTableLength = child_node.InnerText.Length;
                        htmlTableAttDic.Add(htmlTableXpath, htmlTableLength);
                    }
                    GetHtmlAllTableXpath(child_node);
                }
            }
        }
        private string _maxKey;
        public string maxKey
        {
            get
            {
                if (_maxKey == null)
                    _maxKey = htmlTableAttDic.OrderByDescending(e => e.Value).Skip(skip).Select(e => e.Key).FirstOrDefault();
                return _maxKey;
            }
            set { _maxKey = value; }
        }
        private string _secondKey;
        public string secondKey
        {
            get
            {
                if (_secondKey == null)
                    _secondKey = htmlTableAttDic.OrderByDescending(e => e.Value).Skip(skip + 1).Select(e => e.Key).FirstOrDefault();
                return _secondKey;
            }
            set { _secondKey = value; }
        }
    }
}
