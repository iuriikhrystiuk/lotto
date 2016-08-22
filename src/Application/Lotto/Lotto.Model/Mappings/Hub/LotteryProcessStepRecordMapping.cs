// <copyright file="LotteryProcessStepRecordMapping.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data.Entity.ModelConfiguration;
using Lotto.Model.Constants;
using Lotto.Model.Records.Hub;

namespace Lotto.Model.Mappings.Hub
{
    internal class LotteryProcessStepRecordMapping : EntityTypeConfiguration<LotteryProcessStepRecord>
    {
        public LotteryProcessStepRecordMapping()
        {
            this.ToTable("LotteryProcessSteps", SchemaNames.Hub);

            this.HasKey(l => l.Id);
            this.Property(l => l.Status).IsRequired();
            this.Property(l => l.LotteryProcessStatusId).IsRequired();
            this.HasRequired(l => l.LotteryProcessStatus).WithMany().HasForeignKey(l => l.LotteryProcessStatusId);
        }
    }
}
