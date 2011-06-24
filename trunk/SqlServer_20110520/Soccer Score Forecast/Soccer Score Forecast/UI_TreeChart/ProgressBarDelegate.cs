using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;
using SoccerScore.Compact.Linq;
using System.Data.Linq;
using System.Reflection;
using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;

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
        public static DataTable ToDataTable<T>(this IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>))) colType = colType.GetGenericArguments()[0];
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps) dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null);
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
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
        public static void CompressCompact()
        {
            SqlCeEngine engine = new SqlCeEngine(@"Data Source='" + dbpath + "'");
            //Specifynulldestinationconnectionstringforin-placecompaction
            //
            engine.Shrink();
            //Specifyconnectionstringfornewdatabaseoptions.Thefollowing
            //tokensarevalid:
            //-Password
            //-LCID
            //-Encrypt
            //
            //AllotherSqlCeConnection.ConnectionStringtokensareignored
            //
            //engine.Compact("DataSource=;Password=a@3!7f$dQ;");
        }
        public static bool CreateTable(Type linqTableClass)
        {
            bool suc = true;
            string createtable = linqTableClass.Name;
            //MessageBox.Show(createtable);
            //混淆以后反射名称被改变出现问题
            using (DataClassesMatchDataContext match = new DataClassesMatchDataContext(Conn.conn))
            {
                try
                {
                    var metaTable = match.Mapping.GetTable(linqTableClass);
                    var typeName = "System.Data.Linq.SqlClient.SqlBuilder";
                    var type = typeof(DataContext).Assembly.GetType(typeName);
                    var bf = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod;
                    var sql = type.InvokeMember("GetCreateTableCommand", bf, null, null, new[] { metaTable });
                    #region //这里和sql2008不同
                    var sqlAsString = sql.ToString().Replace("(MAX)", "");
                    string querytable = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='"
                        + createtable + @"' AND TABLE_TYPE='TABLE'";
                    int exists = match.ExecuteQuery<int>(querytable).First();
                    if (exists > 0)
                        match.ExecuteCommand("drop table " + createtable);
                    #endregion
                    match.ExecuteCommand(sqlAsString);
                }
                catch (Exception ex)
                {
                    suc = false;
                    MessageBox.Show(ex.ToString());
                }
            }
            return suc;
        }

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
