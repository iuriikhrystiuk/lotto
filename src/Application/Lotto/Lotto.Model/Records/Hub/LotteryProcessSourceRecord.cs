// <copyright file="LotteryProcessSourceRecord.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

namespace Lotto.Model.Records.Hub
{
    public class LotteryProcessSourceRecord
    {
        public int Id { get; set; }

        public string ConnectionString { get; set; }

        public int PrimaryLotteryPrizeId { get; set; }

        public virtual LotteryPrizeMapRecord PrimaryLotteryPrize { get; set; }
    }
}
