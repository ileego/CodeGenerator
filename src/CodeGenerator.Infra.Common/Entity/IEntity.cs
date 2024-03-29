﻿using System.ComponentModel.DataAnnotations;

namespace CodeGenerator.Infra.Common.Entity
{
    /// <summary>
    /// Entity默认接口
    /// </summary>
    public interface IEntity
    {
    }

    public abstract class NoKeyEntity : IEntity
    {

    }

    public interface IEntity<TKey> : IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        TKey Id { get; set; }
    }
}
