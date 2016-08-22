using System.Data.Entity.Migrations;

namespace Lotto.Model.Migrations.Process
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "Keno.Combinations",
                c => new
                    {
                        UniqueIdentifier = c.String(nullable: false, maxLength: 350, unicode: false),
                        RepeatsCount = c.Long(nullable: false),
                        Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UniqueIdentifier);
            
            this.CreateTable(
                "Keno.LotteryDrawings",
                c => new
                    {
                        UniqueIdentifier = c.String(nullable: false, maxLength: 700, unicode: false),
                        Status = c.Int(nullable: false),
                        Number1 = c.Int(nullable: false),
                        Number2 = c.Int(nullable: false),
                        Number3 = c.Int(nullable: false),
                        Number4 = c.Int(nullable: false),
                        Number5 = c.Int(nullable: false),
                        Number6 = c.Int(nullable: false),
                        Number7 = c.Int(nullable: false),
                        Number8 = c.Int(nullable: false),
                        Number9 = c.Int(nullable: false),
                        Number10 = c.Int(nullable: false),
                        Number11 = c.Int(nullable: false),
                        Number12 = c.Int(nullable: false),
                        Number13 = c.Int(nullable: false),
                        Number14 = c.Int(nullable: false),
                        Number15 = c.Int(nullable: false),
                        Number16 = c.Int(nullable: false),
                        Number17 = c.Int(nullable: false),
                        Number18 = c.Int(nullable: false),
                        Number19 = c.Int(nullable: false),
                        Number20 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UniqueIdentifier);
            
            this.CreateTable(
                "Keno.StagingCombinations",
                c => new
                    {
                        UniqueIdentifier = c.String(nullable: false, maxLength: 350, unicode: false),
                        Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UniqueIdentifier);

            this.CreateStoredProcedure(
                "Keno.ImportCombinations",
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
        
        public override void Down()
        {
            this.DropTable("Keno.StagingCombinations");
            this.DropTable("Keno.LotteryDrawings");
            this.DropTable("Keno.Combinations");
            this.DropStoredProcedure("Keno.ImportCombinations");
        }
    }
}
