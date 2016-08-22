// <copyright file="LotterySourceColumnConfig.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

namespace Lotto.Model.Entities.Hub
{
    public class LotterySourceColumnConfig
    {
        public long Id { get; set; }

        public int LotterySourceColumnTypeId { get; set; }

        public string ColumnName { get; set; }

        public string Description { get; set; }

        public string DotNetTypeName { get; set; }

        public bool BelongsToCombination { get; set; }

        public int Order { get; set; }

        public string GetColumnName()
        {
            return this.ColumnName + this.Order;
        }
    }
}
