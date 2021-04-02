using System.ComponentModel.DataAnnotations;

namespace CodeGenerator.Infra.Common.Entity
{
    /// <summary>
    /// Base Entity
    /// </summary>
    public abstract class BaseEntity : IEntity<long>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public long Id { get; set; }
    }
}
