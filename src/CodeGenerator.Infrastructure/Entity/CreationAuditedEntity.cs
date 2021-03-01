using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeGenerator.Infrastructure.Entity
{
    [Serializable]
    public abstract class CreationAuditedEntity<TKey, TUser> : Entity<TKey>
    {
        [Required]
        public virtual TUser Creator { get; set; }
        [Required]
        public virtual DateTime CreationTime { get; set; }
    }
}
