using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;

namespace CodeGenerator.Core.Maps
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);

            builder.ToTable("employee");

			builder.Property(t => t.DepartmentId).HasColumnName("department_id").IsRequired();
			builder.Property(t => t.Account).HasColumnName("account").HasMaxLength(30).IsRequired();
			builder.Property(t => t.Password).HasColumnName("password").HasMaxLength(100).IsRequired();
			builder.Property(t => t.EmployeeName).HasColumnName("employee_name").HasMaxLength(30).IsRequired();
			builder.Property(t => t.Gender).HasColumnName("gender").HasMaxLength(2);
			builder.Property(t => t.Nation).HasColumnName("nation").HasMaxLength(100);
			builder.Property(t => t.BirthDate).HasColumnName("birth_date").HasMaxLength(20);
			builder.Property(t => t.CertificateType).HasColumnName("certificate_type").HasMaxLength(20);
			builder.Property(t => t.CertificateNo).HasColumnName("certificate_no").HasMaxLength(30);
			builder.Property(t => t.CertificateAddress).HasColumnName("certificate_address").HasMaxLength(500);
			builder.Property(t => t.MobileNo).HasColumnName("mobile_no").HasMaxLength(20);
			builder.Property(t => t.ContactNumber).HasColumnName("contact_number").HasMaxLength(50);
			builder.Property(t => t.Weixin).HasColumnName("weixin").HasMaxLength(50);
			builder.Property(t => t.Email).HasColumnName("email").HasMaxLength(50);
			builder.Property(t => t.PostalAddress).HasColumnName("postal_address").HasMaxLength(200);
			builder.Property(t => t.EmergencyContact).HasColumnName("emergency_contact").HasMaxLength(30);
			builder.Property(t => t.EmergencyContactNumber).HasColumnName("emergency_contact_number").HasMaxLength(50);
			builder.Property(t => t.JoiningDate).HasColumnName("joining_date");
			builder.Property(t => t.JobPosition).HasColumnName("job_position").HasMaxLength(50);
			builder.Property(t => t.JobTitle).HasColumnName("job_title").HasMaxLength(50);
			builder.Property(t => t.AdditionalInformation1).HasColumnName("additional_information1").HasMaxLength(50);
			builder.Property(t => t.AdditionalInformation2).HasColumnName("additional_information2").HasMaxLength(50);
			builder.Property(t => t.AdditionalInformation3).HasColumnName("additional_information3").HasMaxLength(50);
			builder.Property(t => t.AdditionalInformation4).HasColumnName("additional_information4").HasMaxLength(50);
			builder.Property(t => t.AdditionalInformation5).HasColumnName("additional_information5").HasMaxLength(300);
			builder.Property(t => t.AdditionalInformation6).HasColumnName("additional_information6").HasMaxLength(300);
			builder.Property(t => t.Status).HasColumnName("status").HasMaxLength(20);

        }
    }
}
