// <copyright file="ProcessContextDescriptor.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;
using IK.DataAccess.Interfaces;

namespace Lotto.Model.Implementation.Process
{
    public class ProcessContextDescriptor : IDbContextDescriptor
    {
        public string ContextName { get { return "ProcessContext"; } }
        public Type DbContextType { get { return typeof(ProcessContext); } }
    }
}
