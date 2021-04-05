using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeGenerator.Application.DTOs.RoleAuthorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;

namespace CodeGenerator.Application.Services.RoleAuthorize
{
    /// <summary>
    /// 角色授权 - 应用服务
    /// </summary>
    public interface IRoleAuthorizeService : IAppService
    {

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppServiceResult<long>> CreateAsync(RoleAuthorizeInputDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppServiceResult> UpdateAsync(long id,RoleAuthorizeInputDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppServiceResult> DeleteAsync(long id);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppServiceResult<RoleAuthorizeDto>> GetByIdAsync(long id);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        Task<AppServiceResult<PagedResult<RoleAuthorizeDto>>> QueryListAsync(RoleAuthorizeParamsDto queryParams);
    }
}