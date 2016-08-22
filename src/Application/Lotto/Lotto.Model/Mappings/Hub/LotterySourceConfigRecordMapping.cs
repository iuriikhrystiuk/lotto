// <copyright file="LotterySourceConfigRecordMapping.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data.Entity.ModelConfiguration;
using Lotto.Model.Constants;
using Lotto.Model.Records.Hub;

namespace Lotto.Model.Mappings.Hub
{
    public class LotterySourceConfigRecordMapping : EntityTypeConfiguration<LotterySourceConfigRecord>
    {
        public LotterySourceConfigRecordMapping()
        {
            this.ToTable("LotterySourceConfigs", SchemaNames.Hub);

            this.HasKey(l => l.Id);

            this.Property(l => l.FileNamePattern).IsRequired().HasMaxLength(255);
            this.Property(l => l.FieldDelimiter).IsRequired().HasMaxLength(10);
            this.Property(l => l.HeadersCount).IsRequired();
            this.Property(l => l.FootersCount).IsRequired();

            this.HasRequired(l => l.LotterySource).WithMany().HasForeignKey(l => l.LotterySourceId);
        }
    }
}
