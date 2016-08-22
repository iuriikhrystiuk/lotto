// <copyright file="IDbContext.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace IK.DataAccess.Interfaces
{
    /// <summary>
    ///     The interface wrapper of a DB context.
    /// </summary>
    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        IDbConnection Connection { get; }

        /// <summary>
        ///     Returns the set of the specified entities.
        /// </summary>
        /// <typeparam name="T">The type of an entity.</typeparam>
        /// <returns>The DB Set of specified entities.</returns>
        IDbSet<T> Set<T>() where T : class;

        /// <summary>
        ///     Gets the data base entry from the context.
        /// </summary>
        /// <typeparam name="T">The type of entity to get entry for.</typeparam>
        /// <param name="item">The item to get entry for.</param>
        /// <returns>The data base entry.</returns>
        DbEntityEntry<T> Entry<T>(T item) where T : class;

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <typeparam name="T">The type of record to get table name for.</typeparam>
        /// <returns>The table name.</returns>
        string TableName<T>();

        /// <summary>
        /// Gets the table definition.
        /// </summary>
        /// <typeparam name="T">The table that presents the type.</typeparam>
        /// <returns>The table definition.</returns>
        DataTable TableDefinition<T>();

        /// <summary>
        ///     Saves changes that were done to entities.
        /// </summary>
        /// <returns>The number of state entries written to the underlying database.</returns>
        int SaveChanges();

        /// <summary>
        ///     Saves the changes asynchronously to the data base.
        /// </summary>
        /// <returns>The task to track the progress.</returns>
        Task<int> SaveChangesAsync();
    }
}