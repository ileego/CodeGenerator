using System;

namespace CodeGenerator.Infra.Common.BaseEntities
{
    /// <summary>
    /// Modify Audit
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TUser"></typeparam>
    public interface IModifyAuditEntity<TKey, TUser> : ICreationAuditEntity<TKey, TUser>
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
