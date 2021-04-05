using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Repository.EmployeeAuthorize
{
    public interface IEmployeeAuthorizeRepository : IEfRepository<Entities.EmployeeAuthorize, long> 
    {
    }
}