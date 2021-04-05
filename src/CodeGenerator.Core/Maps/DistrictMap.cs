using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Maps
{
    public class DistrictMap : EntityTypeConfiguration<District>
    {
        public override void Configure(EntityTypeBuilder<District> builder)
        {
            base.Configure(builder);

            builder.ToTable("district");

			builder.Property(t => t.Code).HasColumnName("code").HasMaxLength(10);
			builder.Property(t => t.Name).HasColumnName("name").HasMaxLength(100);

        }
    }
}
