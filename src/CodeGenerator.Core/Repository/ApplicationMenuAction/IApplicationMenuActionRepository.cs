using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Repository.ApplicationMenuAction
{
    public interface IApplicationMenuActionRepository : IEfRepository<Entities.ApplicationMenuAction, long> 
    {
    }
}