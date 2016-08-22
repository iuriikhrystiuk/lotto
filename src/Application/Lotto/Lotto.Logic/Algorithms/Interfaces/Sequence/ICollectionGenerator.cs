using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotto.Logic.Algorithms.Interfaces.Sequence
{
    public interface ICollectionGenerator<TDrawing>
    {
        IList<int> GenerateCollection(IEnumerable<TDrawing> drawings);
    }
}
