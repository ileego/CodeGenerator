using System;
using System.ComponentModel;

namespace CodeGenerator.Infra.Common.BaseEntities
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
