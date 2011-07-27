using System.Linq;
using System.Data;
using SoccerScore.Compact.Linq;
using System;

namespace Soccer_Score_Forecast
{
    public class MacauslotToSql : ElementParserFunction
    {
        private HtmlAgilityPackGeneric MacauslotHtml;
        public MacauslotToSql(string _html)
        {
            SqlAgilityPackTableXpath tbTag = new SqlAgilityPackTableXpath("web.macauslot.com");
            MacauslotHtml = new HtmlAgilityPackGeneric(_html, tbTag.macauTag, 0);
        }
        public decimal updateMacauslot()
        {
            DataTable dt = MacauslotHtml.GetTableInnerText();
            DateTime dtt = DateTime.Now.Date;
            using (DataClassesMatchDataContext match = new DataClassesMatchDataContext(Conn.conn))
            {
                foreach (DataRow aa in dt.Rows)
                {
                    if (aa[0].ToString().IndexOf("★") != -1)
                        if (aa[4].ToString() != null)
                            if (aa[4].ToString().Length > 1)
                            {
                                MacauPredication mp = new MacauPredication();
                                mp.Record_datetime = dtt;
                                mp.Home_team = extract(aa[1].ToString());
                                mp.Away_team = extract(aa[5].ToString());
                                mp.Macauslot = aa[0].ToString().Trim();
                                mp.Predication = aa[4].ToString();
                                match.MacauPredication.InsertOnSubmit(mp);
                            }
                }
                match.SubmitChanges();
                return match.MacauPredication.Max(e => e.MacauPredication_id);
            }
        }
        private string extract( string team)
        {
            string extractstr = team;
            extractstr = extractstr.Replace("(主)", "").Replace("(中)", "").Trim();
            extractstr = extractstr.Replace("(", "").Replace(")", "").Trim();
            return extractstr;
        }
    }
}
