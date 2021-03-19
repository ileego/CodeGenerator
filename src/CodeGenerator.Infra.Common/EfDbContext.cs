using System.Threading;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.Interfaces;
using CodeGenerator.Infra.Common.ValueModel;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace CodeGenerator.Infra.Common
{
    public sealed class EfDbContext : DbContext
    {
        private readonly UnitOfWorkStatus _unitOfWorkStatus;
        private readonly IEntityInfo _entityInfo;
        public static readonly LoggerFactory LoggerFactory = new LoggerFactory(new[] {
            new DebugLoggerProvider()
        });

        public EfDbContext([NotNull] DbContextOptions options,
            UnitOfWorkStatus unitOfWorkStatus,
            [NotNull] IEntityInfo entityInfo) : base(options)
        {
            _unitOfWorkStatus = unitOfWorkStatus;
            _entityInfo = entityInfo;
            //关闭默认事务
            //Database.AutoTransactionsEnabled = false;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ////没有自动开启事务的情况下,保证主从表插入，主从表更新开启事务。
            //var isManualTransaction = false;
            //if (!Database.AutoTransactionsEnabled && !_unitOfWorkStatus.IsStartingUow)
            //{
            //    isManualTransaction = true;
            //    Database.AutoTransactionsEnabled = true;
            //}

            var result = base.SaveChangesAsync(cancellationToken);

            ////如果手工开启了自动事务，用完后关闭。
            //if (isManualTransaction)
            //    Database.AutoTransactionsEnabled = false;

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var (assembly, types) = _entityInfo.GetEntitiesInfo();

            foreach (var entityType in types)
            {
                modelBuilder.Entity(entityType);
            }

            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //debug日志
            optionsBuilder.UseLoggerFactory(LoggerFactory);
            //关闭缓存，每次都会调用OnModelCreating
            //用于设置是否启用缓存
            //optionsBuilder.EnableServiceProviderCaching(false);
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();

        }
    }
}
