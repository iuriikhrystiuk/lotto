// <copyright file="LotteryProcessStepRecord.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;
using Lotto.Model.Constants;

namespace Lotto.Model.Records.Hub
{
    public class LotteryProcessStepRecord
    {
        public long Id { get; set; }

        public int LotteryProcessStatusId { get; set; }

        public virtual LotteryProcessStatusRecord LotteryProcessStatus { get; set; }

        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }

        public TimeSpan Duration { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }
    }
}
