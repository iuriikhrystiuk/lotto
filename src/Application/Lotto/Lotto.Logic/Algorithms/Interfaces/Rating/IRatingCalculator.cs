using System.Collections.Generic;
using Lotto.Model.Entities.Process;

namespace Lotto.Logic.Algorithms.Interfaces.Rating
{
    public interface IRatingCalculator
    {
        IEnumerable<CombinationRating> CalculateRating(IList<Combination> combinationsToUse, int minRequiredOccurances, int size);
    }
}
