// <copyright file="DbContextFactoryNotRegisteredException.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System;

namespace IK.DataAccess.Exceptions
{
    /// <summary>
    ///     The exception thrown when ta factory for specified data base context was not registered.
    /// </summary>
    public class DbContextFactoryNotRegisteredException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DbContextFactoryNotRegisteredException"/> class.
        /// </summary>
        /// <param name="message">the message for exception.</param>
        public DbContextFactoryNotRegisteredException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DbContextFactoryNotRegisteredException"/> class.
        /// </summary>
        /// <param name="format">The format string for the message.</param>
        /// <param name="args">The arguments for the exception.</param>
        public DbContextFactoryNotRegisteredException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}
