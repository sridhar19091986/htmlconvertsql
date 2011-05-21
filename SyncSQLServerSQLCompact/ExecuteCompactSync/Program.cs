using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using Microsoft.Synchronization.Data.SqlServerCe;

namespace ExecuteCompactSync
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbpath = @"SyncSoccerScore.sdf";
            SqlCeConnection clientConn = new SqlCeConnection(@"Data Source='" + dbpath + "'");

            // create a connection to the SyncCompactDB database
            //SqlCeConnection clientConn = new SqlCeConnection(@"Data Source='G:\htmlconvertsql\SqlCompact_20110520\Tools\SyncSoccerScore.sdf'");

            // create a connection to the SyncDB server database
            SqlConnection serverConn = new SqlConnection(@"Data Source=localhost; Initial Catalog=G:\htmlconvertsql\match_analysis_pdm.mdf; Integrated Security=True");

            // create the sync orhcestrator
            SyncOrchestrator syncOrchestrator = new SyncOrchestrator();

            // set local provider of orchestrator to a CE sync provider associated with the 
            // ProductsScope in the SyncCompactDB compact client database
            syncOrchestrator.LocalProvider = new SqlCeSyncProvider("match_analysisScope", clientConn);

            // set the remote provider of orchestrator to a server sync provider associated with
            // the ProductsScope in the SyncDB server database
            syncOrchestrator.RemoteProvider = new SqlSyncProvider("match_analysisScope", serverConn);

            // set the direction of sync session to Upload and Download
            syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;

            // subscribe for errors that occur when applying changes to the client
            ((SqlCeSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);

            // execute the synchronization process
            SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();

            // print statistics
            Console.WriteLine("Start Time: " + syncStats.SyncStartTime);
            Console.WriteLine("Total Changes Uploaded: " + syncStats.UploadChangesTotal);
            Console.WriteLine("Total Changes Downloaded: " + syncStats.DownloadChangesTotal);
            Console.WriteLine("Complete Time: " + syncStats.SyncEndTime);
            Console.WriteLine(String.Empty);
        }

        static void Program_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {
            // display conflict type
            Console.WriteLine(e.Conflict.Type);

            // display error message 
            Console.WriteLine(e.Error);
        }
    }
}