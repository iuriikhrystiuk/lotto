// <copyright file="SqlBulkRepositoryFactory.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using IK.DataAccess.Interfaces;

namespace IK.DataAccess.Implementation
{
    /// <summary>
    ///     The factory for the <see cref="SqlBulkRepository{T}"/> class.
    /// </summary>
    public class SqlBulkRepositoryFactory : IRepositoryFactory
    {
        /// <summary>
        /// Creates the repository.
        /// </summary>
        /// <typeparam name="T">The type of record for repository to operate.</typeparam>
        /// <param name="context">The data base context for repository to use.</param>
        /// <returns>
        /// The instance of <see cref="IRepository{T}" />.
        /// </returns>
        public IRepository<T> CreateRepository<T>(IDbContext context) where T : class
        {
            return new SqlBulkRepository<T>(context);
        }
    }
}
