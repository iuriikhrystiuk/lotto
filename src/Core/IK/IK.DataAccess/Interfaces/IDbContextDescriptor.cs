// <copyright file="IDbContextDescriptor.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System;

namespace IK.DataAccess.Interfaces
{
    /// <summary>
    ///     The descriptor that knows the type and the name of the DB Context.
    /// </summary>
    public interface IDbContextDescriptor
    {
        /// <summary>
        /// Gets the name of the context.
        /// </summary>
        /// <value>
        /// The name of the context.
        /// </value>
        string ContextName { get; }

        /// <summary>
        /// Gets the type of the database context.
        /// </summary>
        /// <value>
        /// The type of the database context.
        /// </value>
        Type DbContextType { get; }
    }
}
