using CodeGenerator.Application.DTOs.Department;
using FluentValidation;

namespace CodeGenerator.Application.Validations
{
    /// <summary>
    /// 配置信息验证
    /// </summary>
    public class DepartmentValidator:AbstractValidator<DepartmentInputDto>
    {
        /// <summary>
        /// 验证配置
        /// </summary>
        public DepartmentValidator()
        {
            CascadeMode = CascadeMode.Stop;
			RuleFor(t => t.OrganizationId).NotEqual(t => default).WithMessage("所属组织主键必须填写");
			RuleFor(t => t.DepartmentName).MaximumLength(100).WithMessage("部门名称长度不能超过100");
			RuleFor(t => t.DepartmentHead).MaximumLength(30).WithMessage("部门主管长度不能超过30");
			RuleFor(t => t.CurrentLevel).NotEqual(t => default).WithMessage("当前层级必须填写");

        }
    }
}