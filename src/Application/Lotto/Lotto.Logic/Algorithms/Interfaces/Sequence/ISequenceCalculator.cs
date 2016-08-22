using System.Collections.Generic;

namespace Lotto.Logic.Algorithms.Interfaces.Sequence
{
    public interface ISequenceCalculator<TDrawing>
    {
        IEnumerable<SequenceElement> GetSequenceProbability(IEnumerable<TDrawing> drawings);
    }
}
