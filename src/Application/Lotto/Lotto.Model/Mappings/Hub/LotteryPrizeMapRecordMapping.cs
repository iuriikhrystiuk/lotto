// <copyright file="LotteryPrizeMapRecordMapping.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data.Entity.ModelConfiguration;
using Lotto.Model.Constants;
using Lotto.Model.Records.Hub;

namespace Lotto.Model.Mappings.Hub
{
    internal class LotteryPrizeMapRecordMapping : EntityTypeConfiguration<LotteryPrizeMapRecord>
    {
        public LotteryPrizeMapRecordMapping()
        {
            this.ToTable("LotteryPrizeMap", SchemaNames.Hub);

            this.HasKey(l => l.Id);
            this.HasOptional(l => l.NextLotteryPrize).WithMany().HasForeignKey(l => l.NextLotteryPrizeId);
            this.HasRequired(l => l.Lottery).WithMany().HasForeignKey(l => l.LotteryId);
        }
    }
}
