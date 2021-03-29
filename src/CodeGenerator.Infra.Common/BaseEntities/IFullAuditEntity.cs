using System;
using System.ComponentModel;

namespace CodeGenerator.Infra.Common.BaseEntities
{
    /// <summary>
    /// Full Audit
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface IFullAuditEntity<TUser> : IModifyAuditEntity<TUser>, ISoftDelete<TUser>
    {
    }
}
