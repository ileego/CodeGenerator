using CodeGenerator.Infra.Common.ForTest.Entities;
using CodeGenerator.Infra.Common.Interfaces;

namespace CodeGenerator.Infra.Common.ForTest.Repository
{
    public interface IUserRepository : IEfRepository<User, long>
    {
    }
}
