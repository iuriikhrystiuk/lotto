﻿// <copyright file="LotteryProcessResultRecordMapping.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data.Entity.ModelConfiguration;
using Lotto.Model.Constants;
using Lotto.Model.Records.Hub;

namespace Lotto.Model.Mappings.Hub
{
    internal class LotteryProcessResultRecordMapping : EntityTypeConfiguration<LotteryProcessResultRecord>
    {
        public LotteryProcessResultRecordMapping()
        {
            this.ToTable("LotteryProcessResults", SchemaNames.Hub);

            this.HasKey(l => l.Id);
            this.Property(l => l.UniqueIdentifier).IsRequired();
            this.HasRequired(l => l.PrimaryLotteryPrize).WithMany().HasForeignKey(l => l.PrimaryLotteryPrizeId);
        }
    }
}
