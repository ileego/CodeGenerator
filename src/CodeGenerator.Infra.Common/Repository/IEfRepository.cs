using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CodeGenerator.Infra.Common.Repository
{
    public interface IQueryRepository<out TEntity> where TEntity : NoKeyEntity
    {
        /// <summary>
        /// DbQuery
        /// </summary>
        IQueryable<TEntity> Query { get; }
    }

    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IEfRepository<TEntity, in TKey> where TEntity : class
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
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FindById(long id);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        /// <summary>
        /// 包含任意满足条件的数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        bool Any(Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// 包含任意满足条件的数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression,
            CancellationToken cancellationToken = default);
        /// <summary>
        /// 满足条件记录数
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// 满足条件记录数
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression,
            CancellationToken cancellationToken = default);
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(TEntity entity);
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int BulkInsert(ICollection<TEntity> entities);
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> BulkInsertAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(TEntity entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int BulkUpdate(IEnumerable<TEntity> entities);
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> BulkUpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        int Delete(object id);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(object id, CancellationToken cancellationToken = default);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        int Delete(TEntity entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        int Delete(Expression<Func<TEntity, bool>> condition);
        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);
    }
}
