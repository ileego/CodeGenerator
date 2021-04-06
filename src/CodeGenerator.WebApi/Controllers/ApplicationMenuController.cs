using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CodeGenerator.Application.DTOs.ApplicationMenu;
using CodeGenerator.Application.Services.ApplicationMenu;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;


namespace CodeGenerator.WebApi.Controllers
{
    /// <summary>
    /// 应用菜单 - 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationMenuController : ControllerBase
    {
        private readonly IApplicationMenuService _applicationMenuService;

        /// <summary>
        /// Ctor
        /// </summary>
        public ApplicationMenuController(IApplicationMenuService applicationMenuService)
        {
            this._applicationMenuService = applicationMenuService;
        }

        /// <summary>
        /// 创建 - 应用菜单
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Permission("应用菜单管理", "创建")]
        public async Task<ActionResult<AppServiceResult<long>>> CreateAsync(
            [FromBody] ApplicationMenuInputDto inputDto)
        {
            return await _applicationMenuService.CreateAsync(inputDto);
        }

        /// <summary>
        /// 修改 - 应用菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        [Permission("应用菜单管理", "修改")]
        public async Task<ActionResult<AppServiceResult>> UpdateAsync([FromRoute] long id,
            [FromBody] ApplicationMenuInputDto inputDto)
        {
            return await _applicationMenuService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 删除 - 应用菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}")]
        [Permission("应用菜单管理", "删除")]
        public async Task<ActionResult<AppServiceResult>> DeleteAsync([FromRoute] long id)
        {
            return await _applicationMenuService.DeleteAsync(id);
        }

        /// <summary>
        /// 按Id查询 - 应用菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Permission("应用菜单管理", "按Id查明细")]
        public async Task<ActionResult<AppServiceResult<ApplicationMenuDto>>> GetByIdAsync([FromRoute] long id)
        {
            return await _applicationMenuService.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询 - 应用菜单
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("queryList")]
        [Permission("应用菜单管理", "查询列表")]
        public async Task<ActionResult<AppServiceResult<PagedResult<ApplicationMenuDto>>>> QueryListAsync(
            [FromBody] ApplicationMenuParamsDto queryParams)
        {
            return await _applicationMenuService.QueryListAsync(queryParams);
        }
    }
}
