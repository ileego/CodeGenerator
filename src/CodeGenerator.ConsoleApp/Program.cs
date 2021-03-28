using System;
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
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceProvider();
            var tableFactory = serviceProvider.GetService<ITableFactory<CodeGenerator.Core.Db.Entities.Table,
                ICollection<CodeGenerator.Core.Db.Entities.Column>>>();
            var generateContext = await tableFactory.CreateContext();
            generateContext.Namespace = "CodeGenerator.Service";
            var templatePath = Path.Combine(serviceProvider.ApplicationPath, "Template",
                TemplateTypeConst.MAPPER_PROFILES);
            var outPath = Path.Combine("Output", "Profiles", "MapperProfiles.cs");

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
                //生成代码
                generateContext.GenerateCodeSingleFile(templatePath, outPath);
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
    }
}
