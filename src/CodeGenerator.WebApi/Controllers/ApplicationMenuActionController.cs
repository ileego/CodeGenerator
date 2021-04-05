using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CodeGenerator.Application.DTOs.ApplicationMenuAction;
using CodeGenerator.Application.Services.ApplicationMenuAction;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;


namespace CodeGenerator.WebApi.Controllers
{
    /// <summary>
    /// 应用菜单功能关联 - 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationMenuActionController : ControllerBase
    {
        private readonly IApplicationMenuActionService _applicationMenuActionService;

        /// <summary>
        /// Ctor
        /// </summary>
        public ApplicationMenuActionController(IApplicationMenuActionService applicationMenuActionService)
        {
            this._applicationMenuActionService = applicationMenuActionService;
        }

        /// <summary>
        /// 创建 - 应用菜单功能关联
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Permission("应用菜单功能关联管理", "创建")]
        public async Task<ActionResult<AppServiceResult<long>>> CreateAsync(
            [FromBody] ApplicationMenuActionInputDto inputDto)
        {
            return await _applicationMenuActionService.CreateAsync(inputDto);
        }

        /// <summary>
        /// 修改 - 应用菜单功能关联
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        [Permission("应用菜单功能关联管理", "修改")]
        public async Task<ActionResult<AppServiceResult>> UpdateAsync([FromRoute] long id,
            [FromBody] ApplicationMenuActionInputDto inputDto)
        {
            return await _applicationMenuActionService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 删除 - 应用菜单功能关联
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}")]
        [Permission("应用菜单功能关联管理", "删除")]
        public async Task<ActionResult<AppServiceResult>> DeleteAsync([FromRoute] long id)
        {
            return await _applicationMenuActionService.DeleteAsync(id);
        }

        /// <summary>
        /// 按Id查询 - 应用菜单功能关联
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Permission("应用菜单功能关联管理", "按Id查明细")]
        public async Task<ActionResult<AppServiceResult<ApplicationMenuActionDto>>> GetByIdAsync([FromRoute] long id)
        {
            return await _applicationMenuActionService.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询 - 应用菜单功能关联
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("queryList")]
        [Permission("应用菜单功能关联管理", "查询列表")]
        public async Task<ActionResult<AppServiceResult<PagedResult<ApplicationMenuActionDto>>>> QueryListAsync(
            [FromBody] ApplicationMenuActionParamsDto queryParams)
        {
            return await _applicationMenuActionService.QueryListAsync(queryParams);
        }
    }
}
