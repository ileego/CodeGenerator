using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeGenerator.Application.DTOs.ApiResource;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;

namespace CodeGenerator.Application.Services.ApiResource
{
    /// <summary>
    /// Api资源 - 应用服务
    /// </summary>
    public interface IApiResourceService : IAppService
    {

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppServiceResult<long>> CreateAsync(ApiResourceInputDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppServiceResult> UpdateAsync(long id,ApiResourceInputDto input);

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
        Task<AppServiceResult<ApiResourceDto>> GetByIdAsync(long id);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        Task<AppServiceResult<PagedResult<ApiResourceDto>>> QueryListAsync(ApiResourceParamsDto queryParams);
    }
}