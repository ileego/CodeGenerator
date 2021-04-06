using CodeGenerator.Application.DTOs.Client;
using FluentValidation;

namespace CodeGenerator.Application.Validations
{
    /// <summary>
    /// 配置信息验证
    /// </summary>
    public class ClientValidator:AbstractValidator<ClientInputDto>
    {
        /// <summary>
        /// 验证配置
        /// </summary>
        public ClientValidator()
        {
			RuleFor(t => t.ClientNo).NotEmpty().WithMessage("请输入客户端编号");
			RuleFor(t => t.ClientNo).MaximumLength(128).WithMessage("客户端编号长度不能超过128");
			RuleFor(t => t.ClientName).NotEmpty().WithMessage("请输入客户端名称");
			RuleFor(t => t.ClientName).MaximumLength(200).WithMessage("客户端名称长度不能超过200");
			RuleFor(t => t.ClientSecretKey).NotEmpty().WithMessage("请输入客户端密钥");
			RuleFor(t => t.ClientSecretKey).MaximumLength(256).WithMessage("客户端密钥长度不能超过256");
			RuleFor(t => t.TokenLifetime).NotEqual(t => default).WithMessage("Token有效期（分钟）必须填写");
			RuleFor(t => t.Description).MaximumLength(256).WithMessage("描述长度不能超过256");

        }
    }
}