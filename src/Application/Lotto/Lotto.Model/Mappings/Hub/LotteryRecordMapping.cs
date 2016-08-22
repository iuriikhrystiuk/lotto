// <copyright file="LotteryRecordMapping.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data.Entity.ModelConfiguration;
using Lotto.Model.Constants;
using Lotto.Model.Records.Hub;

namespace Lotto.Model.Mappings.Hub
{
    internal class LotteryRecordMapping : EntityTypeConfiguration<LotteryRecord>
    {
        public LotteryRecordMapping()
        {
            this.ToTable("Lotteries", SchemaNames.Hub);

            this.HasKey(l => l.Id);
            this.Property(l => l.Name).IsRequired().HasMaxLength(255);
        }
    }
}
