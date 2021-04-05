using CodeGenerator.Infra.Common.AuthorizationClient;
using CodeGenerator.Infra.Common.Cache;
using CodeGenerator.Infra.Common.Context;
using CodeGenerator.Infra.Common.Extensions;
using CodeGenerator.Infra.Common.Helper;
using CodeGenerator.Infra.Common.Jwt;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.ValueModel;
using Microsoft.Extensions.DependencyInjection;

namespace CodeGenerator.Infra.Common
{
    /// <summary>
    /// 添加依赖
    /// </summary>
    public static class InfrastructureDependency
    {
        public static void AddInfrastructureDependency(this IServiceCollection services)
        {
            services.AddScoped(typeof(UnitOfWorkStatus));
            //注册工作单元
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //注册UserContext
            services.AddScoped<UserContext>((t) =>
                new UserContext()
                {
                    User = new UserModel()
                    {
                        UserId = 0,
                        UserName = "临时",
                        Account = ""
                    }
                });
            //services.AddScoped<IEntityInfo, EntityInfo>();
            //注册Paged 依赖auto mapper
            services.AddScoped<Paged>();
            //注册Redis工具类
            services.AddScoped<ICache, RedisCache>();
            //注册Jwt工具类
            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<StsExtensions, StsExtensions>();
            services.AddScoped<OssExtensions, OssExtensions>();

            services.AddHttpContextAccessor();
            //注册授权验证帮助类
            services.AddScoped<IAuthorizationClientHelper, AuthorizationClientHelper>();
            ControllerExtension.ServiceLocator.Provider = services.BuildServiceProvider();
        }
    }
}
