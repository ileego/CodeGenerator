using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Maps
{
    public class DataAccessAuthorizeMap : EntityTypeConfiguration<DataAccessAuthorize>
    {
        public override void Configure(EntityTypeBuilder<DataAccessAuthorize> builder)
        {
            base.Configure(builder);

            builder.ToTable("data_access_authorize");

			builder.Property(t => t.ApplicationRoleId).HasColumnName("application_role_id").IsRequired();
			builder.Property(t => t.DistrictId).HasColumnName("district_id").IsRequired();

        }
    }
}
