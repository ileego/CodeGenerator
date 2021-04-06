using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CodeGenerator.Application.DTOs.ApplicationSystem;
using CodeGenerator.Application.Services.ApplicationSystem;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;


namespace CodeGenerator.WebApi.Controllers
{
    /// <summary>
    /// 应用系统 - 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationSystemController : ControllerBase
    {
        private readonly IApplicationSystemService _applicationSystemService;

        /// <summary>
        /// Ctor
        /// </summary>
        public ApplicationSystemController(IApplicationSystemService applicationSystemService)
        {
            this._applicationSystemService = applicationSystemService;
        }

        /// <summary>
        /// 创建 - 应用系统
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Permission("应用系统管理", "创建")]
        public async Task<ActionResult<AppServiceResult<long>>> CreateAsync(
            [FromBody] ApplicationSystemInputDto inputDto)
        {
            return await _applicationSystemService.CreateAsync(inputDto);
        }

        /// <summary>
        /// 修改 - 应用系统
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        [Permission("应用系统管理", "修改")]
        public async Task<ActionResult<AppServiceResult>> UpdateAsync([FromRoute] long id,
            [FromBody] ApplicationSystemInputDto inputDto)
        {
            return await _applicationSystemService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 删除 - 应用系统
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}")]
        [Permission("应用系统管理", "删除")]
        public async Task<ActionResult<AppServiceResult>> DeleteAsync([FromRoute] long id)
        {
            return await _applicationSystemService.DeleteAsync(id);
        }

        /// <summary>
        /// 按Id查询 - 应用系统
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Permission("应用系统管理", "按Id查明细")]
        public async Task<ActionResult<AppServiceResult<ApplicationSystemDto>>> GetByIdAsync([FromRoute] long id)
        {
            return await _applicationSystemService.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询 - 应用系统
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("queryList")]
        [Permission("应用系统管理", "查询列表")]
        public async Task<ActionResult<AppServiceResult<PagedResult<ApplicationSystemDto>>>> QueryListAsync(
            [FromBody] ApplicationSystemParamsDto queryParams)
        {
            return await _applicationSystemService.QueryListAsync(queryParams);
        }
    }
}
