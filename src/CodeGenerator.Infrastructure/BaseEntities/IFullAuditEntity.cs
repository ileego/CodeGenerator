using System;
using System.ComponentModel;

namespace CodeGenerator.Infrastructure.BaseEntities
{
    /// <summary>
    /// Full Audit
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TUser"></typeparam>
    public interface IFullAuditEntity<TKey, TUser> : IModifyAuditEntity<TKey, TUser>, ISoftDelete<TUser>
    {
    }
}
