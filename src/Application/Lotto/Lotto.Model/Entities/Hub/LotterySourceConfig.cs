// <copyright file="LotterySourceConfig.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

namespace Lotto.Model.Entities.Hub
{
    public class LotterySourceConfig
    {
        public int Id { get; set; }

        public int LotterySourceId { get; set; }

        public string FileNamePattern { get; set; }

        public string FieldDelimiter { get; set; }

        public int HeadersCount { get; set; }

        public int FootersCount { get; set; }
    }
}
