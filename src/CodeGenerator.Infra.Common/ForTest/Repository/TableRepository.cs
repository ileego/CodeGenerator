using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Infra.Common.ForTest.Entities;
using CodeGenerator.Infra.Common.Implements;
using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Infra.Common.ForTest.Repository
{
    public class TableRepository : QueryRepository<Table>, ITableRepository
    {
        public TableRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
