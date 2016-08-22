// <copyright file="ProcessStatus.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

namespace Lotto.Web.Models
{
    public class ProcessStatus
    {
        public int Id { get; set; }

        public int LotteryId { get; set; }

        public string LotteryName { get; set; }

        public int PrimaryPrizeId { get; set; }

        public int Size { get; set; }

        public string Status { get; set; }

        public int TotalSteps { get; set; }

        public int CurrentStep { get; set; }

        public int AverageDuration { get; set; }

        public int MaxDuration { get; set; }

        public int EstimatedTime { get; set; }
    }
}