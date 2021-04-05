using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Repository.Employee
{
    public interface IEmployeeRepository : IEfRepository<Entities.Employee, long> 
    {
    }
}