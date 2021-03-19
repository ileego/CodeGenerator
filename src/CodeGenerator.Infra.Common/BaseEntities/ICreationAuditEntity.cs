using System;
using System.ComponentModel.DataAnnotations;

namespace CodeGenerator.Infra.Common.BaseEntities
{
    /// <summary>
    /// Create Audit
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TUser"></typeparam>
    public interface ICreationAuditEntity<TKey, TUser> : IEntity<TKey>
    {
        /// <summary>
        /// 创建人
        /// </summary>
        [Required]
        public TUser Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }
    }
}
