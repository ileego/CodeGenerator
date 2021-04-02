using System;

namespace CodeGenerator.Infra.Common.Entity
{
    /// <summary>
    /// Modify Audit
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface IModifyAuditEntity<TUser> : ICreationAuditEntity<TUser>
    {
        /// <summary>
        /// 修改操作人员
        /// </summary>
        public TUser LastModifier { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

    }
}
