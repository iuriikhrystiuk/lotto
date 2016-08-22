// <copyright file="HubContextFactory.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;
using IK.DataAccess.Interfaces;

namespace Lotto.Model.Implementation.Hub
{
    public class HubContextFactory : IDbContextFactory
    {
        public bool CanCreateContext(IDbContextDescriptor contextDescriptor)
        {
            return contextDescriptor.GetType().IsAssignableFrom(typeof(HubContextDescriptor));
        }

        public IDbContext CreateDbContext()
        {
            return new HubContext();
        }

        public IDbContext CreateDbContext(string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
