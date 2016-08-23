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
        Task StartProcessing(LotteryProcessStatus status, IPauseToken pauseToken, CancellationToken cancellationToken);
    }
}
