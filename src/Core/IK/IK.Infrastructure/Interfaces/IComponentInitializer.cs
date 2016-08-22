// <copyright file="IComponentInitializer.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using Ninject;

namespace IK.Infrastructure.Interfaces
{
    /// <summary>
    ///     The interface for component initializer.
    /// </summary>
    public interface IComponentInitializer
    {
        /// <summary>
        ///     Initializes the specified type mappings in the component.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        void Initialize(IKernel kernel);
    }
}
