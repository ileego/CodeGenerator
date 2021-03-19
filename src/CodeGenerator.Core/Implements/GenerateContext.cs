using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Core.Interfaces;

namespace CodeGenerator.Core.Implements
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class GenerateContext : IGenerateContext
    {
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

        public GenerateContext()
        {
            Tables = new List<ITable>();
        }
    }
}
