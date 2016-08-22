// <copyright file="LotterySourceColumnTypeRecordMapping.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data.Entity.ModelConfiguration;
using Lotto.Model.Constants;
using Lotto.Model.Records.Hub;

namespace Lotto.Model.Mappings.Hub
{
    public class LotterySourceColumnTypeRecordMapping : EntityTypeConfiguration<LotterySourceColumnTypeRecord>
    {
        public LotterySourceColumnTypeRecordMapping()
        {

            this.ToTable("LotterySourceColumnTypes", SchemaNames.Hub);

            this.HasKey(l => l.Id);

            this.Property(l => l.Name).IsRequired().HasMaxLength(50);
            this.Property(l => l.DotNetName).IsRequired().HasMaxLength(255);
        }
    }
}
