using CodeGenerator.Core.ForTest.Entities;
using CodeGenerator.Infra.Common;
using CodeGenerator.Infra.Common.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGenerator.Core.ForTest.Maps
{
    public class UserContactMap : EntityTypeConfiguration<UserContact>
    {
        /// <summary>
        ///     Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder"> The builder to be used to configure the entity type. </param>
        public override void Configure(EntityTypeBuilder<UserContact> builder)
        {
            builder.ToTable("tbl_user_contact");
            builder.Property(t => t.UserId).HasColumnName("user_id");
            builder.Property(t => t.ContactAddress).HasColumnName("contact_address");
            builder.Property(t => t.ContactTelephone).HasColumnName("contact_telephone");
            builder.Property(t => t.Geometry).HasColumnName("address_geometry");
            base.Configure(builder);
        }
    }
}
