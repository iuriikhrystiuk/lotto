using System.Collections.Generic;
using Lotto.Model.Entities.Hub;

namespace Lotto.Logic.Interfaces
{
    public interface ILotteryManager
    {
        IEnumerable<Lottery> Get();

        void Save(IList<Lottery> lotteries);
    }
}