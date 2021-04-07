using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Infra.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CodeGenerator.Core
{
    public static class CoreDependency
    {
        public static void AddCoreDependency(this IServiceCollection services)
        {
            services.AddScoped<IEntityInfo, EntityInfo>();

            services.AddScopeAssembly(typeof(CoreDependency).Assembly);
        }
    }
}
