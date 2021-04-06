using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CodeGenerator.Application.DTOs.RoleAuthorize;
using CodeGenerator.Application.Services.RoleAuthorize;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;


namespace CodeGenerator.WebApi.Controllers
{
    /// <summary>
    /// 角色授权 - 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoleAuthorizeController : ControllerBase
    {
        private readonly IRoleAuthorizeService _roleAuthorizeService;

        /// <summary>
        /// Ctor
        /// </summary>
        public RoleAuthorizeController(IRoleAuthorizeService roleAuthorizeService)
        {
            this._roleAuthorizeService = roleAuthorizeService;
        }

        /// <summary>
        /// 创建 - 角色授权
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Permission("角色授权管理", "创建")]
        public async Task<ActionResult<AppServiceResult<long>>> CreateAsync(
            [FromBody] RoleAuthorizeInputDto inputDto)
        {
            return await _roleAuthorizeService.CreateAsync(inputDto);
        }

        /// <summary>
        /// 修改 - 角色授权
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        [Permission("角色授权管理", "修改")]
        public async Task<ActionResult<AppServiceResult>> UpdateAsync([FromRoute] long id,
            [FromBody] RoleAuthorizeInputDto inputDto)
        {
            return await _roleAuthorizeService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 删除 - 角色授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}")]
        [Permission("角色授权管理", "删除")]
        public async Task<ActionResult<AppServiceResult>> DeleteAsync([FromRoute] long id)
        {
            return await _roleAuthorizeService.DeleteAsync(id);
        }

        /// <summary>
        /// 按Id查询 - 角色授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Permission("角色授权管理", "按Id查明细")]
        public async Task<ActionResult<AppServiceResult<RoleAuthorizeDto>>> GetByIdAsync([FromRoute] long id)
        {
            return await _roleAuthorizeService.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询 - 角色授权
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("queryList")]
        [Permission("角色授权管理", "查询列表")]
        public async Task<ActionResult<AppServiceResult<PagedResult<RoleAuthorizeDto>>>> QueryListAsync(
            [FromBody] RoleAuthorizeParamsDto queryParams)
        {
            return await _roleAuthorizeService.QueryListAsync(queryParams);
        }
    }
}
