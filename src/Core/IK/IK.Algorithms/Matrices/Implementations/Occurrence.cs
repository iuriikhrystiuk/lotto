// <copyright file="Occurrence.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System.Collections.Generic;
using IK.Algorithms.Matrices.Interfaces;

namespace IK.Algorithms.Matrices.Implementations
{
    public class Occurance<TIdentifier, TValue> : IOccurrence<TIdentifier, TValue>
    {
        public Occurance(TValue value)
        {
            this.Value = value;
            this.MatrixItems = new List<IMatrixItem<TIdentifier, TValue>>();
        }

        public TValue Value { get; private set; }
        public IList<IMatrixItem<TIdentifier, TValue>> MatrixItems { get; private set; }
        public int Repeats { get; set; }
    }
}
