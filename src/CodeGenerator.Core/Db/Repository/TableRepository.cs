using CodeGenerator.Core.Db.Entities;
using CodeGenerator.Infra.Common.Implements;
using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Core.Db.Repository
{
    public class TableRepository : QueryRepository<Table>, ITableRepository
    {
        public TableRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
