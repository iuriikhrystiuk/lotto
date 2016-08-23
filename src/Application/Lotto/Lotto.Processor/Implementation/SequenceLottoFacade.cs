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

        public Task StartProcessing(LotteryProcessStatus status, IPauseToken pauseToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
