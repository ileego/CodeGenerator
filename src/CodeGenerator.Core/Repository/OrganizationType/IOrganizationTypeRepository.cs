using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Repository.OrganizationType
{
    public interface IOrganizationTypeRepository : IEfRepository<Entities.OrganizationType, long> 
    {
    }
}