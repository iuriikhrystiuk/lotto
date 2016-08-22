using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lotto.Logic.Algorithms.Interfaces.Sequence;
using Lotto.Model.Extensions;

namespace Lotto.Logic.Algorithms.Implementation.Sequence
{
    public class PredecessorsExtractor : IPredecessorsExtractor
    {
        public List<string> Extract(int key, List<int> sequence)
        {
            var sequenceCopy = new List<int>(sequence);
            var predecessors = new List<string>();
            while (sequenceCopy.Contains(key))
            {
                var position = sequenceCopy.IndexOf(key);
                var predecessor = sequenceCopy.Take(position).ToList().CalculateUniqueString(false);
                predecessors.Add(predecessor);
                sequenceCopy = sequenceCopy.Skip(position + 1).ToList();
            }
            return predecessors;
        }
    }
}
