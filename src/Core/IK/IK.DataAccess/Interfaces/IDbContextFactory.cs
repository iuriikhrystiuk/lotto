// <copyright file="IDbContextFactory.cs">
// This is a property of a Iurii Khrystiuk. All of the code comes as is
// and no license required.
// </copyright>

namespace IK.DataAccess.Interfaces
{
    /// <summary>
    ///     The interface for creating data base context.
    /// </summary>
    public interface IDbContextFactory
    {
        /// <summary>
        /// Returns value indicating whether the factory can produce data base context
        /// of the specified type.
        /// </summary>
        /// <param name="contextDescriptor">The context descriptor.</param>
        /// <returns>
        /// [true] if can produce, [false] otherwise.
        /// </returns>
        bool CanCreateContext(IDbContextDescriptor contextDescriptor);

        /// <summary>
        ///     Creates the data base context.
        /// </summary>
        /// <returns>The data base context.</returns>
        IDbContext CreateDbContext();

        /// <summary>
        ///     Creates the data base context.
        /// </summary>
        /// <param name="connectionString">the connection string for data base context.</param>
        /// <returns>The data base context.</returns>
        IDbContext CreateDbContext(string connectionString);
    }
}