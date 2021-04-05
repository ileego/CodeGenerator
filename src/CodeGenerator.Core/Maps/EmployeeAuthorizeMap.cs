using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Maps
{
    public class EmployeeAuthorizeMap : EntityTypeConfiguration<EmployeeAuthorize>
    {
        public override void Configure(EntityTypeBuilder<EmployeeAuthorize> builder)
        {
            base.Configure(builder);

            builder.ToTable("employee_authorize");

			builder.Property(t => t.ApplicationRoleId).HasColumnName("application_role_id").IsRequired();
			builder.Property(t => t.EmployeeId).HasColumnName("employee_id").IsRequired();

        }
    }
}
