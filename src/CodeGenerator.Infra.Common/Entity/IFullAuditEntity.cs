namespace CodeGenerator.Infra.Common.Entity
{
    /// <summary>
    /// Full Audit
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface IFullAuditEntity<TUser> : IModifyAuditEntity<TUser>, ISoftDelete<TUser>
    {
    }
}
