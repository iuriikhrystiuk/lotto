using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using IK.Logging.Interfaces;
using Lotto.Processor.Interfaces.ResourceMaintenance;

namespace Lotto.Processor.Implementation.ResourceMaintenance
{
    public class SqlDatabaseWorker : ISqlDatabaseWorker
    {
        private readonly ILogger logger;

        public SqlDatabaseWorker(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task Attach(string serverName, string dataBaseName, IEnumerable<string> files)
        {
            var connectionString = this.getConnectionString(serverName);
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                if (await dataBaseExists(connection, dataBaseName))
                {
                    this.logger.Info("Database '{0}' already exists on '{1}'. Skipping attach process.", dataBaseName, serverName);
                    return;
                }
                var command = connection.CreateCommand();
                command.CommandText = "sys.sp_attach_db";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@dbname", SqlDbType.NVarChar)).Value = dataBaseName;
                int i = 0;
                foreach (var file in files)
                {
                    i++;
                    command.Parameters.Add(new SqlParameter("@filename" + i, SqlDbType.NVarChar)).Value = file;
                }

                await command.ExecuteNonQueryAsync();
                this.logger.Info("Attached database '{0}' on '{1}'.", dataBaseName, serverName);
            }
        }

        public async Task Detach(string serverName, string dataBaseName)
        {
            var connectionString = this.getConnectionString(serverName);
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                if (await dataBaseExists(connection, dataBaseName))
                {
                    await dropConnections(connection, dataBaseName);
                    this.logger.Info("Dropped all connections to '{0}' on '{1}'.", dataBaseName, serverName);
                    var command = connection.CreateCommand();
                    command.CommandText = "sys.sp_detach_db";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@dbname", SqlDbType.NVarChar)).Value = dataBaseName;

                    await command.ExecuteNonQueryAsync();
                    this.logger.Info("Detached database '{0}' from '{1}'.", dataBaseName, serverName);
                }
                else
                {
                    this.logger.Info("Database '{0}' already does not exist on '{1}'. Skipping detach process.", dataBaseName, serverName);
                }
            }
        }

        private string getConnectionString(string serverName)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = serverName,
                IntegratedSecurity = true
            };
            return builder.ConnectionString;
        }

        private static async Task<bool> dataBaseExists(SqlConnection connection, string dataBaseName)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT TOP 1 1 FROM sys.databases WHERE name = @dbName";
            command.Parameters.Add(new SqlParameter("@dbname", SqlDbType.NVarChar)).Value = dataBaseName;
            using (var reader = await command.ExecuteReaderAsync())
            {
                return await reader.ReadAsync();
            }
        }

        private static async Task dropConnections(SqlConnection connection, string dataBaseName)
        {
            var command = connection.CreateCommand();
            command.CommandText = "ALTER DATABASE " + dataBaseName + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
            await command.ExecuteNonQueryAsync();
            command = connection.CreateCommand();
            command.CommandText = "ALTER DATABASE " + dataBaseName + " SET MULTI_USER";
            await command.ExecuteNonQueryAsync();
        }
    }
}
