using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace Soccer_Score_Forecast
{
    public static class ProgressBarDelegate
    {
        //进度条
        public delegate void SendPMessage(int i);
        public static event SendPMessage sendPEvent;
        public static void DoSendPMessage(int i)
        {
            sendPEvent(i);
        }
    }

    public static class Conn
    {
        private static string connStr = "Data Source=SoccerScoreCompact.sdf;Password=adminadmin123;";
        public static SqlCeConnection cnn =new SqlCeConnection  (connStr);
    }
}
