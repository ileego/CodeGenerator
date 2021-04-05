using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Repository.District
{
    public interface IDistrictRepository : IEfRepository<Entities.District, long> 
    {
    }
}