using System.Collections.Generic;
using Lotto.Model.Entities.Hub;

namespace Lotto.Logic.Interfaces
{
    public interface ILotteryPrizeMapManager
    {
        IList<LotteryPrizeMap> GetPrizeMapFor(int lotteryId);

        void SaveLotteryPrizeMap(IList<LotteryPrizeMap> prizeMap);
    }
}