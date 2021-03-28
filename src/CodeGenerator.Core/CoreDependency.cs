using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Core.Db.Repository;
using CodeGenerator.Core.ForTest.Repository;
using CodeGenerator.Core.Implements;
using CodeGenerator.Core.Interfaces;
using CodeGenerator.Infra.Common.Interfaces;
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
            services.AddScoped<ITableFactory<Db.Entities.Table, ICollection<Db.Entities.Column>>, MySqlTableFactory>();
            services.AddScoped<IGenerateContext, GenerateContext>();
        }
    }
}
