using CodeGenerator.Application.DTOs.ApplicationMenu;
using FluentValidation;

namespace CodeGenerator.Application.Validations
{
    /// <summary>
    /// 配置信息验证
    /// </summary>
    public class ApplicationMenuValidator:AbstractValidator<ApplicationMenuInputDto>
    {
        /// <summary>
        /// 验证配置
        /// </summary>
        public ApplicationMenuValidator()
        {
			RuleFor(t => t.ApplicationSystemId).NotEqual(t => default).WithMessage("所属应用必须填写");
			RuleFor(t => t.MenuCode).NotEmpty().WithMessage("请输入菜单代码");
			RuleFor(t => t.MenuCode).MaximumLength(50).WithMessage("菜单代码长度不能超过50");
			RuleFor(t => t.MenuName).NotEmpty().WithMessage("请输入菜单名称");
			RuleFor(t => t.MenuName).MaximumLength(100).WithMessage("菜单名称长度不能超过100");
			RuleFor(t => t.MenuUrl).MaximumLength(256).WithMessage("菜单Url长度不能超过256");
			RuleFor(t => t.MenuIcon).MaximumLength(256).WithMessage("菜单图标长度不能超过256");
			RuleFor(t => t.Description).MaximumLength(256).WithMessage("描述长度不能超过256");
			RuleFor(t => t.OrdinalPosition).NotEqual(t => default).WithMessage("定序位置必须填写");
			RuleFor(t => t.CurrentLevel).NotEqual(t => default).WithMessage("当前层级必须填写");

        }
    }
}