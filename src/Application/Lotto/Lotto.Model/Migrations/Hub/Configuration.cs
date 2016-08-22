// <copyright file="Configuration.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Principal;
using Lotto.Model.Implementation;
using Lotto.Model.Implementation.Hub;
using Lotto.Model.Records.Hub;

namespace Lotto.Model.Migrations.Hub
{
    internal sealed class Configuration : DbMigrationsConfiguration<HubContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.MigrationsDirectory = @"Migrations\Hub";
        }

        protected override void Seed(HubContext context)
        {
            if (!context.Set<LotterySourceColumnTypeRecord>().Any())
            {
                // Common
                var stringColumnType = new LotterySourceColumnTypeRecord { DotNetName = "System.String", Name = "string" };
                var intColumnType = new LotterySourceColumnTypeRecord { DotNetName = "System.Int32", Name = "int" };
                context.Set<LotterySourceColumnTypeRecord>().Add(stringColumnType);
                context.Set<LotterySourceColumnTypeRecord>().Add(intColumnType);

                var drawingNumberColumn = new LotterySourceColumnConfigRecord
                {
                    ColumnName = "DrawingNumber",
                    LotterySourceColumnType = stringColumnType
                };
                var dateColumn = new LotterySourceColumnConfigRecord
                {
                    ColumnName = "Date",
                    LotterySourceColumnType = stringColumnType
                };
                var lototroneColumn = new LotterySourceColumnConfigRecord
                {
                    ColumnName = "Lototrone",
                    LotterySourceColumnType = stringColumnType
                };
                var ballsetColumn = new LotterySourceColumnConfigRecord
                {
                    ColumnName = "BallSet",
                    LotterySourceColumnType = stringColumnType
                };
                var ballColumn = new LotterySourceColumnConfigRecord { ColumnName = "Ball", LotterySourceColumnType = intColumnType, BelongsToCombination = true };
                var winColumn = new LotterySourceColumnConfigRecord { ColumnName = "Win", LotterySourceColumnType = intColumnType };
                var emptyColumn = new LotterySourceColumnConfigRecord
                {
                    ColumnName = "Empty",
                    LotterySourceColumnType = stringColumnType
                };
                context.Set<LotterySourceColumnConfigRecord>().Add(drawingNumberColumn);
                context.Set<LotterySourceColumnConfigRecord>().Add(dateColumn);
                context.Set<LotterySourceColumnConfigRecord>().Add(lototroneColumn);
                context.Set<LotterySourceColumnConfigRecord>().Add(ballsetColumn);
                context.Set<LotterySourceColumnConfigRecord>().Add(ballColumn);
                context.Set<LotterySourceColumnConfigRecord>().Add(winColumn);
                context.Set<LotterySourceColumnConfigRecord>().Add(emptyColumn);

                // lottos
                var keno = new LotteryRecord { Name = "Keno" };
                var super = new LotteryRecord { Name = "Super" };
                var maxima = new LotteryRecord { Name = "Maxima" };
                var triika = new LotteryRecord { Name = "Triika" };
                context.Set<LotteryRecord>().Add(keno);
                context.Set<LotteryRecord>().Add(super);
                context.Set<LotteryRecord>().Add(maxima);
                context.Set<LotteryRecord>().Add(triika);

                var kenoSource = new LotterySourceRecord { DownloadUrl = "http://lottery.com.ua/mailbox/out/downloading_results/keno_csv.zip", Lottery = keno, IsPrimary = true };
                var superSource = new LotterySourceRecord { DownloadUrl = "http://lottery.com.ua/mailbox/out/downloading_results/super_loto_csv.zip", Lottery = super, IsPrimary = true };
                var maximaSource = new LotterySourceRecord { DownloadUrl = "http://lottery.com.ua/mailbox/out/downloading_results/maxima_csv.zip", Lottery = maxima, IsPrimary = true };
                var triikaSource = new LotterySourceRecord { DownloadUrl = "http://lottery.com.ua/mailbox/out/downloading_results/loto_three_csv.zip", Lottery = triika, IsPrimary = true };
                context.Set<LotterySourceRecord>().Add(kenoSource);
                context.Set<LotterySourceRecord>().Add(superSource);
                context.Set<LotterySourceRecord>().Add(maximaSource);
                context.Set<LotterySourceRecord>().Add(triikaSource);

                var kenoSourceConfig = new LotterySourceConfigRecord
                {
                    FieldDelimiter = ";",
                    FileNamePattern = "keno_results.csv",
                    LotterySource = kenoSource,
                    HeadersCount = 1,
                    FootersCount = 0,
                    LotterySourceColumns = new List<LotterySourceColumnRecord>
                    {
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = drawingNumberColumn, Order = 1},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = dateColumn, Order = 2},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = lototroneColumn, Order = 3},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballsetColumn, Order = 4},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 5},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 6},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 7},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 8},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 9},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 10},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 11},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 12},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 13},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 14},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 15},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 16},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 17},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 18},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 19},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 20},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 21},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 22},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 23},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 24},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 25},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 26},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 27},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 28},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 29},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 30},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 31},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 32},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 33},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 34},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 35},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 36},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 37},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 38},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 39},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 40},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 41},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 42},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 43},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 44},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 45},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 46},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 47},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 48},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 49},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 50},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 51},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 52},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 53},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 54},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 55},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 56},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 57},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 58},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 59},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = emptyColumn, Order = 60}
                    }
                };
                var superSourceConfig = new LotterySourceConfigRecord
                {
                    FieldDelimiter = ";",
                    FileNamePattern = "SuperLoto_Results__918-",
                    LotterySource = superSource,
                    HeadersCount = 1,
                    FootersCount = 1,
                    LotterySourceColumns = new List<LotterySourceColumnRecord>
                    {
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = drawingNumberColumn, Order = 1},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = dateColumn, Order = 2},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = lototroneColumn, Order = 3},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballsetColumn, Order = 4},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 5},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 6},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 7},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 8},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 9},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 10},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 11},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 12},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 13},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 14},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 15},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 16},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 17},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 18},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 19},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 20},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = emptyColumn, Order = 21}
                    }
                };
                var superSourceConfigOld = new LotterySourceConfigRecord
                {
                    FieldDelimiter = ";",
                    FileNamePattern = "SuperLoto_Results__797-917.csv",
                    LotterySource = superSource,
                    HeadersCount = 1,
                    FootersCount = 1,
                    LotterySourceColumns = new List<LotterySourceColumnRecord>
                    {
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = drawingNumberColumn, Order = 1},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = dateColumn, Order = 2},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = lototroneColumn, Order = 3},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballsetColumn, Order = 4},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 5},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 6},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 7},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 8},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 9},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 10},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 11},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 12},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 13},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 14},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 15},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 16},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 17},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 18},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = emptyColumn, Order = 19}
                    }
                };
                var maximaSourceConfig = new LotterySourceConfigRecord
                {
                    FieldDelimiter = ";",
                    FileNamePattern = "Maxima_Results__402-",
                    LotterySource = maximaSource,
                    HeadersCount = 1,
                    FootersCount = 1,
                    LotterySourceColumns = new List<LotterySourceColumnRecord>
                    {
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = drawingNumberColumn, Order = 1},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = dateColumn, Order = 2},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = lototroneColumn, Order = 3},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballsetColumn, Order = 4},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 5},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 6},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 7},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 8},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 9},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 10},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 11},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 12},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 13},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 14},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 15},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 16},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 17},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = emptyColumn, Order = 18}
                    }
                };
                var triikaSourceConfig = new LotterySourceConfigRecord
                {
                    FieldDelimiter = ";",
                    FileNamePattern = "loto_three_results",
                    LotterySource = triikaSource,
                    HeadersCount = 1,
                    FootersCount = 0,
                    LotterySourceColumns = new List<LotterySourceColumnRecord>
                    {
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = drawingNumberColumn, Order = 1},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = dateColumn, Order = 2},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = lototroneColumn, Order = 3},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballsetColumn, Order = 4},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 5},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 6},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = ballColumn, Order = 7},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 8},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 9},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 10},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 11},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 12},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = winColumn, Order = 13},
                        new LotterySourceColumnRecord { LotterySourceColumnConfig = emptyColumn, Order = 14}
                    }
                };
                context.Set<LotterySourceConfigRecord>().Add(kenoSourceConfig);
                context.Set<LotterySourceConfigRecord>().Add(superSourceConfigOld);
                context.Set<LotterySourceConfigRecord>().Add(superSourceConfig);
                context.Set<LotterySourceConfigRecord>().Add(maximaSourceConfig);
                context.Set<LotterySourceConfigRecord>().Add(triikaSourceConfig);

                context.SaveChanges();
            }
        }
    }
}
