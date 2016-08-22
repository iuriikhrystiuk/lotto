// <copyright file="LotteryDrawingSequenceCalculator.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using Lotto.Logic.Algorithms.Interfaces.Sequence;
using Lotto.Model.Entities.Process;

namespace Lotto.Logic.Algorithms.Implementation.Sequence
{
    public class LotteryDrawingSequenceCalculator : ISequenceCalculator<LotteryDrawing>
    {
        private readonly ICollectionGenerator<LotteryDrawing> collectionGenerator;
        private readonly IPredecessorsExtractor predecessorsExtractor;
        private readonly IPredecessorProbabilityCalculator probabilityCalculator;

        public LotteryDrawingSequenceCalculator(
            ICollectionGenerator<LotteryDrawing> collectionGenerator,
            IPredecessorsExtractor predecessorsExtractor, 
            IPredecessorProbabilityCalculator probabilityCalculator)
        {
            this.collectionGenerator = collectionGenerator;
            this.predecessorsExtractor = predecessorsExtractor;
            this.probabilityCalculator = probabilityCalculator;
        }

        public IEnumerable<SequenceElement> GetSequenceProbability(IEnumerable<LotteryDrawing> drawings)
        {
            var dr = drawings.ToList();
            var source = this.collectionGenerator.GenerateCollection(dr).ToList();
            for (int i = 1; i <= 80; i++)
            {
                var predecessors = this.predecessorsExtractor.Extract(i, source);
                var predecessorsWithRatings = this.probabilityCalculator.CountProbabilities(i, predecessors, source);
                var existingPredecessor = predecessorsWithRatings.Where(kv => kv.Value > 0).ToList();
                if (existingPredecessor.Any())
                {
                    yield return
                        new SequenceElement
                        {
                            Number = i,
                            Probability = existingPredecessor.Max(p => p.Value)
                        };
                }
            }
        }
    }
}
