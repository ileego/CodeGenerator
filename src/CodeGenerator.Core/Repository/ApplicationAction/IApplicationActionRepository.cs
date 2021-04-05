using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Repository.ApplicationAction
{
    public interface IApplicationActionRepository : IEfRepository<Entities.ApplicationAction, long> 
    {
    }
}