using System.Collections.Generic;
using Lotto.Logic.Algorithms.Interfaces.Sequence;
using Lotto.Model.Entities.Process;
using Lotto.Model.Extensions;

namespace Lotto.Logic.Algorithms.Implementation.Sequence
{
    public class LotteryDrawingCollectionGenerator : ICollectionGenerator<LotteryDrawing>
    {
        public IList<int> GenerateCollection(IEnumerable<LotteryDrawing> drawings)
        {
            var result = new List<int>();
            foreach (var lotteryDrawing in drawings)
            {
                result.AddRange(lotteryDrawing.Combination);
            }
            return result;
        }
    }
}
