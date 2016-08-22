// <copyright file="LotterySource.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

namespace Lotto.Model.Entities.Hub
{
    public class LotterySource
    {
        public int Id { get; set; }

        public int LotteryId { get; set; }

        public bool IsPrimary { get; set; }

        public string DownloadUrl { get; set; }
    }
}
