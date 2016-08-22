// <copyright file="ProcessSource.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

namespace Lotto.Web.Models
{
    public class ProcessSource
    {
        public int Id { get; set; }

        public int LotteryId { get; set; }

        public string LotteryName { get; set; }

        public int PrimaryLotteryPrizeId { get; set; }

        public int Size { get; set; }

        public string ConnectionString { get; set; }
    }
}