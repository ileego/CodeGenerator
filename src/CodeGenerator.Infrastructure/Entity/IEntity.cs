using System.ComponentModel.DataAnnotations;

namespace CodeGenerator.Infrastructure.Entity
{
    public interface IEntity
    {
    }

    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
    }
}
