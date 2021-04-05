using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Maps
{
    public class ApplicationMenuMap : EntityTypeConfiguration<ApplicationMenu>
    {
        public override void Configure(EntityTypeBuilder<ApplicationMenu> builder)
        {
            base.Configure(builder);

            builder.ToTable("application_menu");

			builder.Property(t => t.ApplicationMenuId).HasColumnName("application_menu_id");
			builder.Property(t => t.ApplicationSystemId).HasColumnName("application_system_id").IsRequired();
			builder.Property(t => t.MenuCode).HasColumnName("menu_code").HasMaxLength(50).IsRequired();
			builder.Property(t => t.MenuName).HasColumnName("menu_name").HasMaxLength(100).IsRequired();
			builder.Property(t => t.MenuUrl).HasColumnName("menu_url").HasMaxLength(256);
			builder.Property(t => t.MenuIcon).HasColumnName("menu_icon").HasMaxLength(256);
			builder.Property(t => t.Description).HasColumnName("description").HasMaxLength(256);
			builder.Property(t => t.OrdinalPosition).HasColumnName("ordinal_position").IsRequired();
			builder.Property(t => t.IsEnabled).HasColumnName("is_enabled").IsRequired();
			builder.Property(t => t.CurrentLevel).HasColumnName("current_level").IsRequired();

        }
    }
}
