﻿using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.ValueModel;
using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CodeGenerator.Infra.Common.Uow
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly UnitOfWorkStatus _unitOfWorkStatus;
        private readonly ICapPublisher _capPublisher;
        private IDbContextTransaction _dbTransaction;

        public UnitOfWork(DbContext dbContext,
            UnitOfWorkStatus unitOfWorkStatus,
            ICapPublisher capPublisher = null)
        {
            this._dbContext = dbContext;
            _unitOfWorkStatus = unitOfWorkStatus;
            _capPublisher = capPublisher;
        }

        /// <summary>
        /// 获取当前DBContext
        /// </summary>
        /// <returns></returns>
        public DbContext GetDbContext()
        {
            return _dbContext;
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <param name="sharedToCap"></param>
        public IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.RepeatableRead, bool sharedToCap = false)
        {
            _dbTransaction = GetDbContextTransaction(isolationLevel, sharedToCap);
            return _dbTransaction;
        }

        /// <summary>
        /// 回滚
        /// </summary>
        public void Rollback()
        {
            CheckNotNull(_dbTransaction);

            _dbTransaction.Rollback();
            _unitOfWorkStatus.IsStartingUow = false;
        }

        /// <summary>
        /// 回滚
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RollbackAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckNotNull(_dbTransaction);

            await _dbTransaction.RollbackAsync(cancellationToken);
            _unitOfWorkStatus.IsStartingUow = false;
        }

        /// <summary>
        /// 提交数据
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            if (_unitOfWorkStatus.IsStartingUow)
                _dbTransaction.Commit();
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 提交数据
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_unitOfWorkStatus.IsStartingUow)
                await _dbTransaction.CommitAsync(cancellationToken);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private IDbContextTransaction GetDbContextTransaction(IsolationLevel isolationLevel, bool sharedToCap = false)
        {
            if (_unitOfWorkStatus.IsStartingUow)
                throw new Exception("UnitOfWork Error");
            else
                _unitOfWorkStatus.IsStartingUow = true;
            IDbContextTransaction trans;

            if (sharedToCap)
            {
                if (_capPublisher == null)
                    throw new Exception("CapPublisher is null");
                else
                    trans = _dbContext.Database.BeginTransaction(_capPublisher, false);
            }
            else
                trans = _dbContext.Database.BeginTransaction(isolationLevel);

            return trans;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _dbTransaction?.Dispose();
            _dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        private bool CheckNotNull(IDbContextTransaction dbContextTransaction, bool isThrowException = true)
        {
            if (dbContextTransaction == null && isThrowException)
                throw new Exception("IDbContextTransaction is null");

            return dbContextTransaction != null;
        }
    }
}
