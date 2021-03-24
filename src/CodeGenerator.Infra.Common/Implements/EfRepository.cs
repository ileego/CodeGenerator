using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.BaseEntities;
using CodeGenerator.Infra.Common.Interfaces;
using CodeGenerator.Infra.Common.ValueModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Z.EntityFramework.Plus;

namespace CodeGenerator.Infra.Common.Implements
{
    public abstract partial class QueryRepository<TEntity> : IQueryRepository<TEntity> where TEntity : NoKeyEntity
    {
        protected QueryRepository(DbContext dbContext)
        {
            var dbSet = dbContext.Set<TEntity>();
            Query = dbSet.AsQueryable();
        }

        /// <summary>
        /// DbQuery
        /// </summary>
        public IQueryable<TEntity> Query { get; }
    }
    /// <summary>
    /// Ef Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class EfRepository<TEntity> : IEfRepository<TEntity, long> where TEntity : Entity
    {
        private readonly UnitOfWorkStatus _unitOfWorkStatus;

        protected EfRepository(DbContext dbContext,
            UnitOfWorkStatus unitOfWorkStatus)
        {
            Context = dbContext;
            _unitOfWorkStatus = unitOfWorkStatus;
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

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity FindById(long id)
        {
            return DbSet.FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(t => t.Id == id, cancellationToken: cancellationToken); ;
        }

        /// <summary>
        /// 包含任意满足条件的数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> whereExpression)
        {
            var dbSet = DbSet.AsNoTracking();
            return dbSet.Any(whereExpression);
        }

        /// <summary>
        /// 包含任意满足条件的数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default)
        {
            var dbSet = DbSet.AsNoTracking();
            return await dbSet.AnyAsync(whereExpression, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 满足条件记录数
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> whereExpression)
        {
            var dbSet = DbSet.AsNoTracking();
            return dbSet.Count(whereExpression);
        }

        /// <summary>
        /// 满足条件记录数
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default)
        {
            var dbSet = DbSet.AsNoTracking();
            return await dbSet.CountAsync(whereExpression, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Insert(TEntity entity)
        {
            DbSet.Add(entity);
            if (_unitOfWorkStatus.IsStartingUow)
                return 0;
            return Context.SaveChanges();
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            if (_unitOfWorkStatus.IsStartingUow)
                return await Task.FromResult(0);
            return await Context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int BulkInsert(ICollection<TEntity> entities)
        {
            DbSet.AddRange(entities);
            if (_unitOfWorkStatus.IsStartingUow)
                return 0;
            return Context.SaveChanges();
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<int> BulkInsertAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await DbSet.AddRangeAsync(entities, cancellationToken);
            if (_unitOfWorkStatus.IsStartingUow)
                return await Task.FromResult(0);
            return await Context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Update(TEntity entity)
        {
            ////获取实体状态
            //var entry = Context.Entry(entity);
            ////如果实体没有被跟踪，将实体添加到Context并标记为Modified
            //if (entry.State == EntityState.Detached)
            //{
            //    DbSet.Attach(entity);
            //    Context.Entry(entity).State = EntityState.Modified;
            //}
            //if (entry.State == EntityState.Unchanged)
            //{
            //    return 0;
            //}
            Context.Update(entity);
            if (_unitOfWorkStatus.IsStartingUow)
                return 0;
            return Context.SaveChanges();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ////获取实体状态
            //var entry = Context.Entry(entity);
            ////如果实体没有被跟踪，将实体添加到Context并标记为Modified
            //if (entry.State == EntityState.Detached)
            //{
            //    DbSet.Attach(entity);
            //    Context.Entry(entity).State = EntityState.Modified;
            //}
            //if (entry.State == EntityState.Unchanged)
            //{
            //    return await Task.FromResult(0);
            //}
            Context.Update(entity);
            if (_unitOfWorkStatus.IsStartingUow)
                return await Task.FromResult(0);
            return await Context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int BulkUpdate(IEnumerable<TEntity> entities)
        {
            DbSet.BulkUpdate(entities);
            if (_unitOfWorkStatus.IsStartingUow)
                return 0;
            return Context.SaveChanges();
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<int> BulkUpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await DbSet.BulkUpdateAsync(entities, cancellationToken);
            if (_unitOfWorkStatus.IsStartingUow)
                return await Task.FromResult(0);
            return await Context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public int Delete(object id)
        {
            TEntity entity = DbSet.Find(id);
            return Delete(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(object id, CancellationToken cancellationToken = default)
        {
            TEntity entity = await DbSet.FindAsync(id);
            return await DeleteAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual int Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
            if (_unitOfWorkStatus.IsStartingUow)
                return 0;
            return Context.SaveChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
            if (_unitOfWorkStatus.IsStartingUow)
                return await Task.FromResult(0);
            return await Context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 条件删除,直接提交
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public virtual int Delete(Expression<Func<TEntity, bool>> condition)
        {
            Query.Where(condition).Delete();
            if (_unitOfWorkStatus.IsStartingUow)
                return 0;
            return Context.SaveChanges();
        }

        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            await Query.Where(condition).DeleteAsync(cancellationToken);
            if (_unitOfWorkStatus.IsStartingUow)
                return await Task.FromResult(0);
            return await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
