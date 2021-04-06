using CodeGenerator.Application.DTOs.RoleAuthorize;
using FluentValidation;

namespace CodeGenerator.Application.Validations
{
    /// <summary>
    /// 配置信息验证
    /// </summary>
    public class RoleAuthorizeValidator:AbstractValidator<RoleAuthorizeInputDto>
    {
        /// <summary>
        /// 验证配置
        /// </summary>
        public RoleAuthorizeValidator()
        {
			RuleFor(t => t.ApplicationRoleId).NotEqual(t => default).WithMessage("角色主键必须填写");
			RuleFor(t => t.ApplicationMenuId).NotEqual(t => default).WithMessage("应用菜单主键必须填写");
			RuleFor(t => t.AvailableActions).MaximumLength(2000).WithMessage("可用功能：以json数组保存长度不能超过2000");

        }
    }
}