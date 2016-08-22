// <copyright file="IOccurrence.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System.Collections.Generic;

namespace IK.Algorithms.Matrices.Interfaces
{
    public interface IOccurrence<TIDentifier, TValue>
    {
        TValue Value { get; }

        IList<IMatrixItem<TIDentifier, TValue>> MatrixItems { get; }

        int Repeats { get; set; }
    }
}
