// <copyright file="ListComparer.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;

namespace Lotto.Logic.Algorithms.Implementation.Rating
{
    internal class ListComparer : IComparer<IList<int>>
    {
        public int Compare(IList<int> x, IList<int> y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            return x.Intersect(y).Count() == x.Count && x.Count == y.Count && x.Count != 0 ? 0 : 1;
        }
    }
}
