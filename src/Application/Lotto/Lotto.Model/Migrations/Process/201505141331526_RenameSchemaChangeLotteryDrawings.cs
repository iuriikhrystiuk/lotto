using System.Data.Entity.Migrations;

namespace Lotto.Model.Migrations.Process
{
    public partial class RenameSchemaChangeLotteryDrawings : DbMigration
    {
        public override void Up()
        {
            this.MoveTable(name: "Keno.Combinations", newSchema: "Lotto");
            this.MoveTable(name: "Keno.LotteryDrawings", newSchema: "Lotto");
            this.MoveTable(name: "Keno.StagingCombinations", newSchema: "Lotto");
            this.AlterColumn("Lotto.LotteryDrawings", "Number7", c => c.Int());
            this.AlterColumn("Lotto.LotteryDrawings", "Number8", c => c.Int());
            this.AlterColumn("Lotto.LotteryDrawings", "Number9", c => c.Int());
            this.AlterColumn("Lotto.LotteryDrawings", "Number10", c => c.Int());
            this.AlterColumn("Lotto.LotteryDrawings", "Number11", c => c.Int());
            this.AlterColumn("Lotto.LotteryDrawings", "Number12", c => c.Int());
            this.AlterColumn("Lotto.LotteryDrawings", "Number13", c => c.Int());
            this.AlterColumn("Lotto.LotteryDrawings", "Number14", c => c.Int());
            this.AlterColumn("Lotto.LotteryDrawings", "Number15", c => c.Int());
            this.AlterColumn("Lotto.LotteryDrawings", "Number16", c => c.Int());
            this.AlterColumn("Lotto.LotteryDrawings", "Number17", c => c.Int());
            this.AlterColumn("Lotto.LotteryDrawings", "Number18", c => c.Int());
            this.AlterColumn("Lotto.LotteryDrawings", "Number19", c => c.Int());
            this.AlterColumn("Lotto.LotteryDrawings", "Number20", c => c.Int());
            this.MoveStoredProcedure("Keno.ImportCombinations", "Lotto");
            this.AlterStoredProcedure("Lotto.ImportCombinations",
                @"MERGE Lotto.Combinations AS target
                  USING Lotto.StagingCombinations AS source
                  ON target.UniqueIdentifier = source.UniqueIdentifier
                  WHEN NOT MATCHED BY TARGET THEN
                  INSERT (UniqueIdentifier, Size, RepeatsCount)
                  VALUES (UniqueIdentifier, Size, 1)
                  WHEN MATCHED THEN
                  UPDATE SET RepeatsCount = target.RepeatsCount + 1;
                  TRUNCATE TABLE Lotto.StagingCombinations
                ");
        }
        
        public override void Down()
        {
            this.AlterColumn("Lotto.LotteryDrawings", "Number20", c => c.Int(nullable: false));
            this.AlterColumn("Lotto.LotteryDrawings", "Number19", c => c.Int(nullable: false));
            this.AlterColumn("Lotto.LotteryDrawings", "Number18", c => c.Int(nullable: false));
            this.AlterColumn("Lotto.LotteryDrawings", "Number17", c => c.Int(nullable: false));
            this.AlterColumn("Lotto.LotteryDrawings", "Number16", c => c.Int(nullable: false));
            this.AlterColumn("Lotto.LotteryDrawings", "Number15", c => c.Int(nullable: false));
            this.AlterColumn("Lotto.LotteryDrawings", "Number14", c => c.Int(nullable: false));
            this.AlterColumn("Lotto.LotteryDrawings", "Number13", c => c.Int(nullable: false));
            this.AlterColumn("Lotto.LotteryDrawings", "Number12", c => c.Int(nullable: false));
            this.AlterColumn("Lotto.LotteryDrawings", "Number11", c => c.Int(nullable: false));
            this.AlterColumn("Lotto.LotteryDrawings", "Number10", c => c.Int(nullable: false));
            this.AlterColumn("Lotto.LotteryDrawings", "Number9", c => c.Int(nullable: false));
            this.AlterColumn("Lotto.LotteryDrawings", "Number8", c => c.Int(nullable: false));
            this.AlterColumn("Lotto.LotteryDrawings", "Number7", c => c.Int(nullable: false));
            this.MoveTable(name: "Lotto.StagingCombinations", newSchema: "Keno");
            this.MoveTable(name: "Lotto.LotteryDrawings", newSchema: "Keno");
            this.MoveTable(name: "Lotto.Combinations", newSchema: "Keno");
            this.MoveStoredProcedure("Lotto.ImportCombinations", "Keno");
            this.AlterStoredProcedure("Keno.ImportCombinations",
                @"MERGE Keno.Combinations AS target
                  USING Keno.StagingCombinations AS source
                  ON target.UniqueIdentifier = source.UniqueIdentifier
                  WHEN NOT MATCHED BY TARGET THEN
                  INSERT (UniqueIdentifier, Size, RepeatsCount)
                  VALUES (UniqueIdentifier, Size, 1)
                  WHEN MATCHED THEN
                  UPDATE SET RepeatsCount = target.RepeatsCount + 1;
                  TRUNCATE TABLE Keno.StagingCombinations
                ");
        }
    }
}
