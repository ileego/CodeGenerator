using System;
using System.ComponentModel;

namespace CodeGenerator.Infra.Common.Entity
{
    public interface ISoftDelete<TUser>
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
