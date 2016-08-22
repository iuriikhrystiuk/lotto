// <copyright file="ILotterySourceManager.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using Lotto.Model.Entities.Hub;

namespace Lotto.Logic.Interfaces
{
    public interface ILotterySourceManager
    {
        LotterySource GetSourceForLottery(int lotteryId);
    }
}
