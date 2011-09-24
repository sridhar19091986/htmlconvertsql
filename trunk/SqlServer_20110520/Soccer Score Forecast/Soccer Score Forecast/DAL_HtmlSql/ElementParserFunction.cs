using System;

namespace Soccer_Score_Forecast
{
    public class ElementParserFunction
    {
        public string HtmlTextToStr(string html)
        {
            string text = null;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            var a = doc.DocumentNode.SelectSingleNode("td");
            if (a == null) return text;
            text = a.InnerText;
            return text;
        }
        public string TeamName(string html)
        {
            string text = HtmlTextToStr(html).Trim();
            int len = text.Length / 2;
            return text.Substring(len);
        }
        public string AiboTeamName(string html)
        {
            string text = HtmlTextToStr(html).Trim();
            int start = text.IndexOf("[");
            int end = text.IndexOf("]");
            string repl = null;
            if (start != -1 && end != -1)
            {
                repl = text.Substring(start, end - start + 1);
                text = text.Replace(repl, "").Trim();
            }
            return text.Replace ("(中)","").Trim ();
        }
        public int HtmlValueToInt(string html)
        {
            string text = null;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            var inputs = doc.DocumentNode.Descendants("input");
            foreach (var input in inputs)
                text = input.Attributes["value"].Value;
            return Int32.Parse(text);
        }
        public string HtmlHrefToStr(string html)
        {
            string text = null;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            var a = doc.DocumentNode.SelectSingleNode("td").SelectSingleNode("a[@href]");
            if (a == null) return text;
            text = a.Attributes["href"].Value;
            text = text.Replace("javascript:", "").Trim();
            return text;
        }
        public string HtmlDateToStrResult(string html)
        {
            string text = null;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            var a = doc.DocumentNode.SelectSingleNode("td").SelectSingleNode("div[@title]");
            if (a == null) return text;
            text = a.Attributes["title"].Value;
            return text;
        }
        public string HtmlDateToStrLive(string html)
        {
            string text = null;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            var a = doc.DocumentNode.SelectSingleNode("td[@class='date']");
            if (a == null) return text;
            text = a.InnerText;
            return text;
        }
        public string GetNumber(String str)
        {
            string ss = "";
            if (str == null) return ss;
            for (int i = 0; i < str.Length; i++)
            {
                if (Char.IsNumber(str, i) == true)
                {
                    ss += str.Substring(i, 1);
                }
            }
            return ss;
        }
        public int StringCount(string string1, string czstring, int count)
        {
            string1 = "find" + string1;
            if (string1.IndexOf(czstring) > 0)
            {
                count = count + 1;
                string string2 = string1.Remove(0, string1.IndexOf(czstring) + czstring.Length);
                StringCount(string2, czstring, count);
            }
            return count;
        }
    }
}
