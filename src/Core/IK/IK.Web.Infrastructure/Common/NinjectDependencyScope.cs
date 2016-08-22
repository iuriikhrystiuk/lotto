// <copyright file="NinjectDependencyScope.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Syntax;

namespace IK.Web.Infrastructure.Common
{
    /// <summary>
    ///     The dependency scope working on top of <c>Ninject</c>.
    /// </summary>
    public class NinjectDependencyScope : IDependencyScope
    {
        /// <summary>
        /// The resolver
        /// </summary>
        private IResolutionRoot resolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectDependencyScope"/> class.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        public NinjectDependencyScope(IResolutionRoot resolver)
        {
            this.resolver = resolver;
        }

        /// <summary>
        /// Retrieves a service from the scope.
        /// </summary>
        /// <param name="serviceType">The service to be retrieved.</param>
        /// <returns>
        /// The retrieved service.
        /// </returns>
        /// <exception cref="System.ObjectDisposedException">this;This scope has been disposed</exception>
        public object GetService(Type serviceType)
        {
            if (this.resolver == null)
            {
                throw new ObjectDisposedException("this", "This scope has been disposed");
            }

            return this.resolver.TryGet(serviceType);
        }

        /// <summary>
        /// Retrieves a collection of services from the scope.
        /// </summary>
        /// <param name="serviceType">The collection of services to be retrieved.</param>
        /// <returns>
        /// The retrieved collection of services.
        /// </returns>
        /// <exception cref="System.ObjectDisposedException">this;This scope has been disposed</exception>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (this.resolver == null)
            {
                throw new ObjectDisposedException("this", "This scope has been disposed");
            }

            return this.resolver.GetAll(serviceType);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                IDisposable disposable = this.resolver as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }

                this.resolver = null;
            }
        }
    }
}