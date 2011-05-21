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
            SqlConnection serverConn = new SqlConnection("Data Source=localhost; Initial Catalog=SyncDB; Integrated Security=True");

            // define a new scope named ProductsScope
            DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription("ProductsScope");

            // get the description of the Products table from SyncDB dtabase
            DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("Products", serverConn);

            // add the table description to the sync scope definition
            scopeDesc.Tables.Add(tableDesc);

            // create a server scope provisioning object based on the ProductScope
            SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);

            // skipping the creation of table since table already exists on server
            serverProvision.SetCreateTableDefault(DbSyncCreationOption.Skip);

            // start the provisioning process
            serverProvision.Apply();
        }
    }
}

