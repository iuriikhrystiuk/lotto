// <copyright file="201608231007020_RemoveNumbersFromDrawings.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data.Entity.Migrations;

namespace Lotto.Model.Migrations.Process
{
    public partial class RemoveNumbersFromDrawings : DbMigration
    {
        public override void Up()
        {
            this.DropColumn("Lotto.LotteryDrawings", "Number1");
            this.DropColumn("Lotto.LotteryDrawings", "Number2");
            this.DropColumn("Lotto.LotteryDrawings", "Number3");
            this.DropColumn("Lotto.LotteryDrawings", "Number4");
            this.DropColumn("Lotto.LotteryDrawings", "Number5");
            this.DropColumn("Lotto.LotteryDrawings", "Number6");
            this.DropColumn("Lotto.LotteryDrawings", "Number7");
            this.DropColumn("Lotto.LotteryDrawings", "Number8");
            this.DropColumn("Lotto.LotteryDrawings", "Number9");
            this.DropColumn("Lotto.LotteryDrawings", "Number10");
            this.DropColumn("Lotto.LotteryDrawings", "Number11");
            this.DropColumn("Lotto.LotteryDrawings", "Number12");
            this.DropColumn("Lotto.LotteryDrawings", "Number13");
            this.DropColumn("Lotto.LotteryDrawings", "Number14");
            this.DropColumn("Lotto.LotteryDrawings", "Number15");
            this.DropColumn("Lotto.LotteryDrawings", "Number16");
            this.DropColumn("Lotto.LotteryDrawings", "Number17");
            this.DropColumn("Lotto.LotteryDrawings", "Number18");
            this.DropColumn("Lotto.LotteryDrawings", "Number19");
            this.DropColumn("Lotto.LotteryDrawings", "Number20");
        }

        public override void Down()
        {
            this.AddColumn("Lotto.LotteryDrawings", "Number20", c => c.Int());
            this.AddColumn("Lotto.LotteryDrawings", "Number19", c => c.Int());
            this.AddColumn("Lotto.LotteryDrawings", "Number18", c => c.Int());
            this.AddColumn("Lotto.LotteryDrawings", "Number17", c => c.Int());
            this.AddColumn("Lotto.LotteryDrawings", "Number16", c => c.Int());
            this.AddColumn("Lotto.LotteryDrawings", "Number15", c => c.Int());
            this.AddColumn("Lotto.LotteryDrawings", "Number14", c => c.Int());
            this.AddColumn("Lotto.LotteryDrawings", "Number13", c => c.Int());
            this.AddColumn("Lotto.LotteryDrawings", "Number12", c => c.Int());
            this.AddColumn("Lotto.LotteryDrawings", "Number11", c => c.Int());
            this.AddColumn("Lotto.LotteryDrawings", "Number10", c => c.Int());
            this.AddColumn("Lotto.LotteryDrawings", "Number9", c => c.Int());
            this.AddColumn("Lotto.LotteryDrawings", "Number8", c => c.Int());
            this.AddColumn("Lotto.LotteryDrawings", "Number7", c => c.Int());
            this.AddColumn("Lotto.LotteryDrawings", "Number6", c => c.Int(nullable: false));
            this.AddColumn("Lotto.LotteryDrawings", "Number5", c => c.Int(nullable: false));
            this.AddColumn("Lotto.LotteryDrawings", "Number4", c => c.Int(nullable: false));
            this.AddColumn("Lotto.LotteryDrawings", "Number3", c => c.Int(nullable: false));
            this.AddColumn("Lotto.LotteryDrawings", "Number2", c => c.Int(nullable: false));
            this.AddColumn("Lotto.LotteryDrawings", "Number1", c => c.Int(nullable: false));
        }
    }
}
