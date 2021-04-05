using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Maps
{
    public class OrganizationMap : EntityTypeConfiguration<Organization>
    {
        public override void Configure(EntityTypeBuilder<Organization> builder)
        {
            base.Configure(builder);

            builder.ToTable("organization");

			builder.Property(t => t.DistrictiD).HasColumnName("districti_d").IsRequired();
			builder.Property(t => t.OrganizationId).HasColumnName("organization_id");
			builder.Property(t => t.OrganizationTypeId).HasColumnName("organization_type_id").IsRequired();
			builder.Property(t => t.UnifiedSocialCreditCode).HasColumnName("unified_social_credit_code").HasMaxLength(30).IsRequired();
			builder.Property(t => t.OrganizationName).HasColumnName("organization_name").HasMaxLength(200).IsRequired();
			builder.Property(t => t.LegalPerson).HasColumnName("legal_person").HasMaxLength(20);
			builder.Property(t => t.RegisteredAddress).HasColumnName("registered_address").HasMaxLength(500);
			builder.Property(t => t.Contact).HasColumnName("contact").HasMaxLength(50).IsRequired();
			builder.Property(t => t.ContactNumber).HasColumnName("contact_number").HasMaxLength(100);
			builder.Property(t => t.ContactAddress).HasColumnName("contact_address").HasMaxLength(500);
			builder.Property(t => t.OrdinalPosition).HasColumnName("ordinal_position").IsRequired();
			builder.Property(t => t.Status).HasColumnName("status").HasMaxLength(20).IsRequired();
			builder.Property(t => t.Remarks).HasColumnName("remarks").HasMaxLength(256);
			builder.Property(t => t.CurrentLevel).HasColumnName("current_level").IsRequired();

        }
    }
}
