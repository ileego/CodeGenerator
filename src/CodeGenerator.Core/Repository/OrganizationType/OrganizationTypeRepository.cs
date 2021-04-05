using Microsoft.EntityFrameworkCore;
using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Infra.Common.ValueModel;

namespace CodeGenerator.Core.Repository.OrganizationType
{
    public class OrganizationTypeRepository : EfRepository<Entities.OrganizationType>, IOrganizationTypeRepository
    {
        public OrganizationTypeRepository(DbContext dbContext, UnitOfWorkStatus unitOfWorkStatus)
			: base(dbContext, unitOfWorkStatus)
        {
        }
    }
}