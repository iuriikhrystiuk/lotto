// <copyright file="LottoWebBootstrapper.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using IK.Web.Infrastructure.Skeleton;
using Lotto.Logic;
using Lotto.Model;

namespace Lotto.Web.Common
{
    public class LottoWebBootstrapper : HttpBootstrapper
    {
        public LottoWebBootstrapper() : base(true)
        {
            this.RegisterComponentInitializer(new DataAccessInitializer());
            this.RegisterComponentInitializer(new LogicInitializer());
        }
    }
}