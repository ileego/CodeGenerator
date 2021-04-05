using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Maps
{
    public class ClientsAuthorizeMap : EntityTypeConfiguration<ClientsAuthorize>
    {
        public override void Configure(EntityTypeBuilder<ClientsAuthorize> builder)
        {
            base.Configure(builder);

            builder.ToTable("clients_authorize");

			builder.Property(t => t.ApiResourceId).HasColumnName("api_resource_id").IsRequired();
			builder.Property(t => t.ClientId).HasColumnName("client_id").IsRequired();

        }
    }
}
