using CodeGenerator.Infra.Common.Repository;
using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Core.Db.Repository.Column
{
    public class ColumnRepository : QueryRepository<Entities.Column>, IColumnRepository
    {
        public ColumnRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
