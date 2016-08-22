// <copyright file="LogicInitializer.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using IK.Infrastructure.Interfaces;
using Lotto.Common.Implementation;
using Lotto.Common.Interfaces;
using Lotto.Logic.Algorithms.Implementation.Rating;
using Lotto.Logic.Algorithms.Implementation.Sequence;
using Lotto.Logic.Algorithms.Interfaces.Rating;
using Lotto.Logic.Algorithms.Interfaces.Sequence;
using Lotto.Logic.Implementation;
using Lotto.Logic.Interfaces;
using Lotto.Model.Entities.Process;
using Ninject;

namespace Lotto.Logic
{
    public class LogicInitializer : IComponentInitializer
    {
        public void Initialize(IKernel kernel)
        {
            kernel.Bind<IConfigurationProvider>().To<ConfigurationProvider>();
            kernel.Bind<IComparer<IList<int>>>().To<ListComparer>();
            kernel.Bind<IRatingCalculator>().To<SimpleRatingCalculator>();
            kernel.Bind<ISequenceCalculator<LotteryDrawing>>().To<LotteryDrawingSequenceCalculator>();
            kernel.Bind<ICollectionGenerator<LotteryDrawing>>().To<LotteryDrawingCollectionGenerator>();
            kernel.Bind<IPredecessorsExtractor>().To<PredecessorsExtractor>();
            kernel.Bind<IPredecessorProbabilityCalculator>().To<SubsetMatchProbabilityCalculator>();
            kernel.Bind<ILotteryManager>().To<LotteryManager>();
            kernel.Bind<ILotteryPrizeMapManager>().To<LotteryPrizeMapManager>();
            kernel.Bind<ILotteryProcessSourceManager>().To<LotteryProcessSourceManager>();
            kernel.Bind<ILotteryProcessResultManager>().To<LotteryProcessResultManager>();
            kernel.Bind<ILotteryProcessStatusManager>().To<LotteryProcessStatusManager>();
            kernel.Bind<ILotteryProcessStepsManager>().To<LotteryProcessStepsManager>();
            kernel.Bind<ILotterySourceManager>().To<LotterySourceManager>();
            kernel.Bind<ILotterySourceConfigManager>().To<LotterySourceConfigManager>();
            kernel.Bind<ILotterySourceColumnConfigManager>().To<LotterySourceColumnConfigManager>();
        }
    }
}
