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

            this.Property(l => l.Number1).IsRequired();
            this.Property(l => l.Number2).IsRequired();
            this.Property(l => l.Number3).IsRequired();
            this.Property(l => l.Number4).IsRequired();
            this.Property(l => l.Number5).IsRequired();
            this.Property(l => l.Number6).IsRequired();
            this.Property(l => l.Number7).IsOptional();
            this.Property(l => l.Number8).IsOptional();
            this.Property(l => l.Number9).IsOptional();
            this.Property(l => l.Number10).IsOptional();
            this.Property(l => l.Number11).IsOptional();
            this.Property(l => l.Number12).IsOptional();
            this.Property(l => l.Number13).IsOptional();
            this.Property(l => l.Number14).IsOptional();
            this.Property(l => l.Number15).IsOptional();
            this.Property(l => l.Number16).IsOptional();
            this.Property(l => l.Number17).IsOptional();
            this.Property(l => l.Number18).IsOptional();
            this.Property(l => l.Number19).IsOptional();
            this.Property(l => l.Number20).IsOptional();
        }
    }
}
