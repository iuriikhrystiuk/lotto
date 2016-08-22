// <copyright file="LotterySourceConfigRecord.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;

namespace Lotto.Model.Records.Hub
{
    public class LotterySourceConfigRecord
    {
        public int Id { get; set; }

        public int LotterySourceId { get; set; }

        public string FileNamePattern { get; set; }

        public string FieldDelimiter { get; set; }

        public int HeadersCount { get; set; }

        public int FootersCount { get; set; }

        public virtual LotterySourceRecord LotterySource { get; set; }

        public virtual ICollection<LotterySourceColumnRecord> LotterySourceColumns { get; set; }
    }
}
