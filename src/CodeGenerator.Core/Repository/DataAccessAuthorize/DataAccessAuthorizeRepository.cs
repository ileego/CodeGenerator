using Microsoft.EntityFrameworkCore;
using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Infra.Common.ValueModel;

namespace CodeGenerator.Core.Repository.DataAccessAuthorize
{
    public class DataAccessAuthorizeRepository : EfRepository<Entities.DataAccessAuthorize>, IDataAccessAuthorizeRepository
    {
        public DataAccessAuthorizeRepository(DbContext dbContext, UnitOfWorkStatus unitOfWorkStatus)
			: base(dbContext, unitOfWorkStatus)
        {
        }
    }
}