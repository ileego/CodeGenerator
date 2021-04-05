using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Repository.ApplicationSystem
{
    public interface IApplicationSystemRepository : IEfRepository<Entities.ApplicationSystem, long> 
    {
    }
}