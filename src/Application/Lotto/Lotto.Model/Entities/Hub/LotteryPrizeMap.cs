// <copyright file="LotteryPrizeMapRecord.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;

namespace Lotto.Model.Entities.Hub
{
    public class LotteryPrizeMap
    {
        public int Id { get; set; }

        public int LotteryId { get; set; }

        public int Size { get; set; }

        public Double Prize { get; set; }

        public int? NextLotteryPrizeId { get; set; }

        public LotteryPrizeMap NextLotteryPrize { get; set; }

        public Lottery Lottery { get; set; }

        public LotteryProcessSource ProcessSource { get; set; }
    }
}
