﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CodeGenerator.Core.Interfaces;

namespace CodeGenerator.ConsoleApp
{
    class Program
    {
        static readonly string[] ExcludeTables = new[]
        {
            "TblUser", "TblUserContact", "ViewColumns", "ViewTables"
        };

        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceProvider();
            var tableFactory = serviceProvider.GetService<ITableFactory<CodeGenerator.Core.Db.Entities.Table,
                ICollection<CodeGenerator.Core.Db.Entities.Column>>>();
            var generateContext = await tableFactory.CreateContext();

            var stopwatch = new Stopwatch();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("开始生成代码...");
            Console.ForegroundColor = ConsoleColor.Green;
            stopwatch.Start();
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            Task task = new Task(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Thread.Sleep(200);
                    Console.Write("\u0008");
                    Console.Write("->");
                }
            }, token);

            Task genTask = new Task(() =>
            {
                //生成MapperProfiles代码
                GenerateMapperProfiles(generateContext, serviceProvider.ApplicationPath);
                GenerateEntities(generateContext, serviceProvider.ApplicationPath);
                GenerateEntityMaps(generateContext, serviceProvider.ApplicationPath);
                GenerateInputDto(generateContext, serviceProvider.ApplicationPath);
                GenerateValidator(generateContext, serviceProvider.ApplicationPath);
                GenerateQueryParams(generateContext, serviceProvider.ApplicationPath);
                GenerateQueryResult(generateContext, serviceProvider.ApplicationPath);
                GenerateIRepository(generateContext, serviceProvider.ApplicationPath);
                GenerateRepository(generateContext, serviceProvider.ApplicationPath);
                GenerateIService(generateContext, serviceProvider.ApplicationPath);
                GenerateService(generateContext, serviceProvider.ApplicationPath);
                GenerateController(generateContext, serviceProvider.ApplicationPath);
            });

            task.Start();
            genTask.Start();
            Task.WaitAll(genTask);
            stopwatch.Stop();
            tokenSource.Cancel();
            var seconds = stopwatch.ElapsedMilliseconds / 1000;

            Thread.Sleep(200);

            Console.WriteLine();
            Console.WriteLine($"生成成功，耗时{seconds}秒");

            Console.ReadKey();
        }

        static void GenerateMapperProfiles(IGenerateContext generateContext, string rootPath)
        {
            generateContext.SetNamespace("CodeGenerator.Application");
            var templatePath = Path.Combine(rootPath, "Template",
                TemplateTypeConst.MAPPER_PROFILES);
            var outPath = Path.Combine("Output", "Profiles", "MapperProfiles.cs");
            generateContext.GenerateCodeSingleFile(templatePath: templatePath, outPath: outPath, excludeTableClassNames: ExcludeTables);
        }

        static void GenerateEntities(IGenerateContext generateContext, string rootPath)
        {
            generateContext.SetNamespace("CodeGenerator.Core");
            var templatePath = Path.Combine(rootPath, "Template",
                TemplateTypeConst.ENTITY);
            var outPath = Path.Combine("Output", "Entities");
            generateContext.GenerateCode(templatePath: templatePath,
                outPath: outPath, filenamePostfix: "",
                createSeparateDirectory: false,
                excludeTableClassNames: ExcludeTables);
        }

        static void GenerateEntityMaps(IGenerateContext generateContext, string rootPath)
        {
            generateContext.SetNamespace("CodeGenerator.Core");
            var templatePath = Path.Combine(rootPath, "Template",
                TemplateTypeConst.ENTITY_MAP);
            var outPath = Path.Combine("Output", "Maps");
            generateContext.GenerateCode(templatePath: templatePath,
                outPath: outPath,
                filenamePostfix: "Map",
                createSeparateDirectory: false,
                excludeTableClassNames: ExcludeTables);
        }

        static void GenerateInputDto(IGenerateContext generateContext, string rootPath)
        {
            generateContext.SetNamespace("CodeGenerator.Application");
            var templatePath = Path.Combine(rootPath, "Template",
                TemplateTypeConst.INPUT_DTO);
            var outPath = Path.Combine("Output", "DTOs");
            generateContext.GenerateCode(templatePath: templatePath,
                outPath: outPath,
                filenamePostfix: "InputDto",
                createSeparateDirectory: true,
                excludeTableClassNames: ExcludeTables);
        }

        static void GenerateValidator(IGenerateContext generateContext, string rootPath)
        {
            generateContext.SetNamespace("CodeGenerator.Application");
            var templatePath = Path.Combine(rootPath, "Template",
                TemplateTypeConst.VALIDATOR);
            var outPath = Path.Combine("Output", "Validations");
            generateContext.GenerateCode(templatePath: templatePath,
                outPath: outPath,
                filenamePostfix: "Validator",
                createSeparateDirectory: false,
                excludeTableClassNames: ExcludeTables);
        }

        static void GenerateQueryParams(IGenerateContext generateContext, string rootPath)
        {
            generateContext.SetNamespace("CodeGenerator.Application");
            var templatePath = Path.Combine(rootPath, "Template",
                TemplateTypeConst.QUERY_PARAMS_DTO);
            var outPath = Path.Combine("Output", "DTOs");
            generateContext.GenerateCode(templatePath: templatePath,
                outPath: outPath,
                filenamePostfix: "ParamsDto",
                createSeparateDirectory: true,
                excludeTableClassNames: ExcludeTables);
        }

        static void GenerateQueryResult(IGenerateContext generateContext, string rootPath)
        {
            generateContext.SetNamespace("CodeGenerator.Application");
            var templatePath = Path.Combine(rootPath, "Template",
                TemplateTypeConst.QUERY_RESULT_DTO);
            var outPath = Path.Combine("Output", "DTOs");
            generateContext.GenerateCode(templatePath: templatePath,
                outPath: outPath,
                filenamePostfix: "Dto",
                createSeparateDirectory: true,
                withDefaultExcludeField: false,
                excludeTableClassNames: ExcludeTables);
        }

        static void GenerateIRepository(IGenerateContext generateContext, string rootPath)
        {
            generateContext.SetNamespace("CodeGenerator.Core");
            var templatePath = Path.Combine(rootPath, "Template",
                TemplateTypeConst.IREPOSITORY);
            var outPath = Path.Combine("Output", "Repository");
            generateContext.GenerateCode(templatePath: templatePath,
                outPath: outPath,
                filenamePrefix: "I",
                filenamePostfix: "Repository",
                createSeparateDirectory: true,
                excludeTableClassNames: ExcludeTables);
        }

        static void GenerateRepository(IGenerateContext generateContext, string rootPath)
        {
            generateContext.SetNamespace("CodeGenerator.Core");
            var templatePath = Path.Combine(rootPath, "Template",
                TemplateTypeConst.REPOSITORY);
            var outPath = Path.Combine("Output", "Repository");
            generateContext.GenerateCode(templatePath: templatePath,
                outPath: outPath,
                filenamePostfix: "Repository",
                createSeparateDirectory: true,
                excludeTableClassNames: ExcludeTables);
        }

        static void GenerateIService(IGenerateContext generateContext, string rootPath)
        {
            generateContext.SetNamespace("CodeGenerator.Application");
            var templatePath = Path.Combine(rootPath, "Template",
                TemplateTypeConst.ISERVICE);
            var outPath = Path.Combine("Output", "Services");
            generateContext.GenerateCode(templatePath: templatePath,
                outPath: outPath,
                filenamePrefix: "I",
                filenamePostfix: "Service",
                createSeparateDirectory: true,
                withDefaultExcludeField: false,
                excludeTableClassNames: ExcludeTables);
        }

        static void GenerateService(IGenerateContext generateContext, string rootPath)
        {
            generateContext.SetNamespace("CodeGenerator.Application");
            var templatePath = Path.Combine(rootPath, "Template",
                TemplateTypeConst.SERVICE);
            var outPath = Path.Combine("Output", "Services");
            generateContext.GenerateCode(templatePath: templatePath,
                outPath: outPath,
                filenamePostfix: "Service",
                createSeparateDirectory: true,
                withDefaultExcludeField: false,
                excludeTableClassNames: ExcludeTables);
        }

        static void GenerateController(IGenerateContext generateContext, string rootPath)
        {
            generateContext.SetNamespace("CodeGenerator.WebApi");
            var templatePath = Path.Combine(rootPath, "Template",
                TemplateTypeConst.CONTROLLER);
            var outPath = Path.Combine("Output", "Controller");
            generateContext.GenerateCode(templatePath: templatePath,
                outPath: outPath,
                filenamePostfix: "Controller",
                createSeparateDirectory: false,
                withDefaultExcludeField: false,
                excludeTableClassNames: ExcludeTables);
        }
    }
}
