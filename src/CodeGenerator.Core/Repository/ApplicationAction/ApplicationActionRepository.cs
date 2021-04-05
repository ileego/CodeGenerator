using Microsoft.EntityFrameworkCore;
using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Infra.Common.ValueModel;

namespace CodeGenerator.Core.Repository.ApplicationAction
{
    public class ApplicationActionRepository : EfRepository<Entities.ApplicationAction>, IApplicationActionRepository
    {
        public ApplicationActionRepository(DbContext dbContext, UnitOfWorkStatus unitOfWorkStatus)
			: base(dbContext, unitOfWorkStatus)
        {
        }
    }
}