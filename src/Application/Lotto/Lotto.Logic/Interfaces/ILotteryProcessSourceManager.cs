// <copyright file="ILotteryProcessSourceManager.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using Lotto.Model.Entities.Hub;

namespace Lotto.Logic.Interfaces
{
    public interface ILotteryProcessSourceManager
    {
        IEnumerable<LotteryProcessSource> Get(bool selectNew);

        void Save(IList<LotteryProcessSource> items);
    }
}
