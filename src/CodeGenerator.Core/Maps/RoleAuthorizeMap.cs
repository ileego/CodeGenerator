using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Maps
{
    public class RoleAuthorizeMap : EntityTypeConfiguration<RoleAuthorize>
    {
        public override void Configure(EntityTypeBuilder<RoleAuthorize> builder)
        {
            base.Configure(builder);

            builder.ToTable("role_authorize");

			builder.Property(t => t.ApplicationRoleId).HasColumnName("application_role_id").IsRequired();
			builder.Property(t => t.ApplicationMenuId).HasColumnName("application_menu_id").IsRequired();
			builder.Property(t => t.AvailableActions).HasColumnName("available_actions").HasMaxLength(2000);

        }
    }
}
