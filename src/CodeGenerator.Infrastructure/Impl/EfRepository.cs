using CodeGenerator.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace CodeGenerator.Infrastructure.Impl
{
    public abstract partial class EfRepository<TEntity> : IRepository<TEntity> where TEntity : Entity.Entity
    {
        protected EfRepository(DbContext dbContext)
        {
            Context = dbContext;
            DbSet = Context.Set<TEntity>();
            Query = DbSet.AsNoTracking().AsQueryable();
        }

        public DbContext Context { get; }
        public DbSet<TEntity> DbSet { get; }
        public IQueryable<TEntity> Query { get; }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void BulkInsert(ICollection<TEntity> entities)
        {
            DbSet.AddRangeAsync(entities);
        }

        public virtual void BulkInsert(IEnumerable<TEntity> entities)
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

    public abstract partial class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        protected EfRepository(DbContext dbContext)
        {
            Context = dbContext;
            DbSet = Context.Set<TEntity>();
            Query = DbSet.AsQueryable();
        }

        public DbContext Context { get; }
        public DbSet<TEntity> DbSet { get; }
        public IQueryable<TEntity> Query { get; }

        public virtual TEntity Get(params TKey[] key)
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
