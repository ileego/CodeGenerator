using CodeGenerator.Application.DTOs.District;
using FluentValidation;

namespace CodeGenerator.Application.Validations
{
    /// <summary>
    /// 配置信息验证
    /// </summary>
    public class DistrictValidator:AbstractValidator<DistrictInputDto>
    {
        /// <summary>
        /// 验证配置
        /// </summary>
        public DistrictValidator()
        {
			RuleFor(t => t.Code).MaximumLength(10).WithMessage("代码长度不能超过10");
			RuleFor(t => t.Name).MaximumLength(100).WithMessage("名称长度不能超过100");

        }
    }
}