using Microsoft.EntityFrameworkCore;
using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Infra.Common.ValueModel;

namespace CodeGenerator.Core.Repository.ApplicationMenuAction
{
    public class ApplicationMenuActionRepository : EfRepository<Entities.ApplicationMenuAction>, IApplicationMenuActionRepository
    {
        public ApplicationMenuActionRepository(DbContext dbContext, UnitOfWorkStatus unitOfWorkStatus)
			: base(dbContext, unitOfWorkStatus)
        {
        }
    }
}