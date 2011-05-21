using System;
using System.Data;
using System.Data.SqlClient;

using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace ProvisionServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection serverConn = new SqlConnection(@"Data Source=localhost; Initial Catalog=G:\htmlconvertsql\match_analysis_pdm.mdf; Integrated Security=True");

            // define a new scope named ProductsScope
            DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription("match_analysisScope");

            string[] tableList = new string[]{
            "live_Aibo",
            "live_okoo",
            "live_Table",
            "live_Table_lib",
            "match_analysis_collection",
            "match_analysis_result",
            "match_table_xpath",
            "result_tb",
            "result_tb_lib"};

            DbSyncTableDescription tableDesc;
            for (int i = 0; i < tableList.Length; i++)
            {
                // get the description of the Products table from SyncDB dtabase
                tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable(tableList[i], serverConn);

                // add the table description to the sync scope definition
                scopeDesc.Tables.Add(tableDesc);
            }

            // create a server scope provisioning object based on the ProductScope
            SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);

            // skipping the creation of table since table already exists on server
            serverProvision.SetCreateTableDefault(DbSyncCreationOption.Skip);

            // start the provisioning process
            serverProvision.Apply();
        }
    }
}

