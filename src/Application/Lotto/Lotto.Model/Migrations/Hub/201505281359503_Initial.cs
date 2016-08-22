// <copyright file="201505281359503_Initial.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data.Entity.Migrations;

namespace Lotto.Model.Migrations.Hub
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "Hub.Lotteries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            this.CreateTable(
                "Hub.LotteryPrizeMap",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LotteryId = c.Int(nullable: false),
                        Size = c.Int(nullable: false),
                        Prize = c.Double(nullable: false),
                        NextLotteryPrizeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hub.Lotteries", t => t.LotteryId, cascadeDelete: true)
                .ForeignKey("Hub.LotteryPrizeMap", t => t.NextLotteryPrizeId)
                .Index(t => t.LotteryId)
                .Index(t => t.NextLotteryPrizeId);
            
            this.CreateTable(
                "Hub.LotteryProcessResults",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PrimaryLotteryPrizeId = c.Int(nullable: false),
                        UniqueIdentifier = c.String(nullable: false),
                        RepeatsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hub.LotteryPrizeMap", t => t.PrimaryLotteryPrizeId, cascadeDelete: true)
                .Index(t => t.PrimaryLotteryPrizeId);
            
            this.CreateTable(
                "Hub.LotteryProcessSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConnectionString = c.String(nullable: false),
                        PrimaryLotteryPrizeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hub.LotteryPrizeMap", t => t.PrimaryLotteryPrizeId, cascadeDelete: true)
                .Index(t => t.PrimaryLotteryPrizeId);
            
        }
        
        public override void Down()
        {
            this.DropForeignKey("Hub.LotteryProcessSources", "PrimaryLotteryPrizeId", "Hub.LotteryPrizeMap");
            this.DropForeignKey("Hub.LotteryProcessResults", "PrimaryLotteryPrizeId", "Hub.LotteryPrizeMap");
            this.DropForeignKey("Hub.LotteryPrizeMap", "NextLotteryPrizeId", "Hub.LotteryPrizeMap");
            this.DropForeignKey("Hub.LotteryPrizeMap", "LotteryId", "Hub.Lotteries");
            this.DropIndex("Hub.LotteryProcessSources", new[] { "PrimaryLotteryPrizeId" });
            this.DropIndex("Hub.LotteryProcessResults", new[] { "PrimaryLotteryPrizeId" });
            this.DropIndex("Hub.LotteryPrizeMap", new[] { "NextLotteryPrizeId" });
            this.DropIndex("Hub.LotteryPrizeMap", new[] { "LotteryId" });
            this.DropTable("Hub.LotteryProcessSources");
            this.DropTable("Hub.LotteryProcessResults");
            this.DropTable("Hub.LotteryPrizeMap");
            this.DropTable("Hub.Lotteries");
        }
    }
}
