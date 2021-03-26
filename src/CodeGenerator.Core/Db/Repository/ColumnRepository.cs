using CodeGenerator.Core.Db.Entities;
using CodeGenerator.Infra.Common.Implements;
using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Core.Db.Repository
{
    public class ColumnRepository : QueryRepository<Column>, IColumnRepository
    {
        public ColumnRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
