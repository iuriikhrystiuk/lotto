// <copyright file="LotteryProcessStatusRecord.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using Lotto.Model.Constants;

namespace Lotto.Model.Records.Hub
{
    public class LotteryProcessStatusRecord
    {
        public int Id { get; set; }

        public int PrimaryLotteryPrizeId { get; set; }

        public virtual LotteryPrizeMapRecord PrimaryLotteryPrize { get; set; }

        public Status Status { get; set; }
    }
}
