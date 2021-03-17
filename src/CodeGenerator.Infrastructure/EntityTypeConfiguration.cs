using CodeGenerator.Infrastructure.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGenerator.Infrastructure
{
    public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
       where TEntity : EfEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var entityType = typeof(TEntity);

            builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id).ValueGeneratedNever(); //不使用数据库自动生成

            if (typeof(IConcurrency).IsAssignableFrom(entityType))
            {
                builder.Property("RowVersion").IsRequired().IsRowVersion().ValueGeneratedOnAddOrUpdate();
            }

            if (typeof(ISoftDelete<>).IsAssignableFrom(entityType))
            {
                builder.Property("IsDeleted")
                       .HasDefaultValue(false);
                builder.HasQueryFilter(d => EF.Property<bool>(d, "IsDeleted") == false);
            }
        }
    }
}
