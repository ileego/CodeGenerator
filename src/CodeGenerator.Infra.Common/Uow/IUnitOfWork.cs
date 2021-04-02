using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CodeGenerator.Infra.Common.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 获取当前DBContext
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <param name="sharedToCap"></param>
        IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.RepeatableRead, bool sharedToCap = false);
        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();
        /// <summary>
        /// 回滚
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RollbackAsync(CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// 提交数据
        /// </summary>
        /// <returns></returns>
        int Commit();
        /// <summary>
        /// 提交数据
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
