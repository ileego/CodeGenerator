using Microsoft.EntityFrameworkCore;
using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Infra.Common.ValueModel;

namespace CodeGenerator.Core.Repository.ApplicationMenu
{
    public class ApplicationMenuRepository : EfRepository<Entities.ApplicationMenu>, IApplicationMenuRepository
    {
        public ApplicationMenuRepository(DbContext dbContext, UnitOfWorkStatus unitOfWorkStatus)
			: base(dbContext, unitOfWorkStatus)
        {
        }
    }
}