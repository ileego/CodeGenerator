using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Maps
{
    public class ApplicationMenuActionMap : EntityTypeConfiguration<ApplicationMenuAction>
    {
        public override void Configure(EntityTypeBuilder<ApplicationMenuAction> builder)
        {
            base.Configure(builder);

            builder.ToTable("application_menu_action");

			builder.Property(t => t.ApplicationMenuId).HasColumnName("application_menu_id").IsRequired();
			builder.Property(t => t.ApplicationActionId).HasColumnName("application_action_id").IsRequired();

        }
    }
}
