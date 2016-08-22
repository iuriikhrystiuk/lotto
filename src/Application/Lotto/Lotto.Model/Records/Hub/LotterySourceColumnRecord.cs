// <copyright file="LotterySourceColumnRecord.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

namespace Lotto.Model.Records.Hub
{
    public class LotterySourceColumnRecord
    {
        public long Id { get; set; }

        public int LotterySourceConfigId { get; set; }

        public long LotterySourceColumnConfigId { get; set; }

        public int Order { get; set; }

        public virtual LotterySourceConfigRecord LotterySourceConfig { get; set; }

        public virtual LotterySourceColumnConfigRecord LotterySourceColumnConfig { get; set; }
    }
}
