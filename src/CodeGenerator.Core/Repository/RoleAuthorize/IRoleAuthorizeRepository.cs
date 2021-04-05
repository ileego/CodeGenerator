using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Repository.RoleAuthorize
{
    public interface IRoleAuthorizeRepository : IEfRepository<Entities.RoleAuthorize, long> 
    {
    }
}