// <copyright file="IMatrix.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System.Collections.Generic;
using IK.Algorithms.Matrices.Constants;

namespace IK.Algorithms.Matrices.Interfaces
{
    public interface IMatrix<TIdentifier, TValue>
    {
        IEnumerable<TIdentifier> Rows { get; }

        IEnumerable<TIdentifier> Columns { get; }

        void Set(TIdentifier rowIdentifier, TIdentifier columnIdentifier, TValue element);

        void Set(TIdentifier identifier, Dimensions dimension, params TValue[] elements);

        TValue Get(TIdentifier rowIdentifier, TIdentifier columnIdentifier);

        void AddRows(params TIdentifier[] rows);

        void AddColumns(params TIdentifier[] columns);
    }
}
