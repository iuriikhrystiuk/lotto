// <copyright file="IRepository.cs">
// This is a property of a Iurii Khrystiuk. All of the code comes as is
// and no license required.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IK.DataAccess.Interfaces
{
    /// <summary>
    ///     The repository interface for working with data base.
    /// </summary>
    /// <typeparam name="T">The type of entity to work with.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        ///     Gets the collection of all items from the data base.
        /// </summary>
        /// <returns>The collection of items.</returns>
        IEnumerable<T> All();

        /// <summary>
        ///     Gets the collection of items from the data base
        ///     based on the filter expression specified.
        /// </summary>
        /// <param name="filter">The filter to use.</param>
        /// <returns>The collection of items.</returns>
        IEnumerable<T> Where(Expression<Func<T, bool>> filter);

        /// <summary>
        ///     Gets the first item from the data base.
        /// </summary>
        /// <returns>The first item.</returns>
        T First();

        /// <summary>
        ///     Gets the first item from the data base or null if there are no items.
        /// </summary>
        /// <returns>The first item if exists, null otherwise.</returns>
        T FirstOrDefault();

        /// <summary>
        ///     Gets the first item from the data base that matches the specified filter.
        /// </summary>
        /// <param name="filter">The filter expression.</param>
        /// <returns>The first item.</returns>
        T First(Expression<Func<T, bool>> filter);

        /// <summary>
        ///     Gets the first item from the data base that matches the specified filter
        ///     or null if there are no items.
        /// </summary>
        /// <param name="filter">The filter expression.</param>
        /// <returns>The first item if exists, null otherwise.</returns>
        T FirstOrDefault(Expression<Func<T, bool>> filter);

        /// <summary>
        ///     Counts all of the items from the data base.
        /// </summary>
        /// <returns>The total count.</returns>
        long Count();

        /// <summary>
        ///     Counts all of the items from the data base that match the specified filter.
        /// </summary>
        /// <param name="filter">The filter expression.</param>
        /// <returns>The total count.</returns>
        long Count(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Selects maximum result values using the specified maximum selector.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="maxSelector">The maximum selector.</param>
        /// <returns>The maximum result</returns>
        TResult Max<TResult>(Expression<Func<T, TResult>> maxSelector);

        /// <summary>
        ///     Adds item to the data set.
        /// </summary>
        /// <param name="item">The item to add.</param>
        void Add(T item);

        /// <summary>
        ///     Updates item in the data set.
        /// </summary>
        /// <param name="item">The item to update.</param>
        void Update(T item);

        /// <summary>
        ///     Deletes item from the data set.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        void Delete(T item);
    }
}