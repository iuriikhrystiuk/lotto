using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IK.Logging.Interfaces;
using Lotto.Logic.Algorithms.Interfaces.Sequence;
using Lotto.Logic.Interfaces;
using Lotto.Model.Entities.Hub;
using Lotto.Model.Entities.Process;
using Lotto.Processor.Interfaces;

namespace Lotto.Processor.Implementation
{
    internal class SequenceLottoFacade : ILottoFacade
    {
        private readonly ILogger logger;
        private readonly IDownloader downloader;
        private readonly IDataProvider provider;
        private readonly ILotterySourceManager sourceManager;
        private readonly ILotterySourceConfigManager sourceConfigManager;
        private readonly ILotterySourceColumnConfigManager sourceColumnConfigManager;
        private readonly ISequenceCalculator<LotteryDrawing> sequenceCalculator;

        public SequenceLottoFacade(
            IDownloader downloader,
            IDataProvider provider,
            ISequenceCalculator<LotteryDrawing> sequenceCalculator,
            ILogger logger,
            ILotterySourceManager sourceManager,
            ILotterySourceConfigManager sourceConfigManager,
            ILotterySourceColumnConfigManager sourceColumnConfigManager)
        {
            this.downloader = downloader;
            this.provider = provider;
            this.sequenceCalculator = sequenceCalculator;
            this.logger = logger;
            this.sourceManager = sourceManager;
            this.sourceConfigManager = sourceConfigManager;
            this.sourceColumnConfigManager = sourceColumnConfigManager;
        }

        public void Run(int size, IPauseToken pauseToken, CancellationToken cancellationToken)
        {
            this.logger.Info("Downloading the file with lottery drawings.");
            string file = this.downloader.Download("http://lottery.com.ua/mailbox/out/downloading_results/keno_csv.zip", "keno_results.csv");

            this.logger.Info("Extracting lottery drawings from {0}.", file);
            List<LotteryDrawing> drawings = this.provider.Provide(file, null, null);

            foreach (var sequenceElement in this.sequenceCalculator.GetSequenceProbability(drawings).OrderBy(p => p.Probability))
            {
                this.logger.Info("Element: {0}, Probability: {1}", sequenceElement.Number, sequenceElement.Probability);
            }

            this.logger.Info("Finished processing drawings.");
        }

        public Task RunAsync(int lotteryId, IPauseToken pauseToken, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                this.logger.Info("Getting lottery source.");

                var source = this.sourceManager.GetSourceForLottery(lotteryId);

                this.logger.Info("Getting lottery source config.");

                var sourceConfigs = this.sourceConfigManager.GetConfigsForSource(source.Id);

                List<LotteryDrawing> drawings = new List<LotteryDrawing>();
                foreach (var lotterySourceConfig in sourceConfigs)
                {
                    this.logger.Info("Getting lottery source config columns.");

                    var sourceColumns = this.sourceColumnConfigManager.GetColumnsForConfig(lotterySourceConfig.Id);

                    this.logger.Info("Downloading from {0} file {1}.", source.DownloadUrl, lotterySourceConfig.FileNamePattern);

                    string file = this.downloader.Download(source.DownloadUrl, lotterySourceConfig.FileNamePattern);

                    this.logger.Info("Extracting lottery drawings from {0}.", file);
                    drawings.AddRange(this.provider.Provide(file, lotterySourceConfig, sourceColumns));
                }

                this.logger.Info("Calculating probabilities.");

                foreach (
                    var sequenceElement in
                        this.sequenceCalculator.GetSequenceProbability(drawings).OrderBy(p => p.Probability))
                {
                    this.logger.Info("Element: {0}, Probability: {1}", sequenceElement.Number,
                        sequenceElement.Probability);
                }

                this.logger.Info("Finished processing drawings.");
            });
        }

        public Task StartProcessing(LotteryProcessStatus status, IPauseToken pauseToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
