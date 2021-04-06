using CodeGenerator.Application.DTOs.ClientsAuthorize;
using FluentValidation;

namespace CodeGenerator.Application.Validations
{
    /// <summary>
    /// 配置信息验证
    /// </summary>
    public class ClientsAuthorizeValidator:AbstractValidator<ClientsAuthorizeInputDto>
    {
        /// <summary>
        /// 验证配置
        /// </summary>
        public ClientsAuthorizeValidator()
        {
			RuleFor(t => t.ApiResourceId).NotEqual(t => default).WithMessage("Api资源主键必须填写");
			RuleFor(t => t.ClientId).NotEqual(t => default).WithMessage("客户端主键必须填写");

        }
    }
}