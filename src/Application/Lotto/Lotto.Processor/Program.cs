// <copyright file="Program.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;
using Lotto.Processor.Implementation;
using Lotto.Processor.Implementation.ResourceMaintenance;

namespace Lotto.Processor
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrapper = new LottoProcessorBootstrapper();
            bootstrapper.Initialize();
            var scheduler = bootstrapper.Get<Scheduler>();
            var maintainer = bootstrapper.Get<SqlResourcesMaintainer>();
            maintainer.MaintainResources().Wait();
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                scheduler.Process(cancellationTokenSource.Token).ContinueWith(t => maintainer.FreeResources(), TaskContinuationOptions.OnlyOnCanceled);
                Console.ReadLine();
                cancellationTokenSource.Cancel();
                Console.ReadLine();
            }
            Console.ReadLine();
            maintainer.FreeResources().Wait();
        }
    }
}
