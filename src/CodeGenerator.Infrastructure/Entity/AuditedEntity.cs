using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Infrastructure.Entity
{
    [Serializable]
    public abstract class AuditedEntity<TKey, TUser> : CreationAuditedEntity<TKey, TUser>
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
