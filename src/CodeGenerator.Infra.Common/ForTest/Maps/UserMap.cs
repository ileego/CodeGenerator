using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Infra.Common.ForTest.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGenerator.Infra.Common.ForTest.Maps
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        /// <summary>
        ///     Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder"> The builder to be used to configure the entity type. </param>
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("tbl_user");
            builder.Property(t => t.UserName).HasColumnName("user_name");
            builder.Property(t => t.Password).HasColumnName("password");
            builder.Property(t => t.CreationTime).HasColumnName("creation_time");
            builder.HasMany(t => t.UserContacts).WithOne(t => t.User);
            base.Configure(builder);
        }
    }
}
