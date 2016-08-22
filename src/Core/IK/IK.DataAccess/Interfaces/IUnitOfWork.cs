// <copyright file="IUnitOfWork.cs">
// This is a property of a Iurii Khrystiuk. All of the code comes as is
// and no license required.
// </copyright>

using System;
using System.Threading.Tasks;

namespace IK.DataAccess.Interfaces
{
    /// <summary>
    ///     The unit of work that gives out repositories and saves changes made by repositories.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the repository from the repository provider.
        /// </summary>
        /// <typeparam name="TRepository">The type of the repository.</typeparam>
        /// <typeparam name="TRecord">The type of the record.</typeparam>
        /// <returns>
        /// The repository to work with.
        /// </returns>
        TRepository GetRepository<TRepository, TRecord>() where TRecord : class where TRepository : class, IRepository<TRecord>;

        /// <summary>
        ///     Saves all of the changes made to data base context.
        /// </summary>
        /// <returns>The number of state entries written to the underlying database.</returns>
        int SaveChanges();

        /// <summary>
        ///     Saves the changes done using current unit of work asynchronously.
        /// </summary>
        /// <returns>The task to track the progress of the save operation.</returns>
        Task<int> SaveChangesAsync();
    }
}