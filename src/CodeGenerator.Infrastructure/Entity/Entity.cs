using System;
using System.ComponentModel.DataAnnotations;

namespace CodeGenerator.Infrastructure.Entity
{
    [Serializable]
    public abstract class Entity : IEntity
    {

    }

    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    [Serializable]
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        [Key]
        public virtual TKey Id { get; set; }
    }

}
