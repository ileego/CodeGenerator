using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeGenerator.Infra.Common.BaseEntities
{
    /// <summary>
    /// Base Entity
    /// </summary>
    public abstract class Entity : IEntity<long>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public long Id { get; set; }
    }
}
