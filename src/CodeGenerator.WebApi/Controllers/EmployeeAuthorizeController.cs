using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CodeGenerator.Application.DTOs.EmployeeAuthorize;
using CodeGenerator.Application.Services.EmployeeAuthorize;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;


namespace CodeGenerator.WebApi.Controllers
{
    /// <summary>
    /// 工作人员授权 - 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAuthorizeController : ControllerBase
    {
        private readonly IEmployeeAuthorizeService _employeeAuthorizeService;

        /// <summary>
        /// Ctor
        /// </summary>
        public EmployeeAuthorizeController(IEmployeeAuthorizeService employeeAuthorizeService)
        {
            this._employeeAuthorizeService = employeeAuthorizeService;
        }

        /// <summary>
        /// 创建 - 工作人员授权
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Permission("工作人员授权管理", "创建")]
        public async Task<ActionResult<AppServiceResult<long>>> CreateAsync(
            [FromBody] EmployeeAuthorizeInputDto inputDto)
        {
            return await _employeeAuthorizeService.CreateAsync(inputDto);
        }

        /// <summary>
        /// 修改 - 工作人员授权
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        [Permission("工作人员授权管理", "修改")]
        public async Task<ActionResult<AppServiceResult>> UpdateAsync([FromRoute] long id,
            [FromBody] EmployeeAuthorizeInputDto inputDto)
        {
            return await _employeeAuthorizeService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 删除 - 工作人员授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}")]
        [Permission("工作人员授权管理", "删除")]
        public async Task<ActionResult<AppServiceResult>> DeleteAsync([FromRoute] long id)
        {
            return await _employeeAuthorizeService.DeleteAsync(id);
        }

        /// <summary>
        /// 按Id查询 - 工作人员授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Permission("工作人员授权管理", "按Id查明细")]
        public async Task<ActionResult<AppServiceResult<EmployeeAuthorizeDto>>> GetByIdAsync([FromRoute] long id)
        {
            return await _employeeAuthorizeService.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询 - 工作人员授权
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("queryList")]
        [Permission("工作人员授权管理", "查询列表")]
        public async Task<ActionResult<AppServiceResult<PagedResult<EmployeeAuthorizeDto>>>> QueryListAsync(
            [FromBody] EmployeeAuthorizeParamsDto queryParams)
        {
            return await _employeeAuthorizeService.QueryListAsync(queryParams);
        }
    }
}
