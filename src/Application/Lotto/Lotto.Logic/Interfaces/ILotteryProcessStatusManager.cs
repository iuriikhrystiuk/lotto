// <copyright file="ILotteryProcessStatusManager.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using Lotto.Model.Constants;
using Lotto.Model.Entities.Hub;

namespace Lotto.Logic.Interfaces
{
    public interface ILotteryProcessStatusManager
    {
        IEnumerable<LotteryProcessStatus> Get();

        LotteryProcessStatus GetForPrizeMap(int prizeMapId);

        void SetStatus(int prizeMapId, Status status);
    }
}
