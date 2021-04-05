using Microsoft.EntityFrameworkCore;
using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Infra.Common.ValueModel;

namespace CodeGenerator.Core.Repository.ApplicationRole
{
    public class ApplicationRoleRepository : EfRepository<Entities.ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(DbContext dbContext, UnitOfWorkStatus unitOfWorkStatus)
			: base(dbContext, unitOfWorkStatus)
        {
        }
    }
}