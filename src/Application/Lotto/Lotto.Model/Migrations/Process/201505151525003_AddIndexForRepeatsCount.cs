using System.Data.Entity.Migrations;

namespace Lotto.Model.Migrations.Process
{
    public partial class AddIndexForRepeatsCount : DbMigration
    {
        public override void Up()
        {
            this.CreateIndex("Lotto.Combinations", "RepeatsCount");
        }
        
        public override void Down()
        {
            this.DropIndex("Lotto.Combinations", new[] { "RepeatsCount" });
        }
    }
}
