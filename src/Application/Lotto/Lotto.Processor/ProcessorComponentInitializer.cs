// <copyright file="ProcessorComponentInitializer.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using IK.Infrastructure.Interfaces;
using Lotto.Common.Implementation;
using Lotto.Common.Interfaces;
using Lotto.Processor.Implementation;
using Lotto.Processor.Implementation.ResourceMaintenance;
using Lotto.Processor.Interfaces;
using Lotto.Processor.Interfaces.ResourceMaintenance;
using Ninject;

namespace Lotto.Processor
{
    public class ProcessorComponentInitializer : IComponentInitializer
    {
        public void Initialize(IKernel kernel)
        {
            kernel.Bind<IDataGenerator>().To<PermutationsDataGenerator>();
            kernel.Bind<IDataProvider>().To<DelimitedEngineProvider>();
            kernel.Bind<IDownloader>().To<WebDownloader>();
            kernel.Bind<ILottoFacade>().To<LottoFacade>();
            kernel.Bind<IWindowsServiceOperator>().To<WindowsServiceOperator>();
            kernel.Bind<ISqlDatabaseWorker>().To<SqlDatabaseWorker>();
        }
    }
}
