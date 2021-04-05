using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Repository.DataAccessAuthorize
{
    public interface IDataAccessAuthorizeRepository : IEfRepository<Entities.DataAccessAuthorize, long> 
    {
    }
}