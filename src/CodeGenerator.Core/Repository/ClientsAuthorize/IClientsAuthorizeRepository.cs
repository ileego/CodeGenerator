using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Repository.ClientsAuthorize
{
    public interface IClientsAuthorizeRepository : IEfRepository<Entities.ClientsAuthorize, long> 
    {
    }
}