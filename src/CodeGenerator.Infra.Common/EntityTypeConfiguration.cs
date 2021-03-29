using CodeGenerator.Infra.Common.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGenerator.Infra.Common
{
    public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
       where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var entityType = typeof(TEntity);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever(); //不使用数据库自动生成

            if (typeof(IConcurrency).IsAssignableFrom(entityType))
            {
                builder.Property("RowVersion")
                    .HasColumnName("row_version") //配置数据库字段名称
                    .IsRequired()
                    .IsRowVersion()
                    .ValueGeneratedOnAddOrUpdate();
            }

            if (typeof(ICreationAuditEntity<>).IsAssignableFrom(entityType))
            {
                builder.Property("Creator").HasColumnName("creator");
                builder.Property("CreationTime").HasColumnName("creation_time");
            }

            if (typeof(IModifyAuditEntity<>).IsAssignableFrom(entityType))
            {
                builder.Property("LastModifier").HasColumnName("last_modifier");
                builder.Property("LastModificationTime").HasColumnName("last_modification_time");
            }

            if (typeof(ISoftDelete<>).IsAssignableFrom(entityType))
            {
                builder.Property("IsDeleted").HasColumnName("is_deleted").HasDefaultValue(false);
                builder.Property("Deleter").HasColumnName("deleter");
                builder.Property("DeletionTime").HasColumnName("deletion_time");
                builder.HasQueryFilter(d => EF.Property<bool>(d, "IsDeleted") == false);
            }

        }
    }
}
