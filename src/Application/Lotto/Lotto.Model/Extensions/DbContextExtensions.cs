// <copyright file="DbContextExtensions.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;

namespace Lotto.Model.Extensions
{
    internal static class DbContextExtensions
    {
        public static void Initialize<TContext, TConfiguration>(this TContext context, string connectionString)
            where TContext : DbContext
            where TConfiguration : DbMigrationsConfiguration<TContext>, new()
        {
            if (context.Database.Exists())
            {
                TConfiguration configuration;
                if (string.IsNullOrEmpty(connectionString))
                {
                    configuration = new TConfiguration();
                }
                else
                {
                    configuration = new TConfiguration
                    {
                        TargetDatabase = new DbConnectionInfo(connectionString, "System.Data.SqlClient")
                    };
                }
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<TContext, TConfiguration>(true, configuration));
            }

            context.Database.Initialize(false);
            context.Database.CommandTimeout = context.Database.Connection.ConnectionTimeout;
        }

        public static void Initialize<TContext, TConfiguration>(this TContext context)
            where TContext : DbContext
            where TConfiguration : DbMigrationsConfiguration<TContext>, new()
        {
            Initialize<TContext, TConfiguration>(context, null);
        }
    }
}
