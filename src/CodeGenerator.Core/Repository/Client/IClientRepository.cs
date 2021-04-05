using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Repository.Client
{
    public interface IClientRepository : IEfRepository<Entities.Client, long> 
    {
    }
}