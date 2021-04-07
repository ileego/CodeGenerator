using CodeGenerator.Infra.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CodeGenerator.Application
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void AddApplicationDependency(this IServiceCollection services)
        {
            services.AddScopeAssembly(typeof(ApplicationDependency).Assembly, "", "Service");
        }
    }
}
