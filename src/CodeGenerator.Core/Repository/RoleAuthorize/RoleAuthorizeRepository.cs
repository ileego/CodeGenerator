using Microsoft.EntityFrameworkCore;
using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Infra.Common.ValueModel;

namespace CodeGenerator.Core.Repository.RoleAuthorize
{
    public class RoleAuthorizeRepository : EfRepository<Entities.RoleAuthorize>, IRoleAuthorizeRepository
    {
        public RoleAuthorizeRepository(DbContext dbContext, UnitOfWorkStatus unitOfWorkStatus)
			: base(dbContext, unitOfWorkStatus)
        {
        }
    }
}