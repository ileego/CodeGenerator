using CodeGenerator.Application.DTOs.ApiResource;
using FluentValidation;

namespace CodeGenerator.Application.Validations
{
    /// <summary>
    /// 配置信息验证
    /// </summary>
    public class ApiResourceValidator:AbstractValidator<ApiResourceInputDto>
    {
        /// <summary>
        /// 验证配置
        /// </summary>
        public ApiResourceValidator()
        {
            CascadeMode = CascadeMode.Stop;
			RuleFor(t => t.ApiKey).NotEmpty().WithMessage("请输入ApiKey");
			RuleFor(t => t.ApiKey).MaximumLength(128).WithMessage("ApiKey长度不能超过128");
			RuleFor(t => t.ApiName).NotEmpty().WithMessage("请输入Api名称");
			RuleFor(t => t.ApiName).MaximumLength(200).WithMessage("Api名称长度不能超过200");
			RuleFor(t => t.DisplayName).MaximumLength(100).WithMessage("显示名称长度不能超过100");
			RuleFor(t => t.ApiDocumentUrl).MaximumLength(256).WithMessage("Api文档Url长度不能超过256");
			RuleFor(t => t.Description).MaximumLength(256).WithMessage("描述长度不能超过256");

        }
    }
}