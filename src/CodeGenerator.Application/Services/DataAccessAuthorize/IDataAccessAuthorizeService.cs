using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeGenerator.Application.DTOs.DataAccessAuthorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;

namespace CodeGenerator.Application.Services.DataAccessAuthorize
{
    /// <summary>
    /// 数据访问授权 - 应用服务
    /// </summary>
    public interface IDataAccessAuthorizeService : IAppService
    {

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppServiceResult<long>> CreateAsync(DataAccessAuthorizeInputDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppServiceResult> UpdateAsync(long id,DataAccessAuthorizeInputDto input);

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
        Task<AppServiceResult<DataAccessAuthorizeDto>> GetByIdAsync(long id);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        Task<AppServiceResult<PagedResult<DataAccessAuthorizeDto>>> QueryListAsync(DataAccessAuthorizeParamsDto queryParams);
    }
}