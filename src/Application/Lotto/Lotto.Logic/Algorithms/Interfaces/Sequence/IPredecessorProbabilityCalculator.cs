using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotto.Logic.Algorithms.Interfaces.Sequence
{
    public interface IPredecessorProbabilityCalculator
    {
        IDictionary<string, double> CountProbabilities(int number, List<string> predecessors, List<int> source);
    }
}
