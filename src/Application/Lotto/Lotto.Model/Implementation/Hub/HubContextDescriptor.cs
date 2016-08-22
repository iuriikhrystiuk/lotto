// <copyright file="HubContextDescriptor.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;
using IK.DataAccess.Interfaces;

namespace Lotto.Model.Implementation.Hub
{
    public class HubContextDescriptor : IDbContextDescriptor
    {
        public string ContextName { get { return "HubContext"; }}
        public Type DbContextType { get { return typeof(HubContext); } }
    }
}
