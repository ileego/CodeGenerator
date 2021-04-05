using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Maps
{
    public class ApplicationActionMap : EntityTypeConfiguration<ApplicationAction>
    {
        public override void Configure(EntityTypeBuilder<ApplicationAction> builder)
        {
            base.Configure(builder);

            builder.ToTable("application_action");

			builder.Property(t => t.GroupTag).HasColumnName("group_tag").HasMaxLength(50).IsRequired();
			builder.Property(t => t.GroupName).HasColumnName("group_name").HasMaxLength(100).IsRequired();
			builder.Property(t => t.ActionTag).HasColumnName("action_tag").HasMaxLength(50).IsRequired();
			builder.Property(t => t.ActionName).HasColumnName("action_name").HasMaxLength(100).IsRequired();
			builder.Property(t => t.Description).HasColumnName("description").HasMaxLength(256);
			builder.Property(t => t.OrdinalPosition).HasColumnName("ordinal_position").IsRequired();
			builder.Property(t => t.IsEnabled).HasColumnName("is_enabled").IsRequired();

        }
    }
}
