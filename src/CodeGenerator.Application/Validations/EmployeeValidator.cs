using CodeGenerator.Application.DTOs.Employee;
using FluentValidation;

namespace CodeGenerator.Application.Validations
{
    /// <summary>
    /// 配置信息验证
    /// </summary>
    public class EmployeeValidator:AbstractValidator<EmployeeInputDto>
    {
        /// <summary>
        /// 验证配置
        /// </summary>
        public EmployeeValidator()
        {
			RuleFor(t => t.DepartmentId).NotEqual(t => default).WithMessage("所属部门主键必须填写");
			RuleFor(t => t.Account).NotEmpty().WithMessage("请输入账号");
			RuleFor(t => t.Account).MaximumLength(30).WithMessage("账号长度不能超过30");
			RuleFor(t => t.Password).NotEmpty().WithMessage("请输入密码");
			RuleFor(t => t.Password).MaximumLength(100).WithMessage("密码长度不能超过100");
			RuleFor(t => t.EmployeeName).NotEmpty().WithMessage("请输入姓名");
			RuleFor(t => t.EmployeeName).MaximumLength(30).WithMessage("姓名长度不能超过30");
			RuleFor(t => t.Gender).MaximumLength(2).WithMessage("性别长度不能超过2");
			RuleFor(t => t.Nation).MaximumLength(100).WithMessage("民族长度不能超过100");
			RuleFor(t => t.BirthDate).MaximumLength(20).WithMessage("出生日期长度不能超过20");
			RuleFor(t => t.CertificateType).MaximumLength(20).WithMessage("证件类型长度不能超过20");
			RuleFor(t => t.CertificateNo).MaximumLength(30).WithMessage("证件号码长度不能超过30");
			RuleFor(t => t.CertificateAddress).MaximumLength(500).WithMessage("证件地址长度不能超过500");
			RuleFor(t => t.MobileNo).MaximumLength(20).WithMessage("手机号长度不能超过20");
			RuleFor(t => t.ContactNumber).MaximumLength(50).WithMessage("联系电话长度不能超过50");
			RuleFor(t => t.Weixin).MaximumLength(50).WithMessage("微信长度不能超过50");
			RuleFor(t => t.Email).MaximumLength(50).WithMessage("电邮长度不能超过50");
			RuleFor(t => t.PostalAddress).MaximumLength(200).WithMessage("通讯地址长度不能超过200");
			RuleFor(t => t.EmergencyContact).MaximumLength(30).WithMessage("紧急联系人长度不能超过30");
			RuleFor(t => t.EmergencyContactNumber).MaximumLength(50).WithMessage("紧急联系人电话长度不能超过50");
			RuleFor(t => t.JobPosition).MaximumLength(50).WithMessage("工作岗位长度不能超过50");
			RuleFor(t => t.JobTitle).MaximumLength(50).WithMessage("工作职务长度不能超过50");
			RuleFor(t => t.AdditionalInformation1).MaximumLength(50).WithMessage("附加信息1长度不能超过50");
			RuleFor(t => t.AdditionalInformation2).MaximumLength(50).WithMessage("附加信息2长度不能超过50");
			RuleFor(t => t.AdditionalInformation3).MaximumLength(50).WithMessage("附加信息3长度不能超过50");
			RuleFor(t => t.AdditionalInformation4).MaximumLength(50).WithMessage("附加信息4长度不能超过50");
			RuleFor(t => t.AdditionalInformation5).MaximumLength(300).WithMessage("附加信息5长度不能超过300");
			RuleFor(t => t.AdditionalInformation6).MaximumLength(300).WithMessage("附加信息6长度不能超过300");
			RuleFor(t => t.Status).MaximumLength(20).WithMessage("状态:正常、停用长度不能超过20");

        }
    }
}