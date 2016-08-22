// <copyright file="SqlBulkRepository.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FastMember;
using IK.DataAccess.Interfaces;

namespace IK.DataAccess.Implementation
{
    /// <summary>
    ///     The implementation of bulk operations specific for SQL.
    /// </summary>
    /// <typeparam name="T">The type of entity to run actions for.</typeparam>
    public class SqlBulkRepository<T> : Repository<T>, IBulkRepository<T> where T : class
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlBulkRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The data base context.</param>
        public SqlBulkRepository(IDbContext context) :
            base(context)
        {
            this.context = context;
        }

        /// <summary>
        /// Inserts the bulk of items in the data base.
        /// </summary>
        /// <param name="records">The records to insert.</param>
        public void BulkInsert(IEnumerable<T> records)
        {
            this.HandleConnection(() =>
            {
                DataTable table = this.PopulateTable(records);

                using (SqlBulkCopy copyOperation = new SqlBulkCopy(this.context.Connection.ConnectionString))
                {
                    copyOperation.BulkCopyTimeout = this.context.Connection.ConnectionTimeout;
                    copyOperation.DestinationTableName = this.context.TableName<T>();
                    copyOperation.WriteToServer(table);
                }

                return 0;
            });
        }

        /// <summary>
        /// The asynchronous implementation of the bulk insert method.
        /// </summary>
        /// <param name="records">The records to insert.</param>
        /// <returns>The task to track the insert.</returns>
        public Task BulkInsertAsync(IEnumerable<T> records)
        {
            return this.HandleConnection(() =>
            {
                DataTable table = this.PopulateTable(records);

                using (SqlBulkCopy copyOperation = new SqlBulkCopy(this.context.Connection.ConnectionString))
                {
                    copyOperation.BulkCopyTimeout = this.context.Connection.ConnectionTimeout;
                    copyOperation.DestinationTableName = this.context.TableName<T>();
                    return copyOperation.WriteToServerAsync(table);
                }
            });
        }

        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        /// <param name="name">The name of the stored procedure.</param>
        public void ExecuteStoredProcedure(string name)
        {
            this.HandleConnection(() =>
            {
                using (IDbCommand command = this.context.Connection.CreateCommand())
                {
                    command.CommandText = name;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = this.context.Connection.ConnectionTimeout;
                    return command.ExecuteNonQuery();
                }
            });
        }

        /// <summary>
        /// Executes the stored procedure asynchronously.
        /// </summary>
        /// <param name="name">The name of the stored procedure.</param>
        /// <returns>The task to track the execution.</returns>
        public Task ExecuteStoredProcedureAsync(string name)
        {
            return this.HandleConnection(() =>
                   {
                       SqlCommand command = this.context.Connection.CreateCommand() as SqlCommand;
                       if (command != null)
                       {
                           using (command)
                           {
                               command.CommandText = name;
                               command.CommandType = CommandType.StoredProcedure;
                               command.CommandTimeout = this.context.Connection.ConnectionTimeout;
                               return command.ExecuteNonQueryAsync();
                           }
                       }

                       return Task.Delay(0);
                   });
        }

        /// <summary>
        /// Handles the connection to the data base for specified function.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="func">The function to execute.</param>
        /// <returns>The response of the function.</returns>
        private TResponse HandleConnection<TResponse>(Func<TResponse> func)
        {
            try
            {
                if (this.context.Connection.State != ConnectionState.Open)
                {
                    this.context.Connection.Open();
                }

                return func();
            }
            catch (Exception)
            {
                if (this.context.Connection.State != ConnectionState.Closed)
                {
                    this.context.Connection.Close();
                }

                throw;
            }
        }

        /// <summary>
        /// Populates the table using specified records.
        /// </summary>
        /// <param name="records">The records.</param>
        /// <returns>The data table.</returns>
        private DataTable PopulateTable(IEnumerable<T> records)
        {
            DataTable table = this.context.TableDefinition<T>();
            TypeAccessor accessor = TypeAccessor.Create(typeof(T));

            foreach (T record in records)
            {
                object[] values = table.Columns.OfType<DataColumn>().Select(col => accessor[record, col.ColumnName]).ToArray();
                table.Rows.Add(values);
            }

            return table;
        }
    }
}
