// <copyright file="IdentifierNotFoundException.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System;
using IK.Algorithms.Matrices.Constants;

namespace IK.Algorithms.Matrices.Exceptions
{
    /// <summary>
    ///     The exception that has to be thrown when no identifier was found for specified dimension.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public class IdentifierNotFoundException<TIdentifier> : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierNotFoundException{TIdentifier}"/> class.
        /// </summary>
        /// <param name="dimension">The dimension.</param>
        /// <param name="identifier">The identifier.</param>
        /// <param name="message">The message.</param>
        public IdentifierNotFoundException(Dimensions dimension, TIdentifier identifier, string message)
            : base(message)
        {
            this.Dimension = dimension;
            this.Identifier = identifier;
        }

        /// <summary>
        /// Gets the dimension.
        /// </summary>
        /// <value>
        /// The dimension.
        /// </value>
        public Dimensions Dimension { get; private set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public TIdentifier Identifier { get; private set; }
    }
}
