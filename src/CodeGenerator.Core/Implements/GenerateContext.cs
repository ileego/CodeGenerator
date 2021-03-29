using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CodeGenerator.Core.Interfaces;
using CodeGenerator.Core.Utils;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;

namespace CodeGenerator.Core.Implements
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class GenerateContext : IGenerateContext
    {
        /// <summary>
        /// 主命名空间
        /// </summary>
        public string Namespace { get; set; }
        /// <summary>
        /// 表集合
        /// </summary>
        public ICollection<ITable> Tables { get; }
        /// <summary>
        /// 添加表
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public ICollection<ITable> AddTable(ITable table)
        {
            this.Tables.Add(table);
            return this.Tables;
        }

        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool TableExists(string tableName)
        {
            return Tables.Any(t => t.TableName.Equals(tableName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool TableExists(ITable table)
        {
            return Tables.Any(t => t.Equals(table));
        }

        /// <summary>
        /// 按表名称获取表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public ITable GetTable(string tableName)
        {
            var table = Tables.FirstOrDefault(t => t.TableName.Equals(tableName, StringComparison.OrdinalIgnoreCase));
            if (table == null)
            {
                throw new Exception("Table does not exist");
            }

            return table;
        }

        /// <summary>
        /// 生成代码
        /// 模板通过@ViewBag.Namespace获取命名空间
        /// 将要排除的字段名放入ViewBag，交由模板处理
        /// @ViewBag.ExcludeFieldNames
        /// </summary>
        /// <param name="templatePath">模板路径</param>
        /// <param name="outPath">文件保存路径</param>
        /// <param name="postfix">名称后缀</param>
        /// <param name="createSeparateDirectory">创建独立目录</param>
        /// <param name="excludeTableNames">要排除的表名</param>
        /// <param name="excludeFieldNames">要排除的字段名</param>
        /// <returns></returns>
        public void GenerateCode(string templatePath, string outPath, string postfix, bool createSeparateDirectory,
            ICollection<string> excludeTableNames = null, ICollection<string> excludeFieldNames = null)
        {
            TemplateServiceConfiguration templateConfig = new TemplateServiceConfiguration
            {
                //Debug = true,
                DisableTempFileLocking = true,
                Language = Language.CSharp,
                EncodedStringFactory = new RawStringFactory()
            };
            var razorEngineService = RazorEngineService.Create((ITemplateServiceConfiguration)templateConfig);
            razorEngineService.AddTemplate("template", templatePath);
            foreach (var table in Tables)
            {
                if (excludeTableNames != null && excludeTableNames.Count > 0 &&
                    excludeTableNames.Any(t => t.Equals(table.TableName, StringComparison.OrdinalIgnoreCase)))
                    continue;
                var viewBag = new DynamicViewBag();
                viewBag.AddValue("Namespace", Namespace);
                viewBag.AddValue("ExcludeFieldNames", excludeFieldNames);
                var outStr = razorEngineService.RunCompile(name: "template", typeof(IGenerateContext), model: table, viewBag);
                OutFile.WriteText(outPath, outStr);
            }

        }

        /// <summary>
        /// 生成代码，单文件
        /// 将要排除表名、字段名放入ViewBag，交由模板处理
        /// @ViewBag.ExcludeTableNames、@ViewBag.ExcludeFieldNames
        /// </summary>
        /// <param name="templatePath">模板路径</param>
        /// <param name="outPath">文件保存路径</param>
        /// <param name="excludeTableNames">要排除的表名</param>
        /// <param name="excludeFieldNames">要排除的字段名</param>
        /// <returns></returns>
        public void GenerateCodeSingleFile(string templatePath, string outPath,
            ICollection<string> excludeTableNames = null, ICollection<string> excludeFieldNames = null)
        {
            TemplateServiceConfiguration templateConfig = new TemplateServiceConfiguration
            {
                //Debug = true,
                DisableTempFileLocking = true,
                Language = Language.CSharp,
                EncodedStringFactory = new RawStringFactory()
            };
            var razorEngineService = RazorEngineService.Create((ITemplateServiceConfiguration)templateConfig);
            razorEngineService.AddTemplate("template", File.ReadAllText(templatePath));
            //将要排除表名、字段名放入ViewBag，交由模板处理
            var viewBag = new DynamicViewBag();
            viewBag.AddValue("ExcludeTableNames", excludeTableNames);
            viewBag.AddValue("ExcludeFieldNames", excludeFieldNames);
            var outStr = razorEngineService.RunCompile(name: "template", typeof(IGenerateContext), model: this, viewBag);
            OutFile.WriteText(outPath, outStr);
        }

        public GenerateContext()
        {
            Tables = new List<ITable>();
        }

        public void Test()
        {
            var usingBuilder = new StringBuilder();
            var mapBuilder = new StringBuilder();
            foreach (var table in Tables)
            {
                usingBuilder.Append(Namespace + ".Dto." + table.TableName + ";\r\n");
                mapBuilder.Append("/*" + table.Comment + "*/\r\n");
                mapBuilder.Append("CreateMap<" + table.TableName + "," + table.TableName + "Input>();");
                mapBuilder.Append("CreateMap<" + table.TableName + "Input," + table.TableName + ">();");
                mapBuilder.Append("CreateMap<" + table.TableName + "," + table.TableName + "QueryResult>();");
                mapBuilder.Append("CreateMap<" + table.TableName + "QueryResult," + table.TableName + ">();");
            }

            usingBuilder.ToString();
            mapBuilder.ToString();
        }
    }
}
