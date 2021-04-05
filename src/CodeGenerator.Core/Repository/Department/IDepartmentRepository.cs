using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Repository.Department
{
    public interface IDepartmentRepository : IEfRepository<Entities.Department, long> 
    {
    }
}