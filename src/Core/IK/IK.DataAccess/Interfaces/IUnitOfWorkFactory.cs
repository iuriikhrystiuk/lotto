// <copyright file="IUnitOfWorkFactory.cs">
// This is a property of a Iurii Khrystiuk. All of the code comes as is
// and no license required.
// </copyright>

namespace IK.DataAccess.Interfaces
{
    /// <summary>
    ///     The unit of work factory.
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Creates unit of work.
        /// </summary>
        /// <param name="contextDescriptor">The context descriptor.</param>
        /// <returns>
        /// The unit of work.
        /// </returns>
        IUnitOfWork CreateUnitOfWork(IDbContextDescriptor contextDescriptor);

        /// <summary>
        /// Creates unit of work.
        /// </summary>
        /// <param name="contextDescriptor">The context descriptor.</param>
        /// <param name="connectionString">The connection string for the unit of work.</param>
        /// <returns>
        /// The unit of work.
        /// </returns>
        IUnitOfWork CreateUnitOfWork(IDbContextDescriptor contextDescriptor, string connectionString);
    }
}