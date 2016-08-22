// <copyright file="Bootstrapper.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System;
using System.Collections.Generic;
using IK.Infrastructure.Interfaces;
using IK.Logging.Implementation;
using IK.Logging.Interfaces;
using Ninject;

namespace IK.Infrastructure.Skeleton
{
    /// <summary>
    ///     The base class of the type mapper.
    /// </summary>
    public class Bootstrapper
    {
        /// <summary>
        ///     The collection of component initializers.
        /// </summary>
        private readonly IList<IComponentInitializer> componentInitializers = new List<IComponentInitializer>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper" /> class.
        /// </summary>
        /// <param name="injectNonPublicMembers">If set to <c>true</c> [inject non public members].</param>
        public Bootstrapper(bool injectNonPublicMembers)
        {
            this.Kernel = new StandardKernel(new NinjectSettings { InjectNonPublic = injectNonPublicMembers });
        }

        /// <summary>
        /// Gets the kernel.
        /// </summary>
        /// <value>
        /// The kernel.
        /// </value>
        protected IKernel Kernel { get; private set; }

        /// <summary>
        ///     Initializes this instance of the type mapper.
        /// </summary>
        public void Initialize()
        {
            this.SetupMappings(this.Kernel);
        }

        /// <summary>
        /// Gets the instance of specified type from the kernel.
        /// </summary>
        /// <typeparam name="T">The type of instance to get.</typeparam>
        /// <returns>The instance of specified type.</returns>
        public T Get<T>()
        {
            return this.Kernel.Get<T>();
        }

        /// <summary>
        /// Gets the instance of specified type from the kernel.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The instance of specified type.
        /// </returns>
        public object Get(Type type)
        {
            return this.Kernel.Get(type);
        }

        /// <summary>
        ///     Registers component initializer to run while setting up the kernel.
        /// </summary>
        /// <param name="initializer">The component initializer.</param>
        public void RegisterComponentInitializer(IComponentInitializer initializer)
        {
            this.componentInitializers.Add(initializer);
        }

        /// <summary>
        /// Sets up the interface to type mappings.
        /// </summary>
        /// <param name="currentKernel">The current kernel.</param>
        protected virtual void SetupMappings(IKernel currentKernel)
        {
            currentKernel.Bind<ILogger>().To<Logger>();
            foreach (IComponentInitializer componentInitializer in this.componentInitializers)
            {
                componentInitializer.Initialize(currentKernel);
            }
        }
    }
}
