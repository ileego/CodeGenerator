using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Maps
{
    public class ApplicationSystemMap : EntityTypeConfiguration<ApplicationSystem>
    {
        public override void Configure(EntityTypeBuilder<ApplicationSystem> builder)
        {
            base.Configure(builder);

            builder.ToTable("application_system");

			builder.Property(t => t.AppNo).HasColumnName("app_no").HasMaxLength(128).IsRequired();
			builder.Property(t => t.AppName).HasColumnName("app_name").HasMaxLength(200).IsRequired();
			builder.Property(t => t.DisplayName).HasColumnName("display_name").HasMaxLength(100).IsRequired();
			builder.Property(t => t.AppUrl).HasColumnName("app_url").HasMaxLength(256).IsRequired();
			builder.Property(t => t.AppIconUrl).HasColumnName("app_icon_url").HasMaxLength(256).IsRequired();
			builder.Property(t => t.AppSecretKey).HasColumnName("app_secret_key").HasMaxLength(256);
			builder.Property(t => t.Description).HasColumnName("description").HasMaxLength(256);
			builder.Property(t => t.IsEnabled).HasColumnName("is_enabled").IsRequired();

        }
    }
}
