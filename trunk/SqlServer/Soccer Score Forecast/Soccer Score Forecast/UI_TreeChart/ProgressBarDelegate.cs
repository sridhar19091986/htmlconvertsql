using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

//    public static class Conn
//    {
//        private static string connStr = "Data Source=SoccerScoreSqlite.db;FailIfMissing=false;";
//        public static System.Data.SQLite.SQLiteConnection cnn = new SQLiteConnection(connStr);
//    }
}
