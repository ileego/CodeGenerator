using System;

namespace CodeGenerator.Infra.Common.BaseEntities
{
    public abstract class ModifyAuditEntity : CreationAuditEntity, IModifyAuditEntity<long>
    {
        /// <summary>
        /// 修改操作人员
        /// </summary>
        public long LastModifier { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }
    }
}
