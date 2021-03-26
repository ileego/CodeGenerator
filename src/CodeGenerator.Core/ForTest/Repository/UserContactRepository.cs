using CodeGenerator.Core.ForTest.Entities;
using CodeGenerator.Infra.Common.Implements;
using CodeGenerator.Infra.Common.ValueModel;
using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Core.ForTest.Repository
{
    public class UserContactRepository : EfRepository<UserContact>, IUserContactRepository
    {
        public UserContactRepository(DbContext dbContext, UnitOfWorkStatus unitOfWorkStatus)
            : base(dbContext, unitOfWorkStatus)
        {
        }
    }
}
