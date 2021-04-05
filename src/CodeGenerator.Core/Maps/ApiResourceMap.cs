using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Maps
{
    public class ApiResourceMap : EntityTypeConfiguration<ApiResource>
    {
        public override void Configure(EntityTypeBuilder<ApiResource> builder)
        {
            base.Configure(builder);

            builder.ToTable("api_resource");

			builder.Property(t => t.ApiKey).HasColumnName("api_key").HasMaxLength(128).IsRequired();
			builder.Property(t => t.ApiName).HasColumnName("api_name").HasMaxLength(200).IsRequired();
			builder.Property(t => t.DisplayName).HasColumnName("display_name").HasMaxLength(100);
			builder.Property(t => t.ApiDocumentUrl).HasColumnName("api_document_url").HasMaxLength(256);
			builder.Property(t => t.Description).HasColumnName("description").HasMaxLength(256);
			builder.Property(t => t.IsEnabled).HasColumnName("is_enabled").IsRequired();

        }
    }
}
