using System.Collections.Generic;
using Lotto.Model.Entities.Process;

namespace Lotto.Processor.Interfaces
{
    internal interface IDataGenerator
    {
        List<Combination> Generate(List<int> collectionToGenerateFrom, int size);
    }
}
