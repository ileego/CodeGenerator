using Microsoft.EntityFrameworkCore;
using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Infra.Common.ValueModel;

namespace CodeGenerator.Core.Repository.ApiResource
{
    public class ApiResourceRepository : EfRepository<Entities.ApiResource>, IApiResourceRepository
    {
        public ApiResourceRepository(DbContext dbContext, UnitOfWorkStatus unitOfWorkStatus)
			: base(dbContext, unitOfWorkStatus)
        {
        }
    }
}