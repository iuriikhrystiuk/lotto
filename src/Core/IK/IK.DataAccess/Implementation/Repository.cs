// <copyright file="Repository.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using IK.DataAccess.Interfaces;

namespace IK.DataAccess.Implementation
{
    /// <summary>
    ///     The repository class for working with data base.
    /// </summary>
    /// <typeparam name="T">The type of entity to work with.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        ///     The data base context.
        /// </summary>
        private readonly IDbContext context;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Repository{T}" /> class.
        /// </summary>
        /// <param name="context">The data base context.</param>
        public Repository(IDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        ///     Gets the collection of all items from the data base.
        /// </summary>
        /// <returns>The collection of items.</returns>
        public IEnumerable<T> All()
        {
            return this.context.Set<T>().ToList();
        }

        /// <summary>
        ///     Gets the collection of items from the data base
        ///     based on the filter expression specified.
        /// </summary>
        /// <param name="filter">The filter to use.</param>
        /// <returns>The collection of items.</returns>
        public IEnumerable<T> Where(Expression<Func<T, bool>> filter)
        {
            return this.context.Set<T>().Where(filter).ToList();
        }

        /// <summary>
        ///     Gets the first item from the data base.
        /// </summary>
        /// <returns>The first item.</returns>
        public T First()
        {
            return this.context.Set<T>().First();
        }

        /// <summary>
        ///     Gets the first item from the data base or null if there are no items.
        /// </summary>
        /// <returns>The first item if exists, null otherwise.</returns>
        public T FirstOrDefault()
        {
            return this.context.Set<T>().FirstOrDefault();
        }

        /// <summary>
        ///     Gets the first item from the data base that matches the specified filter.
        /// </summary>
        /// <param name="filter">The filter expression.</param>
        /// <returns>The first item.</returns>
        public T First(Expression<Func<T, bool>> filter)
        {
            return this.context.Set<T>().First(filter);
        }

        /// <summary>
        ///     Gets the first item from the data base that matches the specified filter
        ///     or null if there are no items.
        /// </summary>
        /// <param name="filter">The filter expression.</param>
        /// <returns>The first item if exists, null otherwise.</returns>
        public T FirstOrDefault(Expression<Func<T, bool>> filter)
        {
            return this.context.Set<T>().FirstOrDefault(filter);
        }

        /// <summary>
        ///     Counts all of the items from the data base.
        /// </summary>
        /// <returns>The total count.</returns>
        public long Count()
        {
            return this.context.Set<T>().Count();
        }

        /// <summary>
        ///     Counts all of the items from the data base that match the specified filter.
        /// </summary>
        /// <param name="filter">The filter expression.</param>
        /// <returns>The total count.</returns>
        public long Count(Expression<Func<T, bool>> filter)
        {
            return this.context.Set<T>().Count(filter);
        }

        /// <summary>
        /// Selects maximum result values using the specified maximum selector.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="maxSelector">The maximum selector.</param>
        /// <returns>
        /// The maximum result
        /// </returns>
        public TResult Max<TResult>(Expression<Func<T, TResult>> maxSelector)
        {
            return this.context.Set<T>().Max(maxSelector);
        }

        /// <summary>
        ///     Adds item to the data set.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item)
        {
            this.context.Set<T>().Add(item);
        }

        /// <summary>
        ///     Updates item in the data set.
        /// </summary>
        /// <param name="item">The item to update.</param>
        public void Update(T item)
        {
            this.context.Set<T>().Attach(item);
            this.context.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        ///     Deletes item from the data set.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        public void Delete(T item)
        {
            this.context.Set<T>().Remove(item);
        }
    }
}