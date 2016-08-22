using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lotto.Model.Entities.Hub;

namespace Lotto.Logic.Interfaces
{
    public interface ILotterySourceConfigManager
    {
        List<LotterySourceConfig> GetConfigsForSource(int lotterySourceId);
    }
}
