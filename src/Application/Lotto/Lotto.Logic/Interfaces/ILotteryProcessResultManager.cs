// <copyright file="ILotteryProcessResultManager.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using Lotto.Logic.Algorithms.Interfaces;
using Lotto.Logic.Algorithms.Interfaces.Rating;
using Lotto.Model.Entities.Hub;

namespace Lotto.Logic.Interfaces
{
    public interface ILotteryProcessResultManager
    {
        IEnumerable<LotteryProcessResult> Get(int lotteryPrizeId);

        void LoadResults(int lotteryPrizeId, int repeatsCount);

        IEnumerable<CombinationRating> CalculateSimpleWeights(int lotteryPrizeId);
    }
}
