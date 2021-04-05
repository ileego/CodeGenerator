using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Repository.ApplicationRole
{
    public interface IApplicationRoleRepository : IEfRepository<Entities.ApplicationRole, long> 
    {
    }
}