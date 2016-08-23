using System.Collections.Generic;
using Lotto.Model.Entities.Hub;
using Lotto.Model.Entities.Process;

namespace Lotto.Processor.Interfaces
{
    internal interface IDataProvider
    {
        List<LotteryDrawing> Provide(int lotteryId);
    }
}
