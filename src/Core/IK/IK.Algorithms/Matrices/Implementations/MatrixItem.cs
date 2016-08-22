// <copyright file="MatrixItem.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using IK.Algorithms.Matrices.Interfaces;

namespace IK.Algorithms.Matrices.Implementations
{
    public class MatrixItem<TIdentifier, TValue> : IMatrixItem<TIdentifier, TValue>
    {
        public MatrixItem(TIdentifier row, TIdentifier column, TValue value)
        {
            this.Row = row;
            this.Column = column;
            this.Value = value;
        }

        public TIdentifier Row { get; private set; }
        public TIdentifier Column { get; private set; }
        public TValue Value { get; private set; }
    }
}
