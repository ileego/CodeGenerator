using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CodeGenerator.Application.DTOs.DataAccessAuthorize;
using CodeGenerator.Application.Services.DataAccessAuthorize;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;


namespace CodeGenerator.WebApi.Controllers
{
    /// <summary>
    /// 数据访问授权 - 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DataAccessAuthorizeController : ControllerBase
    {
        private readonly IDataAccessAuthorizeService _dataAccessAuthorizeService;

        /// <summary>
        /// Ctor
        /// </summary>
        public DataAccessAuthorizeController(IDataAccessAuthorizeService dataAccessAuthorizeService)
        {
            this._dataAccessAuthorizeService = dataAccessAuthorizeService;
        }

        /// <summary>
        /// 创建 - 数据访问授权
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Permission("数据访问授权管理", "创建")]
        public async Task<ActionResult<AppServiceResult<long>>> CreateAsync(
            [FromBody] DataAccessAuthorizeInputDto inputDto)
        {
            return await _dataAccessAuthorizeService.CreateAsync(inputDto);
        }

        /// <summary>
        /// 修改 - 数据访问授权
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        [Permission("数据访问授权管理", "修改")]
        public async Task<ActionResult<AppServiceResult>> UpdateAsync([FromRoute] long id,
            [FromBody] DataAccessAuthorizeInputDto inputDto)
        {
            return await _dataAccessAuthorizeService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 删除 - 数据访问授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}")]
        [Permission("数据访问授权管理", "删除")]
        public async Task<ActionResult<AppServiceResult>> DeleteAsync([FromRoute] long id)
        {
            return await _dataAccessAuthorizeService.DeleteAsync(id);
        }

        /// <summary>
        /// 按Id查询 - 数据访问授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Permission("数据访问授权管理", "按Id查明细")]
        public async Task<ActionResult<AppServiceResult<DataAccessAuthorizeDto>>> GetByIdAsync([FromRoute] long id)
        {
            return await _dataAccessAuthorizeService.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询 - 数据访问授权
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("queryList")]
        [Permission("数据访问授权管理", "查询列表")]
        public async Task<ActionResult<AppServiceResult<PagedResult<DataAccessAuthorizeDto>>>> QueryListAsync(
            [FromBody] DataAccessAuthorizeParamsDto queryParams)
        {
            return await _dataAccessAuthorizeService.QueryListAsync(queryParams);
        }
    }
}
