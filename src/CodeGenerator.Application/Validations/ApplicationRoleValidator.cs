using CodeGenerator.Application.DTOs.ApplicationRole;
using FluentValidation;

namespace CodeGenerator.Application.Validations
{
    /// <summary>
    /// 配置信息验证
    /// </summary>
    public class ApplicationRoleValidator:AbstractValidator<ApplicationRoleInputDto>
    {
        /// <summary>
        /// 验证配置
        /// </summary>
        public ApplicationRoleValidator()
        {
            CascadeMode = CascadeMode.Stop;
			RuleFor(t => t.ApplicationSystemId).NotEqual(t => default).WithMessage("所属应用主键必须填写");
			RuleFor(t => t.RoleCode).NotEmpty().WithMessage("请输入角色代码");
			RuleFor(t => t.RoleCode).MaximumLength(50).WithMessage("角色代码长度不能超过50");
			RuleFor(t => t.RoleName).NotEmpty().WithMessage("请输入角色名称");
			RuleFor(t => t.RoleName).MaximumLength(100).WithMessage("角色名称长度不能超过100");
			RuleFor(t => t.Description).MaximumLength(256).WithMessage("描述长度不能超过256");

        }
    }
}