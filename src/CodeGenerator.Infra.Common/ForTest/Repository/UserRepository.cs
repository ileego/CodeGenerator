using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Infra.Common.ForTest.Entities;
using CodeGenerator.Infra.Common.Implements;
using CodeGenerator.Infra.Common.ValueModel;
using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Infra.Common.ForTest.Repository
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext, UnitOfWorkStatus unitOfWorkStatus)
            : base(dbContext, unitOfWorkStatus)
        {
        }
    }
}
