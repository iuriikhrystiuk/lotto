// <copyright file="LotteryProcessSource.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

namespace Lotto.Model.Entities.Hub
{
    public class LotteryProcessSource
    {
        public int Id { get; set; }

        public string ConnectionString { get; set; }

        public int PrimaryLotteryPrizeId { get; set; }

        public LotteryPrizeMap PrimaryLotteryPrize { get; set; }
    }
}
