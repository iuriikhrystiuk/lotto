// <copyright file="LotterySourceColumnConfigRecord.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;

namespace Lotto.Model.Records.Hub
{
    public class LotterySourceColumnConfigRecord
    {
        public long Id { get; set; }

        public int LotterySourceColumnTypeId { get; set; }

        public string ColumnName { get; set; }

        public string Description { get; set; }

        public bool BelongsToCombination { get; set; }

        public virtual ICollection<LotterySourceColumnRecord> LotterySourceColumns { get; set; }

        public virtual LotterySourceColumnTypeRecord LotterySourceColumnType { get; set; }
    }
}
