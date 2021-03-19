using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Infra.Common.BaseEntities
{
    public class EfFullAuditEntity : EfModifyAuditEntity, IFullAuditEntity<long, long>
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 删除操作人员
        /// </summary>
        public long Deleter { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeletionTime { get; set; }
    }
}
