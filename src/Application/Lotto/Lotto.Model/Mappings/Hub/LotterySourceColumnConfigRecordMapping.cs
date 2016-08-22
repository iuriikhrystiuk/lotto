// <copyright file="LotterySourceColumnConfigRecordMapping.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using Lotto.Model.Constants;
using Lotto.Model.Records.Hub;

namespace Lotto.Model.Mappings.Hub
{
    public class LotterySourceColumnConfigRecordMapping : EntityTypeConfiguration<LotterySourceColumnConfigRecord>
    {
        public LotterySourceColumnConfigRecordMapping()
        {
            this.ToTable("LotterySourceColumnConfigs", SchemaNames.Hub);

            this.HasKey(l => l.Id);
            this.Property(l => l.LotterySourceColumnTypeId).IsRequired();

            this.Property(l => l.ColumnName).IsRequired().HasMaxLength(100);
            this.Property(l => l.Description).HasMaxLength(255);
            this.Property(l => l.BelongsToCombination).IsRequired();

            this.HasRequired(l => l.LotterySourceColumnType).WithMany().HasForeignKey(l => l.LotterySourceColumnTypeId);
        }
    }
}
