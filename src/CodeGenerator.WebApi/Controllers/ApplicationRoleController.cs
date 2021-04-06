using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CodeGenerator.Application.DTOs.ApplicationRole;
using CodeGenerator.Application.Services.ApplicationRole;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;


namespace CodeGenerator.WebApi.Controllers
{
    /// <summary>
    /// 角色 - 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationRoleController : ControllerBase
    {
        private readonly IApplicationRoleService _applicationRoleService;

        /// <summary>
        /// Ctor
        /// </summary>
        public ApplicationRoleController(IApplicationRoleService applicationRoleService)
        {
            this._applicationRoleService = applicationRoleService;
        }

        /// <summary>
        /// 创建 - 角色
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Permission("角色管理", "创建")]
        public async Task<ActionResult<AppServiceResult<long>>> CreateAsync(
            [FromBody] ApplicationRoleInputDto inputDto)
        {
            return await _applicationRoleService.CreateAsync(inputDto);
        }

        /// <summary>
        /// 修改 - 角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        [Permission("角色管理", "修改")]
        public async Task<ActionResult<AppServiceResult>> UpdateAsync([FromRoute] long id,
            [FromBody] ApplicationRoleInputDto inputDto)
        {
            return await _applicationRoleService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 删除 - 角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}")]
        [Permission("角色管理", "删除")]
        public async Task<ActionResult<AppServiceResult>> DeleteAsync([FromRoute] long id)
        {
            return await _applicationRoleService.DeleteAsync(id);
        }

        /// <summary>
        /// 按Id查询 - 角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Permission("角色管理", "按Id查明细")]
        public async Task<ActionResult<AppServiceResult<ApplicationRoleDto>>> GetByIdAsync([FromRoute] long id)
        {
            return await _applicationRoleService.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询 - 角色
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("queryList")]
        [Permission("角色管理", "查询列表")]
        public async Task<ActionResult<AppServiceResult<PagedResult<ApplicationRoleDto>>>> QueryListAsync(
            [FromBody] ApplicationRoleParamsDto queryParams)
        {
            return await _applicationRoleService.QueryListAsync(queryParams);
        }
    }
}
