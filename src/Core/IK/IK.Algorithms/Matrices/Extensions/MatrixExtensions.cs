// <copyright file="MatrixExtensions.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using IK.Algorithms.Matrices.Implementations;
using IK.Algorithms.Matrices.Interfaces;

namespace IK.Algorithms.Matrices.Extensions
{
    /// <summary>
    ///     The extension methods for the matrix class.
    /// </summary>
    public static class MatrixExtensions
    {
        /// <summary>
        /// Calculates the occurrences of values in the matrix.
        /// </summary>
        /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="matrix">The matrix.</param>
        /// <param name="valueComparer">The value comparer.</param>
        /// <returns>The collection of occurrences of values in the matrix.</returns>
        public static IEnumerable<IOccurrence<TIdentifier, TValue>> CalculateOccurrences<TIdentifier, TValue>(this IMatrix<TIdentifier, TValue> matrix, IComparer<TValue> valueComparer)
        {
            List<IOccurrence<TIdentifier, TValue>> occurances = new List<IOccurrence<TIdentifier, TValue>>();
            foreach (TIdentifier row in matrix.Rows)
            {
                foreach (TIdentifier column in matrix.Columns)
                {
                    TValue currentValue = matrix.Get(row, column);
                    if (currentValue != null)
                    {
                        IOccurrence<TIdentifier, TValue> occurance = occurances.FirstOrDefault(d => valueComparer.Compare(d.Value, currentValue) == 0);
                        if (occurance != null)
                        {
                            occurance.Repeats++;
                            occurance.MatrixItems.Add(new MatrixItem<TIdentifier, TValue>(row, column, currentValue));
                        }
                        else
                        {
                            Occurance<TIdentifier, TValue> newOccurance = new Occurance<TIdentifier, TValue>(currentValue) { Repeats = 1 };
                            newOccurance.MatrixItems.Add(new MatrixItem<TIdentifier, TValue>(row, column, currentValue));
                            occurances.Add(newOccurance);
                        }
                    }
                }
            }

            return occurances;
        }
    }
}
