// <copyright file="UnitOfWork.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IK.DataAccess.Interfaces;

namespace IK.DataAccess.Implementation
{
    /// <summary>
    ///     The unit of work to operate on multiple entities.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        ///     The data base context.
        /// </summary>
        private readonly IDbContext context;

        /// <summary>
        ///     The repository provider.
        /// </summary>
        private readonly List<IRepositoryFactory> repoProviders;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="context">The data base context.</param>
        /// <param name="repoProviders">The repository provider.</param>
        public UnitOfWork(IDbContext context, params IRepositoryFactory[] repoProviders)
        {
            this.context = context;
            this.repoProviders = repoProviders == null ? new List<IRepositoryFactory>() : repoProviders.ToList();
        }

        /// <summary>
        ///     Disposes all of the resources used by the instance.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Gets the repository from the repository provider.
        /// </summary>
        /// <typeparam name="TRepository">The type of the repository.</typeparam>
        /// <typeparam name="TRecord">The type of the record.</typeparam>
        /// <returns>
        /// The repository to work with.
        /// </returns>
        public TRepository GetRepository<TRepository, TRecord>() where TRecord : class where TRepository : class, IRepository<TRecord>
        {
            foreach (var repositoryFactory in this.repoProviders)
            {
                var repository = repositoryFactory.CreateRepository<TRecord>(this.context);
                if (repository is TRepository)
                {
                    return repository as TRepository;
                }
            }

            return null;
        }

        /// <summary>
        ///     Saves all of the changes made to data base context.
        /// </summary>
        /// <returns>
        ///     The number of state entries written to the underlying database.
        /// </returns>
        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        /// <summary>
        ///     Saves the changes done using current unit of work asynchronously.
        /// </summary>
        /// <returns>
        ///     The task to track the progress of the save operation.
        /// </returns>
        public async Task<int> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync();
        }

        /// <summary>
        ///     Disposes managed and unmanaged resources based on the flag.
        /// </summary>
        /// <param name="disposing">The flag to dispose managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.context.Dispose();
            }
        }
    }
}