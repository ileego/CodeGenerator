using System.Collections.Generic;
using CodeGenerator.Core.Db.Repository.Column;
using CodeGenerator.Core.Db.Repository.Table;
using CodeGenerator.Core.ForTest.Repository;
using CodeGenerator.Core.Implements;
using CodeGenerator.Core.Interfaces;
using CodeGenerator.Infra.Common.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace CodeGenerator.Core
{
    public static class CoreDependency
    {
        public static void AddCoreDependency(this IServiceCollection services)
        {
            services.AddScoped<IEntityInfo, EntityInfo>();

            //注册Repository
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<IColumnRepository, ColumnRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserContactRepository, UserContactRepository>();
            services.AddScoped<IGenerateBuilder<Db.Entities.Table, ICollection<Db.Entities.Column>>, MySqlGenerateBuilder>();
            services.AddScoped<IGenerateContext, GenerateContext>();
        }
    }
}
