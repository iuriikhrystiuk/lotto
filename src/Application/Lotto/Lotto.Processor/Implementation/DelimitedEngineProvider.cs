// <copyright file="DelimitedEngineProvider.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using FileHelpers;
using FileHelpers.Dynamic;
using IK.Logging.Interfaces;
using Lotto.Common.Tools;
using Lotto.Logic.Interfaces;
using Lotto.Model.Entities.Process;
using Lotto.Processor.Interfaces;

namespace Lotto.Processor.Implementation
{
    public class DelimitedEngineProvider : IDataProvider
    {
        private readonly ILogger logger;
        private readonly ILotterySourceManager lotterySourceManager;
        private readonly ILotterySourceConfigManager lotterySourceConfigManager;
        private readonly ILotterySourceColumnConfigManager lotterySourceColumnConfigManager;
        private readonly IDownloader downloader;

        internal DelimitedEngineProvider(
            ILogger logger, 
            ILotterySourceManager lotterySourceManager,
            ILotterySourceConfigManager lotterySourceConfigManager,
            ILotterySourceColumnConfigManager lotterySourceColumnConfigManager, 
            IDownloader downloader)
        {
            this.logger = logger;
            this.lotterySourceManager = lotterySourceManager;
            this.lotterySourceConfigManager = lotterySourceConfigManager;
            this.lotterySourceColumnConfigManager = lotterySourceColumnConfigManager;
            this.downloader = downloader;
        }

        public List<LotteryDrawing> Provide(int lotteryId)
        {
            this.logger.Info("Determining lottery source for lottery {0}.", lotteryId);
            var source = this.lotterySourceManager.GetSourceForLottery(lotteryId);

            this.logger.Info("Determining lottery source configuration for source {0}.", source.Id);
            var sourceConfigs = this.lotterySourceConfigManager.GetConfigsForSource(source.Id);

            this.logger.Info("Downloading the file with lottery drawings.");
            string downloadedFile = this.downloader.Download(source.DownloadUrl);

            string directory = Directory.GetCurrentDirectory();
            List<string> files = ZipTools.Unzip(downloadedFile, directory);

            var result = new List<LotteryDrawing>();

            foreach (var lotterySourceConfig in sourceConfigs)
            {
                string file = files.FirstOrDefault(f => f.Contains(lotterySourceConfig.FileNamePattern));

                this.logger.Info("Determining lottery source columns configuration for source configuration {0}.", lotterySourceConfig.Id);
                var sourceColumsConfigs = this.lotterySourceColumnConfigManager.GetColumnsForConfig(lotterySourceConfig.Id);

                this.logger.Info("Extracting lottery drawings from {0}.", file);
                DelimitedClassBuilder cb = new DelimitedClassBuilder("Combination_" + lotterySourceConfig.Id, lotterySourceConfig.FieldDelimiter)
                {
                    IgnoreEmptyLines = true,
                    IgnoreFirstLines = lotterySourceConfig.HeadersCount,
                    IgnoreLastLines = lotterySourceConfig.FootersCount
                };

                foreach (var column in sourceColumsConfigs)
                {
                    cb.AddField(column.GetColumnName(), Type.GetType(column.DotNetTypeName));
                }

                Type dynamicallyCreatedRecordClass = cb.CreateRecordClass();
                FileHelperEngine engine = new FileHelperEngine(dynamicallyCreatedRecordClass);
                DataTable parsedColumns = engine.ReadFileAsDT(file);


                foreach (DataRow dataRow in parsedColumns.Rows)
                {
                    var drawing = new LotteryDrawing { Combination = new List<int>() };
                    foreach (var column in sourceColumsConfigs.Where(c => c.BelongsToCombination))
                    {
                        drawing.Combination.Add(dataRow.Field<int>(column.GetColumnName()));
                    }
                    result.Add(drawing);
                }

            }

            return result;
        }
    }
}
