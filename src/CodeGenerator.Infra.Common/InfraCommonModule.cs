using Autofac;
using CodeGenerator.Infra.Common.Implements;
using CodeGenerator.Infra.Common.Interfaces;

namespace CodeGenerator.Infra.Common
{
    public class InfraCommonModule : Module
    {
        /// <summary>Override to add registrations to the container.</summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            //注册工作单元
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
            //注册Repository
            builder.RegisterGeneric(typeof(EfRepository<>))
                .UsingConstructor(typeof(EfDbContext))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            //注册Cache
            builder.RegisterType<RedisCache>()
                .As<ICache>()
                .InstancePerLifetimeScope();
            //注册JwtHelper
            builder.RegisterType<JwtHelper>()
                .As<IJwtHelper>()
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
