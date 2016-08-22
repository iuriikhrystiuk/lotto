// <copyright file="UnitOfWorkFactory.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System.Linq;
using IK.DataAccess.Exceptions;
using IK.DataAccess.Interfaces;

namespace IK.DataAccess.Implementation
{
    /// <summary>
    ///     The factory to produce unit of works.
    /// </summary>
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        /// <summary>
        ///     The factories for data base contexts.
        /// </summary>
        private readonly IDbContextFactory[] contextFactories;

        /// <summary>
        ///     The factory for repositories.
        /// </summary>
        private readonly IRepositoryFactory[] repoFactories;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkFactory" /> class.
        /// </summary>
        /// <param name="contextFactories">The factories for data base contexts.</param>
        /// <param name="repoFactories">The repository factories.</param>
        public UnitOfWorkFactory(IDbContextFactory[] contextFactories, IRepositoryFactory[] repoFactories)
        {
            this.contextFactories = contextFactories;
            this.repoFactories = repoFactories;
        }

        /// <summary>
        /// Creates unit of work.
        /// </summary>
        /// <param name="contextDescriptor">The context descriptor.</param>
        /// <returns>
        /// The unit of work.
        /// </returns>
        public IUnitOfWork CreateUnitOfWork(IDbContextDescriptor contextDescriptor)
        {
            return new UnitOfWork(this.GetDbContextFactoryFor(contextDescriptor).CreateDbContext(), this.repoFactories);
        }

        /// <summary>
        /// Creates unit of work.
        /// </summary>
        /// <param name="contextDescriptor">The context descriptor.</param>
        /// <param name="connectionString">The connection string for the unit of work.</param>
        /// <returns>
        /// The unit of work.
        /// </returns>
        public IUnitOfWork CreateUnitOfWork(IDbContextDescriptor contextDescriptor, string connectionString)
        {
            return new UnitOfWork(this.GetDbContextFactoryFor(contextDescriptor).CreateDbContext(connectionString), this.repoFactories);
        }

        /// <summary>
        /// Gets the required data base context factory by specified data base context.
        /// </summary>
        /// <param name="contextDescriptor">The context descriptor.</param>
        /// <returns>
        /// The data base context factory.
        /// </returns>
        /// <exception cref="DbContextFactoryNotRegisteredException">The data base context {0} was not registered for the unit of work factory</exception>
        private IDbContextFactory GetDbContextFactoryFor(IDbContextDescriptor contextDescriptor)
        {
            var factory = this.contextFactories.FirstOrDefault(f => f.CanCreateContext(contextDescriptor));
            if (factory == null)
            {
                throw new DbContextFactoryNotRegisteredException("The data base context {0} was not registered for the unit of work factory", contextDescriptor.ContextName);
            }

            return factory;
        }
    }
}