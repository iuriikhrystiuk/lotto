using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Algorithms.Matrices.Interfaces
{
    public interface IMatrixItem<TIdentifier, TValue>
    {
        TIdentifier Row { get; }

        TIdentifier Column { get; }

        TValue Value { get; }
    }
}
