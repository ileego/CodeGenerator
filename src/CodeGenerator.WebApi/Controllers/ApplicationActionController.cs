using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CodeGenerator.Application.DTOs.ApplicationAction;
using CodeGenerator.Application.Services.ApplicationAction;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;


namespace CodeGenerator.WebApi.Controllers
{
    /// <summary>
    /// 应用功能 - 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationActionController : ControllerBase
    {
        private readonly IApplicationActionService _applicationActionService;

        /// <summary>
        /// Ctor
        /// </summary>
        public ApplicationActionController(IApplicationActionService applicationActionService)
        {
            this._applicationActionService = applicationActionService;
        }

        /// <summary>
        /// 创建 - 应用功能
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Permission("应用功能管理", "创建")]
        public async Task<ActionResult<AppServiceResult<long>>> CreateAsync(
            [FromBody] ApplicationActionInputDto inputDto)
        {
            return await _applicationActionService.CreateAsync(inputDto);
        }

        /// <summary>
        /// 修改 - 应用功能
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        [Permission("应用功能管理", "修改")]
        public async Task<ActionResult<AppServiceResult>> UpdateAsync([FromRoute] long id,
            [FromBody] ApplicationActionInputDto inputDto)
        {
            return await _applicationActionService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 删除 - 应用功能
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}")]
        [Permission("应用功能管理", "删除")]
        public async Task<ActionResult<AppServiceResult>> DeleteAsync([FromRoute] long id)
        {
            return await _applicationActionService.DeleteAsync(id);
        }

        /// <summary>
        /// 按Id查询 - 应用功能
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Permission("应用功能管理", "按Id查明细")]
        public async Task<ActionResult<AppServiceResult<ApplicationActionDto>>> GetByIdAsync([FromRoute] long id)
        {
            return await _applicationActionService.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询 - 应用功能
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("queryList")]
        [Permission("应用功能管理", "查询列表")]
        public async Task<ActionResult<AppServiceResult<PagedResult<ApplicationActionDto>>>> QueryListAsync(
            [FromBody] ApplicationActionParamsDto queryParams)
        {
            return await _applicationActionService.QueryListAsync(queryParams);
        }
    }
}
