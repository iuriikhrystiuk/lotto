// <copyright file="LotteryProcessResultRecord.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

namespace Lotto.Model.Records.Hub
{
    public class LotteryProcessResultRecord
    {
        public long Id { get; set; }

        public int PrimaryLotteryPrizeId { get; set; }

        public string UniqueIdentifier { get; set; }

        public int RepeatsCount { get; set; }

        public virtual LotteryPrizeMapRecord PrimaryLotteryPrize { get; set; }
    }
}
