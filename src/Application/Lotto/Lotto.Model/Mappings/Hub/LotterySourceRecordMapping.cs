// <copyright file="LotterySourceRecordMapping.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Lotto.Model.Constants;
using Lotto.Model.Records.Hub;

namespace Lotto.Model.Mappings.Hub
{
    public class LotterySourceRecordMapping : EntityTypeConfiguration<LotterySourceRecord>
    {
        public LotterySourceRecordMapping()
        {
            this.ToTable("LotterySources", SchemaNames.Hub);

            this.HasKey(l => l.Id);
            this.Property(l => l.LotteryId).IsRequired();
            this.Property(l => l.IsPrimary).IsRequired();

            this.Property(l => l.DownloadUrl).IsRequired().HasMaxLength(1024);

            this.HasRequired(l => l.Lottery).WithMany().HasForeignKey(l => l.LotteryId);
        }
    }
}
