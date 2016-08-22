// <copyright file="DefaultRepositoryFactory.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using IK.DataAccess.Interfaces;

namespace IK.DataAccess.Implementation
{
    /// <summary>
    ///     The factory for default implementation of repositories.
    /// </summary>
    public class DefaultRepositoryFactory : IRepositoryFactory
    {
        /// <summary>
        ///     Creates default implementation of repositories.
        /// </summary>
        /// <typeparam name="T">The type of record for repository to operate.</typeparam>
        /// <param name="context">The data base context for repository to use.</param>
        /// <returns>The instance of <see cref="Repository{T}" />.</returns>
        public IRepository<T> CreateRepository<T>(IDbContext context) where T : class
        {
            return new Repository<T>(context);
        }
    }
}