using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Maps
{
    public class ApplicationRoleMap : EntityTypeConfiguration<ApplicationRole>
    {
        public override void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            base.Configure(builder);

            builder.ToTable("application_role");

			builder.Property(t => t.ApplicationSystemId).HasColumnName("application_system_id").IsRequired();
			builder.Property(t => t.RoleCode).HasColumnName("role_code").HasMaxLength(50).IsRequired();
			builder.Property(t => t.RoleName).HasColumnName("role_name").HasMaxLength(100).IsRequired();
			builder.Property(t => t.Description).HasColumnName("description").HasMaxLength(256);
			builder.Property(t => t.IsEnabled).HasColumnName("is_enabled").IsRequired();

        }
    }
}
