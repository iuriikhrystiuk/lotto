// <copyright file="201506251345159_AddProcessStatusTables.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Data.Entity.Migrations;

namespace Lotto.Model.Migrations.Hub
{
    public partial class AddProcessStatusTables : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "Hub.LotteryProcessStatuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrimaryLotteryPrizeId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hub.LotteryPrizeMap", t => t.PrimaryLotteryPrizeId, cascadeDelete: true)
                .Index(t => t.PrimaryLotteryPrizeId);
            
            this.CreateTable(
                "Hub.LotteryProcessSteps",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LotteryProcessStatusId = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Duration = c.Time(nullable: false, precision: 7),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hub.LotteryProcessStatuses", t => t.LotteryProcessStatusId, cascadeDelete: true)
                .Index(t => t.LotteryProcessStatusId);
            
        }
        
        public override void Down()
        {
            this.DropForeignKey("Hub.LotteryProcessSteps", "LotteryProcessStatusId", "Hub.LotteryProcessStatuses");
            this.DropForeignKey("Hub.LotteryProcessStatuses", "PrimaryLotteryPrizeId", "Hub.LotteryPrizeMap");
            this.DropIndex("Hub.LotteryProcessSteps", new[] { "LotteryProcessStatusId" });
            this.DropIndex("Hub.LotteryProcessStatuses", new[] { "PrimaryLotteryPrizeId" });
            this.DropTable("Hub.LotteryProcessSteps");
            this.DropTable("Hub.LotteryProcessStatuses");
        }
    }
}
