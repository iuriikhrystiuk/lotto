// <copyright file="HubContext.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data;
using System.Data.Entity;
using IK.DataAccess.Extensions;
using IK.DataAccess.Interfaces;
using Lotto.Model.Extensions;
using Lotto.Model.Mappings.Hub;
using Lotto.Model.Migrations.Hub;

namespace Lotto.Model.Implementation.Hub
{
    internal class HubContext : DbContext, IDbContext
    {
        public HubContext()
        {
            this.Initialize<HubContext, Configuration>();
            this.Configuration.AutoDetectChangesEnabled = false;
        }

        public override int SaveChanges()
        {
            if (!this.Configuration.AutoDetectChangesEnabled)
            {
                this.ChangeTracker.DetectChanges();
            }
            return base.SaveChanges();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public string TableName<T>()
        {
            return this.GetTableName(typeof(T));
        }

        public DataTable TableDefinition<T>()
        {
            return this.GetTableDefinition(typeof(T));
        }

        public IDbConnection Connection
        {
            get { return this.Database.Connection; }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LotteryRecordMapping());
            modelBuilder.Configurations.Add(new LotteryPrizeMapRecordMapping());
            modelBuilder.Configurations.Add(new LotteryProcessResultRecordMapping());
            modelBuilder.Configurations.Add(new LotteryProcessSourceRecordMapping());
            modelBuilder.Configurations.Add(new LotteryProcessStatusRecordMapping());
            modelBuilder.Configurations.Add(new LotteryProcessStepRecordMapping());
            modelBuilder.Configurations.Add(new LotterySourceRecordMapping());
            modelBuilder.Configurations.Add(new LotterySourceConfigRecordMapping());
            modelBuilder.Configurations.Add(new LotterySourceColumnTypeRecordMapping());
            modelBuilder.Configurations.Add(new LotterySourceColumnConfigRecordMapping());
            modelBuilder.Configurations.Add(new LotterySourceColumnRecordMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
