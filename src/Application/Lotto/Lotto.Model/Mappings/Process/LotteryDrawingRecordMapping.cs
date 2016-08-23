// <copyright file="LotteryDrawingRecordMapping.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data.Entity.ModelConfiguration;
using Lotto.Model.Constants;
using Lotto.Model.Records.Process;

namespace Lotto.Model.Mappings.Process
{
    internal class LotteryDrawingRecordMapping : EntityTypeConfiguration<LotteryDrawingRecord>
    {
        public LotteryDrawingRecordMapping()
        {
            this.ToTable("LotteryDrawings", SchemaNames.Lotto);

            this.HasKey(l => l.UniqueIdentifier);

            this.Property(c => c.UniqueIdentifier)
                .HasMaxLength(700)
                .IsUnicode(false)
                .IsRequired();

            this.Property(l => l.Status).IsRequired();
        }
    }
}
