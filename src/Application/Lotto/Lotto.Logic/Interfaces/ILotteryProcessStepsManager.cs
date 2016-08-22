// <copyright file="ILotteryProcessStepsManager.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using Lotto.Model.Entities.Hub;

namespace Lotto.Logic.Interfaces
{
    public interface ILotteryProcessStepsManager
    {
        IEnumerable<LotteryProcessStep> AddRange(IEnumerable<LotteryProcessStep> steps);

        void Update(LotteryProcessStep currentStep);

        IEnumerable<LotteryProcessStep> GetForStatus(int statusId);

        void ClearForStatus(int statusId);
    }
}
