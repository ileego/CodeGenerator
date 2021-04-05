using CodeGenerator.Application.DTOs.Organization;
using FluentValidation;

namespace CodeGenerator.Application.Validations
{
    /// <summary>
    /// 配置信息验证
    /// </summary>
    public class OrganizationValidator:AbstractValidator<OrganizationInputDto>
    {
        /// <summary>
        /// 验证配置
        /// </summary>
        public OrganizationValidator()
        {
            CascadeMode = CascadeMode.Stop;
			RuleFor(t => t.DistrictiD).NotEqual(t => default).WithMessage("行政区主键必须填写");
			RuleFor(t => t.OrganizationTypeId).NotEqual(t => default).WithMessage("组织类型主键必须填写");
			RuleFor(t => t.UnifiedSocialCreditCode).NotEmpty().WithMessage("请输入统一社会信用代码 ");
			RuleFor(t => t.UnifiedSocialCreditCode).MaximumLength(30).WithMessage("统一社会信用代码 长度不能超过30");
			RuleFor(t => t.OrganizationName).NotEmpty().WithMessage("请输入名称");
			RuleFor(t => t.OrganizationName).MaximumLength(200).WithMessage("名称长度不能超过200");
			RuleFor(t => t.LegalPerson).MaximumLength(20).WithMessage("法人长度不能超过20");
			RuleFor(t => t.RegisteredAddress).MaximumLength(500).WithMessage("注册地址长度不能超过500");
			RuleFor(t => t.Contact).NotEmpty().WithMessage("请输入联系人");
			RuleFor(t => t.Contact).MaximumLength(50).WithMessage("联系人长度不能超过50");
			RuleFor(t => t.ContactNumber).MaximumLength(100).WithMessage("联系电话长度不能超过100");
			RuleFor(t => t.ContactAddress).MaximumLength(500).WithMessage("联系地址长度不能超过500");
			RuleFor(t => t.OrdinalPosition).NotEqual(t => default).WithMessage("定序位置必须填写");
			RuleFor(t => t.Status).NotEmpty().WithMessage("请输入状态:正常、停用");
			RuleFor(t => t.Status).MaximumLength(20).WithMessage("状态:正常、停用长度不能超过20");
			RuleFor(t => t.Remarks).MaximumLength(256).WithMessage("备注长度不能超过256");
			RuleFor(t => t.CurrentLevel).NotEqual(t => default).WithMessage("当前层级必须填写");

        }
    }
}