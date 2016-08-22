// <copyright file="LotteryPrizeMapRecord.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;

namespace Lotto.Model.Records.Hub
{
    public class LotteryPrizeMapRecord
    {
        public int Id { get; set; }

        public int LotteryId { get; set; }

        public int Size { get; set; }

        public Double Prize { get; set; }

        public int? NextLotteryPrizeId { get; set; }

        public virtual LotteryRecord Lottery { get; set; }

        public virtual LotteryPrizeMapRecord NextLotteryPrize { get; set; }
    }
}
