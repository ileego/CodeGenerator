using System;

namespace CodeGenerator.Infra.Common.BaseEntities
{
    public abstract class CreationAuditEntity : Entity, ICreationAuditEntity<long, long>
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
