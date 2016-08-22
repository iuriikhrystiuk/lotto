// <copyright file="LotterySourceColumnRecordMapping.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Lotto.Model.Constants;
using Lotto.Model.Records.Hub;

namespace Lotto.Model.Mappings.Hub
{
    public class LotterySourceColumnRecordMapping : EntityTypeConfiguration<LotterySourceColumnRecord>
    {
        public LotterySourceColumnRecordMapping()
        {
            this.ToTable("LotterySourceColumns", SchemaNames.Hub);

            this.HasKey(l => l.Id);

            this.Property(l => l.LotterySourceConfigId).IsRequired();
            this.Property(l => l.LotterySourceColumnConfigId).IsRequired();
            this.Property(l => l.Order).IsRequired();

            this.HasRequired(l => l.LotterySourceConfig).WithMany(l => l.LotterySourceColumns).HasForeignKey(l => l.LotterySourceConfigId);
            this.HasRequired(l => l.LotterySourceColumnConfig).WithMany(l => l.LotterySourceColumns).HasForeignKey(l => l.LotterySourceColumnConfigId);
        }
    }
}
