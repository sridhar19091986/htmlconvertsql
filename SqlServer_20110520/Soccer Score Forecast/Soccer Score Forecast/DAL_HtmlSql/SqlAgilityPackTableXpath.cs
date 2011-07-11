using SoccerScore.Compact.Linq;
using System.Linq;

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
        public string macauTag
        {
            get { return table_id_value; }
        }
        private void init_table_id_value()
        {
            using (DataClassesMatchDataContext match = new DataClassesMatchDataContext(Conn.conn))
            {
                var uri = match.Match_table_xpath.Where(e => e.Uri_host == uri_host).FirstOrDefault();
                if (uri.Max_table_id_value.Length > 1)
                    table_id_value = uri.Max_table_id_value;
                else
                {
                    if (uri.Second_table_id_value.Length > 1)
                        table_id_value = uri.Second_table_id_value;
                    else
                        table_id_value = uri.Max_table_xpath;
                }
            }
        }
    }
}
