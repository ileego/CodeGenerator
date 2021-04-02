using System;
using System.ComponentModel.DataAnnotations;

namespace CodeGenerator.Infra.Common.Entity
{
    /// <summary>
    /// Create Audit
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface ICreationAuditEntity<TUser>
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
