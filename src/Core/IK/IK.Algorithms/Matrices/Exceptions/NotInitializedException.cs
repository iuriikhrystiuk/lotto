// <copyright file="NotInitializedException.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System;

namespace IK.Algorithms.Matrices.Exceptions
{
    /// <summary>
    ///     The exception that has to be thrown when matrix is not initialized.
    /// </summary>
    public class NotInitializedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotInitializedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NotInitializedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotInitializedException"/> class.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public NotInitializedException(string format, params object[] args)
            : this(string.Format(format, args))
        {
        }
    }
}
