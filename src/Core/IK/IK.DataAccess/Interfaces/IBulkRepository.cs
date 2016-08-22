// <copyright file="IBulkRepository.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;

namespace IK.DataAccess.Interfaces
{
    /// <summary>
    ///     The interface for bulk operations.
    /// </summary>
    /// <typeparam name="T">The type of entity to run actions for.</typeparam>
    public interface IBulkRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Inserts the bulk of items in the data base.
        /// </summary>
        /// <param name="records">The records to insert.</param>
        void BulkInsert(IEnumerable<T> records);

        /// <summary>
        /// The asynchronous implementation of the bulk insert method.
        /// </summary>
        /// <param name="records">The records to insert.</param>
        /// <returns>The task to track the insert.</returns>
        Task BulkInsertAsync(IEnumerable<T> records);

        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        /// <param name="name">The name of the stored procedure.</param>
        void ExecuteStoredProcedure(string name);

        /// <summary>
        /// Executes the stored procedure asynchronously.
        /// </summary>
        /// <param name="name">The name of the stored procedure.</param>
        /// <returns>The task to track the execution.</returns>
        Task ExecuteStoredProcedureAsync(string name);
    }
}
