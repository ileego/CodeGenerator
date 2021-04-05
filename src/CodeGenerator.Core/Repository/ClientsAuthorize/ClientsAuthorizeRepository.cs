using Microsoft.EntityFrameworkCore;
using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Infra.Common.ValueModel;

namespace CodeGenerator.Core.Repository.ClientsAuthorize
{
    public class ClientsAuthorizeRepository : EfRepository<Entities.ClientsAuthorize>, IClientsAuthorizeRepository
    {
        public ClientsAuthorizeRepository(DbContext dbContext, UnitOfWorkStatus unitOfWorkStatus)
			: base(dbContext, unitOfWorkStatus)
        {
        }
    }
}