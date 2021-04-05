using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CodeGenerator.Application.DTOs.Organization;
using CodeGenerator.Application.Services.Organization;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;


namespace CodeGenerator.WebApi.Controllers
{
    /// <summary>
    /// 组织 - 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        /// <summary>
        /// Ctor
        /// </summary>
        public OrganizationController(IOrganizationService organizationService)
        {
            this._organizationService = organizationService;
        }

        /// <summary>
        /// 创建 - 组织
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Permission("组织管理", "创建")]
        public async Task<ActionResult<AppServiceResult<long>>> CreateAsync(
            [FromBody] OrganizationInputDto inputDto)
        {
            return await _organizationService.CreateAsync(inputDto);
        }

        /// <summary>
        /// 修改 - 组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        [Permission("组织管理", "修改")]
        public async Task<ActionResult<AppServiceResult>> UpdateAsync([FromRoute] long id,
            [FromBody] OrganizationInputDto inputDto)
        {
            return await _organizationService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 删除 - 组织
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}")]
        [Permission("组织管理", "删除")]
        public async Task<ActionResult<AppServiceResult>> DeleteAsync([FromRoute] long id)
        {
            return await _organizationService.DeleteAsync(id);
        }

        /// <summary>
        /// 按Id查询 - 组织
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Permission("组织管理", "按Id查明细")]
        public async Task<ActionResult<AppServiceResult<OrganizationDto>>> GetByIdAsync([FromRoute] long id)
        {
            return await _organizationService.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询 - 组织
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("queryList")]
        [Permission("组织管理", "查询列表")]
        public async Task<ActionResult<AppServiceResult<PagedResult<OrganizationDto>>>> QueryListAsync(
            [FromBody] OrganizationParamsDto queryParams)
        {
            return await _organizationService.QueryListAsync(queryParams);
        }
    }
}
