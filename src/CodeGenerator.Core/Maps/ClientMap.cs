using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Maps
{
    public class ClientMap : EntityTypeConfiguration<Client>
    {
        public override void Configure(EntityTypeBuilder<Client> builder)
        {
            base.Configure(builder);

            builder.ToTable("client");

			builder.Property(t => t.ClientNo).HasColumnName("client_no").HasMaxLength(128).IsRequired();
			builder.Property(t => t.ClientName).HasColumnName("client_name").HasMaxLength(200).IsRequired();
			builder.Property(t => t.ClientSecretKey).HasColumnName("client_secret_key").HasMaxLength(256).IsRequired();
			builder.Property(t => t.TokenLifetime).HasColumnName("token_lifetime").IsRequired();
			builder.Property(t => t.RefreshTokenLifetime).HasColumnName("refresh_token_lifetime");
			builder.Property(t => t.Description).HasColumnName("description").HasMaxLength(256);
			builder.Property(t => t.IsEnabled).HasColumnName("is_enabled").IsRequired();

        }
    }
}
