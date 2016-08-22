// <copyright file="HttpBootstrapper.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System.Web.Http.Dependencies;
using IK.Infrastructure.Skeleton;
using IK.Web.Infrastructure.Common;

namespace IK.Web.Infrastructure.Skeleton
{
    /// <summary>
    ///     The <c>bootstrapper</c> that is suitable for HTTP.
    /// </summary>
    public class HttpBootstrapper : Bootstrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpBootstrapper"/> class.
        /// </summary>
        /// <param name="injectNonPublicMembers">If set to <c>true</c> [inject non public members].</param>
        public HttpBootstrapper(bool injectNonPublicMembers) : base(injectNonPublicMembers)
        {
        }

        /// <summary>
        /// Gets the dependency resolver to set into web http dependency resolver.
        /// </summary>
        /// <returns>The created dependency resolver.</returns>
        public IDependencyResolver GetDependencyResolver()
        {
            return new NinjectDependencyResolver(this.Kernel);
        }
    }
}