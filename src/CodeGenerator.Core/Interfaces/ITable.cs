using System.Collections.Generic;

namespace CodeGenerator.Core.Interfaces
{
    /// <summary>
    /// 表
    /// </summary>
    public interface ITable
    {
        /// <summary>
        /// 表名
        /// </summary>
        string TableName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        string Comment { get; set; }
        /// <summary>
        /// 获取所有键
        /// </summary>
        ICollection<IKey> Keys { get; }
        /// <summary>
        /// 获取所有字段
        /// </summary>
        ICollection<IField> Fields { get; }
    }
}
