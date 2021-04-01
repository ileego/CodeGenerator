using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CodeGenerator.Core.Interfaces;
using CodeGenerator.Core.Utils;
using CodeGenerator.Infra.Common.Extensions;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;

namespace CodeGenerator.Core.Implements
{
    /// <summary>
    /// 上下文
    /// </summary>
    [Serializable]
    public class GenerateContext : IGenerateContext
    {
        /// <summary>
        /// 命名空间,可以用多个层次，以[.]分隔
        /// </summary>
        private string[] _namespace;
        public string FullNamespace => string.Join(".", _namespace);
        public string[] Namespace => _namespace;
        public void SetNamespace(string value)
        {
            _namespace = value.Split(".");
        }
        /// <summary>
        /// 表集合
        /// </summary>
        public ICollection<ITable> Tables { get; set; }
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
        /// 表是否存在
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public bool TableExistsByClassName(string className)
        {
            return Tables.Any(t => t.ClassName.Equals(className, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 按表名称获取表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public ITable GetTable(string tableName)
        {
            var table = Tables.FirstOrDefault(t => t.TableName.Equals(tableName, StringComparison.OrdinalIgnoreCase));
            return table;
        }

        /// <summary>
        /// 按类名名称获取表
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public ITable GetTableByClassName(string className)
        {
            var table = Tables.FirstOrDefault(t => t.ClassName.Equals(className, StringComparison.OrdinalIgnoreCase));
            return table;
        }

        /// <summary>
        /// 去除错误的外键引用，
        /// 即：如果外键
        /// </summary>
        private void ClearBadForeignKeyRef()
        {
            foreach (var table in Tables)
            {
                table.ForeignKeys = table.ForeignKeys
                    .Where(t => this.TableExistsByClassName(t.RefTableClassName)).ToList();
            }
        }

        /// <summary>
        /// 生成代码，多文件
        /// 模板通过@ViewBag.Namespace获取命名空间
        /// </summary>
        /// <param name="templatePath">模板路径</param>
        /// <param name="outPath">文件保存路径</param>
        /// <param name="filenamePrefix">文件名称前缀</param>
        /// <param name="filenamePostfix">文件名称后缀</param>
        /// <param name="createSeparateDirectory">创建独立目录</param>
        /// <param name="withDefaultExcludeField">添加默认的排除字段</param>
        /// <param name="excludeTableClassNames">要排除的表的类名</param>
        /// <param name="excludeFieldPropertyNames">要排除的字段的属性名</param>
        /// <returns></returns>
        public void GenerateCode(string templatePath, string outPath,
            bool createSeparateDirectory,
            string filenamePrefix = "",
            string filenamePostfix = "",
            bool withDefaultExcludeField = true,
            ICollection<string> excludeTableClassNames = null,
            ICollection<string> excludeFieldPropertyNames = null)
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
            //清理错误的外键
            ClearBadForeignKeyRef();
            var tables = Tables;
            //排除表
            if (excludeTableClassNames != null)
            {
                tables = Tables.ExcludeTablesByClassNames(excludeTableClassNames);
            }
            foreach (var table in tables)
            {
                var viewBag = new DynamicViewBag();
                viewBag.AddValue("Namespace", Namespace);
                viewBag.AddValue("FullNamespace", FullNamespace);
                excludeFieldPropertyNames ??= new List<string>();

                //添加默认的排除字段
                if (withDefaultExcludeField)
                {
                    var defaultExcludeFields = Exclude.DefaultExcludeFields().ToArray();
                    excludeFieldPropertyNames.AddRange(defaultExcludeFields);
                }

                var newTable = (ITable)table.Clone();
                //排除字段
                newTable.Fields = newTable.Fields.ExcludeFieldsByPropertyNames(excludeFieldPropertyNames);
                var outStr = razorEngineService.RunCompile(name: "template", typeof(ITable), model: newTable, viewBag);
                var filename = createSeparateDirectory
                    ? $"{outPath}\\{table.ClassName}\\{filenamePrefix}{table.ClassName}{filenamePostfix}.cs"
                    : $"{outPath}\\{filenamePrefix}{table.ClassName}{filenamePostfix}.cs";
                OutFile.WriteText(filename, outStr);
            }

        }

        /// <summary>
        /// 生成代码，单文件
        /// </summary>
        /// <param name="templatePath">模板路径</param>
        /// <param name="outPath">文件保存路径</param>
        /// <param name="excludeTableClassNames">要排除的表的类名</param>
        /// <param name="excludeFieldPropertyNames">要排除的字段的属性名</param>
        /// <returns></returns>
        public void GenerateCodeSingleFile(string templatePath, string outPath,
            ICollection<string> excludeTableClassNames = null, ICollection<string> excludeFieldPropertyNames = null)
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
            //清理错误的外键
            ClearBadForeignKeyRef();
            var newContext = (IGenerateContext)this.DeepClone();
            //排除表
            if (excludeTableClassNames != null && excludeTableClassNames.Count > 0)
                newContext.Tables = newContext.Tables.ExcludeTablesByClassNames(excludeTableClassNames).ToList();
            //排除字段
            if (excludeFieldPropertyNames != null && excludeFieldPropertyNames.Count > 0)
            {
                foreach (var table in newContext.Tables)
                {
                    table.Fields = table.Fields.ExcludeFieldsByPropertyNames(excludeFieldPropertyNames);
                }
            }
            var outStr = razorEngineService.RunCompile(name: "template", typeof(IGenerateContext), model: newContext);
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
                table.Fields.ExcludeFieldsByPropertyNames(Exclude.DefaultExcludeFields());
            }

            usingBuilder.ToString();
            mapBuilder.ToString();
        }

        public object Clone()
        {
            return this.DeepClone();
        }
    }
}
