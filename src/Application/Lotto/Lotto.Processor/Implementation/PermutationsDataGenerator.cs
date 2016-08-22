// <copyright file="PermutationsDataGenerator.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using Lotto.Model.Entities.Process;
using Lotto.Processor.Interfaces;

namespace Lotto.Processor.Implementation
{
    internal class PermutationsDataGenerator : IDataGenerator
    {
        public List<Combination> Generate(List<int> collectionToGenerateFrom, int size)
        {
            List<Combination> combinations = new List<Combination>();
            combinations = this.GetPermutations(collectionToGenerateFrom, size).Select(x => new Combination { Numbers = x.ToList(), Size = x.Count() }).ToList();
            return combinations;
        }

        private IEnumerable<IEnumerable<int>> GetPermutations(IEnumerable<int> items, int count)
        {
            int i = 0;
            foreach (int item in items)
            {
                if (count == 1)
                {
                    yield return new List<int> { item };
                }
                else
                {
                    foreach (IEnumerable<int> result in this.GetPermutations(items.Skip(i + 1), count - 1))
                        yield return new int[] { item }.Concat(result);
                }

                ++i;
            }
        }
    }
}
