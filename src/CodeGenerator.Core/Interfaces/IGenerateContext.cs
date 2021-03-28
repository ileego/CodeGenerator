using System.Collections.Generic;
using CodeGenerator.Core.Implements;

namespace CodeGenerator.Core.Interfaces
{
    /// <summary>
    /// 上下文
    /// </summary>
    public interface IGenerateContext
    {
        /// <summary>
        /// 主命名空间
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// 表集合
        /// </summary>
        ICollection<ITable> Tables { get; }

        /// <summary>
        /// 添加表
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        ICollection<ITable> AddTable(ITable table);

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="templatePath">模板路径</param>
        /// <param name="outPath">文件保存路径</param>
        /// <param name="postfix">名称后缀</param>
        /// <param name="createSeparateDirectory">创建独立目录</param>
        /// <param name="excludeTableNames">要排除的表名</param>
        /// <param name="excludeFieldNames">要排除的字段名</param>
        /// <returns></returns>
        void GenerateCode(string templatePath,
            string outPath,
            string postfix,
            bool createSeparateDirectory,
            ICollection<string> excludeTableNames = null,
            ICollection<string> excludeFieldNames = null);

        /// <summary>
        /// 生成代码,单文件
        /// </summary>
        /// <param name="templatePath">模板路径</param>
        /// <param name="outPath">文件保存路径</param>
        /// <param name="excludeTableNames">要排除的表名</param>
        /// <param name="excludeFieldNames">要排除的字段名</param>
        /// <returns></returns>
        void GenerateCodeSingleFile(string templatePath,
            string outPath,
            ICollection<string> excludeTableNames = null,
            ICollection<string> excludeFieldNames = null);
    }
}
