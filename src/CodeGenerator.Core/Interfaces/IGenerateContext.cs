using System;
using System.Collections.Generic;
using CodeGenerator.Core.Implements;

namespace CodeGenerator.Core.Interfaces
{
    /// <summary>
    /// 上下文
    /// </summary>
    public interface IGenerateContext : ICloneable
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        string[] Namespace { get; }
        /// <summary>
        /// 命名空间全名
        /// </summary>
        string FullNamespace { get; }
        /// <summary>
        /// 设置命名空间,可以用多个层次，以[.]分隔
        /// </summary>
        /// <param name="value"></param>
        void SetNamespace(string value);
        /// <summary>
        /// 表集合
        /// </summary>
        ICollection<ITable> Tables { get; set; }

        /// <summary>
        /// 添加表
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        ICollection<ITable> AddTable(ITable table);

        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        bool TableExists(string tableName);

        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        bool TableExists(ITable table);

        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        bool TableExistsByClassName(string className);

        /// <summary>
        /// 按表名称获取表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        ITable GetTable(string tableName);

        /// <summary>
        /// 按类名名称获取表
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        ITable GetTableByClassName(string className);

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
        void GenerateCode(string templatePath,
            string outPath,
            bool createSeparateDirectory,
            string filenamePrefix = "",
            string filenamePostfix = "",
            bool withDefaultExcludeField = true,
            ICollection<string> excludeTableClassNames = null,
            ICollection<string> excludeFieldPropertyNames = null);

        /// <summary>
        /// 生成代码，单文件
        /// </summary>
        /// <param name="templatePath">模板路径</param>
        /// <param name="outPath">文件保存路径</param>
        /// <param name="excludeTableClassNames">要排除的表的类名</param>
        /// <param name="excludeFieldPropertyNames">要排除的字段的属性名</param>
        void GenerateCodeSingleFile(string templatePath,
            string outPath,
            ICollection<string> excludeTableClassNames = null,
            ICollection<string> excludeFieldPropertyNames = null);
    }
}
