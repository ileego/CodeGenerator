using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Infra.Common.BaseEntities;

namespace CodeGenerator.Core.Db.Entities
{
    /// <summary>
    /// 列
    /// </summary>
    public class Column : NoKeyEntity
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 列类型，包含长度 decimal(10,2)
        /// </summary>
        public string ColumnType { get; set; }
        /// <summary>
        /// 是否可为空
        /// </summary>
        public string IsNullable { get; set; }
        /// <summary>
        /// 字符串最大长度
        /// </summary>
        public long? CharacterMaximumLength { get; set; }
        /// <summary>
        /// 数字长度
        /// </summary>
        public int? NumericPrecision { get; set; }
        /// <summary>
        /// 数字小数位数
        /// </summary>
        public int? NumericScale { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string ColumnDefault { get; set; }
        /// <summary>
        /// 键类型 PRI MUL
        /// </summary>
        public string ColumnKey { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string ColumnComment { get; set; }
        /// <summary>
        /// 定顺位置
        /// </summary>
        public short OrdinalPosition { get; set; }
    }
}
