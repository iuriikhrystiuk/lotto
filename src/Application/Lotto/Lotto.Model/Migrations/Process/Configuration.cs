// <copyright file="Configuration.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data.Entity.Migrations;
using Lotto.Model.Implementation;
using Lotto.Model.Implementation.Process;

namespace Lotto.Model.Migrations.Process
{
    internal sealed class Configuration : DbMigrationsConfiguration<ProcessContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.MigrationsDirectory = @"Migrations\Process";
        }
    }
}
