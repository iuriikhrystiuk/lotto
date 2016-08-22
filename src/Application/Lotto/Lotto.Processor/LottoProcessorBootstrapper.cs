// <copyright file="LottoProcessorBootstrapper.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using IK.Infrastructure.Skeleton;
using Lotto.Logic;
using Lotto.Model;

namespace Lotto.Processor
{
    public class LottoProcessorBootstrapper : Bootstrapper
    {
        public LottoProcessorBootstrapper() : base(true)
        {
            this.RegisterComponentInitializer(new DataAccessInitializer());
            this.RegisterComponentInitializer(new LogicInitializer());
            this.RegisterComponentInitializer(new ProcessorComponentInitializer());
        }
    }
}
