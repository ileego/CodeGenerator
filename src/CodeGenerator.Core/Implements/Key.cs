using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Core.Interfaces;
using CodeGenerator.Infra.Common;

namespace CodeGenerator.Core.Implements
{
    /// <summary>
    /// 键
    /// </summary>
    public class Key : IKey, IEquatable<IKey>
    {
        public Key(IField field)
        {
            if (!field.IsKey)
            {
                throw new Exception($"This field {field.FieldName} is not key");
            }
            this.KeyName = field.FieldName;
            if (field.KeyType != null) this.KeyType = field.KeyType.Value;
        }

        /// <summary>
        /// 键类型
        /// </summary>
        public KeyTypeEnum KeyType { get; set; }

        /// <summary>
        /// 键名称
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(IKey other)
        {
            Check.NotNull(other, nameof(other));
            return this.KeyName == other.KeyName && this.KeyType == other.KeyType;
        }
    }
}
