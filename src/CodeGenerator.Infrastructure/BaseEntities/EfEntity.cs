using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeGenerator.Infrastructure.BaseEntities
{
    /// <summary>
    /// Ef Base Entity
    /// </summary>
    public class EfEntity : IEntity<long>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public long Id { get; set; }
    }
}
