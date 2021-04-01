using CodeGenerator.Infra.Common.Implements;
using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Core.Db.Repository.Table
{
    public class TableRepository : QueryRepository<Entities.Table>, ITableRepository
    {
        public TableRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
