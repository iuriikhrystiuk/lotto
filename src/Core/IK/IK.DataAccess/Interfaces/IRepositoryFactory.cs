// <copyright file="IRepositoryFactory.cs">
// This is a property of a Iurii Khrystiuk. All of the code comes as is
// and no license required.
// </copyright>

namespace IK.DataAccess.Interfaces
{
    /// <summary>
    ///     The factory interface for creating repositories.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        ///     Creates the repository.
        /// </summary>
        /// <typeparam name="T">The type of record for repository to operate.</typeparam>
        /// <param name="context">The data base context for repository to use.</param>
        /// <returns>The instance of <see cref="IRepository{T}" />.</returns>
        IRepository<T> CreateRepository<T>(IDbContext context) where T : class;
    }
}