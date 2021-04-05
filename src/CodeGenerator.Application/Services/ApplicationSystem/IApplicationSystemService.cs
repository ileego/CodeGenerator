using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeGenerator.Application.DTOs.ApplicationSystem;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;

namespace CodeGenerator.Application.Services.ApplicationSystem
{
    /// <summary>
    /// 应用系统 - 应用服务
    /// </summary>
    public interface IApplicationSystemService : IAppService
    {

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppServiceResult<long>> CreateAsync(ApplicationSystemInputDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppServiceResult> UpdateAsync(long id,ApplicationSystemInputDto input);

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
        Task<AppServiceResult<ApplicationSystemDto>> GetByIdAsync(long id);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        Task<AppServiceResult<PagedResult<ApplicationSystemDto>>> QueryListAsync(ApplicationSystemParamsDto queryParams);
    }
}