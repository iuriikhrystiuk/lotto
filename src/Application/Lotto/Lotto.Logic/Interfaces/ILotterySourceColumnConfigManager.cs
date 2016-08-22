// <copyright file="ILotterySourceColumnConfigManager.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using Lotto.Model.Entities.Hub;

namespace Lotto.Logic.Interfaces
{
    public interface ILotterySourceColumnConfigManager
    {
        List<LotterySourceColumnConfig> GetColumnsForConfig(int sourceConfigId);
    }
}
