using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

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
        public static string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database1.accdb";
        public static System.Data.OleDb.OleDbConnection cnn = new System.Data.OleDb.OleDbConnection(connStr);
        //public static System.Data.SQLite.SQLiteConnection cnn = new SQLiteConnection(connStr);
    }
}
