using CodeGenerator.Application.DTOs.ApplicationSystem;
using FluentValidation;

namespace CodeGenerator.Application.Validations
{
    /// <summary>
    /// 配置信息验证
    /// </summary>
    public class ApplicationSystemValidator:AbstractValidator<ApplicationSystemInputDto>
    {
        /// <summary>
        /// 验证配置
        /// </summary>
        public ApplicationSystemValidator()
        {
			RuleFor(t => t.AppNo).NotEmpty().WithMessage("请输入应用编号");
			RuleFor(t => t.AppNo).MaximumLength(128).WithMessage("应用编号长度不能超过128");
			RuleFor(t => t.AppName).NotEmpty().WithMessage("请输入应用名称");
			RuleFor(t => t.AppName).MaximumLength(200).WithMessage("应用名称长度不能超过200");
			RuleFor(t => t.DisplayName).NotEmpty().WithMessage("请输入显示名称");
			RuleFor(t => t.DisplayName).MaximumLength(100).WithMessage("显示名称长度不能超过100");
			RuleFor(t => t.AppUrl).NotEmpty().WithMessage("请输入应用地址");
			RuleFor(t => t.AppUrl).MaximumLength(256).WithMessage("应用地址长度不能超过256");
			RuleFor(t => t.AppIconUrl).NotEmpty().WithMessage("请输入应用图标Url");
			RuleFor(t => t.AppIconUrl).MaximumLength(256).WithMessage("应用图标Url长度不能超过256");
			RuleFor(t => t.AppSecretKey).MaximumLength(256).WithMessage("密钥长度不能超过256");
			RuleFor(t => t.Description).MaximumLength(256).WithMessage("描述长度不能超过256");

        }
    }
}