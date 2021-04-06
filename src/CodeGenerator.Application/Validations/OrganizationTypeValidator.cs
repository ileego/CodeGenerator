using CodeGenerator.Application.DTOs.OrganizationType;
using FluentValidation;

namespace CodeGenerator.Application.Validations
{
    /// <summary>
    /// 配置信息验证
    /// </summary>
    public class OrganizationTypeValidator:AbstractValidator<OrganizationTypeInputDto>
    {
        /// <summary>
        /// 验证配置
        /// </summary>
        public OrganizationTypeValidator()
        {
			RuleFor(t => t.TypeName).NotEmpty().WithMessage("请输入类型名称");
			RuleFor(t => t.TypeName).MaximumLength(100).WithMessage("类型名称长度不能超过100");

        }
    }
}