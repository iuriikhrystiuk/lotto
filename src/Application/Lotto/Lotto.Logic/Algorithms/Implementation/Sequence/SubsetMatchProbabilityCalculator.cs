using System.Linq;
using Lotto.Model.Extensions;

namespace Lotto.Logic.Algorithms.Implementation.Sequence
{
    public class SubsetMatchProbabilityCalculator : BaseProbabilityCalculator
    {
        protected override double CountProbability(string source, string target)
        {
            var sourceList = source.CalculateCombination();
            var targetList = target.CalculateCombination();

            var total = targetList.Count(t => sourceList.Contains(t));

            return (double)total / (targetList.Count > sourceList.Count ? targetList.Count : sourceList.Count);
        }
    }
}
