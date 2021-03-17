using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeGenerator.Infrastructure.BaseEntities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Z.EntityFramework.Plus;

namespace CodeGenerator.Infrastructure.Impl
{
    /// <summary>
    /// Ef Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class EfRepository<TEntity> : IEfRepository<TEntity, long> where TEntity : EfEntity
    {
        protected EfRepository(DbContext dbContext)
        {
            Context = dbContext;
            Database = Context.Database;
            DbSet = Context.Set<TEntity>();
            Query = DbSet.AsQueryable();
        }
        /// <summary>
        /// DbContext
        /// </summary>
        public DbContext Context { get; }
        /// <summary>
        /// Database
        /// </summary>
        public DatabaseFacade Database { get; }
        /// <summary>
        /// DbSet
        /// </summary>
        public DbSet<TEntity> DbSet { get; }
        /// <summary>
        /// DbQuery
        /// </summary>
        public IQueryable<TEntity> Query { get; }

        public virtual TEntity Get(params long[] key)
        {
            return DbSet.Find(key);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }
        public virtual void BulkInsert(ICollection<TEntity> entities)
        {
            DbSet.AddRangeAsync(entities);
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }
        public virtual void BulkUpdate(IEnumerable<TEntity> entities)
        {
            DbSet.BulkUpdate(entities);
        }


        public void Delete(object ids)
        {
            TEntity entity = DbSet.Find(ids);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
        }

        public virtual int Delete(Expression<Func<TEntity, bool>> condition)
        {
            return Query.Where(condition).Delete();
        }

        public virtual Task<int> DeleteAsync(Expression<Func<TEntity, bool>> condition)
        {
            return Query.Where(condition).DeleteAsync();
        }
    }
}
