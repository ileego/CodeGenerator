using CodeGenerator.Application.DTOs.DataAccessAuthorize;
using FluentValidation;

namespace CodeGenerator.Application.Validations
{
    /// <summary>
    /// 配置信息验证
    /// </summary>
    public class DataAccessAuthorizeValidator:AbstractValidator<DataAccessAuthorizeInputDto>
    {
        /// <summary>
        /// 验证配置
        /// </summary>
        public DataAccessAuthorizeValidator()
        {
			RuleFor(t => t.ApplicationRoleId).NotEqual(t => default).WithMessage("角色主键必须填写");
			RuleFor(t => t.DistrictId).NotEqual(t => default).WithMessage("行政区主键必须填写");

        }
    }
}