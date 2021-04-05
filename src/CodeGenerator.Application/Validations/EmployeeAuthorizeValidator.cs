using CodeGenerator.Application.DTOs.EmployeeAuthorize;
using FluentValidation;

namespace CodeGenerator.Application.Validations
{
    /// <summary>
    /// 配置信息验证
    /// </summary>
    public class EmployeeAuthorizeValidator:AbstractValidator<EmployeeAuthorizeInputDto>
    {
        /// <summary>
        /// 验证配置
        /// </summary>
        public EmployeeAuthorizeValidator()
        {
            CascadeMode = CascadeMode.Stop;
			RuleFor(t => t.ApplicationRoleId).NotEqual(t => default).WithMessage("角色主键必须填写");
			RuleFor(t => t.EmployeeId).NotEqual(t => default).WithMessage("工作人员主键必须填写");

        }
    }
}