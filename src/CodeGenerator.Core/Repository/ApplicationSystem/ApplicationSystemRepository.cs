using Microsoft.EntityFrameworkCore;
using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Infra.Common.ValueModel;

namespace CodeGenerator.Core.Repository.ApplicationSystem
{
    public class ApplicationSystemRepository : EfRepository<Entities.ApplicationSystem>, IApplicationSystemRepository
    {
        public ApplicationSystemRepository(DbContext dbContext, UnitOfWorkStatus unitOfWorkStatus)
			: base(dbContext, unitOfWorkStatus)
        {
        }
    }
}