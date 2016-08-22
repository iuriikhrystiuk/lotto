// <copyright file="DataAccessInitializer.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System;
using System.Collections.Generic;
using IK.DataAccess.Implementation;
using IK.DataAccess.Interfaces;
using IK.Infrastructure.Interfaces;
using Ninject;

namespace IK.DataAccess
{
    /// <summary>
    ///     The class for data access initializer.
    /// </summary>
    public abstract class DataAccessInitializer : IComponentInitializer
    {
        /// <summary>
        /// Initializes the specified kernel.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public virtual void Initialize(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IUnitOfWorkFactory>().To<UnitOfWorkFactory>();
            kernel.Bind<IRepositoryFactory>().To<DefaultRepositoryFactory>().Named("Default");
            kernel.Bind<IRepositoryFactory>().To<SqlBulkRepositoryFactory>().Named("Bulk");
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));

            IEnumerable<Type> typeOfDbContextFactory = this.GetDbContextFactory();
            foreach (var type in typeOfDbContextFactory)
            {
                if (typeof(IDbContextFactory).IsAssignableFrom(type))
                {
                    kernel.Bind(typeof(IDbContextFactory)).To(type).Named(type.Name);
                }
            }
        }

        /// <summary>
        /// Gets the type of database context factory.
        /// </summary>
        /// <returns>The type of database context factory.</returns>
        protected abstract IEnumerable<Type> GetDbContextFactory();
    }
}
