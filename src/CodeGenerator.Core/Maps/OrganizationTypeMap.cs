using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Maps
{
    public class OrganizationTypeMap : EntityTypeConfiguration<OrganizationType>
    {
        public override void Configure(EntityTypeBuilder<OrganizationType> builder)
        {
            base.Configure(builder);

            builder.ToTable("organization_type");

			builder.Property(t => t.TypeName).HasColumnName("type_name").HasMaxLength(100).IsRequired();

        }
    }
}
