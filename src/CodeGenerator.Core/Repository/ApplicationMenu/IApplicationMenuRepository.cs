using CodeGenerator.Infra.Common.Repository;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Repository.ApplicationMenu
{
    public interface IApplicationMenuRepository : IEfRepository<Entities.ApplicationMenu, long> 
    {
    }
}