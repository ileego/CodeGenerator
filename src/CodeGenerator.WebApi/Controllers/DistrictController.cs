using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CodeGenerator.Application.DTOs.District;
using CodeGenerator.Application.Services.District;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;


namespace CodeGenerator.WebApi.Controllers
{
    /// <summary>
    /// 行政区 - 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService _districtService;

        /// <summary>
        /// Ctor
        /// </summary>
        public DistrictController(IDistrictService districtService)
        {
            this._districtService = districtService;
        }

        /// <summary>
        /// 创建 - 行政区
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Permission("行政区管理", "创建")]
        public async Task<ActionResult<AppServiceResult<long>>> CreateAsync(
            [FromBody] DistrictInputDto inputDto)
        {
            return await _districtService.CreateAsync(inputDto);
        }

        /// <summary>
        /// 修改 - 行政区
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        [Permission("行政区管理", "修改")]
        public async Task<ActionResult<AppServiceResult>> UpdateAsync([FromRoute] long id,
            [FromBody] DistrictInputDto inputDto)
        {
            return await _districtService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 删除 - 行政区
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}")]
        [Permission("行政区管理", "删除")]
        public async Task<ActionResult<AppServiceResult>> DeleteAsync([FromRoute] long id)
        {
            return await _districtService.DeleteAsync(id);
        }

        /// <summary>
        /// 按Id查询 - 行政区
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Permission("行政区管理", "按Id查明细")]
        public async Task<ActionResult<AppServiceResult<DistrictDto>>> GetByIdAsync([FromRoute] long id)
        {
            return await _districtService.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询 - 行政区
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("queryList")]
        [Permission("行政区管理", "查询列表")]
        public async Task<ActionResult<AppServiceResult<PagedResult<DistrictDto>>>> QueryListAsync(
            [FromBody] DistrictParamsDto queryParams)
        {
            return await _districtService.QueryListAsync(queryParams);
        }
    }
}
