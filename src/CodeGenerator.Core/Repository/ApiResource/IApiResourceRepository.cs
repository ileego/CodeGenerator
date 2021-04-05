using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Repository.ApiResource
{
    public interface IApiResourceRepository : IEfRepository<Entities.ApiResource, long> 
    {
    }
}