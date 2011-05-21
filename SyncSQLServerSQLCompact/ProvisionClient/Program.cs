using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using Microsoft.Synchronization.Data.SqlServerCe;

namespace ProvisionClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbpath = @"SyncSoccerScore.sdf";
            SqlCeConnection clientConn = new SqlCeConnection(@"Data Source='" + dbpath + "'");

            // create a connection to the SyncCompactDB database
            //SqlCeConnection clientConn = new SqlCeConnection(@"Data Source='" + dbpath + "'");

            SqlConnection serverConn = new SqlConnection(@"Data Source=localhost; Initial Catalog=G:\htmlconvertsql\match_analysis_pdm.mdf; Integrated Security=True");

            // get the description of ProductsScope from the SyncDB server database
            DbSyncScopeDescription scopeDesc = SqlSyncDescriptionBuilder.GetDescriptionForScope("match_analysisScope", serverConn);

            // create CE provisioning object based on the ProductsScope
            SqlCeSyncScopeProvisioning clientProvision = new SqlCeSyncScopeProvisioning(clientConn, scopeDesc);

            // starts the provisioning process
            clientProvision.Apply();
        }
    }
}