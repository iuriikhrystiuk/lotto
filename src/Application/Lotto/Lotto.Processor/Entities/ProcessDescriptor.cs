// <copyright file="ProcessDescriptor.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using Lotto.Model.Entities.Hub;
using Lotto.Processor.Implementation;

namespace Lotto.Processor.Entities
{
    public class ProcessDescriptor
    {
        public LotteryProcessStatus LotteryProcessStatus { get; set; }

        public PauseTokenSource PauseToken { get; set; }

        public CancellationTokenSource CancellationToken { get; set; }

        public Task Task { get; set; }
    }
}
