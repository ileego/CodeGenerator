using System;

namespace CodeGenerator.Infra.Common.Entity
{
    public abstract class CreationAuditEntity : BaseEntity, ICreationAuditEntity<long>
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public long Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
