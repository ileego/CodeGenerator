using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGenerator.Core.Db.Entities
{
    public class TableMap : IEntityTypeConfiguration<Table>
    {
        /// <summary>
        ///     Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder"> The builder to be used to configure the entity type. </param>
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToView("view_tables");
            builder.Property(t => t.TableName).HasColumnName("TABLE_NAME");
            builder.Property(t => t.TableType).HasColumnName("TABLE_TYPE");
            builder.Property(t => t.TableComment).HasColumnName("TABLE_COMMENT");
        }
    }
}
