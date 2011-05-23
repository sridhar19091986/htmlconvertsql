using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;


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
    public static class Conn
    {
        private static string dbpath = Application.StartupPath + @"\SyncSoccerScore.sdf";
        private static string updatepath = Application.StartupPath + @"\UpdateList.xml";
        public static SqlCeConnection conn = new SqlCeConnection(@"Data Source='" + dbpath + "'");

        public static void ImportSdfFile()
        {
            OpenFileDialog dlgOpenfile = new OpenFileDialog();
            string strFileFullName = null;
            dlgOpenfile.Filter = "Sql Compact File|*.sdf";
            dlgOpenfile.Title = "Open";
            dlgOpenfile.ShowDialog();
            dlgOpenfile.RestoreDirectory = true;
            if (!string.IsNullOrEmpty(dlgOpenfile.FileName))
            {
                strFileFullName = dlgOpenfile.FileName;
                if (strFileFullName.IndexOf("SyncSoccerScore.sdf") != -1)
                {
                    if (File.Exists(dbpath)) File.Delete(dbpath);
                    File.Copy(strFileFullName, dbpath);
                    MessageBox.Show("sdf导入成功");
                }
            }
        }
        public static void ImportUpdateFile()
        {
            OpenFileDialog dlgOpenfile = new OpenFileDialog();
            string strFileFullName = null;
            dlgOpenfile.Filter = "Update Xml File|*.xml";
            dlgOpenfile.Title = "Open";
            dlgOpenfile.ShowDialog();
            dlgOpenfile.RestoreDirectory = true;
            if (!string.IsNullOrEmpty(dlgOpenfile.FileName))
            {
                strFileFullName = dlgOpenfile.FileName;
                if (strFileFullName.IndexOf("UpdateList.xml") != -1)
                {
                    if (File.Exists(updatepath)) File.Delete(updatepath);
                    File.Copy(strFileFullName, updatepath);
                    MessageBox.Show("xml导入成功");
                }
            }
        }
    }
}
