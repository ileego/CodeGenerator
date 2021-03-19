using System.Collections.Generic;

namespace CodeGenerator.Core.Interfaces
{
    /// <summary>
    /// 上下文
    /// </summary>
    public interface IGenerateContext
    {
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
    }
}
