// <copyright file="LotteryProcessResult.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using Lotto.Model.Extensions;

namespace Lotto.Model.Entities.Hub
{
    public class LotteryProcessResult
    {
        public long Id { get; set; }

        public int PrimaryLotteryPrizeId { get; set; }

        public string UniqueIdentifier { get; set; }

        public List<int> Numbers
        {
            get { return this.UniqueIdentifier.CalculateCombination(); }
        }

        public int RepeatsCount { get; set; }
    }
}
