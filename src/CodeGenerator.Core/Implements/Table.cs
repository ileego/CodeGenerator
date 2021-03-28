using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Core.Interfaces;
using CodeGenerator.Infra.Common;
using CodeGenerator.Infra.Common.Extensions.String;

namespace CodeGenerator.Core.Implements
{
    /// <summary>
    /// 表
    /// </summary>
    public class Table : ITable, IEquatable<ITable>
    {
        public Table()
        {
            this.Keys = new List<IKey>();
            this.Fields = new List<IField>();
        }

        public Table(string tableName, string comment, TableTypeEnum tableType)
        : this()
        {
            this.TableName = tableName;
            this.ClassName = tableName.ToPascal();
            this.VariableName = tableName.ToCamel();
            this.TableType = tableType;
            this.Comment = comment;
        }

        public Table(string tableName, string comment, TableTypeEnum tableType, ICollection<Field> fields)
        : this(tableName, comment, tableType)
        {
            foreach (var field in fields)
            {
                Fields.Add(field);
                if (field.IsKey)
                    Keys.Add(new Key(field));
            }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 变量名
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// 表类型
        /// </summary>
        public TableTypeEnum TableType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 获取所有键
        /// </summary>
        public ICollection<IKey> Keys { get; }

        /// <summary>
        /// 获取所有字段
        /// </summary>
        public ICollection<IField> Fields { get; }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(ITable other)
        {
            Check.NotNull(other, nameof(other));
            return this.TableName == other.TableName;
        }
    }
}
