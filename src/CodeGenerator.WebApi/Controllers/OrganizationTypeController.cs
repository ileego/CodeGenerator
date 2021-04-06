using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CodeGenerator.Application.DTOs.OrganizationType;
using CodeGenerator.Application.Services.OrganizationType;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;


namespace CodeGenerator.WebApi.Controllers
{
    /// <summary>
    /// 组织类型 - 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationTypeController : ControllerBase
    {
        private readonly IOrganizationTypeService _organizationTypeService;

        /// <summary>
        /// Ctor
        /// </summary>
        public OrganizationTypeController(IOrganizationTypeService organizationTypeService)
        {
            this._organizationTypeService = organizationTypeService;
        }

        /// <summary>
        /// 创建 - 组织类型
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Permission("组织类型管理", "创建")]
        public async Task<ActionResult<AppServiceResult<long>>> CreateAsync(
            [FromBody] OrganizationTypeInputDto inputDto)
        {
            return await _organizationTypeService.CreateAsync(inputDto);
        }

        /// <summary>
        /// 修改 - 组织类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        [Permission("组织类型管理", "修改")]
        public async Task<ActionResult<AppServiceResult>> UpdateAsync([FromRoute] long id,
            [FromBody] OrganizationTypeInputDto inputDto)
        {
            return await _organizationTypeService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 删除 - 组织类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}")]
        [Permission("组织类型管理", "删除")]
        public async Task<ActionResult<AppServiceResult>> DeleteAsync([FromRoute] long id)
        {
            return await _organizationTypeService.DeleteAsync(id);
        }

        /// <summary>
        /// 按Id查询 - 组织类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Permission("组织类型管理", "按Id查明细")]
        public async Task<ActionResult<AppServiceResult<OrganizationTypeDto>>> GetByIdAsync([FromRoute] long id)
        {
            return await _organizationTypeService.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询 - 组织类型
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("queryList")]
        [Permission("组织类型管理", "查询列表")]
        public async Task<ActionResult<AppServiceResult<PagedResult<OrganizationTypeDto>>>> QueryListAsync(
            [FromBody] OrganizationTypeParamsDto queryParams)
        {
            return await _organizationTypeService.QueryListAsync(queryParams);
        }
    }
}
