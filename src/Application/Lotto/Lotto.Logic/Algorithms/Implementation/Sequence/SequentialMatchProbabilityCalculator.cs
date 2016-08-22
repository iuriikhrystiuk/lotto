using System.Collections.Generic;
using System.Linq;
using Lotto.Logic.Algorithms.Interfaces.Sequence;
using Lotto.Model.Extensions;

namespace Lotto.Logic.Algorithms.Implementation.Sequence
{
    public class SequentialMatchProbabilityCalculator : BaseProbabilityCalculator
    {
        protected override double CountProbability(string source, string target)
        {
            var sourceList = source.CalculateCombination();
            var targetList = target.CalculateCombination();

            sourceList.Reverse();
            targetList.Reverse();
            var targetArray = targetList.ToArray();
            var sourceArray = sourceList.ToArray();
            var count = 0;
            for (int i = 0; i < targetArray.Length && i < sourceArray.Length; i++)
            {
                if (targetArray[i] == sourceArray[i])
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            return (double)count / (targetArray.Length > sourceArray.Length ? targetArray.Length : sourceArray.Length);
        }
    }
}
