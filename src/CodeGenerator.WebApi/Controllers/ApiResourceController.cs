using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CodeGenerator.Application.DTOs.ApiResource;
using CodeGenerator.Application.Services.ApiResource;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;


namespace CodeGenerator.WebApi.Controllers
{
    /// <summary>
    /// Api资源 - 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApiResourceController : ControllerBase
    {
        private readonly IApiResourceService _apiResourceService;

        /// <summary>
        /// Ctor
        /// </summary>
        public ApiResourceController(IApiResourceService apiResourceService)
        {
            this._apiResourceService = apiResourceService;
        }

        /// <summary>
        /// 创建 - Api资源
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        //[Permission("Api资源管理", "创建")]
        public async Task<ActionResult<AppServiceResult<long>>> CreateAsync(
            [FromBody] ApiResourceInputDto inputDto)
        {
            return await _apiResourceService.CreateAsync(inputDto);
        }

        /// <summary>
        /// 修改 - Api资源
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        //[Permission("Api资源管理", "修改")]
        public async Task<ActionResult<AppServiceResult>> UpdateAsync([FromRoute] long id,
            [FromBody] ApiResourceInputDto inputDto)
        {
            return await _apiResourceService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 删除 - Api资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}")]
        //[Permission("Api资源管理", "删除")]
        public async Task<ActionResult<AppServiceResult>> DeleteAsync([FromRoute] long id)
        {
            return await _apiResourceService.DeleteAsync(id);
        }

        /// <summary>
        /// 按Id查询 - Api资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        //[Permission("Api资源管理", "按Id查明细")]
        public async Task<ActionResult<AppServiceResult<ApiResourceDto>>> GetByIdAsync([FromRoute] long id)
        {
            return await _apiResourceService.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询 - Api资源
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("queryList")]
        //[Permission("Api资源管理", "查询列表")]
        public async Task<ActionResult<AppServiceResult<PagedResult<ApiResourceDto>>>> QueryListAsync(
            [FromBody] ApiResourceParamsDto queryParams)
        {
            return await _apiResourceService.QueryListAsync(queryParams);
        }
    }
}
