using CodeGenerator.Core.ForTest.Entities;
using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Infra.Common.ValueModel;
using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Core.ForTest.Repository
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext, UnitOfWorkStatus unitOfWorkStatus)
            : base(dbContext, unitOfWorkStatus)
        {
        }
    }
}
