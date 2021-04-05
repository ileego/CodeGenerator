﻿using System.Collections.Generic;
using CodeGenerator.Core.Db.Repository.Column;
using CodeGenerator.Core.Db.Repository.Table;
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
            services.AddScoped<IGenerateContextBuilder<Db.Entities.Table, ICollection<Db.Entities.Column>>, MySqlGenerateContextBuilder>();
            services.AddScoped<IGenerateContext, GenerateContext>();
        }
    }
}
