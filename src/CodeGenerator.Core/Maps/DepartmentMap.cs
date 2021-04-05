using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Maps
{
    public class DepartmentMap : EntityTypeConfiguration<Department>
    {
        public override void Configure(EntityTypeBuilder<Department> builder)
        {
            base.Configure(builder);

            builder.ToTable("department");

			builder.Property(t => t.DepartmentId).HasColumnName("department_id");
			builder.Property(t => t.OrganizationId).HasColumnName("organization_id").IsRequired();
			builder.Property(t => t.DepartmentName).HasColumnName("department_name").HasMaxLength(100);
			builder.Property(t => t.DepartmentHead).HasColumnName("department_head").HasMaxLength(30);
			builder.Property(t => t.OrdinalPosition).HasColumnName("ordinal_position");
			builder.Property(t => t.IsEnabled).HasColumnName("is_enabled").IsRequired();
			builder.Property(t => t.CurrentLevel).HasColumnName("current_level").IsRequired();

        }
    }
}
