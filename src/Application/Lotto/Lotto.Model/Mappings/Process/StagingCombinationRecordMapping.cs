// <copyright file="StagingCombinationRecordMapping.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data.Entity.ModelConfiguration;
using Lotto.Model.Constants;
using Lotto.Model.Records.Process;

namespace Lotto.Model.Mappings.Process
{
    internal class StagingCombinationRecordMapping : EntityTypeConfiguration<StagingCombinationRecord>
    {
        public StagingCombinationRecordMapping()
        {
            this.ToTable("StagingCombinations", SchemaNames.Lotto);

            this.HasKey(s => new { s.UniqueIdentifier });

            this.Property(c => c.UniqueIdentifier)
                .HasMaxLength(350)
                .IsUnicode(false)
                .IsRequired();

            this.Property(c => c.Size).IsRequired();
        }
    }
}
