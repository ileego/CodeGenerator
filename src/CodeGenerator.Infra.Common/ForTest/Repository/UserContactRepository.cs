using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Infra.Common.ForTest.Entities;
using CodeGenerator.Infra.Common.Implements;
using CodeGenerator.Infra.Common.ValueModel;
using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Infra.Common.ForTest.Repository
{
    public class UserContactRepository : EfRepository<UserContact>, IUserContactRepository
    {
        public UserContactRepository(DbContext dbContext, UnitOfWorkStatus unitOfWorkStatus)
            : base(dbContext, unitOfWorkStatus)
        {
        }
    }
}
