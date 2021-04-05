using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeGenerator.Application.DTOs.District;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;

namespace CodeGenerator.Application.Services.District
{
    /// <summary>
    /// 行政区 - 应用服务
    /// </summary>
    public interface IDistrictService : IAppService
    {

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppServiceResult<long>> CreateAsync(DistrictInputDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppServiceResult> UpdateAsync(long id,DistrictInputDto input);

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
        Task<AppServiceResult<DistrictDto>> GetByIdAsync(long id);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        Task<AppServiceResult<PagedResult<DistrictDto>>> QueryListAsync(DistrictParamsDto queryParams);
    }
}