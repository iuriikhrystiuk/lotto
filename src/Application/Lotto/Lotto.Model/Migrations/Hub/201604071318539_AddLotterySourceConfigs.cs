namespace Lotto.Model.Migrations.Hub
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddLotterySourceConfigs : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "Hub.LotterySources",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    LotteryId = c.Int(nullable: false),
                    IsPrimary = c.Boolean(nullable: false),
                    DownloadUrl = c.String(nullable: false, maxLength: 1024),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hub.Lotteries", t => t.LotteryId, cascadeDelete: true)
                .Index(t => t.LotteryId);

            this.CreateTable(
                "Hub.LotterySourceConfigs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    LotterySourceId = c.Int(nullable: false),
                    FileNamePattern = c.String(nullable: false, maxLength: 255),
                    FieldDelimiter = c.String(nullable: false, maxLength: 10),
                    HeadersCount = c.Int(nullable: false),
                    FootersCount = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hub.LotterySources", t => t.LotterySourceId, cascadeDelete: true)
                .Index(t => t.LotterySourceId);

            this.CreateTable(
                "Hub.LotterySourceColumns",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    LotterySourceConfigId = c.Int(nullable: false),
                    LotterySourceColumnConfigId = c.Long(nullable: false),
                    Order = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hub.LotterySourceColumnConfigs", t => t.LotterySourceColumnConfigId, cascadeDelete: true)
                .ForeignKey("Hub.LotterySourceConfigs", t => t.LotterySourceConfigId, cascadeDelete: true)
                .Index(t => t.LotterySourceConfigId)
                .Index(t => t.LotterySourceColumnConfigId);

            this.CreateTable(
                "Hub.LotterySourceColumnConfigs",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    LotterySourceColumnTypeId = c.Int(nullable: false),
                    ColumnName = c.String(nullable: false, maxLength: 100),
                    Description = c.String(maxLength: 255),
                    BelongsToCombination = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hub.LotterySourceColumnTypes", t => t.LotterySourceColumnTypeId, cascadeDelete: true)
                .Index(t => t.LotterySourceColumnTypeId);

            this.CreateTable(
                "Hub.LotterySourceColumnTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    DotNetName = c.String(nullable: false, maxLength: 255),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            this.DropForeignKey("Hub.LotterySourceColumns", "LotterySourceConfigId", "Hub.LotterySourceConfigs");
            this.DropForeignKey("Hub.LotterySourceColumns", "LotterySourceColumnConfigId", "Hub.LotterySourceColumnConfigs");
            this.DropForeignKey("Hub.LotterySourceColumnConfigs", "LotterySourceColumnTypeId", "Hub.LotterySourceColumnTypes");
            this.DropForeignKey("Hub.LotterySourceConfigs", "LotterySourceId", "Hub.LotterySources");
            this.DropForeignKey("Hub.LotterySources", "LotteryId", "Hub.Lotteries");
            this.DropIndex("Hub.LotterySourceColumnConfigs", new[] { "LotterySourceColumnTypeId" });
            this.DropIndex("Hub.LotterySourceColumns", new[] { "LotterySourceColumnConfigId" });
            this.DropIndex("Hub.LotterySourceColumns", new[] { "LotterySourceConfigId" });
            this.DropIndex("Hub.LotterySourceConfigs", new[] { "LotterySourceId" });
            this.DropIndex("Hub.LotterySources", new[] { "LotteryId" });
            this.DropTable("Hub.LotterySourceColumnTypes");
            this.DropTable("Hub.LotterySourceColumnConfigs");
            this.DropTable("Hub.LotterySourceColumns");
            this.DropTable("Hub.LotterySourceConfigs");
            this.DropTable("Hub.LotterySources");
        }
    }
}
