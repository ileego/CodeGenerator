using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CodeGenerator.Infrastructure.Entity
{
    [Serializable]
    public abstract class FullAuditedEntity<TKey, TUser> : AuditedEntity<TKey, TUser>
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 删除操作人员
        /// </summary>
        public TUser Deleter { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeletionTime { get; set; }
    }
}
