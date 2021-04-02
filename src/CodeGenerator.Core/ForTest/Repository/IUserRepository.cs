using CodeGenerator.Core.ForTest.Entities;
using CodeGenerator.Infra.Common.Repository;

namespace CodeGenerator.Core.ForTest.Repository
{
    public interface IUserRepository : IEfRepository<User, long>
    {
    }
}
