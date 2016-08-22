// <copyright file="KenoContextFactory.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;
using IK.DataAccess.Interfaces;

namespace Lotto.Model.Implementation.Process
{
    internal class ProcessContextFactory : IDbContextFactory
    {
        public bool CanCreateContext(IDbContextDescriptor contextDescriptor)
        {
            return contextDescriptor.GetType().IsAssignableFrom(typeof(ProcessContextDescriptor));
        }

        public IDbContext CreateDbContext()
        {
            throw new NotImplementedException("The constructor without parameters is not implemented for the current context.");
        }

        public IDbContext CreateDbContext(string connectionString)
        {
            return new ProcessContext(connectionString);
        }
    }
}