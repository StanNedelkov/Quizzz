using System.Linq.Expressions;

namespace Quizzz.Infrastructure.Data.Common.Contracts
{
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// All records in a table
        /// </summary>
        /// <returns>Queryable expression tree</returns>
        IQueryable<T> All<T>() where T : class;

        /// <summary>
        /// All records in a table
        /// </summary>
        /// <returns>Queryable expression tree</returns>
        IQueryable<T> All<T>(Expression<Func<T, bool>> search) where T : class;

        /// <summary>
        /// The result collection won't be tracked by the context
        /// </summary>
        /// <returns>Expression tree</returns>
        IQueryable<T> AllReadonly<T>() where T : class;

        /// <summary>
        /// The result collection won't be tracked by the context
        /// </summary>
        /// <returns>Expression tree</returns>
        IQueryable<T> AllReadonly<T>(Expression<Func<T, bool>> search) where T : class;

        /// <summary>
        /// Gets specific record from database by primary key
        /// </summary>
        /// <returns>Single record</returns>
        Task<T> GetByIdAsync<T>(object id) where T : class;

        Task<T> GetByIdsAsync<T>(object[] id) where T : class;

        /// <summary>
        /// Adds entity to the database
        /// </summary>

        Task AddAsync<T>(T entity) where T : class;

        /// <summary>
        /// Ads collection of entities to the database
        /// </summary>

        Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class;

        /// <summary>
        /// Updates a record in database
        /// </summary>

        void Update<T>(T entity) where T : class;

        /// <summary>
        /// Updates set of records in the database
        /// </summary>

        void UpdateRange<T>(IEnumerable<T> entities) where T : class;

        /// <summary>
        /// Deletes a record from database
        /// </summary>

        Task DeleteAsync<T>(object id) where T : class;

        /// <summary>
        /// Deletes a record from database
        /// </summary>

        void Delete<T>(T entity) where T : class;

        void DeleteRange<T>(IEnumerable<T> entities) where T : class;
        void DeleteRange<T>(Expression<Func<T, bool>> deleteWhereClause) where T : class;


        /// <summary>
        /// Detaches given entity from the context
        /// </summary>

        void Detach<T>(T entity) where T : class;

        /// <summary>
        /// Saves all made changes in trasaction
        /// </summary>
        /// <returns>Error code</returns>
        Task<int> SaveChangesAsync();
    }
}
