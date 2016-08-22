// <copyright file="CombinationRecordMapping.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Lotto.Model.Constants;
using Lotto.Model.Constants.Process;
using Lotto.Model.Records.Process;

namespace Lotto.Model.Mappings.Process
{
    internal class CombinationRecordMapping : EntityTypeConfiguration<CombinationRecord>
    {
        public CombinationRecordMapping()
        {
            this.ToTable(CombinationRecordAnnotation.TableName, SchemaNames.Lotto);

            this.HasKey(c => c.UniqueIdentifier);

            this.Property(c => c.UniqueIdentifier)
                .HasMaxLength(CombinationRecordAnnotation.UniqueIdentifierLength)
                .IsUnicode(false)
                .IsRequired();

            this.Property(c => c.Size).IsRequired();

            this.Property(c => c.RepeatsCount)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute(CombinationRecordAnnotation.RepeatsCountIndexName)));
        }
    }
}