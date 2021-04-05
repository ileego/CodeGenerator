using CodeGenerator.Application.DTOs.ApplicationMenuAction;
using FluentValidation;

namespace CodeGenerator.Application.Validations
{
    /// <summary>
    /// 配置信息验证
    /// </summary>
    public class ApplicationMenuActionValidator:AbstractValidator<ApplicationMenuActionInputDto>
    {
        /// <summary>
        /// 验证配置
        /// </summary>
        public ApplicationMenuActionValidator()
        {
            CascadeMode = CascadeMode.Stop;
			RuleFor(t => t.ApplicationMenuId).NotEqual(t => default).WithMessage("应用菜单主键必须填写");
			RuleFor(t => t.ApplicationActionId).NotEqual(t => default).WithMessage("应用功能主键必须填写");

        }
    }
}