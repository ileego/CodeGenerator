using Microsoft.EntityFrameworkCore;
using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Infra.Common.ValueModel;

namespace CodeGenerator.Core.Repository.EmployeeAuthorize
{
    public class EmployeeAuthorizeRepository : EfRepository<Entities.EmployeeAuthorize>, IEmployeeAuthorizeRepository
    {
        public EmployeeAuthorizeRepository(DbContext dbContext, UnitOfWorkStatus unitOfWorkStatus)
			: base(dbContext, unitOfWorkStatus)
        {
        }
    }
}