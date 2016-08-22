// <copyright file="DataAccessInitializer.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Lotto.Model.Implementation.Hub;
using Lotto.Model.Implementation.Process;

namespace Lotto.Model
{
    public class DataAccessInitializer : IK.DataAccess.DataAccessInitializer
    {
        protected override IEnumerable<Type> GetDbContextFactory()
        {
            yield return typeof(ProcessContextFactory);
            yield return typeof(HubContextFactory);
        }
    }
}
