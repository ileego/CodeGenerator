using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Core.Interfaces;
using CodeGenerator.Infra.Common;

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


        /// <summary>
        /// 属于哪个表
        /// </summary>
        public string ToTable { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 数据库数据类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public int? Precision { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

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
