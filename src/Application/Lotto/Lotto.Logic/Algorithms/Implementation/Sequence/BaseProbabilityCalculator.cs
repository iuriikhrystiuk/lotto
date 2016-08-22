using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lotto.Logic.Algorithms.Interfaces.Sequence;
using Lotto.Model.Extensions;

namespace Lotto.Logic.Algorithms.Implementation.Sequence
{
    public abstract class BaseProbabilityCalculator : IPredecessorProbabilityCalculator
    {
        public IDictionary<string, double> CountProbabilities(int number, List<string> predecessors, List<int> source)
        {
            var lastIndex = source.LastIndexOf(number);
            var currentPredecessor = source.Skip(lastIndex + 1).ToList().CalculateUniqueString(false);
            if (currentPredecessor.Length == 0)
            {
                return new Dictionary<string, double>();
            }

            var predecessorsWithRatings = new Dictionary<string, double>();
            foreach (var predecessor in predecessors)
            {
                if (predecessorsWithRatings.ContainsKey(predecessor))
                {
                    predecessorsWithRatings[predecessor] = predecessorsWithRatings[predecessor] * 2;
                }
                else
                {
                    predecessorsWithRatings.Add(predecessor, this.CountProbability(predecessor, currentPredecessor));
                }
            }

            return predecessorsWithRatings;
        }

        protected abstract double CountProbability(string source, string target);
    }
}
