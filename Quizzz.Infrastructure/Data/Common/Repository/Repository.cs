using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Quizzz.Infrastructure.Data.Common.Contracts;
using System.Linq.Expressions;

namespace Quizzz.Infrastructure.Data.Common.Repository
{
    public abstract class Repository : IRepository
    {

        /// <summary>
        /// Entity framework DB context holding connection information and properties
        /// and tracking entity states 
        /// </summary>
        /// 
        public Repository(DbContext _context)
        {
            this.Context = _context;
        }
        private DbContext Context { get; set; } 

        /// <summary>
        /// Representation of table in database
        /// </summary>
        private DbSet<T> DbSet<T>() where T : class
        {
            return this.Context.Set<T>();
        }

        /// <summary>
        /// Adds entity to the database
        /// </summary>
        /// 
       
       
        public async Task AddAsync<T>(T entity) where T : class
        {
            await DbSet<T>().AddAsync(entity);
        }

        /// <summary>
        /// Ads collection of entities to the database
        /// </summary>
        
        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            await DbSet<T>().AddRangeAsync(entities);
        }

        /// <summary>
        /// All records in a table
        /// </summary>
        /// <returns>Queryable expression tree</returns>
        public IQueryable<T> All<T>() where T : class
        {
            return DbSet<T>().AsQueryable();
        }

        public IQueryable<T> All<T>(Expression<Func<T, bool>> search) where T : class
        {
            return this.DbSet<T>().Where(search);
        }

        /// <summary>
        /// The result collection won't be tracked by the context
        /// </summary>
        /// <returns>Expression tree</returns>
        public IQueryable<T> AllReadonly<T>() where T : class
        {
            return this.DbSet<T>()
                .AsNoTracking();
        }
        public IQueryable<T> AllReadonly<T>(Expression<Func<T, bool>> search) where T : class
        {
            return this.DbSet<T>()
                .Where(search)
                .AsNoTracking();
        }

        /// <summary>
        /// Deletes a record from database
        /// </summary>
        
        public async Task DeleteAsync<T>(object id) where T : class
        {
            T entity = await GetByIdAsync<T>(id);

            Delete<T>(entity);
        }

        /// <summary>
        /// Deletes a record from database
        /// </summary>
       
        public void Delete<T>(T entity) where T : class
        {
            EntityEntry entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet<T>()
                    .Attach(entity);
            }

            entry.State = EntityState.Deleted;
        }

        /// <summary>
        /// Detaches given entity from the context
        /// </summary>
       
        public void Detach<T>(T entity) where T : class
        {
            EntityEntry entry = this.Context.Entry(entity);

            entry.State = EntityState.Detached;
        }

        /// <summary>
        /// Disposing the context when it is not needed
        /// Don't have to call this method explicitely
        /// Leave it to the IoC container
        /// </summary>
        public void Dispose()
        {
            this.Context.Dispose();
        }

        /// <summary>
        /// Gets specific record from database by primary key
        /// </summary>
        
        public async Task<T> GetByIdAsync<T>(object id) where T : class
        {
            return await DbSet<T>()
                .FindAsync(id);
        }

        public async Task<T> GetByIdsAsync<T>(object[] id) where T : class
        {
            return await DbSet<T>()
                .FindAsync(id);
        }

        /// <summary>
        /// Saves all made changes in trasaction
        /// </summary>
        /// <returns>Error code</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await this.Context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates a record in database
        /// </summary>
        
        public void Update<T>(T entity) where T : class
        {
            this.DbSet<T>()
                .Update(entity);
        }

        /// <summary>
        /// Updates set of records in the database
        /// </summary>
       
        public void UpdateRange<T>(IEnumerable<T> entities) where T : class
        {
            this.DbSet<T>()
                .UpdateRange(entities);
        }

        public void DeleteRange<T>(IEnumerable<T> entities) where T : class
        {
            this.DbSet<T>().RemoveRange(entities);
        }

        public void DeleteRange<T>(Expression<Func<T, bool>> deleteWhereClause) where T : class
        {
            var entities = All<T>(deleteWhereClause);
            DeleteRange(entities);
        }
    }
}
