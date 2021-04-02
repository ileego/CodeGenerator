using CodeGenerator.Core.ForTest.Entities;
using CodeGenerator.Infra.Common.Repository;

namespace CodeGenerator.Core.ForTest.Repository
{
    public interface IUserContactRepository : IEfRepository<UserContact, long>
    {
    }
}
