using System.Reflection;
using CodeGenerator.Infra.Common.Extensions;
using CodeGenerator.Infra.Common.Helper;
using CodeGenerator.Infra.Common.Implements;
using CodeGenerator.Infra.Common.Interfaces;
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
            //注册工作单元
            services.AddScoped<IUnitOfWork, UnitOfWork<EfDbContext>>();
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
