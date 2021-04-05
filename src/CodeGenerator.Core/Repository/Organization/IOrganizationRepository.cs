using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Repository.Organization
{
    public interface IOrganizationRepository : IEfRepository<Entities.Organization, long> 
    {
    }
}