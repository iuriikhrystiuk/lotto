// <copyright file="ILottoFacade.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using Lotto.Model.Entities.Hub;

namespace Lotto.Processor.Interfaces
{
    public interface ILottoFacade
    {
        void Run(int size, IPauseToken pauseToken, CancellationToken cancellationToken);

        Task RunAsync(int lotteryId, IPauseToken pauseToken, CancellationToken cancellationToken);

        Task StartProcessing(LotteryProcessStatus status, IPauseToken pauseToken, CancellationToken cancellationToken);
    }
}
