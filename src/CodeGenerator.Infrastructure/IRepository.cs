using CodeGenerator.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : Entity.Entity
    {
        DbContext Context { get; }
        DbSet<TEntity> DbSet { get; }
        IQueryable<TEntity> Query { get; }
        void Insert(TEntity entity);
        void BulkInsert(ICollection<TEntity> entities);
        void Update(TEntity entity);
        void BulkUpdate(IEnumerable<TEntity> entities);
        void Delete(object id);
        void Delete(TEntity entity);
        int Delete(Expression<Func<TEntity, bool>> condition);
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> condition);
    }
    public interface IRepository<TEntity, in TKey> where TEntity : Entity<TKey>
    {
        DbContext Context { get; }
        DbSet<TEntity> DbSet { get; }
        IQueryable<TEntity> Query { get; }
        TEntity Get(params TKey[] key);
        void Insert(TEntity entity);
        void BulkInsert(ICollection<TEntity> entities);
        void Update(TEntity entity);
        void BulkUpdate(IEnumerable<TEntity> entities);
        void Delete(object id);
        void Delete(TEntity entity);
        int Delete(Expression<Func<TEntity, bool>> condition);
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> condition);
    }
}
