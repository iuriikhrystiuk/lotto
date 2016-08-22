// <copyright file="LotteryProcessStatusRecordMapping.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data.Entity.ModelConfiguration;
using Lotto.Model.Constants;
using Lotto.Model.Records.Hub;

namespace Lotto.Model.Mappings.Hub
{
    internal class LotteryProcessStatusRecordMapping : EntityTypeConfiguration<LotteryProcessStatusRecord>
    {
        public LotteryProcessStatusRecordMapping()
        {
            this.ToTable("LotteryProcessStatuses", SchemaNames.Hub);

            this.HasKey(l => l.Id);
            this.Property(l => l.Status).IsRequired();
            this.Property(l => l.PrimaryLotteryPrizeId).IsRequired();
            this.HasRequired(l => l.PrimaryLotteryPrize).WithMany().HasForeignKey(l => l.PrimaryLotteryPrizeId);
        }
    }
}
