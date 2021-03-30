using System.Collections.Generic;

namespace CodeGenerator.Core.Interfaces
{
    /// <summary>
    /// 表类型枚举
    /// </summary>
    public enum TableTypeEnum
    {
        /// <summary>
        /// 表
        /// </summary>
        Table = 0,
        /// <summary>
        /// 视图
        /// </summary>
        View = 1
    }
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
        /// 类名
        /// </summary>
        string ClassName { get; set; }
        /// <summary>
        /// 基类
        /// </summary>
        string BaseClass { get; set; }
        /// <summary>
        /// 变量名
        /// </summary>
        string VariableName { get; set; }
        /// <summary>
        /// 表类型
        /// </summary>
        TableTypeEnum TableType { get; set; }
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
