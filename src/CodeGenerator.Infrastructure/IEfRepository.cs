using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeGenerator.Infrastructure.BaseEntities;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CodeGenerator.Infrastructure
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IEfRepository<TEntity, in TKey> where TEntity : EfEntity
    {
        /// <summary>
        /// DbContext
        /// </summary>
        DbContext Context { get; }
        /// <summary>
        /// Database
        /// </summary>
        DatabaseFacade Database { get; }
        /// <summary>
        /// DbSet
        /// </summary>
        DbSet<TEntity> DbSet { get; }
        /// <summary>
        /// DbQuery
        /// </summary>
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
