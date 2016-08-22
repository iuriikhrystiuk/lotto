// <copyright file="LotteryProcessStatus.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using Lotto.Model.Constants;

namespace Lotto.Model.Entities.Hub
{
    public class LotteryProcessStatus
    {
        public int Id { get; set; }

        public int PrimaryLotteryPrizeId { get; set; }

        public LotteryPrizeMap PrimaryLotteryPrize { get; set; }

        public Status Status { get; set; }
    }
}
