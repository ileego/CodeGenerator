using System;
using CodeGenerator.Core.Interfaces;
using CodeGenerator.Infra.Common;
using CodeGenerator.Infra.Common.Extensions.String;

namespace CodeGenerator.Core.Implements
{
    /// <summary>
    /// 字段
    /// </summary>
    public class Field : IField, IEquatable<IField>
    {
        public Field()
        {
        }

        public Field(string toTable,
            string fieldName,
            string dataType,
            long length,
            int? precision,
            string comment,
            bool isNullable,
            bool isKey,
            KeyTypeEnum keyType)
        {
            this.ToTable = toTable;
            this.FieldName = fieldName;
            this.PropertyName = fieldName.ToPascal();
            this.VariableName = fieldName.ToCamel();
            this.DataType = dataType;
            this.Length = length;
            this.Precision = precision;
            this.Comment = comment;
            this.IsNullable = isNullable;
            this.IsKey = isKey;
            this.KeyType = keyType;
        }

        /// <summary>
        /// 属于哪个表
        /// </summary>
        public string ToTable { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 变量名
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// 数据库数据类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public int? Precision { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 是否可为空
        /// </summary>
        public bool IsNullable { get; set; }
        /// <summary>
        /// 是否键
        /// </summary>
        public bool IsKey { get; set; }

        /// <summary>
        /// 键类型
        /// </summary>
        public KeyTypeEnum? KeyType { get; set; }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(IField other)
        {
            Check.NotNull(other, nameof(other));
            return this.ToTable == other.ToTable && this.FieldName == other.FieldName;
        }
    }
}
