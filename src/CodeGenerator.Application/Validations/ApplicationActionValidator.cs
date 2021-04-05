using CodeGenerator.Application.DTOs.ApplicationAction;
using FluentValidation;

namespace CodeGenerator.Application.Validations
{
    /// <summary>
    /// 配置信息验证
    /// </summary>
    public class ApplicationActionValidator:AbstractValidator<ApplicationActionInputDto>
    {
        /// <summary>
        /// 验证配置
        /// </summary>
        public ApplicationActionValidator()
        {
            CascadeMode = CascadeMode.Stop;
			RuleFor(t => t.GroupTag).NotEmpty().WithMessage("请输入功能组标识");
			RuleFor(t => t.GroupTag).MaximumLength(50).WithMessage("功能组标识长度不能超过50");
			RuleFor(t => t.GroupName).NotEmpty().WithMessage("请输入功能组名称");
			RuleFor(t => t.GroupName).MaximumLength(100).WithMessage("功能组名称长度不能超过100");
			RuleFor(t => t.ActionTag).NotEmpty().WithMessage("请输入功能标识");
			RuleFor(t => t.ActionTag).MaximumLength(50).WithMessage("功能标识长度不能超过50");
			RuleFor(t => t.ActionName).NotEmpty().WithMessage("请输入功能名称");
			RuleFor(t => t.ActionName).MaximumLength(100).WithMessage("功能名称长度不能超过100");
			RuleFor(t => t.Description).MaximumLength(256).WithMessage("描述长度不能超过256");
			RuleFor(t => t.OrdinalPosition).NotEqual(t => default).WithMessage("定序位置必须填写");

        }
    }
}