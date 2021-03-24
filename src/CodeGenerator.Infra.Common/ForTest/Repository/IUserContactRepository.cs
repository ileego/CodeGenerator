using CodeGenerator.Infra.Common.ForTest.Entities;
using CodeGenerator.Infra.Common.Interfaces;

namespace CodeGenerator.Infra.Common.ForTest.Repository
{
    public interface IUserContactRepository : IEfRepository<UserContact, long>
    {
    }
}
