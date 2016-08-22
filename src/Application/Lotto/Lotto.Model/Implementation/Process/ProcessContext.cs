// <copyright file="KenoContext.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using IK.DataAccess.Extensions;
using IK.DataAccess.Interfaces;
using Lotto.Model.Extensions;
using Lotto.Model.Mappings.Process;
using Lotto.Model.Migrations.Process;

namespace Lotto.Model.Implementation.Process
{
    internal class ProcessContext : DbContext, IDbContext
    {
        public ProcessContext()
        {
        }

        public ProcessContext(string connectionString)
            : base(connectionString)
        {
            this.Initialize<ProcessContext, Configuration>(connectionString);
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
            modelBuilder.Configurations.Add(new CombinationRecordMapping());
            modelBuilder.Configurations.Add(new LotteryDrawingRecordMapping());
            modelBuilder.Configurations.Add(new StagingCombinationRecordMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}